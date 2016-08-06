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
    public class ProgramRepository : RepositoryBase<Program, DTO.Program, long>, IProgramRepository
    {/// <summary>
     /// The constructor, it takes a unit of work
     /// </summary>
     /// <param name="unitOfWork">A unit of Work instance</param>
        public ProgramRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) 
        {

        }

        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override Newhl.Common.DataLayer.DataMapBase<Program, DTO.Program> GetDataMapper()
        {
            return new DataMapper.ProgramDataMapper();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.Program GetDTOById(Program idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.Program GetDTOById(long id)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.Program>();
            criteria.Add(Expression.Eq("Id", id));
            return criteria.UniqueResult<DTO.Program>();
        }
    }
}
