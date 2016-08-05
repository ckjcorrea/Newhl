using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// A class representing a nonce
    /// </summary>
    public class ConsumerNonce
    {
        /// <summary>
        /// Gets or sets the consumer associated with this nonce
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the nonce value
        /// </summary>
        public string Nonce { get; set; }

        /// <summary>
        /// Gets or sets the Date time this was created
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
