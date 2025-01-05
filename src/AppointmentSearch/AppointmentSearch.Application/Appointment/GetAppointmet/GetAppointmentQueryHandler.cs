using System.Runtime.InteropServices;
using AppointmentSearch.Application.Abstractions.Data;
using AppointmentSearch.Application.Abstractions.Messaging;
using AppointmentSearch.Domain.Abstractions;
using Dapper;
using MediatR;

namespace AppointmentSearch.Application.Appointment.GetAppointmet;

internal sealed class GetAppointmentQueryHandler : IQueryHandler<GetAppointmentQuery, AppointmentResponse>
{
    private readonly ISQLConnectionFactory _sqlConnectionFactory;

    public GetAppointmentQueryHandler(ISQLConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AppointmentResponse>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql ="SELECT Id, DoctorId, UserId, Status, Price, Currency, StartDate, EndDate, CreatedOnUtc FROM Appointments WHERE Id = @Id";
        var appointment= await connection.QueryFirstOrDefaultAsync<AppointmentResponse>(sql, new { Id = request.AppointmentId });

        return appointment;
    }

}