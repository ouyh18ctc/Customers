using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;
using Customers.Domain.Security;

namespace Customers.UnitTests.Security
{
    [TestClass]
    public class UserInformationQueriesTest
    {
        private IUserInformationRepository repository = new FakeUserRepository();

        [TestMethod]
        public void TestUserInformationQueries_PermitOneCity()
        {
            repository.UserInformations.ElementAt(0).PermissionList =
                new int[1] { 0 };
            Assert.AreEqual(repository.UserInformations.ElementAt(0).PermissionList.Count(), 1);
            Assert.AreEqual((CityDef)repository.UserInformations.ElementAt(0).PermissionList.ElementAt(0),
                CityDef.Shenzhen);
            Assert.IsNull(repository.UserInformations.ElementAt(0).PermissionList.Select(
                x => ((CityDef)x).GetCityName()).FirstOrDefault(x => x == "佛山"));
            Assert.IsTrue(repository.UserCanEditCity("admin", "深圳"));
            Assert.IsFalse(repository.UserCanEditCity("admin", "佛山"));
            Assert.IsFalse(repository.UserCanEditCity("admin", "揭阳"));
            Assert.IsFalse(repository.UserCanEditCity("admin", "汕尾"));
        }

        [TestMethod]
        public void TestUserInformationQueries_PermitMultiCities()
        {
            repository.UserInformations.ElementAt(0).PermissionList =
                new int[3] { 0, 3, 19 };
            Assert.AreEqual(repository.UserInformations.ElementAt(0).PermissionList.Count(), 3);
            Assert.AreEqual((CityDef)repository.UserInformations.ElementAt(0).PermissionList.ElementAt(0),
                CityDef.Shenzhen);
            Assert.IsNotNull(repository.UserInformations.ElementAt(0).PermissionList.Select(
                x => ((CityDef)x).GetCityName()).FirstOrDefault(x => x == "佛山"));
            Assert.IsTrue(repository.UserCanEditCity("admin", "深圳"));
            Assert.IsTrue(repository.UserCanEditCity("admin", "佛山"));
            Assert.IsFalse(repository.UserCanEditCity("admin", "揭阳"));
            Assert.IsTrue(repository.UserCanEditCity("admin", "汕尾"));
        }
    }
}
