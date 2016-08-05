namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// An interface to allow flexibility in how the endpoints for an OAuth communication are defined.
    /// </summary>
    public interface IOAuthEndpoints
    {
        /// <summary>
        /// Gets the service root uri
        /// </summary>
        string ServiceUri { get;  }

        /// <summary>
        /// Gets the uri to retrieve a request token from a consumer key/secret
        /// </summary>
        string RequestTokenUri { get; }

        /// <summary>
        /// Gets the uri to retrieve an access token from a a request token/secret
        /// </summary>
        string AccessTokenUri { get; }

        /// <summary>
        /// Gets the uri to provide an authorization ui for the user to login with.
        /// </summary>
        string AuthorizationUri { get; }

        /// <summary>
        /// Get the combined Service Uri and RequestToken Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        string GetFullRequestTokenUri();
        
        /// <summary>
        /// Get the combined Service Uri and Authorization Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        string GetFullAuthorizationUri();

        /// <summary>
        /// Get the combined Service Uri and AccessToken Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        string GetFullAccessTokenUri();
    }
}
