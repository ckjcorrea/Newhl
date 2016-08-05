using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.OAuth.Client.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;

namespace AlwaysMoveForward.OAuth.DevDefined.UnitTests.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class ConsumerNonceRepositoryTests : RepositoryTestBase
    {
        [Test]
        public void ConsumerNonceRepositoryTestGetAll()
        {
            IList<ConsumerNonce> foundItems = this.RepositoryManager.ConsumerNonceRepository.GetAll();

            Assert.IsNotNull(foundItems);
        }

        [Test]
        public void ConsumerNonceRepositoryTestGetByConsumerKeyAndNonce()
        {
            OAuthKeyConfiguration testNonceValues = OAuthKeyConfiguration.GetInstance();

            ConsumerNonce foundItem = this.RepositoryManager.ConsumerNonceRepository.GetByNonce(testNonceValues.ConsumerSecret);

            if (foundItem == null)
            {
                foundItem = new ConsumerNonce();
                foundItem.ConsumerKey = testNonceValues.ConsumerKey;
                foundItem.Nonce = testNonceValues.ConsumerSecret;
                foundItem.Timestamp = DateTime.UtcNow;
                this.RepositoryManager.ConsumerNonceRepository.Save(foundItem);
            }

            foundItem = this.RepositoryManager.ConsumerNonceRepository.GetByNonce(testNonceValues.ConsumerSecret);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.ConsumerKey == testNonceValues.ConsumerKey);
            Assert.IsTrue(foundItem.Nonce == testNonceValues.ConsumerSecret);
        }
    }
}
