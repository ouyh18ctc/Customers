using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.TableDef
{
    public class FakeProgressRepository : IProgressRepository
    {
        private List<WeekProgress> weekProgress = new List<WeekProgress>();

        public IQueryable<WeekProgress> WeekProgresses 
        {
            get { return weekProgress.AsQueryable(); } 
        }

        public void SaveProgress(int demandId, int weekNum, string progress)
        {
            WeekProgress wp = weekProgress.FirstOrDefault(x => x.DemandId == demandId && x.WeekNum == weekNum);

            if (wp == null)
            {
                weekProgress.Add(new WeekProgress
                {
                    DemandId = demandId,
                    WeekNum = weekNum,
                    Progress = progress
                });
            }
            else
            {
                wp.Progress = progress;
            }
        }
    }
}
