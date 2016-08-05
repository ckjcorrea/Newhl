using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// The methods defined by the Request token repository
    /// </summary>
    public interface IRequestTokenRepository : INHibernateRepository<RequestToken, long>
    {
        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        IList<RequestToken> GetByConsumerKey(string consumerKey, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Get a request token by its token value
        /// </summary>
        /// <param name="token">The token value</param>
        /// <returns>The request token instance</returns>
        RequestToken GetByToken(string token);

        /// <summary>
        /// Get a request token by its token and verifier code
        /// </summary>
        /// <param name="token">The token value</param>
        /// <param name="verifierCode">The request authorization verifier code</param>
        /// <returns>The request token instance</returns>
        RequestToken GetByTokenAndVerifierCode(string token, string verifierCode);

        /// <summary>
        /// Gets an access token by its token value
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>An access token instance</returns>
        AccessToken GetAccessToken(string token);

        /// <summary>
        /// Get an access token by its request token and request token verifier code
        /// </summary>
        /// <param name="token">The request token value</param>
        /// <param name="verifierCode">The authorization verifier code</param>
        /// <returns>An access token instance</returns>
        AccessToken GetAccessTokenByRequestToken(string token, string verifierCode);

        /// <summary>
        /// Get all of the request tokens associated with a user id
        /// </summary>
        /// <param name="userId">The user id to check for</param>
        /// <returns>A list of request tokens</returns>
        IList<RequestToken> GetByUserId(long userId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Get all of the requst tokens associated with a user name
        /// </summary>
        /// <param name="userName">The user name to check for</param>
        /// <returns>A list of request tokens</returns>
        IList<RequestToken> GetByUserName(string userName, DateTime startDate, DateTime endDate);
    }
}
