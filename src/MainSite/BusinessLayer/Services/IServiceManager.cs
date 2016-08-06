using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.MainSite.Common.Factories;
using Newhl.MainSite.DataLayer;

namespace Newhl.MainSite.BusinessLayer.Services
{
    /// <summary>
    /// Contains the current instances of the business services
    /// </summary>
    public interface IServiceManager
    {
        /// <summary>
        /// The current unit of work
        /// </summary>
        UnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets the current User service instance
        /// </summary>
        IUserService UserService { get; }

        IEmailService EmailService { get; }

        IPaymentService PaymentService { get; }
    }
}

