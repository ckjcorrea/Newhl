using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newhl.MainSite.Common.DomainModel
{
    public class Season
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public IList<Program> Programs { get; set; }

        public IList<PercentageDiscount> GetDiscounts()
        {
            IList<PercentageDiscount> retVal = new List<PercentageDiscount>();

            Program tuesdaySkills = this.Programs.FirstOrDefault(t => t.DayOfWeek == DayOfWeek.Tuesday && t.StartTime.Hour == 9 && t.StartTime.Minute == 30);
            Program wednesdaySkills = this.Programs.FirstOrDefault(t => t.DayOfWeek == DayOfWeek.Wednesday && t.StartTime.Hour == 9 && t.StartTime.Minute == 30);
            Program thursdaySkills = this.Programs.FirstOrDefault(t => t.DayOfWeek == DayOfWeek.Thursday && t.StartTime.Hour == 9 && t.StartTime.Minute == 30);

            IList<Program> tuesdayThursdayPrograms = new List<Program>();
            if (tuesdaySkills != null && thursdaySkills != null)
            {
                tuesdayThursdayPrograms.Add(tuesdaySkills);
                tuesdayThursdayPrograms.Add(thursdaySkills);
                PercentageDiscount tuesdayThursday = new PercentageDiscount(5, tuesdayThursdayPrograms);
                retVal.Add(tuesdayThursday);
            }

            IList<Program> tuesdayWednesdayThursdayPrograms = new List<Program>();
            if (tuesdaySkills != null && wednesdaySkills != null && thursdaySkills != null)
            {
                tuesdayWednesdayThursdayPrograms.Add(tuesdaySkills);
                tuesdayWednesdayThursdayPrograms.Add(wednesdaySkills);
                tuesdayWednesdayThursdayPrograms.Add(thursdaySkills);
                PercentageDiscount tuesdayWednesdayThursday = new PercentageDiscount(5, tuesdayWednesdayThursdayPrograms);
                retVal.Add(tuesdayWednesdayThursday);
            }

            return retVal;
        }
    }
}
