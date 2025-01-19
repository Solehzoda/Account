using System;

namespace Authorization.Entities
{
    public sealed class BankAccount
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public decimal Balance { get; set; }
        public User USer { get; set; }

        public BankAccount()
        {
            Id = Guid.NewGuid();
            Balance = 0;
            Username = string.Empty;
        }

        // Метод для перевода средств между аккаунтами
        public bool Transfer(BankAccount recipient, decimal amount)
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