using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// A default implmentation of IOAuthToken, used mainly to pass a token and secret to/from classes.
    /// </summary>
    public class DefaultOAuthToken : IOAuthToken
    {
        /// <summary>
        /// A request token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// A request token secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Determine if the oauth token has a token and secret value
        /// </summary>
        /// <returns>True if it has both values</returns>
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.Token) && !string.IsNullOrEmpty(this.Secret);
        }
    }
}
