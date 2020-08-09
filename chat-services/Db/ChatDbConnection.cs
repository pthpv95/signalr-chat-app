using System;
using MySql.Data.MySqlClient;

namespace chatservices.Db
{
    public class ChatDbConnection : IDisposable
    {
        private readonly string _connectionString;

        public ChatDbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection Connection
        {
            get
            {
                return new MySqlConnection(_connectionString);
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
