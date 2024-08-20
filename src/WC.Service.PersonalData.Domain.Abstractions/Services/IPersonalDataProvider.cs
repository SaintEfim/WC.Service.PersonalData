using WC.Library.Data.Services;
using WC.Library.Domain.Services;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services;

public interface IPersonalDataProvider : IDataProvider<PersonalDataModel>
{
    Task<PersonalDataModel?> VerifyEmailAndPassword(
        PersonalDataModel model,
        IWcTransaction? transaction = default,
        CancellationToken cancellationToken = default);
}
