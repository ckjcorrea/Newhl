using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models
{
    public class ConfirmPaymentModel
    {
        public AMFUserLogin PlayerInfo { get; set; }
        public Payment PaymentDetails { get; set; }
    }
}