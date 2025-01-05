using AppointmentSearch.Application.Doctors.SearchDoctors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSearch.Api.Controllers.Doctors;
[ApiController]
[Route("api/doctors")]
public class DoctorController: ControllerBase
{
    private readonly ISender _sender;

    public DoctorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> SearchDoctor(
        DateOnly startDate,
        DateOnly endDate,
        CancellationToken cancellationToken
    )
    {
        var query = new SearchDoctorsQuery(startDate, endDate);
        var Doctors =await _sender.Send(query, cancellationToken);
        return Ok(Doctors.Value);
    }
}