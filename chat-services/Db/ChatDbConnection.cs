using System;
using Npgsql;

namespace chatservices.Db
{
    public class ChatDbConnection : IDisposable
    {
        private readonly string _connectionString;

        public ChatDbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_connectionString);
            }
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
