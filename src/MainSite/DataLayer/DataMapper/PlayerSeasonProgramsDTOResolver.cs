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
    class PlayerSeasonProgramsDTOResolver : MappedListResolver<Program, DTO.Program>
    {
        protected override IList<DTO.Program> GetDestinationList(ResolutionResult source)
        {
            return ((DTO.PlayerSeason)source.Context.DestinationValue).Programs;
        }

        protected override IList<Program> GetSourceList(ResolutionResult source)
        {
            IList<Program> retVal = null;

            if (source.Value != null)
            {
                retVal = ((PlayerSeason)source.Value).Programs;
            }

            return retVal;
        }

        protected override DTO.Program FindItemInList(IList<DTO.Program> destinationList, Program searchTarget)
        {
            return destinationList.FirstOrDefault(t => t.Id == searchTarget.Id);
        }

        protected override Program FindItemInList(IList<Program> sourceList, DTO.Program searchTarget)
        {
            return sourceList.FirstOrDefault(t => t.Id == searchTarget.Id);
        }
    }
}
