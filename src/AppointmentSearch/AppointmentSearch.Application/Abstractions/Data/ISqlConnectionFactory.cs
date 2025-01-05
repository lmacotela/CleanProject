using System.Data;

namespace AppointmentSearch.Application.Abstractions.Data;

public interface ISQLConnectionFactory
{
    IDbConnection CreateConnection();
}