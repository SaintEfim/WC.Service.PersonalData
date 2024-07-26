namespace WC.Service.PersonalData.gRPC.Client.Models.Create;

public class PersonalDataCreateRequestModel
{
    public required Guid EmployeeId { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
