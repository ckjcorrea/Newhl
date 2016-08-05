using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.Common.Security;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    public class OAuthServerSecurityPrincipal : SecurityPrincipalBase<AMFUserLogin>
    {
        public OAuthServerSecurityPrincipal(AMFUserLogin user) : base(user, ImplementedAuthenticationType.OAuth)
        {
        }

        public override string Name
        {
            get
            {
                string retVal = string.Empty;

                if(this.User != null)
                {
                    retVal = this.User.Email;
                }

                return retVal;
            }
        }

        public override bool IsInRole(string role)
        {
            bool retVal = false;
            OAuthRoles targetRole;
            
            if(Enum.TryParse<OAuthRoles>(role, out targetRole))
            {
                if (this.User.Role == targetRole)
                {
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}