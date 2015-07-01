using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Customers.Domain.Excel;
using LinqToCsv.Description;
using LinqToCsv.Mapper;
using LinqToCsv.StreamDef;

namespace LinqToCsv.Context
{
    /// <summary>
    /// Summary description for CsvContext
    /// </summary>
    public static class CsvContext
    {
       
        public static IEnumerable<T> Read<T>(string fileName, CsvFileDescription fileDescription) where T : class, new()
        {
            
            FileDataAccess dataAccess = new FileDataAccess(null, fileDescription);
            return dataAccess.ReadData<T>(fileName);
        }
        
        public static IEnumerable<T> Read<T>(StreamReader stream) where T : class, new()
        {
            return Read<T>(stream, new CsvFileDescription());
        }
        
        public static IEnumerable<T> Read<T>(string fileName) where T : class, new()
        {
            return Read<T>(fileName, new CsvFileDescription());
        }
        
        public static IEnumerable<T> Read<T>(StreamReader stream, CsvFileDescription fileDescription) where T : class, new()
        {
            FileDataAccess dataAccess = new FileDataAccess(stream, fileDescription);
            return dataAccess.ReadData<T>(null);
        }

        public static IEnumerable<T> ReadString<T>(string testInput, CsvFileDescription fileDescription) where T : class, new()
        {
            return Read<T>(testInput.GetStreamReader(), fileDescription);
        }

        public static void Write<T>(IEnumerable<T> values, string fileName, CsvFileDescription fileDescription)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, fileDescription.TextEncoding))
            {
                WriteData<T>(values, fileName, sw, fileDescription);
                sw.Close();
            }
        }

        public static void Write<T>(IEnumerable<T> values, TextWriter stream)
        {
            Write<T>(values, stream, new CsvFileDescription());
        }

        public static void Write<T>(IEnumerable<T> values, string fileName)
        {
            Write<T>(values, fileName, new CsvFileDescription());
        }

        public static void Write<T>(IEnumerable<T> values, TextWriter stream, CsvFileDescription fileDescription)
        {
            WriteData<T>(values, null, stream, fileDescription);
        }

        private static void WriteData<T>(IEnumerable<T> values, string fileName, TextWriter stream,
            CsvFileDescription fileDescription)
        {
            FieldMapper<T> fm = new FieldMapper<T>(fileDescription, fileName, true);
            CsvStream cs = new CsvStream(null, stream, fileDescription.SeparatorChar, 
                fileDescription.IgnoreTrailingSeparatorChar);

            List<string> row = new List<string>();

            // If first line has to carry the field names, write the field names now.
            if (fileDescription.FirstLineHasColumnNames)
            {
                fm.WriteNames(row);
                cs.WriteRow(row, fileDescription.QuoteAllFields);
            }

            foreach (T obj in values)
            {
                // Convert obj to row
                fm.WriteObject(obj, row);
                cs.WriteRow(row, fileDescription.QuoteAllFields);
            }
        }

    }

}
