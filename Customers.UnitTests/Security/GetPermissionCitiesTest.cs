using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.Security;

namespace Customers.UnitTests.Security
{
    [TestClass]
    public class GetPermissionCitiesTest
    {
        private IUserInformationRepository userRepository = new FakeUserRepository();
        
        [TestMethod]
        public void TestGetPermissionCities()
        {
            IEnumerable<string> cityList = userRepository.GetPermissionCities("admin");
            Assert.AreEqual(cityList.Count(), 1);
        }

        [TestMethod]
        public void TestGetPermissionCities_ModifyPermissions()
        {
            userRepository.UserInformations.ElementAt(0).PermissionList =
                new int[3] { 1, 3, 7 };
            IEnumerable<string> cityList = userRepository.GetPermissionCities("admin");
            Assert.AreEqual(cityList.Count(), 3);
            Assert.AreEqual(cityList.ElementAt(0), "广州");
            Assert.AreEqual(cityList.ElementAt(1), "佛山");
            Assert.AreEqual(cityList.ElementAt(2), "江门");
        }
    }
}
