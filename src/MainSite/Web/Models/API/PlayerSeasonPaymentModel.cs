using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models.API
{
    public class PlayerSeasonPaymentModel
    {
        public long PlayerSeasonId { get; set; }
        public float TotalCost { get; set; }

        public IList<Payment> Payments { get; set; }
    }
}