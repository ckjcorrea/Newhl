using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using DD = DevDefined.OAuth.Framework;
using DDS = DevDefined.OAuth.Storage;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Client.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.UnitTests.Constants;

namespace AlwaysMoveForward.OAuth.UnitTests.BusinessLayer
{
    [TestFixture]
    public class TokenServiceTests : UnitTestBase
    {
        #region ITokenStore

        private RequestToken CreateRequestToken(bool usedUp)
        {
            RequestToken retVal = new RequestToken();
            retVal.ConsumerKey = ConsumerConstants.TestConsumerKey;
            retVal.Realm = TokenConstants.TestRealm;
            retVal.Token = TokenConstants.TestRequestToken;
            retVal.Secret = TokenConstants.TestRequestTokenSecret;

            return retVal;
        }

        [Test]
        public void TokenServiceTestConsumeRequestToken()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.Token = TokenConstants.TestRequestToken;

            bool exceptionThrown = false;

            try
            {
                this.ServiceManager.TokenService.ConsumeRequestToken(requestContext);
            }
            catch (Exception e)
            {
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }

        [Test]
        public void TokenServiceTestConsumeRequestTokenNullContext()
        {
            bool exceptionThrown = false;

            try
            {
                this.ServiceManager.TokenService.ConsumeRequestToken(null);
            }
            catch (ArgumentNullException e)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [Test]
        public void TokenServiceTestConsumeAccessToken()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.Token = TokenConstants.TestRequestTokenWithAccessToken;

            bool exceptionThrown = false;

            try
            {
                this.ServiceManager.TokenService.ConsumeAccessToken(requestContext);
            }
            catch (Exception e)
            {
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }

        [Test]
        public void TokenServiceTestCreateAccessToken()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.Token = TokenConstants.TestRequestTokenWithAuthorization;
            requestContext.TokenSecret = TokenConstants.TestRequestTokenSecretWithAuthorization;
            requestContext.Verifier = TokenConstants.TestVerifierCode;

            DD.IToken testItem = this.ServiceManager.TokenService.CreateAccessToken(requestContext);
            
            Assert.IsNotNull(testItem);
            Assert.IsTrue(!string.IsNullOrEmpty(testItem.Token));
            Assert.IsTrue(!string.IsNullOrEmpty(testItem.TokenSecret));
        }

        [Test]
        public void ConsumerServiceTestCreateAccessTokenNullContext()
        {
            bool exceptionThrown = false;

            try
            {
                DD.IToken testItem = this.ServiceManager.TokenService.CreateAccessToken(null);
            }
            catch (ArgumentNullException e)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [Test]
        public void ConsumerServiceTestCreateRequestToken()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.ConsumerKey = ConsumerConstants.TestConsumerKey;
            requestContext.Realm = TokenConstants.TestRealm.ToString();

            DD.IToken testItem = this.ServiceManager.TokenService.CreateRequestToken(requestContext);
            
            Assert.IsNotNull(testItem);
            Assert.IsTrue(!string.IsNullOrEmpty(testItem.Token));
            Assert.IsTrue(!string.IsNullOrEmpty(testItem.TokenSecret));
        }

        [Test]
        public void ConsumerServiceTestCreateRequestTokenNullContext()
        {
            bool exceptionThrown = false;

            try
            {
                DD.IToken testItem = this.ServiceManager.TokenService.CreateRequestToken(null);
            }
            catch (ArgumentNullException e)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [Test]
        public void TokenServiceTestGetAccessTokenAssociatedWithRequestToken()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.ConsumerKey = ConsumerConstants.TestConsumerKey;
            requestContext.Token = TokenConstants.TestRequestTokenWithAuthorization;
            requestContext.TokenSecret = TokenConstants.TestRequestTokenSecretWithAuthorization;
            requestContext.Verifier = TokenConstants.TestVerifierCode;
            requestContext.Realm = TokenConstants.TestRealm.ToString();

            DD.IToken testItem = this.ServiceManager.TokenService.GetAccessTokenAssociatedWithRequestToken(requestContext);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(!string.IsNullOrEmpty(testItem.Token));
            Assert.IsTrue(!string.IsNullOrEmpty(testItem.TokenSecret));
        }

        [Test]
        public void TokenServiceTestGetAccessTokenAssociatedWithRequestTokenNullContext()
        {            
            DD.IToken testItem = this.ServiceManager.TokenService.GetAccessTokenAssociatedWithRequestToken(null);

            Assert.IsNull(testItem);
        }

        [Test]
        public void TokenServiceTestGetAccessTokenSecret()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.ConsumerKey = ConsumerConstants.TestConsumerKey;
            requestContext.Token = TokenConstants.TestRequestTokenWithAccessToken;

            string testItem = this.ServiceManager.TokenService.GetAccessTokenSecret(requestContext);

