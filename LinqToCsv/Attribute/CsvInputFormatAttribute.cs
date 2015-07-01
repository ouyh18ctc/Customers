using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace LinqToCsv.Attribute
{
    /// <summary>
    /// Summary description for CsvInputFormat
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CsvInputFormatAttribute : System.Attribute
    {
        private NumberStyles m_NumberStyle = NumberStyles.Any;
        public NumberStyles NumberStyle
        {
            get { return m_NumberStyle; }
            set { m_NumberStyle = value; }
        }


        public CsvInputFormatAttribute(NumberStyles numberStyle)
        {
            m_NumberStyle = numberStyle;
        }
    }

}
