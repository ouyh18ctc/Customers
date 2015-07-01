using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum MarketingThemeDef : int
    {
        Market,
        Campus,
        Scale,
        Rural,
        Flow,
        Open,
        Festival,
        Others
    }

    public static class MarketingThemeDefQueries
    {
        private static Dictionary<MarketingThemeDef, string> list = new Dictionary<MarketingThemeDef, string>(){
            { MarketingThemeDef.Market, "营销渠道"},
            { MarketingThemeDef.Campus, "飞young校园"},
            { MarketingThemeDef.Scale, "规模发展"},
            { MarketingThemeDef.Rural, "农村市场"},
            { MarketingThemeDef.Flow, "流动市场"},
            { MarketingThemeDef.Open, "开放渠道"},
            { MarketingThemeDef.Festival, "双节市场"},
            { MarketingThemeDef.Others, "其他"}
        };

        public static IEnumerable<ListItem> ListItems
        {
            get
            {
                return list.Select(x => new ListItem()
                {
                    Value = x.Value,
                    Text = x.Value
                });
            }
        }
        
        public static string GetMarketingThemeDescription(this MarketingThemeDef marketingTheme)
        {
            return list[marketingTheme];
        }

        public static MarketingThemeDef GetMarketingThemeIndex(this string marketingThemeDescription)
        {
            return (list.ContainsValue(marketingThemeDescription)) ?
                list.FirstOrDefault(x => x.Value == marketingThemeDescription).Key :
                MarketingThemeDef.Others;
        }
    }
}
