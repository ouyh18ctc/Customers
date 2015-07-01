using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum DemandLevelDef : int
    {
        A,
        B,
        C,
        Undefined
    }

    public static class DemandLevelDefQueries
    {
        private static Dictionary<DemandLevelDef, string> list = new Dictionary<DemandLevelDef, string>(){
            { DemandLevelDef.A, "A级"},
            { DemandLevelDef.B, "B级"},
            { DemandLevelDef.C, "C级"},
            { DemandLevelDef.Undefined, "未定义"}
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
        
        public static string GetDemandLevelDescription(this DemandLevelDef demandLevel)
        {
            return list[demandLevel];
        }

        public static DemandLevelDef GetDemandLevelIndex(this string demandLevelDescription)
        {
            return (list.ContainsValue(demandLevelDescription)) ?
                list.FirstOrDefault(x => x.Value == demandLevelDescription).Key :
                DemandLevelDef.Undefined;
        }
    }
}
