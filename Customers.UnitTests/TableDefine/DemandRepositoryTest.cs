using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Customers.Domain.TableDef;
using Customers.Domain.TypeDef;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Customers.UnitTests.TableDefine
{
    public abstract class DemandRepositoryTest
    {
        private IDemandRepository repository;

        protected void Initialize()
        {
            repository = new FakeDemandRepository(new List<Demand>{
                new Demand { Id = 1, City = CityDef.Guangzhou, ProjectName = "project1",
                    ReceiveDate = new DateTime(2013, 12, 12, 3, 0, 0) },
                new Demand { Id = 2, City = CityDef.Foshan, ProjectName = "project2",
                    ReceiveDate = new DateTime(2013, 12, 16, 7, 0, 0) }
            });
        }

        private void AssertEntity(Demand source, DemandView dest)
        {
            Assert.AreEqual(source.ProjectName, dest.ProjectName);
            Assert.AreEqual(source.City.GetCityName(), dest.City);
            Assert.AreEqual(source.ReceiveDate, dest.ReceiveDate);
        }

        protected void AssertAddDemand(DemandView demandView)
        {
            int originalCount = repository.Demands.Count();
            repository.SaveDemand(demandView);
            Demand demand = repository.Demands.GetFittedRecord(demandView);
            Assert.AreEqual(repository.Demands.Count(), originalCount + 1);
            AssertEntity(demand, demandView);
        }

        protected void AssertUpdateDemand(DemandView demandView)
        {
            int originalCount = repository.Demands.Count();
            repository.SaveDemand(demandView);
            Demand demand = repository.Demands.GetFittedRecord(demandView);
            Assert.AreEqual(repository.Demands.Count(), originalCount);
            AssertEntity(demand, demandView);
        }

        protected Demand AssertDeleteDemandFail(int demandId)
        {
            int originalCount = repository.Demands.Count();
            Demand demand = repository.DeleteDemand(demandId);
            Assert.IsNull(demand);
            Assert.AreEqual(repository.Demands.Count(), originalCount);
            return demand;
        }

        protected Demand AssertDeleteDemandSuccess(int demandId)
        {
            int originalCount = repository.Demands.Count(); 
            Demand demand = repository.DeleteDemand(demandId);
            Assert.IsNotNull(demand);
            Assert.AreEqual(repository.Demands.Count(), originalCount - 1);
            return demand;
        }
    }
}
