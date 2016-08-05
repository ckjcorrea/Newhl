﻿/**
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

using Newhl.Common.DataLayer;

namespace Newhl.Common.DataLayer
{
    public interface IRepository<TDomainType, TIdType> 
        where TDomainType : class
    {
        TDomainType GetById(TIdType itemId);
        
        TDomainType GetByProperty(string idPropertyName, object idValue);
        
        IList<TDomainType> GetAll();
        
        IList<TDomainType> GetAllByProperty(string idPropertyName, object idValue);
        
        TDomainType Save(TDomainType itemToSave);
        
        bool Delete(TDomainType itemToDelete);
    }
}
