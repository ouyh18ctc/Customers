using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when the stream passed to Read is either null, or does not support Seek.
    /// It has to support Seek, because that way it can rewind when the stream is accessed again.
    /// </summary>
    public class BadStreamException : LinqToCsvException
    {
        public BadStreamException() :
            base(
                "Stream provided to Read is either null, or does not support Seek.")
        {
        }
    }

}
