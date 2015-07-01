using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LinqToCsv.Mapper;
using LinqToCsv.Description;
using LinqToCsv.Exception;
using LinqToCsv.StreamDef;

namespace LinqToCsv.Context
{
    public class FileDataAccess
    {
        private StreamReader stream;

        private CsvFileDescription fileDescription;

        public CsvFileDescription FileDescription
        {
            get { return fileDescription; }
        }

        private IDataRow row;

        public IDataRow Row
        {
            get { return row; }
            set { row = value; }
        }

        private CsvStream cs;

        public CsvStream Cs
        {
            get { return cs; }
        }

        private AggregatedException ae;

        public AggregatedException Ae
        {
            get { return ae; }
        }

        public FileDataAccess(StreamReader stream, CsvFileDescription fileDescription)
        {
            this.stream = stream;
            this.fileDescription = fileDescription;
            this.cs = new CsvStream(stream, null, fileDescription.SeparatorChar,
                fileDescription.IgnoreTrailingSeparatorChar);
        }

        public IEnumerable<T> ReadData<T>(string fileName) where T : class, new()
        {
            RowReader<T> reader = ReadDataPreparation<T>(fileName);
            if (typeof(IDataRow).IsAssignableFrom(typeof(T)))
            {                
                row = new T() as IDataRow;
                return this.ReadRawData<T>(reader, fileName);
            }
            else
            {
                row = new DataRow();
                return this.ReadFieldData<T>(reader, fileName);
            }
        }

        public RowReader<T> ReadDataPreparation<T>(string fileName) where T : class, new()
        {
            this.RewindStream(fileName);
            this.ae =
                new AggregatedException(typeof(T).ToString(), fileName, fileDescription.MaximumNbrExceptions);
            return new RowReader<T>(this.fileDescription, ae);
        }

        public IEnumerable<T> ReadRawData<T>(RowReader<T> reader, string fileName) where T : class, new()
        {
            bool firstRow = true;
            try
            {
                while (cs.ReadRow(row))
                {
                    if ((row.Count == 1) && ((row[0].Value == null) || (string.IsNullOrEmpty(row[0].Value.Trim()))))
                    {
                        continue;
                    }

                    bool readingResult = reader.ReadingOneRawRow(row, firstRow);

                    if (readingResult) { yield return reader.Obj; }
                    firstRow = false;
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    stream.Close();
                }

                ae.ThrowIfExceptionsStored();
            }

        }

        public IEnumerable<T> ReadFieldData<T>(RowReader<T> reader, string fileName) where T : class, new()
        {
            FieldMapper_Reading<T> fm = new FieldMapper_Reading<T>(fileDescription, fileName, false);
            List<int> charLengths = fm.GetCharLengths();
            return this.ReadFieldDataRows<T>(reader, fileName, fm, charLengths);
        }

        public IEnumerable<T> ReadFieldDataRows<T>(RowReader<T> reader, string fileName,
            FieldMapper_Reading<T> fm, List<int> charLengths) where T : class, new()
        {
            bool firstRow = true;

            try
            {
                while (cs.ReadRow(row, charLengths))
                {
                    if ((row.Count == 1) && ((row[0].Value == null) || (string.IsNullOrEmpty(row[0].Value.Trim()))))
                    {
                        continue;
                    }

                    bool readingResult = reader.ReadingOneFieldRow(fm, row, firstRow);

                    if (readingResult) { yield return reader.Obj; }
                    firstRow = false;
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    stream.Close();
                }

                ae.ThrowIfExceptionsStored();
            }
        }

        public void RewindStream(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                stream = new StreamReader(fileName, fileDescription.TextEncoding,
                    fileDescription.DetectEncodingFromByteOrderMarks);
            }
            else
            {
                if ((stream == null) || (!stream.BaseStream.CanSeek))
                {
                    throw new BadStreamException();
                }

                stream.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
