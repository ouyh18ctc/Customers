using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.ViewHelper;

namespace Customers.Domain.TypeDef
{
    public enum ProjectStateDef : int
    {
        InProgress,
        Complete
    }

    public static class ProjectStateDefQueries
    {
        private static Dictionary<ProjectStateDef, string> list = new Dictionary<ProjectStateDef, string>(){
            { ProjectStateDef.InProgress, "跟进中" },
            { ProjectStateDef.Complete, "完成" }
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
        
        public static string GetProjectStateDescription(this ProjectStateDef projectState)
        {
            return list[projectState];
        }

        public static ProjectStateDef GetProjectStateIndex(this string projectStateDescription)
        {
            return (list.ContainsValue(projectStateDescription)) ?
                list.FirstOrDefault(x => x.Value == projectStateDescription).Key :
                ProjectStateDef.InProgress;
        }
    }
}
