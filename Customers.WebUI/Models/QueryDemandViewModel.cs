using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customers.Domain.ViewHelper;
using Customers.Domain.TableDef;
using Customers.Domain.TypeDef;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Customers.WebUI.Models
{
    public class QueryDemandViewModel
    {
        [CheckBoxList("City")]
        [DisplayName("地市")]
        public IEnumerable<string> CurrentCities { get; set; }

        [RadioButtonList("Satisfactory")]
        [DisplayName("满意度")]
        public string Satisfactory { get; set; }

        [Required(ErrorMessage = "请输入开始时间")]
        [Display(Name = "开始时间")]
        public DateTime BeginDate { get; set; }

        [Required(ErrorMessage = "请输入结束时间")]
        [Display(Name = "结束时间")]
        [CompareValues("BeginDate", CompareValues.GreaterThan, ErrorMessage = "结束时间必须大于开始时间")]
        public DateTime EndDate { get; set; }
    }
}