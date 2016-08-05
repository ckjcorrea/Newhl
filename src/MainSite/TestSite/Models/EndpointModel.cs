using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.OAuth.Common;
using AlwaysMoveForward.OAuth.Contracts;

namespace TestSite.Models
{
    public class EndpointModel : IOAuthEndpoints
    {
        public string ServiceUri { get; set; }

        public string RequestTokenUri { get; set; }

        public string AccessTokenUri { get; set; }

        public string AuthorizationUri { get; set; }
        
        public string TokenValidationUri { get; set; }

        /// <summary>
        /// Get the combined Service Uri and Authorization Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        public string GetFullRequestTokenUri()
        {
            return this.ServiceUri + this.RequestTokenUri;
        }

        /// <summary>
        /// Get the combined Service Uri and Authorization Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        public string GetFullAuthorizationUri()
        {
            return this.ServiceUri + this.AuthorizationUri;
        }

        /// <summary>
        /// Get the combined Service Uri and AccessToken Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        public string GetFullAccessTokenUri()
        {
            return this.ServiceUri + this.AccessTokenUri;
        }

        /// <summary>
        /// Get the combined Service Uri and Token Validation Uri
        /// </summary>
        /// <returns>The combined uri</returns>
        public string GetFullTokenValidationUri()
        {
            return this.ServiceUri + this.TokenValidationUri;
        }
    }
}