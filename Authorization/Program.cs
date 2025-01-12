using System;
using Authorization.Entities;
using Authorization.Entities.UserServices;

namespace Authorization
{
    class Program
    {
        static void Main(string[] args)
        {
            UserServices userService = new UserServices();
            Console.Clear();

            while (true)
            {
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Логин");
                Console.WriteLine("3. Выход из системы");
                Console.WriteLine("Выберите параметр");

                string number = Console.ReadLine(); 
                switch (number)
                {
                    case "1":
                        Console.WriteLine("Введите имя пользователя:");
                        string regUsername = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        string regPassword = Console.ReadLine();
                        userService.Registaration(regUsername, regPassword);
                        break;
                    case "2":
                        Console.WriteLine("Введите имя пользователя для входа:");
                        string loginUsername = Console.ReadLine();
                        Console.WriteLine("Введите пароль для входа:");
                        string loginPassword = Console.ReadLine();
                        userService.Login(loginUsername, loginPassword);
                        break;
                    case "3":
                        Console.WriteLine("Выходим из системы");
                        return; 
                    default:
                        Console.WriteLine("Некорректный выбор, попробуйте снова.");
                        break;
                }
            }
        }
    }
}