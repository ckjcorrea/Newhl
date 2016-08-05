using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// This class will contain constant strings used in the OAuth process
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// This string defines the cookie name used to pass the encrypted user information across from the proxy
        /// </summary>
        public static readonly string ProxyUserCookieName = "ProxyUser";

        /// <summary>
        /// The callback parameter for get request token can't be null, so give it a value when we don't want to 
        /// actually do a callback but rather inline the authorization
        /// </summary>
        public static readonly string InlineCallback = "urn:inline";

        /// <summary>
        /// The parameter used to pass and receive an oauth token
        /// </summary>
        public static readonly string TokenParameter = "oauth_token";

        /// <summary>
        /// The parameter used to pass and receive an oauth token secret
        /// </summary>
        public const string TokenSecretParameter = "oauth_token_secret";

        /// <summary>
        /// The default signature method, hmac 1
        /// </summary>
        public static readonly string HmacSha1SignatureMethod = "HMAC-SHA1";

        /// <summary>
        /// A paremeter for passing a verifier code
        /// </summary>
        public static readonly string VerifierCodeParameter = "oauth_verifier";

        /// <summary>
        /// A parameter for returning if access was granted.
        /// </summary>
        public static readonly string GrantedAccessParameter = "grantedAccess";

        /// <summary>
        /// A parameter for the oauth authroization header name
        /// </summary>
        public static readonly string AuthorizationHeader = "Authorization";
    }

}
