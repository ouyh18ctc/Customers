using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TypeDef;
using Customers.Domain.Excel;

namespace Customers.Domain.TableDef
{
    public class ByTypeDemandStat
    {
        [CsvColumn(Name = "地市", FieldIndex = 1)]
        public string City { get; set; }

        [CsvColumn(Name = "重要区域信号优化", FieldIndex = 2)]
        public int CommunicationTotal { get; set; }

        [CsvColumn(Name = "重要区域信号优化完成", FieldIndex = 3)]
        public int CommunicationFinished { get; set; }

        [CsvColumn(Name = "市场放号", FieldIndex = 4)]
        public int MarketTotal { get; set; }

        [CsvColumn(Name = "市场放号完成", FieldIndex = 5)]
        public int MarketFinished { get; set; }

        [CsvColumn(Name = "政企客户网优", FieldIndex = 6)]
        public int VipTotal { get; set; }

        [CsvColumn(Name = "政企客户网优完成", FieldIndex = 7)]
        public int VipFinished { get; set; }

        [CsvColumn(Name = "VIP客户投诉处理", FieldIndex = 8)]
        public int IntraTotal { get; set; }

        [CsvColumn(Name = "VIP客户投诉处理完成", FieldIndex = 9)]
        public int IntraFinished { get; set; }

        [CsvColumn(Name = "应急支撑", FieldIndex = 10)]
        public int EmergencyTotal { get; set; }

        [CsvColumn(Name = "应急支撑完成", FieldIndex = 11)]
        public int EmergencyFinished { get; set; }

        public static ByTypeDemandStat Parse(IEnumerable<Demand> demandList, string city = null)
        {
            if (city != null)
            { demandList = demandList.Where(x => x.City.GetCityName() == city); }

            return new ByTypeDemandStat
            {
                City = city ?? "合计",
                CommunicationTotal = demandList.Where(x => x.DemandType == DemandTypeDef.Communication).Count(),
                CommunicationFinished = demandList.Where(x => x.DemandType == DemandTypeDef.Communication
                    && x.ProjectState == ProjectStateDef.Complete).Count(),
                MarketTotal = demandList.Where(x => x.DemandType == DemandTypeDef.Market).Count(),
                MarketFinished = demandList.Where(x => x.DemandType == DemandTypeDef.Market
                    && x.ProjectState == ProjectStateDef.Complete).Count(),
                VipTotal = demandList.Where(x => x.DemandType == DemandTypeDef.Vip).Count(),
                VipFinished = demandList.Where(x => x.DemandType == DemandTypeDef.Vip
                    && x.ProjectState == ProjectStateDef.Complete).Count(),
                IntraTotal = demandList.Where(x => x.DemandType == DemandTypeDef.Intra).Count(),
                IntraFinished = demandList.Where(x => x.DemandType == DemandTypeDef.Intra
                    && x.ProjectState == ProjectStateDef.Complete).Count(),
                EmergencyTotal = demandList.Where(x => x.DemandType == DemandTypeDef.Emergency).Count(),
                EmergencyFinished = demandList.Where(x => x.DemandType == DemandTypeDef.Emergency
                    && x.ProjectState == ProjectStateDef.Complete).Count()
            };
        }
    }
}
