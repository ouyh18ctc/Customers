using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{

    /// <summary>
    /// Thrown when there are no names in the first line, so each field assigned to must have a FieldIndex,
    /// but there is a field that is both required (CanBeNull is false) and that doesn't have a FieldIndex.
    /// </summary>
    public class RequiredButMissingFieldIndexException : LinqToCsvException
    {
        public RequiredButMissingFieldIndexException(
                    string typeName,
                    string fieldName) :
            base(string.Format(
                "Field or property \"{0}\" of type \"{1}\" is required, but does not have a FieldIndex. " +
                "This exception only happens for files without column names in the first record.",
                fieldName,
                typeName))
        {
            Data["TypeName"] = typeName;
            Data["FieldName"] = fieldName;
        }
    }


}
