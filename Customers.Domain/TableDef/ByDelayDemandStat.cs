using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TypeDef;
using Customers.Domain.Excel;

namespace Customers.Domain.TableDef
{
    public class ByDelayDemandStat
    {
        [CsvColumn(Name = "地市", FieldIndex = 1)]
        public string City { get; set; }

        [CsvColumn(Name = "A级历时一个月", FieldIndex = 2)]
        public int ALevelOneMonth { get; set; }

        [CsvColumn(Name = "A级历时一季度", FieldIndex = 3)]
        public int ALevelOneQuarter { get; set; }

        [CsvColumn(Name = "A级历时半年", FieldIndex = 4)]
        public int ALevelHalfYear { get; set; }

        [CsvColumn(Name = "A级总数", FieldIndex = 5)]
        public int ALevelTotal { get; set; }

        [CsvColumn(Name = "B级历时一个月", FieldIndex = 6)]
        public int BLevelOneMonth { get; set; }

        [CsvColumn(Name = "B级历时一季度", FieldIndex = 7)]
        public int BLevelOneQuarter { get; set; }

        [CsvColumn(Name = "B级历时半年", FieldIndex = 8)]
        public int BLevelHalfYear { get; set; }

        [CsvColumn(Name = "B级总数", FieldIndex = 9)]
        public int BLevelTotal { get; set; }

        [CsvColumn(Name = "C级历时一个月", FieldIndex = 10)]
        public int CLevelOneMonth { get; set; }

        [CsvColumn(Name = "C级历时一季度", FieldIndex = 11)]
        public int CLevelOneQuarter { get; set; }

        [CsvColumn(Name = "C级历时半年", FieldIndex = 12)]
        public int CLevelHalfYear { get; set; }

        [CsvColumn(Name = "C级总数", FieldIndex = 13)]
        public int CLevelTotal { get; set; }

        public static ByDelayDemandStat Parse(IEnumerable<Demand> demandList, string city = null)
        {
            if (city != null)
            { demandList = demandList.Where(x => x.City.GetCityName() == city); }

            return new ByDelayDemandStat
            {
                City = city ?? "合计",
                ALevelTotal = demandList.Where(x => x.DemandLevel == DemandLevelDef.A).Count(),
                ALevelHalfYear = demandList.Where(x => x.DemandLevel == DemandLevelDef.A
                    && x.Delay >= new TimeSpan(183, 0, 0, 0, 0)).Count(),
                ALevelOneQuarter = demandList.Where(x => x.DemandLevel == DemandLevelDef.A
                    && x.Delay < new TimeSpan(183, 0, 0, 0, 0) && x.Delay >= new TimeSpan(91, 0, 0, 0, 0)).Count(),
                ALevelOneMonth = demandList.Where(x => x.DemandLevel == DemandLevelDef.A
                    && x.Delay < new TimeSpan(91, 0, 0, 0, 0) && x.Delay >= new TimeSpan(30, 0, 0, 0, 0)).Count(),
                BLevelTotal = demandList.Where(x => x.DemandLevel == DemandLevelDef.B).Count(),
                BLevelHalfYear = demandList.Where(x => x.DemandLevel == DemandLevelDef.B
                    && x.Delay >= new TimeSpan(183, 0, 0, 0, 0)).Count(),
                BLevelOneQuarter = demandList.Where(x => x.DemandLevel == DemandLevelDef.B
                    && x.Delay < new TimeSpan(183, 0, 0, 0, 0) && x.Delay >= new TimeSpan(91, 0, 0, 0, 0)).Count(),
                BLevelOneMonth = demandList.Where(x => x.DemandLevel == DemandLevelDef.B
                    && x.Delay < new TimeSpan(91, 0, 0, 0, 0) && x.Delay >= new TimeSpan(30, 0, 0, 0, 0)).Count(),
                CLevelTotal = demandList.Where(x => x.DemandLevel == DemandLevelDef.C).Count(),
                CLevelHalfYear = demandList.Where(x => x.DemandLevel == DemandLevelDef.C
                    && x.Delay >= new TimeSpan(183, 0, 0, 0, 0)).Count(),
                CLevelOneQuarter = demandList.Where(x => x.DemandLevel == DemandLevelDef.C
                    && x.Delay < new TimeSpan(183, 0, 0, 0, 0) && x.Delay >= new TimeSpan(91, 0, 0, 0, 0)).Count(),
                CLevelOneMonth = demandList.Where(x => x.DemandLevel == DemandLevelDef.C
                    && x.Delay < new TimeSpan(91, 0, 0, 0, 0) && x.Delay >= new TimeSpan(30, 0, 0, 0, 0)).Count()
            };
        }
    }
}
