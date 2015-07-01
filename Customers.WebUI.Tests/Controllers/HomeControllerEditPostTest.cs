using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.WebUI.Controllers;
using Customers.WebUI.Models;
using Customers.Domain.ViewHelper;
using Customers.Domain.TableDef;
using Customers.Domain.TypeDef;
using Customers.Domain.Security;
using Moq;
using System.Web.Mvc;

namespace Customers.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerEditPostTest
    {
        private HomeController controller;
        private DemandView demand;
        private IDemandRepository repository;

        [TestInitialize]
        public void TestInitialize()
        {
            repository = new FakeDemandRepository(new List<Demand>{
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
                });
            controller = new HomeController(repository, new FakeUserRepository(), new FakeProgressRepository());
        }

        [TestMethod]
        public void TestHomeControllerEdit_Post_ValidDemand_Existed()
        {
            demand = new DemandView
            {
                Id = 1,
                City = "广州",
                ProjectName = "project1",
                ReceiveDate = new DateTime(2013, 11, 11)
            };
            Assert.AreEqual(repository.Demands.Count(), 5);
            ActionResult result = controller.Edit(demand);
            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.AreEqual(controller.TempData["success"], "project1需求已成功保存！");
            Assert.AreEqual(repository.Demands.Count(), 5);
        }

        [TestMethod]
        public void TestHomeControllerEdit_Post_ValidDemand_NotExisted()
        {
            demand = new DemandView
            {
                Id = 1,
                City = "广州",
                ProjectName = "project6",
                ReceiveDate = new DateTime(2013, 11, 11)
            };
            Assert.AreEqual(repository.Demands.Count(), 5);
            ActionResult result = controller.Edit(demand);
            Assert.IsTrue(controller.ModelState.IsValid);
            Assert.AreEqual(controller.TempData["success"], "project6需求已成功保存！");
            Assert.AreEqual(repository.Demands.Count(), 6);
        }
    }
}
