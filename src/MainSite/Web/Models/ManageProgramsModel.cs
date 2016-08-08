using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models
{
    public class ManageProgramsModel
    {
        public AMFUserLogin Player { get; set; }

        public Season SelectedSeason { get; set; }
        public IList<Season> ActiveSeasons { get; set; }
    }
}