using System;
using Authorization.Entities;

namespace Authorization.Entities.UserServices
{
    public class UserServices
    {
        public User RegisterUser;

        public User Registaration(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Заполните это поле");
            }

            RegisterUser  = new User()
            {
                Username = username,
                Password = password
            };
            return RegisterUser;


        }

        public bool Login(string username, string password)
        {
            if (RegisterUser == null)
            {
                Console.WriteLine("Ваедите логин и пароль");
                return false;
            }
            if (RegisterUser.Username == username && RegisterUser.Password == password)
            {
                Console.WriteLine("Вы зарегистрировались на 1XBET");
                return true;
            }
            else
            {
                Console.WriteLine("Неверно введен пароль или логин Пашол Нахуй далбаёб");
                return false;
            }
        }
    }
}