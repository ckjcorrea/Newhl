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
            ProgramDataMapper.ConfigureAutoMapper();
            PaymentDataMapper.ConfigureAutoMapper();

            if (AutoMapper.Mapper.FindTypeMapFor<PlayerSeason, DTO.PlayerSeason>() == null)
            {
                AutoMapper.Mapper.CreateMap<PlayerSeason, DTO.PlayerSeason>()
                    .ForMember(season => season.Programs, programs => programs.ResolveUsing<PlayerSeasonProgramsDTOResolver>())
                    .ForMember(season => season.Payments, payments => payments.ResolveUsing<PlayerSeasonPaymentsDTOResolver>());
            }

            if (AutoMapper.Mapper.FindTypeMapFor<DTO.PlayerSeason, PlayerSeason>() == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.PlayerSeason, PlayerSeason>();
            }

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

            foreach (DTO.Program currentProgram in retVal.Programs)
            {
                currentProgram.SeasonId = retVal.Id;
            }

            foreach(DTO.Payment currentPayment in retVal.Payments)
            {
                currentPayment.PlayerSeasonId = retVal.Id;
            }

            return retVal;
        }
    }
}
