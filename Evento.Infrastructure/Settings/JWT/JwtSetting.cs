using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Infrastructure.Settings
{
    public class JwtSetting
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int  ExpiryMinutes { get; set; }
        

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}
