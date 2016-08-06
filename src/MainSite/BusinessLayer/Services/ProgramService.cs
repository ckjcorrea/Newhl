using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.DataLayer.Repositories;


namespace Newhl.MainSite.BusinessLayer.Services
{
    public class ProgramService : IProgramService
    {
        public ProgramService(IProgramRepository programRepository)
        {
            this.ProgramRepository = programRepository;
        }

        protected IProgramRepository ProgramRepository { get; private set; }

        public IList<Program> GetAll(bool activeOnly)
        {
            IList<Program> retVal = null;

            if (activeOnly)
            {
                retVal = this.ProgramRepository.GetAllActive();
            }
            else
            {
                retVal = this.ProgramRepository.GetAll();
            }

            return retVal;
        }
    }
}
