using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newhl.MainSite.DataLayer.Repositories
{
    /// <summary>
    /// The common interface for the repository manager.  This makes mocking easier
    /// </summary>
    public interface IRepositoryManager
    {        
        /// <summary>
        /// Gets the current instance of the AMFUserRepository
        /// </summary>
        IAMFUserRepository UserRepository { get; }

        /// <summary>
        /// Gets the current instance of the LoginAttemptRepository
        /// </summary>
        ILoginAttemptRepository LoginAttemptRepository { get; }

        IProgramRepository ProgramRepository { get; }
    }
}