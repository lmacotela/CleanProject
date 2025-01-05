using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AppointmentSearch.Application.Abstractions.Data;
using AppointmentSearch.Application.Abstractions.Messaging;
using Dapper;

namespace AppointmentSearch.Application.Doctors.SearchDoctors;

public sealed record SearchDoctorsQuery(
DateOnly StartDate,
DateOnly EndDate
) : IQuery<IReadOnlyList<DoctorResponse>>;