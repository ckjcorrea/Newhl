using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newhl.Common.DomainModel;
using Newhl.Common.Utilities;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.BusinessLayer.Services;
using Newhl.MainSite.Web.Code.Filters;
using Newhl.MainSite.Web.Areas.Admin.Models;

namespace Newhl.MainSite.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Manage the users int he system
    /// </summary>
    [MVCAuthorization(Roles = RoleType.Names.Administrator)]
    public class ManageUsersController : Newhl.MainSite.Web.Controllers.ControllerBase
    {
        /// <summary>
        /// Displays a list of users
        /// </summary>
        /// <returns>A view</returns>
        public ActionResult Index(int? page)
        {
            int currentPageIndex = 0;

            if(page.HasValue)
            {
                currentPageIndex = page.Value - 1;
            }

            IPagedList<AMFUserLogin> users = new PagedList<AMFUserLogin>(this.ServiceManager.UserService.GetAll(), currentPageIndex, Newhl.MainSite.Web.Models.PagedListModel<int>.PageSize);
            return this.View(users);
        }

        /// <summary>
        /// Display a user to edit
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>A view</returns>
        [MVCAuthorization(Roles = RoleType.Names.Administrator)]
        public ActionResult Edit(int id)
        {
            AMFUserLogin retVal = this.ServiceManager.UserService.GetUserById(id);
            return this.View(retVal);
        }

        /// <summary>
        /// Save changes to a user
        /// </summary>
        /// <param name="user">The user to save</param>
        /// <returns>A view</returns>
        [MVCAuthorization(Roles = RoleType.Names.Administrator)]
        public ActionResult Save(AMFUserLogin user)
        {
            if (user != null)
            {
                using (this.ServiceManager.UnitOfWork.BeginTransaction())
                {
                    this.ServiceManager.UserService.Update(user.Id, user.FirstName, user.LastName, user.UserStatus, user.Role);
                    this.ServiceManager.UnitOfWork.EndTransaction(true);
                }
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Dislay teh login history for a user
        /// </summary>
        /// <param name="userName">The users name</param>
        /// <returns>A view</returns>
        [MVCAuthorization(Roles = RoleType.Names.Administrator)]
        public ActionResult LoginHistory(string userName, int? page)
        {
            LoginHistoryModel retVal = new LoginHistoryModel();

            retVal.UserName = userName;

            if(!string.IsNullOrEmpty(userName))
            {
                int currentPageIndex = 0;

                if(page.HasValue)
                {
                    currentPageIndex = page.Value - 1;
                }

                retVal.LoginHistory = new PagedList<LoginAttempt>(this.ServiceManager.UserService.GetLoginHistory(userName), currentPageIndex, Newhl.MainSite.Web.Models.PagedListModel<int>.PageSize);
            }

            return this.View(retVal);
        }
    }
}