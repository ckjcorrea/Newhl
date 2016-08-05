using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Web.Areas.Admin.Models
{
    /// <summary>
    /// This is a model with the user details
    /// </summary>
    public class LoginHistoryModel
    {
        public LoginHistoryModel()
        {
            this.LoginHistory = new List<LoginAttempt>();
        }

        /// <summary>
        /// Gets or sets the username to get history for.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Gets and sets the login history associated with the user name
        /// </summary>
        public IList<LoginAttempt> LoginHistory { get; set; }
    }
}