using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// This class groups together the verifier code and if the grant was successfull
    /// </summary>
    public class TokenVerification
    {
        /// <summary>
        /// Gets or sets the request token verifier code
        /// </summary>
        public string VerifierCode { get; set; }

        /// <summary>
        /// Gets or sets if the token authorizaiton was granted
        /// </summary>
        public bool Granted { get; set; }
    }
}
