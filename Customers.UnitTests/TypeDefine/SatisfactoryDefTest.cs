using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class SatisfactoryDefTest
    {
        [TestMethod]
        public void TestSatisfactoryDef_GetDescription()
        {
            Assert.AreEqual(SatisfactoryDef.Perfect.GetSatisfactoryDescription(), "满意");
            Assert.AreEqual(SatisfactoryDef.Unknown.GetSatisfactoryDescription(), "未知");
        }

        [TestMethod]
        public void TestSatisfactoryDef_GetIndex()
        {
            Assert.AreEqual(("差").GetSatisfactoryIndex(), SatisfactoryDef.Poor);
            Assert.AreEqual(("已知").GetSatisfactoryIndex(), SatisfactoryDef.Unknown);
        }
    }
}
