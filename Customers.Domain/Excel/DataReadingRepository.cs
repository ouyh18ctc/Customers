using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Customers.Domain.Excel
{
    public class DataReadingRepository<T> where T : class, new()
    {
        public List<T> DataList { get; private set; }

        private Dictionary<PropertyInfo, string> propertyNames;

        public DataReadingRepository()
        {
            DataList = new List<T>();
            propertyNames = typeof(T).GetColumnPropertyNames();
        }

        public void Reading(IDataReader dataReader)
        {
            DataList.Clear();
            while (dataReader.Read())
            {
                T entity = new T();
                entity.SetValues(propertyNames, dataReader.GetFields(propertyNames.Select(x => x.Value)));
                DataList.Add(entity);
            }
            dataReader.Close();
        }
    }
}
