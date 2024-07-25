namespace WC.Service.PersonalData.gRPC.Client.Models;

public class CreateEmployeeWithPersonalDataRequestModel
{
    public required string Name { get; set; }

    public required string Surname { get; set; }

    public string? Patronymic { get; set; }

    public required Guid PositionId { get; set; }

    public required string Role { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
