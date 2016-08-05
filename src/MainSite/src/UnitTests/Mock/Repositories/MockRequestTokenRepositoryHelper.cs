using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using AlwaysMoveForward.OAuth.UnitTests.Constants;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;

namespace AlwaysMoveForward.OAuth.UnitTests.Mock.Repositories
{
    public class MockRequestTokenRepositoryHelper
    {
        public static void ConfigureAllMethods(Mock<IRequestTokenRepository> repositoryObject)
        {
            MockRequestTokenRepositoryHelper.ConfigureGetByConsumerKey(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureGetByToken(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureGetByTokenAndVerifierCode(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureGetAccessToken(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureGetAccessTokenByRequestToken(repositoryObject);
            MockRequestTokenRepositoryHelper.ConfigureSave(repositoryObject);
        }

        public static IList<RequestToken> GenerateRequestTokens(string consumerKey)
        {
            IList<RequestToken> retVal = new List<RequestToken>();

            retVal.Add(MockRequestTokenRepositoryHelper.GenerateRequestToken(consumerKey, TokenConstants.TestRequestToken, TokenConstants.TestRequestTokenSecret, string.Empty, string.Empty));

            return retVal;
        }

        public static RequestToken AuthorizeRequestToken(RequestToken originalToken, string verifierCode)
        {
            RequestToken retVal = originalToken;
            retVal.DateAuthorized = DateTime.Now;
            retVal.UserId = UserConstants.TestUserId;
            retVal.UserName = UserConstants.TestUserName;
            retVal.VerifierCode = verifierCode;
            return retVal;
        }

        public static AccessToken GenerateAccessToken(string accessToken)
        {
            AccessToken retVal = new AccessToken();
            retVal.ConsumerKey = ConsumerConstants.TestConsumerKey;
            retVal.DateGranted = DateTime.Now;
            retVal.ExpirationDate = DateTime.Now.AddYears(5);
            retVal.Token = accessToken;
            retVal.Secret = TokenConstants.TestAccessTokenSecret;
            retVal.Realm = TokenConstants.TestRealm;
            return retVal;
        }


        public static RequestToken GenerateRequestToken(string consumerKey, string tokenValue, string requestTokenSecret, string verifierCode, string accessToken)
        {
            RequestToken retVal = null;

            if (tokenValue == TokenConstants.TestRequestToken 
                || tokenValue == TokenConstants.TestRequestTokenWithAuthorization
                || tokenValue == TokenConstants.TestRequestTokenWithAccessToken)
            {
                retVal = new RequestToken();
                retVal.ConsumerKey = consumerKey;
                retVal.Realm = TokenConstants.TestRealm;
                retVal.Token = tokenValue;
                retVal.Secret = requestTokenSecret;
                retVal.CallbackUrl = TokenConstants.TestCallbackUrl;
                retVal.ExpirationDate = DateTime.UtcNow.AddDays(1);

                if (tokenValue == TokenConstants.TestRequestTokenWithAuthorization)
                {
                    retVal = MockRequestTokenRepositoryHelper.AuthorizeRequestToken(retVal, verifierCode);
                }

                if (tokenValue == TokenConstants.TestRequestTokenWithAccessToken)
                {
                    retVal = MockRequestTokenRepositoryHelper.AuthorizeRequestToken(retVal, verifierCode);
                    retVal.AccessToken = MockRequestTokenRepositoryHelper.GenerateAccessToken(accessToken);
                }
            }
            else if (tokenValue == TokenConstants.TestAccessToken)
            {
                retVal = new RequestToken();
                retVal.ConsumerKey = consumerKey;
                retVal.Realm = TokenConstants.TestRealm;
                retVal.Token = TokenConstants.TestRequestToken;
                retVal.Secret = requestTokenSecret;
                retVal.CallbackUrl = TokenConstants.TestCallbackUrl;
                retVal.ExpirationDate = DateTime.UtcNow.AddDays(1);
                retVal = MockRequestTokenRepositoryHelper.AuthorizeRequestToken(retVal, verifierCode);
                retVal.AccessToken = MockRequestTokenRepositoryHelper.GenerateAccessToken(tokenValue);
            }

            return retVal;
        }

        public static void ConfigureGetByConsumerKey(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByConsumerKey(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns((string consumerKey) => MockRequestTokenRepositoryHelper.GenerateRequestTokens(consumerKey));
        }

        public static void ConfigureGetByToken(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByToken(It.IsAny<string>()))
             .Returns((string tokenValue) => MockRequestTokenRepositoryHelper.GenerateRequestToken(ConsumerConstants.TestConsumerKey, tokenValue, TokenConstants.TestRequestTokenSecret, TokenConstants.TestVerifierCode, TokenConstants.TestAccessToken));
        }

        public static void ConfigureGetByTokenAndVerifierCode(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByTokenAndVerifierCode(It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string requestToken, string verifierCode) => MockRequestTokenRepositoryHelper.GenerateRequestToken(ConsumerConstants.TestConsumerKey, requestToken, TokenConstants.TestRequestTokenSecretWithAuthorization, verifierCode, TokenConstants.TestAccessToken));
        }

        public static void ConfigureGetAccessToken(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetAccessToken(It.IsAny<string>()))
                .Returns((string accessToken) => MockRequestTokenRepositoryHelper.GenerateAccessToken(accessToken));
        }

        public static void ConfigureGetAccessTokenByRequestToken(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.GetAccessTokenByRequestToken(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string accessToken, string verifierCode) => MockRequestTokenRepositoryHelper.GenerateAccessToken(accessToken));
        }

        public static void ConfigureSave(Mock<IRequestTokenRepository> moqObject)
        {
            moqObject.Setup(x => x.Save(It.IsAny<RequestToken>()))
                .Returns((RequestToken requestToken) => requestToken);
        }
    }
}
