using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models
{
    public class ManagePaymentsModel
    {
        public AMFUserLogin Player { get; set; }

        public IList<Payment> Payments { get; set; }
    }
}