using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newhl.Common.DomainModel
{
    public interface IRemoteOAuthUser
    {        
        long Id { get; set; }
        long OAuthServiceUserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string AccessToken { get; set; }
        string AccessTokenSecret { get; set; }
    }
}
