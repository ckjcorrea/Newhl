using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newhl.Common.Configuration;
using Newhl.Common.DomainModel;
using Newhl.Common.Security;
using Newhl.Common.Utilities;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.Web.Models;
using Newhl.MainSite.Web.Code.Filters;

namespace Newhl.MainSite.Web.Controllers
{
    public class PaymentController : ControllerBase
    {
        [MVCAuthorization]
        public ActionResult Index()
        {
            if (this.CurrentPrincipal == null || this.CurrentPrincipal.IsAuthenticated == false)
            {
                return this.RedirectToAction("Signin", "User");
            }
            else
            {
                return this.View(this.CurrentPrincipal.User);
            }
        }

        [MVCAuthorization]
        public ActionResult Confirm(decimal paymentAmount, PaymentPortions paymentPortion, PaymentMethods paymentMethod, string checkNumber)
        {
            if (this.CurrentPrincipal == null || this.CurrentPrincipal.IsAuthenticated == false)
            {
                return this.RedirectToAction("Signin", "User");
            }
            else
            {
                ConfirmPaymentModel retVal = new ConfirmPaymentModel();

                retVal.PaymentDetails = this.ServiceManager.UserService.MakePayment(this.CurrentPrincipal.User.Id, paymentMethod, paymentAmount, checkNumber);
                retVal.DesiredPortion = paymentPortion;
                retVal.PlayerInfo = this.CurrentPrincipal.User;

                return this.View(retVal);
            }
        }
    }
}