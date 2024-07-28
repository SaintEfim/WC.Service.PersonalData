namespace WC.Service.PersonalData.gRPC.Client.Models.Verify;

public class VerifyCredentialsResponseModel
{
    public required Guid EmployeeId { get; set; }

    public required string Role { get; set; }
}
