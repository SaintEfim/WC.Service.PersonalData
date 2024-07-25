using Grpc.Net.Client;
using WC.Library.Domain.Models;
using WC.Service.PersonalData.gRPC.Client.Models;
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

    public async Task<CreateResultModel> Create(
        PersonalDataCreateRequestModel request,
        CancellationToken cancellationToken = default)
    {
        var createResult = await _client.CreateAsync(
            new PersonalDataCreateRequest
            {
                PersonalData = new PersonalData
                {
                    Email = request.Email,
                    Password = request.Password
                }
            }, cancellationToken: cancellationToken);

        return new CreateResultModel { Id = Guid.Parse(createResult.Id) };
    }

    public async Task<VerifyEmployeeCredentialsResponseModel> VerifyEmployeeCredentials(
        VerifyEmployeeCredentialsRequestModel request,
        CancellationToken cancellationToken = default)
    {
        var verifyResult = await _client.VerifyEmployeeCredentialsAsync(
            new VerifyEmployeeCredentialsRequest
            {
                PersonalData = new PersonalData
                {
                    Email = request.Email,
                    Password = request.Password
                }
            }, cancellationToken: cancellationToken);

        return new VerifyEmployeeCredentialsResponseModel { Exists = verifyResult.Exist };
    }
}
