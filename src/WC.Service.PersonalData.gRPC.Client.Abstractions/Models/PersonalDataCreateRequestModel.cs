namespace WC.Service.PersonalData.gRPC.Client.Models;

public class PersonalDataCreateRequestModel
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
