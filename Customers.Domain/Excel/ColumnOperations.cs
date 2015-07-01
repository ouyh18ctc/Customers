using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace Customers.Domain.Excel
{
    public static class ColumnOperations
    {
        public static MethodInfo GetParseNumberMethod(this Type t)
        {
            return t.GetMethod("Parse",
                new Type[] { typeof(String), typeof(NumberStyles), typeof(IFormatProvider) });
        }

        public static MethodInfo GetParseMethod(this Type t)
        {
            return t.GetMethod("Parse", new Type[] { typeof(String) });
        }

        public static MethodInfo GetParseExactMethod(this Type t)
        {
            return t.GetMethod("ParseExact",
                new Type[] { typeof(string), typeof(string), typeof(IFormatProvider) });
        }

        public static void SetValueByText<T>(this T result, PropertyInfo property, string valueText)
            where T : class, new()
        {
            Type propertyType = property.PropertyType;
            string dateTimeFormat = (Attribute.GetCustomAttribute(property, typeof(ColumnAttribute))
                as ColumnAttribute).DateTimeFormat;
            try
            {
                property.SetValue(result,
                    (propertyType == typeof(string) ? valueText
                    : (propertyType == typeof(DateTime) ?
                    valueText.ConvertToDateTime(DateTime.Today)
                    : propertyType.GetParseMethod().Invoke(propertyType,
                    new object[] { valueText }))),
                    null);
            }
            catch
            {
                property.SetValue(result, 0, null);
            }
        }

        public static Dictionary<PropertyInfo, string> GetColumnPropertyNames(this Type t)
        {
            Dictionary<PropertyInfo, string> names = new Dictionary<PropertyInfo, string>();
            foreach (PropertyInfo property in t.GetProperties())
            {
                ColumnAttribute attribute = Attribute.GetCustomAttribute(property, typeof(ColumnAttribute))
                    as ColumnAttribute;
                if (attribute != null)
                {
                    names.Add(property, attribute.Name);
                }
            }
            return names;
        }

        public static void SetValues<T>(this T result, Dictionary<PropertyInfo, string> names,
            Dictionary<string, string> values)
            where T : class, new()
        {
            foreach (KeyValuePair<PropertyInfo, string> name in names)
            {
                if (values.ContainsKey(name.Value) && name.Key.CanWrite)
                {
                    result.SetValueByText(name.Key, values[name.Value]);
                }
            }
        }
    }
}
