using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TimeTrackerApi.Models;

namespace TimeTrackerApi.Services
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
