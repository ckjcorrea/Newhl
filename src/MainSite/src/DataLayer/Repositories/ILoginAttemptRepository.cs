using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.DataMapper;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    public interface ILoginAttemptRepository : INHibernateRepository<LoginAttempt, long>
    {
        /// <summary>
        /// Get a AMFUserLogin by the attempted login name
        /// </summary>
        /// <param name="userName">The users email address used to attempt to login</param>
        /// <returns>The found domain object instance</returns>
        IList<LoginAttempt> GetByUserName(string userName);
    }
}
