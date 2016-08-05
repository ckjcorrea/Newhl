using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Storage;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
{
    /// <summary>
    /// The primary business rules of the consumer and consumer nonce
    /// </summary>
    public class ConsumerService : IConsumerService, IConsumerStore, INonceStore
    {
        /// <summary>
        /// The constructor for the class
        /// </summary>
        /// <param name="consumerRepository">The consumer repository</param>
        /// <param name="consumerNonceRepository">The consumer nonce repository</param>
        public ConsumerService(IConsumerRepository consumerRepository, IConsumerNonceRepository consumerNonceRepository)
        {
            this.ConsumerRepository = consumerRepository;
            this.ConsumerNonceRepository = consumerNonceRepository;
        }

        protected IConsumerRepository ConsumerRepository { get; private set; }

        protected IConsumerNonceRepository ConsumerNonceRepository { get; private set; }

        /// <summary>
        /// Get all of the consumers
        /// </summary>
        /// <returns>A list of consumers</returns>
        public IList<Consumer> GetAll()
        {
            return this.ConsumerRepository.GetAll();
        }

        /// <summary>
        /// Save a consumer
        /// </summary>
        /// <param name="consumer">the object to save</param>
        /// <returns>The saved consumer</returns>
        public Consumer Save(Consumer consumer)
        {
            if(consumer != null)
            {
                if(consumer.PublicKey==null)
                {
                    consumer.PublicKey = string.Empty;
                }

                consumer = this.ConsumerRepository.Save(consumer);
            }

            return consumer;
        }
        /// <summary>
        /// Creates a new consumer and saves it to the database.
        /// </summary>
        /// <returns>A new consumer</returns>
        public Consumer Create(string consumerName, string contactEmail)
        {
            Consumer newConsumer = null;

            if (!string.IsNullOrEmpty(consumerName) && !string.IsNullOrEmpty(contactEmail))
            {
                newConsumer = new Consumer();
                newConsumer.ConsumerKey = Guid.NewGuid().ToString();
                newConsumer.ConsumerSecret = Guid.NewGuid().ToString();
                newConsumer.Name = consumerName;
                newConsumer.ContactEmail = contactEmail;
                newConsumer.PublicKey = string.Empty;
            }

            return this.ConsumerRepository.Save(newConsumer);
        }

        /// <summary>
        /// Implemented for the ICOnsumerStore of DevDefined, not really used though
        /// </summary>
        /// <param name="consumer">The consumer to load the public key for</param>
        /// <returns>The public key</returns>
        public System.Security.Cryptography.AsymmetricAlgorithm GetConsumerPublicKey(IConsumer consumer)
        {
            System.Security.Cryptography.AsymmetricAlgorithm retVal = null;

            if (consumer != null && !string.IsNullOrEmpty(consumer.ConsumerKey))
            {
                Consumer fullConsumer = this.ConsumerRepository.GetByConsumerKey(consumer.ConsumerKey);

                if (fullConsumer != null)
                {
                    retVal = new RSACryptoServiceProvider();
                    retVal.FromXmlString(fullConsumer.PublicKey);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Finds a consumer in the repository by the key
        /// </summary>
        /// <param name="consumerKey">The consumer key</param>
        /// <returns>The consumer instance</returns>
        public Consumer GetConsumer(string consumerKey)
        {
            Consumer retVal = null;

            if (!string.IsNullOrEmpty(consumerKey))
            {
                retVal = this.ConsumerRepository.GetByConsumerKey(consumerKey);
            }

            return retVal;
        }

        /// <summary>
        /// Finds a consumer in the repository and returns its secret
        /// </summary>
        /// <param name="consumer">The current OAuth context</param>
        /// <returns>The consumer secret</returns>
        public string GetConsumerSecret(IOAuthContext consumer)
        {
            string retVal = string.Empty;

            Consumer foundConsumer = this.GetConsumer(consumer.ConsumerKey);

            if (foundConsumer != null)
            {
                retVal = foundConsumer.ConsumerSecret;
            }

            return retVal;
        }

        /// <summary>
        /// Validates that the consumer passed in is found in the database
        /// </summary>
        /// <param name="consumer">The consumer to search for</param>
        /// <returns>True if the consumer is found</returns>
        public bool IsConsumer(IConsumer consumer)
        {
            bool retVal = false;

            Consumer foundConsumer = this.ConsumerRepository.GetByConsumerKey(consumer.ConsumerKey);

            if (foundConsumer != null)
            {
                retVal = true;
            }

            return retVal;
        }

        /// <summary>
        /// Associates a public key with the consumer
        /// </summary>
        /// <param name="consumer">The consumer to add the key to</param>
        /// <param name="certificate">The certificate to add to the consumer</param>
        public void SetConsumerCertificate(IConsumer consumer, System.Security.Cryptography.X509Certificates.X509Certificate2 certificate)
        {
            Consumer foundConsumer = this.ConsumerRepository.GetByConsumerKey(consumer.ConsumerKey);

            if (foundConsumer != null)
            {
                foundConsumer.PublicKey = certificate.PublicKey.ToString();
                this.ConsumerRepository.Save(foundConsumer);
            }
        }

        /// <summary>
        /// sets the consumer secret
        /// </summary>
        /// <param name="consumer">The consumer to update</param>
        /// <param name="consumerSecret">The secret to change to</param>
        public void SetConsumerSecret(IConsumer consumer, string consumerSecret)
        {
            Consumer foundConsumer = this.ConsumerRepository.GetByConsumerKey(consumer.ConsumerKey);

            if (foundConsumer != null)
            {
                foundConsumer.ConsumerSecret = consumerSecret;
                this.ConsumerRepository.Save(foundConsumer);
            }
        }

        /// <summary>
        /// Save the nonce (which will fail if it exists) and ensure it is unique in the system
        /// </summary>
        /// <param name="consumer">The consumer to associate with the nonce</param>
        /// <param name="nonce">The nonce itself</param>
        /// <returns>True of the nonce is unique and stored successfully</returns>
        public bool RecordNonceAndCheckIsUnique(IConsumer consumer, string nonce)
        {
            bool retVal = false;

            ConsumerNonce foundNonce = this.ConsumerNonceRepository.GetByNonce(nonce);

            if (foundNonce == null)
            {
                foundNonce = new ConsumerNonce();
                foundNonce.Nonce = nonce;
                foundNonce.ConsumerKey = consumer.ConsumerKey;
                foundNonce.Timestamp = DateTime.Now;

                this.ConsumerNonceRepository.Save(foundNonce);

                retVal = true;
            }

            return retVal;
        }

        /// <summary>
        /// Find a consumer by the a request token
        /// </summary>
        /// <param name="consumerKey">The request token</param>
        /// <returns>The target consumer</returns>
        public Consumer GetByRequestToken(string requestToken)
        {
            Consumer retVal = null;

            if (!string.IsNullOrEmpty(requestToken))
            {
                retVal = this.ConsumerRepository.GetByRequestToken(requestToken);
            }

            return retVal;
        }
    }
}
