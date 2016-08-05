using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.Common.DomainModel;

namespace Newhl.Common.Security
{
    public interface ISecurityRepository
    {
        User GetUserInfo();
    }
}
