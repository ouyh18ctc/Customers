using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.Security;

namespace Customers.UnitTests.Security
{
    [TestClass]
    public class UserInformationPermissionTest
    {
        [TestMethod]
        public void TestUserInformationPermission_Get_7()
        {
            UserInformation info = new UserInformation() { CityPermissioin = 7 };
            IEnumerable<int> permissionList = info.PermissionList;
            Assert.AreEqual(permissionList.Count(), 3);
            Assert.AreEqual(permissionList.ElementAt(0), 0);
            Assert.AreEqual(permissionList.ElementAt(1), 1);
            Assert.AreEqual(permissionList.ElementAt(2), 2);
        }

        [TestMethod]
        public void TestUserInformationPermission_Get_65534()
        {
            UserInformation info = new UserInformation() { CityPermissioin = 65534 };
            IEnumerable<int> permissionList = info.PermissionList;
            Assert.AreEqual(permissionList.Count(), 15);
            Assert.AreEqual(permissionList.ElementAt(0), 1);
            Assert.AreEqual(permissionList.ElementAt(1), 2);
            Assert.AreEqual(permissionList.ElementAt(2), 3);
            Assert.AreEqual(permissionList.ElementAt(3), 4);
            Assert.AreEqual(permissionList.ElementAt(14), 15);
        }

        [TestMethod]
        public void TestUserInformationPermission_Set_7()
        {
            UserInformation info = new UserInformation();
            int[] indexList = new int[3] { 0, 1, 2 };
            info.PermissionList = indexList;
            Assert.AreEqual(info.CityPermissioin, 7);
        }

        [TestMethod]
        public void TestUserInformationPermission_Set_4487()
        {
            UserInformation info = new UserInformation();
            int[] indexList = new int[6] { 0, 1, 2, 7, 8, 12 };
            info.PermissionList = indexList;
            Assert.AreEqual(info.CityPermissioin, 4487);
        }

        [TestMethod]
        public void TestUserInformationPermission_SetAndGet()
        {
            UserInformation info = new UserInformation();
            info.PermissionList = new int[4] { 3, 6, 8, 13 };
            Assert.AreEqual(info.PermissionList.ElementAt(0), 3);
            Assert.AreEqual(info.PermissionList.ElementAt(1), 6);
            Assert.AreEqual(info.PermissionList.ElementAt(2), 8);
            Assert.AreEqual(info.PermissionList.ElementAt(3), 13); 
        }
    }
}
