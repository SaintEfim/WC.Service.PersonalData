using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using WC.Library.Shared.Exceptions;
using WC.Service.PersonalData.Domain.Models;
using WC.Service.PersonalData.Domain.Services;

namespace WC.Service.PersonalData.API.gRPC.Services;

public class GreeterPersonalDataService : GreeterPersonalData.GreeterPersonalDataBase
{
    private readonly ILogger<GreeterPersonalDataService> _logger;
    private readonly IPersonalDataManager _manager;
    private readonly IPersonalDataProvider _provider;

    public GreeterPersonalDataService(
        IPersonalDataManager manager,
        IPersonalDataProvider provider,
        ILogger<GreeterPersonalDataService> logger)
    {
        _manager = manager;
        _provider = provider;
        _logger = logger;
    }

    public override async Task<PersonalDataCreateResponse> Create(
        PersonalDataCreateRequest request,
        ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("Received Create request for EmployeeId: {EmployeeId}", request.EmployeeId);

            var createItem = await _manager.Create(new PersonalDataModel
            {
                EmployeeId = Guid.Parse(request.EmployeeId),
                Email = request.Email,
                Password = request.Password
            }, cancellationToken: context.CancellationToken);

            _logger.LogInformation("Successfully created personal data with Id: {Id}", createItem.Id);

            return new PersonalDataCreateResponse { PersonalDataId = createItem.Id.ToString() };
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed for Create request with EmployeeId: {EmployeeId}",
                request.EmployeeId);
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Validation failed."), ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing Create request for EmployeeId: {EmployeeId}",
                request.EmployeeId);
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<Empty> ResetPassword(
        PersonalDataResetPasswordRequest request,
        ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("Received ResetPassword request for PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);

            await _manager.ResetPassword(new PersonalDataModel
            {
                Id = Guid.Parse(request.PersonalDataId),
                Password = request.Password
            }, cancellationToken: context.CancellationToken);

            _logger.LogInformation("Successfully reset password for PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);

            return new Empty();
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed for ResetPassword request with PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Validation failed."), ex.Message);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex,
                "Personal data not found for ResetPassword request with PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);
            throw new RpcException(new Status(StatusCode.NotFound, "Personal data not found."), ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error occurred while processing ResetPassword request for PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<Empty> Delete(
        PersonalDataDeleteRequest request,
        ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("Received Delete request for PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);

            await _manager.Delete(Guid.Parse(request.PersonalDataId), cancellationToken: context.CancellationToken);

            _logger.LogInformation("Successfully deleted personal data with PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);

            return new Empty();
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Personal data not found for Delete request with PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);
            throw new RpcException(new Status(StatusCode.NotFound, "Personal data not found."), ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while processing Delete request for PersonalDataId: {PersonalDataId}",
                request.PersonalDataId);
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }

    public override async Task<VerifyCredentialsResponse?> VerifyCredentials(
        VerifyCredentialsRequest request,
        ServerCallContext context)
    {
        try
        {
            _logger.LogInformation("Received VerifyCredentials request for personal dara: {Email} and {Password}",
                request.Email, request.Password);

            var resultVerify = await _provider.VerifyEmailAndPassword(new PersonalDataModel
            {
                Email = request.Email,
                Password = request.Password
            }, cancellationToken: context.CancellationToken);

            if (resultVerify == null)
            {
                _logger.LogWarning("Credentials verification failed for personal dara: {Email} and {Password}",
                    request.Email, request.Password);
                return null;
            }

            _logger.LogInformation("Credentials successfully verified for personal dara: {Email} and {Password}",
                request.Email, request.Password);

            return new VerifyCredentialsResponse
            {
                EmployeeId = resultVerify.Id.ToString(),
                Role = resultVerify.Role.ToString()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while verifying credentials for personal dara: {Email} and {Password}",
                request.Email, request.Password);
            throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."), ex.Message);
        }
    }
}
