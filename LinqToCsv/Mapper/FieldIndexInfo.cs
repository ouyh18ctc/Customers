using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToCsv.Description;
using LinqToCsv.Exception;

namespace LinqToCsv.Mapper
{
    public class FieldIndexInfo
    {
        // IndexToInfo is used to quickly translate the index of a field
        // to its TypeFieldInfo.
        private TypeFieldInfo[] indexToInfo = null;

        public TypeFieldInfo[] IndexToInfo
        {
            get { return indexToInfo; }
        }

        /// <summary>
        /// Contains a mapping between the CSV column indexes that will read and the property indexes in the business object.
        /// </summary>
        protected IDictionary<int, int> _mappingIndexes = new Dictionary<int, int>();

        protected Dictionary<string, TypeFieldInfo> nameToInfo;

        public FieldIndexInfo(Dictionary<string, TypeFieldInfo> nameToInfo)
        {
            this.nameToInfo = nameToInfo;
            int nbrTypeFields = nameToInfo.Keys.Count;
            this.indexToInfo = new TypeFieldInfo[nbrTypeFields];
            _mappingIndexes = new Dictionary<int, int>();

            int i = 0;
            foreach (KeyValuePair<string, TypeFieldInfo> kvp in this.nameToInfo)
            {
                this.indexToInfo[i++] = kvp.Value;
            }

            // Sort by FieldIndex. Fields without FieldIndex will 
            // be sorted towards the back, because their FieldIndex
            // is Int32.MaxValue.
            //
            // The sort order is important when reading a file that 
            // doesn't have the field names in the first line, and when
            // writing a file. 
            //
            // Note that for reading from a file with field names in the 
            // first line, method ReadNames reworks IndexToInfo.
            Array.Sort(this.indexToInfo);
        }

        public void AddMappingIndex(int i, int currentNameIndex)
        {
            _mappingIndexes.Add(i, currentNameIndex);
        }

        public void UpdateIndexToInfo<T>(IDataRow row, Dictionary<string, TypeFieldInfo> nameToInfo,
            bool enforceCsvColumnAttribute, string fileName)
        {
            for (int i = 0; i < row.Count; i++)
            {
                if (!_mappingIndexes.ContainsKey(i))
                {
                    continue;
                }

                this.indexToInfo[_mappingIndexes[i]] = nameToInfo[row[i].Value];
                if (enforceCsvColumnAttribute && (!this.indexToInfo[i].HasColumnAttribute))
                {
                    // enforcing column attr, but this field/prop has no column attr.
                    throw new MissingCsvColumnAttributeException(typeof(T).ToString(), row[i].Value, fileName);
                }
            }
        }

        public TypeFieldInfo QueryTypeFieldInfo(bool ignoreUnknownColumns, int i)
        {
            //If there is some index mapping generated and the IgnoreUnknownColums is `true`
            if (ignoreUnknownColumns && _mappingIndexes.Count > 0)
            {
                if (!_mappingIndexes.ContainsKey(i))
                {
                    return null;
                }
                return this.indexToInfo[_mappingIndexes[i]];
            }
            else
            {
                return this.indexToInfo[i];
            }
        }

        public List<int> GetCharLengthList()
        {
            return this.indexToInfo.Select(e => e.charLength).ToList();
        }

        public int GetMaxRowCount(int defaultRowCount)
        {
            return _mappingIndexes.Count > 0 ? defaultRowCount : Math.Min(defaultRowCount, this.indexToInfo.Length);
        }
    }
}
