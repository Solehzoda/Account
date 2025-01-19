using System;
using System.Collections.Generic;
using System.Linq;
using Authorization.Entities;

namespace Authorization.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            UserServices userServices = new();

            // Пример добавления пользователей
            User user1 = new() { Username = "IKROM", Password = "password123" };
            User user2 = new() { Username = "ALINA", Password = "password456" };
            userServices.RegisteredUsers.Add(user1);
            userServices.RegisteredUsers.Add(user2);

            BankAccount account1 = new() { Username = "IKROM", Balance = 1000, USer = user1 };
            BankAccount account2 = new() { Username = "ALINA", Balance = 500, USer = user2 };
            userServices.BankAccounts.Add(account1);
            userServices.BankAccounts.Add(account2);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Регистрация");
                Console.WriteLine("2 - Авторизация");
                Console.WriteLine("3 - Вызов всех пользователей");
                Console.WriteLine("4 - Создание аккаунта ");
                Console.WriteLine("5 - Пополнение");
                Console.WriteLine("6 - Снятие счёта");
                Console.WriteLine("7 - Информация о пользователе");
                Console.WriteLine("8 - Перевод средств");
                Console.WriteLine("9 - Выход");
                Console.WriteLine("Выберите параметр:");
                string input = Console.ReadLine();
                int number;
                if (!int.TryParse(input, out number))
                {
                    Console.WriteLine("Ошибка ввода");
                    continue;
                }

                switch (number)
                {
                    case 1:
                        Console.WriteLine("Введите имя:");
                        string regUsername = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        string regPassword = Console.ReadLine();
                        userServices.Registration(regUsername, regPassword);
                        break;
                    case 2:
                        Console.WriteLine("Вход:");
                        string loginUsername = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        string loginPassword = Console.ReadLine();
                        userServices.Login(loginUsername, loginPassword);
                        break;
                    case 3:
                        userServices.GetUsers();
                        break;
                    case 4:
                        Console.WriteLine("Введите своё имя:");
                        string accountName = Console.ReadLine();
                        userServices.CreateBankAccount(Guid.NewGuid(), accountName);
                        break;
                    case 5:
                        Console.WriteLine("Введите имя:");
                        string depositName = Console.ReadLine();
                        Console.WriteLine("Введите сумму для пополнения:");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        userServices.Deposit(depositName, depositAmount);
                        break;
                    case 6:
                        Console.WriteLine("Введите имя:");
                        string withdrawName = Console.ReadLine();
                        Console.WriteLine("Введите сумму для снятия:");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        userServices.Withdraw(withdrawName, withdrawAmount);
                        break;
                    case 7:
                        Console.WriteLine("Введите имя пользователя:");
                        string accountInfoName = Console.ReadLine();
                        userServices.GetAccountInfo(accountInfoName);
                        break;
                    case 8:
                        Console.WriteLine("Введите имя отправителя:");
                        string senderUsername = Console.ReadLine();
                        Console.WriteLine("Введите имя получателя:");
                        string recipientUsername = Console.ReadLine();
                        Console.WriteLine("Введите сумму для перевода:");
                        decimal transferAmount = decimal.Parse(Console.ReadLine());
                        userServices.Transfer(senderUsername, recipientUsername, transferAmount);
                        break;
                    case 9:
                        Console.WriteLine("Спасибо за использование программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }

                Console.ReadKey();
            }
        }
    }

    public class UserServices
    {
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

            var newUser = new User
            {
                Username = username,
                Password = password
            };
            RegisteredUsers.Add(newUser);
            CreateBankAccount(newUser.Id, newUser.Username);
            return true;
        }

        public bool Login(string username, string password)
        {
            foreach (var registeredUser in RegisteredUsers)
            {
                if (registeredUser.Username == username && registeredUser.Password == password)
                {
                    Console.WriteLine("Вы успешно вошли!");
                    return true;
                }
            }

            Console.WriteLine("Имя или пароль неверны!");
            return false;
        }

        public void GetUsers()
        {
            if (RegisteredUsers.Count == 0)
            {
                Console.WriteLine("Нет зарегистрированных пользователей.");
                return;
            }

            Console.WriteLine("Лист пользователей:");
            foreach (var user in RegisteredUsers)
            {
                Console.WriteLine($"Пользователь: {user.Username}");
            }
        }

        public void CreateBankAccount(Guid userId, string username)
        {
            var bankAccount = new BankAccount()
            {
                UserID = userId,
                Username = username,
                Balance = 0
            };
            BankAccounts.Add(bankAccount);
            Console.WriteLine("Банковский аккаунт создан для пользователя.");
        }

        public void Deposit(string username, decimal amount)
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

        public void Withdraw(string username, decimal amount)
        {
            foreach (var bankAccount in BankAccounts)
            {
                if (bankAccount.Username == username)
                {
                    if (bankAccount.Balance >= amount)
                    {
                        bankAccount.Balance -= amount;
                        Console.WriteLine($"Снято {amount}. Новый баланс: {bankAccount.Balance}");
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

        public void GetAccountInfo(string username)
        {
            foreach (var bankAccount in BankAccounts)
            {
                if (bankAccount.Username == username)
                {
                    Console.WriteLine($"Информация о счете {username}:");
                    Console.WriteLine($"Баланс: {bankAccount.Balance}");
                    return;
                }
            }

            Console.WriteLine("Пользователь не найден!");
        }

        // Новый метод для перевода средств
        public void Transfer(string senderUsername, string recipientUsername, decimal amount)
        {
            BankAccount senderAccount = null;
            BankAccount recipientAccount = null;

            foreach (var bankAccount in BankAccounts)
            {
                if (bankAccount.Username == senderUsername)
                {
                    senderAccount = bankAccount;
                }
                else if (bankAccount.Username == recipientUsername)
                {
                    recipientAccount = bankAccount;
                }
            }

            if (senderAccount == null)
            {
                Console.WriteLine("Отправитель не найден.");
                return;
            }

            if (recipientAccount == null)
            {
                Console.WriteLine("Получатель не найден.");
                return;
            }

            if (senderAccount.Balance >= amount)
            {
                senderAccount.Balance -= amount;
                recipientAccount.Balance += amount;
                Console.WriteLine($"Перевод {amount} от {senderUsername} к {recipientUsername} успешно выполнен.");
                Console.WriteLine($"Новый баланс {senderUsername}: {senderAccount.Balance}");
                Console.WriteLine($"Новый баланс {recipientUsername}: {recipientAccount.Balance}");
            }
            else
            {
                Console.WriteLine("Недостаточно средств для перевода.");
            }
        }
    }
}
