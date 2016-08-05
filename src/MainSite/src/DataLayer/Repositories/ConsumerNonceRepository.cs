using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// The consumer nonce repository implementation
    /// </summary>
    public class ConsumerNonceRepository : RepositoryBase<ConsumerNonce, DTO.ConsumerNonce, string>, IConsumerNonceRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public ConsumerNonceRepository(UnitOfWork unitOfWork) : base(unitOfWork) 
        { 
        
        }
        
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Common.DataLayer.DataMapBase<ConsumerNonce, DTO.ConsumerNonce> GetDataMapper()
        {
            return new DataMapper.ConsumerNonceDataMapper(); 
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.ConsumerNonce GetDTOById(ConsumerNonce idSource)
        {
            return this.GetDTOById(idSource.Nonce);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.ConsumerNonce GetDTOById(string nonce)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.ConsumerNonce>();
            criteria.Add(Expression.Eq(DTO.ConsumerNonce.NonceFieldName, nonce));
            return criteria.UniqueResult<DTO.ConsumerNonce>();
        }

        /// <summary>
        /// Get the domain instance by the Nonce
        /// </summary>
        /// <param name="nonce">The nonce value to search for</param>
        /// <returns>A consumernonce instance if it is found</returns>
        public ConsumerNonce GetByNonce(string nonce)
        {
            return this.GetDataMapper().Map(this.GetDTOById(nonce));
        }
    }
}
