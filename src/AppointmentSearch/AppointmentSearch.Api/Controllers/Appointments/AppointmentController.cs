using AppointmentSearch.Application.Appointment.GetAppointmet;
using AppointmentSearch.Application.Appointment.ReserveAppointment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSearch.Api.Controllers.Appointments;
[ApiController]
[Route("api/appointments")]
public class AppointmentController: ControllerBase
{
    private readonly ISender _sender;

    public AppointmentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> SearchAppointment(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetAppointmentQuery(id);
        var appointment =await _sender.Send(query, cancellationToken);
        return appointment.IsSuccess ? Ok(appointment.Value) : NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> ReserveAppointment(
        Guid id,
        AppointmentReserveRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new ReserveAppointmentCommand(request.DoctorId,request.UserId,request.StartDate,request.EndDate);
        
        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(SearchAppointment), new
        {
            result.Value
        }) : BadRequest(result.Error);
    }
}