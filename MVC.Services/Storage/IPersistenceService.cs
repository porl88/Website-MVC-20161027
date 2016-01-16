namespace MVC.Services.Storage
{
    public interface IPersistenceService
    {
        string GetValue(string key);

        void SaveValue(string key, string value);

        void DeleteValue(string key);

        void DeleteAllValues();
    }
}
