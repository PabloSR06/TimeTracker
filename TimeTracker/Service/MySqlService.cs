using MySql.Data.MySqlClient;

namespace TimeTracker.Service
{
    public class MySqlService
    {
        private static MySqlConnection? _mySqlConnection;
        public MySqlService(IConfiguration configuration)
        {
            var sqlIsConfigured = !string.IsNullOrEmpty(configuration["sqlkeys:server"])
                && !string.IsNullOrEmpty(configuration["sqlkeys:user"])
                && !string.IsNullOrEmpty(configuration["sqlkeys:password"])
                && !string.IsNullOrEmpty(configuration["sqlkeys:database"]);

            if (sqlIsConfigured)
            {
                string keys = $"server={configuration["sqlkeys:server"]};userid={configuration["sqlkeys:user"]};password={configuration["sqlkeys:password"]};database={configuration["sqlkeys:database"]}";
                _mySqlConnection = new MySqlConnection(keys);
            }
            else
            {
                throw new ArgumentException("SQL not configured");
            }
        }

        public void CloseConnection()
        {
            if (_mySqlConnection != null)
            {
                _mySqlConnection.Close();
            }
        }

        public void TryOpen()
        {
            if (_mySqlConnection != null)
            {
                _mySqlConnection.Open();
            }
        }
        public MySqlConnection GetConnection()
        {
            if (_mySqlConnection != null)
            {
                return _mySqlConnection;
            }
            return null;
        }
    }
}
