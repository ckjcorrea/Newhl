using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// This class represents a login attempt for a given user id
    /// </summary>
    public class LoginAttempt
    {
        /// <summary>
        /// Gets or sets the database id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the source of the login attempt
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets if the login attempt was successfull
        /// </summary>
        public bool WasSuccessfull { get; set; }

        /// <summary>
        /// Gets or sets the date of the login attempt
        /// </summary>
        public DateTime AttemptDate { get; set; }

        /// <summary>
        /// Gets or sets the current user associated with this login attempt
        /// </summary>
        public string UserName { get; set; }
    }
}
