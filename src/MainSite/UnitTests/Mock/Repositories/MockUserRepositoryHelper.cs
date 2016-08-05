﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Newhl.Common.Encryption;
using Newhl.MainSite.Common.DomainModel;
using Newhl.MainSite.DataLayer.Repositories;
using Newhl.MainSite.UnitTests.Constants;


namespace Newhl.MainSite.UnitTests.Mock.Repositories
{
    public class MockUserRepositoryHelper
    {
        public static void ConfigureAllMethods(Mock<IAMFUserRepository> repositoryObject)
        {
            ConfigureSave(repositoryObject);
            ConfigureGetById(repositoryObject);
            ConfigureGetByEmail(repositoryObject);
        }

        public static void ConfigureSave(Mock<IAMFUserRepository> moqObject)
        {
            moqObject.Setup(x => x.Save(It.IsAny<AMFUserLogin>()))
                .Returns((AMFUserLogin newCustomer) => newCustomer);
        }

        public static AMFUserLogin GenerateNewUser(int userId)
        {
            return GenerateNewUser(userId, UserConstants.TestUserName, UserConstants.HashedPasswordWithDefaultSalt, UserConstants.TestSalt, SHA1HashUtility.Pbkdf2Iterations);
        }


        public static AMFUserLogin GenerateNewUser(string userName)
        {
            return GenerateNewUser(UserConstants.TestUserId, userName, UserConstants.HashedPasswordWithDefaultSalt, UserConstants.TestSalt, SHA1HashUtility.Pbkdf2Iterations);
        }

        public static AMFUserLogin GenerateNewUser(int userId, string userName, string hashedPassword, string passwordSalt, int passwordIterations)
        {
            AMFUserLogin retVal = new AMFUserLogin();
            retVal.Id = userId;
            retVal.Email = userName;
            retVal.FirstName = string.Empty;
            retVal.LastName = string.Empty;
            retVal.UpdatePassword(hashedPassword, passwordSalt);

            return retVal;
        }

        public static void ConfigureGetById(Mock<IAMFUserRepository> moqObject)
        {
            moqObject.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int targetId) => GenerateNewUser(targetId));
        }

        public static void ConfigureGetByEmail(Mock<IAMFUserRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns((string targetEmail) => GenerateNewUser(targetEmail));
        }
    }
}
