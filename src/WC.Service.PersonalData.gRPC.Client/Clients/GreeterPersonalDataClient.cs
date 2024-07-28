using Grpc.Net.Client;
using WC.Service.PersonalData.gRPC.Client.Models;
using WC.Service.PersonalData.gRPC.Client.Models.Create;
using WC.Service.PersonalData.gRPC.Client.Models.Verify;

namespace WC.Service.PersonalData.gRPC.Client.Clients;

public class GreeterPersonalDataClient : IGreeterPersonalDataClient
{
    private readonly GreeterPersonalData.GreeterPersonalDataClient _client;

    public GreeterPersonalDataClient(
        IPersonalDataClientConfiguration configuration)
    {
        var channel = GrpcChannel.ForAddress(configuration.GetBaseUrl());
        _client = new GreeterPersonalData.GreeterPersonalDataClient(channel);
    }

    public async Task<PersonalDataCreateResponseModel> Create(
        PersonalDataCreateRequestModel request,
        CancellationToken cancellationToken = default)
    {
        var createResult = await _client.CreateAsync(
            new PersonalDataCreateRequest
            {
                EmployeeId = request.EmployeeId.ToString(),
                Email = request.Email,
                Password = request.Password
            }, cancellationToken: cancellationToken);

        return new PersonalDataCreateResponseModel { PersonalDataId = Guid.Parse(createResult.PersonalDataId) };
    }

    public async Task ResetPassword(
        PersonalDataResetPasswordRequestModel request,
        CancellationToken cancellationToken = default)
    {
        await _client.ResetPasswordAsync(new PersonalDataResetPasswordRequest
        {
            Id = request.Id.ToString(),
            Password = request.Password
        }, cancellationToken: cancellationToken);
    }

    public async Task Delete(
        PersonalDataDeleteRequestModel request,
        CancellationToken cancellationToken = default)
    {
        await _client.DeleteAsync(new PersonalDataDeleteRequest { PersonalDataId = request.Id.ToString() },
            cancellationToken: cancellationToken);
    }

    public async Task<VerifyCredentialsResponseModel> VerifyCredentials(
        VerifyCredentialsRequestModel request,
        CancellationToken cancellationToken = default)
    {
        var verifyResult = await _client.VerifyCredentialsAsync(new VerifyCredentialsRequest
        {
            Email = request.Email,
            Password = request.Password
        }, cancellationToken: cancellationToken);

        return new VerifyCredentialsResponseModel
        {
            EmployeeId = Guid.Parse(verifyResult.PersonalDataId),
            Role = verifyResult.Role
        };
    }
}
