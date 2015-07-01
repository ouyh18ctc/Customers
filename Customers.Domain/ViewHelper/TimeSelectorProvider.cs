using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.ViewHelper
{
    public class TimeSelectorProvider : ListProviderBase
    {
        public TimeSelectorProvider()
        {
            listItems.Clear();
            listItems.Add("TimeSpan", new List<ListItem>{
                new ListItem{Text="最近一周",Value="最近一周"},
                new ListItem{Text="最近一月",Value="最近一月"},
                new ListItem{Text="最近一季度",Value="最近一季度"},
                new ListItem{Text="自由选择",Value="自由选择"}
            });
        }
    }
}
