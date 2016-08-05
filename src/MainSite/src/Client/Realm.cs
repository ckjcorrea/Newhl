using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth.Client
{
    /// <summary>
    /// A class to represent a parsed realm (or realm info to convert to a string)
    /// </summary>
    public class Realm
    {
        /// <summary>
        /// Defines which element in the urn defines the area (AlwaysMoveForward)
        /// </summary>
        private const int AreaElement = 0;

        /// <summary>
        /// Defines which element in the urn defines the target service 
        /// </summary>
        private const int ServiceElement = 1;

        /// <summary>
        /// Defines which element in the urn defines the data boundary identifier
        /// </summary>
        private const int DataIdElement = 2;

        /// <summary>
        /// Defines which element in the urn defines the data boundary name
        /// </summary>
        private const int DataNameElement = 3;

        /// <summary>
        /// A constant string to define the urn prefix.
        /// </summary>
        private const string UrnPrefix = "urn://";

        /// <summary>
        /// A constant string to define the field separator character
        /// </summary>
        private const char ElementSeparator = '/';

        /// <summary>
        /// The default realm string
        /// </summary>
        private const string DefaultRealm = UrnPrefix + "AlwaysMoveForward/Unknown/0/Unknown";

        /// <summary>
        /// Get a default instance of the realm
        /// </summary>
        /// <returns></returns>
        public static Realm GetDefault()
        {
            return Realm.Parse(DefaultRealm);
        }

        /// <summary>
        /// Parse the Realm components out of a string
        /// </summary>
        /// <param name="input">The incoming string</param>
        /// <returns>A Realm instance populated with the string elements</returns>
        public static Realm Parse(string input)
        {
            Realm retVal = new Realm();

            if (!string.IsNullOrEmpty(input))
            {
                string cleanedInput = input;

                int foundPrefix = input.IndexOf(UrnPrefix);

                if (foundPrefix > -1 && (input.Length > foundPrefix + UrnPrefix.Length))
                {
                    cleanedInput = input.Substring(foundPrefix + UrnPrefix.Length);
                }

                string[] realmElements = cleanedInput.Split(ElementSeparator);

                if (realmElements != null)
                {
                    retVal.Area = Realm.ParseElement(realmElements, Realm.AreaElement);
                    retVal.Service = Realm.ParseElement(realmElements, Realm.ServiceElement);
                    retVal.DataId = Realm.ParseElement(realmElements, Realm.DataIdElement);
                    retVal.DataName = Realm.ParseElement(realmElements, Realm.DataNameElement);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Parse out the particular element form the uri segments
        /// </summary>
        /// <param name="realmElements">The uri segments</param>
        /// <param name="elementLocation">The target segment</param>
        /// <returns>The segment text</returns>
        private static string ParseElement(string[] realmElements, int elementLocation)
        {
            string retVal = string.Empty;

            if (realmElements.Length > elementLocation)
            {
                retVal = realmElements[elementLocation];
            }

            return retVal;
        }

        /// <summary>
        /// Gets or sets the business area (such as AlwaysMoveForward)
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the service target (such as Blog)
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets the Data Identifer (such as User Id)
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// Gets or sets teh data name (such as username)
        /// </summary>
        public string DataName { get; set; }

        /// <summary>
        /// Convert the object into a realm string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder realmStringBuilder = new StringBuilder();

            realmStringBuilder.Append(UrnPrefix);

            if (string.IsNullOrEmpty(this.Area))
            {
                realmStringBuilder.Append(string.Empty);
            }
            else
            {
                realmStringBuilder.Append(this.Area);
            }

            realmStringBuilder.Append(ElementSeparator);

            if (string.IsNullOrEmpty(this.Service))
            {
                realmStringBuilder.Append(string.Empty);
            }
            else
            {
                realmStringBuilder.Append(this.Service);
            }

            realmStringBuilder.Append(ElementSeparator);

            if (string.IsNullOrEmpty(this.DataId))
            {
                realmStringBuilder.Append(string.Empty);
            }
            else
            {
                realmStringBuilder.Append(this.DataId);
            }

            realmStringBuilder.Append(ElementSeparator);

            if (string.IsNullOrEmpty(this.DataName))
            {
                realmStringBuilder.Append(string.Empty);
            }
            else
            {
                realmStringBuilder.Append(this.DataName);
            }

            return realmStringBuilder.ToString();
        }
    }
}