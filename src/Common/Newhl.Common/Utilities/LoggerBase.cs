using System;
using System.Security.Principal;
using System.Runtime.CompilerServices;

namespace Newhl.Common.Utilities
{
    public abstract class LoggerBase
    {
        private const string messageDelimiter = ":";

        protected abstract void LogDebug(string message);
        protected abstract void LogError(string message);
        protected abstract void LogInfo(string message);
        protected abstract void LogWarn(string message);

        public IPrincipal CurrentPrincial
        {
            get
            {
                IPrincipal retVal = null;

                if (System.Threading.Thread.CurrentPrincipal != null)
                {
                    retVal = System.Threading.Thread.CurrentPrincipal;
                }

                return retVal;
            }
        }

        private string GenerateBaseMessage(string className, string methodName)
        {
            string retVal = string.Empty;
            
            if(String.IsNullOrEmpty(className))
            {
                className = string.Empty;
            }

            if(string.IsNullOrEmpty(methodName))
            {
                methodName = string.Empty;
            }

            // Get logged in user name
            if (this.CurrentPrincial != null)
            {
                retVal = this.CurrentPrincial.Identity.Name + LoggerBase.messageDelimiter;
            }

            // Attach class name method name and message
            retVal += "Class Name - " + className + LoggerBase.messageDelimiter + "Method Name - " + methodName;
            
            return retVal;
        }

        public void Error(Exception e, [CallerFilePath] string className = null, [CallerMemberName] string methodName = null)
        {
            // Log Error message
            string fullMessage = this.GenerateBaseMessage(className, methodName);
            
            // Attach class name method name and message
            fullMessage += LoggerBase.messageDelimiter + e.GetType().Name + LoggerBase.messageDelimiter + e.Message;

            if (e.InnerException != null)
            {
                // Attach inner exception if any
                fullMessage += LoggerBase.messageDelimiter + "InnerException:";
                fullMessage += e.InnerException.GetType().Name + LoggerBase.messageDelimiter + e.InnerException.Message;
            }

            this.LogError(fullMessage);
        }

        public void Info(string message, [CallerFilePath] string className = null, [CallerMemberName] string methodName = null)
        {
            // Log Error message
            string fullMessage = this.GenerateBaseMessage(className, methodName);

            // Attach class name method name and message
            fullMessage += LoggerBase.messageDelimiter + message;

            this.LogInfo(fullMessage);
        }

        public void Debug(string message, [CallerFilePath] string className = null, [CallerMemberName] string methodName = null)
        {
            // Log Error message
            string fullMessage = this.GenerateBaseMessage(className, methodName);

            // Attach class name method name and message
            fullMessage += LoggerBase.messageDelimiter + message;

            this.LogDebug(fullMessage);
        }

        public void Error(string message, [CallerFilePath] string className = null, [CallerMemberName] string methodName = null)
        {
            // Log Error message
            string fullMessage = this.GenerateBaseMessage(className, methodName);

            // Attach class name method name and message
            fullMessage += LoggerBase.messageDelimiter + message;

            this.LogError(fullMessage);
        }
    }
}