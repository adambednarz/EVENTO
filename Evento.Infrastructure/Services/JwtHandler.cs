using Evento.Infrastructure.Dto;
using Evento.Infrastructure.Extensions;
using Evento.Infrastructure.Services.Interfaces;
using Evento.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Evento.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSetting _jwtSetting;
        public JwtHandler(IOptions<JwtSetting> options)
        {
            _jwtSetting = options.Value;
        }
        public JwtDto CreateJwtToken(Guid userId, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString())
            };

            var expires = now.AddMinutes(_jwtSetting.ExpiryMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expires = expires.ToTimestamp()
            };
        }
    }
}
