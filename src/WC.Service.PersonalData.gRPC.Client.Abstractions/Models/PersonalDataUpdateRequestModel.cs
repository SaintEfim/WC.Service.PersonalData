namespace WC.Service.PersonalData.gRPC.Client.Models;

public class PersonalDataUpdateRequestModel
{
    public required Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
