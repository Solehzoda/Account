using System;

namespace Authorization.Entities

{
    public sealed class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }
    }
}