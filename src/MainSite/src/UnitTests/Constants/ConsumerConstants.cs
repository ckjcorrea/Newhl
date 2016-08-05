using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.UnitTests.Constants
{
    /// <summary>
    /// A class to contain consumer constant values used in unit tests
    /// </summary>
    public class ConsumerConstants
    {
        /// <summary>
        /// A default consumer name
        /// </summary>
        public const string TestName = "UnitTestProcess";

        /// <summary>
        /// A default consumer email
        /// </summary>
        public const string TestEmail = "artie@test.com";

        /// <summary>
        /// A default consumer key
        /// </summary>
        public const string TestConsumerKey = "{80a22bea-0bdd-4bee-ad7c-b10c0ae44a3c}";

        /// <summary>
        /// A default nonce
        /// </summary>
        public const string TestNotFoundNonce = "NonFindingNonce";

        /// <summary>
        /// A secret value to test changing the secret
        /// </summary>
        public const string TestUpdatedSecret = "UpdatedSecret";
    }
}
