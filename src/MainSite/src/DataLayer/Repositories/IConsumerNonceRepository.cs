using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// The interface to interact with ConsumerNonces in the database
    /// </summary>
    public interface IConsumerNonceRepository : INHibernateRepository<ConsumerNonce, string>
    {
        /// <summary>
        /// Get an instance by the nonce value
        /// </summary>
        /// <param name="nonce">The nonce value</param>
        /// <returns>A ConsumerNonce instance if found, null otherwise</returns>
        ConsumerNonce GetByNonce(string nonce);
    }
}
