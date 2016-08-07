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
        public SeasonService(ISeasonRepository seasonRepository)
        {
            this.SeasonRepository = seasonRepository;
        }

        protected ISeasonRepository SeasonRepository { get; private set; }

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
    }
}
