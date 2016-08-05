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
    public class UserRepositoryTests : RepositoryTestBase
    {
        private const string TestHashedPassword = "lfcTgtQr7OlaNF1YmrazuYTS5fCyASGT";
        private const string TestPasswordSalt = "t8Oipwx4MnV9A+oVlXG1wKXeirWqzuBv";
        private const int TestPasswordIterations = 1000;

        private string GenerateEmail(string uniqueAddIn)
        {
            return string.Format("artie{0}@test.com", uniqueAddIn);
        }

        private AMFUserLogin CreateTestUser(string emailAddress)
        {
            AMFUserLogin retVal = new AMFUserLogin();
            retVal.Email = emailAddress;
            retVal.FirstName = "Artie";
            retVal.LastName = "Test";
            retVal.PasswordHash = TestHashedPassword;
            retVal.PasswordSalt = TestPasswordSalt;

            return retVal;
        }
        [Test]
        public void UserRepositoryTestGetAll()
        {
            IList<AMFUserLogin> foundItems = this.RepositoryManager.UserRepository.GetAll();

            Assert.IsNotNull(foundItems);
        }

        [Test]
        public void UserRepositoryTestGetByEmail()
        {
            string testEmail = this.GenerateEmail(Guid.NewGuid().ToString("N"));

            AMFUserLogin foundItem = this.RepositoryManager.UserRepository.GetByEmail(testEmail);

            if (foundItem == null)
            {
                this.RepositoryManager.UserRepository.Save(this.CreateTestUser(testEmail));
            }

            foundItem = this.RepositoryManager.UserRepository.GetByEmail(testEmail);
            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Email == testEmail);
        }

        [Test]
        public void UserRepositoryTestGetByid()
        {
            long testLoginId = 1;
            AMFUserLogin foundItem = this.RepositoryManager.UserRepository.GetById(1);

            if (foundItem == null)
            {
                string testEmail = this.GenerateEmail(Guid.NewGuid().ToString("N"));
                AMFUserLogin newUser = this.RepositoryManager.UserRepository.Save(this.CreateTestUser(testEmail));

                if (newUser != null)
                {
                    testLoginId = newUser.Id;
                }
            }

            foundItem = this.RepositoryManager.UserRepository.GetById(testLoginId);
            Assert.IsNotNull(foundItem);
        }

        [Test]
        public void UserRepositoryTestSave()
        {
            string testEmail = this.GenerateEmail(Guid.NewGuid().ToString("N"));
            AMFUserLogin testUser = this.RepositoryManager.UserRepository.GetByEmail(testEmail);
            AMFUserLogin foundItem = null;
            string uniqueTest = Guid.NewGuid().ToString("N").Substring(0, 30);

            if (testUser == null)
            {
                testUser = this.CreateTestUser(testEmail);
                testUser.Email = testEmail;
                testUser.LastName = uniqueTest;
                foundItem = this.RepositoryManager.UserRepository.Save(testUser);
            }
            else
            {
                testUser.LastName = uniqueTest;
            }

            foundItem = this.RepositoryManager.UserRepository.GetByEmail(testUser.Email);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Email == testUser.Email);
            Assert.IsTrue(foundItem.LastName == uniqueTest);
        }       
    }
}
