using WC.Library.Domain.Models;

namespace WC.Service.PersonalData.Domain.Models;

public class PersonalDataModel : ModelBase
{
    public Guid EmployeeId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
