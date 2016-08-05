using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.Web.Areas.Admin.Models
{
    public class OAuthTokensModel
    {
        public OAuthTokensModel()
        {
            this.Tokens = new List<RequestToken>();
        }

        /// <summary>
        /// Gets or sets the username to get history for.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the consumer key to get history for
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the search start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the search end date
        /// </summary>
        public DateTime EndDate { get; set;}

        /// <summary>
        /// Gets and sets the login history associated with the user name
        /// </summary>
        public IList<RequestToken> Tokens { get; set; }
    }
}