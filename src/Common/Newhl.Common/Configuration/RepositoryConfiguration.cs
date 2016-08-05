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
using System.Configuration;

namespace Newhl.Common.Configuration
{
    public class RepositoryConfiguration : ConfigurationSection 
    {
        public const string ManagerClassSetting = "ManagerClass";
        public const string ManagerAssemblySetting = "ManagerAssembly";
        public const string UnitOfWorkClassSetting = "UnitOfWorkClass";

        public const string DefaultConfiguration = "Newhl/RepositoryConfiguration";

        public RepositoryConfiguration() { }
        public RepositoryConfiguration(string managerClass, string managerAssembly, string unitOfWorkClass)
        {
            this.ManagerClass = managerClass;
            this.ManagerAssembly = managerAssembly;
            this.UnitOfWorkClass = unitOfWorkClass;
        }
        /// <summary>
        /// Define the email address outgoing emails are tagged with.
        /// </summary>
        [ConfigurationProperty(ManagerClassSetting, IsRequired = true)]
        public string ManagerClass
        {
            get { return (string)this[ManagerClassSetting]; }
            set { this[ManagerClassSetting] = value; }
        }
        /// <summary>
        /// Define the email server to use
        /// </summary>
        [ConfigurationProperty(ManagerAssemblySetting, IsRequired = true)]
        public string ManagerAssembly
        {
            get { return (string)this[ManagerAssemblySetting]; }
            set { this[ManagerAssemblySetting] = value; }
        }

        [ConfigurationProperty(UnitOfWorkClassSetting, IsRequired = true)]
        public string UnitOfWorkClass
        {
            get { return (string)this[UnitOfWorkClassSetting]; }
            set { this[UnitOfWorkClassSetting] = value; }
        }
    }
}
