using WC.Library.Domain.Services;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services;

public interface IPersonalDataProvider : IDataProvider<PersonalDataModel>
{
    Task<bool> DoesEmailAndPasswordExist(
        PersonalDataModel model,
        CancellationToken cancellationToken = default);

    Task<bool> DoesEmailExist(
        string email,
        CancellationToken cancellationToken = default);
}
