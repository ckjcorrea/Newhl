using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Client;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// The user and realm associated with a validated token
    /// </summary>
    public class ValidatedToken
    {
        /// <summary>
        /// Gets or sets the OAuth token and secret validated
        /// </summary>
        public AccessToken OAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the Realm associated with the token
        /// </summary>
        public Realm Realm { get; set; }
    }
}
