using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.TableDef
{
    public interface IProgressRepository
    {
        IQueryable<WeekProgress> WeekProgresses { get; }

        void SaveProgress(int demandId, int weekNum, string progress);
    }
}
