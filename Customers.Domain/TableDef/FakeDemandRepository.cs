using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TypeDef;
using Customers.Domain.Excel;

namespace Customers.Domain.TableDef
{
    public class FakeDemandRepository : IDemandRepository
    {
        private static List<Demand> demands = new List<Demand>();

        public IQueryable<Demand> Demands
        { get { return demands.AsQueryable(); } }

        public int SaveDemand(DemandView demand)
        {
            Demand objectDemand = Demands.GetFittedRecord(demand);
            if (objectDemand == null)
            {
                demand.Id = (demands.Count == 0) ? 1 : demands.Max(x => x.Id) + 1;
                demands.Add(Demand.Parse(demand));
            }
            else
            {
                demand.Id = objectDemand.Id;
                objectDemand.CloneProperties(Demand.Parse(demand));
            }
            return demand.Id;
        }

        public Demand DeleteDemand(int demandId)
        {
            Demand demand = demands.FirstOrDefault(x => x.Id == demandId);
            if (demand != null) { demands.Remove(demand); }
            return demand;
        }

        public bool FinishDemand(FinishDemandView demand)
        {
            Demand objectDemand = Demands.FirstOrDefault(x => x.Id == demand.Id);
            if (objectDemand == null) { return false; }
            demand.FinishDemand(objectDemand);
            return true;
        }

        public FakeDemandRepository() { }

        public FakeDemandRepository(List<Demand> demandlist)
        { demands = demandlist; }
    }
}
