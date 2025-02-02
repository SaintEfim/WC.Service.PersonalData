using WC.Service.PersonalData.gRPC.Client.Models;
using WC.Service.PersonalData.gRPC.Client.Models.Create;
using WC.Service.PersonalData.gRPC.Client.Models.GetEmailEmployee;
using WC.Service.PersonalData.gRPC.Client.Models.Verify;

namespace WC.Service.PersonalData.gRPC.Client.Clients;

public interface IGreeterPersonalDataClient
{
    Task<PersonalDataCreateResponseModel> Create(
        PersonalDataCreateRequestModel request,
        CancellationToken cancellationToken = default);

    Task ResetPassword(
        PersonalDataResetPasswordRequestModel request,
        CancellationToken cancellationToken = default);

    Task Delete(
        PersonalDataDeleteRequestModel request,
        CancellationToken cancellationToken = default);

    Task<VerifyCredentialsResponseModel> VerifyCredentials(
        VerifyCredentialsRequestModel request,
        CancellationToken cancellationToken = default);

    Task<GetEmailEmployeeResponseModel> GetEmailEmployee(
        GetEmailEmployeeRequestModel request,
        CancellationToken cancellationToken = default);
}
