using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.NHibernate;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// This override defines the base class for the repositories.  The core feature it provides is the
    /// strongly typed DataContext to make it easier to work with the database
    /// </summary>
    /// <typeparam name="TDomainType">The domain typeto return</typeparam>
    /// <typeparam name="TDTOType">The dto type to read/write with</typeparam>
    public abstract class RepositoryBase<TDomainType, TDTOType, TIdType> : NHibernateRepository<TDomainType, TDTOType, TIdType>
        where TDomainType : class, new()
        where TDTOType : class, new()
    {
        /// <summary>
        /// The constructor that takes the current unit of work as a parameter
        /// </summary>
        /// <param name="unitOfWork"></param>
        protected RepositoryBase(IUnitOfWork unitOfWork) : base(unitOfWork as UnitOfWork)
        {
            this.UnitOfWork = unitOfWork as UnitOfWork;
        }

        /// <summary>
        /// Gets the current unit of work
        /// </summary>
        public UnitOfWork UnitOfWork { get; private set; }
    }
}
