using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Tests.Services.PersonalData;

public static class PersonalData
{
    public static readonly Func<PersonalDataModel> PersonalDataModel = () => new PersonalDataModel
    {
        EmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Email = "test@test.com",
        Password = "Test@1234",
        Role = 0
    };

    public static readonly Func<PersonalDataEntity> PersonalDataEntity = () => new PersonalDataEntity
    {
        EmployeeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Email = "test@test.com",
        Password = "Test@1234",
        Role = 0
    };
}
