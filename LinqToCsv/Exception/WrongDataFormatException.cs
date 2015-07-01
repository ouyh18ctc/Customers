using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when a data field has the wrong format (for example a number field with letters).
    /// 
    /// All WrongDataFormatExceptions get aggregated into
    /// an AggregatedException.
    /// </summary>
    public class WrongDataFormatException : LinqToCsvException
    {
        public WrongDataFormatException(
                        string typeName,
                        string fieldName,
                        string fieldValue,
                        int lineNbr,
                        string fileName,
                        System.Exception innerExc) :
            base(
                 string.Format(
                     "Value \"{0}\" in line {1} has the wrong format for field or property \"{2}\" in type \"{3}\"." +
                     FileNameMessage(fileName),
                     fieldValue,
                     lineNbr,
                     fieldName,
                     typeName),
                 innerExc)
        {
            Data["TypeName"] = typeName;
            Data["LineNbr"] = lineNbr;
            Data["FileName"] = fileName;
            Data["FieldValue"] = fieldValue;
            Data["FieldName"] = fieldName;
        }
    }


}
