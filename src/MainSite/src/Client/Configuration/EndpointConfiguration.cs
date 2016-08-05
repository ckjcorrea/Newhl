using System.Configuration;

namespace AlwaysMoveForward.OAuth.Client.Configuration
{
    /// <summary>
    /// A class to simplify placing the OAuth URIs into a configuration file.
    /// </summary>
    public class EndpointConfiguration : ConfigurationSection, IOAuthEndpoints
    {
        /// <summary>
        /// The Request Token uri configuration setting.
        /// </summary>
        public const string ServiceUriSetting = "ServiceUri";

        /// <summary>
        /// The Request Token uri configuration setting.
        /// </summary>
        public const string RequestTokenUriSetting = "RequestTokenUri";

        /// <summary>
        /// The Access Token uri configuration setting.
        /// </summary>
        public const string AccessTokenUriSetting = "AccessTokenUri";

        /// <summary>
        /// The Authorization uri configuration setting.
        /// </summary>
        public const string AuthorizationUriSetting = "AuthorizationUri";

        /// <summary>
        /// The default app.config configuration section
        /// </summary>
        private static readonly string DefaultSection = "AlwaysMoveForward/OAuthEndpoints";

        /// <summary>
        /// Gets the instance of the configuration, based on a default section
        /// </summary>
        /// <returns>Returns an instance of this class</returns>
        public static EndpointConfiguration GetInstance()
        {
            return EndpointConfiguration.GetInstance(DefaultSection);
        }

        /// <summary>
        /// Gets the instance of the configuration, based on a config section parameter
        /// </summary>
        /// <param name="configurationSection">The configuration section</param>
        /// <returns>Returns an instance of this class.</returns>
        public static EndpointConfiguration GetInstance(string configurationSection)
        {
            return (EndpointConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
        }

        /// <summary>
        /// Gets or sets the Request Token URI string found in the config file.
        /// </summary>
        [ConfigurationProperty(ServiceUriSetting, IsRequired = true)]
        public string ServiceUri
        {
            get { return (string)this[ServiceUriSetting]; }
            set { this[ServiceUriSetting] = value; }
        }

        /// <summary>
        /// Gets or sets the Request Token URI string found in the config file.
        /// </summary>
        [ConfigurationProperty(RequestTokenUriSetting, IsRequired = true)]
        public string RequestTokenUri
        {
            get { return (string)this[RequestTokenUriSetting]; }
            set { this[RequestTokenUriSetting] = value; }
        }

        /// <summary>
        /// Gets or sets the Access Token URI string found in the config file.
        /// </summary>
        [ConfigurationProperty(AccessTokenUriSetting, IsRequired = true)]
        public string AccessTokenUri
        {
            get { return (string)this[AccessTokenUriSetting]; }
            set { this[AccessTokenUriSetting] = value; }
        }

        /// <summary>
        /// Gets or sets the Authorization URI string found in the config file.
        /// </summary>
        [ConfigurationProperty(AuthorizationUriSetting, IsRequired = true)]
        public string AuthorizationUri
        {
            get { return (string)this[AuthorizationUriSetting]; }
            set { this[AuthorizationUriSetting] = value; }
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
