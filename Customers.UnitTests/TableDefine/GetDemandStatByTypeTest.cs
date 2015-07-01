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
    public class GetDemandStatByTypeTest
    {
        private IDemandRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            repository = new FakeDemandRepository(new List<Demand>{
                new Demand{City=CityDef.Dongguan,DemandType=DemandTypeDef.Communication,
                    ProjectState=ProjectStateDef.InProgress},
                new Demand{City=CityDef.Dongguan,DemandType=DemandTypeDef.Vip,
                    ProjectState=ProjectStateDef.InProgress},
                new Demand{City=CityDef.Dongguan,DemandType=DemandTypeDef.Intra,
                    ProjectState=ProjectStateDef.InProgress},
                new Demand{City=CityDef.Dongguan,DemandType=DemandTypeDef.Emergency,
                    ProjectState=ProjectStateDef.InProgress},
                new Demand{City=CityDef.Dongguan,DemandType=DemandTypeDef.Market,
                    ProjectState=ProjectStateDef.Complete},
                new Demand{City=CityDef.Foshan,DemandType=DemandTypeDef.Communication,
                    ProjectState=ProjectStateDef.InProgress},
                new Demand{City=CityDef.Shenzhen,DemandType=DemandTypeDef.Market,
                    ProjectState=ProjectStateDef.Complete}
            });
        }

        [TestMethod]
        public void TestGetDemandStatByType()
        {
            IEnumerable<ByTypeDemandStat> stats = repository.Demands.GetDemandStatByType();
            Assert.AreEqual(stats.Count(), 4);
            Assert.AreEqual(stats.ElementAt(0).City, "东莞");
            Assert.AreEqual(stats.ElementAt(0).CommunicationFinished, 0);
            Assert.AreEqual(stats.ElementAt(0).VipTotal, 1);
        }
    }
}
