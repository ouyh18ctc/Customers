using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using LinqToCsv.Description;
using LinqToCsv.Exception;
using LinqToCsv.Attribute;
using Customers.Domain.Excel;

namespace LinqToCsv.Mapper
{
    public class FieldMapper_Reading<T> : FieldMapper<T> where T : new()
    {
        /// </summary>
        /// <param name="fileDescription"></param>
        public FieldMapper_Reading(
                    CsvFileDescription fileDescription,
                    string fileName,
                    bool writingFile)
            : base(fileDescription, fileName, writingFile)
        {
        }

        /// <summary>
        /// Assumes that the fields in parameter row are field names.
        /// Reads the names into the objects internal structure.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="firstRow"></param>
        /// <returns></returns>
        ///
        public void ReadNames(IDataRow row)
        {
            // It is now the order of the field names that determines
            // the order of the elements in m_IndexToInfo, instead of
            // the FieldIndex fields.
            // If there are more names in the file then fields in the type,
            // and IgnoreUnknownColumns is set to `false` one of the names will 
            // not be found, causing an exception.
            int currentNameIndex = 0;
            for (int i = 0; i < row.Count; i++)
            {
                if (!nameToInfo.ContainsKey(row[i].Value))
                {
                    //If we have to ignore this column
                    if (_fileDescription.IgnoreUnknownColumns)
                    {
                        continue;
                    }

                    // name not found
                    throw new NameNotInTypeException(typeof(T).ToString(), row[i].Value, _fileName);
                }

                //Map the column index in the CSV file with the column index of the business object.
                this.fieldIndexInfo.AddMappingIndex(i, currentNameIndex);
                currentNameIndex++;
            }

            this.fieldIndexInfo.UpdateIndexToInfo<T>(row, nameToInfo, _fileDescription.EnforceCsvColumnAttribute,
                _fileName);
        }


        public List<int> GetCharLengths()
        {
            if (!_fileDescription.NoSeparatorChar)
                return null;
            return this.fieldIndexInfo.GetCharLengthList();
        }

        /// <summary>
        /// Creates an object of type T from the data in row and returns that object.
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="firstRow"></param>
        /// <returns></returns>
        public T ReadObject(IDataRow row, AggregatedException ae)
        {
            //If there are more columns than the required
            if (row.Count > this.fieldIndexInfo.IndexToInfo.Length)
            {
                //Are we ignoring unknown columns?
                if (!_fileDescription.IgnoreUnknownColumns)
                {
                    // Too many fields
                    throw new TooManyDataFieldsException(typeof(T).ToString(), row[0].LineNbr, _fileName);
                }
            }

            T obj = new T();

            //If we will be using the mappings, we just iterate through all the cells in this row
            int maxRowCount = this.fieldIndexInfo.GetMaxRowCount(row.Count);


            for (int i = 0; i < maxRowCount; i++)
            {
                TypeFieldInfo tfi = this.fieldIndexInfo.QueryTypeFieldInfo(_fileDescription.IgnoreUnknownColumns, i);
                if (tfi == null) { continue; }

                if (_fileDescription.EnforceCsvColumnAttribute &&
                        (!tfi.HasColumnAttribute))
                {
                    // enforcing column attr, but this field/prop has no column attr.
                    // So there are too many fields in this record.
                    throw new TooManyNonCsvColumnDataFieldsException(typeof(T).ToString(), row[i].LineNbr, _fileName);
                }

                if ((!_fileDescription.FirstLineHasColumnNames) &&
                        (tfi.Index == CsvColumnAttribute.mc_DefaultFieldIndex))
                {
                    // First line in the file does not have field names, so we're 
                    // depending on the FieldIndex of each field in the type
                    // to ensure each value is placed in the correct field.
                    // However, now hit a field where there is no FieldIndex.
                    throw new MissingFieldIndexException(typeof(T).ToString(), row[i].LineNbr, _fileName);
                }

                if (_fileDescription.UseFieldIndexForReadingData && (!_fileDescription.FirstLineHasColumnNames) &&
                    (tfi.Index > row.Count))
                {
                    // First line in the file does not have field names, so we're 
                    // depending on the FieldIndex of each field in the type
                    // to ensure each value is placed in the correct field.
                    // However, now hit a field where the FieldIndex is bigger
                    // than the total number of items in a row generated by the separatorChar
                    throw new WrongFieldIndexException(typeof(T).ToString(), row[i].LineNbr, _fileName);
                }


                int index = _fileDescription.UseFieldIndexForReadingData ? tfi.Index - 1 : i;

                // value to put in the object
                string value = row[index].Value;

                if (value == null)
                {
                    if (!tfi.CanBeNull)
                    {
                        ae.AddException(new MissingRequiredFieldException(
                                    typeof(T).ToString(), tfi.Name, row[i].LineNbr, _fileName));
                    }
                }
                else
                {
                    try
                    {
                        Object objValue = tfi.UpdateObjectValue(value, _fileDescription.FileCultureInfo);

                        if (tfi.MemberInfo is PropertyInfo)
                        {
                            ((PropertyInfo)tfi.MemberInfo).SetValue(obj, objValue, null);
                        }
                        else
                        {
                            ((FieldInfo)tfi.MemberInfo).SetValue(obj, objValue);
                        }
                    }
                    catch (System.Exception e)
                    {
                        if (e is TargetInvocationException)
                        {
                            e = e.InnerException;
                        }

                        if (e is FormatException)
                        {
                            e = new WrongDataFormatException(typeof(T).ToString(), tfi.Name,
                                    value, row[i].LineNbr, _fileName, e);
                        }
                        ae.AddException(e);
                    }
                }
            }

            // Visit any remaining fields in the type for which no value was given
            // in the data row, to see whether any of those was required.
            // If only looking at fields with CsvColumn attribute, do ignore
            // fields that don't have that attribute.
            for (int i = row.Count; i < this.fieldIndexInfo.IndexToInfo.Length; i++)
            {
                TypeFieldInfo tfi = this.fieldIndexInfo.IndexToInfo[i];

                if (((!_fileDescription.EnforceCsvColumnAttribute) || tfi.HasColumnAttribute) &&
                    (!tfi.CanBeNull))
                {
                    ae.AddException(new MissingRequiredFieldException(typeof(T).ToString(), tfi.Name,
                        row[row.Count - 1].LineNbr, _fileName));
                }
            }
            return obj;
        }
    }
}
