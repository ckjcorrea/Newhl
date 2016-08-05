using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Web.Mvc;

namespace AlwaysMoveForward.OAuth.Web.Code
{
    /// <summary>
    /// Simplifies the return of an object from a web method as xml
    /// </summary>
    public class XmlResult : ActionResult
    {
        /// <summary>
        /// The object we want to serialize and return
        /// </summary>
        private object objectToSerialize;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlResult"/> class.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize to XML.</param>
        public XmlResult(object objectToSerialize)
        {
            this.objectToSerialize = objectToSerialize;
        }

        /// <summary>
        /// Gets the object to be serialized to XML.
        /// </summary>
        public object ObjectToSerialize
        {
            get { return this.objectToSerialize; }
        }

        /// <summary>
        /// Serialises the object that was passed into the constructor to XML and writes the corresponding XML to the result stream.
        /// </summary>
        /// <param name="context">The controller context for the current request.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (this.ObjectToSerialize != null)
            {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.Output.Write(AlwaysMoveForward.Common.Utilities.SerializationUtilities.SerializeObjectToXmlString(this.ObjectToSerialize));
            }
        }
    }
}