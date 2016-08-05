using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newhl.Common.Utilities;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.BusinessLayer.Services;

namespace Newhl.MainSite.Web.Controllers.API
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

        public NewhlSecurityPrincipal CurrentPrincipal
        {
            get
            {
                NewhlSecurityPrincipal retVal = System.Threading.Thread.CurrentPrincipal as NewhlSecurityPrincipal;

                if (retVal == null)
                {
                    try
                    {
                        retVal = new NewhlSecurityPrincipal(null);
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