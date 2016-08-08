using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newhl.MainSite.Common.DomainModel
{
    public class PlayerSeason
    {
        public long Id { get; set; }

        public long SeasonId { get; set; }

        public long PlayerId { get; set; }

        public IList<Program> Programs { get; set; }

        public IList<Payment> Payments { get; set; }

        public void UpdateSeasonPrograms(IList<Program> programsToAdd)
        {
            if(this.Programs==null)
            {
                this.Programs = new List<Program>();
            }

            this.Programs.Clear();

            for (int i = 0; i < programsToAdd.Count; i++)
            {
                this.Programs.Add(programsToAdd[i]);
            }
        }

        public float CalculateAmountDue()
        {
            float retVal = 0;

            for (int i = 0; i < this.Programs.Count; i++)
            {
                retVal += this.Programs[i].Price;
            }

            return retVal;
        }
    }
}
