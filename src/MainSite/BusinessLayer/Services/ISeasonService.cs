using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.BusinessLayer.Services
{
    public interface ISeasonService
    {
        IList<Season> GetAll(bool activeOnly);

        Season GetById(long id);
    }
}
