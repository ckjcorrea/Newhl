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
    public class AccessTokenAuthorizationFilter
    {
        public static OAuthServerSecurityPrincipal ProcessOAuthHeader()
        {
            // Get the authentication cookie
            IServiceManager serviceManager = ServiceManagerBuilder.CreateServiceManager();
            OAuthServerSecurityPrincipal retVal = null;

            var context = new HttpContextWrapper(HttpContext.Current);

            try
            {
                HttpRequestBase request = context.Request;

                string loadBalancerEndpointsValue = System.Configuration.ConfigurationManager.AppSettings[AMFOAuthContextBuilder.LoadBalancerEndpointsSetting];
                string[] loadBalancerEndpoints = null;

                if (!string.IsNullOrEmpty(loadBalancerEndpointsValue))
                {
                    loadBalancerEndpoints = loadBalancerEndpointsValue.Split(',');
                }

                IOAuthContext oauthContext = AMFOAuthContextBuilder.FromHttpRequest(request, loadBalancerEndpoints);

                ValidatedToken validatedToken = serviceManager.TokenService.ValidateSignature(oauthContext, oauthContext.Token, AlwaysMoveForward.OAuth.Client.Constants.HmacSha1SignatureMethod, oauthContext.GenerateSignatureBase());

                if (validatedToken != null && validatedToken.User != null && validatedToken.OAuthToken != null)
                {
                    retVal = new OAuthServerSecurityPrincipal(serviceManager.UserService.GetUserById(validatedToken.User.Id));
                    System.Threading.Thread.CurrentPrincipal = retVal;
                    HttpContext.Current.User = retVal;
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
                retVal = null;
            }

            return retVal;
        }
    }
}