using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.Encryption;

namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// A class to facilitate encrypting and decrypting the user we will pass from the proxy to the endpoint
    /// </summary>
    public class UserTransferManager
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserTransferManager() : this(AESConfiguration.GetInstance())
        {

        }

        /// <summary>
        /// Constructor with specified configuration
        /// </summary>
        /// <param name="aesConfiguration">The AES configuration instance</param>
        public UserTransferManager(AESConfiguration aesConfiguration) : this(aesConfiguration.EncryptionKey, aesConfiguration.Salt)
        {

        }

        /// <summary>
        /// Constructor with key and salt passed in directly
        /// </summary>
        /// <param name="encryptionKey">The encryption key</param>
        /// <param name="encryptionSalt">The encryption salt</param>
        public UserTransferManager(string encryptionKey, string encryptionSalt)
        {
            this.EncryptionKey = encryptionKey;
            this.Salt = encryptionSalt;
        }

        /// <summary>
        /// Gets or sets the encryption key
        /// </summary>
        private string EncryptionKey { get; set;}

        /// <summary>
        /// Gets or sets the encryption salt
        /// </summary>
        private string Salt { get; set; }

        /// <summary>
        /// Encrypt a AlwaysMoveForward User
        /// </summary>
        /// <param name="user">The current user</param>
        /// <returns>An encrypted string</returns>
        public string Encrypt(AlwaysMoveForward.Common.DomainModel.User user)
        {
            string retVal = string.Empty;

            if (user != null)
            {
                AESManager encryptor = new AESManager(this.EncryptionKey, this.Salt);
                retVal = encryptor.Encrypt(AlwaysMoveForward.Common.Utilities.SerializationUtilities.SerializeObjectToXmlString(user));
            }

            return retVal;
        }

        /// <summary>
        /// Decrypt to a AlwaysMoveForward User
        /// </summary>
        /// <param name="encryptedString">The encrypted string</param>
        /// <returns>A AlwaysMoveForward user instance</returns>
        public AlwaysMoveForward.Common.DomainModel.User Decrypt(string encryptedString)
        {
            AlwaysMoveForward.Common.DomainModel.User retVal = null;

            if (!string.IsNullOrEmpty(encryptedString))
            {
                AESManager encryptor = new AESManager(this.EncryptionKey, this.Salt);
                string decryptedValue = encryptor.Decrypt(encryptedString);
                retVal = AlwaysMoveForward.Common.Utilities.SerializationUtilities.DeserializeXmlToObject<AlwaysMoveForward.Common.DomainModel.User>(decryptedValue);
            }

            return retVal;
        }
    }
}
