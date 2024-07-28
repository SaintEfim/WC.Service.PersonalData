namespace WC.Service.PersonalData.gRPC.Client.Models.Verify;

public class VerifyCredentialsResponseModel
{
    public required Guid PersonalDataId { get; set; }

    public required string Role { get; set; }
}
