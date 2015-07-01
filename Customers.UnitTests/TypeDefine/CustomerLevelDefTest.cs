using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class CustomerLevelDefTest
    {
        [TestMethod]
        public void TestCustomerLevelDef_GetLevel()
        {
            Assert.AreEqual(CustomerLevelDef.One.GetCustomerLevelValue(), 1);
            Assert.AreEqual(CustomerLevelDef.Three.GetCustomerLevelValue(), 3);
            Assert.AreEqual(CustomerLevelDef.Undefined.GetCustomerLevelValue(), 0);
        }

        [TestMethod]
        public void TestCustomerLevelDef_GetIndex()
        {
            Assert.AreEqual((1).GetCustomerLevelIndex(), CustomerLevelDef.One);
            Assert.AreEqual((4).GetCustomerLevelIndex(), CustomerLevelDef.Four);
            Assert.AreEqual((5).GetCustomerLevelIndex(), CustomerLevelDef.Undefined);
        }
    }
}
