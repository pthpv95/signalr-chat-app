using System;
using MySql.Data.MySqlClient;
using MySqlConnector;

namespace chatservices.Db
{
    public class ChatDbConnection : IDisposable
    {
        private readonly string _connectionString;

        public ChatDbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection Connection => new MySqlConnection(_connectionString);

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
