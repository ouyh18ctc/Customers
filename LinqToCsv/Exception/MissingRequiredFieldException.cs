using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when a data field is empty, while its CsvColumn attribute has CanBeNull=false
    /// 
    /// All WrongDataFormatExceptions get aggregated into
    /// an AggregatedException.
    /// </summary>
    public class MissingRequiredFieldException : LinqToCsvException
    {
        public MissingRequiredFieldException(
                        string typeName,
                        string fieldName,
                        int lineNbr,
                        string fileName) :
            base(
                 string.Format(
                     "In line {0}, no value provided for required field or property \"{1}\" in type \"{2}\"." +
                     FileNameMessage(fileName),
                     lineNbr,
                     fieldName,
                     typeName))
        {
            Data["TypeName"] = typeName;
            Data["LineNbr"] = lineNbr;
            Data["FileName"] = fileName;
            Data["FieldName"] = fieldName;
        }
    }


}
