using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.OAuth.Client.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.UnitTests.Constants;

namespace AlwaysMoveForward.OAuth.DevDefined.UnitTests.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class ConsumerRepositoryTests : RepositoryTestBase
    {
        private Consumer CreateTestConsumer()
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();

            Consumer retVal = new Consumer();
            retVal.AutoGrant = true;
            retVal.ConsumerKey = keyConfiguration.ConsumerKey;
            retVal.ConsumerSecret = keyConfiguration.ConsumerSecret;
            retVal.ContactEmail = ConsumerConstants.TestEmail;
            retVal.Name = ConsumerConstants.TestName;
            retVal.PublicKey = string.Empty;

            return retVal;
        }
        [Test]
        public void ConsumerRepositoryTestsGetAll()
        {
            IList<Consumer> foundItems = this.RepositoryManager.ConsumerRepository.GetAll();

            Assert.IsNotNull(foundItems);
        }

        [Test]
        public void ConsumerRepositoryTestsGetByEmail()
        {
            IList<Consumer> foundItems = this.RepositoryManager.ConsumerRepository.GetByContactEmail(ConsumerConstants.TestEmail);

           if (foundItems == null || foundItems.Count == 0)
           {
               this.RepositoryManager.ConsumerRepository.Save(this.CreateTestConsumer());
           }

           foundItems = this.RepositoryManager.ConsumerRepository.GetByContactEmail(ConsumerConstants.TestEmail);
           Assert.IsNotNull(foundItems);
           Assert.IsTrue(foundItems.Count > 0);
           Assert.IsTrue(foundItems[0].ContactEmail  == ConsumerConstants.TestEmail);
        }

        [Test]
        public void ConsumerRepositoryTestsGetByConsumer()
        {
            OAuthKeyConfiguration keyConfiguration = OAuthKeyConfiguration.GetInstance();
            Consumer foundItem = this.RepositoryManager.ConsumerRepository.GetByConsumerKey(keyConfiguration.ConsumerKey);

            if (foundItem == null)
            {
                this.RepositoryManager.ConsumerRepository.Save(this.CreateTestConsumer());
            }

            foundItem = this.RepositoryManager.ConsumerRepository.GetByConsumerKey(keyConfiguration.ConsumerKey);
            Assert.IsNotNull(foundItem);
        }

        [Test]
        public void ConsumerRepositoryTestsSave()
        {
            Consumer testConsumer = this.CreateTestConsumer();
            testConsumer.ContactEmail = "artie2" + Guid.NewGuid().ToString("N") + "@test.com";
            testConsumer.ConsumerKey = Guid.NewGuid().ToString();
            testConsumer.ConsumerSecret = Guid.NewGuid().ToString();
            testConsumer.AutoGrant = false;
       
            Consumer foundItem = this.RepositoryManager.ConsumerRepository.Save(testConsumer);
            foundItem = this.RepositoryManager.ConsumerRepository.GetByConsumerKey(testConsumer.ConsumerKey);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.ConsumerKey == testConsumer.ConsumerKey);
            Assert.IsTrue(foundItem.ConsumerSecret == testConsumer.ConsumerSecret);
        }

        [Test]
        [Ignore]
        public void ConsumerRepositoryTestsGetByRequestToken()
        {
            Consumer foundItem = this.RepositoryManager.ConsumerRepository.GetByRequestToken(TokenConstants.TestRequestToken);

            Assert.IsNotNull(foundItem);
        }
    }
}
