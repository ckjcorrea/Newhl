using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlwaysMoveForward.OAuth.Contracts;
using AlwaysMoveForward.OAuth.Contracts.Configuration;
using TestSite.Models;

namespace TestSite.Controllers
{
    public class ControllerBase : Controller
    {
        protected const int TestUserId = 15223034;
        //
        // GET: /Home/

        protected Realm GenerateRealm(int userId, string userName)
        {
            Realm retVal = new Realm();
            retVal.Area = "Digital";
            retVal.Service = "Social";
            retVal.DataId = userId.ToString();
            retVal.DataName = userName;
            return retVal;
        }

        protected EndpointModel GenerateEndPointModel()
        {
            EndpointConfiguration endpointConfiguration = EndpointConfiguration.GetInstance();
            EndpointModel endpointModel = new EndpointModel();
            endpointModel.ServiceUri = endpointConfiguration.ServiceUri;
            endpointModel.RequestTokenUri = endpointConfiguration.RequestTokenUri;
            endpointModel.AuthorizationUri = endpointConfiguration.AuthorizationUri;
            endpointModel.AccessTokenUri = endpointConfiguration.AccessTokenUri;
            return endpointModel;
        }
    }
}