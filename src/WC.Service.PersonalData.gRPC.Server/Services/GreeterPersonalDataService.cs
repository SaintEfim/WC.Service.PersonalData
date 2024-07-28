using Google.Protobuf.WellKnownTypes;
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
            EmployeeId = Guid.Parse(request.EmployeeId),
            Email = request.Email,
            Password = request.Password
        }, context.CancellationToken);

        return new PersonalDataCreateResponse { PersonalDataId = createItem.Id.ToString() };
    }

    public override async Task<Empty> ResetPassword(
        PersonalDataResetPasswordRequest request,
        ServerCallContext context)
    {
        var personalModel = await _provider.GetOneById(Guid.Parse(request.PersonalDataId),
            cancellationToken: context.CancellationToken);

        await _manager.Update(new PersonalDataModel
        {
            Id = Guid.Parse(request.PersonalDataId),
            Email = personalModel!.Email,
            Password = request.Password,
            Role = personalModel.Role
        }, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> Delete(
        PersonalDataDeleteRequest request,
        ServerCallContext context)
    {
        await _manager.Delete(Guid.Parse(request.PersonalDataId), context.CancellationToken);

        return new Empty();
    }

    public override async Task<VerifyCredentialsResponse> VerifyCredentials(
        VerifyCredentialsRequest request,
        ServerCallContext context)
    {
        var resultVerify = await _provider.VerifyEmailAndPassword(new PersonalDataModel
        {
            Email = request.Email,
            Password = request.Password
        }, context.CancellationToken);

        return new VerifyCredentialsResponse
        {
            PersonalDataId = resultVerify.Id.ToString(),
            Role = resultVerify.Role
        };
    }
}
