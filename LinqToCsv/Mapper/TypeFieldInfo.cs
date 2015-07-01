using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;
using Customers.Domain.Excel;
using LinqToCsv.Attribute;
using LinqToCsv.Exception;

namespace LinqToCsv.Mapper
{
    public class TypeFieldInfo : IComparable<TypeFieldInfo>
    {
        private int index = CsvColumnAttribute.mc_DefaultFieldIndex;

        public int Index
        {
            get { return index; }
        }

        private string name = null;

        public string Name
        {
            get { return name; }
        }

        private bool canBeNull = true;

        public bool CanBeNull
        {
            get { return canBeNull; }
        }

        private NumberStyles inputNumberStyle = NumberStyles.Any;

        private string outputFormat = null;

        public string OutputFormat
        {
            get { return outputFormat; }
        }

        private bool hasColumnAttribute = false;

        public bool HasColumnAttribute
        {
            get { return hasColumnAttribute; }
        }

        public int charLength = 0;

        public void UpdateAttributes()
        {
            this.index = CsvColumnAttribute.mc_DefaultFieldIndex;
            this.name = this.memberInfo.Name;
            this.inputNumberStyle = NumberStyles.Any;
            this.outputFormat = "";
            this.hasColumnAttribute = false;
            this.charLength = 0;

            foreach (Object attribute in this.memberInfo.GetCustomAttributes(typeof(CsvColumnAttribute), true))
            {
                CsvColumnAttribute cca = (CsvColumnAttribute)attribute;

                if (!string.IsNullOrEmpty(cca.Name))
                {
                    this.name = cca.Name;
                }
                this.index = cca.FieldIndex;
                this.hasColumnAttribute = true;
                this.canBeNull = cca.CanBeNull;
                this.outputFormat = cca.OutputFormat;
                this.inputNumberStyle = cca.NumberStyle;
                this.charLength = cca.CharLength;
            }
        }

        public void ValidateAttributes<T>(bool allCsvColumnFieldsMustHaveFieldIndex,
            bool allRequiredFieldsMustHaveFieldIndex)
        {
            if (allCsvColumnFieldsMustHaveFieldIndex &&
                this.hasColumnAttribute &&
                this.index == CsvColumnAttribute.mc_DefaultFieldIndex)
            {
                throw new ToBeWrittenButMissingFieldIndexException(
                                typeof(T).ToString(),
                                this.name);
            }
            
            if (allRequiredFieldsMustHaveFieldIndex && (!this.canBeNull) &&
                (this.index == CsvColumnAttribute.mc_DefaultFieldIndex))
            {
                throw new RequiredButMissingFieldIndexException(typeof(T).ToString(), this.name);
            }
        }

        private MemberInfo memberInfo = null;

        public MemberInfo MemberInfo
        {
            get { return memberInfo; }
            set 
            { 
                memberInfo = value;

                if (value is PropertyInfo)
                {
                    this.fieldType = ((PropertyInfo)value).PropertyType;
                }
                else
                {
                    this.fieldType = ((FieldInfo)value).FieldType;
                }

            }
        }

        private Type fieldType = null;
        
        // parseNumberMethod will remain null if the property is not a numeric type.
        // This would be the case for DateTime, Boolean, String and custom types.
        // In those cases, just use a TypeConverter.
        //
        // DateTime and Boolean also have Parse methods, but they don't provide
        // functionality that TypeConverter doesn't give you.
        
        private TypeConverter typeConverter = null;
        private MethodInfo parseNumberMethod = null;
        private MethodInfo parseExactMethod;

        public void UpdateParseParameters(bool useOutputFormatForParsingCsvValue)
        {
            this.parseNumberMethod = this.fieldType.GetParseNumberMethod();

            if (this.parseNumberMethod == null)
            {
                if (useOutputFormatForParsingCsvValue)
                {
                    this.parseExactMethod = this.fieldType.GetParseExactMethod();
                }

                this.typeConverter = null;
                if (this.parseExactMethod == null)
                {
                    this.typeConverter = TypeDescriptor.GetConverter(this.fieldType);
                }
            }

        }

        public int CompareTo(TypeFieldInfo other)
        {
            return index.CompareTo(other.index);
        }
        
        public override string ToString()
        {
            return string.Format("Index: {0}, Name: {1}", index, name);
        }

        public string UpdateLastName<T>(string lastName, ref int lastFieldIndex)
        {
            if ((this.index == lastFieldIndex) &&
                    (this.index != CsvColumnAttribute.mc_DefaultFieldIndex))
            {
                throw new DuplicateFieldIndexException(typeof(T).ToString(),
                            this.Name, lastName, this.index);
            }

            lastFieldIndex = this.index;
            return this.Name;
        }

        public Object UpdateObjectValue(string value, 
            CultureInfo fileCultureInfo)
        {
            Object objValue = null;

            // Normally, either tfi.typeConverter is not null,
            // or tfi.parseNumberMethod is not null. 
            // 
            if (this.typeConverter != null)
            {
                objValue = this.typeConverter.ConvertFromString(null, fileCultureInfo, value);
            }
            else if (this.parseExactMethod != null)
            {
                objValue = this.parseExactMethod.Invoke(this.fieldType,
                    new Object[] { value, 
                        this.OutputFormat, 
                        fileCultureInfo });
            }
            else if (this.parseNumberMethod != null)
            {
                objValue = this.parseNumberMethod.Invoke(this.fieldType,
                    new Object[] { value, 
                        this.inputNumberStyle, 
                        fileCultureInfo });
            }
            else
            {
                // No TypeConverter and no Parse method available.
                // Try direct approach.
                objValue = value;
            }

            return objValue;
        }
    }

}
