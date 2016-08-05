using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Newhl.Common.Utilities;
using Newhl.Common.DataLayer;

namespace Newhl.Common.DataLayer.NHibernate
{
    /// <summary>
    /// A base class for an NHibernate repository that implements some common methods
    /// </summary>
    /// <typeparam name="TDomainType">The domain type to return</typeparam>
    /// <typeparam name="TDTOType">The dto type to use for querying</typeparam>
    public abstract class NHibernateRepository<TDomainType, TDTOType, TIdType>
        where TDomainType : class, new()
        where TDTOType : class, new()
    {
        /// <summary>
        /// The default constructor that takes the current unit of work as a parameter
        /// </summary>
        /// <param name="unitOfWork">The current unit of work</param>
        protected NHibernateRepository(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the current unit of work
        /// </summary>
        public UnitOfWork UnitOfWork { get; private set; }

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

        /// <summary>
        /// Get a Domain instance by a specific property and value
        /// </summary>
        /// <param name="idPropertyName">The property name</param>
        /// <param name="idValue">The value to search for</param>
        /// <returns>A domain object instance</returns>
        public TDomainType GetByProperty(string idPropertyName, object idValue)
        {
            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<TDTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));

            return this.GetDataMapper().Map(criteria.UniqueResult<TDTOType>());
        }

        /// <summary>
        /// Get all instances found in the database
        /// </summary>
        /// <returns>A list of domain objects</returns>
        public IList<TDomainType> GetAll()
        {
            LogManager.GetLogger().Info("Index getting all" + typeof(TDomainType).ToString());
            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<TDTOType>();
            IList<TDTOType> retVal = criteria.List<TDTOType>();
            LogManager.GetLogger().Info("Index found " + retVal.Count);
            return this.GetDataMapper().Map(retVal);
        }

        /// <summary>
        /// Get all records that have a property with a specific value
        /// </summary>
        /// <param name="idPropertyName">The name of the property</param>
        /// <param name="idValue">The value of the property</param>
        /// <returns>A list of domain objects</returns>
        public IList<TDomainType> GetAllByProperty(string idPropertyName, object idValue)
        {
            ICriteria criteria = this.UnitOfWork.CurrentSession.CreateCriteria<TDTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            return this.GetDataMapper().Map(criteria.List<TDTOType>());
        }

                /// <summary>
        /// Save the object to the data store
        /// </summary>
        /// <param name="itemToSave">The item values to save</param>
        /// <returns>The saved domain object</returns>
        public TDomainType Add(TDomainType itemToSave)
        {
            return this.Save(itemToSave);
        }

        /// <summary>
        /// Save the object to the data store
        /// </summary>
        /// <param name="itemToSave">The item values to save</param>
        /// <returns>The saved domain object</returns>
        public TDomainType Update(TDomainType itemToSave)
        {
            return this.Save(itemToSave);
        }

        /// <summary>
        /// Save the object to the data store
        /// </summary>
        /// <param name="itemToSave">The item values to save</param>
        /// <returns>The saved domain object</returns>
        public virtual TDomainType Save(TDomainType itemToSave)
        {
            if (itemToSave != null)
            {
                TDTOType dtoItemToSave = this.GetDTOById(itemToSave);

                if (dtoItemToSave != null)
                {
                    dtoItemToSave = this.GetDataMapper().Map(itemToSave, dtoItemToSave);
                }
                else
                {
                    dtoItemToSave = this.GetDataMapper().Map(itemToSave);
                }

                if (dtoItemToSave != null)
                {
                    this.UnitOfWork.CurrentSession.SaveOrUpdate(dtoItemToSave);
                    this.UnitOfWork.Flush();
                }

                itemToSave = this.GetDataMapper().Map(dtoItemToSave);
            }

            return itemToSave;
        }

        /// <summary>
        /// Remove the object from the data store
        /// </summary>
        /// <param name="itemToDelete">The object to delete</param>
        public virtual bool Delete(TDomainType itemToDelete)
        {
            bool retVal = false;

            if (itemToDelete != null)
            {
                TDTOType dtoItemToDelete = this.GetDTOById(itemToDelete);

                if (dtoItemToDelete != null)
                {
                    this.UnitOfWork.CurrentSession.Delete(dtoItemToDelete);
                    this.UnitOfWork.Flush();
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}
