using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using AlwaysMoveForward.OAuth.Client.OpenAuth;

namespace TestSite.Controllers
{
    public class MonolithOAuthController : Controller
    {
        public virtual ActionResult Index(string returnUrl = "")
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("/Account/Index");
            }

            return new OAuthSignIn
            {
                ReturnUrl = Url.Action("/Monolith/Callback?returnUrl=" + returnUrl)
            };
        }

        // Callback page invoked when the user has completed the oauth action.
        public virtual ActionResult Callback(string returnUrl)
        {
            if (this.Request[VistaprintOAuthClient.FailedQueryParameter] == "1")
            {
                return this.Failure();
            }

            AuthenticationResult authResult =
                OAuthWebSecurity.VerifyAuthentication(
                    Url.Action("/Monolith/Callback?returnUrl=" + returnUrl));

            if (!authResult.IsSuccessful)
            {
                return this.RedirectToAction("/Monolith/Failure");
            }

            return this.Redirect(returnUrl);
        }

        // something failed during log in.
        // usually this means the user didn't grant permission.
        public virtual ActionResult Failure()
        {
            ViewBag.Title = "SignInFailure";
            return this.View();
        }

        private class OAuthSignIn : ActionResult
        {
            public string ReturnUrl { private get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(VistaprintOAuthClient.AuthProviderName, this.ReturnUrl);
            }
        }
    }
}
