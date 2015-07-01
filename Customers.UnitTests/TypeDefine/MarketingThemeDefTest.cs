using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;

namespace Customers.UnitTests.TypeDefine
{
    [TestClass]
    public class MarketingThemeDefTest
    {
        [TestMethod]
        public void TestMarketingThemeDef_GetDescription()
        {
            Assert.AreEqual(MarketingThemeDef.Festival.GetMarketingThemeDescription(), "双节市场");
            Assert.AreEqual(MarketingThemeDef.Campus.GetMarketingThemeDescription(), "飞young校园");
            Assert.AreEqual(MarketingThemeDef.Others.GetMarketingThemeDescription(), "其他");
        }

        [TestMethod]
        public void TestMarketingThemeDef_GetIndex()
        {
            Assert.AreEqual(("流动市场").GetMarketingThemeIndex(), MarketingThemeDef.Flow);
            Assert.AreEqual(("固定市场").GetMarketingThemeIndex(), MarketingThemeDef.Others);
        }
    }
}
