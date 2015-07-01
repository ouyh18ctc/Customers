using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class DemandTypeDefTest
    {
        [TestMethod]
        public void TestDemandTypeDef_GetDescription()
        {
            Assert.AreEqual(DemandTypeDef.Communication.GetDemandTypeDescription(), "通信保障");
            Assert.AreEqual(DemandTypeDef.Undefined.GetDemandTypeDescription(), "未定义");
        }

        [TestMethod]
        public void TestDemandTypeDef_GetIndex()
        {
            Assert.AreEqual(("内部投诉").GetDemandTypeIndex(), DemandTypeDef.Intra);
            Assert.AreEqual(("外部投诉").GetDemandTypeIndex(), DemandTypeDef.Undefined);
        }
    }
}
