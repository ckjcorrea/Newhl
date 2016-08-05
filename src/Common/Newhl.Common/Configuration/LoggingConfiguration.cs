using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Newhl.Common.Configuration
{
    public class LoggingConfiguration : ConfigurationSection
    {
        public const string DefaultConfigurationSetting = "Newhl/LoggingConfiguration";

        private const string SourceSetting = "Source";
        private const string LevelSetting = "Level";
        private const string LoggingClassSetting = "LoggingClass";
        private const string LoggingAssemblySetting = "LoggingAssembly";

        public LoggingConfiguration() { }

        [ConfigurationProperty(LoggingConfiguration.SourceSetting, IsRequired = true)]
        public string Source
        {
            get { return (string)this[LoggingConfiguration.SourceSetting]; }
            set { this[LoggingConfiguration.SourceSetting] = value; }
        }

        [ConfigurationProperty(LoggingConfiguration.LevelSetting, IsRequired = true)]
        public string Level
        {
            get { return (string)this[LoggingConfiguration.LevelSetting]; }
            set { this[LoggingConfiguration.LevelSetting] = value; }
        }

        [ConfigurationProperty(LoggingConfiguration.LoggingClassSetting, IsRequired = false)]
        public string LoggingClass
        {
            get { return (string)this[LoggingConfiguration.LoggingClassSetting]; }
            set { this[LoggingConfiguration.LoggingClassSetting] = value; }
        }

        [ConfigurationProperty(LoggingConfiguration.LoggingAssemblySetting, IsRequired = false)]
        public string LoggingAssembly
        {
            get { return (string)this[LoggingConfiguration.LoggingAssemblySetting]; }
            set { this[LoggingConfiguration.LoggingAssemblySetting] = value; }
        }
    }
}