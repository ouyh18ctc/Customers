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
    public class HomeControllerIndexTest : MockDemandRepository
    {
        private HomeController controller;
        private IUserInformationRepository userRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeMockObject();
            userRepository = new FakeUserRepository();
            controller = new HomeController(mockRepository.Object, userRepository, new FakeProgressRepository())
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
            Assert.AreEqual(viewModel.PagingInfo.TotalItems, 5);
            Assert.AreEqual(viewModel.PagingInfo.TotalPages, 3);
            Assert.AreEqual(viewModel.PagingInfo.ItemsPerPage, 2);
            Assert.AreEqual(viewModel.PagingInfo.CurrentPage, 1);
            Assert.AreEqual(viewModel.CurrentCity, null);
            Assert.AreEqual(viewModel.Demands.Count(), 2);
            Assert.AreEqual(viewModel.Demands.ElementAt(0).City, "广州");
            Assert.AreEqual(viewModel.Demands.ElementAt(1).ProjectName, "project2");
        }

        [TestMethod]
        public void TestHomeControllerIndex_AllCities_ThirdPage_PageSize2()
        {
            ViewResult result = controller.Index(null, 3);
            Assert.IsNotNull(result.Model);
            DemandListViewModel viewModel = result.Model as DemandListViewModel;
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(viewModel.PagingInfo.TotalItems, 5);
            Assert.AreEqual(viewModel.PagingInfo.TotalPages, 3);
            Assert.AreEqual(viewModel.PagingInfo.ItemsPerPage, 2);
            Assert.AreEqual(viewModel.PagingInfo.CurrentPage, 3);
            Assert.AreEqual(viewModel.CurrentCity, null);
            Assert.AreEqual(viewModel.Demands.Count(), 1);
            Assert.AreEqual(viewModel.Demands.ElementAt(0).City, "佛山");
            Assert.AreEqual(viewModel.Demands.ElementAt(0).ProjectName, "project5");
        }

        [TestMethod]
        public void TestHomeControllerIndex_AllCities_FirstPage_PageSize4()
        {
            controller.PageSize = 4;
            ViewResult result = controller.Index(null);
            Assert.IsNotNull(result.Model);
            DemandListViewModel viewModel = result.Model as DemandListViewModel;
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(viewModel.PagingInfo.TotalItems, 5);
            Assert.AreEqual(viewModel.PagingInfo.TotalPages, 2);
            Assert.AreEqual(viewModel.PagingInfo.ItemsPerPage, 4);
            Assert.AreEqual(viewModel.PagingInfo.CurrentPage, 1);
            Assert.AreEqual(viewModel.CurrentCity, null);
            Assert.AreEqual(viewModel.Demands.Count(), 4);
            Assert.AreEqual(viewModel.Demands.ElementAt(0).City, "广州");
            Assert.AreEqual(viewModel.Demands.ElementAt(1).ProjectName, "project2");
        }

        [TestMethod]
        public void TestHomeControllerIndex_Foshan_FirstPage_PageSize2()
        {
            ViewResult result = controller.Index("佛山");
            Assert.IsNotNull(result.Model);
            DemandListViewModel viewModel = result.Model as DemandListViewModel;
            Assert.IsNotNull(viewModel);
            Assert.AreEqual(viewModel.PagingInfo.TotalItems, 3);
            Assert.AreEqual(viewModel.PagingInfo.TotalPages, 2);
            Assert.AreEqual(viewModel.PagingInfo.ItemsPerPage, 2);
            Assert.AreEqual(viewModel.PagingInfo.CurrentPage, 1);
            Assert.AreEqual(viewModel.CurrentCity, "佛山");
            Assert.AreEqual(viewModel.Demands.Count(), 2);
            Assert.AreEqual(viewModel.Demands.ElementAt(0).City, "佛山");
            Assert.AreEqual(viewModel.Demands.ElementAt(1).ProjectName, "project4");
        }

        [TestMethod]
        public void TestHomeControllerIndex_AllowFinish()
        {
            userRepository.UserInformations.ElementAt(0).PermissionList = new int[3] { 0, 3, 7 };
            controller.PageSize = 10;
            ViewResult result = controller.Index(null);
            DemandListViewModel viewModel = result.Model as DemandListViewModel;
            Assert.IsTrue(viewModel.Demands[0].AllowFinish);
            Assert.IsTrue(viewModel.Demands[1].AllowFinish);
            Assert.IsTrue(viewModel.Demands[2].AllowFinish);
            Assert.AreEqual(viewModel.Demands[2].City, "佛山");

        }

        [TestMethod]
        public void TestDemandView_AllowFinish()
        {
            DemandView view = new DemandView() { AllowFinish = false };
            Assert.IsFalse(view.AllowFinish);
            view.AllowFinish = true;
            Assert.IsTrue(view.AllowFinish);
        }

        [TestMethod]
        public void TestViewModel_DemandView_AllowFinish()
        {
            userRepository.UserInformations.ElementAt(0).CityPermissioin = (1 << 21) - 1;
            controller.PageSize = 10;
            ViewResult result = controller.Index(null);
            DemandListViewModel viewModel = result.Model as DemandListViewModel;
            DemandView view = viewModel.Demands[3];
            view.AllowFinish = true;
            Assert.IsTrue(view.AllowFinish);
            viewModel.Demands[3].AllowFinish = true;
            Assert.IsTrue(viewModel.Demands[3].AllowFinish);
        }
    }
}
