using Microsoft.Extensions.Configuration;

namespace Settings
{
    public class StoreSetting
    {
        private static StoreSetting _instance = null;
        private static readonly object padlock = new object();
        public string _sqliteConnectionString = string.Empty;

        private StoreSetting()
        {
            InitializeSettings();
        }

        public static StoreSetting Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new StoreSetting();
                    }
                    return _instance;
                }
            }
        }

        public string SqliteConnectionString
        {
            get => _sqliteConnectionString;
        }

        private void InitializeSettings(){
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString())
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false)
            .AddJsonFile(Path.Combine("Settings","StoreSettings.json"), optional: false)
            .Build();

            _sqliteConnectionString = configuration.GetSection("ConnectionStrings").GetSection("SqliteConnection").Value;
        }

    }
}