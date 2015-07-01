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
    public class DemandTest
    {
        [TestMethod]
        public void TestDemand_BasicParameters()
        {
            Demand demand = new Demand();
            Assert.AreEqual(demand.ExpectedSubscriber, 0);
            Assert.AreEqual(demand.AcceptPath, AcceptPathDef.Direct);
            Assert.AreEqual(demand.CustomerLevel, CustomerLevelDef.Four);
            Assert.AreEqual(demand.DemandLevel, DemandLevelDef.C);
            Assert.AreEqual(demand.DemandSource, DemandSourceDef.Branch);
            Assert.AreEqual(demand.DemandType, DemandTypeDef.Intra);
            Assert.AreEqual(demand.MarketingTheme, MarketingThemeDef.Market);
            Assert.AreEqual(demand.Satisfactory, SatisfactoryDef.Unknown);
            Assert.AreEqual(demand.ProjectState, ProjectStateDef.InProgress);
        }

        [TestMethod]
        public void TestDemand_ParseBasicParameters()
        {
            Demand demand = new Demand()
            {
                ExpectedCompleteDate = new DateTime(2014, 6, 22),
                ExpectedProfit = 20000,
                ActualCompleteDate = new DateTime(2014, 7, 31),
                Department = "MarketDepartment"
            };
            DemandView demandView = DemandView.Parse(demand);
            Assert.AreEqual(demandView.ExpectedCompleteDate, new DateTime(2014, 6, 22));
            Assert.AreEqual(demandView.ExpectedProfit, 20000);
            Assert.AreEqual(demandView.ActualCompleteDate, new DateTime(2014, 7, 31));
            Assert.AreEqual(demandView.Department, "MarketDepartment");
        }

        [TestMethod]
        public void TestDemand_ParseDemandParameters()
        {
            Demand demand = new Demand()
            {
                DemandLevel = DemandLevelDef.B,
                DemandSource = DemandSourceDef.Government,
                DemandType = DemandTypeDef.Communication
            };
            DemandView demandView = DemandView.Parse(demand);
            Assert.AreEqual(demandView.DemandLevel, "B级");
            Assert.AreEqual(demandView.DemandSource, "政企客户");
            Assert.AreEqual(demandView.DemandType, "通信保障");
        }

        [TestMethod]
        public void TestDemand_ParseMarketInfos()
        {
            Demand demand = new Demand()
            {
                CustomerLevel = CustomerLevelDef.Two,
                MarketingTheme = MarketingThemeDef.Rural,
                Satisfactory = SatisfactoryDef.Perfect,
                ProjectState = ProjectStateDef.Complete
            };
            DemandView demandView = DemandView.Parse(demand);
            Assert.AreEqual(demandView.CustomerLevel, "2");
            Assert.AreEqual(demandView.MarketingTheme, "农村市场");
            Assert.AreEqual(demandView.Satisfactory, "满意");
            Assert.AreEqual(demandView.ProjectState, "完成");
        }
    }
}
