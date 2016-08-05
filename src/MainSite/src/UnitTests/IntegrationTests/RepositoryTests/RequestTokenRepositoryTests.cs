using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Client.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.UnitTests.Constants;

namespace AlwaysMoveForward.OAuth.DevDefined.UnitTests.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class RequestTokenRepositoryTests : RepositoryTestBase
    {
        private RequestToken CreateTestRequestToken(string token, string tokenSecret)
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();

            RequestToken retVal = new RequestToken();
            retVal.ConsumerKey = keyConfiguration.ConsumerKey;
            retVal.Realm = TokenConstants.TestRealm;
            retVal.Token = token;
            retVal.Secret = tokenSecret;
            retVal.CallbackUrl = TokenConstants.TestCallbackUrl;
            return retVal;
        }

        [Test]
        public void RequestTokenRepositoryTestGetAll()
        {
            IList<RequestToken> foundItems = this.RepositoryManager.RequestTokenRepository.GetAll();

            Assert.IsNotNull(foundItems);
        }

        [Test]
        public void RequestTokenRepositoryTestGetByConsumerKey()
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();
            IList<RequestToken> foundItems = this.RepositoryManager.RequestTokenRepository.GetByConsumerKey(keyConfiguration.ConsumerKey, DateTime.UtcNow.AddYears(-1), DateTime.UtcNow);

            if (foundItems != null && foundItems.Count == 0)
            {
                RequestToken newToken = this.CreateTestRequestToken(Guid.NewGuid().ToString("N"), Guid.NewGuid().ToString("N"));
                this.RepositoryManager.RequestTokenRepository.Save(newToken);
            }

            foundItems = this.RepositoryManager.RequestTokenRepository.GetByConsumerKey(keyConfiguration.ConsumerKey, DateTime.UtcNow.AddYears(-1), DateTime.UtcNow);

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
            Assert.IsTrue(foundItems[0].ConsumerKey == keyConfiguration.ConsumerKey);
        }

        [Test]
        public void RequestTokenRepositoryTestGetByToken()
        {
            RequestToken foundItem = this.RepositoryManager.RequestTokenRepository.GetByToken(TokenConstants.TestRequestToken);

            if (foundItem == null)
            {
                foundItem = this.CreateTestRequestToken(TokenConstants.TestRequestToken, Guid.NewGuid().ToString("N"));
                foundItem = this.RepositoryManager.RequestTokenRepository.Save(foundItem);
            }

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Token == TokenConstants.TestRequestToken);
        }

        [Test]
        public void RequestTokenRepositoryTestGetByTokenAndVerifierCode()
        {
            RequestToken foundItem = this.RepositoryManager.RequestTokenRepository.GetByTokenAndVerifierCode(TokenConstants.TestRequestToken, TokenConstants.TestVerifierCode);

            if (foundItem == null)
            {
                foundItem = this.RepositoryManager.RequestTokenRepository.GetByToken(TokenConstants.TestRequestToken);

                if (foundItem != null)
                {
                    if (foundItem.IsAuthorized() == false)
                    {
                        Realm targetRealm = foundItem.Realm;
                        foundItem.DateAuthorized = DateTime.UtcNow;
                        foundItem.VerifierCode = TokenConstants.TestVerifierCode;

                        using (this.UnitOfWork.BeginTransaction())
                        {
                            this.RepositoryManager.RequestTokenRepository.Save(foundItem);
                            this.UnitOfWork.EndTransaction(true);
                        }
                    }
                }
            }

            foundItem = this.RepositoryManager.RequestTokenRepository.GetByTokenAndVerifierCode(TokenConstants.TestRequestToken, TokenConstants.TestVerifierCode);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Token == TokenConstants.TestRequestToken);
            Assert.IsTrue(foundItem.IsAuthorized());
            Assert.IsTrue(foundItem.VerifierCode == TokenConstants.TestVerifierCode);
        }

        [Test]
        public void RequestTokenRepositoryTestSave()
        {
            RequestToken foundItem = this.RepositoryManager.RequestTokenRepository.GetByToken(TokenConstants.TestRequestToken);
            String uniqueSecret = Guid.NewGuid().ToString("N");

            if (foundItem == null)
            {
                foundItem = this.CreateTestRequestToken(TokenConstants.TestRequestToken, uniqueSecret);
            }
            else
            {
                foundItem.Secret = uniqueSecret;
            }

            using (this.UnitOfWork.BeginTransaction())
            {
                foundItem = this.RepositoryManager.RequestTokenRepository.Save(foundItem);
                this.UnitOfWork.EndTransaction(true);
            }

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Secret == uniqueSecret);
        }

        [Test]
        public void RequestTokenRepositoryTestGetAccessTokenByRequestToken()
        {
            RequestToken requestToken = this.RepositoryManager.RequestTokenRepository.GetByToken(TokenConstants.TestRequestToken);
            
            if (requestToken == null)
            {
                requestToken = this.CreateTestRequestToken(TokenConstants.TestRequestToken, Guid.NewGuid().ToString("N"));

                Realm targetRealm = requestToken.Realm;
                requestToken.DateAuthorized = DateTime.UtcNow;
                requestToken.UserName = UserConstants.TestUserName;
                requestToken.UserId = UserConstants.TestUserId;
                requestToken.VerifierCode = TokenConstants.TestVerifierCode;

                using (this.UnitOfWork.BeginTransaction())
                {
                    requestToken = this.RepositoryManager.RequestTokenRepository.Save(requestToken);
                    this.UnitOfWork.EndTransaction(true);
                }

                requestToken = this.RepositoryManager.RequestTokenRepository.Save(requestToken);
            }

            if (requestToken != null)
            {
                AccessToken accessToken = this.RepositoryManager.RequestTokenRepository.GetAccessToken(TokenConstants.TestAccessToken);

                if (accessToken == null)
                {
                    AccessToken newAccessToken = new AccessToken
                    {
                        ConsumerKey = requestToken.ConsumerKey,
                        DateGranted = DateTime.UtcNow,
                        ExpirationDate = DateTime.UtcNow.AddDays(20),
                        Realm = requestToken.Realm,
                        Token = TokenConstants.TestAccessToken,
                        Secret = TokenConstants.TestAccessTokenSecret,
                        UserName = requestToken.UserName,
                        UserId = requestToken.UserId
                    };

                    requestToken.AccessToken = newAccessToken;
                    requestToken.State = TokenState.AccessGranted;

                    using (this.UnitOfWork.BeginTransaction())
                    {
                        requestToken = this.RepositoryManager.RequestTokenRepository.Save(requestToken);
                        this.UnitOfWork.EndTransaction(true);
                    }
                }
            }

            AccessToken foundItem = this.RepositoryManager.RequestTokenRepository.GetAccessTokenByRequestToken(TokenConstants.TestRequestToken, TokenConstants.TestVerifierCode);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Token == TokenConstants.TestAccessToken);
        }

        [Test]
        public void RequestTokenRepositoryTestGetAccessToken()
        {
            AccessToken foundItem = this.RepositoryManager.RequestTokenRepository.GetAccessToken(TokenConstants.TestAccessToken);

            if (foundItem == null)
            {
                RequestToken requestToken = this.RepositoryManager.RequestTokenRepository.GetByToken(TokenConstants.TestRequestToken);

                if (requestToken == null)
                {
                    requestToken = this.CreateTestRequestToken(TokenConstants.TestRequestToken, Guid.NewGuid().ToString("N"));

                    Realm targetRealm = requestToken.Realm;
                    requestToken.DateAuthorized = DateTime.UtcNow;
                    requestToken.UserName = UserConstants.TestUserName;
                    requestToken.UserId = UserConstants.TestUserId;
                    requestToken.VerifierCode = TokenConstants.TestVerifierCode;

                    using (this.UnitOfWork.BeginTransaction())
                    {
                        requestToken = this.RepositoryManager.RequestTokenRepository.Save(requestToken);
                        this.UnitOfWork.EndTransaction(true);
                    }

                    requestToken = this.RepositoryManager.RequestTokenRepository.Save(requestToken);
                }

                if (requestToken != null)
                {
                    AccessToken newAccessToken = new AccessToken
                    {
                        ConsumerKey = requestToken.ConsumerKey,
                        DateGranted = DateTime.UtcNow,
                        ExpirationDate = DateTime.UtcNow.AddDays(20),
                        Realm = requestToken.Realm,
                        Token = TokenConstants.TestAccessToken,
                        Secret = TokenConstants.TestAccessTokenSecret,
                        UserName = requestToken.UserName,
                        UserId = requestToken.UserId
                    };

                    requestToken.AccessToken = newAccessToken;
                    requestToken.State = TokenState.AccessGranted;

                    using (this.UnitOfWork.BeginTransaction())
                    {
                        requestToken = this.RepositoryManager.RequestTokenRepository.Save(requestToken);
                        this.UnitOfWork.EndTransaction(true);
                    }
                }

                foundItem = this.RepositoryManager.RequestTokenRepository.GetAccessToken(TokenConstants.TestAccessToken);
            }

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Token == TokenConstants.TestAccessToken);
        }

        [Test]
        public void RequestTokenRepositoryTestGetByUserId()
        {
            IList<RequestToken> foundItems = this.RepositoryManager.RequestTokenRepository.GetByUserId(UserConstants.TestUserId, DateTime.UtcNow.AddYears(-1), DateTime.UtcNow);

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
        }

        [Test]
        public void RequestTokenRepositoryTestGetByUserName()
        {
            IList<RequestToken> foundItems = this.RepositoryManager.RequestTokenRepository.GetByUserName(UserConstants.TestUserName, DateTime.UtcNow.AddYears(-1), DateTime.UtcNow);

            Assert.IsNotNull(foundItems);
            Assert.IsTrue(foundItems.Count > 0);
        }
    }
}
