using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class AcceptPathDefTest
    {
        [TestMethod]
        public void TestAcceptPathDef_GetDescription()
        {
            Assert.AreEqual(AcceptPathDef.Telephone.GetAcceptPathDescription(), "电话");
            Assert.AreEqual(AcceptPathDef.Others.GetAcceptPathDescription(), "其他");
        }

        [TestMethod]
        public void TestAcceptPathDef_GetIndex()
        {
            Assert.AreEqual(("电话").GetAcceptPathIndex(), AcceptPathDef.Telephone);
            Assert.AreEqual(("电子邮件").GetAcceptPathIndex(), AcceptPathDef.Others);
        }
    }
}
