using WC.Library.Data.Models;
using WC.Service.PersonalData.Shared.Models;

namespace WC.Service.PersonalData.Data.Models;

public class PersonalDataEntity : EntityBase
{
    public Guid EmployeeId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public UserRole Role { get; set; }
}
