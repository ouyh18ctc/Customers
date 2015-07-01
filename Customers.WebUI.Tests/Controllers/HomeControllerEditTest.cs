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

namespace Customers.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerEditTest : MockDemandRepository
    {       
        private HomeController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeMockObject();
            controller = new HomeController(mockRepository.Object, null, null);
        }

        [TestMethod]
        public void HomeControllerEdit_Id_Existed()
        {
            ViewResult result = controller.Edit(2);
            Assert.IsNotNull(result.Model);
            DemandView viewModel = result.Model as DemandView;
            Assert.AreEqual(result.ViewBag.Title, "编辑需求：project2");
            Assert.AreEqual(viewModel.City, "广州");
        }

        [TestMethod]
        public void HomeControllerEdit_Id_NotExisted()
        {
            ViewResult result = controller.Edit(6);
            Assert.IsNotNull(result.Model);
            DemandView viewModel = result.Model as DemandView;
            Assert.AreEqual(result.ViewBag.Title, "新建需求");
            Assert.AreEqual(viewModel.City, "深圳");
        }
    }
}
