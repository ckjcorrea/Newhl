using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.Common.DataLayer;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.DataLayer.DataMapper
{
    internal class ProgramDataMapper : DataMapBase<Program, DTO.Program>
    {
        static ProgramDataMapper()
        {
            ProgramDataMapper.ConfigureAutoMapper();
        }

        internal static void ConfigureAutoMapper()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<Program, DTO.Program>() == null)
            {
                AutoMapper.Mapper.CreateMap<Program, DTO.Program>()
                    .ForMember(va => va.SeasonId, opt => opt.Ignore());
            }

            if (AutoMapper.Mapper.FindTypeMapFor<DTO.Program, Program>() == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.Program, Program>();
            }

#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

        public override Program Map(DTO.Program source, Program destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override DTO.Program Map(Program source, DTO.Program destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
