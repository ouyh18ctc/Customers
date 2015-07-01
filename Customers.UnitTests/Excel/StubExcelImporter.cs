using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.Excel;
using System.Data;

namespace Customers.UnitTests.Excel
{
    public class StubExcelImporter : IExcelImporter
    {
        private DataTable[] dataTables;

        public DataTable this[string tableName]
        {
            get { return this.dataTables.FirstOrDefault(x => x.TableName == tableName); }
        }

        public StubExcelImporter(string[] tableNames)
        {
            dataTables = new DataTable[tableNames.Length];
            for (int i = 0; i < tableNames.Length; i++)
            {
                dataTables[i] = new DataTable(tableNames[i]);
            }
        }

        public void Close()
        { }
    }
}
