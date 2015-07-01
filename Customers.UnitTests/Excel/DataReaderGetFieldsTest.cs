using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    [TestClass]
    public class DataReaderGetFieldsTest : MockDataReader
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Initialize();
        }

        [TestMethod]
        public void TestDataReaderGetFields_AllNameExisted()
        {
            IEnumerable<string> names = new List<string> {
                "地市", "eNodeB ID", "经度", "纬度" };
            Dictionary<string, string> result = mockReader.Object.GetFields(names);
            Assert.AreEqual(result.Count(), 4);
            Assert.AreEqual(result["经度"], "112.123");
        }

        [TestMethod]
        public void TestDataReaderGetFields_SomeNameExisted()
        {
            IEnumerable<string> names = new List<string> {
                "地市", "eNodeB ID", "经度", "海拔" };
            Dictionary<string, string> result = mockReader.Object.GetFields(names);
            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result["eNodeB ID"], "3344");
            Assert.IsFalse(result.ContainsKey("海拔"));
            Assert.IsTrue(result.ContainsKey("地市"));
            Assert.IsFalse(result.ContainsKey("纬度"));
        }
    }
}
