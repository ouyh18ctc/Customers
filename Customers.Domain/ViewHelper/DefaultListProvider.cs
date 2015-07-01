using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.ViewHelper
{
    public class DefaultListProvider : ListProviderBase
    {
        static DefaultListProvider()
        {
            listItems.Clear();
            var items = new ListItem[]{
            new ListItem{ Text = "男", Value="M"},
            new ListItem{ Text = "女", Value="F"}};
            listItems.Add("Gender", items);

            items = new ListItem[]{            
            new ListItem{ Text = "高中", 	Value="H"}  , 
            new ListItem{ Text = "大学本科", 	Value="B"},
            new ListItem{ Text = "硕士",	Value="M"} ,                
            new ListItem{ Text = "博士",	Value="D"}};
            listItems.Add("Education", items);

            items = new ListItem[]{            
            new ListItem{ Text = "人事部", Value="HR"}  , 
            new ListItem{ Text = "行政部", Value="AD"},
            new ListItem{ Text = "IT部",  Value="IT"}};
            listItems.Add("Department", items);

            items = new ListItem[]{            
            new ListItem{ Text = "C#",      Value="CSharp"}  , 
            new ListItem{ Text = "ASP.NET", Value="AspNet"},
            new ListItem{ Text = "ADO.NET", Value="AdoNet"}};
            listItems.Add("Skill", items);
        }
    }
}
