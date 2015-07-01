using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Customers.Domain.Excel
{
    public static class GeneralText
    {
        public static bool IsValidString(this string inputText, string regularExpression)
        {
            return (string.IsNullOrEmpty(regularExpression)) ? true
                : (Regex.IsMatch(inputText, regularExpression) ? true : false);
        }

        public static int StringListLength(this IEnumerable<string> stringList)
        {
            return stringList.Sum(s => s.Length);
        }

        public static IEnumerable<int> GetFieldWidth(this IEnumerable<string> stringList, int totalWidth)
        {
            int totalLength = stringList.StringListLength();
            return stringList.Select(s => s.Length * totalWidth / totalLength);
        }

        public static IEnumerable<int> GetFieldWidth(this IEnumerable<int> itemList, int totalWidth)
        {
            int totalLength = itemList.Sum();
            return itemList.Select(s => s * totalWidth / totalLength);
        }

        public static bool MatchKeyWordInLine(this string sLine, string keyWord)
        {
            return sLine.IndexOf(keyWord) != -1;
        }

        public static string[] GetSplittedFields(this string line)
        {
            return line.Split(new char[] { '=', ',', '\"', ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] GetSplittedFields(this string line, char splitter)
        {
            return line.Split(new char[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string GetSubStringInFirstBracket(this string line)
        {
            int index1 = line.IndexOf('(');
            int index2 = line.IndexOf(')');
            if (index2 == -1) { index2 = line.Length; }
            string ipData = line.Substring(index1 + 1, index2 - index1 - 1);
            return ipData;
        }

        public static int[] GenerateIpDataDigits(this string ipData, string errorTest)
        {
            int[] digits = new int[6];
            string[] buf = ipData.GetSplittedFields(',');
            if (buf.Length < 6) { throw new IOException("Too few fields"); }
            for (int i = 0; i < 6; i++)
            {
                try
                {
                    int digit = int.Parse(buf[i]);
                    if ((digit < 0) || (digit > 255)) { throw new IOException("At least one digit is negative number or two large"); }
                    digits[i] = digit;
                }
                catch
                { throw new IOException(errorTest); }
            }
            return digits;
        }

        public static StreamReader GetStreamReader(this string source)
        {
            byte[] stringAsByteArray = System.Text.Encoding.UTF8.GetBytes(source);
            Stream stream = new MemoryStream(stringAsByteArray);

            var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8);
            return streamReader;
        }
    }
}