            Assert.IsTrue(!string.IsNullOrEmpty(testItem));
            Assert.IsTrue(testItem == TokenConstants.TestAccessTokenSecret);
        }

        [Test]
        public void TokenServiceTestGetAccessTokenSecretNullContext()
        {
            string testItem = this.ServiceManager.TokenService.GetAccessTokenSecret(null);            
            Assert.IsTrue(string.IsNullOrEmpty(testItem));
        }

        [Test]
        public void TokenServiceTestGetRequestTokenSecret()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.ConsumerKey = ConsumerConstants.TestConsumerKey;
            requestContext.Token = TokenConstants.TestRequestToken;

            string testItem = this.ServiceManager.TokenService.GetRequestTokenSecret(requestContext);

            Assert.IsTrue(!string.IsNullOrEmpty(testItem));
            Assert.IsTrue(testItem == TokenConstants.TestRequestTokenSecret);
        }

        [Test]
        public void TokenServiceTestGetRequestTokenSecretNullContext()
        {
            string testItem = this.ServiceManager.TokenService.GetRequestTokenSecret(null);           
            Assert.IsTrue(string.IsNullOrEmpty(testItem));
        }

        [Test]
        public void TokenServiceTestGetCallbackUrlForToken()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.ConsumerKey = ConsumerConstants.TestConsumerKey;
            requestContext.Token = TokenConstants.TestRequestToken;

            string testItem = this.ServiceManager.TokenService.GetCallbackUrlForToken(requestContext);

            Assert.IsTrue(!string.IsNullOrEmpty(testItem));
            Assert.IsTrue(testItem == TokenConstants.TestCallbackUrl);
        }

        [Test]
        public void TokenServiceTestGetCallbackUrlForTokenNullContext()
        {
            string testItem = this.ServiceManager.TokenService.GetCallbackUrlForToken(null);
            Assert.IsTrue(string.IsNullOrEmpty(testItem));
        }

        [Test]
        public void TokenServiceTestGetStatusOfRequestForAccess()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.ConsumerKey = ConsumerConstants.TestConsumerKey;
            requestContext.Token = TokenConstants.TestRequestToken;

            DDS.RequestForAccessStatus status = this.ServiceManager.TokenService.GetStatusOfRequestForAccess(requestContext);

            Assert.IsNotNull(status);
        }

        [Test]
        public void TokenServiceTestGetStatusOfRequestForAccessNullContext()
        {
            DDS.RequestForAccessStatus status = this.ServiceManager.TokenService.GetStatusOfRequestForAccess(null);
            Assert.IsTrue(status == DDS.RequestForAccessStatus.Unknown);
        }

        [Test]
        public void TokenServiceTestGetVerificationCodeForRequestToken()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.ConsumerKey = ConsumerConstants.TestConsumerKey;
            requestContext.Token = TokenConstants.TestRequestTokenWithAccessToken;

            string testItem = this.ServiceManager.TokenService.GetVerificationCodeForRequestToken(requestContext);

            Assert.IsTrue(!string.IsNullOrEmpty(testItem));
            Assert.IsTrue(testItem == TokenConstants.TestVerifierCode);
        }

        [Test]
        public void TokenServiceTestGetVerificationCodeForRequestTokenNullContext()
        {
            string testItem = this.ServiceManager.TokenService.GetVerificationCodeForRequestToken(null);
            Assert.IsTrue(string.IsNullOrEmpty(testItem));
        }

        #endregion

        [Test]
        public void TokenServiceTestGetAccessTokenByString()
        {
            AccessToken testItem = this.ServiceManager.TokenService.GetAccessToken(TokenConstants.TestAccessToken);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Token == TokenConstants.TestAccessToken);
        }      

        [Test]
        public void TokenServiceTestExchangeRequestTokenForAccessToken()
        {
            AccessToken testItem = this.ServiceManager.TokenService.ExchangeRequestTokenForAccessToken(TokenConstants.TestRequestTokenWithAuthorization, TokenConstants.TestVerifierCode);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(!string.IsNullOrEmpty(testItem.Token));
            Assert.IsTrue(!string.IsNullOrEmpty(testItem.Secret));
        }


        [Test]
        public void TokenServiceDenyRequestToken()
        {
            DD.IOAuthContext requestContext = new DD.OAuthContext();
            requestContext.ConsumerKey = ConsumerConstants.TestConsumerKey;
            requestContext.Token = TokenConstants.TestRequestToken;

            RequestToken testItem = this.ServiceManager.TokenService.DenyRequestToken(TokenConstants.TestRequestToken);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.State == TokenState.AccessDenied);
        }

        [Test]
        public void TokenServiceDenyRequestTokenNullContext()
        {
            RequestToken testItem = this.ServiceManager.TokenService.DenyRequestToken(null);
            Assert.IsNull(testItem);
        }
    }
}
