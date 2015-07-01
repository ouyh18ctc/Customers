using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.UnitTests.ViewHelper
{
    public class FakeListProvider : ListProviderBase
    {
        public FakeListProvider()
        {
            listItems.Clear();
            listItems.Add("MyProperty", new ListItem[]{
                new ListItem{ Text = "Tip1", Value="P1"},
                new ListItem{ Text = "Tip2", Value="P2"}
            });
        }
    }
}
