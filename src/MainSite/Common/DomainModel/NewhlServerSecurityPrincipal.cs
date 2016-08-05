using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.Common.Security;

namespace Newhl.MainSite.Common.DomainModel
{
    public class NewhlSecurityPrincipal : SecurityPrincipalBase<AMFUserLogin>
    {
        public NewhlSecurityPrincipal(AMFUserLogin user) : base(user, ImplementedAuthenticationType.Login)
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
            RoleType.Id targetRole;
            
            if(Enum.TryParse<RoleType.Id>(role, out targetRole))
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