using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Data;

namespace Customers.Domain.Excel
{
    public static class PropertyOperations
    {
        public static void CloneProperties<T>(this T source, T destination, bool ignoreId = true)
            where T : class, new()
        {
            PropertyInfo[] properties = (typeof(T)).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!(property.CanRead && property.CanWrite)) { continue; }
                if (!(property.Name == "Id" && ignoreId))
                {
                    property.SetValue(destination, property.GetValue(source, null), null);
                }
            }
        }

        public static string GetField(this IDataReader dataReader, string fieldName)
        {
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (dataReader.GetName(i).Trim() == fieldName)
                {
                    return dataReader.GetValue(i).ToString().Trim();
                }
            }
            return "";
        }

        public static Dictionary<string, string> GetFields(this IDataReader dataReader, IEnumerable<string> names)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string name = names.FirstOrDefault(x => x == dataReader.GetName(i));
                if (!string.IsNullOrEmpty(name))
                {
                    fields.Add(name, dataReader.GetValue(i).ToString());
                }
            }
            return fields;
        }
    }
}
