using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using Customers.Domain.ViewHelper;

namespace Customers.WebUI.Models
{
    public class ModifyPermissionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UserName { get; set; }

        [CheckBoxList("City")]
        [DisplayName("允许访问的城市")]
        public IEnumerable<string> Cities { get; set; }
    }
}