﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.Common.DataLayer.NHibernate;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.DataLayer.DataMapper;

namespace Newhl.MainSite.DataLayer.Repositories
{
    public interface ILoginAttemptRepository : INHibernateRepository<LoginAttempt, long>
    {
        IList<LoginAttempt> GetByUserName(string userName);
    }
}
