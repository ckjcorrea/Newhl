using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models.API
{
    public class DisplaySeasonModel
    {
        public DisplaySeasonModel() { }

        public void Initialize(Season sourceSeason,  PlayerSeason playerSeason)
        {
            this.Id = sourceSeason.Id;
            this.Name = sourceSeason.Name;
            this.StartDate = sourceSeason.StartDate.ToShortDateString();
            this.EndDate = sourceSeason.EndDate.ToShortDateString();
            this.DateCreated = sourceSeason.DateCreated.ToShortDateString();
            this.IsActive = sourceSeason.IsActive;

            this.Programs = new List<DisplayProgramModel>();

            for (int i = 0; i < sourceSeason.Programs.Count; i++)
            {
                DisplayProgramModel programToAdd = new DisplayProgramModel();

                bool isSelected = false;

                if (playerSeason != null && playerSeason.Programs != null)
                {
                    if (playerSeason.Programs.FirstOrDefault(p => p.Id == sourceSeason.Programs[i].Id)!=null)
                    {
                        isSelected = true;
                    }
                }

                programToAdd.Initialize(sourceSeason, sourceSeason.Programs[i], isSelected);
                Programs.Add(programToAdd);

                if(playerSeason != null)
                {
                    this.DiscountAmount = playerSeason.CalculateDiscount(sourceSeason.GetDiscounts());
                }
            }
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        public string DateCreated { get; set; }

        public bool IsActive { get; set; }

        public float DiscountAmount { get; set; }
        public IList<DisplayProgramModel> Programs { get; set; }
    }
}