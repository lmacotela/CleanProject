using System.Data;
using AppointmentSearch.Application.Abstractions.Data;
using Npgsql;

namespace AppointmentSearch.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISQLConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}