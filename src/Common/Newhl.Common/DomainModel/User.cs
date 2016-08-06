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
using System.Web;
using System.Text;
using Newhl.Common.DomainModel.DataMap;

namespace Newhl.Common.DomainModel
{
    public class User
    {
        public User() 
        {
            this.Id = -1;
        }

        public User(long userId)
        {
            this.Id = userId;
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string GetDisplayName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
