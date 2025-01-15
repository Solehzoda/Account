using System;
using System.Collections.Generic;
using Authorization.Entities;
using Authorization.Entities.UserServices;

namespace Authorization
{
    class Program
    {
        static void Main(string[] args)
        {
             User user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = "22233",
            };

            List<User> users = new()
            {
                new User { Id = Guid.NewGuid(), Username = "Faranush", Password = "123456" },
                new User { Id = Guid.NewGuid(), Username = "Aziz", Password = "123456" },
                new User { Id = Guid.NewGuid(), Username = "Alex", Password = "123456" },
            };

            UserServices userServices = new();
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Register");
                Console.WriteLine("2 - Login");
                Console.WriteLine("3 - вызов всех юсеров");
                Console.WriteLine("4 - Logout");
                Console.WriteLine("Выберите параметр:");
                string input = Console.ReadLine();
                int number;
                if (!int.TryParse(input, out number))
                {
                    Console.WriteLine("Oshibka vvoda");
                    continue;
                }

                switch (number)
                {
                    case 1:
                        Console.WriteLine("vvedite imya:");
                        string regUsername = Console.ReadLine();
                        Console.WriteLine("vvedite parol");
                        string regPassword = Console.ReadLine();
                        userServices.Registration(regUsername, regPassword);
                        break;
                    case 2:
                        Console.WriteLine("vvedite imya:");
                        string LoginUsername = Console.ReadLine();
                        Console.WriteLine("vvedite parol:");
                        string LoginPassword = Console.ReadLine();
                        userServices.Login(LoginUsername, LoginPassword);
                        if (userServices.Login(LoginUsername, LoginPassword))
                        {
                            Console.WriteLine("Dobro pjdhalovat:" + LoginUsername + "\nvash balance:" + user.Balance);
                        }

                        break;
                    case 3:
                        foreach (var USer in users)
                        {
                            Console.WriteLine($"Id: {USer.Id}, Username: {USer.Username}, Password: {USer.Password}");
                        }

                        break;

                    case 4:
                        Console.WriteLine("ВЫХОДИМ ИЗ ЁБАННОЙ СИСТЕМЫ");
                        break;
                    default:
                        Console.WriteLine("SPASIBO");
                        break;
                }

                Console.ReadKey();
            }
        }
    }
}