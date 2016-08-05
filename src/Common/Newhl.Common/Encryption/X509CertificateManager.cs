using System;
using System.Security.Cryptography.X509Certificates;

namespace Newhl.Common.Encryption
{
    /// <summary>
    /// X.509 Certificate Manager
    /// </summary>
    public class X509CertificateManager
    {
        private class KeyStoreParameters
        {
            public KeyStoreParameters(string storeName, string storeLocation, string certificateName)
            {
                this.StoreName = (StoreName)Enum.Parse(typeof(StoreName), storeName);
                this.StoreLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), storeLocation);
                this.CertificateName = certificateName;
            }

            public StoreName StoreName { get; private set; }

            public StoreLocation StoreLocation { get; private set; }

            public string CertificateName { get; private set; }
        }

        /// <summary>
        /// Parameters for generating a key file
        /// </summary>
        public class KeyFileParameters
        {
            /// <summary>
            /// Key File Parameters Constructor
            /// </summary>
            /// <param name="keyFile">Key File</param>
            /// <param name="keyFilePassword">Key File Password</param>
            public KeyFileParameters(string keyFile, string keyFilePassword)
            {
                this.KeyFile = keyFile;
                this.KeyFilePassword = keyFilePassword;
            }

            /// <summary>
            /// Gets the Key File
            /// </summary>
            public string KeyFile { get; private set; }

            /// <summary>
            /// Gets the Key File Password
            /// </summary>
            public string KeyFilePassword { get; private set; }
        }

        /// <summary>
        /// X509 Certificate Manager Constructor
        /// </summary>
        /// <param name="storeName">Certificate Store Name</param>
        /// <param name="storeLocation">Certificate Store Location</param>
        /// <param name="certificateName">Certificate Name</param>
        public X509CertificateManager(string storeName, string storeLocation, string certificateName)
        {
            this.KeyStoreInfo = new KeyStoreParameters(storeName, storeLocation, certificateName);
        }

        /// <summary>
        /// X.509 Certificate Manager Constructor
        /// </summary>
        /// <param name="keyFile">Key File</param>
        /// <param name="keyFilePassword">Key File Password</param>
        public X509CertificateManager(string keyFile, string keyFilePassword)
        {
            this.KeyFileInfo = new KeyFileParameters(keyFile, keyFilePassword);
        }

        private KeyFileParameters KeyFileInfo { get; set; }

        private KeyStoreParameters KeyStoreInfo { get; set; }

        private X509Certificate2 certificateFile;

        /// <summary>
        /// Gets a certificate file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "Suppressed - Valid error")]
        public X509Certificate2 CertificateFile
        {
            get
            {
                if (this.certificateFile == null)
                {
                    if (this.KeyFileInfo != null)
                    {
                        this.certificateFile = new X509Certificate2(this.KeyFileInfo.KeyFile, this.KeyFileInfo.KeyFilePassword, X509KeyStorageFlags.Exportable);
                    }
                    else
                    {
                        if (this.KeyStoreInfo != null)
                        {
                            X509Store store = new X509Store(this.KeyStoreInfo.StoreName, this.KeyStoreInfo.StoreLocation);

                            try
                            {
                                store.Open(OpenFlags.ReadOnly);

                                X509Certificate2Collection certificateCollection =
                                    store.Certificates.Find(X509FindType.FindBySubjectName, this.KeyStoreInfo.CertificateName, false);

                                if (certificateCollection.Count > 0)
                                {
                                    // We ignore if there is more than one matching cert, 
                                    // we just return the first one.
                                    return certificateCollection[0];
                                }
                                else
                                {
                                    throw new ArgumentException("Certificate not found");
                                }
                            }
                            finally
                            {
                                store.Close();
                            }
                        }
                    }
                }

                return this.certificateFile;
            }
        }

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="sourceData">Data to be encrypted</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string sourceData)
        {
            string retVal = string.Empty;

            if (this.CertificateFile != null)
            {
                RSAEncryptionHelper certificateHelper = new RSAEncryptionHelper(2048);
                retVal = certificateHelper.Encrypt(sourceData, false, this.CertificateFile);
            }

            return retVal;
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="encryptedData">Encrypted data</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string encryptedData)
        {
            string retVal = string.Empty;

            if (this.CertificateFile != null)
            {
                RSAEncryptionHelper certificateHelper = new RSAEncryptionHelper(2048);
                retVal = certificateHelper.Decrypt(encryptedData, false, this.CertificateFile);
            }

            return retVal;
        }
    }
}
