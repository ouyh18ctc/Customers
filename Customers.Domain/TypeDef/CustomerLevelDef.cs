using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum CustomerLevelDef : int
    {
        Undefined,
        One,
        Two,
        Three,
        Four
    }

    public static class CustomerLevelDefQueries
    {
        public static int GetCustomerLevelValue(this CustomerLevelDef customerLevel)
        {
            return (int)customerLevel;
        }
        public static IEnumerable<ListItem> ListItems
        {
            get
            {
                return new List<ListItem>(){
                    new ListItem{Value="0",Text="未定义"},
                    new ListItem{Value="1",Text="1级"},
                    new ListItem{Value="2",Text="2级"},
                    new ListItem{Value="3",Text="3级"},
                    new ListItem{Value="4",Text="4级"}
                };
            }
        }
        public static CustomerLevelDef GetCustomerLevelIndex(this int customerLevelValue)
        {
            return (customerLevelValue > 0 && customerLevelValue < 5) ?
                (CustomerLevelDef)customerLevelValue : CustomerLevelDef.Undefined;
        }
    }
}
