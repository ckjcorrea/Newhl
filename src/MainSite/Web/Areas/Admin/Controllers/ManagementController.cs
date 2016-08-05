using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newhl.Common.DomainModel;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.BusinessLayer.Services;
using Newhl.MainSite.Web.Code.Filters;

namespace Newhl.MainSite.Web.Areas.Admin.Controllers
{
    [MVCAuthorization(Roles = "Administrator")]
    public class ManagementController : Newhl.MainSite.Web.Controllers.ControllerBase
    {
        // GET: Admin/Management
        [MVCAuthorization(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
    }
}