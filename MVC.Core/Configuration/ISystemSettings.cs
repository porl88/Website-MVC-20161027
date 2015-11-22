namespace MVC.Core.Configuration
{
	public interface ISystemSettings
	{
		bool IsProductionEnviroment { get; }

		string DwsDomainName { get; }
	}
}
