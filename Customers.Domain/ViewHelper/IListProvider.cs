using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Customers.Domain.ViewHelper
{
    public interface IListProvider
    {
        IEnumerable<ListItem> GetListItems(string listName);
    }
}
