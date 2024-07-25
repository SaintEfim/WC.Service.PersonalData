using WC.Library.Domain.Models;

namespace WC.Service.PersonalData.Domain.Models;

public class PersonalDataModel : ModelBase
{
    public required Guid EmployeeId { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
