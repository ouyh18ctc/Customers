using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Customers.Domain.TypeDef;
using Customers.Domain.TableDef;
using Customers.WebUI.Controllers;
using Customers.Domain.Security;
using System.Web.Mvc;

namespace Customers.WebUI.Tests.Controllers
{
    [TestClass]
    public class NavControllerTest : MockDemandRepository
    {
        private NavController controller;
        private Mock<IUserInformationRepository> mockUserRepository = new Mock<IUserInformationRepository>();

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeMockObject();
            UserInformation user = new UserInformation
            {
                UserName = "admin",
                CityPermissioin = (1 << 21) - 1
            };
            mockUserRepository.Setup(x => x.UserInformations).Returns(new List<UserInformation>{
                user }.AsQueryable());
            controller = new NavController(mockRepository.Object, mockUserRepository.Object, null);
            controller.CurrentUser = "admin";
        }

        [TestMethod]
        public void TestNavController_Menu_AllCities()
        {
            PartialViewResult viewResult = controller.Menu();
            Assert.IsNotNull(viewResult);
            IEnumerable<string> cityList = viewResult.Model as IEnumerable<string>;
            Assert.IsNotNull(cityList);
            Assert.AreEqual(cityList.Count(), 2);
            Assert.AreEqual(cityList.ElementAt(0), "广州");
            Assert.AreEqual(cityList.ElementAt(1), "佛山");
        }

        [TestMethod]
        public void TestNavController_Menu_FoshanCity()
        {
            PartialViewResult viewResult = controller.Menu("佛山");
            Assert.AreEqual(viewResult.ViewBag.SelectedCity, "佛山");
            Assert.IsNotNull(viewResult);
            IEnumerable<string> cityList = viewResult.Model as IEnumerable<string>;
            Assert.IsNotNull(cityList);
            Assert.AreEqual(cityList.Count(), 2);
            Assert.AreEqual(cityList.ElementAt(0), "广州");
            Assert.AreEqual(cityList.ElementAt(1), "佛山");
        }
    }
}
