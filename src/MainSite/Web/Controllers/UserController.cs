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
        public ActionResult Register(RegisterModel registerModel)
        {
            AMFUserLogin registeredUser = null;

            if (registerModel != null)
            {
                registeredUser = this.ServiceManager.UserService.GetByEmail(registerModel.UserEmail);

                if (registeredUser == null)
                {
                    registeredUser = this.ServiceManager.UserService.Register(registerModel.UserEmail, registerModel.Password, registerModel.PasswordHint, registerModel.FirstName, registerModel.LastName);
                }
                else
                {
                     return this.View("Signin");
                }
            }

            if (registeredUser != null)
            {
                this.SetCurrentUser(registeredUser);
                return this.View();
            }
            else
            {
                return this.View("SignUp");
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

                    if(this.CurrentPrincipal.IsInRole(RoleType.Names.Administrator))
                    {
                        return this.Redirect("/Admin/Management/Index");
                    }
                    else 
                    {
                        return this.View(user);
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
        public ActionResult Edit(RegisterModel registerModel)
        {
            AMFUserLogin registeredUser = null;

            if (registerModel != null)
            {
                registeredUser = this.ServiceManager.UserService.Update(registerModel.Id, registerModel.FirstName, registerModel.LastName, registerModel.Password);

                if (registeredUser != null)
                {
                    this.SetCurrentUser(registeredUser);
                }

                return this.View("ProcessSignin", registeredUser);
            }
            else
            {
                return this.View("Signin");
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
