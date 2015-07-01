using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TableDef;
using Customers.Domain.TypeDef;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    [TestClass]
    public class DemandClonePropertiesTest
    {
        private Demand source = new Demand();
        private Demand dest = new Demand();

        [TestMethod]
        public void TestDemandCloneProperties_CloneBasicParameters()
        {
            source.Comments = "aaa";
            source.ReceiveDate = new DateTime(2014, 1, 1);
            dest.Comments = "bbb";
            dest.ReceiveDate = new DateTime(2013, 3, 3);
            source.CloneProperties(dest);
            Assert.AreEqual(dest.Comments, "aaa");
            Assert.AreEqual(dest.ReceiveDate, new DateTime(2014, 1, 1));
        }

        [TestMethod]
        public void TestDemandCloneProperties_CloneEnumParameters()
        {
            source.City = CityDef.Guangzhou;
            source.AcceptPath = AcceptPathDef.Direct;
            dest.City = CityDef.Foshan;
            dest.AcceptPath = AcceptPathDef.System;
            source.CloneProperties(dest);
            Assert.AreEqual(dest.City, CityDef.Guangzhou);
            Assert.AreEqual(dest.AcceptPath, AcceptPathDef.Direct);
        }

        [TestMethod]
        public void TestDemandCloneProperties_CloneId()
        {
            source.Id = 1;
            dest.Id = 2;
            source.CloneProperties(dest);
            Assert.AreEqual(dest.Id, 2);
            source.CloneProperties(dest, false);
            Assert.AreEqual(dest.Id, 1);
        }
    }
}
