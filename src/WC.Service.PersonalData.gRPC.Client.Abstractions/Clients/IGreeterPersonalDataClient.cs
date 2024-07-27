using WC.Service.PersonalData.gRPC.Client.Models;
using WC.Service.PersonalData.gRPC.Client.Models.Create;
using WC.Service.PersonalData.gRPC.Client.Models.Verify;

namespace WC.Service.PersonalData.gRPC.Client.Clients;

public interface IGreeterPersonalDataClient
{
    Task<PersonalDataCreateResponseModel> Create(
        PersonalDataCreateRequestModel request,
        CancellationToken cancellationToken = default);

    Task Update(
        PersonalDataUpdateRequestModel request,
        CancellationToken cancellationToken = default);

    Task Delete(
        PersonalDataDeleteRequestModel request,
        CancellationToken cancellationToken = default);

    Task<VerifyCredentialsResponseModel> VerifyCredentials(
        VerifyCredentialsRequestModel request,
        CancellationToken cancellationToken = default);
}
