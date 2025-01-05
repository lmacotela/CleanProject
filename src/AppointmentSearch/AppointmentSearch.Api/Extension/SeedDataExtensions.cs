using AppointmentSearch.Application.Abstractions.Data;
using AppointmentSearch.Domain.Doctors;
using Bogus;
using Dapper;

namespace AppointmentSearch.Api.Extension;
public static class SeedDataExtensions
{
    public static async void SeedData( this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISQLConnectionFactory>();

        using var connection= sqlConnectionFactory.CreateConnection();

        var faker = new Faker();
        List<object> doctors = new();
        for (int i = 0; i < 100; i++)
        {
            doctors.Add(new
            {
                id = Guid.NewGuid(),
                name = faker.Name.FirstName(),
                lastName = faker.Name.LastName(),
                country= faker.Address.Country(),
                street= faker.Address.StreetName(),
                city= faker.Address.City(),
                state= faker.Address.State(),
                zipCode= faker.Address.ZipCode(),
                amount= faker.Random.Decimal(min: 10000, max: 20000),
                currency= "USD",
                lastAppointmentDate= faker.Date.Past(),
                specialities= new List<int> { (int) Speciality.Hematologist, (int) Speciality.Gastroenterologist }
            });             
        }
        const string sql = """
        INSERT INTO public.doctors
        (id, name, last_name, address_country, address_street, address_city, address_state, address_zip_code, salary_amount, salary_currency, last_appointment_date, specialities)
        values (@id, @name, @lastName, @country, @street, @city, @state, @zipCode, @amount, @currency, @lastAppointmentDate, @specialities)
        """;
    await connection.ExecuteAsync(sql, doctors);
    }
}