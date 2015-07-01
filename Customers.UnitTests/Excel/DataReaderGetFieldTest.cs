using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    [TestClass]
    public class DataReaderGetFieldTest : MockDataReader
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Initialize();
        }

        [TestMethod]
        public void TestDataReaderGetField_Existed()
        {
            Assert.AreEqual(mockReader.Object.GetField("地市"), "Foshan");
            Assert.AreEqual(mockReader.Object.GetField("eNodeB ID"), "3344");
        }

        [TestMethod]
        public void TestDataReaderGetField_NotExisted()
        {
            Assert.AreEqual(mockReader.Object.GetField("Foo"), "");
        }
    }
}
