using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newhl.Common.DomainModel.Poll;

namespace Newhl.Common.DataLayer.Repositories
{
    public interface IPollRepository : IRepository<PollQuestion, int>
    {
        PollQuestion GetByPollOptionId(int pollOptionId);
    }
}
