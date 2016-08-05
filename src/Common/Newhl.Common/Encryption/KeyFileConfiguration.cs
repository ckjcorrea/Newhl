using System.Configuration;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// KeyFile Encryption Configuration
    /// </summary>
    public class KeyFileConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Default Section Constant
        /// </summary>
        public const string DEFAULT_SECTION = "Newhl/KeyFileEncryptionConfiguration";

        /// <summary>
        /// Key File Constant
        /// </summary>
        public const string KEY_FILE = "KeyFile";

        /// <summary>
        /// Key file password constant
        /// </summary>
        public const string KEY_FILE_PASSWORD = "KeyFilePassword";

        private static KeyFileConfiguration configurationInstance;

        /// <summary>
        /// Gets a Key File encryption configuration using the default section
        /// </summary>
        /// <returns>Key File Encryption Configuration</returns>
        public static KeyFileConfiguration GetInstance()
        {
            return KeyFileConfiguration.GetInstance(DEFAULT_SECTION);
        }

        /// <summary>
        /// Gets a key file encryption configuration using a specified configuration section
        /// </summary>
        /// <param name="configurationSection">The Configuration Section</param>
        /// <returns>Key File Encryption Configuration</returns>
        public static KeyFileConfiguration GetInstance(string configurationSection)
        {
            if (configurationInstance == null)
            {
                configurationInstance = (KeyFileConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
            }

            return configurationInstance;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public KeyFileConfiguration() { }

        /// <summary>
        /// Gets or sets the key file
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(KEY_FILE, IsRequired = false)]
        public string KeyFile
        {
            get { return (string)this[KEY_FILE]; }
            set { this[KEY_FILE] = value; }
        }

        /// <summary>
        /// Gets or sets the key file password
        /// Define the salt used to modify the password.
        /// </summary>
        [ConfigurationProperty(KEY_FILE_PASSWORD, IsRequired = false)]
        public string KeyFilePassword
        {
            get { return (string)this[KEY_FILE_PASSWORD]; }
            set { this[KEY_FILE_PASSWORD] = value; }
        }
    }
}
