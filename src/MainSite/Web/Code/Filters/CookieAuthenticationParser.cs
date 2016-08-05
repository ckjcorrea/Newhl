using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Newhl.Common.DataLayer;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.BusinessLayer.Services;

namespace Newhl.MainSite.Web.Code.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CookieAuthenticationParser : FilterAttribute, IAuthorizationFilter
    {
        public static NewhlSecurityPrincipal ParseCookie(HttpCookieCollection cookies)
        {
            // Get the authentication cookie
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = cookies[cookieName];
            NewhlSecurityPrincipal retVal = null;

            IServiceManager serviceManager = ServiceManagerBuilder.CreateServiceManager();

            if (authCookie != null)
            {
                if (authCookie.Value != string.Empty)
                {
                    try
                    {
                        // Get the authentication ticket 
                        // and rebuild the principal & identity
                        FormsAuthenticationTicket authTicket =
                        FormsAuthentication.Decrypt(authCookie.Value);

                        AMFUserLogin currentUser = serviceManager.UserService.GetUserById(int.Parse(authTicket.Name));
                        retVal = new NewhlSecurityPrincipal(currentUser);
                    }
                    catch (Exception e)
                    {
                        retVal = new NewhlSecurityPrincipal(null);
                    }
                }
            }
            else
            {
                retVal = new NewhlSecurityPrincipal(null);
            }

            System.Threading.Thread.CurrentPrincipal = retVal;
            HttpContext.Current.User = retVal;

            return retVal;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            CookieAuthenticationParser.ParseCookie(filterContext.RequestContext.HttpContext.Request.Cookies);
        }
    }
}