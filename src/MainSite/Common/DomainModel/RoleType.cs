using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newhl.MainSite.Common.DomainModel
{
    public class RoleType
    {
        public static Dictionary<RoleType.Id, string> Roles;

        static RoleType()
        {
            RoleType.Roles = new Dictionary<RoleType.Id, string>();
            RoleType.Roles.Add(RoleType.Id.User, RoleType.Id.User.ToString());
            RoleType.Roles.Add(RoleType.Id.Administrator, RoleType.Id.Administrator.ToString());
        }

        public enum Id
        {
            User = 0,
            Administrator = 1
        }

        public class Names
        {
            public const string User = "User";
            public const string Administrator = "Administrator";
        }
    }
}
