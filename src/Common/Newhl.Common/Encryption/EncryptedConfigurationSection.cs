using System.Configuration;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// A class to simplify getting the configuration settings for a database
    /// </summary>
    public class EncryptedConfigurationSection : ConfigurationSection
    {        
        /// <summary>
        /// The value for the encryption method setting
        /// </summary>
        private const string EncryptionMethodSetting = "EncryptionMethod";

        /// <summary>
        /// The value for the related encryption configuration key
        /// </summary>
        private const string EncryptionConfigurationSetting = "EncryptionSetting";

        /// <summary>
        /// Possible options for the Encryption method
        /// </summary>
        public enum EncryptionMethodOptions
        {
            /// <summary>
            /// There is no encryption
            /// </summary>
            None,

            /// <summary>
            /// The values were encrypted with a certificate stored in a key file
            /// </summary>
            CertificateKeyFile,

            /// <summary>
            /// The values were encrypted with a certificate stored in the key store
            /// </summary>
            CertificateKeyStore,

            /// <summary>
            /// The values were encrypted with AES
            /// </summary>
            AES,

            /// <summary>
            /// The values were encrytped using RSA with the key valies in an xml file
            /// </summary>
            RSAXmlKeyFile,

            /// <summary>
            /// Uses internal settings for encryption
            /// </summary>
            Internal
        }

        /// <summary>
        /// Get the DatabaseConfiguration from the app.config using a specified default configuration section
        /// </summary>
        /// <param name="configurationSection">Configuration string</param>
        /// <returns>Database configuration instance</returns>
        public static EncryptedConfigurationSection GetInstance(string configurationSection)
        {
            return (EncryptedConfigurationSection)System.Configuration.ConfigurationManager.GetSection(configurationSection);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public EncryptedConfigurationSection()
        { }

        /// <summary>
        /// Gets or sets a value indicating whether or not it's encrypted
        /// </summary>
        [ConfigurationProperty(EncryptionMethodSetting, IsRequired = false, DefaultValue = EncryptionMethodOptions.None)]
        public EncryptionMethodOptions EncryptionMethod
        {
            get { return (EncryptionMethodOptions)this[EncryptionMethodSetting]; }
            set { this[EncryptionMethodSetting] = value; }
        }

        /// <summary>
        /// Gets the configuration section that defines the encryption parameters
        /// </summary>
        [ConfigurationProperty(EncryptionConfigurationSetting, IsRequired = false, DefaultValue = "")]
        public string EncryptionSetting
        {
            get { return (string)this[EncryptionConfigurationSetting]; }
            set { this[EncryptionConfigurationSetting] = value; }
        }

        /// <summary>
        /// This method decrypts a string using the configuration settings supplied
        /// </summary>
        /// <param name="encryptedString">The encrypted string</param>
        /// <returns>The passed in string, decrypted</returns>
        public string DecryptString(string encryptedString)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(encryptedString))
            {
                switch(this.EncryptionMethod)
                {
                    case EncryptionMethodOptions.None:
                        retVal = encryptedString;
                        break;
                    case EncryptionMethodOptions.AES:
                        AESConfiguration aesconfiguration = AESConfiguration.GetInstance(this.EncryptionSetting);
                        AESManager aesencryption = new AESManager(aesconfiguration.EncryptionKey, aesconfiguration.Salt);
                        retVal = aesencryption.Decrypt(encryptedString);
                        break;
                    case EncryptionMethodOptions.CertificateKeyFile:
                        KeyFileConfiguration keyfileConfiguration = KeyFileConfiguration.GetInstance(this.EncryptionSetting);
                        X509CertificateManager keyfileEncryption = new X509CertificateManager(keyfileConfiguration.KeyFile, keyfileConfiguration.KeyFilePassword);
                        retVal = keyfileEncryption.Decrypt(encryptedString);
                        break;
                    case EncryptionMethodOptions.CertificateKeyStore:
                        KeyStoreConfiguration keystoreConfiguration = KeyStoreConfiguration.GetInstance(this.EncryptionSetting);
                        X509CertificateManager keystoreEncryption = new X509CertificateManager(keystoreConfiguration.StoreName, keystoreConfiguration.StoreLocation, keystoreConfiguration.CertificateName);
                        retVal = keystoreEncryption.Decrypt(encryptedString);
                        break;
                    case EncryptionMethodOptions.RSAXmlKeyFile:
                        RSAXmlKeyFileConfiguration rsaxmlKeyFileConfiguration = RSAXmlKeyFileConfiguration.GetInstance();
                        RSAXmlKeyFileManager rsaxmlKeyFileEncryption = new RSAXmlKeyFileManager(rsaxmlKeyFileConfiguration.PublicKeyFile, rsaxmlKeyFileConfiguration.PrivateKeyFile);
                        retVal = rsaxmlKeyFileEncryption.Decrypt(encryptedString);
                        break;
                }
            }

            return retVal;
        }

        public string DecryptString(string encryptedString, string decryptionKey, string decryptionSalt)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(encryptedString))
            {
                AESManager internalManager = new AESManager(decryptionKey, decryptionSalt);
                retVal = internalManager.Decrypt(encryptedString);
            }

            return retVal;
        }
    }
}