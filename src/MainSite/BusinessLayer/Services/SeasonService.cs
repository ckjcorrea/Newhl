using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.DataLayer.Repositories;


namespace Newhl.MainSite.BusinessLayer.Services
{
    public class SeasonService : ISeasonService
    {
        public SeasonService(ISeasonRepository seasonRepository, IPlayerSeasonRepository playerSeasonRepository)
        {
            this.SeasonRepository = seasonRepository;
            this.PlayerSeasonRepository = playerSeasonRepository;
        }

        protected ISeasonRepository SeasonRepository { get; private set; }

        protected IPlayerSeasonRepository PlayerSeasonRepository { get; private set; }

        public IList<Season> GetAll(bool activeOnly)
        {
            IList<Season> retVal = null;

            if (activeOnly)
            {
                retVal = this.SeasonRepository.GetAllActive();
            }
            else
            {
                retVal = this.SeasonRepository.GetAll();
            }

            return retVal;
        }

        public Season GetById(long id)
        {
            return this.SeasonRepository.GetById(id);
        }

        public bool UpdateSeasonPrograms(long playerId, long seasonId, IList<long> programsToAdd)
        {
            bool retVal = false;

            // Load up the season by the first
            Season targetSeason = this.SeasonRepository.GetById(seasonId);
            IList<Program> foundProgramsToAdd = new List<Program>();

            if (targetSeason != null)
            {
                bool allMatch = true;

                // first make sure all the programs are for the same season
                for(int i = 0; i < programsToAdd.Count; i++)
                {
                    Program foundProgramToAdd = targetSeason.Programs.FirstOrDefault(p => p.Id == programsToAdd[i]);

                    if(foundProgramToAdd != null)
                    {
                        foundProgramsToAdd.Add(foundProgramToAdd);
                    }
                    else
                    {
                        // write an error
                        allMatch = false;
                        break;
                    }
                }  
                    
                if(allMatch)
                {
                    PlayerSeason playerSeason = this.PlayerSeasonRepository.GetByPlayerIdAndSeasonId(playerId, seasonId);

                    if(playerSeason==null)
                    {
                        playerSeason = new PlayerSeason();
                        playerSeason.PlayerId = playerId;
                        playerSeason.SeasonId = seasonId;
                    }

                    if (targetSeason.StartDate >= DateTime.Now && (playerSeason.Payments == null || playerSeason.Payments.Count == 0))
                    {
                        playerSeason.UpdateSeasonPrograms(foundProgramsToAdd);
                        playerSeason = this.PlayerSeasonRepository.Save(playerSeason);

                        if (playerSeason != null)
                        {
                            retVal = true;
                        }
                    }
                }                
            }

            return retVal;
        }

        public PlayerSeason GetPlayerSeasonById(long playerSeasonId)
        {
            return this.PlayerSeasonRepository.GetById(playerSeasonId);
        }
        public PlayerSeason GetPlayerSeasonBySeasonId(long playerId, long seasonId)
        {
            return this.PlayerSeasonRepository.GetByPlayerIdAndSeasonId(playerId, seasonId);
        }

        public IList<PlayerSeason> GetPlayerSeasons(long playerId)
        {
            return this.PlayerSeasonRepository.GetByPlayerId(playerId);
        }

    }
}
