using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.ViewHelper
{
    public class ListProviderBase : IListProvider
    {
        protected static Dictionary<string, IEnumerable<ListItem>> listItems 
            = new Dictionary<string, IEnumerable<ListItem>>();

        public IEnumerable<ListItem> GetListItems(string listName)
        {
            IEnumerable<ListItem> items;
            if (listItems.TryGetValue(listName, out items))
            {
                return items;
            }
            return new ListItem[0];
        }
    }
}
