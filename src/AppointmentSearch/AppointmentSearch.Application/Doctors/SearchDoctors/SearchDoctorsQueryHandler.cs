using AppointmentSearch.Application.Abstractions.Data;
using AppointmentSearch.Application.Abstractions.Messaging;
using AppointmentSearch.Domain.Abstractions;
using AppointmentSearch.Domain.Appointments;
using Dapper;

namespace AppointmentSearch.Application.Doctors.SearchDoctors;

internal sealed class SearchDoctorsQueryHandler : IQueryHandler<SearchDoctorsQuery, IReadOnlyList<DoctorResponse>>
{
    public static readonly int[] ActiveAppointmentsStatuses = 
    {(int)Status.Reserved, (int)Status.Confirmed, (int)Status.Completed};
    private readonly ISQLConnectionFactory _connectionFactory;

    public SearchDoctorsQueryHandler(ISQLConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IReadOnlyList<DoctorResponse>>> Handle(SearchDoctorsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<DoctorResponse>();
        }

        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
         SELECT a.id
         , a.name as Name
         , a.last_Name as LastName
         , a.salary_amount as Salary
         , a.salary_currency as Currency
         , a.address_country as Country
         , a.address_street as Street
         , a.address_city as City
         , a.address_state as State
         , a.address_zip_code as ZipCode
            FROM Doctors As a
            WHERE NOT EXISTS (
                SELECT 1
                FROM Appointments As b
                WHERE b.doctor_id = a.Id
                AND b.start<= @EndDate
                AND b.end>= @StartDate
                AND b.status <> ALL(@ActiveAppointmentsStatuses)
            )
        """;
        var doctors = await connection.QueryAsync<DoctorResponse, AddressResponse, DoctorResponse>(sql
           ,
           (Doctor, Address) => { Doctor.Address = Address; return Doctor; },
                       new { 
                        StartDate= request.StartDate, 
                        EndDate= request.EndDate,
                        ActiveAppointmentsStatuses
                         },
                         splitOn: "Country"
                         );

        return doctors.ToList();
    }
}