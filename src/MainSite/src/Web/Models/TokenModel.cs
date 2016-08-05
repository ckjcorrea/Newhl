using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlwaysMoveForward.OAuth.Web.Models
{
    /// <summary>
    /// A model for returning a token key and secret
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Gets or sets the application name associated with the consumer
        /// </summary>
        public string ConsumerName { get; set; }

        /// <summary>
        /// Gets or sets the token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the token's secret
        /// </summary>
        public string Secret { get; set; }
    }
}