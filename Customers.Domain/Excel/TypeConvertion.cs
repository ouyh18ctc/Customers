using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.Excel
{
    public static class TypeConvertion
    {
        public static byte ConvertToByte(this string text, byte defaultValue)
        {
            try
            {
                return Convert.ToByte(text);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static short ConvertToShort(this string text, short defaultValue)
        {
            try
            {
                return Convert.ToInt16(text);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int ConvertToInt(this string text, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(text);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long ConvertToLong(this string text, long defaultValue)
        {
            try
            {
                return Convert.ToInt64(text);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double ConvertToDouble(this string text, double defaultValue)
        {
            try
            {
                return Convert.ToDouble(text);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime ConvertToDateTime(this string text, DateTime defaultValue)
        {
            try
            {
                return Convert.ToDateTime(text);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
