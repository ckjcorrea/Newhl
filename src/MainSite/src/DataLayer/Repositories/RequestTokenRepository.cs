using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;
using NHibernate;
using NHibernate.Criterion;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// The implementation of the Request Token repository
    /// </summary>
    public class RequestTokenRepository : RepositoryBase<RequestToken, DTO.RequestToken, long>, IRequestTokenRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public RequestTokenRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Common.DataLayer.DataMapBase<RequestToken, DTO.RequestToken> GetDataMapper()
        {
            return new DataMapper.RequestTokenDataMapper(); 
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.RequestToken GetDTOById(RequestToken domainInstance)
        {
            return this.GetDTOById(domainInstance.Id);
        }

        /// <summary>
        /// Get the table for this repository to assist the base class
        /// </summary>
        /// <param name="id">The id value to search for</param>
        /// <returns>The dbset for the table</returns>
        protected override DTO.RequestToken GetDTOById(long id)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.RequestToken>();
            criteria.Add(Expression.Eq(DTO.RequestToken.IdFieldName, id));
            return criteria.UniqueResult<DTO.RequestToken>();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <param name="startDate">The start of a date range to search for</param>
        /// <param name="endDate">The end of a date range to search for</param>
        /// <returns>An instance of the DTO</returns>
        public IList<RequestToken> GetByConsumerKey(string consumerKey, DateTime startDate, DateTime endDate)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.RequestToken>();
            criteria.Add(Expression.Eq(DTO.RequestToken.ConsumerKeyFieldName, consumerKey));
            criteria.Add(Expression.Between(DTO.RequestToken.DateCreatedFieldName, startDate, endDate));
            criteria.AddOrder(Order.Desc(DTO.RequestToken.IdFieldName));
            return this.GetDataMapper().Map(criteria.List<DTO.RequestToken>());
        }

        /// <summary>
        /// Get a request token by its token value
        /// </summary>
        /// <param name="token">The token value</param>
        /// <returns>The request token instance</returns>
        public RequestToken GetByToken(string token)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.RequestToken>();
            criteria.Add(Expression.Eq(DTO.RequestToken.TokenFieldName, token));
            return this.GetDataMapper().Map(criteria.UniqueResult<DTO.RequestToken>());
        }

        /// <summary>
        /// Get a request token by its token and verifier code
        /// </summary>
        /// <param name="token">The token value</param>
        /// <param name="verifierCode">The request authorization verifier code</param>
        /// <returns>The request token instance</returns>
        public RequestToken GetByTokenAndVerifierCode(string token, string verifierCode)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.RequestToken>();
            criteria.Add(Expression.Eq(DTO.RequestToken.TokenFieldName, token));
            criteria.Add(Expression.Eq(DTO.RequestToken.VerifierCodeFieldName, verifierCode));
            return this.GetDataMapper().Map(criteria.UniqueResult<DTO.RequestToken>());
        }

        /// <summary>
        /// Gets an access token by its token value
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>An access token instance</returns>
        public AccessToken GetAccessToken(string token)
        {
            AccessToken retVal = null;

            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.RequestToken>();
            criteria.CreateCriteria(DTO.RequestToken.AccessTokenFieldName).Add(Expression.Eq(DTO.AccessToken.TokenFieldName, token));

            RequestToken mappedToken = this.GetDataMapper().Map(criteria.UniqueResult<DTO.RequestToken>());

            if (mappedToken != null)
            {
                retVal = mappedToken.AccessToken;
            }


            return retVal;
        }

        /// <summary>
        /// Get an access token by its request token and request token verifier code
        /// </summary>
        /// <param name="token">The request token value</param>
        /// <param name="verifierCode">The authorization verifier code</param>
        /// <returns>An access token instance</returns>
        public AccessToken GetAccessTokenByRequestToken(string token, string verifierCode)
        {
            AccessToken retVal = null;

            RequestToken mappedToken = this.GetByTokenAndVerifierCode(token, verifierCode);

            if (mappedToken != null)
            {
                retVal = mappedToken.AccessToken;
            }

            return retVal;
        }

        /// <summary>
        /// Get all of the request tokens associated with a user id
        /// </summary>
        /// <param name="userId">The user id to check for</param>
        /// <param name="startDate">The start of a date range to search for</param>
        /// <param name="endDate">The end of a date range to search for</param>
        /// <returns>A list of request tokens</returns>
        public IList<RequestToken> GetByUserId(long userId, DateTime startDate, DateTime endDate)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.RequestToken>();
            criteria.Add(Restrictions.Eq(DTO.RequestToken.UserIdFieldName, userId));
            criteria.Add(Expression.Between(DTO.RequestToken.DateCreatedFieldName, startDate, endDate)); 
            criteria.AddOrder(Order.Desc(DTO.RequestToken.IdFieldName));
            return this.GetDataMapper().Map(criteria.List<DTO.RequestToken>());
        }

        /// <summary>
        /// Get all of the requst tokens associated with a user name
        /// </summary>
        /// <param name="userName">The user name to check for</param>
        /// <param name="startDate">The start of a date range to search for</param>
        /// <param name="endDate">The end of a date range to search for</param>
        /// <returns>A list of request tokens</returns>
        public IList<RequestToken> GetByUserName(string userName, DateTime startDate, DateTime endDate)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.RequestToken>();
            criteria.Add(Restrictions.Eq(DTO.RequestToken.UserNameFieldName, userName));
            criteria.Add(Expression.Between(DTO.RequestToken.DateCreatedFieldName, startDate, endDate)); 
            criteria.AddOrder(Order.Desc(DTO.RequestToken.IdFieldName));
            return this.GetDataMapper().Map(criteria.List<DTO.RequestToken>());
        }
    }
}
