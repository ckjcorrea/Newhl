using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// A class to encrypt or decrypt a string using an RSA Xml Key file
    /// </summary>
    public class RSAXmlKeyFileManager
    {
        /// <summary>
        /// A constructor that takes a public and a private key as parameters
        /// </summary>
        /// <param name="publicKeyFile"></param>
        /// <param name="privateKeyFile"></param>
        public RSAXmlKeyFileManager(string publicKeyFile, string privateKeyFile)
        {
            this.PublicKeyFile = publicKeyFile;
            this.PrivateKeyFile = privateKeyFile;
        }

        /// <summary>
        /// Gets or sets the Public Key File
        /// </summary>
        private string PublicKeyFile { get; set; }

        /// <summary>
        /// Gets or sets the Private Key file
        /// </summary>
        private string PrivateKeyFile { get; set; }

        /// <summary>
        /// Gets the contents of the private or public xml file
        /// </summary>
        /// <param name="keyFile">The path to the key file</param>
        /// <returns>A string with the file contents</returns>
        public string GetKeyFileContents(string keyFile)
        {
            if (!File.Exists(keyFile))
            {
                throw new Exception("Unable to locate expected key for RsaXml decryption (" + keyFile + ")");
            }

            return File.ReadAllText(keyFile);
        }

        /// <summary>
        /// Decrypt a give string
        /// </summary>
        /// <param name="encryptedString">The original encrypted string</param>
        /// <returns>The decrytped string</returns>
        public string Decrypt(string encryptedString)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(encryptedString))
            {
                using (var p = new RSACryptoServiceProvider())
                {
                    p.FromXmlString(this.GetKeyFileContents(this.PrivateKeyFile));
                    retVal = Encoding.UTF8.GetString(p.Decrypt(Convert.FromBase64String(encryptedString), true));
                }
            }

            return retVal;
        }

        /// <summary>
        /// Encrypt a give string
        /// </summary>
        /// <param name="decryptedString">The original decrypted string</param>
        /// <returns>The encrypted string</returns>
        public string Encrypt(string decryptedString)
        {
            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(decryptedString))
            {
                using (var p = new RSACryptoServiceProvider())
                {
                    p.FromXmlString(this.GetKeyFileContents(this.PublicKeyFile));
                    byte[] encryptedBytes = p.Encrypt(System.Text.Encoding.UTF8.GetBytes(decryptedString), true);
                    retVal = Convert.ToBase64String(encryptedBytes);
                }
            }

            return retVal;
        }
    }
}
