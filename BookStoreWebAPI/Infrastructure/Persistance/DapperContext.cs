using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Persistance
{
    public class DapperContext
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("SecondPC"));
            Connection.Open();
        }
    }
}