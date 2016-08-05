using System.Diagnostics;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;
using AlwaysMoveForward.OAuth.Client;

namespace AlwaysMoveForward.OAuth.Client.RestSharp
{
    /// <summary>
    /// Puts together endpoints and parameters to make REST calls to service URIs provided.
    /// </summary>
    public class OAuthClient : OAuthClientBase
    {                  
        /// <summary>
        /// The constructor that takes the IP hostname of the remote web service
        /// </summary>
        /// <param name="serviceAddress">The ip address of the host service</param>
        /// <param name="consumerKey">The consumer key to use with the oauth</param>
        /// <param name="consumerSecret">The consumer secret to use with the oauth</param>
        /// <param name="oauthEndpoints">The endpoints to use to get a tokens and authorize</param>
        public OAuthClient(string serviceAddress, string consumerKey, string consumerSecret, IOAuthEndpoints oauthEndpoints) : base(consumerKey, consumerSecret, oauthEndpoints)
        {
            this.ServiceAddress = serviceAddress;
        }

        /// <summary>
        /// Gets the hostname of the remote web service
        /// </summary>
        public string ServiceAddress { get; private set; }

        /// <summary>
        /// Create an instance of a RestClient that makes its calls against the remote hostname and uses OAuth for authentication
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="accessTokenSecret">The oauth secret used to sign the request</param>
        /// <returns>An instance of a rest client configured to use OAuth</returns>
        protected RestClient GetRestClient(string accessToken, string accessTokenSecret)
        {
            RestClient restClient = new RestClient(this.ServiceAddress);
            restClient.Authenticator = OAuth1Authenticator.ForProtectedResource(this.ConsumerKey, this.ConsumerSecret, accessToken, accessTokenSecret);
            return restClient;
        }

        /// <summary>
        /// Get a requset token from an oauth server
        /// </summary>
        /// <param name="callbackUrl">The url to use to callback to after authorization</param>
        /// <param name="realm">The realm requesting access to</param>
        /// <returns>A token and secret</returns>
        public override IOAuthToken GetRequestToken(Realm realm, string callbackUrl)
        {
            DefaultOAuthToken retVal = null;

            if (realm != null && !string.IsNullOrEmpty(callbackUrl))
            {
                RestClient restClient = new RestClient(this.OAuthEndpoints.ServiceUri);
                OAuth1Authenticator authenticator = OAuth1Authenticator.ForRequestToken(this.ConsumerKey, this.ConsumerSecret, callbackUrl);
                authenticator.Realm = realm.ToString();
                restClient.Authenticator = authenticator;
                RestRequest request = new RestRequest(this.OAuthEndpoints.RequestTokenUri, Method.GET);
                var response = restClient.Execute(request);

                var qs = HttpUtility.ParseQueryString(response.Content);
                string token = qs[Constants.TokenParameter];
                string tokenSecret = qs[Constants.TokenSecretParameter];

                if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(tokenSecret))
                {
                    retVal = new DefaultOAuthToken();
                    retVal.Token = token;
                    retVal.Secret = tokenSecret;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Authorize a request token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        public void Authorize(IOAuthToken oauthToken)
        {
            if (oauthToken != null)
            {
                RestClient restClient = new RestClient(this.OAuthEndpoints.ServiceUri);
                RestRequest request = new RestRequest(this.OAuthEndpoints.AuthorizationUri);
                request.AddParameter(Constants.TokenParameter, oauthToken.Token);
                var url = restClient.BuildUri(request).ToString();
                Process.Start(url);
            }
        }

        /// <summary>
        /// Authorize a request token
        /// </summary>
        /// <param name="oauthToken">The request token</param>
        /// <returns>An object containing a response elements (verifier code and granted)</returns>
        private TokenVerification InlineAuthorize(IOAuthToken oauthToken)
        {
            TokenVerification retVal = null;

            if (oauthToken != null)
            {
                RestClient restClient = new RestClient(this.OAuthEndpoints.ServiceUri);
                RestRequest request = new RestRequest(this.OAuthEndpoints.AuthorizationUri, Method.GET);
                request.AddParameter(Constants.TokenParameter, oauthToken.Token);
                var response = restClient.Execute(request);

                var qs = HttpUtility.ParseQueryString(response.Content);
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
        /// Exchange an request token for an access token
        /// </summary>
        /// <param name="token">The request token</param>
        /// <param name="verifierCode">The verifier code</param>
        /// <returns>An access token and secret</returns>
        public override IOAuthToken ExchangeRequestTokenForAccessToken(IOAuthToken token, string verifierCode)
        {
            DefaultOAuthToken retVal = null;

            if (token != null && !string.IsNullOrEmpty(verifierCode))
            {
                RestClient restClient = new RestClient(this.OAuthEndpoints.ServiceUri);
                RestRequest request = new RestRequest(this.OAuthEndpoints.AccessTokenUri);
                restClient.Authenticator = OAuth1Authenticator.ForAccessToken(this.ConsumerKey, this.ConsumerSecret, token.Token, token.Secret, verifierCode);
                var response = restClient.Execute(request);

                var qs = HttpUtility.ParseQueryString(response.Content);
                retVal = new DefaultOAuthToken();
                retVal.Token = qs[Constants.TokenParameter];
                retVal.Secret = qs[Constants.TokenSecretParameter];
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

            DefaultOAuthToken requestToken = this.GetRequestToken(realm, Constants.InlineCallback) as DefaultOAuthToken;

            if (requestToken != null && requestToken.IsValid())
            {
                TokenVerification tokenVerification = this.InlineAuthorize(requestToken);

                if (tokenVerification != null)
                {
                    retVal = this.ExchangeRequestTokenForAccessToken(requestToken, tokenVerification.VerifierCode) as DefaultOAuthToken;
                }
            }

            return retVal;
        }

        public string ExecuteAuthorizedRequest(string targetEndpoint, string targetAction, IOAuthToken oauthToken)
        {
            string retVal = string.Empty;

            RestClient restClient = new RestClient(targetEndpoint);
            restClient.Authenticator = OAuth1Authenticator.ForProtectedResource(this.ConsumerKey, this.ConsumerSecret, oauthToken.Token, oauthToken.Secret);
            
            string[] parameterItems = null;

            if (targetAction.Contains("?"))
            {
                string[] actionElements = targetAction.Split('?');

                parameterItems = actionElements[1].Split('&');

                targetAction = actionElements[0];
            }

            RestRequest request = new RestRequest(targetAction, Method.GET);
            request.RequestFormat = DataFormat.Json;

            if (parameterItems != null)
            {
                for (int i = 0; i < parameterItems.Length; i++)
                {
                    string[] parameterElements = parameterItems[i].Split('=');

                    request.AddParameter(parameterElements[0], parameterElements[1]);
                }
            }

            IRestResponse response = restClient.Execute(request);

            if (response != null)
            {
                retVal = response.Content;
            }

            return retVal;
        }
    }
}
