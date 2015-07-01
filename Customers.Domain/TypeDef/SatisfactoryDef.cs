using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum SatisfactoryDef
    {
        Perfect,
        Plain,
        Poor,
        Unknown
    }

    public static class SatisfactoryDefQueries
    {
        private static Dictionary<SatisfactoryDef, string> list = new Dictionary<SatisfactoryDef, string>(){
            { SatisfactoryDef.Perfect, "满意" },
            { SatisfactoryDef.Plain, "一般" },
            { SatisfactoryDef.Poor, "差" },
            { SatisfactoryDef.Unknown, "未知" }
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
        
        public static string GetSatisfactoryDescription(this SatisfactoryDef satisfactory)
        {
            return list[satisfactory];
        }

        public static SatisfactoryDef GetSatisfactoryIndex(this string satisfactoryDescription)
        {
            return (list.ContainsValue(satisfactoryDescription)) ?
                list.FirstOrDefault(x => x.Value == satisfactoryDescription).Key :
                SatisfactoryDef.Unknown;
        }
    }
}
