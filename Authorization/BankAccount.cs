using System;
using Authorization.Entities;

namespace Authorization
{
    public class BankAccount
    {
        public Guid ID { get; set; }
        public Guid USerID { get; set; }
        public string Username { get; set; }
        public decimal Balance { get; set; }
        public User User { get; set; }

        public BankAccount()
        {
            ID = Guid.NewGuid();
            Balance = 0;
        }
    }
}