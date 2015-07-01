using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCsv.Exception
{
    /// <summary>
    /// Thrown when one or more Exceptions were raised while
    /// reading data record from a file.
    /// 
    /// Contains a List with all the Exceptions.
    /// </summary>
    public class AggregatedException : LinqToCsvException
    {
        public List<System.Exception> m_InnerExceptionsList;
        private int m_MaximumNbrExceptions = 100;

        public AggregatedException(string typeName, string fileName, int maximumNbrExceptions) :
            base(string.Format(
                 "There were 1 or more exceptions while reading data using type \"{0}\"." +
                 FileNameMessage(fileName),
                 typeName))
        {
            m_MaximumNbrExceptions = maximumNbrExceptions;
            m_InnerExceptionsList = new List<System.Exception>();


            Data["TypeName"] = typeName;
            Data["FileName"] = fileName;
            Data["InnerExceptionsList"] = m_InnerExceptionsList;
        }

        public void AddException(System.Exception e)
        {
            m_InnerExceptionsList.Add(e);
            if ((m_MaximumNbrExceptions != -1) &&
                (m_InnerExceptionsList.Count >= m_MaximumNbrExceptions))
            {
                throw this;
            }
        }

        public void ThrowIfExceptionsStored()
        {
            if (m_InnerExceptionsList.Count > 0)
            {
                throw this;
            }
        }
    }

}
