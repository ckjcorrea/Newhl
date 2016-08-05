using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newhl.Common.DomainModel.Poll
{
    public class PollOption
    {
        public PollOption()
        {
            this.Id = -1;
        }

        public int Id { get; set; }
        public string OptionText { get; set; }
        public IList<VoterAddress> VoterAddresses { get; set; }
    }
}
