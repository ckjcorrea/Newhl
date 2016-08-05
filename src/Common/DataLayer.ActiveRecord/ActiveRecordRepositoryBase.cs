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
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.Common.DataLayer.ActiveRecord
{
    public abstract class ActiveRecordRepositoryBase<TDomainType, TDTOType, TIdType> : RepositoryBase<ActiveRecordUnitOfWork, TDomainType, TDTOType, TIdType>, IRepository<TDomainType, TIdType> 
        where TDomainType : class, new()
        where TDTOType : class, new()
    {
        /// <summary>
        /// The default constructor that takes the current unit of work as a parameter
        /// </summary>
        /// <param name="unitOfWork">The current unit of work</param>
        protected ActiveRecordRepositoryBase(ActiveRecordUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Get a Domain instance by a specific property and value
        /// </summary>
        /// <param name="idPropertyName">The property name</param>
        /// <param name="idValue">The value to search for</param>
        /// <returns>A domain object instance</returns>
        public override TDomainType GetByProperty(string idPropertyName, object idValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TDTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));

            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<TDTOType>.FindOne(criteria));
        }

        /// <summary>
        /// Get all instances found in the database
        /// </summary>
        /// <returns>A list of domain objects</returns>
        public override IList<TDomainType> GetAll()
        {
            DetachedCriteria criteria = DetachedCriteria.For<TDTOType>();
            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<TDTOType>.FindAll(criteria));
        }

        /// <summary>
        /// Get all records that have a property with a specific value
        /// </summary>
        /// <param name="idPropertyName">The name of the property</param>
        /// <param name="idValue">The value of the property</param>
        /// <returns>A list of domain objects</returns>
        public override IList<TDomainType> GetAllByProperty(string idPropertyName, object idValue)
        {
            DetachedCriteria criteria = DetachedCriteria.For<TDTOType>();
            criteria.Add(Expression.Eq(idPropertyName, idValue));
            return this.GetDataMapper().Map(Castle.ActiveRecord.ActiveRecordMediator<TDTOType>.FindAll(criteria));
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
        public override TDomainType Save(TDomainType itemToSave)
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
                    Castle.ActiveRecord.ActiveRecordMediator<TDTOType>.Save(dtoItemToSave);
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
        public override bool Delete(TDomainType itemToDelete)
        {
            bool retVal = false;

            if (itemToDelete != null)
            {
                TDTOType dtoItemToDelete = this.GetDTOById(itemToDelete);

                if (dtoItemToDelete != null)
                {
                    Castle.ActiveRecord.ActiveRecordMediator<TDTOType>.Delete(dtoItemToDelete);
                    this.UnitOfWork.Flush();
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}
