using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Customers.Domain.Security;
using Customers.Domain.TypeDef;
using Customers.Domain.Excel;

namespace Customers.Domain.TableDef
{
    public class EFDemandRepository : IDemandRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Demand> Demands
        {
            get { return context.Demands; }
        }

        public int SaveDemand(DemandView demand)
        {
            Demand objectDemand = Demands.GetFittedRecord(demand);
            int id;
            if (objectDemand == null)
            {
                id = context.Demands.Add(Demand.Parse(demand)).Id;
            }
            else
            {
                objectDemand.CloneProperties(Demand.Parse(demand));
                id = objectDemand.Id;
            }
            context.SaveChanges();
            return id;
        }

        public Demand DeleteDemand(int demandId)
        {
            Demand demand = Demands.FirstOrDefault(x => x.Id == demandId);
            if (demand != null) { context.Demands.Remove(demand); }
            context.SaveChanges();
            return demand;
        }

        public bool FinishDemand(FinishDemandView demand)
        {
            Demand objectDemand = Demands.FirstOrDefault(x => x.Id == demand.Id);
            if (objectDemand == null) { return false; }
            demand.FinishDemand(objectDemand);
            context.SaveChanges();
            return true;
        }

    }
}
