using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHC = NHibernate.Cfg;
using Newhl.Common.Utilities;
using NHM = NHibernate.Mapping.Attributes;

namespace Newhl.Common.DataLayer.NHibernate
{
    public class NHibernateSessionFactory
    {
        /// <summary>
        /// A static object for locking while initializing configuration
        /// </summary>
        private static System.Object configurationLock = new System.Object();

        /// <summary>
        /// A static object for locking while initializing the session factory
        /// </summary>
        private static System.Object sessionFactoryLock = new System.Object();

        /// <summary>
        /// A reference to the nhibernate configuration
        /// </summary>
        private static NHC.Configuration nhibernateConfig;

        /// <summary>
        /// A single instance of a session factory for whatever is running this.
        /// </summary>
        private static ISessionFactory sessionFactory;

        /// <summary>
        /// Return the NHibernate configuration if allocated, allocate it if not yet allocated
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <returns>The NHibernate configuration</returns>
        public static NHC.Configuration GetConfiguration(string connectionString)
        {
            // Only try to allocate if the config is null
            if (NHibernateSessionFactory.nhibernateConfig == null)
            {
                IDictionary<string, string> properties = new Dictionary<string, string>();

                if (!string.IsNullOrEmpty(connectionString))
                {
                    properties.Add(ConfigurationProperties.ConnectionString, connectionString);
                }
                else
                {
                    LogManager.GetLogger().Error("Missing connection string");
                }

                // Lock so more than 1 thread can't do the configuration
                lock (configurationLock)
                {
                    // Since threads held up by the lock will go to this next line, make one more check to see if it isn't null
                    // if it is, then this is that same original thread that grabbed the lock, go ahead and allocate
                    // If not then another thread already allocated an instance to just bow out.
                    if (NHibernateSessionFactory.nhibernateConfig == null)
                    {
                        NHibernateSessionFactory.nhibernateConfig = new NHC.Configuration();
                        NHibernateSessionFactory.nhibernateConfig.SetProperties(properties);
                    }

                    NHibernateSessionFactory.nhibernateConfig.Configure();
                }
            }

            return NHibernateSessionFactory.nhibernateConfig;
        }

        /// <summary>
        /// Instatiates or returns a static instance of the NHibernate Session Factory
        /// </summary>
        /// <param name="connectionString">The database connection string in case the NHibernate COnfiguration has not yet been allocated</param>
        /// <returns>The static instance of the session factory</returns>
        public static ISessionFactory BuildSessionFactory(NHC.Configuration nhibernateConfiguration,  System.Reflection.Assembly mappingAssembly)
        {
            // Only try to allocate if the sessionFactory is null
            if (NHibernateSessionFactory.sessionFactory == null)
            {
                // Lock so more than 1 thread can't do the configuration
                lock (sessionFactoryLock)
                {
                    try
                    {
                        // Since threads held up by the lock will go to this next line, make one more check to see if it isn't null
                        // if it is, then this is that same original thread that grabbed the lock, go ahead and allocate
                        // If not then another thread already allocated an instance to just bow out.
                        if (NHibernateSessionFactory.sessionFactory == null)
                        {
                            if (nhibernateConfiguration != null)
                            {
                                // Enable validation (optional)
                                // Here, we serialize all decorated classes (but you can also do it class by class)
                                NHM.HbmSerializer.Default.Validate = true;
                                nhibernateConfiguration.AddInputStream(NHM.HbmSerializer.Default.Serialize(mappingAssembly));
                                NHibernateSessionFactory.sessionFactory = nhibernateConfiguration.BuildSessionFactory();
                            }
                            else
                            {
                                LogManager.GetLogger().Error("Missing Nhibernate Configuration");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                    }
                }
            }

            return NHibernateSessionFactory.sessionFactory;
        }
    }
}
