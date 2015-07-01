using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Customers.Domain.Security
{
    public class EFUserRepository : IUserInformationRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<UserInformation> UserInformations
        {
            get { return context.UserInformations; }
        }

        public UserInformation AddUser(UserInformation user)
        {
            if (user != null)
            {
                UserInformation existedUser = UserInformations.FirstOrDefault(
                    x => x.UserName == user.UserName);
                if (existedUser == null) 
                { 
                    context.UserInformations.Add(user);
                    context.SaveChanges();
                }
                return existedUser;
            }
            return user;
        }

        public bool DeleteUser(string username)
        {
            UserInformation existedUser = UserInformations.FirstOrDefault(
                x => x.UserName == username);
            UserInformation user = context.UserInformations.Remove(existedUser);
            context.SaveChanges();
            return (existedUser != null) ? user != null : false;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
