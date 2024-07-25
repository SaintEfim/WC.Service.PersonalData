namespace WC.Service.PersonalData.gRPC.Client.Models;

public class VerifyCredentialsRequestModel
{
    public required Guid PersonalDataId { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
