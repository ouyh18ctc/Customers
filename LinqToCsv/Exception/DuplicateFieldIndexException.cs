using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{

    /// <summary>
    /// Thrown when 2 or more fields or properties have the same FieldIndex in the CsvColumn attribute.
    /// </summary>
    public class DuplicateFieldIndexException : LinqToCsvException
    {
        public DuplicateFieldIndexException(
                    string typeName,
                    string fieldName,
                    string fieldName2,
                    int duplicateIndex) :
            base(string.Format(
                "Fields or properties \"{0}\" and \"{1}\" of type \"{2}\" have duplicate FieldIndex {3}.",
                fieldName,
                fieldName2,
                typeName,
                duplicateIndex))
        {
            Data["TypeName"] = typeName;
            Data["FieldName"] = fieldName;
            Data["FieldName2"] = fieldName2;
            Data["Index"] = duplicateIndex;
        }
    }


}
