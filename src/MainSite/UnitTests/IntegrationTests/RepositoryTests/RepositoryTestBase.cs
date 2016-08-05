using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using Newhl.Common.Configuration;
using Newhl.MainSite.DataLayer;
using Newhl.MainSite.DataLayer.Repositories;

namespace Newhl.MainSite.DevDefined.UnitTests.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class RepositoryTestBase
    {        
        private UnitOfWork unitOfWork;

        protected UnitOfWork UnitOfWork
        {
            get
            {
                if (this.unitOfWork == null)
                {
                    DatabaseConfiguration databaseConfiguration = DatabaseConfiguration.GetInstance();
                    this.unitOfWork = new UnitOfWork(databaseConfiguration.GetDecryptedConnectionString());
                }

                return this.unitOfWork;
            }
        }
     
        private IRepositoryManager repositoryManager;

        protected IRepositoryManager RepositoryManager
        {
            get
            {
                if (this.repositoryManager == null)
                {
                    this.repositoryManager = new RepositoryManager(this.UnitOfWork);
                }

                return this.repositoryManager;
            }
        }
    }
}
