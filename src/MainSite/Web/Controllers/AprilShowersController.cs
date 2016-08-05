using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newhl.Common.Configuration;
using Newhl.Common.DomainModel;
using Newhl.Common.Security;
using Newhl.Common.Utilities;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.Web.Models;
using Newhl.MainSite.Web.Code.Filters;

namespace Newhl.MainSite.Web.Controllers
{
    /// <summary>
    /// This controller allows a user to sign in and authorize an OAuth token
    /// </summary>
    public class AprilShowersController : ControllerBase
    {
        /// <summary>
        /// Returns the Index page.
        /// </summary>
        /// <returns>The MVC View for the home page of the NEWHL site</returns>
        public ActionResult TournamentInfo()
        {
            return this.View();
        }

        /// <summary>
        /// Returns the About page.
        /// </summary>
        /// <returns>The MVC View for the About page of the NEWHL site</returns>
        public ActionResult History()
        {
            return this.View();
        }

    }
}
