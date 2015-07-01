using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum DemandTypeDef : int
    {
        Market,
        Communication,
        Intra,
        Vip,
        Emergency,
        Undefined
    }

    public static class DemandTypeDefQueries
    {
        private static Dictionary<DemandTypeDef, string> list = new Dictionary<DemandTypeDef, string>(){
            { DemandTypeDef.Market, "市场支撑"},
            { DemandTypeDef.Communication, "通信保障"},
            { DemandTypeDef.Intra, "内部投诉"},
            { DemandTypeDef.Vip, "VIP客户投诉处理"},
            { DemandTypeDef.Emergency, "应急支撑"},
            { DemandTypeDef.Undefined, "未定义"}
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
        
        public static string GetDemandTypeDescription(this DemandTypeDef demandType)
        {
            return list[demandType];
        }

        public static DemandTypeDef GetDemandTypeIndex(this string demandTypeDescription)
        {
            return (list.ContainsValue(demandTypeDescription)) ?
                list.FirstOrDefault(x => x.Value == demandTypeDescription).Key :
                DemandTypeDef.Undefined;
        }
    }
}
