using System;
using System.Collections.Generic;
using Authorization.Entities;

namespace Authorization
{
    public class UserServices
    {
        public User RegisteredUser; // Registration
        public List<User> RegisteredUsers { get; set; } = new List<User>();
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

        public bool Registration(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Имя или пароль пустой");
                return false;
            }

            foreach (var user in RegisteredUsers)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("Такое имя уже занято");
                    return false;
                }
            }

            var newUser = new User  // Adding the new Users
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password
            };
            RegisteredUsers.Add(newUser);
            CreateBankAccount(newUser.Id, newUser.Username);
            return true;
        }

        public bool Login(string username, string password) // Login
        {
            foreach (var registeredUser in RegisteredUsers)
            {
                if (registeredUser.Username == username && registeredUser.Password == password)
                {
                    Console.WriteLine("Вы успешно зарегистрировались!");
                    return true;
                }
                else
                {
                    Console.WriteLine("12345678");
                    return false;
                }
            }

            Console.WriteLine("Имя или пароль неверны!");
            return false;
        }

        public void GetUsers()   // List of Users in baza
        {
            if (RegisteredUsers.Count == 0)
            {
                Console.WriteLine("Это не зарегистрированный пользователь");
                return;
            }

            Console.WriteLine("Лист пользователей:");
            foreach (var user in RegisteredUsers)
            {
                Console.WriteLine($"Пользователь: {user.Username}");
            }
        }

        public void CreateBankAccount(Guid userId, string username) // Creating the bankaccount for users with ID
        {
            var bankAccount = new BankAccount()
            {
                Username = username,
                Balance = 0
            };
            BankAccounts.Add(bankAccount);
            Console.WriteLine("Банковский аккаунт создан для пользователя.");
        }

        public void Deposit(string username, decimal amount) // Deposit of Users
        {
            foreach (var bankAccount in BankAccounts)
            {
                if (bankAccount.Username == username)
                {
                    bankAccount.Balance += amount;
                    Console.WriteLine($"Аккаунт пополнен на {amount}. Новый баланс: {bankAccount.Balance}");
                    return;
                }
            }

            Console.WriteLine("Пользователь не найден!");
        }

        public void Withdraw(string username, decimal amount)  // Get or send deposit to or from Users
        {
            foreach (var bankAccount in BankAccounts)
            {
                if (bankAccount.Username == username)
                {
                    if (bankAccount.Balance >= amount)
                    {
                        bankAccount.Balance -= amount;
                        Console.WriteLine($"Сумма {amount} снята. Новый баланс: {bankAccount.Balance}");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно средств.");
                        return;
                    }
                }
            }

            Console.WriteLine("Пользователь не найден!");
        }

        public void GetAccountInfo(string username) // Info about bankaccount User
        {
            foreach (var bankAccount in BankAccounts)
            {
                if (bankAccount.Username == username)
                {
                    Console.WriteLine($"Информация о аккаунте {username}:");
                    Console.WriteLine($"Валюта: USD (example)");
                    Console.WriteLine($"Баланс: {bankAccount.Balance}");
                    return;
                }
            }

            Console.WriteLine("Пользователь не найден!");
        }

        public void Transfer(string senderUsername, string recipientUsername, decimal amount) // Transfer of Users send money or get money from their bankaccount
        {
            var senderAccount = BankAccounts.Find(acc => acc.Username == senderUsername);
            var recipientAccount = BankAccounts.Find(acc => acc.Username == recipientUsername);

            if (senderAccount == null)
            {
                Console.WriteLine($"Пользователь-отправитель {senderUsername} не найден!");
                return;
            }

            if (recipientAccount == null)
            {
                Console.WriteLine($"Пользователь-получатель {recipientUsername} не найден!");
                return;
            }

            if (senderAccount.Balance < amount)
            {
                Console.WriteLine($"Недостаточно средств на счету {senderUsername} для перевода {amount}.");
                return;
            }

            senderAccount.Balance -= amount; // Отправитель
            recipientAccount.Balance += amount; // Получатель

            Console.WriteLine($"Перевод {amount} от {senderUsername} к {recipientUsername} выполнен успешно.");
            Console.WriteLine($"Новый баланс {senderUsername}: {senderAccount.Balance}");
            Console.WriteLine($"Новый баланс {recipientUsername}: {recipientAccount.Balance}");
        }
    }
}
