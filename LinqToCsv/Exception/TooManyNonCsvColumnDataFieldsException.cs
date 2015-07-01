using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when a data record has more fields then there are fields in the type with
    /// the CsvColumn attribute.
    /// 
    /// All  these Exceptions get aggregated into
    /// an AggregatedException.
    /// </summary>
    public class TooManyNonCsvColumnDataFieldsException : LinqToCsvException
    {
        public TooManyNonCsvColumnDataFieldsException(string typeName, int lineNbr, string fileName) :
            base(string.Format(
                     "Line {0} has more fields then there are fields or properties in type \"{1}\" with the CsvColumn attribute set." +
                     FileNameMessage(fileName),
                     lineNbr,
                     typeName))
        {
            Data["TypeName"] = typeName;
            Data["LineNbr"] = lineNbr;
            Data["FileName"] = fileName;
        }
    }


}
