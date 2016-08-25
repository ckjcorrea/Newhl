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
using Newhl.MainSite.BusinessLayer.Services;
using Newhl.MainSite.Web.Models;
using Newhl.MainSite.Web.Code.Filters;

namespace Newhl.MainSite.Web.Controllers
{
    public class PaymentsController : ControllerBase
    {
        [MVCAuthorization]
        public ActionResult Season(long? seasonId)
        {
            if (this.CurrentPrincipal == null || this.CurrentPrincipal.IsAuthenticated == false)
            {
                return this.RedirectToAction("Signin", "User");
            }
            else
            {
                ManagePaymentsModel retVal = new ManagePaymentsModel();
                retVal.Player = this.CurrentPrincipal.User;
                retVal.PlayerSeasons = this.ServiceManager.SeasonService.GetPlayerSeasons(this.CurrentPrincipal.User.Id);
                retVal.PlayerSeasonDetails = new Dictionary<long, Season>();

                if(seasonId.HasValue)
                {
                    retVal.SelectedSeason = this.ServiceManager.SeasonService.GetPlayerSeasonBySeasonId(this.CurrentPrincipal.User.Id, seasonId.Value);
                }
                else
                {
                    if(retVal.PlayerSeasons != null && retVal.PlayerSeasons.Count > 0)
                    {
                        retVal.SelectedSeason = retVal.PlayerSeasons[0];
                    }
                }

                for(int i = 0; i < retVal.PlayerSeasons.Count; i++)
                {
                    retVal.PlayerSeasonDetails.Add(retVal.PlayerSeasons[i].Id, this.ServiceManager.SeasonService.GetById(retVal.PlayerSeasons[i].SeasonId));
                }

                return this.View(retVal);
            }
        }

        [MVCAuthorization]
        public ActionResult Confirm(decimal paymentAmount, PaymentPortions paymentPortion, PaymentMethods paymentMethod, string checkNumber, long playerSeasonId)
        {
            if (this.CurrentPrincipal == null || this.CurrentPrincipal.IsAuthenticated == false)
            {
                return this.RedirectToAction("Signin", "User");
            }
            else
            {
                ConfirmPaymentModel retVal = new ConfirmPaymentModel();

                retVal.PlayerSeason = this.ServiceManager.SeasonService.GetPlayerSeasonById(playerSeasonId);

                if(retVal.PlayerSeason.PlayerId == this.CurrentPrincipal.User.Id)
                {
                    retVal.PaymentDetails = this.ServiceManager.PaymentService.AddSeasonPayment(this.CurrentPrincipal.User.Id, paymentMethod, paymentAmount, checkNumber, playerSeasonId);
                    retVal.PlayerInfo = this.CurrentPrincipal.User;
                }

                return this.View(retVal);
            }
        }

        [MVCAuthorization]
        public ActionResult CancelPromise(long paymentId, long playerSeasonId)
        {
            if (this.CurrentPrincipal == null || this.CurrentPrincipal.IsAuthenticated == false)
            {
                return this.RedirectToAction("Signin", "User");
            }
            else
            {
                if(this.ServiceManager.PaymentService.CancelPromise(this.CurrentPrincipal.User.Id, playerSeasonId, paymentId))
                {
                    return this.RedirectToAction("Season");
                }
                else
                {
                    ConfirmPaymentModel retVal = new ConfirmPaymentModel();

                    PlayerSeason playerSeason = this.ServiceManager.SeasonService.GetPlayerSeasonById(playerSeasonId);
                    retVal.PaymentDetails = playerSeason.Payments.FirstOrDefault(p => p.Id == paymentId);
                    retVal.PlayerInfo = this.CurrentPrincipal.User;
                    return this.View("Confirm", retVal);
                }
            }
        }

        [MVCAuthorization]
        public ActionResult ConfirmPromise(long paymentId, long playerSeasonId)
        {
            if (this.CurrentPrincipal == null || this.CurrentPrincipal.IsAuthenticated == false)
            {
                return this.RedirectToAction("Signin", "User");
            }
            else
            {
                Payment targetPayment = this.ServiceManager.PaymentService.ConfirmPromise(this.CurrentPrincipal.User.Id, playerSeasonId, paymentId);

                if(targetPayment != null)
                {
                    //ADD CODE HERE FOR SENDING EMAIL TO PLAYER AND KERRI ABOUT PAYMENT
                    EmailService sendPlayerEmail = new EmailService();
                    EmailConfiguration emailConfig = new EmailConfiguration();              
                    AMFUserLogin playerDemoInfo = this.ServiceManager.UserService.GetByEmail(this.CurrentPrincipal.User.Email);
                    PlayerSeason playerSeasonInfo = this.ServiceManager.SeasonService.GetPlayerSeasonById(playerSeasonId);
                                           
                    sendPlayerEmail.SendThankYouForPaymentEmail(this.CurrentPrincipal.User.Email, emailConfig, playerDemoInfo, playerSeasonInfo);
                    sendPlayerEmail.SendThankYouForPaymentEmail("NEWHLeague@gmail.com", emailConfig, playerDemoInfo, playerSeasonInfo);

                    return this.RedirectToAction("Season");

                }
                else
                {
                    ConfirmPaymentModel retVal = new ConfirmPaymentModel();

                    PlayerSeason playerSeason = this.ServiceManager.SeasonService.GetPlayerSeasonById(playerSeasonId);
                    retVal.PaymentDetails = playerSeason.Payments.FirstOrDefault(p => p.Id == paymentId);
                    retVal.PlayerInfo = this.CurrentPrincipal.User;
                    return this.View("Confirm", retVal);
                }
            }
        }
    }
}