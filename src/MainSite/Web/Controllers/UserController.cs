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
    /// <summary>
    /// This controller allows a user to sign in and authorize an OAuth token
    /// </summary>
    public class UserController : ControllerBase
    {

        public ActionResult EmbeddedSignin()
        {
            return this.View();
        }

        /// <summary>
        /// Show the initial sign in page
        /// </summary>
        /// <param name="oauth_Token">The oauth token that this sign in is trying to authorize.</param>
        /// <returns>The index view</returns>
        public ActionResult Signin()
        {
            return this.View("Signin");
        }

        /// <summary>
        /// Returns the Signup view for registration.
        /// </summary>
        /// <returns>The MVC View for a registaration</returns>
        public ActionResult SignUp()
        {
            return this.View();
        }

        /// <summary>
        /// Setup the logged in user on the current thread and setup an auth cookie.
        /// </summary>
        /// <param name="user"></param>
        private void SetCurrentUser(AMFUserLogin user)
        {
            this.CurrentPrincipal = new NewhlSecurityPrincipal(user);
            FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
        }

        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="registerModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        public ActionResult Register(AMFUserLogin userLogin)
        {
            AMFUserLogin retVal = userLogin;
            
            if(retVal==null)
            {
                retVal = new AMFUserLogin();
            }            

            return this.View(retVal);
        }


        public void ValidateUserRegistration(AMFUserLogin playerInfo)
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
        }

        public ActionResult ProcessRegistration(AMFUserLogin playerInfo, string emailConfirm, string password, string passwordConfirm)
        {
            AMFUserLogin retVal = playerInfo;

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
                ViewBag.playerExists = "That email currently exists for a NEWHL player. Please check the spelling and try again or login to continue if this is your correct email address.";
                ViewData.ModelState.AddModelError("exists", "That email address currently exists for a NEWHL player. Please check the spelling and try again or login to continue if this is your correct email address.");
            }

            this.ValidateUserRegistration(playerInfo);

            if (String.IsNullOrEmpty(emailConfirm) || (!playerInfo.Email.Equals(emailConfirm)))
            {
                ViewData.ModelState.AddModelError("emailConfirm", "Email addresses could not be confirmed. Both Email address fields must match");
            }

            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(passwordConfirm) || password != passwordConfirm)
            {
                ViewData.ModelState.AddModelError("passwordConfirm", "Passwords could not be confirmed. Both password fields must match");
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
                    AMFUserLogin newPlayer = this.ServiceManager.UserService.Register(playerInfo.FirstName, playerInfo.LastName, playerInfo.Email, password, String.Empty, playerInfo.USAHockeyNum, playerInfo.DOB, playerInfo.Address1, playerInfo.Address2, playerInfo.City, playerInfo.State, playerInfo.ZipCode, playerInfo.Phone1, playerInfo.Phone2, playerInfo.Emergency1, playerInfo.Emergency2, playerInfo.YearsExp, playerInfo.Level, playerInfo.Internet, playerInfo.Referral, playerInfo.Tournament, playerInfo.Other);
                    //hard coded (userID=0, isSiteAdmin=false, isActive=true, isFirstLogin=true ... Guests=0)

                    this.ServiceManager.EmailService.SendPlayerEmailConfiguration(newPlayer.Email, emailConfig, newPlayer);
                    this.ServiceManager.EmailService.SendAdminNotificationEmail("", emailConfig, newPlayer);

                    retVal = newPlayer;
                }

                this.SetCurrentUser(retVal);
                return this.View("Manage", retVal);
            }
            else
            {
                return this.View("Register", retVal);
            }
        }



        /// <summary>
        /// Logs a user into the system and authorizes a request token
        /// </summary>
        /// <param name="userName">The username to log in</param>
        /// <param name="password">The users password to sign in</param>
        /// <param name="oauthToken">The oauth token to authorize</param>
        /// <returns>The Grant Access view or the sign in screen again</returns>
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSignin(string userName, string password, string oauthToken)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // yes actually look up the user, but for now
                AMFUserLogin user = this.ServiceManager.UserService.LogonUser(userName, password, this.Request.UserHostAddress);

                if (user != null)
                {
                    this.SetCurrentUser(user);

                    if (this.CurrentPrincipal.IsInRole(RoleType.Names.Administrator))
                    {
                        return this.Redirect("/Admin/Management/Index");
                    }
                    else
                    {
                        return this.View("Manage", user);
                    }
                }
            }

            return this.Signin();
        }

        private void EliminateUserCookie()
        {
            try
            {
                string cookieName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = this.Response.Cookies[cookieName];

                if (authCookie != null)
                {
                    authCookie.Expires = DateTime.Now.AddDays(-1);
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }
        }

        [MVCAuthorization]
        public ActionResult Signout()
        {
            this.EliminateUserCookie();
            this.CurrentPrincipal = new NewhlSecurityPrincipal(null);
            return this.Signin();
        }

        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <param name="emailAddress">The email address of the user</param>
        /// <returns>An MVC view</returns>
        public ActionResult PasswordHint(string emailAddress)
        {
            PasswordHintModel retVal = new PasswordHintModel();

            AMFUserLogin targetUser = this.ServiceManager.UserService.GetByEmail(emailAddress);

            if (targetUser != null)
            {
                retVal.PasswordHint = targetUser.PasswordHint;
            }

            return this.View(retVal);
        }

        public ActionResult ForgotPassword(string oauthToken)
        {
            return this.View();
        }

        public ActionResult ResetPassword(string oauthToken, string userEmail, string resetToken)
        {
            this.ServiceManager.UserService.ResetPassword(userEmail, EmailConfiguration.GetInstance());
            return this.View("Signin");
        }

        /// <summary>
        /// Register a new user with the site
        /// </summary>
        /// <param name="registerModel">The incoming parameters used to register the user</param>
        /// <returns>The Registered user view</returns>
        [MVCAuthorization]
        public ActionResult Edit(AMFUserLogin registerModel)
        {
            AMFUserLogin registeredUser = null;

            if (registerModel != null)
            {
                registeredUser = this.ServiceManager.UserService.Update(registerModel.Id, registerModel.FirstName, registerModel.LastName, registerModel.Email,
                    registerModel.USAHockeyNum, registerModel.DOB, registerModel.Address1, registerModel.Address2, registerModel.City, registerModel.State, registerModel.ZipCode,
                    registerModel.Phone1, registerModel.Phone2, registerModel.Emergency1, registerModel.Emergency2, registerModel.YearsExp, registerModel.Level, registerModel.Internet,
                    registerModel.Referral, registerModel.Tournament, registerModel.Other);

                if (registeredUser != null)
                {
                    this.SetCurrentUser(registeredUser);
                }

                return this.View("Manage", registeredUser);
            }
            else
            {
                return this.View("Signin");
            }
        }
        
        [MVCAuthorization]
        public ActionResult MakePayment()
        {
            if(this.CurrentPrincipal==null || this.CurrentPrincipal.IsAuthenticated==false)
            {
                return this.View("Signin");
            }
            else
            {
                return this.View(this.CurrentPrincipal.User);
            }
        }
        /// <summary>
        /// this action returns the partial view to show the password hint
        /// </summary>
        /// <returns>An MVC view</returns>
        [MVCAuthorization]
        public JsonResult GetByEmail(string emailAddress)
        {
            User retVal = new User();

            if (this.CurrentPrincipal.User != null)
            {
                retVal = this.ServiceManager.UserService.GetByEmail(emailAddress);
            }

            return this.Json(retVal, JsonRequestBehavior.AllowGet);
        }
    }
}
