using Microsoft.Extensions.Configuration;

namespace Settings
{
    public class StoreSetting
    {
        private static StoreSetting _instance = null;
        private static readonly object padlock = new object();
        private string _sqliteConnectionString = string.Empty;
        private string _apiUrl = String.Empty;
        private string _databaseProvider = String.Empty;
        private string _corsHttpsOrigin = String.Empty;
        private string _corsHttpOrigin = String.Empty;
        private string _redisConnectionString = String.Empty;
        private string _identitySqliteConnectionString = String.Empty;
        private string _tokenKey = String.Empty;
        private string _tokenIssuer = String.Empty;



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

        public string IdentitySqliteConnectionString
        {
            get => _identitySqliteConnectionString;
        }

        public string ApiUrl
        {
            get => _apiUrl;
        }

        public string DatabaseProvider
        {
            get => _databaseProvider;
        }

        public string CORSHttpsOrigin
        {
            get => _corsHttpsOrigin;
        }

        public string CORSHttpOrigin
        {
            get => _corsHttpOrigin;
        }

        public string RedisConnectionString
        {
            get => _redisConnectionString;
        }

        public string TokenKey
        {
            get => _tokenKey;
        }

        public string TokenIssuer
        {
            get => _tokenIssuer;
        }

        private void InitializeSettings()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString())
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false)
            .AddJsonFile(Path.Combine("Settings", "StoreSettings.json"), optional: false)
            .Build();

            _sqliteConnectionString = configuration.GetSection("ConnectionStrings").GetSection("SqliteConnection").Value;
            _identitySqliteConnectionString = configuration.GetSection("ConnectionStrings").GetSection("IdentitySqliteConnection").Value;
            _apiUrl = configuration.GetSection("ApiUrl").Value;
            _databaseProvider = configuration.GetSection("DatabaseProvider").Value;
            _corsHttpsOrigin = configuration.GetSection("CORSHttpsOrigin").Value;
            _corsHttpOrigin = configuration.GetSection("CORSHttpOrigin").Value;
            _redisConnectionString = configuration.GetSection("ConnectionStrings").GetSection("RedisConnection").Value;
            _tokenKey = configuration.GetSection("Token").GetSection("Key").Value;
            _tokenIssuer = configuration.GetSection("Token").GetSection("Issuer").Value;
        }

    }
}
