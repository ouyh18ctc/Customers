using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.Security;
using System.Web.Security;

namespace Customers.UnitTests.Security
{
    [TestClass]
    public class TwoUsersMembershipProviderTest
    {
        FakeUserRepository repository = new FakeUserRepository();
        CustomerMembershipProvider provider;

        [TestInitialize]
        public void TestInitialize()
        {
            repository.ResetUsers();
            provider = new CustomerMembershipProvider(repository);
        }

        [TestMethod]
        public void TestCustomerMemembershipProvider_CreateOneUser_Success()
        {
            MembershipCreateStatus status = new MembershipCreateStatus();
            Assert.IsNull(provider.CreateUser("foshan", "123456", "", "abc", "123", true, 4, out status));
            Assert.AreEqual(status, MembershipCreateStatus.Success);
            Assert.AreEqual(repository.UserInformations.Count(), 2);
        }

        [TestMethod]
        public void TestCustomerMemembershipProvider_CreateOneUser_Fail()
        {
            MembershipCreateStatus status = new MembershipCreateStatus();
            Assert.IsNull(provider.CreateUser("admin", "123456", "", "abc", "123", true, 4, out status));
            Assert.AreEqual(status, MembershipCreateStatus.DuplicateUserName);
            Assert.AreEqual(repository.UserInformations.Count(), 1);
        }
    }
}
