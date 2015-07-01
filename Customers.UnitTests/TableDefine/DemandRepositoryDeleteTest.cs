using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;
using Customers.Domain.TableDef;

namespace Customers.UnitTests.TableDefine
{
    [TestClass]
    public class DemandRepositoryDeleteTest : DemandRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Initialize();
        }

        [TestMethod]
        public void TestDemandRepositoryDelete_Success()
        {
            Demand demand = AssertDeleteDemandSuccess(1);
            Assert.AreEqual(demand.ProjectName, "project1");
            demand = AssertDeleteDemandSuccess(2);
            Assert.AreEqual(demand.ProjectName, "project2");
            Assert.AreEqual(demand.ReceiveDate, new DateTime(2013, 12, 16, 7, 0, 0));
        }

        [TestMethod]
        public void TestDemandRepositoryDelete_Fail()
        {
            Assert.IsNull(AssertDeleteDemandFail(3));
            Assert.IsNull(AssertDeleteDemandFail(4));
            Assert.IsNull(AssertDeleteDemandFail(5));
        }
    }
}
