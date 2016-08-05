using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newhl.MainSite.DataLayer.Repositories
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
        /// Gets the current instance of the ConsumerNonceRepository
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