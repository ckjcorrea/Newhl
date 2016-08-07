using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.Common.DataLayer;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.DataLayer.DataMapper
{
    internal class PlayerSeasonDataMapper : DataMapBase<PlayerSeason, DTO.PlayerSeason>
    {
        static PlayerSeasonDataMapper()
        {
            PlayerSeasonDataMapper.ConfigureAutoMapper();
        }

        internal static void ConfigureAutoMapper()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<PlayerSeason, DTO.PlayerSeason>() == null)
            {
                AutoMapper.Mapper.CreateMap<PlayerSeason, DTO.PlayerSeason>()
                    .ForMember(season => season.Programs, programs => programs.ResolveUsing<PlayerSeasonProgramsDTOResolver>());
            }

            if (AutoMapper.Mapper.FindTypeMapFor<DTO.PlayerSeason, PlayerSeason>() == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.PlayerSeason, PlayerSeason>();
            }

            ProgramDataMapper.ConfigureAutoMapper();
#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

        public override PlayerSeason Map(DTO.PlayerSeason source, PlayerSeason destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override DTO.PlayerSeason Map(PlayerSeason source, DTO.PlayerSeason destination)
        {
            DTO.PlayerSeason retVal = AutoMapper.Mapper.Map(source, destination);

            foreach (DTO.Program currentListItem in retVal.Programs)
            {
                currentListItem.SeasonId = retVal.Id;
            }

            return retVal;
        }
    }
}
