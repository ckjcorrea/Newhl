using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Web.Code;

namespace AlwaysMoveForward.OAuth.Web.Controllers.API
{
    public class UsersController : BaseAPIController
    {
        [WebAPIAuthorization]
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

        [WebAPIAuthorization]
        public User Get(long id)
        {
            User retVal = null;
            AlwaysMoveForward.OAuth.Common.DomainModel.AMFUserLogin foundUser = this.Services.UserService.GetUserById(id);

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

        [WebAPIAuthorization]
        public IList<User> Get(string emailAddress)
        {
            IList<User> retVal = new List<User>();
            
            IList<AlwaysMoveForward.OAuth.Common.DomainModel.AMFUserLogin> foundUsers = this.Services.UserService.SearchByEmail(emailAddress);

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
    }
}
