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
    public class LoginAttemptRepository: RepositoryBase<LoginAttempt, DTO.LoginAttempt, long>, ILoginAttemptRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public LoginAttemptRepository(UnitOfWork unitOfWork) : base(unitOfWork) 
        { 
        
        }
       
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Common.DataLayer.DataMapBase<LoginAttempt, DTO.LoginAttempt> GetDataMapper()
        {
            return new DataMapper.LoginAttemptDataMapper(); 
        }
        
        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.LoginAttempt GetDTOById(LoginAttempt idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.LoginAttempt GetDTOById(long id)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.LoginAttempt>();
            criteria.Add(Expression.Eq(DTO.LoginAttempt.IdFieldName, id));
            return criteria.UniqueResult<DTO.LoginAttempt>();
        }

        /// <summary>
        /// Get a AMFUserLogin by the attempted login name
        /// </summary>
        /// <param name="userName">The users email address used to attempt to login</param>
        /// <returns>The found domain object instance</returns>
        public IList<LoginAttempt> GetByUserName(string userName)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.LoginAttempt>();
            criteria.Add(Expression.Eq(DTO.LoginAttempt.UserNameField, userName));
            criteria.AddOrder(Order.Desc("AttemptDate"));
            return this.GetDataMapper().Map(criteria.List<DTO.LoginAttempt>());
        }
    }
}
