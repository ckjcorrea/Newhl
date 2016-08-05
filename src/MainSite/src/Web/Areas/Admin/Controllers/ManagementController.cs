using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.Web.Code;

namespace AlwaysMoveForward.OAuth.Web.Areas.Admin.Controllers
{
    [CookieAuthorizationAttribute(RequiredRoles = "Administrator")]
    public class ManagementController : AlwaysMoveForward.OAuth.Web.Controllers.ControllerBase
    {
        // GET: Admin/Management
        [CookieAuthorizationAttribute(RequiredRoles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
    }
}