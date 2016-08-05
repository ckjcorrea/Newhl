using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Storage;
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Security;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
{
    /// <summary>
    /// THe interface for the token service
    /// </summary>
    public interface ITokenService : ITokenStore
    {
        /// <summary>
        /// Create an access token using the request and request authorization values.
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <param name="verifierCode">The verifier code</param>
        /// <returns>An access token instance</returns>
        AccessToken ExchangeRequestTokenForAccessToken(string requestToken, string verifier);

        /// <summary>
        /// Marks the request token as access denied
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <returns>A request token</returns>
        RequestToken DenyRequestToken(string requestToken);

        /// <summary>
        /// Finds the access token and checks its expriation date
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <param name="verifierCode">The verifier code</param>
        /// <returns>An instance of the access token if it has not expired</returns>
        AccessToken GetValidAccessTokenByRequestToken(string requestToken, string verifierCode);

        /// <summary>
        /// Gets the access token associated with a request token
        /// </summary>
        /// <param name="oauthToken">The token value (request or access)</param>
        /// <returns>An access token</returns>
        AccessToken GetAccessToken(string oauthToken);

        /// <summary>
        /// Take a reqeust token, establish a verifier, and store its authorization information
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <param name="currentUser">The current user to use as authorization info</param>
        /// <returns>The updated request token</returns>
        RequestToken CreateVerifierAndAssociateUserInfo(string requestToken, AMFUserLogin currentUser);

        /// <summary>
        /// Take a reqeust token, establish a verifier, and store its authorization information
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <param name="securityPrincipal">The current security principal to use as authorization info</param>
        /// <returns>The updated request token</returns>
        RequestToken AutoGrantRequestToken(string requestToken, Consumer consumer);

        /// <summary>
        /// Validates the signature of a request
        /// </summary>
        /// <param name="requestContext">The current request context</param>
        /// <param name="oauthToken">The access token associated with the signature</param>
        /// <param name="signatureMethod">The method of signature</param>
        /// <param name="signatureBase">The base string of the signature</param>
        /// <returns>The current user associated with the Access token</returns>
        ValidatedToken ValidateSignature(IOAuthContext requestContext, string oauthToken, string signatureMethod, string signatureBase);

        /// <summary>
        /// Gets the requst tokens associated with a user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>A list of request tokens</returns>
        IList<RequestToken> GetByUser(AMFUserLogin user, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the requst tokens associated with a consumer key.
        /// </summary>
        /// <param name="consumerKey">The consumer key</param>
        /// <returns>A list of request tokens</returns>
        IList<RequestToken> GetByConsumerKey(string consumerKey, DateTime startDate, DateTime endDate);
    }
}
