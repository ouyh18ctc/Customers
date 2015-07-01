using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    [TestClass]
    public class DataReadingRepositoryColumnTest
    {
        private DataReadingRepository<ColumnClass> repository = new DataReadingRepository<ColumnClass>();
        private Mock<IDataReader> mockReader = new Mock<IDataReader>();
        private int[] firstValues = { 10, 20, 30 };
        private double[] secondValues = { 1.1, 2.2, 3.3 };
        private int step;

        [TestInitialize]
        public void TestInitialize()
        {
            step = 0;
            mockReader.Setup(x => x.Read()).Callback(() => step++)
                .Returns(() => step < 4 ? true : false);
            mockReader.Setup(x => x.GetValue(0))
                .Returns(() => firstValues[step - 1]);
            mockReader.Setup(x => x.GetValue(1))
                .Returns(() => secondValues[step - 1]);
            mockReader.Setup(x => x.FieldCount).Returns(100);
            mockReader.Setup(x => x.GetName(It.IsAny<int>())).Returns("Undefined");
            mockReader.Setup(x => x.GetName(0)).Returns("First Field");
            mockReader.Setup(x => x.GetName(1)).Returns("Second Field");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            mockReader = new Mock<IDataReader>();
        }

        [TestMethod]
        public void TestDataReadingRepositoryColumn()
        {
            repository.Reading(mockReader.Object);
            Assert.IsNotNull(repository.DataList);
            Assert.AreEqual(repository.DataList.Count, 3);
            Assert.AreEqual(repository.DataList[0].FirstField, 10);
            Assert.AreEqual(repository.DataList[1].SecondField, 2.2);
            Assert.AreEqual(repository.DataList[2].FirstField, 30);
        }
    }
}
