using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Newhl.Common.DomainModel.Poll
{
    public class PollQuestion
    {
        public PollQuestion()
        {
            this.Id = -1;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public IList<PollOption> Options { get; set; }

        public PollOption AddOption(string optionText)
        {
            PollOption newItem = new PollOption();
            newItem.OptionText = optionText;

            if(this.Options == null)
            {
                this.Options = new List<PollOption>();
            }

            this.Options.Add(newItem);
            return newItem;
        }

        public void RemoveOption(int optionId)
        {
            PollOption optionToRemove = (from targetOption in this.Options
                                                where targetOption.Id == optionId
                                                select targetOption).Single();

            if(optionToRemove != null)
            {
                this.Options.Remove(optionToRemove);
            }
        }
        /// <summary>
        /// Add a vote for a poll option
        /// </summary>
        /// <param name="pollOptionId"></param>
        /// <param name="address"></param>
        /// <returns>The poll question with the additional vote</returns>
        public void AddOptionVote(int pollOptionId, IPAddress address)
        {
            PollOption previousVote = (from targetOption in this.Options 
                                                    where targetOption.VoterAddresses.Any(var => var.Address == address)
                                                    select targetOption).Single();

            if (previousVote != null)
            {
                VoterAddress voterAddress = (from addressItem in previousVote.VoterAddresses where addressItem.Address == address select addressItem).Single();
                previousVote.VoterAddresses.Remove(voterAddress);
            }

            PollOption votedOption = (from targetOption in this.Options where targetOption.Id == pollOptionId select targetOption).First();

            if (votedOption != null)
            {
                votedOption.VoterAddresses.Add(new VoterAddress(address));
            }
        }
    }
}
