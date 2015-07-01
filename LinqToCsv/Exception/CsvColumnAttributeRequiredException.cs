using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// CsvFileDescription.FirstLineHasColumnNames is false, then the only way
    /// to reliably identify data fields is by their order in the data file.
    /// To get that order, the Read and Write methods look at the FieldIndex property
    /// of the CsvColumn attribute of the fields/properties of the data class.
    /// 
    /// However, if CsvFileDescription.EnforceCsvColumnAttribute is false,
    /// then that implies that fields/properties that don't have the CsvColumn attribute
    /// (and therefore no FieldIndex), participate in reading and writing.
    /// 
    /// When this inconsistency within the CsvFileDescription object is detected,
    /// this exception is thrown.
    /// </summary>
    public class CsvColumnAttributeRequiredException : LinqToCsvException
    {
        public CsvColumnAttributeRequiredException() :
            base(
                "CsvFileDescription.EnforceCsvColumnAttribute is false, but needs to be true because " +
                "CsvFileDescription.FirstLineHasColumnNames is false. See the description for CsvColumnAttributeRequiredException.")
        {
        }
    }


}
