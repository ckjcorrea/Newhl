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
    public class WebAPIAuthorizationAttribute : System.Web.Http.AuthorizeAttribute
    {
        public WebAPIAuthorizationAttribute()
            : base()
        {

        }

        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool retVal = false;

            OAuthServerSecurityPrincipal principal = AccessTokenAuthorizationFilter.ProcessOAuthHeader();

            if (principal != null)
            {
                retVal = principal.IsAuthenticated;
            }

            return retVal;
        }
    }
}