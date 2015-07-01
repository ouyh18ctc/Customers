using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TableDef;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TableDefine
{
    [TestClass]
    public class DemandRepositorySaveTest : DemandRepositoryTest
    {
        private DemandView demandView;

        [TestInitialize]
        public void TestInitialize()
        {
            Initialize();
            demandView = new DemandView
            {
                Id = 1,
                City = "广州",
                ProjectName = "project1",
                ReceiveDate = new DateTime(2013, 12, 12, 3, 0, 0)
            };
        }

        [TestMethod]
        public void DemandRepositorySave_EntityExisted()
        {
            AssertUpdateDemand(demandView);
        }

        [TestMethod]
        public void DemandRepositorySave_EntityNotExisted()
        {
            demandView.ProjectName = "project2";
            AssertAddDemand(demandView);
        }
    }
}
