using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.Encryption;
using AlwaysMoveForward.OAuth.Client;

namespace AlwaysMoveForward.OAuth.UnitTests.CommonTests
{
    [TestFixture]
    public class UserTransferManagerTests
    {
        private const string testEmail = "test@test.com";
        private const string encryptedTestString = "jAs6UGcdZy+U4K+O6sOdKpFaZNi4AK5+RJ7u8y4PeyHU4l/z0/cES48sGFog2Vyw/lTSreUxZq14gWFWnWlorrAubz9MRasf2vpc3DlhEQ2Y98z/8dk0UqOptsYgwvMFrYvERLkahxwuhxU6GqZLCMfAu+SR8O84/LrlmV2pnTaoJh9A9hL02uf8j7+YiGs9hn5cjqerpQH8jqfScSKfhTknfOGMHzsuWosyuQUmJzZxckoBh3oTMijvk5qRdXiB+j5uLeJZN9T/+fI68stPyg==";

        private User GenerateUser()
        {
            User retVal = new User();
            retVal.Email = testEmail;
            retVal.FirstName = "Unit";
            retVal.LastName = "Test";
            retVal.Id = 1;

            return retVal;
        }

        [Test]
        public void UserTransferManagerTestEncryptionDefaultConstructor()
        {
            UserTransferManager userTransferManager = new UserTransferManager();
            string testItem = userTransferManager.Encrypt(this.GenerateUser());

            Assert.IsTrue(testItem == UserTransferManagerTests.encryptedTestString);
        }

        [Test]
        public void UserTransferManagerTestEncryptionConfigurationConstructor()
        {
            AESConfiguration aesConfiguration = AESConfiguration.GetInstance();
            UserTransferManager userTransferManager = new UserTransferManager(aesConfiguration);
            string testItem = userTransferManager.Encrypt(this.GenerateUser());

            Assert.IsTrue(testItem == UserTransferManagerTests.encryptedTestString);
        }

        [Test]
        public void UserTransferManagerTestEncryptionKeySaltConstructor()
        {
            AESConfiguration aesConfiguration = AESConfiguration.GetInstance();
            UserTransferManager userTransferManager = new UserTransferManager(aesConfiguration.EncryptionKey, aesConfiguration.Salt);
            string testItem = userTransferManager.Encrypt(this.GenerateUser());

            Assert.IsTrue(testItem == UserTransferManagerTests.encryptedTestString);
        }

        [Test]
        public void UserTransferManagerTestDecryptionDefaultConstructor()
        {
            UserTransferManager userTransferManager = new UserTransferManager();
            AlwaysMoveForward.Common.DomainModel.User testItem = userTransferManager.Decrypt(UserTransferManagerTests.encryptedTestString);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Email == UserTransferManagerTests.testEmail);
        }

        [Test]
        public void UserTransferManagerTestDecryptionConfigurationConstructor()
        {
            AESConfiguration aesConfiguration = AESConfiguration.GetInstance();
            UserTransferManager userTransferManager = new UserTransferManager(aesConfiguration);
            AlwaysMoveForward.Common.DomainModel.User testItem = userTransferManager.Decrypt(UserTransferManagerTests.encryptedTestString);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Email == UserTransferManagerTests.testEmail);
        }

        [Test]
        public void UserTransferManagerTestDecryptionKeySaltConstructor()
        {
            AESConfiguration aesConfiguration = AESConfiguration.GetInstance();
            UserTransferManager userTransferManager = new UserTransferManager(aesConfiguration.EncryptionKey, aesConfiguration.Salt);
            AlwaysMoveForward.Common.DomainModel.User testItem = userTransferManager.Decrypt(UserTransferManagerTests.encryptedTestString);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Email == UserTransferManagerTests.testEmail);
        }
    }
}
