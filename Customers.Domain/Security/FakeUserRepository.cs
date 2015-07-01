using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.Security
{
    public class FakeUserRepository : IUserInformationRepository
    {
        private static List<UserInformation> userInformations = new List<UserInformation>();

        public FakeUserRepository()
        {
            if (userInformations.Count == 0)
            {
                userInformations.Add(new UserInformation()
                {
                    UserName = "admin",
                    Password = "Abcdef9*",
                    EMail = "ouyh19@189.cn",
                    PasswordQuestion = "How old are you?",
                    PasswordAnswer = "21st Century.",
                    IsApproved = true,
                    CityPermissioin = (1 << 21) - 1
                });
            }
        }

        public void ResetUsers()
        {
            userInformations.Clear();
            userInformations.Add(new UserInformation()
            {
                UserName = "admin",
                Password = "Abcdef9*",
                EMail = "ouyh19@189.cn",
                PasswordQuestion = "How old are you?",
                PasswordAnswer = "21st Century.",
                IsApproved = true,
                CityPermissioin = 1 << 21 - 1
            });
        }

        public IQueryable<UserInformation> UserInformations
        {
            get { return userInformations.AsQueryable(); }
        }

        public UserInformation AddUser(UserInformation user)
        {
            if (user != null)
            {
                UserInformation existedUser = userInformations.FirstOrDefault(
                    x => x.UserName == user.UserName);
                if (existedUser == null) { userInformations.Add(user); }
                return existedUser;
            }
            return user;
        }

        public bool DeleteUser(string username)
        {
            UserInformation existedUser = userInformations.FirstOrDefault(
                x => x.UserName == username);
            return (existedUser != null) ? userInformations.Remove(existedUser) : false;
        }

        public void SaveChanges()
        {            
        }
    }
}
