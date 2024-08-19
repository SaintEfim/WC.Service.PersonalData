using WC.Library.Data.Models;

namespace WC.Service.PersonalData.Data.Models;

public class PersonalDataEntity : EntityBase
{
    public Guid EmployeeId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
