using WC.Library.Domain.Models;
using WC.Service.PersonalData.gRPC.Client.Models;

namespace WC.Service.PersonalData.gRPC.Client.Clients;

public interface IGreeterPersonalDataClient
{
    Task<CreateResultModel> Create(
        CreateEmployeeWithPersonalDataRequestModel request,
        CancellationToken cancellationToken = default);

    Task<ExistResponseModel> VerifyCredentials(
        VerifyCredentialsRequestModel request,
        CancellationToken cancellationToken = default);
}
