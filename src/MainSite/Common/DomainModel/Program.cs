using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newhl.MainSite.Common.DomainModel
{
    public class Program
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public float Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public string Location { get; set; }
    }
}
