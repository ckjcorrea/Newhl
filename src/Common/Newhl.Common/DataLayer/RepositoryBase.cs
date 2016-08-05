using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Newhl.Common.Utilities;

namespace Newhl.Common.DataLayer
{
    public abstract class RepositoryBase<TUnitOfWorkType, TDomainType, TDTOType, TIdType> 
        : IRepository<TDomainType, TIdType>
        where TUnitOfWorkType : IUnitOfWork
        where TDomainType : class, new()
        where TDTOType : class, new()
    {
        public RepositoryBase(TUnitOfWorkType unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public TUnitOfWorkType UnitOfWork { get; set; }

        /// <summary>
        /// Gets the associated DataMapper.  This allows for common functionality to be implemented in this base class
        /// </summary>
        /// <returns>An instance of the appropriate DataMapper</returns>
        protected abstract DataMapBase<TDomainType, TDTOType> GetDataMapper();
    
        /// <summary>
        /// Get an instance of the dto by its database id (which is domain type specfic, so this is abstract)
        /// </summary>
        /// <param name="domainInstance">A domain object instance</param>
        /// <returns>An instance of the DTO type</returns>
        protected abstract TDTOType GetDTOById(TDomainType domainInstance);

        /// <summary>
        /// Gets an instance of the DTO object based upon the domain object.  This allows for common functionality to be implemented in this base class
        /// </summary>
        /// <param name="idSource">An instance of the related Domain type</param>
        /// <returns>An instance of the DTO object</returns>
        protected abstract TDTOType GetDTOById(TIdType idSource);

        /// <summary>
        /// Gets an instance of the Domain object based upon the specific id object
        /// </summary>
        /// <param name="idValue">The id object instance</param>
        /// <returns>A Domain object instance</returns>
        public TDomainType GetById(TIdType idValue)
        {
            return this.GetDataMapper().Map(this.GetDTOById(idValue));
        }

        public abstract TDomainType GetByProperty(string idPropertyName, object idValue);
        public abstract IList<TDomainType> GetAll();
        public abstract IList<TDomainType> GetAllByProperty(string idPropertyName, object idValue);
        public abstract TDomainType Save(TDomainType itemToSave);
        public abstract bool Delete(TDomainType itemToDelete);

        public virtual bool DeleteDependencies(TDomainType parentItem) { return true; }
    }
}
