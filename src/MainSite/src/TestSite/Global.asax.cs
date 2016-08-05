using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AlwaysMoveForward.OAuth.Client.OpenAuth;
using TestSite.Code;

namespace TestSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            TestSite.Code.AuthorizationConfig.RegisterOAuthConfig();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private static InMemoryTokenManager tokenManager;

        public static InMemoryTokenManager GetTokenManager(string consumerKey, string consumerSecret)
        {
            if (tokenManager == null)
            {
                tokenManager = new InMemoryTokenManager(consumerKey, consumerSecret);
            }

            return tokenManager;
        }
    }
}