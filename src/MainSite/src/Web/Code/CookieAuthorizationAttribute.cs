using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Web.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CookieAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public CookieAuthorizationAttribute()
            : base()
        {
            this.RequiredRoles = string.Empty;
        }

        public string RequiredRoles { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthorized = false;

            OAuthServerSecurityPrincipal securityPrincipal = null;
            HttpCookie authCookie = filterContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if(authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                IServiceManager serviceManager = ServiceManagerBuilder.CreateServiceManager();

                if (serviceManager != null)
                {
                    AMFUserLogin currentUser = serviceManager.UserService.GetUserById(int.Parse(ticket.Name));
                    securityPrincipal = new OAuthServerSecurityPrincipal(currentUser);
                    System.Threading.Thread.CurrentPrincipal = securityPrincipal;
                    HttpContext.Current.User = securityPrincipal;

                    if (securityPrincipal.IsAuthenticated == true)
                    {
                        if (string.IsNullOrEmpty(this.RequiredRoles))
                        {
                            isAuthorized = true;
                        }
                        else
                        {
                            string[] roleList = this.RequiredRoles.Split(',');

                            foreach (string role in roleList)
                            {
                                if (securityPrincipal.IsInRole(role))
                                {
                                    isAuthorized = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (isAuthorized == false)
            {
                // not allowed to proceed
                filterContext.Result = new RedirectResult("http://" + HttpContext.Current.Request.Url.Authority);
            }
        }
    }
}