using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.UnitTests.Constants
{
    /// <summary>
    /// Constant values used in unit tests
    /// </summary>
    public class UserConstants
    {
        /// <summary>
        /// The default user id to use in unit tests
        /// </summary>
        public const int TestUserId = 0;

        /// <summary>
        /// The default username to use in unit tests
        /// </summary>
        public const string TestUserName = "artie@test.com";

        /// <summary>
        /// The default password to use in unit tests
        /// </summary>
        public const string TestPassword = "teststring";

        /// <summary>
        /// The default salt to use in unit tests
        /// </summary>
        public const string TestSalt = "t8Oipwx4MnV9A+oVlXG1wKXeirWqzuBv";

        /// <summary>
        /// The default hashed  password using the Test Salt
        /// </summary>
        public const string HashedPasswordWithDefaultSalt = "lfcTgtQr7OlaNF1YmrazuYTS5fCyASGT";

        /// <summary>
        /// The default password hint used for testing.
        /// </summary>
        public const string TestPasswordHint = "PasswordHint";
    }
}
