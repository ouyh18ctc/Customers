using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when a field will be written to a file that has no names in the first line,
    /// but that field has no FieldIndex.
    /// </summary>
    public class ToBeWrittenButMissingFieldIndexException : LinqToCsvException
    {
        public ToBeWrittenButMissingFieldIndexException(
                    string typeName,
                    string fieldName) :
            base(string.Format(
                "Field or property \"{0}\" of type \"{1}\" will be written to a file, but does not have a FieldIndex. " +
                "This exception only happens for input files without column names in the first record.",
                fieldName,
                typeName))
        {
            Data["TypeName"] = typeName;
            Data["FieldName"] = fieldName;
        }
    }

}
