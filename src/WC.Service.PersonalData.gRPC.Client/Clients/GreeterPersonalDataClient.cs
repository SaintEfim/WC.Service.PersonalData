using Grpc.Net.Client;
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
            EmployeeId = Guid.Parse(verifyResult.EmployeeId),
            Role = verifyResult.Role
        };
    }
}
