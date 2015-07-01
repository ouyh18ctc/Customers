using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when a data field has no corresponding field in the type with a FieldIndex.
    /// This means there is no guarantee that the data field will be assigned to the right
    /// field in the type.
    /// 
    /// All MissingFieldIndexExceptions get aggregated into
    /// an AggregatedException.
    /// </summary>
    public class MissingFieldIndexException : LinqToCsvException
    {
        public MissingFieldIndexException(string typeName, int lineNbr, string fileName) :
            base(string.Format(
                 "Line {0} has more fields then there are fields or properties in type \"{1}\" with a FieldIndex." +
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
