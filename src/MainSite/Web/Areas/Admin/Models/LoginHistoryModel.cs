using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.Common.Utilities;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Areas.Admin.Models
{
    /// <summary>
    /// This is a model with the user details
    /// </summary>
    public class LoginHistoryModel
    {
        public LoginHistoryModel()
        {
            this.LoginHistory = new PagedList<LoginAttempt>();
        }

        /// <summary>
        /// Gets or sets the username to get history for.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Gets and sets the login history associated with the user name
        /// </summary>
        public IPagedList<LoginAttempt> LoginHistory { get; set; }
    }
}