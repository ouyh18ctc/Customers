using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.Excel
{
    [System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public static int mc_DefaultFieldIndex = Int32.MaxValue;

        public string Name { get; set; }

        public bool CanBeNull { get; set; }

        public int FieldIndex { get; set; }

        public string DateTimeFormat { get; set; }

        public ColumnAttribute()
        {
            Name = "";

            FieldIndex = mc_DefaultFieldIndex;

            CanBeNull = true;
        }

        public ColumnAttribute(string name, int fieldIndex, bool canBeNull)
        {
            Name = name;

            FieldIndex = fieldIndex;

            CanBeNull = canBeNull;
        }
    }
}
