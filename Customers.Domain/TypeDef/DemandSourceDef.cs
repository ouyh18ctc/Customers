using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum DemandSourceDef : int
    {
        Branch,
        Government,
        Industry,
        Undefined
    }

    public static class DemandSourceDefQueries
    {
        private static Dictionary<DemandSourceDef, string> list = new Dictionary<DemandSourceDef, string>(){
            { DemandSourceDef.Branch, "分公司" },
            { DemandSourceDef.Government, "政企客户" },
            { DemandSourceDef.Industry, "行业应用" },
            { DemandSourceDef.Undefined, "未定义" }
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
        
        public static string GetDemandSourceDescription(this DemandSourceDef demandSource)
        {
            return list[demandSource];
        }

        public static DemandSourceDef GetDemandSourceIndex(this string demandSourceDescription)
        {
            return (list.ContainsValue(demandSourceDescription)) ?
                list.FirstOrDefault(x => x.Value == demandSourceDescription).Key :
                DemandSourceDef.Undefined;
        }
    }
}
