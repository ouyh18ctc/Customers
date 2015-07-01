using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.Excel;

namespace Customers.UnitTests.Excel
{
    public class ColumnClass
    {
        [Column(Name = "First Field", CanBeNull = true)]
        public int FirstField { get; set; }

        [Column(Name = "Second Field", FieldIndex = 2)]
        public double SecondField { get; set; }
    }

    internal class ExtendedColumnClass : ColumnClass
    {
        public string ThirdField { get; set; }

        [Column(Name = "Fourth Field")]
        public DateTime FourthField { get; set; }

        [Column(Name = "Fifth Field")]
        public string FifthField { get; set; }
    }
}
