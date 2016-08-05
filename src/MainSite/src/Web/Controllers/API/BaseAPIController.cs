using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;

namespace AlwaysMoveForward.OAuth.Web.Controllers.API
{
    public class BaseAPIController : ApiController
    {
        private IServiceManager serviceManager;

        public IServiceManager Services
        {
            get
            {
                if (this.serviceManager == null)
                {
                    this.serviceManager = ServiceManagerBuilder.CreateServiceManager();
                }

                return this.serviceManager;
            }
        }

        public OAuthServerSecurityPrincipal CurrentPrincipal
        {
            get
            {
                OAuthServerSecurityPrincipal retVal = System.Threading.Thread.CurrentPrincipal as OAuthServerSecurityPrincipal;

                if (retVal == null)
                {
                    try
                    {
                        retVal = new OAuthServerSecurityPrincipal(null);
                        System.Threading.Thread.CurrentPrincipal = retVal;
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                    }
                }

                return retVal;
            }
            set
            {
                System.Threading.Thread.CurrentPrincipal = value;
            }
        }
    }
}