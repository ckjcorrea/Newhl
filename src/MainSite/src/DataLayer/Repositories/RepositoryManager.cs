using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// Wraps up instances of the repositories so they can all participate in the same unit of work
    /// </summary>
    public class RepositoryManager : IRepositoryManager
    {
        /// <summary>
        /// The constructor that takes a unit of work as a parameter
        /// </summary>
        /// <param name="unitOfWork">The unit of work</param>
        public RepositoryManager(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// The contained instance of the Unit Of Work
        /// </summary>
        protected UnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// The current instance of the RequestTokenRepository
        /// </summary>
        private IRequestTokenRepository requestTokenRepository;

        /// <summary>
        /// Gets the current instance of the RequestTokenRepository
        /// </summary>
        public IRequestTokenRepository RequestTokenRepository 
        { 
            get
            {
                if (this.requestTokenRepository == null)
                {
                    this.requestTokenRepository = new RequestTokenRepository(this.UnitOfWork);
                }

                return this.requestTokenRepository;
            }
        }

        /// <summary>
        /// The current instance of the ConsumerRepository
        /// </summary>
        private IConsumerRepository consumerRepository;

        /// <summary>
        /// Gets the current instance of the ConsumerRepository
        /// </summary>
        public IConsumerRepository ConsumerRepository
        {
            get
            {
                if (this.consumerRepository == null)
                {
                    this.consumerRepository = new ConsumerRepository(this.UnitOfWork);
                }

                return this.consumerRepository;
            }
        }

        /// <summary>
        /// The current instance of the ConsumerNonceRepository
        /// </summary>
        private IConsumerNonceRepository consumerNonceRepository;

        /// <summary>
        /// Gets the current instance of the ConsumerNonceRepository
        /// </summary>
        public IConsumerNonceRepository ConsumerNonceRepository
        {
            get
            {
                if (this.consumerNonceRepository == null)
                {
                    this.consumerNonceRepository = new ConsumerNonceRepository(this.UnitOfWork);
                }

                return this.consumerNonceRepository;
            }
        }

        /// <summary>
        /// The current instance of the UserRepository
        /// </summary>
        private IAMFUserRepository userRepository;

        /// <summary>
        /// Gets the current instance of the ConsumerNonceRepository
        /// </summary>
        public IAMFUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new AMFUserRepository(this.UnitOfWork);
                }

                return this.userRepository;
            }
        }

        /// <summary>
        /// The current instance of the LoginAttemptRepository
        /// </summary>
        private ILoginAttemptRepository loginAttemptRepository;

        /// <summary>
        /// Gets the current instance of the loginAttemptRepository
        /// </summary>
        public ILoginAttemptRepository LoginAttemptRepository
        {
            get
            {
                if (this.loginAttemptRepository == null)
                {
                    this.loginAttemptRepository = new LoginAttemptRepository(this.UnitOfWork);
                }

                return this.loginAttemptRepository;
            }
        }
    }
}