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
    public class MockDataReaderStateTest
    {
        private Mock<IDataReader> mockReader = new Mock<IDataReader>(MockBehavior.Strict);

        [TestInitialize]
        public void TestInitialize()
        {
            mockReader.As<IDisposable>().Setup(d => d.Dispose());
            mockReader.SetupSequence(rdr => rdr.Read())
                .Returns(true)
                .Returns(true)
                .Returns(true)
                .Returns(false);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            mockReader = new Mock<IDataReader>();
        }

        [TestMethod]
        public void TestDataReaderState_Read()
        {
            Assert.IsTrue(mockReader.Object.Read());
            Assert.IsTrue(mockReader.Object.Read());
            Assert.IsTrue(mockReader.Object.Read());
            Assert.IsFalse(mockReader.Object.Read());
        }

        [TestMethod]
        public void TestDataReaderState_GetValue()
        {
            mockReader.Setup(rdr => rdr.GetValue(It.IsAny<int>())).Returns(100);
            mockReader.SetupSequence(rdr => rdr.GetValue(0))
                .Returns(1)
                .Returns(2)
                .Returns(3);
            Assert.AreEqual(mockReader.Object.GetValue(0), 1);
            Assert.AreEqual(mockReader.Object.GetValue(0), 2);
            Assert.AreEqual(mockReader.Object.GetValue(0), 3);
        }
    }
}
