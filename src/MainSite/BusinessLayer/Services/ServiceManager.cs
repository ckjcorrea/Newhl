using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.Common.DataLayer;
using Newhl.Common.Security;
using Newhl.Common.Utilities;
using Newhl.MainSite.Common.Factories;
using Newhl.MainSite.DataLayer;
using Newhl.MainSite.DataLayer.Repositories;

namespace Newhl.MainSite.BusinessLayer.Services
{
    /// <summary>
    /// The service manager for the OAuth services
    /// </summary>
    public class ServiceManager : IServiceManager
    {
        /// <summary>
        /// The constructor for the manager
        /// </summary>
        /// <param name="repositoryManager">The container for the repositories used by this classes services</param>
        public ServiceManager(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            this.RepositoryManager = repositoryManager;

            this.UnitOfWork = unitOfWork as UnitOfWork;
        }

        /// <summary>
        /// Gets the current Unit Of Work
        /// </summary>
        public UnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets the repository container
        /// </summary>
        private IRepositoryManager RepositoryManager { get; set; }

        /// <summary>
        /// The service containing the Token business rules
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// Gets the current Token service
        /// </summary>
        public IUserService UserService
        {
            get
            {
                if (this.userService == null)
                {
                    this.userService = new UserService(this.RepositoryManager.UserRepository, this.RepositoryManager.LoginAttemptRepository);
                }

                return this.userService;
            }
        }

        private IEmailService emailService;
        public IEmailService EmailService
        {
            get
            {
                if (this.emailService == null)
                {
                    this.emailService = new EmailService();
                }

                return this.emailService;
            }
        }

        private IPaymentService paymentService;

        public IPaymentService PaymentService
        {
            get
            {
                if(this.paymentService == null)
                {
                    this.paymentService = new PaymentService(this.RepositoryManager.PaymentRepository, this.RepositoryManager.UserRepository);
                }

                return this.paymentService;
            }
        }

        private ISeasonService seasonService;

        public ISeasonService SeasonService
        {
            get
            {
                if (this.seasonService == null)
                {
                    this.seasonService = new SeasonService(this.RepositoryManager.SeasonRepository, this.RepositoryManager.PlayerSeasonRepository);
                }

                return this.seasonService;
            }
        }
        
    }
}
