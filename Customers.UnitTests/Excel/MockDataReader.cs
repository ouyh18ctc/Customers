using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Data;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    public class MockDataReader
    {
        protected Mock<IDataReader> mockReader = new Mock<IDataReader>();

        public void Initialize()
        {
            mockReader.Setup(x => x.FieldCount).Returns(100);
            mockReader.Setup(x => x.GetName(It.IsAny<int>())).Returns("Undefined");
            mockReader.Setup(x => x.GetName(0)).Returns("地市");
            mockReader.Setup(x => x.GetValue(0)).Returns("Foshan");
            mockReader.Setup(x => x.GetName(1)).Returns("eNodeB ID");
            mockReader.Setup(x => x.GetValue(1)).Returns("3344");
            mockReader.Setup(x => x.GetName(2)).Returns("经度");
            mockReader.Setup(x => x.GetValue(2)).Returns("112.123");
            mockReader.Setup(x => x.GetName(3)).Returns("纬度");
            mockReader.Setup(x => x.GetValue(3)).Returns("23.456");
        }
    }
}
