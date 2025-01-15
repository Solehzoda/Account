using System;
using System.Collections.Generic;
using System.Linq;
using Authorization.Entities;

namespace Authorization.Entities.UserServices
{
    public class UserServices
    {
        public User RegisteredUser;
        public List<User> RegisteredUsers;

        public List<User> Registration(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))

            {
                Console.WriteLine("Username or password is empty");
            }

            RegisteredUsers = new List<User>()
            {
                new User()
                {
                    Username = username,
                    Password = password
                }

            };
            return RegisteredUsers.ToList();
        }

        public bool Login(string username, string password)
        {
            foreach (var registereduser in RegisteredUsers)
            {
                if (registereduser == null)
                {
                    Console.WriteLine("Введите пароль!");
                    return false;
                }

                if (registereduser.Username == username && registereduser.Password == password)
                {
                    Console.WriteLine("Вы успешно зарегистрировались");
                    return true;
                }

                else
                {
                    Console.WriteLine("Неверно введен пароли и логин");
                    return false;
                }
            }

            return false;
        }

        public List<User> GetUsers()
        {
            return RegisteredUsers;
        }
    }
}