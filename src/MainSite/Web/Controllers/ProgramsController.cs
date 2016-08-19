using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newhl.Common.Business;
using Newhl.Common.Configuration;
using Newhl.Common.DomainModel;
using Newhl.Common.Security;
using Newhl.Common.Utilities;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.BusinessLayer.Services;
using Newhl.MainSite.DataLayer;
using Newhl.MainSite.Web.Models;
using Newhl.MainSite.Web.Code.Filters;

namespace Newhl.MainSite.Web.Controllers
{
    /// <summary>
    /// This controller allows a user to sign in and authorize an OAuth token
    /// </summary>
    public class ProgramsController : ControllerBase
    {
        [MVCAuthorization]
        public ActionResult Index(long? id)
        {
            if (this.CurrentPrincipal == null || this.CurrentPrincipal.IsAuthenticated == false)
            {
                return this.RedirectToAction("Signin", "User");
            }
            else
            {
                ManageProgramsModel retVal = new ManageProgramsModel();
                retVal.Player = this.CurrentPrincipal.User;
                retVal.ActiveSeasons = this.ServiceManager.SeasonService.GetAll(true);

                if(retVal.ActiveSeasons.Count > 0)
                {
                    retVal.SelectedSeason = retVal.ActiveSeasons[0];

                    if (id.HasValue)
                    {
                        retVal.SelectedSeason = retVal.ActiveSeasons.FirstOrDefault(i => i.Id == id.Value);
                    }
                }

                return this.View(retVal);
            }
        }
        /// <summary>
        /// Returns the LearnToPlay Programs page.
        /// </summary>
        /// <returns>The MVC View for the LearnToPlay page of the NEWHL site</returns>
        public ActionResult LTP()
        {
            return this.View();
        }

        /// <summary>
        /// Returns the Rec Programs page.
        /// </summary>
        /// <returns>The MVC View for the Rec page of the NEWHL site</returns>
        public ActionResult Rec()
        {
            return this.View();
        }

        /// <summary>
        /// Returns the CD page.
        /// </summary>
        /// <returns>The MVC View for the CD page of the NEWHL site</returns>
        public ActionResult Somerville()
        {
            return this.View();
        }
        /// <summary>
        /// Returns the CD page.
        /// </summary>
        /// <returns>The MVC View for the CD page of the NEWHL site</returns>
        public ActionResult CD()
        {
            return this.View();
        }

