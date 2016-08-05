using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

using NHibernate;
using NHibernate.Cfg;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;

using AlwaysMoveForward.Common.DataLayer;

namespace AlwaysMoveForward.Common.DataLayer.ActiveRecord
{
    /// <summary>
    /// A unit of work made to work with NHibernate transactions
    /// </summary>
    public abstract class ActiveRecordUnitOfWork : IUnitOfWork, IDisposable
    {
        private static bool isInitialized = false;
        
        /// <summary>
        /// A default constructor that will result in using NHibernate configuration settings just from the .config file
        /// </summary>
        public ActiveRecordUnitOfWork(Assembly dtoAssembly) : this(dtoAssembly, false)
        {

        }

        /// <summary>
        /// A default constructor that will result in using NHibernate configuration settings just from the .config file
        /// </summary>
        public ActiveRecordUnitOfWork(Assembly dtoAssembly, bool createSession)
        {
            if (ActiveRecordUnitOfWork.isInitialized == false)
            {
                Castle.ActiveRecord.Framework.IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activeRecord") as Castle.ActiveRecord.Framework.IConfigurationSource;
                Castle.ActiveRecord.ActiveRecordStarter.Initialize(dtoAssembly, source);

                NHibernate.Cfg.Environment.UseReflectionOptimizer = false;

                ActiveRecordUnitOfWork.isInitialized = true;
            }

            if(createSession == true)
            {
                this.sessionCope = new SessionScope();
            }
        }

        private SessionScope sessionCope;

        private TransactionScope transactionScope;

        #region IUnitOfWork Members

        /// <summary>
        /// Begins a transaction with a default isolation level
        /// </summary>
        /// <returns>A reference to the transaction as an IDisposable</returns>
        public IDisposable BeginTransaction()
        {
            return this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Begins a transaction with a specific isolation level
        /// </summary>
        /// <param name="isolationLevel">The isolation level for the transaction</param>
        /// <returns>A reference to the transaction as an IDisposable</returns>
        public IDisposable BeginTransaction(IsolationLevel isolationLevel)
        {
            transactionScope = new TransactionScope(TransactionMode.Inherits, isolationLevel, OnDispose.Commit);
            return transactionScope;
        }

        /// <summary>
        /// Ends the current transaction, if can commit is true then it commits, otherwise it rolls back
        /// </summary>
        /// <param name="canCommit">Can the current transaction be commited to the database</param>
        public void EndTransaction(bool canCommit)
        {
            if(this.transactionScope!=null)
            {
                if (canCommit == true)
                {
                    this.transactionScope.VoteCommit();
                }
                else
                {
                    this.transactionScope.VoteRollBack();
                }

                this.transactionScope.Flush();
            }
        }

        /// <summary>
        /// Flush any changes to the database (if outside of a transaction, otherwise NHibernate will make sure the changes don't get written
        /// unitl the transaction is committed
        /// </summary>
        public void Flush()
        {
            if (this.transactionScope != null)
            {
                this.transactionScope.Flush();
            }
        }

        #endregion

        /// <summary>
        /// Dispose of the elements contained in this class
        /// </summary>
        public void Dispose()
        {
            if (this.transactionScope != null)
            {
                this.transactionScope.Dispose();
                this.transactionScope = null;
            }
        }
    }        
}
