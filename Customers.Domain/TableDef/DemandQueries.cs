using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TypeDef;

namespace Customers.Domain.TableDef
{
    public static class DemandQueries
    {
        public static Demand GetFittedRecord(this IEnumerable<Demand> demands,
            Demand demand) {
                return demands.FirstOrDefault(
                    x => x.City == demand.City
                    && x.ProjectName.Trim() == demand.ProjectName.Trim()
                    && x.ReceiveDate > demand.ReceiveDate.AddDays(-1)
                    && x.ReceiveDate < demand.ReceiveDate.AddDays(1));
        }

        public static Demand GetFittedRecord(this IEnumerable<Demand> demands,
            DemandView demandView)
        {
            return demands.FirstOrDefault(
                x => x.City.GetCityName() == demandView.City
                && x.ProjectName.Trim() == demandView.ProjectName.Trim()
                && x.ReceiveDate > demandView.ReceiveDate.AddDays(-1)
                && x.ReceiveDate < demandView.ReceiveDate.AddDays(1));
        }

        public static IEnumerable<ByLevelDemandStat> GetDemandStatByLevel(
            this IEnumerable<Demand> demands)
        {
            IEnumerable<string> cities = demands.Select(x => x.City.GetCityName()).Distinct();
            List<ByLevelDemandStat> stats = new List<ByLevelDemandStat>();
            foreach (string city in cities)
            {
                stats.Add(ByLevelDemandStat.Parse(demands, city));
            }
            stats.Add(ByLevelDemandStat.Parse(demands));
            return stats;
        }

        public static IEnumerable<ByDelayDemandStat> GetDemandStatByDelay(
            this IEnumerable<Demand> demands)
        {
            IEnumerable<string> cities = demands.Select(x => x.City.GetCityName()).Distinct();
            List<ByDelayDemandStat> stats = new List<ByDelayDemandStat>();
            foreach (string city in cities)
            {
                stats.Add(ByDelayDemandStat.Parse(demands, city));
            }
            stats.Add(ByDelayDemandStat.Parse(demands));
            return stats;
        }

        public static IEnumerable<ByTypeDemandStat> GetDemandStatByType(
            this IEnumerable<Demand> demands)
        {
            IEnumerable<string> cities = demands.Select(x => x.City.GetCityName()).Distinct();
            List<ByTypeDemandStat> stats = new List<ByTypeDemandStat>();
            foreach (string city in cities)
            {
                stats.Add(ByTypeDemandStat.Parse(demands, city));
            }
            stats.Add(ByTypeDemandStat.Parse(demands));
            return stats;
        }
    }
}