        /// <summary>
        /// Returns the Games page.
        /// </summary>
        /// <returns>The MVC View for the Players page of the NEWHL site</returns>
        public ActionResult Games()
        {
            return this.View();
        }

//        public ActionResult IgnoreThisForNow(AMFUserLogin newPlayer, String formAction, String paymentPortion, String paymentAmount, String paymentType, String checkNumber)
//        {
//            /**
//             * pass parameters into page, 
            
//             * Eventually, query database to get program pricing  -- Hard coded just to get it up and running
             
//             * * render page with registrations 
//             * * Show program pricing and totals
//             * * Give choice of 50% payment, full payment, or other amount             
//             * * Give choice of payment options: check or PayPal
//             * 
//             * Update player registration
//             * Send email with registration confirmation

//             * include PayPal payment button and check number fields
//             * 
//             * *Save payment info into database
//             * *Send email with payment option, PayPay or Check number, and payment amount entered
//             * */

//            //Decimal PaymentAmount = paymentAmount;
//            //String CheckNumber = checkNumber;

//            DateTime date = DateTime.Now;

//            int x;
//            if (formAction == "complete")
//            {
//                //Do I need to use a different order of operations
//                x = 1;
//            }
//            else
//            {
//                //Form action == null
//                x = 0;
//            }


//            AMFUserLogin confirmedPlayer = this.ServiceManager.UserService.Update(newPlayer.FirstName, newPlayer.LastName, newPlayer.Email, newPlayer.USAHockeyNum, newPlayer.DOB, newPlayer.Address1, newPlayer.Address2, newPlayer.City, newPlayer.State, newPlayer.ZipCode, newPlayer.Phone1, newPlayer.Phone2, newPlayer.Emergency1, newPlayer.Emergency2, newPlayer.YearsExp, newPlayer.Level, newPlayer.Internet, newPlayer.Referral, newPlayer.Tournament, newPlayer.Other, newPlayer.LTP, newPlayer.Tuesday, newPlayer.Wednesday, newPlayer.Stickhandling, newPlayer.Somerville, newPlayer.Games, newPlayer.DateCreated, newPlayer.UserStatus, newPlayer.PasswordHint);

//            ViewBag.FirstName = newPlayer.FirstName;

//            ViewBag.LastName = newPlayer.LastName;

//            ViewBag.Email = newPlayer.Email;

//            ViewBag.USAHockeyNum = newPlayer.USAHockeyNum;

//            ViewBag.DOB = newPlayer.DOB;

//            ViewBag.Address1 = newPlayer.Address1;

//            ViewBag.Address2 = newPlayer.Address2;

//            ViewBag.City = newPlayer.City;

//            ViewBag.State = newPlayer.State;

//            ViewBag.ZipCode = newPlayer.ZipCode;

//            ViewBag.Phone1 = newPlayer.Phone1;

//            ViewBag.Phone2 = newPlayer.Phone2;

//            ViewBag.Emergency1 = newPlayer.Emergency1;

//            ViewBag.Emergency2 = newPlayer.Emergency2;

//            ViewBag.YearsExp = newPlayer.YearsExp;

//            ViewBag.Level = newPlayer.Level;

//            ViewBag.Internet = newPlayer.Internet;

//            ViewBag.Referral = newPlayer.Referral;

//            ViewBag.Tournament = newPlayer.Tournament;

//            ViewBag.Other = newPlayer.Other;

//            ViewBag.LTP = newPlayer.LTP;

//            ViewBag.Tuesday = newPlayer.Tuesday;

//            ViewBag.Wednesday = newPlayer.Wednesday;

//            ViewBag.Stickhandling = newPlayer.Stickhandling;

//            ViewBag.Somerville = newPlayer.Somerville;

//            ViewBag.Games = newPlayer.Games;

//            ViewBag.Payment = paymentAmount;

//            ViewBag.PaymentType = paymentType;

//            ViewBag.PaymentAmount = paymentAmount;


//            //generate email to Player
//            EmailConfiguration emailConfig = new EmailConfiguration();
//            EmailManager emailManager = new EmailManager(EmailConfiguration.GetInstance());
//            String emailBody = "Thank you for submitting your registration for a NEWHL Program. ";


//            if (confirmedPlayer != null)
//            { 
//                emailBody = " Your updated registration information will be displayed below." +
//                "\n" + "\n" + "First Name: " + newPlayer.FirstName + "\n" + "Last Name: " + newPlayer.LastName + "\n" + "Email Address: " + newPlayer.Email + "\n" +
//                "USA Hockey Number: " + newPlayer.USAHockeyNum + "\n" + "DOB: " + newPlayer.DOB + "\n" + "Address: " + newPlayer.Address1 + " " + newPlayer.Address2 + "\n" +
//                "City: " + newPlayer.City + "\n" + "State: " + newPlayer.State + "\n" + "Zip Code: " + newPlayer.ZipCode + "\n" + "Primary Phone: " + newPlayer.Phone1 + "\n" +
//                "Secondary Phone: " + newPlayer.Phone2 + "\n" + "Emergency Contacts: " + newPlayer.Emergency1 + ", " + newPlayer.Emergency2 + "\n" + "Years Experience: " + newPlayer.YearsExp + "\n" +
//                "Experience Level: " + newPlayer.Level + "\n" + "\n" + "Programs requested: " + "\n" + "Learn To Play (Monday): " + newPlayer.LTP + "Rec Skill (Tuesday): " + newPlayer.Tuesday + "\n" +
//                "C/D SKills (Wednesday): " + newPlayer.Wednesday + "\n" + "Stickhandling and Shooting Skills (Thursday): " + newPlayer.Stickhandling + "\n" +
//                "Mixed Level Skills (Somerville, Thursday PM): " + newPlayer.Somerville + "\n" + "Games (Friday): " + newPlayer.Games;

//                emailManager.SendEmail(emailConfig.FromAddress, newPlayer.Email, "Updated NEWHL Registration", emailBody);
//            }
//            else
//            {
//                //send error message to View
//            }


//            Decimal LTP_Price = 1;
//            ViewBag.LTP_Price = LTP_Price.ToString();

//            Decimal Tues_Price = 1;
//            ViewBag.Tuesday_Price = Tues_Price.ToString();

//            Decimal Wed_Price = 1;
//            ViewBag.Wednesday_Price = Wed_Price.ToString();

//            Decimal Stickhandling_Price = 1;
//            ViewBag.Stickhandling_Price = Stickhandling_Price.ToString();

//            Decimal Somerville_Price = 1;
//            ViewBag.Somerville_Price = Somerville_Price.ToString();

//            Decimal Games_Price = 1;
//            ViewBag.Games_Price = Games_Price.ToString();

//            Decimal Total_Price = 0;

//            if (confirmedPlayer.LTP == "yes")
//            {
//                Total_Price = Total_Price + LTP_Price;
//            }
//            if (confirmedPlayer.Tuesday == "yes")
//            {
//                Total_Price = Total_Price + Tues_Price;
//            }
//            if (confirmedPlayer.Wednesday == "yes")
//            {
//                Total_Price = Total_Price + Wed_Price;
//            }
//            if(confirmedPlayer.Stickhandling == "yes")
//            {
//                Total_Price = Total_Price + Stickhandling_Price;
//            }
//            if (confirmedPlayer.Somerville == "yes")
//            {
//                Total_Price = Total_Price + Somerville_Price;
//            }
//            if (confirmedPlayer.Games == "yes")
//            {
//                Total_Price = Total_Price + Games_Price;
//            }

//            ViewBag.Total_Price = Total_Price.ToString();
//            Decimal minimumPayment = Total_Price / 2 ;
//            ViewBag.minimumPayment = minimumPayment.ToString();

//            String strPaymentAmt = paymentAmount;
//            decimal decPaymentAmt = Convert.ToDecimal(strPaymentAmt);

//   /*         ActionResult retval = CallPayPal();
//            if (paymentType = "cc"  && hasPaid == false)
//            {
//                CallPayPal();

//            }
//            //{ then add ViewBag.ShowPayPal, show paypal button with instructions 
//            //  OR
//            //   Call a function the will open a new view for PayPal and send return value that can be used in Code.
//            //return (View(ViewBag));}
//*/
//            //else if formaction = complete then
//                //save payment record, then
//                //this.Redirect("http://" + this.Request.Url.Authority + "/Programs/RegistrationComplete");
//            //else
//            //return View(model);

//            return (View(ViewBag));


//        }

        public ActionResult EnterPayment(AMFUserLogin confirmedPlayer, String formAction, String paymentPortion, String paymentAmount, String paymentType, String checkNumber)
        {
            ViewBag.PaymentAmount = paymentAmount;
            ViewBag.PaymentType = paymentType;
            ViewBag.CheckNumber = checkNumber;

            //need to enter payment info into database

            if(formAction == null)
            {
                return (this.View(ViewBag));
            }
            else
            {
                return (this.Redirect("http://" + this.Request.Url.Authority + "/Programs/RegistrationComplete"));
            }
            
        }
        /// <summary>
        /// Returns the RegisterProgram page.
        /// </summary>
        /// <returns>The MVC View for the Players page of the NEWHL site</returns>
        public ActionResult RegistrationComplete()
        {
            return this.View();
        }

        /// <summary>
        /// Returns the Schedule page.
        /// </summary>
        /// <returns>The MVC View for the Schedule page of the NEWHL site</returns>
        public ActionResult Schedule()
        {
            return this.View();
        }
    }
}
