using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class CityDefTest
    {
        [TestMethod]
        public void TestCityDef_GetName()
        {
            Assert.AreEqual(CityDef.Shantou.GetCityName(), "汕头");
            Assert.AreEqual(CityDef.Heyuan.GetCityName(), "河源");
        }

        [TestMethod]
        public void TestCityDef_GetName_Undefined()
        {
            Assert.AreEqual(CityDef.Undefined.GetCityName(), "未定义");
        }

        [TestMethod]
        public void TestCityDef_GetIndex()
        {
            Assert.AreEqual(("佛山").GetCityIndex(), CityDef.Foshan);
            Assert.AreEqual(("韶关").GetCityIndex(), CityDef.Shaoguan);
        }

        [TestMethod]
        public void TestCityDef_GetIndex_Undefined()
        {
            Assert.AreEqual(("北京").GetCityIndex(), CityDef.Undefined);
        }
    }
}
