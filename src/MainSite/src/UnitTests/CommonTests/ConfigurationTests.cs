using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.OAuth.Client.Configuration;

namespace AlwaysMoveForward.OAuth.UnitTests.CommonTests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void EndpointConfigurationTest()
        {
            EndpointConfiguration endpointConfiguration = EndpointConfiguration.GetInstance();

            Assert.IsNotNull(endpointConfiguration);

            Assert.IsTrue(endpointConfiguration.GetFullAccessTokenUri() == "http://localhost:50001/OAuth/ExchangeRequestTokenForAccessToken");
            Assert.IsTrue(endpointConfiguration.GetFullAuthorizationUri() == "http://localhost:50001/OAuth/AuthorizeConsumer");
            Assert.IsTrue(endpointConfiguration.GetFullRequestTokenUri() == "http://localhost:50001/OAuth/GetRequestToken");
        }

        [Test]
        public void OAuthKeyConfigurationTest()
        {
            OAuthKeyConfiguration oauthConfiguration = OAuthKeyConfiguration.GetInstance();

            Assert.IsNotNull(oauthConfiguration);
            Assert.IsTrue(oauthConfiguration.ConsumerKey == "204d869d-5cf1-4601-b21f-e62622d8920a");
            Assert.IsTrue(oauthConfiguration.ConsumerSecret == "057ac52d-9b99-48be-970b-3290dcac47cd");
        }
    }
}
