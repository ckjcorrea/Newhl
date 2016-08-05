using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.UnitTests.Constants
{
    /// <summary>
    /// Constant values used when unit testing tokens
    /// </summary>
    public class TokenConstants
    {
        /// <summary>
        /// A default realm to use
        /// </summary>
        public static Realm TestRealm
        {
            get
            {
                return new Realm() { Area = "Digital", Service = "OAuth", DataId = UserConstants.TestUserId.ToString(), DataName = UserConstants.TestUserName };
            }
        }

        /// <summary>
        /// A default request token
        /// </summary>
        public const string TestRequestToken = "b8c20715-1a32-4c81-9a73-5c9a38770c41";

        /// <summary>
        /// A default request token secret
        /// </summary>
        public const string TestRequestTokenSecret = "3643c9e5-826b-4786-9cc2-17f907e52a69";

        /// <summary>
        /// A default access token
        /// </summary>
        public const string TestAccessToken = "e2b6c5f2-072a-493e-8aa0-2b1d5671e601";
        
        /// <summary>
        /// A default access token secret
        /// </summary>
        public const string TestAccessTokenSecret = "166e829e-1e06-45ae-9b6e-cb6e9faf1372";

        /// <summary>
        /// A default verifier code
        /// </summary>
        public const string TestVerifierCode = "1234";

        /// <summary>
        /// A default request token to tell Mock to generate a full RequestToken Aggregate object
        /// </summary>
        public const string TestRequestTokenWithAuthorization = "997BD82F-65CE-49D1-8764-52A74C6B5FF1";

        /// <summary>
        /// A secret to go with the Full RequestToken Aggregate request token
        /// </summary>
        public const string TestRequestTokenSecretWithAuthorization = "19C02FE3-E637-4DB4-A25E-254218E7FE5A";
        
        /// <summary>
        /// A default request token to tell Mock to generate a full RequestToken Aggregate object
        /// </summary>
        public const string TestRequestTokenWithAccessToken = "3643c9e5-826b-4786-9cc2-17f907e52a69";

        /// <summary>
        /// A secret to go with the Full RequestToken Aggregate request token
        /// </summary>
        public const string TestRequestTokenSecretWithAccessToken = "3643c9e5-826b-4786-9cc2-17f907e52a69";

        /// <summary>
        /// A default callback Url
        /// </summary>
        public const string TestCallbackUrl = "http://localhost/OAuth/Callback";
    }
}
