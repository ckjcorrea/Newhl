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
    public class LoginAttemptRepositoryTest : RepositoryTestBase
    {
        private LoginAttempt GenerateLoginAttempt(string userName, bool wasSuccessfull)
        {
            LoginAttempt retVal = new LoginAttempt();
            retVal.UserName = userName;
            retVal.Source = string.Empty;
            retVal.WasSuccessfull = wasSuccessfull;
            retVal.AttemptDate = DateTime.UtcNow;
            return retVal;
        }
        
        [Test]
        public void LoginAttemptRepositoryTestSave()
        {
            LoginAttempt testItem = this.GenerateLoginAttempt(AlwaysMoveForward.OAuth.UnitTests.Constants.UserConstants.TestUserName, true);

            testItem = this.RepositoryManager.LoginAttemptRepository.Save(testItem);

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Id > 0);
        }

        [Test]
        public void LoginAttemptRepositoryTestGetById()
        {
            LoginAttempt testItem = this.RepositoryManager.LoginAttemptRepository.GetById(1);

            if(testItem == null)
            {
                testItem = this.GenerateLoginAttempt(AlwaysMoveForward.OAuth.UnitTests.Constants.UserConstants.TestUserName, true);
                testItem = this.RepositoryManager.LoginAttemptRepository.Save(testItem);
                testItem = this.RepositoryManager.LoginAttemptRepository.GetById(testItem.Id);
            }

            Assert.IsNotNull(testItem);
            Assert.IsTrue(testItem.Id > 0);
        }

        [Test]
        public void LoginAttemptRepositoryTestGetByUserName()
        {
            IList<LoginAttempt> testItems = this.RepositoryManager.LoginAttemptRepository.GetByUserName(AlwaysMoveForward.OAuth.UnitTests.Constants.UserConstants.TestUserName);

            if (testItems == null || testItems.Count == 0)
            {
                LoginAttempt testItem = this.GenerateLoginAttempt(AlwaysMoveForward.OAuth.UnitTests.Constants.UserConstants.TestUserName,true);
                testItem = this.RepositoryManager.LoginAttemptRepository.Save(testItem);
                testItems = this.RepositoryManager.LoginAttemptRepository.GetByUserName(AlwaysMoveForward.OAuth.UnitTests.Constants.UserConstants.TestUserName);
            }

            Assert.IsNotNull(testItems);
            Assert.IsTrue(testItems.Count > 0);
        }
    }
}
