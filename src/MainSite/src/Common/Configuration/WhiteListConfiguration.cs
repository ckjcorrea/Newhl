using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AlwaysMoveForward.OAuth.Common.Configuration
{
    public class WhiteListConfiguration : ConfigurationSection
    {
         /// <summary>
        /// The Request Token uri configuration setting.
        /// </summary>
        public const string FolderWhitelistSetting = "Folders";

        /// <summary>
        /// The Request Token uri configuration setting.
        /// </summary>
        public const string FileTypeWhitelistSetting = "FileTypes";

        /// <summary>
        /// The default app.config configuration section
        /// </summary>
        private static readonly string DefaultSection = "AlwaysMoveForward/WhiteList";

        /// <summary>
        /// Gets the instance of the configuration, based on a default section
        /// </summary>
        /// <returns>Returns an instance of this class</returns>
        public static WhiteListConfiguration GetInstance()
        {
            return WhiteListConfiguration.GetInstance(DefaultSection);
        }

        /// <summary>
        /// Gets the instance of the configuration, based on a config section parameter
        /// </summary>
        /// <param name="configurationSection">The configuration section</param>
        /// <returns>Returns an instance of this class.</returns>
        public static WhiteListConfiguration GetInstance(string configurationSection)
        {
            return (WhiteListConfiguration)System.Configuration.ConfigurationManager.GetSection(configurationSection);
        }

        /// <summary>
        /// Gets or sets the Request Token URI string found in the config file.
        /// </summary>
        [ConfigurationProperty(FolderWhitelistSetting, IsRequired = true)]
        public string FolderWhitelist
        {
            get { return (string)this[FolderWhitelistSetting]; }
            set { this[FolderWhitelistSetting] = value; }
        }

        /// <summary>
        /// Gets or sets the Request Token URI string found in the config file.
        /// </summary>
        [ConfigurationProperty(FileTypeWhitelistSetting, IsRequired = true)]
        public string FileTypeWhitelist
        {
            get { return (string)this[FileTypeWhitelistSetting]; }
            set { this[FileTypeWhitelistSetting] = value; }
        }
    }
}
