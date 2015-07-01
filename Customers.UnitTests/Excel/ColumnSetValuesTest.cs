using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    [TestClass]
    public class ColumnSetValuesTest
    {
        private ExtendedColumnClass column = new ExtendedColumnClass();
        private Dictionary<string, string> values = new Dictionary<string, string>{
            {"First Field", "1"},
            {"Second Field", "2.3"},
            {"Third Field", "111"},
            {"Fourth Field", "2012-10-10"},
            {"Fifth Field", "bbb"}
        };

        [TestMethod]
        public void TestColumnSetValues_AllLegalValues()
        {
            column.SetValues(typeof(ExtendedColumnClass).GetColumnPropertyNames(), values);
            Assert.AreEqual(column.FirstField, 1);
            Assert.AreEqual(column.SecondField, 2.3);
            Assert.AreNotEqual(column.ThirdField, "111");
            Assert.AreEqual(column.FourthField, new DateTime(2012, 10, 10));
            Assert.AreEqual(column.FifthField, "bbb");
        }

        [TestMethod]
        public void TestColumnSetValues_SomeLegalValues()
        {
            values["Second Field"] = "11.a";
            values["Fourth Field"] = "11-11-0";
            column.SetValues(typeof(ExtendedColumnClass).GetColumnPropertyNames(), values);
            Assert.AreEqual(column.FirstField, 1);
            Assert.AreEqual(column.SecondField, 0);
            Assert.AreNotEqual(column.ThirdField, "111");
            Assert.AreEqual(column.FourthField, DateTime.Today);
            Assert.AreEqual(column.FifthField, "bbb");
        }
    }
}
