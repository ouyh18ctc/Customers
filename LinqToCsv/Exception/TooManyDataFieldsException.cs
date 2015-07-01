using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when a data record has too many fields.
    /// 
    /// All TooManyDataFieldsExceptions get aggregated into
    /// an AggregatedException.
    /// </summary>
    public class TooManyDataFieldsException : LinqToCsvException
    {
        public TooManyDataFieldsException(string typeName, int lineNbr, string fileName) :
            base(string.Format(
                     "Line {0} has more fields then are available in type \"{1}\"." +
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
