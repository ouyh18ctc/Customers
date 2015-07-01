using System;
using System.Linq;
using System.Collections.Generic;
using Customers.Domain.TableDef;

namespace Customers.WebUI.Models
{
    public interface ITimeSpanSelector
    {
        DateTime BeginTime { get; set; }

        DateTime EndTime { get; set; }

        string TimeSpan { get; set; }
    }

    public static class TimeSpanSelectorQueries
    {
        public static IEnumerable<Demand> FilterByTimeSpan(this ITimeSpanSelector span,
            IEnumerable<Demand> sourceDemands)
        {
            switch (span.TimeSpan)
            { 
                case "自由选择":
                    return sourceDemands.Where(x =>
                        x.ReceiveDate >= span.BeginTime && x.ReceiveDate < span.EndTime);
                case "最近一周":
                    return sourceDemands.Where(x =>
                        x.ReceiveDate >= DateTime.Now.AddDays(-7));
                case "最近一月":
                    return sourceDemands.Where(x =>
                        x.ReceiveDate >= DateTime.Now.AddDays(-7));
                case "最近一季度":
                    return sourceDemands.Where(x =>
                        x.ReceiveDate >= DateTime.Now.AddDays(-7));
                default:
                    return sourceDemands;
            }
        }
    }
}
