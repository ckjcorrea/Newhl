using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Newhl.Common.DomainModel.Poll;
using Newhl.Common.DataLayer;
using Newhl.Common.DataLayer.Repositories;

namespace Newhl.Common.Business
{
    /// <summary>
    /// This is the business layer that provides Polling logic
    /// </summary>
    public class PollService
    {
        /// <summary>
        /// A constructor that takes a unit of work and a poll repository to work with
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="pollRepository"></param>
        public PollService(IUnitOfWork unitOfWork, IPollRepository pollRepository)
        {
            this.UnitOfWork = unitOfWork;
            this.PollRepository = pollRepository;
        }

        private IPollRepository PollRepository { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Get a poll question by a specific Id
        /// </summary>
        /// <param name="pollId"></param>
        /// <returns>A Poll Question</returns>
        public PollQuestion GetById(int pollId)
        {
            return this.PollRepository.GetById(pollId);
        }
        
        /// <summary>
        /// Get all poll questions
        /// </summary>
        /// <returns>A list of Poll Questions</returns>
        public IList<PollQuestion> GetAll()
        {
            return this.PollRepository.GetAll();
        }
        
        /// <summary>
        /// Add a Poll question to storage
        /// </summary>
        /// <param name="questionText"></param>
        /// <param name="title"></param>
        /// <returns>The saved Poll Question</returns>
        public PollQuestion AddPollQuestion(string questionText, string title)
        {
            PollQuestion newQuestion = new PollQuestion();
            newQuestion.QuestionText = questionText;
            newQuestion.Title = title;
            return this.PollRepository.Save(newQuestion);
        }
        
        /// <summary>
        /// Add an answer option to a poll
        /// </summary>
        /// <param name="pollQuestionId"></param>
        /// <param name="optionText"></param>
        /// <returns>The updated Poll Question with the option in it</returns>
        public PollQuestion AddPollOption(int pollQuestionId, string optionText)
        {
            PollQuestion targetQuestion = this.GetById(pollQuestionId);

            if (targetQuestion != null)
            {
                targetQuestion.AddOption(optionText);
                targetQuestion = this.PollRepository.Save(targetQuestion);
            }

            return targetQuestion;
        }
        
        /// <summary>
        /// Update a poll option for a question
        /// </summary>
        /// <param name="pollQuestion"></param>
        /// <returns>The poll question with the updated option</returns>
        public PollQuestion UpdatePollQuestion(PollQuestion pollQuestion)
        {
            return this.PollRepository.Save(pollQuestion);
        }
        
        /// <summary>
        /// Add a vote for a poll option
        /// </summary>
        /// <param name="pollOptionId"></param>
        /// <param name="address"></param>
        /// <returns>The poll question with the additional vote</returns>
        public PollQuestion AddOptionVote(int pollOptionId, IPAddress address)
        {
            PollQuestion pollQuestion = this.PollRepository.GetByPollOptionId(pollOptionId);

            if (pollQuestion != null)
            {
                pollQuestion.AddOptionVote(pollOptionId, address);
                pollQuestion = this.PollRepository.Save(pollQuestion);
            }

            return pollQuestion;
        }

        public bool Delete(PollQuestion pollQuestion)
        {
            return this.PollRepository.Delete(pollQuestion);
        }

        public PollQuestion Save(PollQuestion pollQuestion)
        {
            return this.PollRepository.Save(pollQuestion);
        }
    }
}
