using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Customers.Domain.Excel
{
    public class ExcelImporter : IExcelImporter
    {
        private string[] sheetNames;

        private DataSet dataSet = new DataSet();

        public DataTable this[string tableName]
        {
            get
            {
                string name = sheetNames.FirstOrDefault(x => x == tableName);
                if (name == null)
                {
                    return null;
                }
                else
                {
                    return this.dataSet.Tables[name];
                }
            }
        }

        private OleDbConnection conn;

        public ExcelImporter(string filePath, string[] sheetNames)
        {
            this.sheetNames = sheetNames;

            conn = GenerateOleConnection(filePath);
            if (conn != null)
            { conn.Open(); }
            else return;

            for (int i = 0; i < sheetNames.Length; i++)
            {
                OleDbDataAdapter odda = new OleDbDataAdapter("select * from [" + sheetNames[i] + "$]", conn);
                try
                {
                    odda.Fill(this.dataSet, sheetNames[i]);
                }
                catch
                { continue; }
            }
            conn.Close();
        }

        private static OleDbConnection GenerateOleConnection(string filePath)
        {
            string connstr2003 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + filePath + ";Extended Properties='Excel 8.0; HDR=YES; IMEX=1'";
            string connstr2007 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                + filePath + ";Extended Properties='Excel 12.0; HDR=YES'";

            string fileExt = Path.GetExtension(filePath).ToString().ToLower();
            OleDbConnection conn;

            if (fileExt == ".xls")
            { conn = new OleDbConnection(connstr2003); }
            else if (fileExt == ".xlsx")
            { conn = new OleDbConnection(connstr2007); }
            else { return null; }
            return conn;
        }

        public void Close()
        {
            if (conn != null) { conn.Close(); }
        }
    }
}
