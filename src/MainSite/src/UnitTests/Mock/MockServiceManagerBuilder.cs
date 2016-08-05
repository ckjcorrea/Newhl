using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.DataLayer.Repositories;
using AlwaysMoveForward.OAuth.UnitTests.Mock.Repositories;

namespace AlwaysMoveForward.OAuth.UnitTests.Mock
{
    public class MockServiceManagerBuilder : ServiceManagerBuilder
    {
        public static IServiceManager GetServiceManager()
        {
            var mockServiceManagerBuilder = new MockServiceManagerBuilder();
            return mockServiceManagerBuilder.Create(string.Empty);
        }

        public override IRepositoryManager CreateRepositoryManager(IUnitOfWork unitOfWork)
        {
            return new MockRepositoryManager();
        }
    }
}
