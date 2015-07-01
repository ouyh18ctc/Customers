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
    public class GetDemandStatByDelayTest
    {
        private IDemandRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            repository = new FakeDemandRepository(new List<Demand>
            {
                new Demand{City=CityDef.Chaozhou,DemandLevel=DemandLevelDef.A,
                    ProjectState=ProjectStateDef.InProgress,ReceiveDate=DateTime.Today.AddDays(-100)},
                new Demand{City=CityDef.Chaozhou,DemandLevel=DemandLevelDef.A,
                    ProjectState=ProjectStateDef.InProgress,ReceiveDate=DateTime.Today.AddDays(-200)},
                new Demand{City=CityDef.Chaozhou,DemandLevel=DemandLevelDef.A,
                    ProjectState=ProjectStateDef.InProgress,ReceiveDate=DateTime.Today.AddDays(-10)},
                new Demand{City=CityDef.Foshan,DemandLevel=DemandLevelDef.B,
                    ProjectState=ProjectStateDef.InProgress,ReceiveDate=DateTime.Today.AddDays(-20)},
                new Demand{City=CityDef.Foshan,DemandLevel=DemandLevelDef.B,
                    ProjectState=ProjectStateDef.Complete,ReceiveDate=DateTime.Today.AddDays(-100),
                    ActualCompleteDate=DateTime.Today.AddDays(-30)
                },
                new Demand{City=CityDef.Chaozhou,DemandLevel=DemandLevelDef.C,
                    ProjectState=ProjectStateDef.Complete,ReceiveDate=DateTime.Today.AddDays(-100),
                    ActualCompleteDate=DateTime.Today.AddDays(-80)
                }
            });
        }

        [TestMethod]
        public void TestGetDemandStatByDelay()
        {
            IEnumerable<ByDelayDemandStat> stats = repository.Demands.GetDemandStatByDelay();
            Assert.AreEqual(stats.Count(), 3);
            Assert.AreEqual(stats.ElementAt(0).City, "潮州");
            Assert.AreEqual(stats.ElementAt(0).ALevelTotal, 3);
            Assert.AreEqual(stats.ElementAt(0).ALevelOneQuarter, 1);
            Assert.AreEqual(stats.ElementAt(0).ALevelHalfYear, 1);
            Assert.AreEqual(stats.ElementAt(0).ALevelOneMonth, 0);
            Assert.AreEqual(stats.ElementAt(0).BLevelTotal, 0);
            Assert.AreEqual(stats.ElementAt(0).CLevelTotal, 1);
            Assert.AreEqual(stats.ElementAt(1).BLevelTotal, 2);
            Assert.AreEqual(stats.ElementAt(1).BLevelOneMonth, 1);
            Assert.AreEqual(stats.ElementAt(2).BLevelOneQuarter, 0);
        }
    }
}
