using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Newhl.Common.DataLayer;
using Newhl.MainSite.BusinessLayer.Services;
using Newhl.MainSite.DataLayer.Repositories;
using Newhl.MainSite.UnitTests.Mock.Repositories;

namespace Newhl.MainSite.UnitTests.Mock
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
