using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customers.Domain.TableDef;
using Customers.Domain.ViewHelper;
using System.ComponentModel;

namespace Customers.WebUI.Models
{
    public class StatByLevelViewModel : ITimeSpanSelector
    {
        [DisplayName("开始日期：")]
        public DateTime BeginTime { get; set; }

        [DisplayName("结束日期：")]
        public DateTime EndTime { get; set; }

        [RadioButtonList("TimeSpan")]
        [DisplayName("时间跨度选择")]
        public string TimeSpan { get; set; }

        public IEnumerable<ByLevelDemandStat> Results { get; set; }
    }
}