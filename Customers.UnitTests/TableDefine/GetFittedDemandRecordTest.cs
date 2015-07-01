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
    public class GetFittedDemandRecordTest
    {
        private List<Demand> demandList = new List<Demand>();
        private Demand demand;
        private DemandView demandView;

        [TestInitialize]
        public void TestInitialize()
        {
            demandList.Add(new Demand
            {
                Id = 1,
                City = CityDef.Guangzhou,
                ProjectName = "project1",
                ReceiveDate = new DateTime(2013, 12, 12, 2, 0, 0)
            });
            demandList.Add(new Demand
            {
                Id = 2,
                City = CityDef.Foshan,
                ProjectName = "project2",
                ReceiveDate = new DateTime(2013, 12, 11, 3, 0, 0)
            });
            demandList.Add(new Demand
            {
                Id = 3,
                City = CityDef.Foshan,
                ProjectName = "project2",
                ReceiveDate = new DateTime(2013, 12, 10, 5, 0, 0)
            });
        }

        [TestMethod]
        public void TestGetFittedDemandRecord_Demand_Fit()
        {
            demand = new Demand
            {
                Id = 4,
                City = CityDef.Guangzhou,
                ProjectName = "project1",
                ReceiveDate = new DateTime(2013, 12, 12, 6, 0, 0)
            };
            Demand fittedDemand = demandList.GetFittedRecord(demand);
            Assert.IsNotNull(fittedDemand);
            Assert.AreEqual(fittedDemand.Id, 1);
        }

        [TestMethod]
        public void TestGetFittedDemandRecord_Demand_CityUnFit()
        {
            demand = new Demand
            {
                Id = 4,
                City = CityDef.Shantou,
                ProjectName = "project1",
                ReceiveDate = new DateTime(2013, 12, 12, 6, 0, 0)
            };
            Demand fittedDemand = demandList.GetFittedRecord(demand);
            Assert.IsNull(fittedDemand);
        }

        [TestMethod]
        public void TestGetFittedDemandRecord_Demand_ProjectUnFit()
        {
            demand = new Demand
            {
                Id = 4,
                City = CityDef.Guangzhou,
                ProjectName = "project3",
                ReceiveDate = new DateTime(2013, 12, 12, 6, 0, 0)
            };
            Demand fittedDemand = demandList.GetFittedRecord(demand);
            Assert.IsNull(fittedDemand);
        }

        [TestMethod]
        public void TestGetFittedDemandRecord_Demand_ReceivedDateUnFit()
        {
            demand = new Demand
            {
                Id = 4,
                City = CityDef.Guangzhou,
                ProjectName = "project1",
                ReceiveDate = new DateTime(2012, 12, 12, 6, 0, 0)
            };
            Demand fittedDemand = demandList.GetFittedRecord(demand);
            Assert.IsNull(fittedDemand);
        }

        [TestMethod]
        public void TestGetFittedDemandRecord_DemandView_Fit()
        {
            demandView = new DemandView
            {
                Id = 5,
                City = "佛山",
                ProjectName = "project2",
                ReceiveDate = new DateTime(2013, 12, 11, 4, 50, 0)
            };
            Demand fittedDemand = demandList.GetFittedRecord(demandView);
            Assert.IsNotNull(fittedDemand);
            Assert.AreEqual(fittedDemand.Id, 2);
            demandView.ReceiveDate = new DateTime(2013, 12, 10, 0, 0, 4);
            fittedDemand = demandList.GetFittedRecord(demandView);
            Assert.IsNotNull(fittedDemand);
            Assert.AreEqual(fittedDemand.Id, 3);
        }

        [TestMethod]
        public void TestGetFittedDemandRecord_DemandView_CityUnFit()
        {
            demandView = new DemandView
            {
                Id = 5,
                City = "广州",
                ProjectName = "project2",
                ReceiveDate = new DateTime(2013, 12, 11, 4, 50, 0)
            };
            Demand fittedDemand = demandList.GetFittedRecord(demandView);
            Assert.IsNull(fittedDemand);
        }

        [TestMethod]
        public void TestGetFittedDemandRecord_DemandView_ProjectUnFit()
        {
            demandView = new DemandView
            {
                Id = 5,
                City = "佛山",
                ProjectName = "project3",
                ReceiveDate = new DateTime(2013, 12, 11, 4, 50, 0)
            };
            Demand fittedDemand = demandList.GetFittedRecord(demandView);
            Assert.IsNull(fittedDemand);
        }

        [TestMethod]
        public void TestGetFittedDemandRecord_DemandView_ReceivedDateUnFit()
        {
            demandView = new DemandView
            {
                Id = 5,
                City = "佛山",
                ProjectName = "project2",
                ReceiveDate = new DateTime(2012, 12, 11, 4, 50, 0)
            };
            Demand fittedDemand = demandList.GetFittedRecord(demandView);
            Assert.IsNull(fittedDemand);
        }
    }
}
