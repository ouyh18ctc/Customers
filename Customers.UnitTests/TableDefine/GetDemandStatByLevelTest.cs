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
    public class GetDemandStatByLevelTest
    {
        private IDemandRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            repository = new FakeDemandRepository(new List<Demand>{
                new Demand{City=CityDef.Dongguan,DemandLevel=DemandLevelDef.A,ProjectState=ProjectStateDef.InProgress},
                new Demand{City=CityDef.Dongguan,DemandLevel=DemandLevelDef.B,ProjectState=ProjectStateDef.InProgress},
                new Demand{City=CityDef.Dongguan,DemandLevel=DemandLevelDef.C,ProjectState=ProjectStateDef.InProgress},
                new Demand{City=CityDef.Dongguan,DemandLevel=DemandLevelDef.A,ProjectState=ProjectStateDef.Complete},
                new Demand{City=CityDef.Shenzhen,DemandLevel=DemandLevelDef.B,ProjectState=ProjectStateDef.Complete}
            });
        }

        [TestMethod]
        public void TestGetDemandStatByLevel()
        {
            IEnumerable<ByLevelDemandStat> stats = repository.Demands.GetDemandStatByLevel();
            Assert.AreEqual(stats.Count(), 3);
            Assert.AreEqual(stats.ElementAt(0).City, "东莞");
            Assert.AreEqual(stats.ElementAt(0).ALevelFinished, 1);
            Assert.AreEqual(stats.ElementAt(0).ALevelTotal, 2);
            Assert.AreEqual(stats.ElementAt(0).BLevelFinished, 0);
            Assert.AreEqual(stats.ElementAt(0).BLevelTotal, 1);
            Assert.AreEqual(stats.ElementAt(0).CLevelFinished, 0);
            Assert.AreEqual(stats.ElementAt(0).CLevelTotal, 1);
            Assert.AreEqual(stats.ElementAt(1).BLevelTotal, 1);
            Assert.AreEqual(stats.ElementAt(1).BLevelFinished, 1);
            Assert.AreEqual(stats.ElementAt(2).BLevelFinished, 1);
        }
    }
}
