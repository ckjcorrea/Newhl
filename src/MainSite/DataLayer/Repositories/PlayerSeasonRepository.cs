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
    public class PlayerSeasonRepository : RepositoryBase<PlayerSeason, DTO.PlayerSeason, long>, IPlayerSeasonRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public PlayerSeasonRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) 
        {

        }

        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override Newhl.Common.DataLayer.DataMapBase<PlayerSeason, DTO.PlayerSeason> GetDataMapper()
        {
            return new DataMapper.PlayerSeasonDataMapper();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.PlayerSeason GetDTOById(PlayerSeason idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.PlayerSeason GetDTOById(long id)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.PlayerSeason>();
            criteria.Add(Expression.Eq("Id", id));
            return criteria.UniqueResult<DTO.PlayerSeason>();
        }

        public PlayerSeason GetByPlayerIdAndSeasonId(long playerId, long seasonId)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.PlayerSeason>();
            criteria.Add(Expression.Eq("PlayerId", playerId));
            criteria.Add(Expression.Eq("SeasonId", seasonId));
            return this.GetDataMapper().Map(criteria.UniqueResult<DTO.PlayerSeason>());
        }

        public override PlayerSeason Save(PlayerSeason itemToSave)
        {
            if (itemToSave != null && itemToSave.Programs != null)
            {
                DTO.PlayerSeason dtoPost = this.GetDTOById(itemToSave.Id);

                if (dtoPost != null)
                {
                    foreach (Program domainProgram in itemToSave.Programs)
                    {
                        if (dtoPost.Programs.FirstOrDefault(t => t.Id == domainProgram.Id) == null)
                        {
                            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<DTO.Program>();
                            criteria.Add(Expression.Eq("Id", domainProgram.Id));
                            DTO.Program existsTest = criteria.UniqueResult<DTO.Program>();

                            if (existsTest != null)
                            {
                                dtoPost.Programs.Add(existsTest);
                            }
                        }
                    }
                }
            }

            return base.Save(itemToSave);
        }
    }
}
