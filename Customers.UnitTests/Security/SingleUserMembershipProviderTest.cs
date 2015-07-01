using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.Security;

namespace Customers.UnitTests.Security
{
    [TestClass]
    public class SingleUserMembershipProviderTest
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
        public void TestCustomerMembershipProvider_BasicParameters()
        {
            Assert.AreEqual(1 << 21 - 1, 1048576);
            Assert.AreEqual(repository.UserInformations.Count(), 1);
            Assert.AreEqual(repository.UserInformations.ElementAt(0).UserName, "admin");
            Assert.AreEqual(repository.UserInformations.ElementAt(0).Password, "Abcdef9*");
            Assert.AreEqual(repository.UserInformations.ElementAt(0).CityPermissioin, 1048576);
        }

        [TestMethod]
        public void TestCustomerMembershipProvider_ChangePassword_Success()
        {
            Assert.IsTrue(provider.ChangePassword("admin", "Abcdef9*", "123456"));
            Assert.AreEqual(repository.UserInformations.ElementAt(0).Password, "123456");
        }

        [TestMethod]
        public void TestCustomerMembershipProvider_ChangePassword_Fail()
        {
            Assert.IsFalse(provider.ChangePassword("admin", "Abcdef8*", "123456"));
            Assert.AreEqual(repository.UserInformations.ElementAt(0).Password, "Abcdef9*");
        }

        [TestMethod]
        public void TestCustomerMembershipProvider_ChangePasswordQuestionAndAnswer_Success()
        {
            Assert.IsTrue(provider.ChangePasswordQuestionAndAnswer("admin", "Abcdef9*", 
                "Have a lunch Today?", "I don't know!"));
            Assert.AreEqual(repository.UserInformations.ElementAt(0).PasswordQuestion, "Have a lunch Today?");
            Assert.AreEqual(repository.UserInformations.ElementAt(0).PasswordAnswer, "I don't know!");
        }

        [TestMethod]
        public void TestCustomerMembershipProvider_ChangePasswordQuestionAndAnswer_Fail()
        {
            Assert.IsFalse(provider.ChangePasswordQuestionAndAnswer("admin", "Abcdef8*",
                "Have a lunch Today?", "I don't know!"));
            Assert.AreEqual(repository.UserInformations.ElementAt(0).PasswordQuestion, "How old are you?");
            Assert.AreEqual(repository.UserInformations.ElementAt(0).PasswordAnswer, "21st Century.");
        }

        [TestMethod]
        public void TestCustomerMembershipProvider_GetPassword_Success()
        {
            Assert.AreEqual(provider.GetPassword("admin", "21st Century."), "Abcdef9*");
        }

        [TestMethod]
        public void TestCustomerMembershipProvider_GetPassword_Fail()
        {
            Assert.IsNull(provider.GetPassword("admin", "22 Century."));
        }

        [TestMethod]
        public void TestCustomerMembershipProvider_ValidateUser_Success()
        {
            Assert.IsTrue(provider.ValidateUser("admin", "Abcdef9*"));
        }

        [TestMethod]
        public void TestCustomerMembershipProvider_ValidateUser_Fail()
        {
            Assert.IsFalse(provider.ValidateUser("admin1", "Abcdef9*"));
            Assert.IsFalse(provider.ValidateUser("admin", "Abcdef8*"));
        }
    }
}
