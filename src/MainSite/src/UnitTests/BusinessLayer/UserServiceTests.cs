using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using DD = DevDefined.OAuth.Framework;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Client.Configuration;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.UnitTests.Constants;

namespace AlwaysMoveForward.OAuth.UnitTests.BusinessLayer
{
    /// <summary>
    /// Test class for User Service
    /// </summary>
    [TestFixture]
    public class UserServiceTests : UnitTestBase
    {
        /// <summary>
        /// Tests getting the user by id
        /// </summary>
        [Test]
        [Ignore]
        public void UserServiceTestGetUserById()
        {
            User foundItem = this.ServiceManager.UserService.GetUserById(UserConstants.TestUserId);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Id == UserConstants.TestUserId);
        }

        /// <summary>
        /// Tests logging in a user with a given user name password
        /// </summary>
        [Test]
        public void UserServiceTestLogonUser()
        {
            User foundItem = this.ServiceManager.UserService.LogonUser(UserConstants.TestUserName, UserConstants.TestPassword, string.Empty);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Email == UserConstants.TestUserName);
        }

        /// <summary>
        /// tests registering a new user
        /// </summary>
        [Test]
        public void UserServiceTestRegisterUser()
        {
            User foundItem = this.ServiceManager.UserService.Register(UserConstants.TestUserName, UserConstants.TestPassword, UserConstants.TestPasswordHint, string.Empty, string.Empty);

            Assert.IsNotNull(foundItem);
            Assert.IsTrue(foundItem.Email == UserConstants.TestUserName);
        }

        /// <summary>
        /// Update the login attempt tracking information based upon the last login status
        /// </summary>
        [Test]
        public void UserServiceTestAddLoginAttempt()
        {           
            UserStatus testItem = this.ServiceManager.UserService.AddLoginAttempt(true, "", UserConstants.TestUserName, null);
            Assert.IsTrue(testItem == UserStatus.Active);
        }

        /// <summary>
        /// Update the login attempt tracking information based upon the last login status
        /// </summary>
        [Test]
        public void UserServiceTestAddLoginAttemptToLockAccount()
        {
            string testUserName = Guid.NewGuid().ToString("N");
            UserStatus testItem = this.ServiceManager.UserService.AddLoginAttempt(false, "", testUserName, null);
            Assert.IsTrue(testItem == UserStatus.Locked);
        }

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        [Test]
        public void UserServiceTestGetLoginFailureCountNoFailures()
        {
            string testUserName = UserConstants.TestUserName;
            int testItem = this.ServiceManager.UserService.GetLoginFailureCount(testUserName);
            Assert.IsTrue(testItem == 0);
        }

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        [Test]
        public void UserServiceTestGetLoginFailureCountFailures()
        {
            string testUserName = Guid.NewGuid().ToString("N");
            int testItem = this.ServiceManager.UserService.GetLoginFailureCount(testUserName);
            Assert.IsTrue(testItem > 0);
        }       

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        [Test]
        public void UserServiceTestGetLoginFailureCountMaxItemsNoFailures()
        {
            string testUserName = UserConstants.TestUserName;
            int testItem = this.ServiceManager.UserService.GetLoginFailureCount(testUserName, 1);
            Assert.IsTrue(testItem == 0);
        }

        /// <summary>
        /// Find out how many times the user has failed to login in the past N tries (default to MaxAllowedLoginFailures)
        /// </summary>
        [Test]
        public void UserServiceTestGetLoginFailureCountMaxItemsFailures()
        {
            string testUserName = Guid.NewGuid().ToString("N");
            int testItem = this.ServiceManager.UserService.GetLoginFailureCount(testUserName, 1);
            Assert.IsTrue(testItem > 0);
        }
    }
}
