using System.Configuration;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// Key store Encryption Configuration
    /// </summary>
    public class KeyStoreConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Default Section Constant
        /// </summary>
        public const string DEFAULT_SECTION = "Newhl/KeyStoreEncryptionConfiguration";

        /// <summary>
        /// Store Name Constant
        /// </summary>
        public const string STORE_NAME = "StoreName";

        /// <summary>
        /// Store Location Constant
        /// </summary>
        public const string STORE_LOCATION = "StoreLocation";

        /// <summary>
        /// Certificate Name Constant
        /// </summary>
        public const string CERTIFICATE_NAME = "CertificateName";

        private static KeyStoreConfiguration configurationInstance;

        /// <summary>
        /// Gets an instance of the key store encryption configuration using the default section
        /// </summary>
        /// <returns>Key Store Encryption Configuration</returns>
        public static KeyStoreConfiguration GetInstance()
        {
            return KeyStoreConfiguration.GetInstance(DEFAULT_SECTION);
        }

        /// <summary>
        /// Gets an instance of the key store encryption configuration using a specified configuration section
        /// </summary>
        /// <param name="configurationSection">Configuration Section</param>
        /// <returns>Key Store Encryption Configuration</returns>
        public static KeyStoreConfiguration GetInstance(string configurationSection)
        {
            if (configurationInstance == null)
            {
                configurationInstance = (KeyStoreConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
            }

            return configurationInstance;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public KeyStoreConfiguration() { }

        /// <summary>
        /// Gets or sets the store name
        /// </summary>
        [ConfigurationProperty(STORE_NAME, IsRequired = false)]
        public string StoreName
        {
            get { return (string)this[STORE_NAME]; }
            set { this[STORE_NAME] = value; }
        }

        /// <summary>
        /// Gets or sets the store location 
        /// </summary>
        [ConfigurationProperty(STORE_LOCATION, IsRequired = false)]
        public string StoreLocation
        {
            get { return (string)this[STORE_LOCATION]; }
            set { this[STORE_LOCATION] = value; }
        }

        /// <summary>
        /// Gets or sets the certificate name
        /// </summary>
        [ConfigurationProperty(CERTIFICATE_NAME, IsRequired = false)]
        public string CertificateName
        {
            get { return (string)this[CERTIFICATE_NAME]; }
            set { this[CERTIFICATE_NAME] = value; }
        }
    }
}
