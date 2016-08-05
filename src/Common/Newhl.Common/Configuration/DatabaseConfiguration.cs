using System.Configuration;

namespace Newhl.Common.Configuration
{
    /// <summary>
    /// A class to simplify getting the configuration settings for a database
    /// </summary>
    public class DatabaseConfiguration : Newhl.Common.Encryption.EncryptedConfigurationSection
    {
        /// <summary>
        /// The value for the connnection string setting
        /// </summary>
        private const string ConnectionStringSetting = "ConnectionString";

        /// <summary>
        /// The value for the databae name setting
        /// </summary>
        private const string DatabaseNameSetting = "DatabaseName";

        /// <summary>
        /// The default app.config configuration section
        /// </summary>
        public const string DEFAULT_SECTION = "Newhl/DatabaseConfiguration";

        /// <summary>
        /// Get the DatabaseConfiguration from the app.config using the default configuration section
        /// </summary>
        /// <returns>Database configuration instance</returns>
        public static DatabaseConfiguration GetInstance()
        {
            return DatabaseConfiguration.GetInstance(DEFAULT_SECTION);
        }

        /// <summary>
        /// Get the DatabaseConfiguration from the app.config using a specified default configuration section
        /// </summary>
        /// <param name="configurationSection">Configuration string</param>
        /// <returns>Database configuration instance</returns>
        public new static DatabaseConfiguration GetInstance(string configurationSection)
        {
            return (DatabaseConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DatabaseConfiguration()
        { }

        /// <summary>
        /// Gets or sets the connection string found in the config file.
        /// </summary>
        [ConfigurationProperty(ConnectionStringSetting, IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)this[ConnectionStringSetting]; }
            set { this[ConnectionStringSetting] = value; }
        }

        /// <summary>
        /// Gets or sets database name found in the config file
        /// </summary>
        [ConfigurationProperty(DatabaseNameSetting, IsRequired = true)]
        public string DatabaseName
        {
            get { return (string)this[DatabaseNameSetting]; }
            set { this[DatabaseNameSetting] = value; }
        }

        /// <summary>
        /// Get the connection string decrypted
        /// </summary>
        /// <returns>The decrypted connection string</returns>
        public string GetDecryptedConnectionString()
        {
            return this.DecryptString(this.ConnectionString);
        }

        /// <summary>
        /// Get the connection string decrypted
        /// </summary>
        /// <param name="encryptionKey">the key used for decryption</param>
        /// <param name="decryptionSalt">The salt used when encrypting</param>
        /// <returns>The decrypted connection string</returns>
        public string GetDecryptedConnectionString(string decryptionKey, string decryptionSalt)
        {
            return this.DecryptString(this.ConnectionString, decryptionKey, decryptionSalt);
        }

        /// <summary>
        /// Gets the database name decrytped
        /// </summary>
        /// <returns>The decrytped database name</returns>
        public string GetDecryptedDatabaseName()
        {
            return this.DecryptString(this.DatabaseName);
        }

        /// <summary>
        /// Gets the database name decrytped
        /// </summary>
        /// <param name="encryptionKey">the key used for decryption</param>
        /// <param name="decryptionSalt">The salt used when encrypting</param>
        /// <returns>The decrytped database name</returns>
        public string GetDecryptedDatabaseName(string decryptionKey, string decryptionSalt)
        {
            return this.DecryptString(this.DatabaseName, decryptionKey, decryptionSalt);
        }
    }
}
