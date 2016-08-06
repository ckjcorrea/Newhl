using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.Common.DataLayer;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.DataLayer.DataMapper
{
    public class PaymentDataMapper : DataMapBase<Payment, DTO.Payment>
    {
        static PaymentDataMapper()
        {
            PaymentDataMapper.ConfigureAutoMapper();
        }

        internal static void ConfigureAutoMapper()
        {
            if (AutoMapper.Mapper.FindTypeMapFor<Payment, DTO.Payment>() == null)
            {
                AutoMapper.Mapper.CreateMap<Payment, DTO.Payment>()
                    .ForMember(dest => dest.Player, opt => opt.Ignore());
            }

            if (AutoMapper.Mapper.FindTypeMapFor<DTO.Payment, Payment>() == null)
            {
                AutoMapper.Mapper.CreateMap<DTO.Payment, Payment>();
            }

#if DEBUG
            AutoMapper.Mapper.AssertConfigurationIsValid();
#endif
        }

        public override Payment Map(DTO.Payment source, Payment destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public override DTO.Payment Map(Payment source, DTO.Payment destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
