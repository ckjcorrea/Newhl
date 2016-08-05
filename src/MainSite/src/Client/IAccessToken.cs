using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// The minimum access token information requried
    /// </summary>
    public interface IAccessToken : IOAuthToken
    {
        /// <summary>
        /// Each Access token should have a Realm associated with it
        /// </summary>
        Realm Realm { get; set; }
    }
}
