using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DD = DevDefined.OAuth;

namespace AlwaysMoveForward.OAuth.UnitTests.Mock
{
    public class MockDevDefinedConsumer : DD.Framework.IConsumer
    {
        public MockDevDefinedConsumer()
        {
            this.ConsumerKey = Constants.ConsumerConstants.TestConsumerKey;
            this.Realm = string.Empty;
        }

        public string ConsumerKey { get; set; }

        public string Realm { get; set; }
    }
}
