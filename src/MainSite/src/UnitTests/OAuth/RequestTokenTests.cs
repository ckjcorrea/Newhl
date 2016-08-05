using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.UnitTests.Constants;

namespace AlwaysMoveForward.OAuth.UnitTests.OAuth
{
    [TestFixture]
    public class RequestTokenTests
    {
        private RequestToken CreateRequestToken(bool usedUp)
        {
            RequestToken retVal = new RequestToken();
            retVal.ConsumerKey = ConsumerConstants.TestConsumerKey;
            retVal.Realm = TokenConstants.TestRealm;
            retVal.Token = TokenConstants.TestRequestToken;
            retVal.Secret = TokenConstants.TestRequestTokenSecret;
            retVal.CallbackUrl = "http://localhost/oauth/callback";
            retVal.VerifierCode = RequestTokenAuthorizer.GenerateVerifierCode();
            retVal.DateAuthorized = DateTime.UtcNow;

            return retVal;
        }

        [Test]
        public void RequestTokenGenerateCallbackTest()
        {
            RequestToken testToken = this.CreateRequestToken(false);

            string callbackUrl = testToken.GenerateCallBackUrl();

            Assert.IsTrue(callbackUrl.Contains(testToken.Token));
            Assert.IsTrue(callbackUrl.Contains(testToken.VerifierCode));
        }
    }
}
