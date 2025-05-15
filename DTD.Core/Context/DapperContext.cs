using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace DTD.Core.Context
{
    public class DapperContext : IDisposable
    {
        private readonly ILogger<DapperContext> _logger;
        private readonly string _connectionString;
        private SqlConnection? _sqlConnection;

        public DapperContext(IConfiguration configuration, ILogger<DapperContext> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new Exception("DefaultConnection string not found.");
        }
        public IDbConnection CreateConnection()
        {
            if (_sqlConnection == null || _sqlConnection.State == ConnectionState.Closed)
            {
                if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Broken)
                {
                    _sqlConnection.Dispose();
                    _sqlConnection = null;
                }

                _sqlConnection = new SqlConnection(_connectionString);

                try
                {
                    _sqlConnection.Open();
                    _logger.LogInformation("Database connection opened successfully.");
                }
                catch (Exception ex)
                {
                    _sqlConnection.Dispose();
                    _logger.LogError(ex, "Failed to open database connection.");
                    throw;
                }
            }
            return _sqlConnection;
        }
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            if (_sqlConnection == null || _sqlConnection.State == ConnectionState.Closed)
            {
                if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Broken)
                {
                    _sqlConnection.Dispose();
                    _sqlConnection = null;
                }

                _sqlConnection = new SqlConnection(_connectionString);

                try
                {
                    await _sqlConnection.OpenAsync();
                    _logger.LogInformation("Database connection opened successfully.");
                }
                catch (Exception ex)
                {
                    _sqlConnection.Dispose();
                    _logger.LogError(ex, "Failed to open database connection.");
                    throw;
                }
            }
            return _sqlConnection;
        }
        public void Dispose()
        {
            if (_sqlConnection?.State == ConnectionState.Open)
            {
                _sqlConnection.Close();
                _logger.LogInformation("Database connection closed successfully.");
            }
            _sqlConnection?.Dispose();
        }
    }

}
