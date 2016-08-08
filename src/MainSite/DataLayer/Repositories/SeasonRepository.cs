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
    public class SeasonRepository : RepositoryBase<Season, DTO.Season, long>, ISeasonRepository
    {/// <summary>
     /// The constructor, it takes a unit of work
     /// </summary>
     /// <param name="unitOfWork">A unit of Work instance</param>
        public SeasonRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) 
        {

        }

        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override Newhl.Common.DataLayer.DataMapBase<Season, DTO.Season> GetDataMapper()
        {
            return new DataMapper.SeasonDataMapper();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.Season GetDTOById(Season idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.Season GetDTOById(long id)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.Season>();
            criteria.Add(Expression.Eq("Id", id));
            return criteria.UniqueResult<DTO.Season>();
        }

        public IList<Season> GetAllActive()
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.Season>();
            criteria.Add(Expression.Eq("IsActive", true));
            return this.GetDataMapper().Map(criteria.List<DTO.Season>());
        }
    }
}
