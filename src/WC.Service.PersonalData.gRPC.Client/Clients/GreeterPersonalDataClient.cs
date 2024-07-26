using Grpc.Net.Client;
using WC.Library.Domain.Models;
using WC.Service.PersonalData.gRPC.Client.Models;

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
        CreateEmployeeWithPersonalDataRequestModel request,
        CancellationToken cancellationToken = default)
    {
        var createResult = await _client.CreateEmployeeWithPersonalDataAsync(
            new CreateEmployeeWithPersonalDataRequest
            {
                Employee = new Employee
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic,
                    PositionId = request.PositionId.ToString()
                },
                PersonalData = new PersonalData
                {
                    Email = request.Email,
                    Password = request.Password
                }
            }, cancellationToken: cancellationToken);

        return new CreateResultModel { Id = Guid.Parse(createResult.EmployeeId) };
    }

    public async Task<ExistResponseModel> VerifyCredentials(
        VerifyCredentialsRequestModel request,
        CancellationToken cancellationToken = default)
    {
        var verifyResult = await _client.VerifyCredentialsAsync(
            new VerifyCredentialsRequest
            {
                Email = request.Email,
                Password = request.Password
            }, cancellationToken: cancellationToken);

        return new ExistResponseModel { Exists = verifyResult.Exist };
    }
}
