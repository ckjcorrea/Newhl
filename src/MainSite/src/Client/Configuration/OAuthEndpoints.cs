namespace AlwaysMoveForward.OAuth.Client.Configuration
{
    /// <summary>
    /// Immutable class to describe the OAuth functionality endpoints.
    /// </summary>
    public class OAuthEndpoints : IOAuthEndpoints
    {
        /// <summary>
        /// Gets or sets the URI for the service.
        /// </summary>
        public string ServiceUri { get; private set; }

        /// <summary>
        /// Gets or sets the path for the request token functionality.
        /// </summary>
        public string RequestTokenUri { get; private set; }

        /// <summary>
        /// Gets or sets the path for the access token functionality.
        /// </summary>
        public string AccessTokenUri { get; private set; }

        /// <summary>
        /// Gets or sets the path for the authorization functionality.
        /// </summary>
        public string AuthorizationUri { get; private set; }
     
        /// <summary>
        /// Create an OAuth endpoint 
        /// </summary>
        /// <param name="serviceUri">The root uri of the oauth server</param>
        /// <param name="requestTokenUri">The uri extension to get a request token</param>
        /// <param name="accessTokenuri">The uri extension to get an access token</param>
        /// <param name="authorizationuri">The uri extension to authorize the request token</param>
        public OAuthEndpoints(string serviceUri, string requestTokenUri, string accessTokenuri, string authorizationuri)
        {
            this.ServiceUri = serviceUri;
            this.RequestTokenUri = requestTokenUri;
            this.AccessTokenUri = accessTokenuri;
            this.AuthorizationUri = authorizationuri;
        }

        /// <summary>
        /// Get the combined Service Uri and RequestToken Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        public string GetFullRequestTokenUri()
        {
            return this.ServiceUri + this.RequestTokenUri;
        }

        /// <summary>
        /// Get the combined Service Uri and Authorization Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        public string GetFullAuthorizationUri()
        {
            return this.ServiceUri + this.AuthorizationUri;
        }

        /// <summary>
        /// Get the combined Service Uri and AccessToken Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        public string GetFullAccessTokenUri()
        {
            return this.ServiceUri + this.AccessTokenUri;
        }
    }
}
