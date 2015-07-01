using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;
using LinqToCsv.Description;
using LinqToCsv.Exception;

namespace LinqToCsv.Mapper
{
    public class FieldMapper<T>
    {
        protected FieldIndexInfo fieldIndexInfo;

        public FieldIndexInfo FieldIndexInfo
        {
            get { return this.fieldIndexInfo; }
        }

        // Used to build IndexToInfo
        protected Dictionary<string, TypeFieldInfo> nameToInfo = null;

        public Dictionary<string, TypeFieldInfo> NameToInfo
        {
            get { return nameToInfo; }
        }

        protected CsvFileDescription _fileDescription;

        // Only used when throwing an exception
        protected string _fileName;

        // -----------------------------
        // AnalyzeTypeField
        //
        private TypeFieldInfo AnalyzeTypeField(
                                MemberInfo mi,
                                bool allRequiredFieldsMustHaveFieldIndex,
                                bool allCsvColumnFieldsMustHaveFieldIndex)
        {
            TypeFieldInfo tfi = new TypeFieldInfo();
            tfi.MemberInfo = mi;
            tfi.UpdateParseParameters(_fileDescription.UseOutputFormatForParsingCsvValue);
            tfi.UpdateAttributes();
            tfi.ValidateAttributes<T>(allCsvColumnFieldsMustHaveFieldIndex, allRequiredFieldsMustHaveFieldIndex);

            return tfi;
        }

        protected void AnalyzeType(
                        Type type,
                        bool allRequiredFieldsMustHaveFieldIndex,
                        bool allCsvColumnFieldsMustHaveFieldIndex)
        {  
            InitializeNameToInfo(type, allRequiredFieldsMustHaveFieldIndex, allCsvColumnFieldsMustHaveFieldIndex);

            // -------
            // Initialize IndexToInfo
            this.fieldIndexInfo = new FieldIndexInfo(nameToInfo);

            // ----------
            // Make sure there are no duplicate FieldIndices.
            // However, allow gaps in the FieldIndex range, to make it easier to later insert
            // fields in the range.
            int lastFieldIndex = Int32.MinValue;
            string lastName = "";
            foreach (TypeFieldInfo tfi in fieldIndexInfo.IndexToInfo)
            {
                lastName = tfi.UpdateLastName<T>(lastName, ref lastFieldIndex);
            }
        }

        private void InitializeNameToInfo(Type type, bool allRequiredFieldsMustHaveFieldIndex, bool allCsvColumnFieldsMustHaveFieldIndex)
        {
            nameToInfo.Clear();

            foreach (MemberInfo mi in type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                // Only process field and property members.
                if ((mi.MemberType == MemberTypes.Field) ||
                    (mi.MemberType == MemberTypes.Property))
                {
                    // Note that the compiler does not allow fields and/or properties
                    // with the same name as some other field or property.
                    TypeFieldInfo tfi =
                        AnalyzeTypeField(mi,
                                allRequiredFieldsMustHaveFieldIndex,
                                allCsvColumnFieldsMustHaveFieldIndex);
                    nameToInfo[tfi.Name] = tfi;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileDescription"></param>
        public FieldMapper(CsvFileDescription fileDescription, string fileName, bool writingFile)
        {
            if ((!fileDescription.FirstLineHasColumnNames) &&
                (!fileDescription.EnforceCsvColumnAttribute))
            {
                throw new CsvColumnAttributeRequiredException();
            }

            _fileDescription = fileDescription;
            _fileName = fileName;
            nameToInfo = new Dictionary<string, TypeFieldInfo>();

            AnalyzeType(
                typeof(T),
                !fileDescription.FirstLineHasColumnNames,
                writingFile && !fileDescription.FirstLineHasColumnNames);
        }

        /// <summary>
        /// Writes the field names given in T to row.
        /// </summary>
        /// 
        public void WriteNames(List<string> row)
        {
            row.Clear();

            for (int i = 0; i < this.fieldIndexInfo.IndexToInfo.Length; i++)
            {
                TypeFieldInfo tfi = this.fieldIndexInfo.IndexToInfo[i];

                if (_fileDescription.EnforceCsvColumnAttribute &&
                        (!tfi.HasColumnAttribute))
                {
                    continue;
                }

                row.Add(tfi.Name);
            }
        }

        public void WriteObject(T obj, List<string> row)
        {
            row.Clear();
            
            for (int i = 0; i < this.fieldIndexInfo.IndexToInfo.Length; i++)
            {
                TypeFieldInfo tfi = this.fieldIndexInfo.IndexToInfo[i];

                if (_fileDescription.EnforceCsvColumnAttribute &&
                        (!tfi.HasColumnAttribute))
                {
                    continue;
                }

                Object objValue = null;


                if (tfi.MemberInfo is PropertyInfo)
                {
                    objValue =
                        ((PropertyInfo)tfi.MemberInfo).GetValue(obj, null);
                }
                else
                {
                    objValue =
                        ((FieldInfo)tfi.MemberInfo).GetValue(obj);
                }

                string resultString = null;
                if (objValue != null)
                {
                    if ((objValue is IFormattable))
                    {
                        resultString =
                            ((IFormattable)objValue).ToString(
                                tfi.OutputFormat,
                                _fileDescription.FileCultureInfo);
                    }
                    else
                    {
                        resultString = objValue.ToString();
                    }
                }

                row.Add(resultString);
            }
        }
    }

}
