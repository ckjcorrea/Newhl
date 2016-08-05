/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Newhl.Common.Utilities
{
    public class SerializationUtilities
    {
        /// <summary>
        /// Serialize an object to an xml string
        /// </summary>
        /// <param name="sourceData">The object to serialize</param>
        /// <returns>A string of xml</returns>
        public static string SerializeObjectToXmlString(object sourceData)
        {
            XmlElement xmlData = SerializationUtilities.SerializeObjectToXml(sourceData);
            return xmlData.OuterXml;
        }

        public static XmlElement SerializeObjectToXml(object sourceData)
        {
            XmlElement retVal = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                XmlSerializer serializer = new XmlSerializer(sourceData.GetType());
                serializer.Serialize(sw, sourceData);

                XmlDocument tempDoc = new XmlDocument();
                tempDoc.LoadXml(sw.ToString());

                retVal = tempDoc.DocumentElement;
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }

            return retVal;
        }

        public static object DeserializeXmlToObject(XmlElement sourceData, Type targetType)
        {
            object retVal = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(targetType);
                StringReader reader = new StringReader(sourceData.OuterXml);
                retVal = serializer.Deserialize(reader);
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }

            return retVal;
        }

        public static object DeserializeXmlToObject(XmlElement sourceData, Type targetType, string defaultNamespace)
        {
            object retVal = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(targetType, defaultNamespace);
                StringReader reader = new StringReader(sourceData.OuterXml);
                retVal = serializer.Deserialize(reader);
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }

            return retVal;
        }

        /// <summary>
        /// Take xml and deserialize it into a specific object instance
        /// </summary>
        /// <typeparam name="TTargetType">The type to return</typeparam>
        /// <param name="sourceData">The xml to serialize</param>
        /// <returns>An instance of TTargetType</returns>
        public static TTargetType DeserializeXmlToObject<TTargetType>(string sourceData) where TTargetType : class
        {
            TTargetType retVal = null;

            if (!string.IsNullOrEmpty(sourceData))
            {
                XmlDocument xmlData = new XmlDocument();
                xmlData.LoadXml(sourceData);

                retVal = SerializationUtilities.DeserializeXmlToObject<TTargetType>(xmlData.DocumentElement);
            }

            return retVal;
        }

        /// <summary>
        /// Take xml and deserialize it into a specific object instance
        /// </summary>
        /// <typeparam name="TTargetType">The type to return</typeparam>
        /// <param name="sourceData">The xml to serialize</param>
        /// <returns>An instance of TTargetType</returns>
        public static TTargetType DeserializeXmlToObject<TTargetType>(XmlNode sourceData) where TTargetType : class
        {
            TTargetType retVal = null;

            if (sourceData != null)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TTargetType));

                    using (StringReader reader = new StringReader(sourceData.OuterXml))
                    {
                        retVal = serializer.Deserialize(reader) as TTargetType;
                    }
                }
                catch (Exception e)
                {
                    LogManager.GetLogger().Error(e);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Take xml and deserialize it into a specific object instance
        /// </summary>
        /// <typeparam name="TTargetType">The type to return</typeparam>
        /// <param name="sourceData">The xml to serialize</param>
        /// <param name="defaultNamespace">The namespace to find the object xml in</param>
        /// <returns>An instance of TTargetType</returns>
        public static TTargetType DeserializeXmlToObject<TTargetType>(XmlElement sourceData, string defaultNamespace) where TTargetType : class
        {
            TTargetType retVal = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TTargetType), defaultNamespace);

                using (StringReader reader = new StringReader(sourceData.OuterXml))
                {
                    retVal = serializer.Deserialize(reader) as TTargetType;
                }
            }
            catch (Exception e)
            {
                LogManager.GetLogger().Error(e);
            }

            return retVal;
        }
    }
}
