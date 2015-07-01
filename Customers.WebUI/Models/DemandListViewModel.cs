using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customers.Domain.ViewHelper;
using Customers.Domain.TableDef;
using Customers.Domain.TypeDef;

namespace Customers.WebUI.Models
{
    public class DemandListViewModel
    {
        public List<DemandView> Demands { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentCity { get; set; }
    }
}