using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customers.Domain.TypeDef;
using Customers.Domain.Excel;

namespace Customers.Domain.TableDef
{
    public class ByLevelDemandStat
    {
        [CsvColumn(Name = "地市", FieldIndex = 1)]
        public string City { get; set; }

        [CsvColumn(Name = "A级总数", FieldIndex = 2)]
        public int ALevelTotal { get; set; }

        [CsvColumn(Name = "A级已完成", FieldIndex = 3)]
        public int ALevelFinished { get; set; }

        [CsvColumn(Name = "B级总数", FieldIndex = 4)]
        public int BLevelTotal { get; set; }

        [CsvColumn(Name = "B级已完成", FieldIndex = 5)]
        public int BLevelFinished { get; set; }

        [CsvColumn(Name = "C级总数", FieldIndex = 6)]
        public int CLevelTotal { get; set; }

        [CsvColumn(Name = "C级已完成", FieldIndex = 7)]
        public int CLevelFinished { get; set; }

        public static ByLevelDemandStat Parse(IEnumerable<Demand> demandList, string city = null)
        {
            if (city != null)
            { demandList = demandList.Where(x => x.City.GetCityName() == city); }

            return new ByLevelDemandStat
            {
                City = city ?? "合计",
                ALevelFinished = demandList.Where(x => x.DemandLevel == DemandLevelDef.A
                    && x.ProjectState == ProjectStateDef.Complete).Count(),
                ALevelTotal = demandList.Where(x => x.DemandLevel == DemandLevelDef.A).Count(),
                BLevelFinished = demandList.Where(x => x.DemandLevel == DemandLevelDef.B
                    && x.ProjectState == ProjectStateDef.Complete).Count(),
                BLevelTotal = demandList.Where(x => x.DemandLevel == DemandLevelDef.B).Count(),
                CLevelFinished = demandList.Where(x => x.DemandLevel == DemandLevelDef.C
                    && x.ProjectState == ProjectStateDef.Complete).Count(),
                CLevelTotal = demandList.Where(x => x.DemandLevel == DemandLevelDef.C).Count()
            };
        }
    }
}