using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Client.OpenAuth
{
    /// <summary>
    /// A class to contain constant strings defining OAuth paraemters
    /// </summary>
    public class OAuthParameters
    {
        /// <summary>
        /// The oauth request token parameter name
        /// </summary>
        public const string RequestToken = "request_token";

        /// <summary>
        /// The oauth request token parameter name (there are two because platform was using both)
        /// </summary>
        public const string RequestToken2 = "requesttoken";

        /// <summary>
        /// The parameter name for the api key (consumer key)
        /// </summary>
        public const string ApiKey = "api_key";

        /// <summary>
        /// The parameter name for the api key  (there are two because platform was using both)
        /// </summary>
        public const string ApiKey2 = "apikey";

        /// <summary>
        /// The parameter name for the timestamp
        /// </summary>
        public const string Timestamp = "ts";

        /// <summary>
        /// The Auth Header parameter name
        /// </summary>
        public const string Auth = "auth";

        /// <summary>
        /// The Uri for the response callback
        /// </summary>
        public const string ResponseUri = "response_uri";

        /// <summary>
        /// The uri for faiure callbacks
        /// </summary>
        public const string FailureUri = "failure_uri";

        /// <summary>
        /// The oauth parameter for the user id
        /// </summary>
        public const string UserId = "id";

        /// <summary>
        /// The oauth parameter for the user name
        /// </summary>
        public const string UserName = "username";

        /// <summary>
        /// The oauth paramater for the access token
        /// </summary>
        public const string AccessToken = "accesstoken";
    }
}
