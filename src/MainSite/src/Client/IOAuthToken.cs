namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// A interface representing an OAuth token and its secret
    /// </summary>
    public interface IOAuthToken
    {
        /// <summary>
        /// Gets the token
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Gets the token's secret
        /// </summary>
        string Secret { get;  }
    }
}
