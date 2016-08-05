﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Transactions;
using NHibernate;
using NHC = NHibernate.Cfg;
using Newhl.Common.DataLayer;

namespace Newhl.MainSite.DataLayer
{
    /// <summary>
    /// A unit of work implementation to co locate the NHibernate configuration with the DTOs
    /// </summary>
    public class UnitOfWork : Newhl.Common.DataLayer.NHibernate.UnitOfWork, IUnitOfWork, IDisposable
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public UnitOfWork()
            : base()
        {

        }

        /// <summary>
        /// A constructor that takes database connection strings to vistaprint.  These need to go away long term.
        /// </summary>
        /// <param name="vistaprintConnectionString">The connection string for the vistaprint database</param>
        public UnitOfWork(string connectionString)
            : base(connectionString)
        {

        }

        protected override void StartSession()
        {
            this.CurrentSession = Newhl.Common.DataLayer.NHibernate.NHibernateSessionFactory.BuildSessionFactory(this.NHibernateConfiguration, System.Reflection.Assembly.GetExecutingAssembly()).OpenSession(); ;
        }
    }
}
