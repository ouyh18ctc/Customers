using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TypeDef;

namespace Customers.Domain.ViewHelper
{
    public class DemandListProvider : ListProviderBase
    {
        public DemandListProvider()
        {
            listItems.Clear();
            listItems.Add("City", CityDefQueries.ListItems);
            listItems.Add("AcceptPath", AcceptPathDefQueries.ListItems);
            listItems.Add("CustomerLevel", CustomerLevelDefQueries.ListItems);
            listItems.Add("DemandLevel", DemandLevelDefQueries.ListItems);
            listItems.Add("DemandSource", DemandSourceDefQueries.ListItems);
            listItems.Add("DemandType", DemandTypeDefQueries.ListItems);
            listItems.Add("MarketingTheme", MarketingThemeDefQueries.ListItems);
            listItems.Add("ProjectState", ProjectStateDefQueries.ListItems);
            listItems.Add("Satisfactory", SatisfactoryDefQueries.ListItems);
        }

        public DemandListProvider(IEnumerable<ListItem> customCity)
            : this()
        {
            if (listItems.ContainsKey("City"))
            {
                listItems["City"] = customCity;
            }
        }
    }
}
