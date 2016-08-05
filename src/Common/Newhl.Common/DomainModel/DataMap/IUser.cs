using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newhl.Common.DomainModel.DataMap
{
    public interface IUser
    {
        int UserId { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        bool ApprovedCommenter { get; set; }
        bool IsActive { get; set; }
        bool IsSiteAdministrator { get; set; }
        string About { get; set; }
        string DisplayName { get; set; }
    }
}
