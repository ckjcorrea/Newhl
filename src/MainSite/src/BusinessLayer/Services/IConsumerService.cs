using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Storage;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
{
    public interface IConsumerService : IConsumerStore, INonceStore
    {
                /// <summary>
        /// Save a consumer
        /// </summary>
        /// <param name="consumer">the object to save</param>
        /// <returns>The saved consumer</returns>
        Consumer Save(Consumer consumer);

        /// <summary>
        /// Get all of the consumers
        /// </summary>
        /// <returns>A list of consumers</returns>
        IList<Consumer> GetAll();

        /// <summary>
        /// Creates a new consumer and saves it to the database.
        /// </summary>
        /// <returns>A new consumer</returns>
        Consumer Create(string consumerName, string contactEmail);

        /// <summary>
        /// Finds a consumer in the repository by the key
        /// </summary>
        /// <param name="consumerKey">The consumer key</param>
        /// <returns>The consumer instance</returns>
        Consumer GetConsumer(string consumerKey);

        /// <summary>
        /// Find a consumer by the a request token
        /// </summary>
        /// <param name="consumerKey">The request token</param>
        /// <returns>The target consumer</returns>
        Consumer GetByRequestToken(string requestToken);
    }
}
