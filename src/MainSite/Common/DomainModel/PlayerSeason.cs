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

        public bool IsChangeRemovingPrograms(IList<Program> programsToUpdate)
        {
            bool retVal = false;

            if (this.Programs == null)
            {
                this.Programs = new List<Program>();
            }

            // First find all the programs currently saved in the database, but not in this programsToUpdate list.
            for (int i = this.Programs.Count - 1; i > -1; i--)
            {
                Program currentProgram = this.Programs[i];
                Program alreadyRegisteredProgram = programsToUpdate.FirstOrDefault(t => t.Id == currentProgram.Id);

                /// We didn't find it, so see if we can remove it and remove it
                if (alreadyRegisteredProgram == null)
                {
                    retVal = true;
                    break;
                }
            }

            return retVal;
        }

        public bool CanRemovePrograms(Season targetSeason)
        {
            bool retVal = false;

            if(targetSeason.Id == this.SeasonId)
            {
                if (targetSeason.StartDate >= DateTime.Now && (this.Payments == null || this.Payments.Count == 0))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public void UpdateSeasonPrograms(Season targetSeason, IList<Program> programsToUpdate)
        {
            if(this.Programs==null)
            {
                this.Programs = new List<Program>();
            }

            bool isChangeRemovingPrograms = this.IsChangeRemovingPrograms(programsToUpdate);

            if ((isChangeRemovingPrograms == false) ||
                (isChangeRemovingPrograms == true && this.CanRemovePrograms(targetSeason) == true))
            {
                // First find all the programs currently saved in the database, but not in this programsToUpdate list.
                for (int i = this.Programs.Count - 1; i > -1; i--)
                {
                    Program currentProgram = this.Programs[i];
                    Program alreadyRegisteredProgram = programsToUpdate.FirstOrDefault(t => t.Id == currentProgram.Id);

                    /// We didn't find it, so see if we can remove it and remove it
                    if (alreadyRegisteredProgram == null)
                    {
                        this.Programs.RemoveAt(i);
                    }
                }

                // Next go through the programsToUpdate list and add in any of those not found in the programs currently saved
                foreach (Program programToUpdate in programsToUpdate)
                {
                    Program alreadyRegisteredProgram = this.Programs.FirstOrDefault(t => t.Id == programToUpdate.Id);

                    if (alreadyRegisteredProgram == null)
                    {
                        this.Programs.Add(programToUpdate);
                    }
                }
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

        public float CalculateDiscount(IList<PercentageDiscount> discounts)
        {
            float retVal = 0.0f;

            for(int i = 0; i < discounts.Count; i++)
            {
                float currentDiscount = discounts[i].CalculateDiscount(this.CalculateAmountDue(), this.Programs);

                if(currentDiscount > retVal)
                {
                    retVal = currentDiscount;
                }
            }

            return retVal;
        }
    }
}
