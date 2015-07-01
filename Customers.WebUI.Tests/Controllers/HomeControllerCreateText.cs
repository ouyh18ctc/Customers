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
using Moq;
using System.Web.Mvc;
using Customers.Domain.Security;

namespace Customers.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerCreateText : MockDemandRepository
    {
        private HomeController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeMockObject();
            controller = new HomeController(mockRepository.Object, new FakeUserRepository(), null);
            controller.CurrentUser = "admin";
        }

        [TestMethod]
        public void TestHomeControllerCreate_NullCity()
        {
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(result.ViewBag.Title, "新建需求");
            DemandView viewModel = result.Model as DemandView;
            Assert.AreEqual(viewModel.City, "深圳");
        }

        [TestMethod]
        public void TestHomeControllerCreate_ExistedCity()
        {
            ViewResult result = controller.Create("佛山") as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(result.ViewBag.Title, "新建需求");
            DemandView viewModel = result.Model as DemandView;
            Assert.AreEqual(viewModel.City, "佛山");
        }

        [TestMethod]
        public void TestHomeControllerCreate_UnExistedCity()
        {
            ViewResult result = controller.Create("汕头") as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(result.ViewBag.Title, "新建需求");
            DemandView viewModel = result.Model as DemandView;
            Assert.AreEqual(viewModel.City, "深圳");
        }
    }
}
