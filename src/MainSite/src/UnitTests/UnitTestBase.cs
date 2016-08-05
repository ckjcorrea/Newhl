using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth.BusinessLayer.Services;
using AlwaysMoveForward.OAuth.UnitTests.Mock;

namespace AlwaysMoveForward.OAuth.UnitTests
{
    public class UnitTestBase
    {
        public IServiceManager ServiceManager
        {
            get { return MockServiceManagerBuilder.GetServiceManager(); }
        }
    }
}
