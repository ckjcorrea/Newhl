using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.DataMapper;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// A repository that retrieves a AMFUserLogin
    /// </summary>
    public class AMFUserRepository : RepositoryBase<AMFUserLogin, DTO.AMFUser, long>, IAMFUserRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public AMFUserRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) 
        { 
        
        }
       
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Common.DataLayer.DataMapBase<AMFUserLogin, DTO.AMFUser> GetDataMapper()
        {
            return new DataMapper.AMFUserLoginDataMapper(); 
        }
        
        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.AMFUser GetDTOById(AMFUserLogin idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.AMFUser GetDTOById(long id)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.AMFUser>();
            criteria.Add(Expression.Eq(DTO.AMFUser.IdFieldName, id));
            return criteria.UniqueResult<DTO.AMFUser>();
        }

        /// <summary>
        /// Get a AMFUserLogin by the email address.
        /// </summary>
        /// <param name="emailAddress">The users email address</param>
        /// <returns>The found domain object instance</returns>
        public AMFUserLogin GetByEmail(string emailAddress)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.AMFUser>();
            criteria.Add(Expression.Eq(DTO.AMFUser.EmailFieldName, emailAddress));
            return this.GetDataMapper().Map(criteria.UniqueResult<DTO.AMFUser>());
        }

        /// <summary>
        /// Search for a user by its email
        /// </summary>
        /// <param name="email">Search the email field for similar strings</param>
        /// <returns>The user if one is found</returns>
        public IList<AMFUserLogin> SearchByEmail(string emailAddress)
        {
            /// tbd just do a contains for now, implement full text search later
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.AMFUser>();
            criteria.Add(Expression.Like(DTO.AMFUser.EmailFieldName, emailAddress, MatchMode.Anywhere));
            return this.GetDataMapper().Map(criteria.List<DTO.AMFUser>());
        }
    }
}
