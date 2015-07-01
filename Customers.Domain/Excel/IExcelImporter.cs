using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Customers.Domain.Excel
{
    public interface IExcelImporter
    {
        DataTable this[string tableName] { get; }
        void Close();
    }
}
