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
    /// <summary>
    /// Defines the configuration file entries neccessary to drive email integration in the system.
    /// </summary>
    public class EmailConfiguration : ConfigurationSection
    {
        public const string FromAddressSetting = "FromAddress";
        public const string SmtpServerSetting = "SmtpServer";
        public const string SmtpPortSetting = "SmtpPort";
        public const string IsDebugModeSetting = "IsDebugMode";

        public const string DefaultConfiguration = "Newhl/EmailConfiguration";

        public static EmailConfiguration GetInstance()
        {
            return EmailConfiguration.GetInstance(DefaultConfiguration);
        }

        public static EmailConfiguration GetInstance(string configurationSection)
        {
            return (EmailConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
        }

        public EmailConfiguration() { }
        public EmailConfiguration(string fromAddress, string smtpServer)
        {
            this.FromAddress = fromAddress;
            this.SmtpServer = smtpServer;
        }

        [ConfigurationProperty(IsDebugModeSetting, IsRequired = false)]
        public bool IsDebugMode
        {
            get { return true; }//(bool)this[IsDebugModeSetting]; }
            set { this[IsDebugModeSetting] = value; }
        }
        /// <summary>
        /// Define the email address outgoing emails are tagged with.
        /// </summary>
        [ConfigurationProperty(FromAddressSetting, IsRequired = true)]
        public string FromAddress
        {
            get { return (string)this[FromAddressSetting]; }
            set { this[FromAddressSetting] = value; }
        }
        /// <summary>
        /// Define the email server to use
        /// </summary>
        [ConfigurationProperty(SmtpServerSetting, IsRequired = true)]
        public string SmtpServer
        {
            get { return (string)this[SmtpServerSetting]; }
            set { this[SmtpServerSetting] = value; }
        }
        /// <summary>
        /// Define the email port to use
        /// </summary>
        [ConfigurationProperty(SmtpPortSetting, IsRequired = true)]
        public int SmtpPort
        {
            get { return (int)this[SmtpPortSetting]; }
            set { this[SmtpPortSetting] = value; }
        }
    }
}
