using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class ProjectStateDefTest
    {
        [TestMethod]
        public void TestProjectStateDef_GetDescription()
        {
            Assert.AreEqual(ProjectStateDef.InProgress.GetProjectStateDescription(), "跟进中");
            Assert.AreEqual(ProjectStateDef.Complete.GetProjectStateDescription(), "完成");
        }

        [TestMethod]
        public void TestProjectStateDef_GetIndex()
        {
            Assert.AreEqual(("跟进中").GetProjectStateIndex(), ProjectStateDef.InProgress);
            Assert.AreEqual(("完成").GetProjectStateIndex(), ProjectStateDef.Complete);
        }
    }
}
