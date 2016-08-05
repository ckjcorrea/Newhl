using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// A class representing an OAUth consumer
    /// </summary>
    public class Consumer
    {
        /// <summary>
        /// A default access token lifetime of 5 years.
        /// </summary>
        public const int DefaultAccessTokenLifetime = 7800;

        /// <summary>
        /// A default constructor for the class
        /// </summary>
        public Consumer()
        {
            this.AccessTokenLifetime = DefaultAccessTokenLifetime;
        }

        /// <summary>
        /// Gets or sets the consumer key
        /// </summary>
        public String ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the consumer secret
        /// </summary>
        public String ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets the name of the consumer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Public Key
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// Gets or sets if the access tokens should be auto granted
        /// </summary>
        public bool AutoGrant { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets how many hours before an access token expires for this consumer
        /// </summary>
        public int AccessTokenLifetime { get; set; }
    }
}
