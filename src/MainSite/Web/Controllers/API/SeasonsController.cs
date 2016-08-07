using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newhl.Common.DomainModel;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.Web.Code.Filters;
using Newhl.MainSite.Web.Models;
using Newhl.MainSite.Web.Models.API;

namespace Newhl.MainSite.Web.Controllers.API
{
    public class SeasonsController : BaseAPIController
    {
        [Route("api/Seasons"), HttpGet()]
        [WebApiAuthorization]
        public IList<Season> Get(bool? activeOnly)
        {
            IList<Season> retVal = null;

            if (activeOnly.HasValue)
            {
                retVal = this.Services.SeasonService.GetAll(activeOnly.Value);
            }
            else
            {
                retVal = this.Services.SeasonService.GetAll(false);
            }

            return retVal;
        }

        [Route("api/Season/{id}"), HttpGet()]
        [WebApiAuthorization]
        public Season Get(long id)
        {
            return this.Services.SeasonService.GetById(id);
        }


        [Route("api/Display/Season/{id}"), HttpGet()]
        [WebApiAuthorization]
        public DisplaySeasonModel GetDisplay(long id)
        {
            DisplaySeasonModel retVal = new DisplaySeasonModel();
            PlayerSeason playerSeason = this.Services.SeasonService.GetPlayerSeason(this.CurrentPrincipal.User.Id, id);
            IList<Program> seasonPrograms = null;

            if(playerSeason!=null)
            {
                seasonPrograms = playerSeason.Programs;
            }

            retVal.Initialize(this.Services.SeasonService.GetById(id), seasonPrograms);
            return retVal;
        }

        [Route("api/Season/{id}/Programs/Update"), HttpPut()]
        [WebApiAuthorization]
        public void UpdatePrograms(long id, [FromBody] DisplaySeasonModel input)
        {
            IList<Season> activeSeasons = this.Services.SeasonService.GetAll(true);

            IList<long> programsToAdd = new List<long>();

            if(input != null && input.Programs != null)
            {
                foreach (DisplayProgramModel program in input.Programs)
                {
                    if(program.IsSelected == true)
                    {
                        programsToAdd.Add(program.Id);
                    }
                }
            }

            this.Services.SeasonService.UpdateSeasonPrograms(this.CurrentPrincipal.User.Id, id, programsToAdd);            
        }
    }
}
