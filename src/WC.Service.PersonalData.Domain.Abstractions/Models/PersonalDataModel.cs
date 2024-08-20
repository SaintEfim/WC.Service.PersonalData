using WC.Library.Domain.Models;
using WC.Service.PersonalData.Shared.Models;

namespace WC.Service.PersonalData.Domain.Models;

public class PersonalDataModel : ModelBase
{
    public Guid EmployeeId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public UserRole Role { get; set; }
}
