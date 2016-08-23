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
            Program fridayGames = this.Programs.FirstOrDefault(t => t.DayOfWeek == DayOfWeek.Friday && t.StartTime.Hour == 9 && t.StartTime.Minute == 30);

            if (tuesdaySkills != null && thursdaySkills != null)
            {
                IList<Program> tuesdayThursdayPrograms = new List<Program>();
                tuesdayThursdayPrograms.Add(tuesdaySkills);
                tuesdayThursdayPrograms.Add(thursdaySkills);
                PercentageDiscount tuesdayThursday = new PercentageDiscount(5, tuesdayThursdayPrograms);
                retVal.Add(tuesdayThursday);
            }

            if (wednesdaySkills != null && thursdaySkills != null)
            {
                IList<Program> wednesdayThursdayPrograms = new List<Program>();
                wednesdayThursdayPrograms.Add(wednesdaySkills);
                wednesdayThursdayPrograms.Add(thursdaySkills);
                PercentageDiscount wednesdayThursday = new PercentageDiscount(5, wednesdayThursdayPrograms);
                retVal.Add(wednesdayThursday);
            }

            if (wednesdaySkills != null && thursdaySkills != null && fridayGames != null)
            {
                IList<Program> wednesdayThursdayFridayPrograms = new List<Program>();
                wednesdayThursdayFridayPrograms.Add(wednesdaySkills);
                wednesdayThursdayFridayPrograms.Add(thursdaySkills);
                wednesdayThursdayFridayPrograms.Add(fridayGames);
                PercentageDiscount wednesdayThursdayFriday = new PercentageDiscount(5, wednesdayThursdayFridayPrograms);
                retVal.Add(wednesdayThursdayFriday);
            }

            if (tuesdaySkills != null && wednesdaySkills != null && thursdaySkills != null && fridayGames != null)
            {
                IList<Program> tuesdayWednesdayThursdayFridayPrograms = new List<Program>();
                tuesdayWednesdayThursdayFridayPrograms.Add(tuesdaySkills);
                tuesdayWednesdayThursdayFridayPrograms.Add(wednesdaySkills);
                tuesdayWednesdayThursdayFridayPrograms.Add(thursdaySkills);
                tuesdayWednesdayThursdayFridayPrograms.Add(fridayGames);
                PercentageDiscount tuesdayWednesdayThursdayFriday = new PercentageDiscount(5, tuesdayWednesdayThursdayFridayPrograms);
                retVal.Add(tuesdayWednesdayThursdayFriday);
            }

            return retVal;
        }
    }
}
