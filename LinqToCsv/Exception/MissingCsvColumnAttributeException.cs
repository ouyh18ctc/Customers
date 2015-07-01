using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when the file has a field name that corresponds to a field in the type,
    /// but that field does not have the CsvColumn attribute while only fields with that
    /// attribute can be used according to the CsvFileDescription.
    /// </summary>
    public class MissingCsvColumnAttributeException : LinqToCsvException
    {
        public MissingCsvColumnAttributeException(string typeName, string fieldName, string fileName) :
            base(string.Format(
                     "Field \"{0}\" in type \"{1}\" does not have the CsvColumn attribute." +
                     FileNameMessage(fileName),
                     fieldName,
                     typeName))
        {
            Data["TypeName"] = typeName;
            Data["FieldName"] = fieldName;
            Data["FileName"] = fileName;
        }
    }


}
