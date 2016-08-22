using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newhl.Common.DomainModel;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.Web.Code.Filters;
using Newhl.MainSite.Web.Models;
using Newhl.MainSite.Web.Models.API;
namespace Newhl.MainSite.Web.Controllers.API
{
    public class PaymentController : BaseAPIController
    {
        [Route("api/Payment"), HttpGet()]
        [WebApiAuthorization]
        public IList<Payment> Get()
        {
            IList<Payment> retVal = null;

            return retVal;
        }

        [Route("api/Season/{id}/Registered/Payment"), HttpGet()]
        [WebApiAuthorization]
        public PlayerSeasonPaymentModel Get(long id)
        {
            PlayerSeasonPaymentModel retVal = new PlayerSeasonPaymentModel();

            PlayerSeason playerSeason = this.Services.SeasonService.GetPlayerSeasonById(id);

            if (playerSeason != null)
            {
                retVal.PlayerSeasonId = playerSeason.Id;
                retVal.TotalCost = playerSeason.CalculateAmountDue();
                retVal.Payments = new List<DisplaySeasonPayment>();

                if(playerSeason.Payments != null)
                {
                    foreach(Payment seasonPayment in playerSeason.Payments)
                    {
                        retVal.Payments.Add(new DisplaySeasonPayment(seasonPayment));
                    }
                }
            }
            else
            {
                retVal.PlayerSeasonId = 0;
                retVal.TotalCost = 0;
                retVal.Payments = new List<DisplaySeasonPayment>();
            }

            return retVal;
        }

    }
}
