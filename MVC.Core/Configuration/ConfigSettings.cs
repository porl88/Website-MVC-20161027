namespace MVC.Core.Configuration
{
	using System;
	using System.Collections.Specialized;
	using System.Configuration;

	public sealed class ConfigSettings
	{
		public static string GetConfigValue(string section, string key, string defaultValue)
		{
			try
			{
				var configSection = (NameValueCollection)ConfigurationManager.GetSection(section);

				if (configSection == null || configSection[key] == null)
				{
					return defaultValue;
				}
				else
				{
					return configSection[key];
				}
			}
			catch (Exception)
			{
				return defaultValue;
			}
		}

		public static string GetApplicationSetting(string key, string defaultValue)
		{
			if (ConfigurationManager.AppSettings[key] != null)
			{
				return ConfigurationManager.AppSettings[key];
			}

			return defaultValue;
		}
	}
}
