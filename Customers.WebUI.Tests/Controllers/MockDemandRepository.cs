using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TableDef;
using Customers.Domain.TypeDef;
using Moq;

namespace Customers.WebUI.Tests.Controllers
{
    public abstract class MockDemandRepository
    {
        protected Mock<IDemandRepository> mockRepository = new Mock<IDemandRepository>();

        protected void InitializeMockObject()
        {
            mockRepository.SetupGet(x => x.Demands).Returns(new List<Demand> {
                new Demand {
                    Id = 1,
                    City = CityDef.Guangzhou,
                    ProjectName = "project1",
                    ReceiveDate = new DateTime(2013,11,11) },
                new Demand {
                    Id = 2,
                    City = CityDef.Guangzhou,
                    ProjectName = "project2",
                    ReceiveDate = new DateTime(2013,11,11) },
                new Demand {
                    Id = 3,
                    City = CityDef.Foshan,
                    ProjectName = "project3",
                    ReceiveDate = new DateTime(2013,11,11) },
                new Demand {
                    Id = 4,
                    City = CityDef.Foshan,
                    ProjectName = "project4",
                    ReceiveDate = new DateTime(2013,11,11) },
                new Demand {
                    Id = 5,
                    City = CityDef.Foshan,
                    ProjectName = "project5",
                    ReceiveDate = new DateTime(2013,11,11) }
                }.AsQueryable()
            );
        }
    }
}
