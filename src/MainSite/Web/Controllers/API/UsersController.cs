using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newhl.Common.DomainModel;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.Web.Code.Filters;
using Newhl.MainSite.Web.Models;

namespace Newhl.MainSite.Web.Controllers.API
{
    public class UsersController : BaseAPIController
    {
        [Route("api/User"), HttpGet()]
        [WebApiAuthorization]
        public User Get()
        {
            User retVal = null;

            if (this.CurrentPrincipal.User != null)
            {
                retVal = new User();
                retVal.Email = this.CurrentPrincipal.User.Email;
                retVal.FirstName = this.CurrentPrincipal.User.FirstName;
                retVal.LastName = this.CurrentPrincipal.User.LastName;
                retVal.Id = this.CurrentPrincipal.User.Id;
            }

            return retVal;
        }

        [Route("api/User/{id}"), HttpGet()]
        [WebApiAuthorization]
        public User Get(long id)
        {
            User retVal = null;
            Newhl.MainSite.Common.DomainModel.AMFUserLogin foundUser = this.Services.UserService.GetUserById(id);

            if(foundUser != null)
            {
                retVal = new User();
                retVal.Email = foundUser.Email;
                retVal.FirstName = foundUser.FirstName;
                retVal.LastName = foundUser.LastName;
                retVal.Id = foundUser.Id;
            }

            return retVal;
        }

        [Route("api/Users"), HttpGet()]
        [WebApiAuthorization(Roles = RoleType.Names.Administrator)]
        public PagedListModel<User> GetAll(int? page)
        {
            int currentPageIndex = 0;

            if(page.HasValue)
            {
                currentPageIndex = page.Value - 1;
            }

            IList<User> retVal = this.Services.UserService.GetAll().Cast<User>().ToList();
            return new PagedListModel<Newhl.Common.DomainModel.User>(retVal, currentPageIndex);
        }

        [Route("api/Users/{emailAddress}"), HttpGet()]
        [WebApiAuthorization]
        public IList<User> Get(string emailAddress)
        {
            IList<User> retVal = new List<User>();
            
            IList<Newhl.MainSite.Common.DomainModel.AMFUserLogin> foundUsers = this.Services.UserService.SearchByEmail(emailAddress);

            for (int i = 0; i < foundUsers.Count; i++)
            {
                User mappedUser = new User();
                mappedUser.Email = foundUsers[i].Email;
                mappedUser.FirstName = foundUsers[i].FirstName;
                mappedUser.LastName = foundUsers[i].LastName;
                mappedUser.Id = foundUsers[i].Id;
                retVal.Add(mappedUser);
            }

            return retVal;
        }

        [Route("api/User/{id}"), HttpDelete()]
        [WebApiAuthorization(Roles=RoleType.Names.Administrator)]
        public void Delete(long id)
        {
            this.Services.UserService.Delete(id);
        }
    }
}
