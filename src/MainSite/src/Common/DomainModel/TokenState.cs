using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Common.DomainModel
{
    /// <summary>
    /// The current state of a request token
    /// </summary>
    public enum TokenState
    {
        /// <summary>
        /// The token state is unknown
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Access has been denied for this token
        /// </summary>
        AccessDenied = 1,
        
        /// <summary>
        /// Access has been granted for this token
        /// </summary>
        AccessGranted = 2,
    }
}
