using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.Web.Code;
using AlwaysMoveForward.OAuth.Web.Areas.Admin.Models;

namespace AlwaysMoveForward.OAuth.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Manage the users int he system
    /// </summary>
    [CookieAuthorizationAttribute(RequiredRoles = "Administrator")]
    public class ManageUsersController : AlwaysMoveForward.OAuth.Web.Controllers.ControllerBase
    {
        /// <summary>
        /// Displays a list of users
        /// </summary>
        /// <returns>A view</returns>
        public ActionResult Index()
        {
            IList<AMFUserLogin> users = this.ServiceManager.UserService.GetAll();
            return this.View(users);
        }

        /// <summary>
        /// Display a user to edit
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>A view</returns>
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
        public ActionResult LoginHistory(string userName)
        {
            LoginHistoryModel retVal = new LoginHistoryModel();

            retVal.UserName = userName;

            if(!string.IsNullOrEmpty(userName))
            {
                retVal.LoginHistory = this.ServiceManager.UserService.GetLoginHistory(userName);
            }

            return this.View(retVal);
        }
    }
}