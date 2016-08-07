using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models.API
{
    public class DisplayProgramModel
    {
        public DisplayProgramModel() { }

        public void Initialize(Season season, Program program, bool isSelected)
        {
            this.Id = program.Id;
            this.Name = program.Name;
            this.DayOfWeek = program.DayOfWeek.ToString();
            this.StartDate = season.StartDate.AddDays(int.Parse(program.DayOfWeek.ToString("d"))).ToShortDateString();
            this.StartTime = program.StartTime.ToShortTimeString();
            this.Price = program.Price;
            this.IsActive = program.IsActive;
            this.Location = program.Location;
            this.IsSelected = isSelected;
        }
        public long Id { get; set; }

        public string Name { get; set; }

        public string DayOfWeek { get; set; }

        public string StartDate { get; set; }

        public string StartTime { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }

        public string Location { get; set; }

        public bool IsSelected { get; set; }
    }
}