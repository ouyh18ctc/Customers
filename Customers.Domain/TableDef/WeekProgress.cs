using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customers.Domain.TableDef
{
    public class WeekProgress
    {
        public int Id { get; set; }

        public int DemandId { get; set; }

        public int WeekNum { get; set; }

        public string Progress { get; set; }
    }
}
