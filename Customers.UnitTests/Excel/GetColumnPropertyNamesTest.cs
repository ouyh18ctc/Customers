using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    [TestClass]
    public class GetColumnPropertyNamesTest
    {
        [TestMethod]
        public void TestGetColumnPropertyNames()
        {
            Dictionary<PropertyInfo, string> propertyNames =
                typeof(ExtendedColumnClass).GetColumnPropertyNames();
            Assert.AreEqual(typeof(ExtendedColumnClass).GetProperties().Length, 5);
            Assert.IsNotNull(propertyNames);
            Assert.AreEqual(propertyNames.Count(), 4);
            Assert.IsTrue(propertyNames.ContainsValue("First Field"));
            Assert.IsTrue(propertyNames.ContainsValue("Second Field"));
            Assert.IsTrue(propertyNames.ContainsValue("Fourth Field"));
            Assert.IsTrue(propertyNames.ContainsValue("Fifth Field"));
        }
    }
}
