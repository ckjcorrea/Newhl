using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newhl.MainSite.BusinessLayer.Services;
using Newhl.MainSite.UnitTests.Mock;

namespace Newhl.MainSite.UnitTests
{
    public class UnitTestBase
    {
        public IServiceManager ServiceManager
        {
            get { return MockServiceManagerBuilder.GetServiceManager(); }
        }
    }
}
