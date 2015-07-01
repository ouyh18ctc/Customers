using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TypeDef;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.Security
{
    public interface IUserInformationRepository
    {
        IQueryable<UserInformation> UserInformations { get; }

        UserInformation AddUser(UserInformation user);

        bool DeleteUser(string username);

        void SaveChanges();
    }

    public static class UserInformationQueries
    {
        public static bool UserCanEditCity(this IUserInformationRepository repository,
            string username, string cityname)
        {
            UserInformation user = repository.UserInformations.FirstOrDefault(
                x => x.UserName == username);
            return (user == null) ? false :
                user.PermissionList.Select(x => ((CityDef)x).GetCityName()).FirstOrDefault(
                    x => x == cityname) != null;
        }

        public static IEnumerable<string> GetPermissionCities(this IUserInformationRepository repository,
            string username)
        {
            UserInformation user = repository.UserInformations.FirstOrDefault(
                x => x.UserName == username);
            return (user == null) ? null :
                user.PermissionList.Select(x => ((CityDef)x).GetCityName());
        }

        public static bool SetPermissionCities(this IUserInformationRepository repository,
            string username)
        {
            IEnumerable<string> cityList = repository.GetPermissionCities(username);
            if (cityList == null || cityList.Count() == 0)
            {
                return false;
            }
            IEnumerable<ListItem> list =
                cityList.Select(x => new ListItem() { Value = x, Text = x });
            ListProviders.SetListProvider(() => new DemandListProvider(list));
            return true;
        }
    }
}
