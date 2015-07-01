using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class DemandSourceDefTest
    {
        [TestMethod]
        public void TestDemandSourceDef_GetDescription()
        {
            Assert.AreEqual(DemandSourceDef.Branch.GetDemandSourceDescription(), "分公司");
            Assert.AreEqual(DemandSourceDef.Undefined.GetDemandSourceDescription(), "未定义");
        }

        [TestMethod]
        public void TestDemandSourceDef_GetIndex()
        {
            Assert.AreEqual(("行业应用").GetDemandSourceIndex(), DemandSourceDef.Industry);
            Assert.AreEqual(("公众客户").GetDemandSourceIndex(), DemandSourceDef.Undefined);
        }
    }
}
