using WC.Library.Data.Models;

namespace WC.Service.PersonalData.Data.Models;

public class PersonalDataEntity : EntityBase
{
    public required Guid EmployeeId { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Role { get; set; } = "User";
}
