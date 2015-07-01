using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when a type field/property has no corresponding field in the data for the corresponding FieldIndex.
    /// This means that the FieldIndex valus is greater then the number of items in a data.
    /// 
    /// All WrongFieldIndexException get aggregated into
    /// an AggregatedException.
    /// </summary>
    public class WrongFieldIndexException : LinqToCsvException
    {
        public WrongFieldIndexException(string typeName, int lineNbr, string fileName) :
            base(string.Format(
                 "Line {0} has less fields then the FieldIndex value is indicating in type \"{1}\" ." +
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
