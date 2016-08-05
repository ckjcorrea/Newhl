using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Client.DevDefined
{
 /// <summary>
    /// OAuth implementation for Request validate contract.
    /// </summary>
    public class OAuthClient : OAuthClientBase
    {
        public static OAuthClientBase CreateClient(string consumerKey, string consumerSecret, IOAuthEndpoints oauthEndpoints)
        {
            return new OAuthClient(consumerKey, consumerSecret, oauthEndpoints);
        }

        /// <summary>
        /// Initializes a new instance of the this class.
        /// </summary>
        /// <param name="consumerKey">An OAuth consumer key/api key</param>
        /// <param name="consumerSecret">The secret for the consumer key/api key</param>
        /// <param name="oauthEndpoints">The OAuth server endoints</param>
        public OAuthClient(string consumerKey, string consumerSecret, IOAuthEndpoints oauthEndpoints)
            : base(consumerKey, consumerSecret, oauthEndpoints) 
        {
            this.AdditionalParameters = new NameValueCollection();
        }

        /// <summary>
        /// Gets or sets the additional parameters.
        /// </summary>
        public NameValueCollection AdditionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public AsymmetricAlgorithm Key { get; set; }

        /// <summary>
        /// Authorizes the specified request.
        /// </summary>
        /// <param name="webRequest">The request.</param>
        /// <param name="requestBody">The requestBody if form encoded parameters.</param>
        /// <param name="accessToken">The access token to include in the request</param>
        /// <param name="accessTokenSecret">The access token secret to sign the request with</param>
        public WebRequest Authorize(WebRequest webRequest, string requestBody, string accessToken, string accessTokenSecret)
        {
            IOAuthSession withConsumerContext = OAuthClient.CreateOAuthSessionWithConsumerContext(this.CreateConsumerContext(true, null), this.OAuthEndpoints);
            withConsumerContext.AccessToken = this.CreateAccessToken(accessToken, accessTokenSecret);
            string headerForRequest = this.GetOAuthHeaderForRequest(withConsumerContext, webRequest);
            ((NameValueCollection)webRequest.Headers).Add(OAuthClient.AuthorizationHeader, headerForRequest);

            return webRequest;
        }

        /// <summary>
        /// call out to the oauth server and get a request token for a given realm
        /// </summary>
        /// <param name="realm">The realm being requested</param>
        /// <param name="callbackUrl">The callback url</param>
        /// <returns>A request token and secret</returns>
        public override IOAuthToken GetRequestToken(Realm realm, string callbackUrl)
        {
            IOAuthSession oauthSession = OAuthClient.CreateOAuthSessionWithConsumerContext(this.CreateConsumerContext(true, realm), this.OAuthEndpoints, callbackUrl);
            IToken devDefinedRequestToken = oauthSession.GetRequestToken();

            return new RequestToken() { Token = devDefinedRequestToken.Token, Secret = devDefinedRequestToken.TokenSecret };
        }

        /// <summary>
        /// Exchange a request token and its verifier code for an access token
        /// </summary>y
        /// <param name="requestToken">The request token</param>
        /// <param name="verificationCode">The verification code</param>
        /// <returns>An acces token and its secret</returns>
        public override IOAuthToken ExchangeRequestTokenForAccessToken(IOAuthToken requestToken, string verificationCode)
        {
            RequestToken devDefinedRequestToken = new RequestToken() { Token = requestToken.Token, Secret = requestToken.Secret };

            IOAuthSession oauthSession = OAuthClient.CreateOAuthSessionWithConsumerContext(this.CreateConsumerContext(true), this.OAuthEndpoints);
            IToken devDefinedAccessToken = oauthSession.ExchangeRequestTokenForAccessToken(devDefinedRequestToken, verificationCode);

            return new AccessToken() { Token = devDefinedAccessToken.Token, Secret = devDefinedAccessToken.TokenSecret };
        }

        /// <summary>
        /// Create an OAuth session form a given consumer context, and predefined oauth server endpoints
        /// </summary>
        /// <param name="consumerContext">The consumer context (consumer key, tokens, etc)</param>
        /// <param name="oauthEndpoints">The oauth server endpoints for request, access, and authoriz url</param>
        /// <returns></returns>
        private static IOAuthSession CreateOAuthSessionWithConsumerContext(IOAuthConsumerContext consumerContext, IOAuthEndpoints oauthEndpoints)
        {
            IOAuthSession retVal = null;

            if (oauthEndpoints != null)
            {
                retVal = (IOAuthSession)new OAuthSession(consumerContext, oauthEndpoints.GetFullRequestTokenUri(), oauthEndpoints.GetFullAuthorizationUri(), oauthEndpoints.GetFullAccessTokenUri());
            }

            return retVal;
        }

        /// <summary>
        /// Create an OAuth session form a given consumer context, and predefined oauth server endpoints
        /// </summary>
        /// <param name="consumerContext">The consumer context (consumer key, tokens, etc)</param>
        /// <param name="oauthEndpoints">The oauth server endpoints for request, access, and authoriz url</param>
        /// <param name="callbackUrl">A specific callback url for post authorization</param>
        /// <returns></returns>
        private static IOAuthSession CreateOAuthSessionWithConsumerContext(IOAuthConsumerContext consumerContext, IOAuthEndpoints oauthEndpoints, string callbackUrl)
        {
            IOAuthSession retVal = null;

            if (oauthEndpoints != null)
            {
                retVal = (IOAuthSession)new OAuthSession(consumerContext, oauthEndpoints.GetFullRequestTokenUri(), oauthEndpoints.GetFullAuthorizationUri(), oauthEndpoints.GetFullAccessTokenUri(), callbackUrl);
            }

            return retVal;
        }

        /// <summary>
        /// Generate an OAuth header for a given web reqeust
        /// </summary>
        /// <param name="oauthSession">The current oauth session (the consumerkey, tokens, etc)</param>
        /// <param name="webRequest">The given web request</param>
        /// <returns>The string to put into the authorization header</returns>
        private string GetOAuthHeaderForRequest(IOAuthSession oauthSession, WebRequest webRequest)
        {
            IConsumerRequest cconsumerRequest = ConsumerRequestExtensions.ForUri(ConsumerRequestExtensions.ForMethod(oauthSession.Request(), webRequest.Method), webRequest.RequestUri);

            if (webRequest.Headers.Count > 0)
            {
                ConsumerRequestExtensions.AlterContext(cconsumerRequest, (Action<IOAuthContext>)(context => context.Headers = (NameValueCollection)webRequest.Headers));
            }

            if (this.AdditionalParameters != null)
            {
                cconsumerRequest.Context.AuthorizationHeaderParameters.Add(this.AdditionalParameters);
            }

            return cconsumerRequest.SignWithToken().Context.GenerateOAuthParametersForHeader();
        }

        /// <summary>
        /// Creates the consumer context.
        /// </summary>
        /// <param name="isHeaderToBeAdded">if set to <c>true</c> header will be added to the request.</param>
        /// <returns>
        /// Returns IOAuthConsumerContext object.
        /// </returns>
        private IOAuthConsumerContext CreateConsumerContext(bool isHeaderToBeAdded)
        {
            return this.CreateConsumerContext(isHeaderToBeAdded, null);
        }

        /// <summary>
        /// Creates the consumer context.
        /// </summary>
        /// <param name="isHeaderToBeAdded">if set to <c>true</c> header will be added to the request.</param>
        /// <returns>
        /// Returns IOAuthConsumerContext object.
        /// </returns>
        private IOAuthConsumerContext CreateConsumerContext(bool isHeaderToBeAdded, Realm realm)
        {
            OAuthConsumerContext oauthConsumerContext = new OAuthConsumerContext();
            oauthConsumerContext.ConsumerKey = this.ConsumerKey;
            oauthConsumerContext.ConsumerSecret = this.ConsumerSecret;

            if (realm != null)
            {
                oauthConsumerContext.Realm = realm.ToString();
            }

            oauthConsumerContext.SignatureMethod = this.OAuthSignatureMethod;

            IOAuthConsumerContext authConsumerContext = (IOAuthConsumerContext)oauthConsumerContext;

            if (this.Key != null)
            {
                authConsumerContext.Key = this.Key;
            }

            if (isHeaderToBeAdded)
            {
                authConsumerContext.UseHeaderForOAuthParameters = true;
            }

            return authConsumerContext;
        }

        /// <summary>
        /// Creates the access token.
        /// </summary>
        /// <returns>
        /// returns OAuth token.
        /// </returns>
        private IToken CreateAccessToken(string accessToken, string accessTokenSecret)
        {
            TokenBase tokenBase = new TokenBase();
            tokenBase.Token = accessToken;
            tokenBase.ConsumerKey = this.ConsumerKey;
            tokenBase.TokenSecret = accessTokenSecret;
            return (IToken)tokenBase;
        }

        /// <summary>
        /// Execute an authorized web request
        /// </summary>
        /// <param name="requestUri">The request endpoint</param>
        /// <param name="contentType">The content type of the request</param>
        /// <param name="requestBody">The requestBody</param>
        /// <param name="oauthToken">The oauth token/secret to sign with</param>
        /// <returns></returns>
        public string ExecuteAuthorizedRequest(string requestUri, string contentType, string requestBody, IOAuthToken oauthToken)
        {
            string retVal = string.Empty;

            WebRequest request = HttpWebRequest.Create(requestUri);
            request.ContentType = contentType;
            request = this.Authorize(request, string.Empty, oauthToken.Token, oauthToken.Secret);

            try
            {
                // No more ProtocolViolationException!
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);

                    // Read the content. 
                    retVal = reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                retVal = we.Message;
            }

            return retVal;
        }

         /// <summary>
        /// Authorize a request token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        /// <returns>An object containing a response elements (verifier code and granted)</returns>
        private TokenVerification InlineAuthorizeRequestToken(IOAuthToken oauthToken)
        {
            TokenVerification retVal = null;

            if (oauthToken != null)
            {
                string requestUri = this.OAuthEndpoints.GetFullAuthorizationUri() + "?" + Constants.TokenParameter + "=" + oauthToken.Token;
                WebRequest webRequest = HttpWebRequest.Create(requestUri);

                string responseContent = string.Empty;

                try
                {
                    // No more ProtocolViolationException!
                    using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);

                        // Read the content. 
                        responseContent = reader.ReadToEnd();
                    }
                }
                catch (WebException we)
                {
                    responseContent = we.Message;
                }

                var qs = HttpUtility.ParseQueryString(responseContent);
                string verifierCode = qs[Constants.VerifierCodeParameter];
                string grantedAccessParameter = qs[Constants.GrantedAccessParameter];

                bool grantedAccess = false;
                if (!string.IsNullOrWhiteSpace(grantedAccessParameter))
                {
                    grantedAccess = bool.Parse(grantedAccessParameter);
                }

                if (!string.IsNullOrEmpty(verifierCode))
                {
                    retVal = new TokenVerification();
                    retVal.VerifierCode = verifierCode;
                    retVal.Granted = grantedAccess;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Get an access token without using a callback url.  This only works for the auto grant path.(which is only enabled for Vistaprint users
        /// </summary>
        /// <param name="realm"></param>
        /// <returns>An access token and secret</returns>
        public IOAuthToken GetInlineAccessToken(Realm realm)
        {
            DefaultOAuthToken retVal = null;

            IOAuthToken requestToken = this.GetRequestToken(realm, Constants.InlineCallback);
            
            if (requestToken != null)
            {
                DefaultOAuthToken testToken = new DefaultOAuthToken() { Token = requestToken.Token, Secret = requestToken.Secret };

                if (testToken.IsValid() == true)
                {
                    TokenVerification tokenVerification = this.InlineAuthorizeRequestToken(requestToken);

                    if (tokenVerification != null)
                    {
                        IOAuthToken accessToken = this.ExchangeRequestTokenForAccessToken(requestToken, tokenVerification.VerifierCode);

                        if (accessToken != null)
                        {
                            retVal = new DefaultOAuthToken() { Token = accessToken.Token, Secret = accessToken.Secret };
                        }
                    }
                }
            }

            return retVal;
        }
    } 
}
