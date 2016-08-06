using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newhl.Common.DataLayer;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.DataLayer.DataMapper
{
    internal class PaymentDTOListResolver : MappedListResolver<Payment, DTO.Payment>
    {
        protected override IList<DTO.Payment> GetDestinationList(ResolutionResult source)
        {
            return ((DTO.AMFUser)source.Context.DestinationValue).Payments;
        }

        protected override IList<Payment> GetSourceList(ResolutionResult source)
        {
            IList<Payment> retVal = null;

            if (source.Value != null)
            {
                retVal = ((AMFUserLogin)source.Value).Payments;
            }

            return retVal;
        }

        protected override DTO.Payment FindItemInList(IList<DTO.Payment> destinationList, Payment searchTarget)
        {
            return destinationList.FirstOrDefault(t => t.Id == searchTarget.Id);
        }

        protected override Payment FindItemInList(IList<Payment> sourceList, DTO.Payment searchTarget)
        {
            return sourceList.FirstOrDefault(t => t.Id == searchTarget.Id);
        }
    }
}
