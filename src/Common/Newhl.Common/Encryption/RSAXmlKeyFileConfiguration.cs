using System.Configuration;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// AES Encryption Configuration
    /// </summary>
    public class RSAXmlKeyFileConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Default Section Constant
        /// </summary>
        public const string DEFAULT_SECTION = "Newhl/RSAXmlKeyFileConfiguration";

        /// <summary>
        /// The path to the public key file
        /// </summary>
        public const string PublicKeyFileSetting = "PublicKeyFile";

        /// <summary>
        /// The path to the private key file
        /// </summary>
        public const string PrivateKeyFileSetting = "PrivateKeyFile";

        /// <summary>
        /// Gets the instance of the configuration, based on a default section
        /// </summary>
        /// <returns>AESEncryption Configuration</returns>
        public static RSAXmlKeyFileConfiguration GetInstance()
        {
            return RSAXmlKeyFileConfiguration.GetInstance(DEFAULT_SECTION);
        }

        /// <summary>
        /// Gets the instance of the configuration, based on a config section parameter
        /// </summary>
        /// <param name="configurationSection">The configuration section</param>
        /// <returns>AESEncryption Configuration</returns>
        public static RSAXmlKeyFileConfiguration GetInstance(string configurationSection)
        {
            return (RSAXmlKeyFileConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RSAXmlKeyFileConfiguration() { }

        /// <summary>
        /// Gets or sets the public key file path
        /// </summary>
        [ConfigurationProperty(PublicKeyFileSetting, IsRequired = false)]
        public string PublicKeyFile
        {
            get { return (string)this[PublicKeyFileSetting]; }
            set { this[PublicKeyFileSetting] = value; }
        }

        /// <summary>
        /// Gets or sets the private key file path
        /// </summary>
        [ConfigurationProperty(PrivateKeyFileSetting, IsRequired = false)]
        public string PrivateKeyFile
        {
            get { return (string)this[PrivateKeyFileSetting]; }
            set { this[PrivateKeyFileSetting] = value; }
        }
    }
}