namespace WC.Service.PersonalData.gRPC.Client.Models;

public class PersonalDataResetPasswordRequestModel
{
    public required Guid Id { get; set; }

    public required string Password { get; set; }
}
