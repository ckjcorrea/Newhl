using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models
{
    public class ConfirmRegistrationModel
    {
        public AMFUserLogin PlayerRegistration{ get; set; }

        public decimal LTPPrice { get; set; }
        public decimal TuesdayPrice { get; set; }
        public decimal WednesdayPrice { get; set; }
        public decimal StickhandlingPrice { get; set; }
        public decimal SomervillePrice { get; set; }
        public decimal GamesPrice { get; set; } 
        public decimal TotalPrice { get; set; }
    }
}