using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models.API
{
    public class DisplaySeasonModel
    {
        public DisplaySeasonModel(Season source)
        {
            this.Source = source;
        }

        private Season source;
        private IList<DisplayProgramModel> programs;

        private Season Source
        {
            get { return this.source; }
            set
            {
                this.source = value;
                this.programs = new List<DisplayProgramModel>();

                for(int i = 0; i < this.source.Programs.Count; i++)
                {
                    this.programs.Add(new DisplayProgramModel(this.source, this.source.Programs[i]));
                }
            }
        }

        public long Id
        {
            get { return this.Source.Id; }
        }

        public string Name
        {
            get { return this.Source.Name; }
        }

        public string StartDate
        {
            get { return this.Source.StartDate.ToShortDateString(); }
        }

        public string EndDate
        {
            get { return this.Source.EndDate.ToShortDateString(); }
        }

        public string DateCreated
        {
            get { return this.Source.DateCreated.ToShortDateString(); }
        }

        public bool IsActive
        {
            get { return this.Source.IsActive; }
        }

        public IList<DisplayProgramModel> Programs
        {
            get { return this.programs; }
        }
    }
}