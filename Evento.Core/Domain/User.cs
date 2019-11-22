using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Core.Domain
{
    public class User : Entity
    {
        private static List<string> _role = new List<string> { "user", "admin" };
        public string Role { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        //public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected User()
        {
        }

        public User(Guid id, string role, string name, 
            string email, string password)
        {
            Id = id;
            SetRole(role);
            SetName(name);
            SetEmail(email);
            SetPassword(password);
            CreatedAt = DateTime.UtcNow;
        }

        //========== Setting and validation methods ==========
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Value of name can not be null or empty");

            Name = name;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                throw new Exception("Value of role can not be null or empty");

            if (!_role.Contains(role))
                throw new Exception($"New user can not heve '{role}' role.");

            Role = role;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Value of email can not be null or empty");

            Email = email;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("Value of password can not be null or empty");

            Password = password;
        }
    }
}
