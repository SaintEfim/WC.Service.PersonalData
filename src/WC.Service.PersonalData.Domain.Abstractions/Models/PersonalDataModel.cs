using WC.Library.Domain.Models;

namespace WC.Service.PersonalData.Domain.Models;

public class PersonalDataModel : ModelBase
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
