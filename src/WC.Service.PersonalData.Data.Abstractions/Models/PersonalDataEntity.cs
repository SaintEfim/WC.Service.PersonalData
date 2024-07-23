using WC.Library.Data.Models;

namespace WC.Service.PersonalData.Data.Models;

public class PersonalDataEntity : EntityBase
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
