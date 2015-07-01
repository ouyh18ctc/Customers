using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Customers.Domain.Excel
{
    public class CsvColumnAttribute : ColumnAttribute
    {
        public NumberStyles NumberStyle { get; set; }
        public string OutputFormat { get; set; }
        public int CharLength { get; set; }


        public CsvColumnAttribute()
        {
            
            NumberStyle = NumberStyles.Any;
            OutputFormat = "G";
        }


        public CsvColumnAttribute(string name, int fieldIndex, bool canBeNull,
                    string outputFormat, NumberStyles numberStyle, int charLength)
            : base(name, fieldIndex, canBeNull)
        {

            NumberStyle = numberStyle;
            OutputFormat = outputFormat;


            CharLength = charLength;
        }
    }

}
