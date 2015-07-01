using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum AcceptPathDef : int
    {
        Telephone,
        Email,
        Direct,
        System,
        Others
    }

    public static class AcceptPathDefQueries
    {
        private static Dictionary<AcceptPathDef, string> list = new Dictionary<AcceptPathDef, string>(){
            { AcceptPathDef.Telephone, "电话" },
            { AcceptPathDef.Email, "邮件" },
            { AcceptPathDef.System, "系统" },
            { AcceptPathDef.Direct, "现场沟通" },
            { AcceptPathDef.Others, "其他" }
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
        
        public static string GetAcceptPathDescription(this AcceptPathDef acceptPath)
        {
            return list[acceptPath];
        }

        public static AcceptPathDef GetAcceptPathIndex(this string acceptPathDescription)
        {
            return (list.ContainsValue(acceptPathDescription)) ?
                list.FirstOrDefault(x => x.Value == acceptPathDescription).Key :
                AcceptPathDef.Others;
        }
    }
}
