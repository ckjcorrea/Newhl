using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Client.Configuration;

namespace AlwaysMoveForward.OAuth.Client
{
    public class OAuthRepository : IOAuthRepository
    {
        private const string GetUserInfoAction = "api/Users";
        private const string GetByEmailAction = "api/Users";
        private const string GetUserByIdAction = "api/Users";

        public OAuthRepository(OAuthClientBase oauthClient)
        {
            this.OAuthClient = oauthClient;
        }

        public OAuthClientBase OAuthClient { get; private set; }

        private IList<User> DeserializeUserList(string serializedJSon)
        {
            IList<User> retVal = new List<User>();

            if (!string.IsNullOrEmpty(serializedJSon))
            {
                using (var stringReader = new StringReader(serializedJSon))
                using (var jsonReader = new JsonTextReader(stringReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    retVal = jsonSerializer.Deserialize<IList<User>>(jsonReader);
                }
            }

            return retVal;
        }

        private User DeserializeUser(string serializedJSon)
        {
            User retVal = null;

            if (!string.IsNullOrEmpty(serializedJSon))
            {
                using (var stringReader = new StringReader(serializedJSon))
                using (var jsonReader = new JsonTextReader(stringReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    retVal = jsonSerializer.Deserialize<User>(jsonReader);
                }
            }

            return retVal;
        }

        public User GetEntity(IOAuthToken oauthToken, string targetAction)
        {
            User retVal = null;

            if (this.OAuthClient != null)
            {
                string response = this.OAuthClient.ExecuteAuthorizedRequest(this.OAuthClient.OAuthEndpoints.ServiceUri, targetAction, oauthToken);
                retVal = this.DeserializeUser(response);
            }

            return retVal;            
        }

        public IList<User> GetCollection(IOAuthToken oauthToken, string targetAction)
        {
            IList<User> retVal = null;

            if (this.OAuthClient != null)
            {
                string response = this.OAuthClient.ExecuteAuthorizedRequest(this.OAuthClient.OAuthEndpoints.ServiceUri, targetAction, oauthToken);
                retVal = this.DeserializeUserList(response);
            }

            return retVal;
        }

        public User GetUserInfo(IOAuthToken oauthToken)
        {
            return this.GetEntity(oauthToken, OAuthRepository.GetUserInfoAction);
        }

        public User GetById(IOAuthToken oauthToken, long id)
        {
            return this.GetEntity(oauthToken, OAuthRepository.GetUserByIdAction + "/" + id);
        }

        public IList<User> GetByEmail(IOAuthToken oauthToken, string emailAddress)
        {
            return this.GetCollection(oauthToken, OAuthRepository.GetByEmailAction + "?emailAddress=" + emailAddress);
        }
    }
}

