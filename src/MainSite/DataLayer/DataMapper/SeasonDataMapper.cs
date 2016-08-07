using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.Common.DataLayer;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.DataLayer.DataMapper
{
    internal class SeasonDataMapper : DataMapBase<Season, DTO.Season>
    {
        static SeasonDataMapper()
        {
            SeasonDataMapper.ConfigureAutoMapper();
        }

        internal static void ConfigureAutoMapper()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<Season, DTO.Season>() == null)
            {
                AutoMapper.Mapper.CreateMap<Season, DTO.Season>()
                    .ForMember(season => season.Programs, programs => programs.ResolveUsing<SeasonProgramsDTOResolver>());
            }

            if (AutoMapper.Mapper.FindTypeMapFor<DTO.Season, Season>() == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.Season, Season>();
            }

            ProgramDataMapper.ConfigureAutoMapper();
#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

        public override Season Map(DTO.Season source, Season destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override DTO.Season Map(Season source, DTO.Season destination)
        {
            DTO.Season retVal = AutoMapper.Mapper.Map(source, destination);

            foreach (DTO.Program currentListItem in retVal.Programs)
            {
                currentListItem.SeasonId = retVal.Id;
            }

            return retVal;
        }
    }
}
