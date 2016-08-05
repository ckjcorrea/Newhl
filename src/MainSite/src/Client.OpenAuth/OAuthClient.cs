using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using DotNetOpenAuth.OpenId.Extensions.OAuth;
using AlwaysMoveForward.OAuth.Contracts;

namespace AlwaysMoveForward.OAuth.Client.OpenAuth
{
    public class OAuthClient
    {
        /// <summary>
        /// The constructor that takes the IP hostname of the remote web service
        /// </summary>
        /// <param name="oauthEndpoints">The endpoints to use to get a tokens and authorize</param>
        /// <param name="tokenManager">The storage mechanism to use between OAuth calls</param>
        public OAuthClient(IOAuthEndpoints oauthEndpoints, IConsumerTokenManager tokenManager)
        {
            this.OAuthEndpoints = oauthEndpoints;
            this.TokenManager = tokenManager;
        }

        /// <summary>
        /// Gets or sets the consumer key.
        /// </summary>
        protected string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        protected string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets the endpoints to use for the OAuth communication
        /// </summary>
        public IOAuthEndpoints OAuthEndpoints { get; private set; }

        /// <summary>
        /// Gets a IConsumerTokenManager instance
        /// </summary>
        protected IConsumerTokenManager TokenManager { get; private set; }

        /// <summary>
        /// Create a DotNet OpenAuth consumer
        /// </summary>
        /// <param name="tokenManager">The storage for tokens between calls</param>
        /// <returns>A web consumer initialized to the OAuth endpoints</returns>
        public WebConsumer CreateConsumer(IConsumerTokenManager tokenManager)
        {
            WebConsumer consumer = null;

            if (tokenManager != null)
            {
                consumer = new WebConsumer(
                 new ServiceProviderDescription
                 {
                     ProtocolVersion = DotNetOpenAuth.OAuth.ProtocolVersion.V10a,
                     RequestTokenEndpoint = new DotNetOpenAuth.Messaging.MessageReceivingEndpoint(
                         this.OAuthEndpoints.GetFullRequestTokenUri(), 
                         DotNetOpenAuth.Messaging.HttpDeliveryMethods.GetRequest | DotNetOpenAuth.Messaging.HttpDeliveryMethods.AuthorizationHeaderRequest),
                     UserAuthorizationEndpoint = new DotNetOpenAuth.Messaging.MessageReceivingEndpoint(
                         this.OAuthEndpoints.GetFullAuthorizationUri(), 
                         DotNetOpenAuth.Messaging.HttpDeliveryMethods.GetRequest | DotNetOpenAuth.Messaging.HttpDeliveryMethods.AuthorizationHeaderRequest),
                     AccessTokenEndpoint = new DotNetOpenAuth.Messaging.MessageReceivingEndpoint(
                         this.OAuthEndpoints.GetFullAccessTokenUri(), 
                         DotNetOpenAuth.Messaging.HttpDeliveryMethods.GetRequest | DotNetOpenAuth.Messaging.HttpDeliveryMethods.AuthorizationHeaderRequest),
                     TamperProtectionElements = new DotNetOpenAuth.Messaging.ITamperProtectionChannelBindingElement[] { new DotNetOpenAuth.OAuth.ChannelElements.HmacSha1SigningBindingElement() }
                 }, 
                 tokenManager);
            }

            return consumer;
        }

        /// <summary>
        /// DotNetOpenAuth gets the requst token and calls the authorization url automatically
        /// </summary>
        /// <param name="realm">The oauth realm</param>
        /// <param name="callbackUrl">The callback url</param>
        /// <returns>Null always because it automatically calls the authorization url</returns>
        public IOAuthToken GetRequestToken(Realm realm, string callbackUrl)
        {
            return this.GetRequestToken(realm, callbackUrl, this.TokenManager);
        }

        /// <summary>
        /// Add an option to pass in a token manager
        /// </summary>
        /// <param name="realm">The oauth realm</param>
        /// <param name="callbackUrl">The oauth authorization callback url</param>
        /// <param name="consumerTokenManager">An IConsumerTokenManager instance</param>
        /// <returns>Null always because DotNetOpenAuth automatically calls the authoriztion Url</returns>
        public IOAuthToken GetRequestToken(Realm realm, string callbackUrl, IConsumerTokenManager consumerTokenManager)
        {
            WebConsumer oauthClient = this.CreateConsumer(consumerTokenManager);

            if (oauthClient != null)
            {
                // TBD, this isnt' the right way to pass realm, this isn't putting it into the oauth header.
                IDictionary<string, string> additionalParameters = new Dictionary<string, string>();
                additionalParameters.Add("realm", realm.ToString());

                DotNetOpenAuth.OAuth.Messages.UserAuthorizationRequest requestTokenRequest = oauthClient.PrepareRequestUserAuthorization(new Uri(callbackUrl), additionalParameters, new Dictionary<string, string>());
                oauthClient.Channel.Send(requestTokenRequest);
            }

            return null;
        }

        /// <summary>
        /// Get an access token, using the request token DotNetOpen auth pulls from the request and finds in the token manager
        /// </summary>
        /// <param name="verificationCode">The verification code</param>
        /// <returns>The access token</returns>
        public IOAuthToken ExchangeRequestTokenForAccessToken(string verificationCode)
        {
            return this.ExchangeRequestTokenForAccessToken(verificationCode, this.TokenManager);
        }

        /// <summary>
        /// Get an access token, using the request token DotNetOpen auth pulls from the request and finds in the token manager
        /// </summary>
        /// <param name="verificationCode">The verification code</param>
        /// <param name="tokenManager">The token manager used for storage</param>
        /// <returns>The access token</returns>
        public IOAuthToken ExchangeRequestTokenForAccessToken(string verificationCode, IConsumerTokenManager tokenManager)
        {
            DefaultOAuthToken retVal = null;
            WebConsumer oauthClient = this.CreateConsumer(tokenManager);

            if (oauthClient != null)
            {
                DotNetOpenAuth.OAuth.Messages.AuthorizedTokenResponse requestTokenResponse = oauthClient.ProcessUserAuthorization();

                if (requestTokenResponse != null)
                {
                    retVal = new DefaultOAuthToken();
                    retVal.Token = requestTokenResponse.AccessToken;
                    retVal.Secret = (requestTokenResponse as DotNetOpenAuth.OAuth.Messages.ITokenSecretContainingMessage).TokenSecret;
                }
            }

            return retVal;
        }
    }
}
