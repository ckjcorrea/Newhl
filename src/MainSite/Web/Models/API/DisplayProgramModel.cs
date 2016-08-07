using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models.API
{
    public class DisplayProgramModel
    {
        public DisplayProgramModel(Season season, Program program)
        {
            this.Season = season;
            this.Source = program;
        }

        private Season Season { get; set; }
        private Program Source { get; set; }

        public long Id
        {
            get { return this.Source.Id; }
        }

        public string Name
        {
            get { return this.Source.Name; }
        }

        public string DayOfWeek
        {
            get { return this.Source.DayOfWeek.ToString(); }
        }

        public string StartDate
        {
            get { return this.Season.StartDate.AddDays(int.Parse(this.Source.DayOfWeek.ToString("d"))).ToShortDateString(); }
        }

        public string StartTime
        {
            get { return this.Source.StartTime.ToShortTimeString(); }
        }
        public float Price
        {
            get { return this.Source.Price; }
        }

        public bool IsActive
        {
            get { return this.Source.IsActive; }
        }

        public string Location
        {
            get { return this.Source.Location; }
        }
    }
}