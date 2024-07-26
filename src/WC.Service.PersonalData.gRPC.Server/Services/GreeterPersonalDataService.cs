using Grpc.Core;
using WC.Service.Employees.gRPC.Client.Clients;
using WC.Service.Employees.gRPC.Client.Models.Employee;
using WC.Service.PersonalData.Domain.Models;
using WC.Service.PersonalData.Domain.Services;
using WC.Service.PersonalData.gRPC.Server.Services;

namespace WC.Service.EmailDomains.gRPC.Server.Services;

public class GreeterPersonalDataService : GreeterPersonalData.GreeterPersonalDataBase
{
    private readonly IPersonalDataManager _manager;
    private readonly IPersonalDataProvider _provider;
    private readonly IGreeterEmployeesClient _employeesClient;

    public GreeterPersonalDataService(
        IPersonalDataManager manager,
        IPersonalDataProvider provider,
        IGreeterEmployeesClient employeesClient)
    {
        _manager = manager;
        _provider = provider;
        _employeesClient = employeesClient;
    }

    public override async Task<CreateEmployeeWithPersonalDataResponse> CreateEmployeeWithPersonalData(
        CreateEmployeeWithPersonalDataRequest request,
        ServerCallContext context)
    {
        var employeeCreateItem = await _employeesClient.Create(
            new EmployeeCreateRequestModel
            {
                Name = request.Employee.Name,
                Surname = request.Employee.Surname,
                Patronymic = request.Employee.Patronymic,
                PositionId = Guid.Parse(request.Employee.PositionId)
            }, context.CancellationToken);

        var createItem = await _manager.Create(new PersonalDataModel
        {
            EmployeeId = employeeCreateItem.Id,
            Email = request.PersonalData.Email,
            Password = request.PersonalData.Password
        }, context.CancellationToken);

        return new CreateEmployeeWithPersonalDataResponse { EmployeeId = createItem.Id.ToString() };
    }

    public override async Task<ExistResponse> VerifyCredentials(
        VerifyCredentialsRequest request,
        ServerCallContext context)
    {
        var result = await _provider.DoesEmailAndPasswordExist(
            new PersonalDataModel
            {
                Email = request.Email,
                Password = request.Password,
                EmployeeId = default
            }, context.CancellationToken);

        return new ExistResponse { Exist = result };
    }
}
