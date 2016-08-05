using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.Common.Configuration;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Security;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.Web.Models;
using AlwaysMoveForward.OAuth.Web.Code;

namespace AlwaysMoveForward.OAuth.Web.Controllers
{
    /// <summary>
    /// This controller allows a user to sign in and authorize an OAuth token
    /// </summary>
    public class UserController : ControllerBase
    {        
        /// <summary>
        /// Show the initial sign in page
        /// </summary>
        /// <param name="oauth_Token">The oauth token that this sign in is trying to authorize.</param>
        /// <returns>The index view</returns>
        public ActionResult Signin(string oauth_Token)
        {
            TokenModel model = new TokenModel() { Token = oauth_Token };

            Consumer consumer = this.ServiceManager.ConsumerService.GetByRequestToken(oauth_Token);

            if (consumer != null)
            {
                model.ConsumerName = consumer.Name;
            }

            return this.View("Signin", model);
        }

        /// <summary>
        /// Returns the Signup view for registration.
        /// </summary>
        /// <returns>The MVC View for a registaration</returns>
        public ActionResult SignUp(string oauthToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View(model);
        }

        /// <summary>
        /// Setup the logged in user on the current thread and setup an auth cookie.
        /// </summary>
        /// <param name="user"></param>
        private void SetCurrentUser(AMFUserLogin user)
        {
            this.CurrentPrincipal = new OAuthServerSecurityPrincipal(user);
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
                    TokenModel model = new TokenModel() { Token = registerModel.OAuthToken };
                    return this.View("Signin", model);
                }
            }

            if (registeredUser != null)
            {
                this.SetCurrentUser(registeredUser);
                TokenModel model = new TokenModel() { Token = registerModel.OAuthToken };
                return this.View(model);
            }
            else
            {
                return this.View("SignUp", new { oauthToken = registerModel.OAuthToken });
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

                    if (!string.IsNullOrEmpty(oauthToken))
                    {
                        return this.GrantAccess(oauthToken);
                    }
                    else
                    {
                        if(this.CurrentPrincipal.IsInRole(OAuthRoles.Administrator.ToString()))
                        {
                            return this.Redirect("/Admin/Management/Index");
                        }
                        else 
                        {
                            return this.View(user);
                        }
                    }
                }               
            }

            return this.Signin(oauthToken);
        }

        /// <summary>
        /// Prompt the user to request or deny access to the oauth token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        /// <returns>The view that gives the user the option to grant or deny access</returns>
        [CookieAuthorization]
        public ActionResult GrantAccess(string oauthToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View("GrantAccess", model);              
        }

        /// <summary>
        /// Approves access for the request token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        [CookieAuthorization]
        public void ApproveAccess(string oauthToken)
        {
            try
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                AMFUserLogin currentUser = this.ServiceManager.UserService.GetUserById(int.Parse(ticket.Name));

                if (currentUser != null)
                {
                    this.CurrentPrincipal = new OAuthServerSecurityPrincipal(currentUser);
                    this.RedirectToClient(this.ServiceManager.TokenService.CreateVerifierAndAssociateUserInfo(oauthToken, currentUser), true);
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }
        }

        /// <summary>
        /// Denies access to the request token
        /// </summary>
        /// <param name="oauthToken">the request token</param>
        [CookieAuthorization]
        public void DenyAccess(string oauthToken)
        {
            this.RedirectToClient(this.ServiceManager.TokenService.DenyRequestToken(oauthToken), false);
        }

        /// <summary>
        /// Redirect the caller to the oauth call back once the user has been approved/denied
        /// </summary>
        /// <param name="requestToken">The oauth token</param>
        /// <param name="granted">Whether or not access ahs been granted</param>
        void RedirectToClient(RequestToken requestToken, bool granted)
        {
            string consumerKey = string.Empty;
            string callBackUrl = string.Empty;
            string verifier = string.Empty;
            string oauthToken = string.Empty;

            if (requestToken != null)
            {
                consumerKey = requestToken.ConsumerKey;
                oauthToken = requestToken.Token;

                if (requestToken.IsAuthorized() == true)
                {
                    verifier = requestToken.VerifierCode;
                    callBackUrl = requestToken.GenerateCallBackUrl();
                }
            }

            if (!string.IsNullOrEmpty(callBackUrl))
            {
                this.Response.Redirect(callBackUrl, true);
            }
            else
            {
                if (granted)
                {
                    string successMessage = string.Format(
                      "You have been successfully granted Access, To complete the process, please provide <I>{0}</I> with this verification code: <B>{1}</B>",
                      consumerKey, HttpUtility.HtmlEncode(verifier));

                    this.Response.Write(successMessage);
                }
                else
                {
                    this.Response.Write("Denied");
                }

                this.Response.End();
            }
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
            TokenModel model = new TokenModel() { Token = oauthToken };
            return this.View(model);
        }

        public ActionResult ResetPassword(string oauthToken, string userEmail, string resetToken)
        {
            TokenModel model = new TokenModel() { Token = oauthToken };
            this.ServiceManager.UserService.ResetPassword(userEmail, EmailConfiguration.GetInstance());
            return this.View("Signin", new { oauth_token = oauthToken });
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
