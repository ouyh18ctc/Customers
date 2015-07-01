using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Customers.Domain.TypeDef;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    internal class SimpleClass
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateProperty { get; set; }
    }

    internal class DataClass
    {
        public int IntProperty { get; private set; }
        public string StringProperty { get; set; }

        public DataClass() { IntProperty = 2; }
        public DataClass(int intProperty) { IntProperty = intProperty; }
    }

    internal class ClassWithSomeReadonlyProperties
    {
        private int property1 = 0;

        public int Property1
        {
            get { return property1; }
        }

        public int Property2 { get; set; }

        public ClassWithSomeReadonlyProperties() { }

        public ClassWithSomeReadonlyProperties(int property1)
        {
            this.property1 = property1;
        }
    }

    [TestClass]
    public class GeneralClonePropertiesTest
    {
        [TestMethod]
        public void TestSimpleCloneProperties()
        {
            SimpleClass source = new SimpleClass();
            SimpleClass dest = new SimpleClass();
            source.IntProperty = 11;
            source.StringProperty = "aaa";
            source.DateProperty = new DateTime(2014, 2, 2);

            source.CloneProperties(dest);
            Assert.AreEqual(dest.IntProperty, 11);
            Assert.AreEqual(dest.StringProperty, "aaa");
            Assert.AreEqual(dest.DateProperty, new DateTime(2014, 2, 2));
        }

        [TestMethod]
        public void TestDataCloneProperties()
        {
            DataClass source = new DataClass(4) { StringProperty = "aaa" };
            DataClass dest = new DataClass() { StringProperty = "bbb" };
            source.CloneProperties(dest);
            Assert.AreEqual(dest.IntProperty, 4);
            Assert.AreEqual(dest.StringProperty, "aaa");
        }

        [TestMethod]
        public void TestCloneProperties_ClassWithSomeReadonlyProperties()
        {
            ClassWithSomeReadonlyProperties source = new ClassWithSomeReadonlyProperties(1) { Property2 = 100 };
            ClassWithSomeReadonlyProperties dest = new ClassWithSomeReadonlyProperties(2) { Property2 = 200 };
            source.CloneProperties(dest);
            Assert.AreEqual(dest.Property2, 100);
            Assert.AreEqual(dest.Property1, 2);
        }
    }
}
