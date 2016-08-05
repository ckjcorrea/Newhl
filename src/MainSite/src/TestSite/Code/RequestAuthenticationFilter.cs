using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using VP.Digital.Common.Entities;
using VP.Digital.Common.Security;

namespace TestSite.Code
{  
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RequestAuthenticationFilter : FilterAttribute, IAuthorizationFilter
    {
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            DefaultDigitalSecurityPrincipal currentPrincipal = new DefaultDigitalSecurityPrincipal(null);

            System.Web.HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                if (!string.IsNullOrEmpty(ticket.Name))
                {
                    int userId = int.Parse(ticket.Name);
                    DigitalUser digitalUser = new DigitalUser() { Id = int.Parse(ticket.Name) };
                    // Get the authentication cookie
                    currentPrincipal = new DefaultDigitalSecurityPrincipal(digitalUser);
                }
            }

            System.Threading.Thread.CurrentPrincipal = filterContext.RequestContext.HttpContext.User = currentPrincipal;
        }
    }
}