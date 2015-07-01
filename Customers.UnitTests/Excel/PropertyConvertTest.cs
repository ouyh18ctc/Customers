using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    [TestClass]
    public class PropertyConvertTest
    {
        [TestMethod]
        public void TestPropertyConvert_ToByte()
        {
            Assert.AreEqual(("1").ConvertToByte(0), 1);
            Assert.AreEqual(("-1").ConvertToByte(0), 0);
        }

        [TestMethod]
        public void TestPropertyConvert_ToShort()
        {
            Assert.AreEqual(("22").ConvertToShort(0), 22);
            Assert.AreEqual(("as").ConvertToShort(0), 0);
        }

        [TestMethod]
        public void TestPropertyConvert_ToInt()
        {
            Assert.AreEqual(("-233").ConvertToInt(0), -233);
            Assert.AreEqual(("78.2").ConvertToInt(0), 0);
        }

        [TestMethod]
        public void TestPropertyConvert_ToLong()
        {
            Assert.AreEqual(("-2333").ConvertToLong(0), -2333);
            Assert.AreEqual(("78.2").ConvertToLong(0), 0);
        }

        [TestMethod]
        public void TestPropertyConvert_ToDouble()
        {
            Assert.AreEqual(("-2333").ConvertToDouble(0), -2333);
            Assert.AreEqual(("78.2").ConvertToDouble(0), 78.2);
            Assert.AreEqual(("a78.2").ConvertToDouble(0), 0);
        }

        [TestMethod]
        public void TestPropertyConvert_ToDateTime()
        {
            Assert.AreEqual(("").ConvertToDateTime(new DateTime(2012, 1, 1)), new DateTime(2012, 1, 1));
            Assert.AreEqual(("2013-10-1").ConvertToDateTime(new DateTime(2012, 1, 1)), new DateTime(2013, 10, 1));
            Assert.AreEqual(("2013-10-1 ").ConvertToDateTime(new DateTime(2012, 1, 1)), new DateTime(2013, 10, 1));
            Assert.AreEqual(("2013-10-01").ConvertToDateTime(new DateTime(2012, 1, 1)), new DateTime(2013, 10, 1));
            Assert.AreEqual(("2013/10/1").ConvertToDateTime(new DateTime(2012, 1, 1)), new DateTime(2013, 10, 1));
            Assert.AreEqual(("2013/10/01").ConvertToDateTime(new DateTime(2012, 1, 1)), new DateTime(2013, 10, 1));
            Assert.AreEqual(("10/01/2013").ConvertToDateTime(new DateTime(2012, 1, 1)), new DateTime(2013, 10, 1));
            Assert.AreEqual(("10-1-2013").ConvertToDateTime(new DateTime(2012, 1, 1)), new DateTime(2013, 10, 1));
            Assert.AreEqual(("2013-10-1 2:45").ConvertToDateTime(new DateTime(2012, 1, 1)), 
                new DateTime(2013, 10, 1, 2, 45, 0));
            Assert.AreEqual(("2013-10-1 2:45pm").ConvertToDateTime(new DateTime(2012, 1, 1)),
                new DateTime(2013, 10, 1, 14, 45, 0));
        }
    }
}
