using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;

namespace AlwaysMoveForward.OAuth.UnitTests.Mock.Repositories
{
    public class MockConsumerNonceRepositoryHelper
    {
        public static void ConfigureAllMethods(Mock<IConsumerNonceRepository> repositoryObject)
        {
            ConfigureGetByNonce(repositoryObject);
            ConfigureGetById(repositoryObject);
            ConfigureSave(repositoryObject);
        }

        public static ConsumerNonce GenerateMockConsumerNonce(string nonce)
        {
            ConsumerNonce retVal = new ConsumerNonce();
            retVal.ConsumerKey = Constants.ConsumerConstants.TestConsumerKey;
            retVal.Nonce = nonce;
            retVal.Timestamp = DateTime.Now;
            return retVal;
        }

        public static void ConfigureGetById(Mock<IConsumerNonceRepository> moqObject)
        {
            moqObject.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((string nonce) => GenerateMockConsumerNonce(nonce));
        }

        public static void ConfigureGetByNonce(Mock<IConsumerNonceRepository> moqObject)
        {
            moqObject.Setup(x => x.GetByNonce(It.IsAny<string>()))
                .Returns((string nonce) => GenerateMockConsumerNonce(nonce));

            moqObject.Setup(x => x.GetByNonce(Constants.ConsumerConstants.TestNotFoundNonce))
                .Returns((string searchNonce) => null);
        }

        public static void ConfigureSave(Mock<IConsumerNonceRepository> moqObject)
        {
            moqObject.Setup(x => x.Save(It.IsAny<ConsumerNonce>()))
                .Returns((ConsumerNonce newItem) => newItem);
        }
    }
}
