using Microsoft.Data.SqlClient;
using System.Data;

namespace WebAPI.Data
{
    public class ADONetDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ADONetDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
