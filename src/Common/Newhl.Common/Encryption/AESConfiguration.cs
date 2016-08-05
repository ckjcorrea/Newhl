using System.Configuration;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// AES Encryption Configuration
    /// </summary>
    public class AESConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Default Section Constant
        /// </summary>
        public const string DEFAULT_SECTION = "Newhl/AESEncryptionConfiguration";

        /// <summary>
        /// Encryption Key Constant
        /// </summary>
        public const string ENCRYPTION_KEY = "EncryptionKey";

        /// <summary>
        /// Salt Constant
        /// </summary>
        public const string SALT = "Salt";

        private static AESConfiguration configurationInstance;

        /// <summary>
        /// Gets the instance of the configuration, based on a default section
        /// </summary>
        /// <returns>AESEncryption Configuration</returns>
        public static AESConfiguration GetInstance()
        {
            return AESConfiguration.GetInstance(DEFAULT_SECTION);
        }

        /// <summary>
        /// Gets the instance of the configuration, based on a config section parameter
        /// </summary>
        /// <param name="configurationSection">The configuration section</param>
        /// <returns>AESEncryption Configuration</returns>
        public static AESConfiguration GetInstance(string configurationSection)
        {
            if (configurationInstance == null)
            {
                configurationInstance = (AESConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
            }

            return configurationInstance;
        }      
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AESConfiguration() { }

        /// <summary>
        /// Gets or sets the encryption key
        /// </summary>
        [ConfigurationProperty(ENCRYPTION_KEY, IsRequired = false)]
        public string EncryptionKey
        {
            get { return (string)this[ENCRYPTION_KEY]; }
            set { this[ENCRYPTION_KEY] = value; }
        }
        
        /// <summary>
        /// Gets or sets the salt
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(SALT, IsRequired = false)]
        public string Salt
        {
            get { return (string)this[SALT]; }
            set { this[SALT] = value; }
        }
    }
}