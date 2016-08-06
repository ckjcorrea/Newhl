using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.DataLayer.DataMapper;


namespace Newhl.MainSite.DataLayer.Repositories
{
    public class PaymentRepository : RepositoryBase<Payment, DTO.Payment, long>, IPaymentRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public PaymentRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) 
        {

        }

        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override Newhl.Common.DataLayer.DataMapBase<Payment, DTO.Payment> GetDataMapper()
        {
            return new DataMapper.PaymentDataMapper();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.Payment GetDTOById(Payment idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.Payment GetDTOById(long id)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.Payment>();
            criteria.Add(Expression.Eq("Id", id));
            return criteria.UniqueResult<DTO.Payment>();
        }

        public IList<Payment> GetByUserId(long userId)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.Payment>();
            criteria.CreateCriteria("Player").Add(Expression.Eq("Id", userId));
            criteria.AddOrder(Order.Desc("DateSubmitted"));
            return this.GetDataMapper().Map(criteria.List<DTO.Payment>());
        }
    }
}
