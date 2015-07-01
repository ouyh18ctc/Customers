using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.TableDef
{
    public interface IDemandRepository
    {
        IQueryable<Demand> Demands { get; }

        int SaveDemand(DemandView demand);

        Demand DeleteDemand(int demandId);

        bool FinishDemand(FinishDemandView demand);
    }
}
