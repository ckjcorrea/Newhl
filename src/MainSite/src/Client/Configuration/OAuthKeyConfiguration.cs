using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.OAuth.Client.Configuration
{
    /// <summary>
    /// A class to simplify putting the consumer key and secret into a configuration file.
    /// </summary>
    public class OAuthKeyConfiguration : ConfigurationSection
    {
        private const string ConsumerKeySetting = "ConsumerKey";
        private const string ConsumerSecretSetting = "ConsumerSecret";

        /// <summary>
        /// The default app.config configuration section
        /// </summary>
        private static readonly string DefaultSection = "AlwaysMoveForward/OAuthKeys";

        /// <summary>
        /// Gets the instance of the configuration, based on a default section
        /// </summary>
        /// <returns>OAuthConfiguration Configuration</returns>
        public static OAuthKeyConfiguration GetInstance()
        {
            return OAuthKeyConfiguration.GetInstance(DefaultSection);
        }

        /// <summary>
        /// Gets the instance of the configuration, based on a config section parameter
        /// </summary>
        /// <param name="configurationSection">The configuration section</param>
        /// <returns>OAuthConfiguration Configuration</returns>
        public static OAuthKeyConfiguration GetInstance(string configurationSection)
        {
            return (OAuthKeyConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
        }

        /// <summary>
        /// Gets or sets the Request Token URI string found in the config file.
        /// </summary>
        [ConfigurationProperty(ConsumerKeySetting, IsRequired = true)]
        public string ConsumerKey
        {
            get { return (string)this[ConsumerKeySetting]; }
            set { this[ConsumerKeySetting] = value; }
        }

        /// <summary>
        /// Gets or sets the Access Token URI string found in the config file.
        /// </summary>
        [ConfigurationProperty(ConsumerSecretSetting, IsRequired = true)]
        public string ConsumerSecret
        {
            get { return (string)this[ConsumerSecretSetting]; }
            set { this[ConsumerSecretSetting] = value; }
        }      
    }
}
