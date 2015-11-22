namespace MVC.Core.Configuration
{
	public class SystemSettings : ISystemSettings
	{
		public bool IsProductionEnviroment
		{
			get { return ConfigSettings.GetApplicationSetting("ProductionEnvironment", "1") == "1"; }
		}

		public string DwsDomainName
		{
			get { return ConfigSettings.GetApplicationSetting("DWSDomainName", string.Empty); }
		}
	}
}
