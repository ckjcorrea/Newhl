namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// OAuth implementation for Request validate contract.
    /// </summary>
    public abstract class OAuthClientBase
    {        
        /// <summary>
        /// The Authorization Header constant.
        /// </summary>
        protected const string AuthorizationHeader = "Authorization";

        /// <summary>
        /// Initializes a new instance of the this class.
        /// </summary>
        /// <param name="consumerKey">An OAuth consumer key/api key</param>
        /// <param name="consumerSecret">The secret for the consumer key/api key</param>
        /// <param name="oauthEndpoints">The OAuth server endoints</param>
        protected OAuthClientBase(string consumerKey, string consumerSecret, IOAuthEndpoints oauthEndpoints)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
            this.OAuthSignatureMethod = Constants.HmacSha1SignatureMethod;
            this.OAuthEndpoints = oauthEndpoints;
        }

        /// <summary>
        /// Gets or sets the OAuth signature method.
        /// </summary>
        protected string OAuthSignatureMethod { get; set; }

        /// <summary>
        /// Gets or sets the consumer key.
        /// </summary>
        protected string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret.
        /// </summary>
        protected string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets the endpoints to use for the OAuth communication
        /// </summary>
        public IOAuthEndpoints OAuthEndpoints { get; private set; }
 
        /// <summary>
        /// Generate the url used to authorize the request token
        /// </summary>
        /// <param name="requestToken">The request token to authorize</param>
        /// <param name="callbackUrl">The callback url</param>
        /// <returns>The ful authorize url</returns>
        public string GetUserAuthorizationUrl(IOAuthToken requestToken)
        {
            return string.Format("{0}{1}?{2}={3}", this.OAuthEndpoints.ServiceUri, this.OAuthEndpoints.AuthorizationUri, Constants.TokenParameter, requestToken.Token);
        }

        /// <summary>
        /// call out to the oauth server and get a request token for a given realm
        /// </summary>
        /// <param name="realm">The realm being requested</param>
        /// <param name="callbackUrl">The callback url</param>
        /// <returns>A request token and secret</returns>
        public abstract IOAuthToken GetRequestToken(Realm realm, string callbackUrl);

        /// <summary>
        /// Exchange a request token and its verifier code for an access token
        /// </summary>y
        /// <param name="requestToken">The request token</param>
        /// <param name="verificationCode">The verification code</param>
        /// <returns>An acces token and its secret</returns>
        public abstract IOAuthToken ExchangeRequestTokenForAccessToken(IOAuthToken requestToken, string verificationCode);       

        public abstract string ExecuteAuthorizedRequest(string targetEndpoint, string targetAction, IOAuthToken oauthToken);
    } 
}
