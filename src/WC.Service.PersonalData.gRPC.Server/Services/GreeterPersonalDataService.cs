using Grpc.Core;
using WC.Service.PersonalData.Domain.Models;
using WC.Service.PersonalData.Domain.Services;
using WC.Service.PersonalData.gRPC.Server.Services;

namespace WC.Service.EmailDomains.gRPC.Server.Services;

public class GreeterPersonalDataService : GreeterPersonalData.GreeterPersonalDataBase
{
    private readonly IPersonalDataManager _manager;
    private readonly IPersonalDataProvider _provider;

    public GreeterPersonalDataService(
        IPersonalDataManager manager,
        IPersonalDataProvider provider)
    {
        _manager = manager;
        _provider = provider;
    }

    public override async Task<PersonalDataCreateResponse> Create(
        PersonalDataCreateRequest request,
        ServerCallContext context)
    {
        var createItem = await _manager.Create(new PersonalDataModel
        {
            Email = request.PersonalData.Email,
            Password = request.PersonalData.Password
        }, context.CancellationToken);

        return new PersonalDataCreateResponse { Id = createItem.Id.ToString() };
    }

    public override async Task<VerifyEmployeeCredentialsResponse> VerifyEmployeeCredentials(
        VerifyEmployeeCredentialsRequest request,
        ServerCallContext context)
    {
        var result = await _provider.DoesEmailAndPasswordExist(
            new PersonalDataModel
            {
                Email = request.PersonalData.Email,
                Password = request.PersonalData.Password
            }, context.CancellationToken);

        return new VerifyEmployeeCredentialsResponse { Exist = result };
    }
}
