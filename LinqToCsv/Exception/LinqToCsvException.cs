using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// All exceptions have a human readable message in the Message property,
    /// and machine readable data in the Data property.
    /// </summary>
    public class LinqToCsvException : System.Exception
    {
        public LinqToCsvException(
                    string message,
                    System.Exception innerException)
            : base(message, innerException)
        {
        }


        public LinqToCsvException(
                    string message)
            : base(message)
        {
        }

        public static string FileNameMessage(string fileName)
        {
            return ((fileName == null) ? "" : " Reading file \"" + fileName + "\".");
        }
    }
    
}
