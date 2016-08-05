using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.Common.DataLayer.ActiveRecord
{
    /// <summary>
    /// These values allow us to set NHibernate configuratin properties dynamically rather than only on the .config file
    /// </summary>
    public class ConfigurationProperties
    {
        /// <summary>
        /// The configuration setting for the connection string
        /// </summary>
        public const string ConnectionString = "connection.connection_string";

        /// <summary>
        /// The configuration setting for the connection provider
        /// </summary>
        public const string ConnectionProvider = "connection.provider";

        /// <summary>
        /// The configuration setting for the dialect 
        /// </summary>
        public const string Dialect = "dialect";

        /// <summary>
        /// The configuraiton setting for the driver type.
        /// </summary>
        public const string Driver = "connection.driver_class";
    }
}

