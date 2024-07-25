using WC.Library.Domain.Models;
using WC.Service.PersonalData.gRPC.Client.Models;
using WC.Service.PersonalData.gRPC.Client.Models.Verify;

namespace WC.Service.PersonalData.gRPC.Client.Clients;

public interface IGreeterPersonalDataClient
{
    Task<CreateResultModel> Create(
        PersonalDataCreateRequestModel request,
        CancellationToken cancellationToken = default);

    Task<VerifyEmployeeCredentialsResponseModel> VerifyEmployeeCredentials(
        VerifyEmployeeCredentialsRequestModel request,
        CancellationToken cancellationToken = default);
}
