using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSite.Models
{
    public class ConsumerKeyModel
    {
        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }

        public string CallbackUrl { get; set; }

        public EndpointModel EndpointModel { get; set; }
    }
}