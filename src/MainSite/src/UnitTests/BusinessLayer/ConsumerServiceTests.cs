using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using DD = DevDefined.OAuth.Framework;
using AlwaysMoveForward.OAuth.Client.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.UnitTests.Constants;

namespace AlwaysMoveForward.OAuth.UnitTests.BusinessLayer
{
    /// <summary>
    /// A test class for the Consumer service
    /// </summary>
    [TestFixture]
    public class ConsumerServiceTests : UnitTestBase
    {
        /// <summary>
        /// Create a Dev Defined OAuth context
        /// </summary>
        /// <returns>A dev defined oauth context</returns>
        private DD.IOAuthContext CreateOAuthContext()
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();

            DD.IOAuthContext retVal = new DD.OAuthContext();
            retVal.ConsumerKey = keyConfiguration.ConsumerKey;

            return retVal;
        }

        /// <summary>
        /// Create a new Consumer
        /// </summary>
        [Test]
        public void ConsumerServiceTestCreate()
        {
            Consumer newConsumer = this.ServiceManager.ConsumerService.Create(ConsumerConstants.TestName, ConsumerConstants.TestEmail);

            Assert.IsNotNull(newConsumer);
            Assert.IsTrue(newConsumer.Name == ConsumerConstants.TestName);
            Assert.IsTrue(newConsumer.ContactEmail == ConsumerConstants.TestEmail);
        }

        [Test]
        public void ConsumerServiceTestGetConsumer()
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();

            Consumer foundItem = this.ServiceManager.ConsumerService.GetConsumer(keyConfiguration.ConsumerKey);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.ConsumerKey == keyConfiguration.ConsumerKey);
        }

        /// <summary>
        /// Find a consumer by their key and get its secret
        /// </summary>
        [Test]
        public void ConsumerServiceTestGetConsumerSecret()
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();

            String foundItem = this.ServiceManager.ConsumerService.GetConsumerSecret(this.CreateOAuthContext());

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem == keyConfiguration.ConsumerSecret);
        }

        /// <summary>
        /// Test to determine if a consumer with the specified consumer key is found
        /// </summary>
        [Test]
        public void ConsumerServiceTestIsConsumer()
        {
            Assert.IsTrue(this.ServiceManager.ConsumerService.IsConsumer(this.CreateOAuthContext()));
        }

        /// <summary>
        /// Test if a nonce is unique and save it if it is
        /// </summary>
        [Test]
        public void ConsumerServiceTestRecordNonceAndCheckIsUniquePass()
        {
            Assert.IsTrue(this.ServiceManager.ConsumerService.RecordNonceAndCheckIsUnique(new Mock.MockDevDefinedConsumer(), Constants.ConsumerConstants.TestNotFoundNonce));
        }

        /// <summary>
        /// Test if a nonce is not unique and fail if it is
        /// </summary>
        [Test]
        public void ConsumerServiceTestRecordNonceAndCheckIsUniqueFail()
        {
            Assert.IsFalse(this.ServiceManager.ConsumerService.RecordNonceAndCheckIsUnique(new Mock.MockDevDefinedConsumer(), Guid.NewGuid().ToString()));
        }

        [Test]
        public void ConsumerServiceTestSetConsumerSecret()
        {
            bool exceptionThrown = false;

            try
            {
                this.ServiceManager.ConsumerService.SetConsumerSecret(new Mock.MockDevDefinedConsumer(), ConsumerConstants.TestUpdatedSecret);
            }
            catch (Exception e)
            {
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }

        [Test]
        public void TestGetConsumerByRequestToken()
        {
            Consumer testItem = this.ServiceManager.ConsumerService.GetByRequestToken(TokenConstants.TestRequestToken);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.ConsumerKey == ConsumerConstants.TestConsumerKey);
        }
    }
}
