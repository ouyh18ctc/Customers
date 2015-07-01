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
    public class DemandViewTest
    {
        [TestMethod]
        public void TestDemandView_BasicParameters()
        {
            DemandView demandView = new DemandView();
            Assert.AreEqual(demandView.ExpectedSubscriber, 0);
            Assert.AreEqual(demandView.AcceptPath, "现场沟通");
            Assert.AreEqual(demandView.CustomerLevel, "4");
            Assert.AreEqual(demandView.DemandLevel, "C级");
            Assert.AreEqual(demandView.DemandSource, "分公司");
            Assert.AreEqual(demandView.DemandType, "内部投诉");
            Assert.AreEqual(demandView.MarketingTheme, "营销渠道");
            Assert.AreEqual(demandView.Satisfactory, "未知");
            Assert.AreEqual(demandView.ProjectState, "跟进中");
        }

        [TestMethod]
        public void TestDemandView_ParseBasicParameters()
        {
            DemandView demandView = new DemandView()
            {
                ExpectedCompleteDate = new DateTime(2014, 6, 22),
                ExpectedProfit = 20000,
                ActualCompleteDate = new DateTime(2014, 7, 31),
                Department = "MarketDepartment"
            };
            Demand demand = Demand.Parse(demandView);
            Assert.AreEqual(demand.ExpectedCompleteDate, new DateTime(2014, 6, 22));
            Assert.AreEqual(demand.ExpectedProfit, 20000);
            Assert.AreEqual(demand.ActualCompleteDate, DateTime.Today.AddDays(-3));
            Assert.AreEqual(demand.Department, "MarketDepartment");
        }

        [TestMethod]
        public void TestDemandView_ParseDemandParameters()
        {
            DemandView demandView = new DemandView()
            {
                DemandLevel = "B级",
                DemandSource = "政企客户",
                DemandType = "通信保障"
            };
            Demand demand = Demand.Parse(demandView);
            Assert.AreEqual(demand.DemandLevel, DemandLevelDef.B);
            Assert.AreEqual(demand.DemandSource, DemandSourceDef.Government);
            Assert.AreEqual(demand.DemandType, DemandTypeDef.Communication);
        }

        [TestMethod]
        public void TestDemand_ParseMarketInfos()
        {
            DemandView demandView = new DemandView()
            {
                CustomerLevel = "2",
                MarketingTheme = "农村市场",
                Satisfactory = "满意",
                ProjectState = "完成"
            };
            Demand demand = Demand.Parse(demandView);
            Assert.AreEqual(demand.CustomerLevel, CustomerLevelDef.Two);
            Assert.AreEqual(demand.MarketingTheme, MarketingThemeDef.Rural);
            Assert.AreEqual(demand.Satisfactory, SatisfactoryDef.Perfect);
            Assert.AreEqual(demand.ProjectState, ProjectStateDef.Complete);
        }
    }
}
