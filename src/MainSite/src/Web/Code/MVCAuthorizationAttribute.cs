using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Security;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.BusinessLayer;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.Common;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Client.Configuration;

namespace AlwaysMoveForward.OAuth.Web.Code
{
    /// <summary>
    /// This attribute can be put on a web method to test the signature of an oauth signed request
    /// before getting into the method itself
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MVCAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// The on authorization method is where the work happens
        /// </summary>
        /// <param name="authorizationContext">Wraps the request context.</param>
        public virtual void OnAuthorization(AuthorizationContext authorizationContext)
        {
            OAuthServerSecurityPrincipal principal = AccessTokenAuthorizationFilter.ProcessOAuthHeader();

            if (principal == null)
            {
                authorizationContext.Result = new RedirectResult(Constants.LoginRoute);
            }
        }
    }
}