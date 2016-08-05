using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newhl.Common.DomainModel
{
    public class RemoteOAuthUser : IRemoteOAuthUser
    {
        public RemoteOAuthUser()
        {
            this.Id = -1;
            this.OAuthServiceUserId = 0;
        }

        public long Id { get; set; }
        public long OAuthServiceUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public string GetDisplayName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
