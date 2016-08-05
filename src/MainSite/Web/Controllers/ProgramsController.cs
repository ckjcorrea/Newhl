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

        public ActionResult PlayerRegistration(AMFUserLogin playerRegistration)
        {
            if (playerRegistration == null)
            {
                playerRegistration = new AMFUserLogin();
            }

            if (TempData["PlayerRegistrationDetails"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["PlayerRegistrationDetails"];
            }

            return this.View(playerRegistration);
        }

        public void ValidatePlayerRegistration(AMFUserLogin playerInfo)
        {
            //Confirm Email            
            if (playerInfo.Email == null || playerInfo.Email == "")
            {
                ViewData.ModelState.AddModelError("Email", "User email required.");
            }
            else
            {
                if (!Newhl.Common.Business.EmailManager.IsValidEmail(playerInfo.Email))
                {
                    ViewData.ModelState.AddModelError("Email", "A valid email format is required");
                }

            }

            //FirstName is not Null
            if (playerInfo.FirstName == null || playerInfo.FirstName == "")
            {
                ViewData.ModelState.AddModelError("FirstName", "First Name is required");
            }
            //LastName is not null
            if (playerInfo.LastName == null || playerInfo.LastName == "")
            {
                ViewData.ModelState.AddModelError("FirstName", "Last Name is required");
            }
            //Confirm USA Hockey Registration is entered
            if (playerInfo.USAHockeyNum == null || playerInfo.USAHockeyNum == "")
            {
                ViewData.ModelState.AddModelError("USAHockeyNum", "USA Hockey Registration Number is required");
            }
            //DOB is not null
            if (playerInfo.DOB == null)
            {
                ViewData.ModelState.AddModelError("DOB", "Date of Birth is required");
            }
            //Address
            if (playerInfo.Address1 == null || playerInfo.Address1 == "")
            {
                ViewData.ModelState.AddModelError("Address1", "Address is required");
            }
            //City
            if (playerInfo.City == null || playerInfo.City == "")
            {
                ViewData.ModelState.AddModelError("City", "City is required");
            }
            //State
            if (playerInfo.State == null || playerInfo.State == "")
            {
                ViewData.ModelState.AddModelError("State", "State is required");
            }
            //Zip
            if (playerInfo.ZipCode == null || playerInfo.ZipCode == "")
            {
                ViewData.ModelState.AddModelError("ZipCode", "Zip Code is required");
            }
            //Phone1
            if (playerInfo.Phone1 == null || playerInfo.Phone1 == "")
            {
                ViewData.ModelState.AddModelError("Phone1", "Primary phone number is required");
            }
            //Emergency1
            if (playerInfo.Emergency1 == null || playerInfo.Emergency1 == "")
            {
                ViewData.ModelState.AddModelError("Emergency1", "At least one emergency contact is required");
            }
            //Emergency2
            //Emergency1
            if (playerInfo.Emergency2 == null || playerInfo.Emergency2 == "")
            {
                ViewData.ModelState.AddModelError("Emergency2", "A second emergency contact is required");
            }
            if (playerInfo.Level == null || playerInfo.Level == "")
            {
                ViewData.ModelState.AddModelError("Level", "You must select an experience level");
            }
        }

        /// <summary>
        /// Returns the RegisterPlayer page.
        /// </summary>
        /// <returns>The MVC View for the Players page of the NEWHL site</returns>
        public ActionResult ConfirmRegistration(AMFUserLogin playerInfo, String confirm, bool update = false)
        {
            /**
             * Look at CQIC Controller - newCQICRegistration
             * 
             * Look at Visitor Controller - a way to confirm registration via email
             * */
            ConfirmRegistrationModel retVal = new ConfirmRegistrationModel();
            retVal.PlayerRegistration = playerInfo;

            bool isNewPlayer = true;
            DateTime date = DateTime.Now;
            
            ///Make sure this is unique registration by checking for unique email address
            AMFUserLogin existingPlayer = new AMFUserLogin();

            if (playerInfo.Email != null)
            {
                existingPlayer = this.ServiceManager.UserService.GetByEmail(playerInfo.Email);
            }

            if (existingPlayer != null)
            {
                isNewPlayer = false;

                if (update == false)
                {
                    ViewBag.playerExists = "That email currently exists for a NEWHL player. Please check the spelling and try again or login to continue if this is your correct email address.";
                    ViewData.ModelState.AddModelError("exists", "That email address currently exists for a NEWHL player. Please check the spelling and try again or login to continue if this is your correct email address.");
                    //model.CurrentUser = newUser;
                }
            }

            this.ValidatePlayerRegistration(playerInfo);

            if (!playerInfo.Email.Equals(confirm))
            {
                ViewData.ModelState.AddModelError("confirm", "Email addresses could not be confirmed. Both Email address fields must match");
            }

            if (ViewData.ModelState.IsValid)
            {
                DateTime time = DateTime.Now;

                //generate email to Player
                EmailConfiguration emailConfig = new EmailConfiguration();
                // this.ServiceManager.EmailService.SendThankYouForRegisteringEmail(Email, emailConfig);

                //save player registration, and program selected.
                if (isNewPlayer) //only want to save to users table if it is a new user to prevent duplicate records
                {
                    AMFUserLogin newPlayer = this.ServiceManager.UserService.Register(playerInfo.FirstName, playerInfo.LastName, playerInfo.Email, playerInfo.USAHockeyNum, playerInfo.DOB, playerInfo.Address1, playerInfo.Address2, playerInfo.City, playerInfo.State, playerInfo.ZipCode, playerInfo.Phone1, playerInfo.Phone2, playerInfo.Emergency1, playerInfo.Emergency2, playerInfo.YearsExp, playerInfo.Level, playerInfo.Internet, playerInfo.Referral, playerInfo.Tournament, playerInfo.Other, playerInfo.LTP, playerInfo.Tuesday, playerInfo.Wednesday, playerInfo.Stickhandling, playerInfo.Somerville, playerInfo.Games, time, UserStatus.Active, playerInfo.PasswordHint);
                    //hard coded (userID=0, isSiteAdmin=false, isActive=true, isFirstLogin=true ... Guests=0)

                    this.ServiceManager.EmailService.SendPlayerEmailConfiguration(newPlayer.Email, emailConfig, newPlayer);
                    this.ServiceManager.EmailService.SendAdminNotificationEmail("", emailConfig, newPlayer);

                    retVal.PlayerRegistration = newPlayer;
                }
                else
                {
                    existingPlayer = this.ServiceManager.UserService.Update(playerInfo.FirstName, playerInfo.LastName, playerInfo.Email, playerInfo.USAHockeyNum, playerInfo.DOB, playerInfo.Address1, playerInfo.Address2, playerInfo.City, playerInfo.State, playerInfo.ZipCode, playerInfo.Phone1, playerInfo.Phone2, playerInfo.Emergency1, playerInfo.Emergency2, playerInfo.YearsExp, playerInfo.Level, playerInfo.Internet, playerInfo.Referral, playerInfo.Tournament, playerInfo.Other, playerInfo.LTP, playerInfo.Tuesday, playerInfo.Wednesday, playerInfo.Stickhandling, playerInfo.Somerville, playerInfo.Games, time, UserStatus.Active, playerInfo.PasswordHint);

                    this.ServiceManager.EmailService.SendPlayerUpdateEmailConfiguration(existingPlayer.Email, emailConfig, existingPlayer);
                    this.ServiceManager.EmailService.SendAdminNotificationEmail("", emailConfig, existingPlayer);

                    retVal.PlayerRegistration = existingPlayer;
                }

                /*Make sure no error in register or update then send email, then set key*/
                //Set key if successful.  Contact.aspx will show message that request was sent.
                ViewData["emailSent"] = true;
                ViewBag.emailSent = true;

                // add in prices
                retVal.LTPPrice = 1;
                retVal.TuesdayPrice = 1;
                retVal.WednesdayPrice = 1;
                retVal.StickhandlingPrice = 1;
                retVal.SomervillePrice = 1;
                retVal.GamesPrice = 1;
                
                if(retVal.PlayerRegistration.LTP=="yes")
                {
                    retVal.TotalPrice += retVal.LTPPrice;
                }

                if(retVal.PlayerRegistration.Tuesday == "yes")
                {
                    retVal.TotalPrice += retVal.TuesdayPrice;
                }

                if (retVal.PlayerRegistration.Wednesday == "yes")
                {
                    retVal.TotalPrice += retVal.WednesdayPrice;
                }
                
                if (retVal.PlayerRegistration.Stickhandling == "yes")
                {
                    retVal.TotalPrice += retVal.StickhandlingPrice;
                }

                if (retVal.PlayerRegistration.Games == "yes")
                {
                    retVal.TotalPrice += retVal.GamesPrice;
                }

                return this.View(retVal);
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    TempData["PlayerRegistrationDetails"] = ViewData;
                }

                return this.RedirectToAction("PlayerRegistration", retVal.PlayerRegistration);
            }
        }

        public ActionResult IgnoreThisForNow(AMFUserLogin newPlayer, String formAction, String paymentPortion, String paymentAmount, String paymentType, String checkNumber)
        {
            /**
             * pass parameters into page, 
            
             * Eventually, query database to get program pricing  -- Hard coded just to get it up and running
             
             * * render page with registrations 
             * * Show program pricing and totals
             * * Give choice of 50% payment, full payment, or other amount             
             * * Give choice of payment options: check or PayPal
             * 
             * Update player registration
             * Send email with registration confirmation

             * include PayPal payment button and check number fields
             * 
             * *Save payment info into database
             * *Send email with payment option, PayPay or Check number, and payment amount entered
             * */

            //Decimal PaymentAmount = paymentAmount;
            //String CheckNumber = checkNumber;

            DateTime date = DateTime.Now;

            int x;
            if (formAction == "complete")
            {
                //Do I need to use a different order of operations
                x = 1;
            }
            else
            {
                //Form action == null
                x = 0;
            }


            AMFUserLogin confirmedPlayer = this.ServiceManager.UserService.Update(newPlayer.FirstName, newPlayer.LastName, newPlayer.Email, newPlayer.USAHockeyNum, newPlayer.DOB, newPlayer.Address1, newPlayer.Address2, newPlayer.City, newPlayer.State, newPlayer.ZipCode, newPlayer.Phone1, newPlayer.Phone2, newPlayer.Emergency1, newPlayer.Emergency2, newPlayer.YearsExp, newPlayer.Level, newPlayer.Internet, newPlayer.Referral, newPlayer.Tournament, newPlayer.Other, newPlayer.LTP, newPlayer.Tuesday, newPlayer.Wednesday, newPlayer.Stickhandling, newPlayer.Somerville, newPlayer.Games, newPlayer.DateCreated, newPlayer.UserStatus, newPlayer.PasswordHint);

            ViewBag.FirstName = newPlayer.FirstName;

            ViewBag.LastName = newPlayer.LastName;

            ViewBag.Email = newPlayer.Email;

            ViewBag.USAHockeyNum = newPlayer.USAHockeyNum;

            ViewBag.DOB = newPlayer.DOB;

            ViewBag.Address1 = newPlayer.Address1;

            ViewBag.Address2 = newPlayer.Address2;

            ViewBag.City = newPlayer.City;

            ViewBag.State = newPlayer.State;

            ViewBag.ZipCode = newPlayer.ZipCode;

            ViewBag.Phone1 = newPlayer.Phone1;

            ViewBag.Phone2 = newPlayer.Phone2;

            ViewBag.Emergency1 = newPlayer.Emergency1;

            ViewBag.Emergency2 = newPlayer.Emergency2;

            ViewBag.YearsExp = newPlayer.YearsExp;

            ViewBag.Level = newPlayer.Level;

            ViewBag.Internet = newPlayer.Internet;

            ViewBag.Referral = newPlayer.Referral;

            ViewBag.Tournament = newPlayer.Tournament;

            ViewBag.Other = newPlayer.Other;

            ViewBag.LTP = newPlayer.LTP;

            ViewBag.Tuesday = newPlayer.Tuesday;

            ViewBag.Wednesday = newPlayer.Wednesday;

            ViewBag.Stickhandling = newPlayer.Stickhandling;

            ViewBag.Somerville = newPlayer.Somerville;

            ViewBag.Games = newPlayer.Games;

            ViewBag.Payment = paymentAmount;

            ViewBag.PaymentType = paymentType;

            ViewBag.PaymentAmount = paymentAmount;


            //generate email to Player
            EmailConfiguration emailConfig = new EmailConfiguration();
            EmailManager emailManager = new EmailManager(EmailConfiguration.GetInstance());
            String emailBody = "Thank you for submitting your registration for a NEWHL Program. ";


            if (confirmedPlayer != null)
            { 
                emailBody = " Your updated registration information will be displayed below." +
                "\n" + "\n" + "First Name: " + newPlayer.FirstName + "\n" + "Last Name: " + newPlayer.LastName + "\n" + "Email Address: " + newPlayer.Email + "\n" +
                "USA Hockey Number: " + newPlayer.USAHockeyNum + "\n" + "DOB: " + newPlayer.DOB + "\n" + "Address: " + newPlayer.Address1 + " " + newPlayer.Address2 + "\n" +
                "City: " + newPlayer.City + "\n" + "State: " + newPlayer.State + "\n" + "Zip Code: " + newPlayer.ZipCode + "\n" + "Primary Phone: " + newPlayer.Phone1 + "\n" +
                "Secondary Phone: " + newPlayer.Phone2 + "\n" + "Emergency Contacts: " + newPlayer.Emergency1 + ", " + newPlayer.Emergency2 + "\n" + "Years Experience: " + newPlayer.YearsExp + "\n" +
                "Experience Level: " + newPlayer.Level + "\n" + "\n" + "Programs requested: " + "\n" + "Learn To Play (Monday): " + newPlayer.LTP + "Rec Skill (Tuesday): " + newPlayer.Tuesday + "\n" +
                "C/D SKills (Wednesday): " + newPlayer.Wednesday + "\n" + "Stickhandling and Shooting Skills (Thursday): " + newPlayer.Stickhandling + "\n" +
                "Mixed Level Skills (Somerville, Thursday PM): " + newPlayer.Somerville + "\n" + "Games (Friday): " + newPlayer.Games;

                emailManager.SendEmail(emailConfig.FromAddress, newPlayer.Email, "Updated NEWHL Registration", emailBody);
            }
            else
            {
                //send error message to View
            }


            Decimal LTP_Price = 1;
            ViewBag.LTP_Price = LTP_Price.ToString();

            Decimal Tues_Price = 1;
            ViewBag.Tuesday_Price = Tues_Price.ToString();

            Decimal Wed_Price = 1;
            ViewBag.Wednesday_Price = Wed_Price.ToString();

            Decimal Stickhandling_Price = 1;
            ViewBag.Stickhandling_Price = Stickhandling_Price.ToString();

            Decimal Somerville_Price = 1;
            ViewBag.Somerville_Price = Somerville_Price.ToString();

            Decimal Games_Price = 1;
            ViewBag.Games_Price = Games_Price.ToString();

            Decimal Total_Price = 0;

            if (confirmedPlayer.LTP == "yes")
            {
                Total_Price = Total_Price + LTP_Price;
            }
            if (confirmedPlayer.Tuesday == "yes")
            {
                Total_Price = Total_Price + Tues_Price;
            }
            if (confirmedPlayer.Wednesday == "yes")
            {
                Total_Price = Total_Price + Wed_Price;
            }
            if(confirmedPlayer.Stickhandling == "yes")
            {
                Total_Price = Total_Price + Stickhandling_Price;
            }
            if (confirmedPlayer.Somerville == "yes")
            {
                Total_Price = Total_Price + Somerville_Price;
            }
            if (confirmedPlayer.Games == "yes")
            {
                Total_Price = Total_Price + Games_Price;
            }

            ViewBag.Total_Price = Total_Price.ToString();
            Decimal minimumPayment = Total_Price / 2 ;
            ViewBag.minimumPayment = minimumPayment.ToString();

            String strPaymentAmt = paymentAmount;
            decimal decPaymentAmt = Convert.ToDecimal(strPaymentAmt);

   /*         ActionResult retval = CallPayPal();
            if (paymentType = "cc"  && hasPaid == false)
            {
                CallPayPal();

            }
            //{ then add ViewBag.ShowPayPal, show paypal button with instructions 
            //  OR
            //   Call a function the will open a new view for PayPal and send return value that can be used in Code.
            //return (View(ViewBag));}
*/
            //else if formaction = complete then
                //save payment record, then
                //this.Redirect("http://" + this.Request.Url.Authority + "/Programs/RegistrationComplete");
            //else
            //return View(model);

            return (View(ViewBag));


        }

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
