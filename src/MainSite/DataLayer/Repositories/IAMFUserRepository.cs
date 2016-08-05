using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.Common.DataLayer.NHibernate;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.DataLayer.DataMapper;

namespace Newhl.MainSite.DataLayer.Repositories
{
    /// <summary>
    /// A repository for retrieving Newhl Users
    /// </summary>
    public interface IAMFUserRepository : INHibernateRepository<AMFUserLogin, long>
    {
        /// <summary>
        /// Get a AMFUserLogin by the email address.
        /// </summary>
        /// <param name="emailAddress">The users email address</param>
        /// <returns>The found domain object instance</returns>
        AMFUserLogin GetByEmail(string emailAddress);

        /// <summary>
        /// Search for a user by its email
        /// </summary>
        /// <param name="email">Search the email field for similar strings</param>
        /// <returns>The user if one is found</returns>
        IList<AMFUserLogin> SearchByEmail(string email);
    }
}
