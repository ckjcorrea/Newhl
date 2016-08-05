using System;
using System.Collections.Generic;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Framework.Signing;
using DevDefined.OAuth.Storage;
using DevDefined.OAuth.Utility;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Security;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
{
    /// <summary>
    /// This services does the primary business rules for working with Request/Access tokens
    /// And acts as the IToken store for the DevDefined codebase
    /// </summary>
    public class TokenService : ITokenService, ITokenStore
    {
        /// <summary>
        /// A constructor that takes the repositories to retrieve consumer and token info
        /// </summary>
        /// <param name="consumerRepository">The repository containing consumer information</param>
        /// <param name="requestTokenRepository">The repository containing token information</param>
        public TokenService(IConsumerRepository consumerRepository, IRequestTokenRepository requestTokenRepository)
        {
            this.ConsumerRepository = consumerRepository;
            this.RequestTokenRepository = requestTokenRepository;
        }

        /// <summary>
        /// Gets the passed in Consumer Repository
        /// </summary>
        protected IConsumerRepository ConsumerRepository { get; private set; }

        /// <summary>
        /// Gets the passed in RequestToken repository
        /// </summary>
        protected IRequestTokenRepository RequestTokenRepository { get; private set; }

        #region ITokenStore

        /// <summary>
        /// When converting a request token to an access token we want to be sure to mark
        /// the request token as used.
        /// </summary>
        /// <param name="requestContext">The current OAuthContext</param>
        public void ConsumeRequestToken(IOAuthContext requestContext)
        {
            if (requestContext != null)
            {
                RequestToken foundToken = this.RequestTokenRepository.GetByToken(requestContext.Token);

                if (foundToken != null)
                {
                    if (foundToken.UsedUp || foundToken.AccessToken != null)
                    {
                        throw new OAuthException(requestContext, OAuthProblems.TokenRejected, "The request token has already be consumed.");
                    }

                    this.RequestTokenRepository.Save(foundToken);
                }
            }
            else
            {
                throw new ArgumentNullException("requestContext");
            }
        }

        /// <summary>
        /// Verify that the access token has expired
        /// </summary>
        /// <param name="accessContext">The current OAuthContext</param>
        public void ConsumeAccessToken(IOAuthContext accessContext)
        {
            if (accessContext != null)
            {
                AccessToken accessToken = GetAccessToken(accessContext);

                if (accessToken.ExpirationDate < Clock.Now)
                {
                    throw new OAuthException(accessContext, OAuthProblems.TokenExpired, "Token has expired");
                }
            }
        }

        /// <summary>
        /// Create an access token using xAuth.
        /// </summary>
        /// <param name="context">The current OAuthContext</param>
        /// <returns>An access token</returns>
        public IToken CreateAccessToken(IOAuthContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return this.ExchangeRequestTokenForAccessToken(context.Token, context.Verifier);
        }

        /// <summary>
        /// Creates a new request token
        /// </summary>
        /// <param name="context">The current OAuthContext</param>
        /// <returns>A request token instance</returns>
        public IToken CreateRequestToken(IOAuthContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            Realm parsedRealm = Realm.Parse(context.Realm);
 
            RequestToken retVal = new RequestToken
            {
                ConsumerKey = context.ConsumerKey,
                Realm = parsedRealm,
                Token = Guid.NewGuid().ToString(),
                Secret = Guid.NewGuid().ToString(),
                CallbackUrl = context.CallbackUrl
            };

            retVal = this.RequestTokenRepository.Save(retVal);

            return retVal;
        }

        /// <summary>
        /// Find the access token using the request token
        /// </summary>
        /// <param name="requestContext">The current OAuthContext</param>
        /// <returns>The access Token</returns>
        public IToken GetAccessTokenAssociatedWithRequestToken(IOAuthContext requestContext)
        {
            IToken retVal = null;

            if (requestContext != null)
            {
                retVal = this.ExchangeRequestTokenForAccessToken(requestContext.Token, requestContext.Verifier);
            }

            return retVal;
        }

        /// <summary>
        /// Get the secret associated with the access token
        /// </summary>
        /// <param name="context">The current OAuthContext</param>
        /// <returns>The value of the secret</returns>
        public string GetAccessTokenSecret(IOAuthContext context)
        {
            string retVal = string.Empty;

            AccessToken token = this.GetAccessToken(context);

            if (token != null)
            {
                retVal = token.Secret;
            }

            return retVal;
        }

        /// <summary>
        /// Gets the callbackurl for the request token
        /// </summary>
        /// <param name="requestContext">The current OAuthContext</param>
        /// <returns>The callbackurl</returns>
        public string GetCallbackUrlForToken(IOAuthContext requestContext)
        {
            string retVal = string.Empty;

            if (requestContext != null)
            {
                RequestToken requestToken = this.RequestTokenRepository.GetByToken(requestContext.Token);

                if (requestToken != null)
                {
                    retVal = requestToken.CallbackUrl;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the secret of a request token
        /// </summary>
        /// <param name="context">The current OAuthContext</param>
        /// <returns>The request token secret</returns>
        public string GetRequestTokenSecret(IOAuthContext context)
        {
            string retVal = string.Empty;

            if (context != null)
            {
                RequestToken requestToken = this.RequestTokenRepository.GetByToken(context.Token);

                if (requestToken != null)
                {
                    retVal = requestToken.Secret;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Check the status of the request token
        /// </summary>
        /// <param name="accessContext">The current OAuthContext</param>
        /// <returns>The status of the request token</returns>
        public RequestForAccessStatus GetStatusOfRequestForAccess(IOAuthContext requestContext)
        {
            RequestForAccessStatus retVal = RequestForAccessStatus.Unknown;

            if (requestContext != null)
            {
                RequestToken request = this.RequestTokenRepository.GetByToken(requestContext.Token);

                if (request != null)
                {
                    if (request.State == TokenState.AccessDenied)
                    {
                        retVal = RequestForAccessStatus.Denied;
                    }

                    if (request.IsAuthorized() == true)
                    {
                        retVal = RequestForAccessStatus.Granted;
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Get the verification code associated with the request token
        /// </summary>
        /// <param name="requestContext">The current OAuthContext</param>
        /// <returns>The verifier code</returns>
        public string GetVerificationCodeForRequestToken(IOAuthContext requestContext)
        {
            string retVal = string.Empty;

            if (requestContext != null)
            {
                RequestToken requestToken = this.RequestTokenRepository.GetByToken(requestContext.Token);

                if (requestToken != null)
                {
                    if (requestToken.IsAuthorized() == true)
                    {
                        retVal = requestToken.VerifierCode;
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Renew an access token, not used at this time
        /// </summary>
        /// <param name="requestContext">The current OAuthContext</param>
        /// <returns>An access token</returns>
        public IToken RenewAccessToken(IOAuthContext requestContext)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Create an access token using the request and request authorization values.
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <param name="verifier">The verifier code</param>
        /// <returns>An access token instance</returns>
        public AccessToken ExchangeRequestTokenForAccessToken(string requestToken, string verifier)
        {
            AccessToken retVal = null;

            if (!string.IsNullOrEmpty(requestToken) && !string.IsNullOrEmpty(verifier))
            {
                RequestToken authorizedRequestToken = this.RequestTokenRepository.GetByTokenAndVerifierCode(requestToken, verifier);

                if (authorizedRequestToken != null && authorizedRequestToken.IsAuthorized() == true && authorizedRequestToken.AccessToken == null)
                {
                    Consumer tokenConsumer = this.ConsumerRepository.GetByConsumerKey(authorizedRequestToken.ConsumerKey);

                    AccessToken newAccessToken = new AccessToken
                    {
                        ConsumerKey = authorizedRequestToken.ConsumerKey,
                        DateGranted = DateTime.UtcNow,
                        ExpirationDate = DateTime.UtcNow.AddHours(tokenConsumer.AccessTokenLifetime),
                        Realm = authorizedRequestToken.Realm,
                        Token = Guid.NewGuid().ToString(),
                        Secret = Guid.NewGuid().ToString(),
                        UserName = authorizedRequestToken.UserName,
                        UserId = authorizedRequestToken.UserId
                    };

                    authorizedRequestToken.AccessToken = newAccessToken;
                    authorizedRequestToken.State = TokenState.AccessGranted;

                    authorizedRequestToken = this.RequestTokenRepository.Save(authorizedRequestToken);

                    if (authorizedRequestToken != null && authorizedRequestToken.AccessToken != null)
                    {
                        retVal = authorizedRequestToken.AccessToken;
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Marks the request token as access denied
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <returns>A request token</returns>
        public RequestToken DenyRequestToken(string requestToken)
        {
            RequestToken retVal = null;

            if (!string.IsNullOrEmpty(requestToken))
            {
                RequestToken foundToken = this.RequestTokenRepository.GetByToken(requestToken);

                if (foundToken != null)
                {
                    foundToken.State = TokenState.AccessDenied;
                    retVal = this.RequestTokenRepository.Save(foundToken);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the access token associated with a request token
        /// </summary>
        /// <param name="context">The current OAuthContext</param>
        /// <returns>An access token</returns>
        public AccessToken GetAccessToken(IOAuthContext context)
        {
            AccessToken retVal = null;

            if (context != null)
            {
                retVal = this.GetAccessToken(context.Token);
            }

            return retVal;
        }

        /// <summary>
        /// Gets the access token associated with a request token
        /// </summary>
        /// <param name="oauthToken">The token value (request or access)</param>
        /// <returns>An access token</returns>
        public AccessToken GetAccessToken(string oauthToken)
        {
            AccessToken retVal = null;

            if (!string.IsNullOrEmpty(oauthToken))
            {
                RequestToken requestToken = this.RequestTokenRepository.GetByToken(oauthToken);

                if (requestToken == null)
                {
                    retVal = this.RequestTokenRepository.GetAccessToken(oauthToken);
                }
                else
                {
                    retVal = requestToken.AccessToken;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Finds the access token and checks its expiration date
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <param name="verifierCode">The verifier code</param>
        /// <returns>An instance of the access token if it has not expired</returns>
        public AccessToken GetValidAccessTokenByRequestToken(string requestToken, string verifierCode)
        {
            AccessToken retVal = this.RequestTokenRepository.GetAccessTokenByRequestToken(requestToken, verifierCode);

            if (retVal != null)
            {
                if (retVal.ExpirationDate < Clock.Now)
                {
                    retVal = null;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Take a request token, establish a verifier, and store its authorization information
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <param name="currentUser">The current user to use as authorization info</param>
        /// <returns>The updated request token</returns>
        public RequestToken CreateVerifierAndAssociateUserInfo(string requestToken, AMFUserLogin currentUser)
        {
            RequestToken retVal = null;

            if (currentUser != null && !string.IsNullOrEmpty(requestToken))
            {
                retVal = this.RequestTokenRepository.GetByToken(requestToken);

                if (retVal != null && retVal.State != TokenState.AccessDenied && retVal.IsAuthorized() == false)
                {
                    retVal.DateAuthorized = DateTime.UtcNow;
                    retVal.UserName = currentUser.Email;
                    retVal.UserId = currentUser.Id;
                    retVal.VerifierCode = RequestTokenAuthorizer.GenerateVerifierCode();
                    retVal.State = TokenState.AccessGranted;
                    retVal = this.RequestTokenRepository.Save(retVal);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Take a request token, establish a verifier, and store its authorization information
        /// </summary>
        /// <param name="requestToken">The request token</param>
        /// <param name="consumer">The current consumer requesting an auto grant</param>
        /// <returns>The updated request token</returns>
        public RequestToken AutoGrantRequestToken(string requestToken, Consumer consumer)
        {
            RequestToken retVal = null;

            if (consumer != null && consumer.AutoGrant == true && !string.IsNullOrEmpty(requestToken))
            {
                retVal = this.RequestTokenRepository.GetByToken(requestToken);

                if (retVal != null && retVal.State != TokenState.AccessDenied && retVal.IsAuthorized() == false)
                {
                    retVal.DateAuthorized = DateTime.UtcNow;
                    Realm parsedRealm = retVal.Realm;

                    if (parsedRealm != null)
                    {
                        retVal.UserName = parsedRealm.DataName;
                        retVal.UserId = long.Parse(parsedRealm.DataId);
                    }

                    retVal.VerifierCode = RequestTokenAuthorizer.GenerateVerifierCode();
                    retVal.State = TokenState.AccessGranted;
                    retVal = this.RequestTokenRepository.Save(retVal);
                }
                else
                {
                    retVal = null;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Validates the signature of a request
        /// </summary>
        /// <param name="requestContext">The current request context</param>
        /// <param name="oauthToken">The access token associated with the signature</param>
        /// <param name="signatureMethod">The method of signature</param>
        /// <param name="signatureBase">The base string of the signature</param>
        /// <returns>The current user associated with the Access token</returns>
        public ValidatedToken ValidateSignature(IOAuthContext requestContext, string oauthToken, string signatureMethod, string signatureBase)
        {
            ValidatedToken retVal = null;

            AccessToken targetToken = this.GetAccessToken(oauthToken);

            if (targetToken != null && targetToken.IsExpired == false && requestContext != null)
            {
                Consumer targetConsumer = this.ConsumerRepository.GetByConsumerKey(targetToken.ConsumerKey);

                if (targetConsumer != null)
                {
                    retVal = new ValidatedToken();

                    requestContext.ConsumerKey = targetConsumer.ConsumerKey;
                    requestContext.TokenSecret = targetToken.Secret;
                    requestContext.Token = oauthToken;
                    requestContext.SignatureMethod = signatureMethod;

                    OAuthContextSigner signer = new OAuthContextSigner();
                    SigningContext signingContext = new SigningContext();
                    signingContext.ConsumerSecret = targetConsumer.ConsumerSecret;
                    signingContext.SignatureBase = signatureBase;

                    // use context.ConsumerKey to fetch information required for signature validation for this consumer.
                    if (signer.ValidateSignature(requestContext, signingContext))
                    {
                        retVal.User = new User();
                        retVal.User.Email = targetToken.UserName;
                        retVal.User.Id = targetToken.UserId;
                        retVal.OAuthToken = targetToken;
                        retVal.Realm = targetToken.Realm;
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the requst tokens associated with a user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>A list of request tokens</returns>
        public IList<RequestToken> GetByUser(AMFUserLogin user, DateTime startDate, DateTime endDate)
        {
            IList<RequestToken> retVal = new List<RequestToken>();

            if (user != null)
            {
                if (user.Id > 0)
                {
                    retVal = this.RequestTokenRepository.GetByUserId(user.Id, startDate, endDate);
                }
                else
                {
                    retVal = this.RequestTokenRepository.GetByUserName(user.Email, startDate, endDate);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the requst tokens associated with a consumer key.
        /// </summary>
        /// <param name="consumerKey">The consumer key</param>
        /// <returns>A list of request tokens</returns>
        public IList<RequestToken> GetByConsumerKey(string consumerKey, DateTime startDate, DateTime endDate)
        {
            IList<RequestToken> retVal = new List<RequestToken>();

            if (!string.IsNullOrEmpty(consumerKey))
            {
                retVal = this.RequestTokenRepository.GetByConsumerKey(consumerKey, startDate, endDate);
            }

            return retVal;
        }
    }
}