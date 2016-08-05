using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using DevDefined.OAuth.Provider.Inspectors;
using DevDefined.OAuth.Framework.Signing;
using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.Encryption;
using AlwaysMoveForward.OAuth.Client;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.Web.Models;
using AlwaysMoveForward.OAuth.Web.Code;

namespace AlwaysMoveForward.OAuth.Web.Controllers
{
    public class ProxyController : ControllerBase
    {        
        /// <summary>
        /// Pull out the proxy target form the url, it will be the first element in the query path.
        /// </summary>
        /// <param name="path">The query path</param>
        /// <returns>The proxy target host</returns>
        private string ExtractTargetServiceFromPath(string path)
        {
            string retVal = string.Empty;

            string[] splitPath = path.Split('/');

            if (splitPath.Length > 1)
            {
                retVal = splitPath[1];
            }

            return retVal;
        }
       
        /// <summary>
        /// Proxy signed OAuth calls to the desired service.
        /// </summary>
        /// <returns>the output of the proxy call</returns>
        [MVCAuthorization]
        public string RequestProxy()
        {
            string proxyTargetHost = string.Empty;
            string targetService = this.ExtractTargetServiceFromPath(this.Request.Path);

            try
            {
                proxyTargetHost = System.Configuration.ConfigurationManager.AppSettings[targetService];
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }

            string retVal = string.Empty;

            if (!string.IsNullOrEmpty(proxyTargetHost))
            {
                retVal = this.ExecuteProxyRequest(proxyTargetHost, this.Request, this.Response);
            }

            return retVal;
        }

        /// <summary>
        /// Make the call to the proxy target.
        /// </summary>
        /// <param name="proxyTargetHost">The url of the proxy target</param>
        /// <param name="request">The original request</param>
        /// <returns> The content and the content type</returns>
        private string ExecuteProxyRequest(string proxyTargetHost, HttpRequestBase request, HttpResponseBase proxyResponse)
        {
            string retVal = string.Empty;

            string logIdentifier = Guid.NewGuid().ToString("N");

            string targetUrl = proxyTargetHost + request.Path + request.Url.Query;
            Uri targetUri = new Uri(targetUrl);

            LogManager.GetLogger().Debug("ProxyRequest:" + logIdentifier + ":TargetUrl:" + targetUrl);

            // Create a request for the URL. 		
            HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(targetUri);
            newRequest.AllowAutoRedirect = false;
            newRequest.ContentLength = request.ContentLength;

            LogManager.GetLogger().Debug("ProxyRequest:" + logIdentifier + ":ContentLength:" + request.ContentLength);

            newRequest.ContentType = request.ContentType;

            LogManager.GetLogger().Debug("ProxyRequest:" + logIdentifier + ":ContentLength:" + request.ContentType);
            
            newRequest.UseDefaultCredentials = true;
            newRequest.UserAgent = ".NET Web Proxy";
            newRequest.Referer = proxyTargetHost + request.Path;
            newRequest.Method = request.RequestType;

            LogManager.GetLogger().Debug("ProxyRequest:" + logIdentifier + ":RequestType:" + request.RequestType);

            newRequest.Headers.Add("X-Forwarded-For", this.Request.UserHostAddress + "," + this.Request.Url.Host);

            if (this.Request.AcceptTypes != null && this.Request.AcceptTypes.Length > 0)
            {
                newRequest.MediaType = this.Request.AcceptTypes[0];
            }

            UserTransferManager userTransferManager = new UserTransferManager();
            Cookie encryptedCookie = new Cookie(AlwaysMoveForward.OAuth.Client.Constants.ProxyUserCookieName, userTransferManager.Encrypt(this.CurrentPrincipal.User));
            encryptedCookie.Domain = targetUri.Host;

            if (newRequest.CookieContainer == null)
            {
                newRequest.CookieContainer = new CookieContainer();
            }

            newRequest.CookieContainer.Add(encryptedCookie);

            foreach (string str in this.Request.Headers.Keys)
            {
                try { newRequest.Headers.Add(str, this.Request.Headers[str]); }
                catch { }
            }

            // No need to copy input stream for GET (actually it would throw an exception)
            if (this.Request.ContentLength > 0 || Request.Headers.Get("Transfer-Encoding") != null)
            {
                LogManager.GetLogger().Debug("ProxyRequest:" + logIdentifier + ":Copying inputStream");

                Request.InputStream.Position = 0;  //***** THIS IS REALLY IMPORTANT GOTCHA

                var requestStream = HttpContext.Request.InputStream;
                Stream webStream = null;
                try
                {
                    // copy incoming request body to outgoing request
                    if (requestStream != null && requestStream.Length > 0)
                    {
                        newRequest.ContentLength = requestStream.Length;
                        webStream = newRequest.GetRequestStream();
                        requestStream.CopyTo(webStream);
                    }
                }
                finally
                {
                    if (null != webStream)
                    {
                        webStream.Flush();
                        webStream.Close();
                    }
                }
            }

            try
            {
                // No more ProtocolViolationException!
                using (HttpWebResponse response = (HttpWebResponse)newRequest.GetResponse())
                {
                    proxyResponse.ContentType = response.ContentType;

                    // Get the stream containing content returned by the server.
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);

                        // Read the content. 
                        retVal = reader.ReadToEnd();
                    }

                    proxyResponse.StatusCode = (int)response.StatusCode;
                }
            }
            catch (WebException we)
            {
                LogManager.GetLogger().Error(we);
                LogManager.GetLogger().Error("ProxyRequest:" + logIdentifier + "RequestUrl:" + targetUrl);
                retVal = we.Message;

                var response = we.Response as HttpWebResponse;
               
                if (response != null)
                {
                    proxyResponse.StatusCode = (int)response.StatusCode;
                    proxyResponse.ContentType = response.ContentType;

                    try
                    {
                        // Get the stream containing content returned by the server.
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            // Open the stream using a StreamReader for easy access.
                            StreamReader reader = new StreamReader(dataStream);

                            // Read the content. 
                            retVal = reader.ReadToEnd();
                        }
                    }
                    catch(Exception e)
                    {
                        // try to read the body, but be prepared if we can't.
                        LogManager.GetLogger().Error(e);
                    }
                }
                else
                {
                    proxyResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
                       
            return retVal;
        }
    }
}