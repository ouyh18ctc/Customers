using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using Customers.Domain.Security;
using Customers.Domain.TableDef;
using Customers.Domain.TypeDef;
using Customers.WebUI.Controllers;
using Customers.WebUI.Models;

namespace Customers.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerEmptyIndexTest
    {
        protected Mock<IDemandRepository> mockRepository = new Mock<IDemandRepository>();
        private HomeController controller;
        private IUserInformationRepository userRepository = new FakeUserRepository();

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository.SetupGet(x => x.Demands).Returns((new List<Demand>()).AsQueryable());
            controller = new HomeController(mockRepository.Object, userRepository, null)
            {
                PageSize = 2
            };
            controller.CurrentUser = "admin";
        }

        [TestMethod]
        public void TestHomeControllerIndex_AllCities_FirstPage_PageSize2()
        {
            ViewResult result = controller.Index(null);
            Assert.AreEqual(result.ViewName, "");
            Assert.IsNotNull(result.Model);
            DemandListViewModel viewModel = result.Model as DemandListViewModel;
            Assert.IsNotNull(viewModel);
        }
    }
}
