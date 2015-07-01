using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class DemandLevelDefTest
    {
        [TestMethod]
        public void TestDemandLevelDef_GetDescription()
        {
            Assert.AreEqual(DemandLevelDef.A.GetDemandLevelDescription(), "A级");
            Assert.AreEqual(DemandLevelDef.Undefined.GetDemandLevelDescription(), "未定义");
        }

        [TestMethod]
        public void TestDemandLevelDef_GetIndex()
        {
            Assert.AreEqual(("B级").GetDemandLevelIndex(), DemandLevelDef.B);
            Assert.AreEqual(("D级").GetDemandLevelIndex(), DemandLevelDef.Undefined);
        }
    }
}
