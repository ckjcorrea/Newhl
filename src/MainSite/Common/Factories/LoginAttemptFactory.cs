using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Common.Factories
{
    public class LoginAttemptFactory
    {
        public static LoginAttempt Create(bool didLoginSucceed, string source, string userName)
        {
            LoginAttempt retVal = new LoginAttempt();
            retVal.AttemptDate = DateTime.UtcNow;
            retVal.WasSuccessfull = didLoginSucceed;
            retVal.Source = source;
            retVal.UserName = userName;

            return retVal;
        }
    }
}
