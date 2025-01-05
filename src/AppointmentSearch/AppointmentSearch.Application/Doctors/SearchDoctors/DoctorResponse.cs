namespace AppointmentSearch.Application.Doctors.SearchDoctors;
public sealed class DoctorResponse
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? LastName { get; init; }
    public AddressResponse? Address { get; set; }    
    public decimal Salary { get; init; }
    public string? Currency { get; init; }
}
