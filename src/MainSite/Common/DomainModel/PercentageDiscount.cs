using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newhl.MainSite.Common.DomainModel
{
    public class PercentageDiscount
    {
        public PercentageDiscount(float percentage, IList<Program> requiredPrograms)
        {
            this.Percentage = percentage / 100;
            this.RequiredPrograms = requiredPrograms;
        }

        public float Percentage { get; private set; }

        public IList<Program> RequiredPrograms { get; private set; }

        public float CalculateDiscount(float originalAmount, IList<Program> registeredPrograms)
        {
            float retVal = 0.0f;
            int foundMatches = 0;

            for(int i = 0; i < this.RequiredPrograms.Count; i++)
            {
                if(registeredPrograms.FirstOrDefault(t => t.Id == this.RequiredPrograms[i].Id)!=null)
                {
                    foundMatches++;
                }
            }

            if (foundMatches == this.RequiredPrograms.Count)
            {
                retVal = originalAmount * this.Percentage;
            }

            return retVal;
        }
    }
}
