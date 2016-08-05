using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.Contracts;

namespace TestSite.Models
{
    public class RequestTokenModel : IOAuthToken
    {
        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }

        public string Token { get; set; }

        public string Secret { get; set; }

        public string SessionHandle { get; set;}

        public string Realm { get; set; }

        public EndpointModel EndpointModel { get; set; }
    }
}