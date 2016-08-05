using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// RSA Encryption Help Class
    /// </summary>
    public class RSAEncryptionHelper
    {
        /// <summary>
        /// The default RSA Key Size
        /// </summary>
        public const int DEFAULT_KEY_SIZE = 2048;

        /// <summary>
        /// Default constructor
        /// </summary>
        public RSAEncryptionHelper() : this(DEFAULT_KEY_SIZE) { }
        
        /// <summary>
        /// A constructor that takes a key size as a parameter.
        /// </summary>
        /// <param name="rsaKeySize">The size of the RSA key to use when encrypting/decrypting</param>
        public RSAEncryptionHelper(int rsaKeySize)
        {
            this.KeySize = rsaKeySize;
        }
        
        /// <summary>
        /// Gets the size of the RSA key to use when encrypting/decrypting
        /// </summary>
        public int KeySize { get; private set; }
        
        /// <summary>
        /// Encrypt some plain text
        /// </summary>
        /// <param name="plainData">plaintext string</param>
        /// <param name="certificate">encryption certificate</param>
        /// <returns>encrypted string</returns>
        public string Encrypt(string plainData, X509Certificate2 certificate)
        {
            return this.Encrypt(plainData, false, certificate);
        }

        /// <summary>
        /// Encrypt some plain text with optional padding
        /// </summary>
        /// <param name="plainData">plaintext string</param>
        /// <param name="useOAEPPadding">use OAEP Padding</param>
        /// <param name="certificate">encryption certificate</param>
        /// <returns>encrypted string</returns>
        public string Encrypt(string plainData, bool useOAEPPadding, X509Certificate2 certificate)
        {
            string retVal = string.Empty;

            byte[] encryptedData = this.Encrypt(System.Text.Encoding.Unicode.GetBytes(plainData), useOAEPPadding, certificate);

            if (encryptedData != null)
            {
                retVal = Convert.ToBase64String(encryptedData);
            }

            return retVal;
        }

        /// <summary>
        /// Encrypt some plaintext byte array using padding
        /// </summary>
        /// <param name="plainData">Plaintext byte array</param>
        /// <param name="useOAEP">Use OAEP Padding</param>
        /// <param name="certificate">Encryption Certificate</param>
        /// <returns>encrypted byte array</returns>
        public byte[] Encrypt(byte[] plainData, bool useOAEP, X509Certificate2 certificate)
        {
            if (plainData == null)
            {
                throw new ArgumentNullException("plainData");
            }

            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(this.KeySize))
            {
                // Note that we use the public key to encrypt
                provider.FromXmlString(this.GetPublicKey(certificate));

                return provider.Encrypt(plainData, useOAEP);
            }
        }

        /// <summary>
        /// Decrypt an encrypted string with a certificate
        /// </summary>
        /// <param name="encryptedData">Encrypted string</param>
        /// <param name="certificate">Encryption Certificate</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string encryptedData, X509Certificate2 certificate)
        {
            return this.Decrypt(encryptedData, false, certificate);
        }

        /// <summary>
        /// Decrypt an encrypted string with padding and a certificate 
        /// </summary>
        /// <param name="encryptedData">Encrypted string</param>
        /// <param name="useOAEPPadding">Use OAEP Padding</param>
        /// <param name="certificate">Encryption Certificate</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string encryptedData, bool useOAEPPadding, X509Certificate2 certificate)
        {
            string retVal = string.Empty;

            byte[] decryptedData = this.Decrypt(Convert.FromBase64String(encryptedData), useOAEPPadding, certificate);

            if (decryptedData != null)
            {
                retVal = System.Text.Encoding.Unicode.GetString(decryptedData);
            }

            return retVal;
        }

        /// <summary>
        /// Decrypt given a byte array, padding and a certificate
        /// </summary>
        /// <param name="encryptedData">Encrypted byte array</param>
        /// <param name="useOAEP">Use OAEP Padding</param>
        /// <param name="certificate">Encryption Certificate</param>
        /// <returns>Decrypted byte array</returns>
        public byte[] Decrypt(byte[] encryptedData, bool useOAEP, X509Certificate2 certificate)
        {
            if (encryptedData == null)
            {
                throw new ArgumentNullException("encryptedData");
            }

            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(this.KeySize))
            {
                // Note that we use the private key to decrypt
                provider.FromXmlString(this.GetXmlKeyPair(certificate));

                return provider.Decrypt(encryptedData, useOAEP);
            }
        }

        /// <summary>
        /// Get the public key given a certificate
        /// </summary>
        /// <param name="certificate">Encryption Certificate</param>
        /// <returns>The public key</returns>
        public string GetPublicKey(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            return certificate.PublicKey.Key.ToXmlString(false);
        }

        /// <summary>
        /// Get an XML key pair given a certificate
        /// </summary>
        /// <param name="certificate">Encryption Certificate</param>
        /// <returns>XML Key Pair</returns>
        public string GetXmlKeyPair(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            if (!certificate.HasPrivateKey)
            {
                throw new ArgumentException("certificate does not have a private key");
            }
            else
            {
                return certificate.PrivateKey.ToXmlString(true);
            }
        }
    }
}
