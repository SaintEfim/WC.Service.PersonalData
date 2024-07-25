namespace WC.Service.PersonalData.gRPC.Client.Models.Verify;

public class VerifyEmployeeCredentialsRequestModel
{
    public required Guid PersonalDataId { get; set; }
    public required string Email { get; set; }

    public required string Password { get; set; }
}
