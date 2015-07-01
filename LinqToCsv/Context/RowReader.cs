using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToCsv.Mapper;
using LinqToCsv.Exception;
using LinqToCsv.Description;

namespace LinqToCsv.Context
{
    public class RowReader<T>
        where T : class, new()
    {
        private T obj;

        public T Obj
        {
            get { return obj; }
        }

        private CsvFileDescription description;
        private AggregatedException ae;

        public RowReader(CsvFileDescription description, AggregatedException ae)
        { 
            obj = default(T);
            this.description = description;
            this.ae = ae;
        }

        public bool ReadingOneRawRow(IDataRow row, bool firstRow)
        {
            if (firstRow && description.FirstLineHasColumnNames)
            {
                return false;
            }
            else
            {
                try
                {
                    obj = row as T;
                }
                catch (AggregatedException ae2)
                {
                    throw ae2;
                }
                catch (System.Exception e)
                {
                    ae.AddException(e);
                }
                return true;
            }
        }

        public bool ReadingOneFieldRow(FieldMapper_Reading<T> fm, IDataRow row, bool firstRow)
        {
            if (firstRow && description.FirstLineHasColumnNames)
            {
                fm.ReadNames(row); 
                return false;
            }
            else
            {
                try
                {
                    obj = fm.ReadObject(row, ae);
                }
                catch (AggregatedException ae2)
                {
                    throw ae2;
                }
                catch (System.Exception e)
                {
                    ae.AddException(e);
                }
                return true;
            }
        }

    }
}
