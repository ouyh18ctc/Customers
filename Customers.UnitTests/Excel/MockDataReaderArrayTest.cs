using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;

namespace Customers.UnitTests.Excel
{
    [TestClass]
    public class MockDataReaderArrayTest
    {
        private Mock<IDataReader> mockReader = new Mock<IDataReader>();
        private int step;
        private int[] values = { 4, 5 };

        [TestInitialize]
        public void TestInitialize()
        {
            step = 0;
            mockReader.Setup(x => x.Read()).Callback(() => step++)
                .Returns(() => step < 3 ? true : false);
            mockReader.Setup(x => x.GetValue(0))
                .Returns(() => values[step - 1]);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            mockReader = new Mock<IDataReader>();
        }

        [TestMethod]
        public void TestMockDataReaderArray_Read()
        {
            Assert.IsTrue(mockReader.Object.Read());
            Assert.AreEqual(step, 1);
            Assert.IsTrue(mockReader.Object.Read());
            Assert.AreEqual(step, 2);
            Assert.IsFalse(mockReader.Object.Read());
            Assert.AreEqual(step, 3);
        }

        [TestMethod]
        public void TestMockDataReaderArray_GetValue()
        {
            mockReader.Object.Read();
            Assert.AreEqual(mockReader.Object.GetValue(0), 4);
            Assert.AreEqual(mockReader.Object.GetValue(0), 4);
            mockReader.Object.Read();
            Assert.AreEqual(mockReader.Object.GetValue(0), 5);
            Assert.AreEqual(mockReader.Object.GetValue(0), 5);
            Assert.AreEqual(mockReader.Object.GetValue(0), 5);
        }
    }
}
