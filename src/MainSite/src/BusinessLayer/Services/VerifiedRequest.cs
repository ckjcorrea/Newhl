using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.BusinessLayer.Services
{
    /// <summary>
    /// This object represents a verified request token
    /// </summary>
    public class VerifiedRequest
    {
        /// <summary>
        /// Gets or sets the callback url
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the verifier code
        /// </summary>
        public string VerifierCode { get; set; }
    }
}
