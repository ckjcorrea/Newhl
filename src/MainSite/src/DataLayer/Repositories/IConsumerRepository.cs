using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Storage;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// The Consumer Repository interface
    /// </summary>
    public interface IConsumerRepository : INHibernateRepository<Consumer, string>
    {
        /// <summary>
        /// Get a Consumer by its key
        /// </summary>
        /// <param name="consumerKey">The consumer key value</param>
        /// <returns>The consumer if found, null otherwise</returns>
        Consumer GetByConsumerKey(string consumerKey);

        /// <summary>
        /// Find all Consumers with the same email address
        /// </summary>
        /// <param name="contactEmail">The email address to search for</param>
        /// <returns>A list of all found Consumers</returns>
        IList<Consumer> GetByContactEmail(string contactEmail);

        /// <summary>
        /// Get a consumer instance by a request token
        /// </summary>
        /// <param name="consumerKey">The request token to search for</param>
        /// <returns>An instance of a consumer</returns>
        Consumer GetByRequestToken(string requestToken);
    }
}
