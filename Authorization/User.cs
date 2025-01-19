using System;

namespace Authorization.Entities
{
    public sealed class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
#nullable enable
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Balance { get; set; }
#nullable disable
        public User()
        {
            Id = Guid.NewGuid();
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Balance = 0;
        }

        // Добавление метода Transfer для перевода средств
        public bool Transfer(User recipient, decimal amount)
        {
            if (this.Balance >= amount)
            {
                this.Balance -= amount;
                recipient.Balance += amount;
                return true;
            }
            return false;
        }
    }
}