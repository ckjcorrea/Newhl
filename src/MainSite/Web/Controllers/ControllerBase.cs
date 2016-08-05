using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newhl.Common.Utilities;
using Newhl.Common.Security;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.BusinessLayer.Services;
using Newhl.MainSite.Web.Code;

namespace Newhl.MainSite.Web.Controllers
{
    /// <summary>
    /// A common base class for controllers
    /// </summary>
    public class ControllerBase : Controller
    {
        /// <summary>
        /// The service manager instance for the current controller context
        /// </summary>
        private IServiceManager serviceManager;

        /// <summary>
        /// Gets the current instance of the service manager
        /// </summary>
        public IServiceManager ServiceManager
        {
            get
            {
                if (serviceManager == null)
                {
                    serviceManager = ServiceManagerBuilder.CreateServiceManager();
                }

                return serviceManager;
            }
        }

        /// <summary>
        /// Gets or sets the current IPrincipal off of, or onto, the thread
        /// </summary>
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
                this.HttpContext.User = value;
            }
        }
    }
}