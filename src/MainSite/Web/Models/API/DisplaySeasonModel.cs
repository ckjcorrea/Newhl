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

        public void Initialize(Season source, IList<Program> selectedPrograms)
        {
            this.Id = source.Id;
            this.Name = source.Name;
            this.StartDate = source.StartDate.ToShortDateString();
            this.EndDate = source.EndDate.ToShortDateString();
            this.DateCreated = source.DateCreated.ToShortDateString();
            this.IsActive = source.IsActive;

            this.Programs = new List<DisplayProgramModel>();

            for (int i = 0; i < source.Programs.Count; i++)
            {
                DisplayProgramModel programToAdd = new DisplayProgramModel();

                bool isSelected = false;

                if (selectedPrograms != null)
                {
                    if (selectedPrograms.FirstOrDefault(p => p.Id == source.Programs[i].Id)!=null)
                    {
                        isSelected = true;
                    }
                }

                programToAdd.Initialize(source, source.Programs[i], isSelected);
                Programs.Add(programToAdd);
            }
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        public string DateCreated { get; set; }

        public bool IsActive { get; set; }

        public IList<DisplayProgramModel> Programs { get; set; }
    }
}