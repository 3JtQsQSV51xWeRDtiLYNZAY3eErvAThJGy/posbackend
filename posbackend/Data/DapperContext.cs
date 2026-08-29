using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace posbackend.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _provider;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") 
                                ?? "Host=localhost;Database=posdb;Username=postgres;Password=postgres";
            _provider = _configuration["DatabaseProvider"] ?? "PostgreSQL";
        }

        public IDbConnection CreateConnection()
        {
            if (_provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                return new SqlConnection(_connectionString);
            }
            return new NpgsqlConnection(_connectionString);
        }
    }
}
