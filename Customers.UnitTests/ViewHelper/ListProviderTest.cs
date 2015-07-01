using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.ViewHelper;

namespace Customers.UnitTests.ViewHelper
{
    [TestClass]
    public class ListProviderTest
    {
        [TestMethod]
        public void TestListProvider_Default()
        {
            Assert.AreEqual(ListProviders.Current.GetListItems("Gender").Count(), 2);
            Assert.AreEqual(ListProviders.Current.GetListItems("Education").Count(), 4);
            Assert.AreEqual(ListProviders.Current.GetListItems("Department").Count(), 3);
        }

        [TestMethod]
        public void TestListProvider_SetFake()
        {
            ListProviders.SetListProvider(() => new FakeListProvider());
            Assert.AreEqual(ListProviders.Current.GetListItems("Gender").Count(), 0);
            Assert.AreEqual(ListProviders.Current.GetListItems("MyProperty").Count(), 2);
            Assert.AreEqual(ListProviders.Current.GetListItems("MyProperty").ElementAt(1).Text, "Tip2");
            Assert.AreEqual(ListProviders.Current.GetListItems("MyProperty").ElementAt(0).Value, "P1");
        }
    }
}
