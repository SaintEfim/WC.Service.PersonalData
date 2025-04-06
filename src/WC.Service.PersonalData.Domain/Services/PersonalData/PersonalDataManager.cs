using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using WC.Library.BCryptPasswordHash;
using WC.Library.Data.Services;
using WC.Library.Domain.Services;
using WC.Library.Domain.Validators;
using WC.Library.Shared.Exceptions;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;
using WC.Service.PersonalData.Shared.Models;

namespace WC.Service.PersonalData.Domain.Services;

public class PersonalDataManager
    : DataManagerBase<PersonalDataManager, IPersonalDataRepository, PersonalDataModel, PersonalDataEntity>,
        IPersonalDataManager
{
    private readonly ILogger<PersonalDataManager> _logger;
    private readonly IBCryptPasswordHasher _passwordHasher;
    private readonly IWcTransactionService _transactionService;

    public PersonalDataManager(
        IMapper mapper,
        ILogger<PersonalDataManager> logger,
        IPersonalDataRepository repository,
        IEnumerable<IValidator> validators,
        IBCryptPasswordHasher passwordHasher,
        IWcTransactionService transactionService)
        : base(mapper, logger, repository, validators)
    {
        _passwordHasher = passwordHasher;
        _transactionService = transactionService;
        _logger = logger;
    }

    public async Task ResetPassword(
        PersonalDataModel model,
        IWcTransaction? transaction = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Resetting password for personal data with Id: {Id}", model.Id);
            Validate<IDomainUpdateValidator>(model, cancellationToken);

            var personalModel = await Repository.GetOneById(model.Id, transaction: transaction,
                cancellationToken: cancellationToken);
            var personalDataPatch = Mapper.Map<PersonalDataEntity>(personalModel);
            personalDataPatch.Password = _passwordHasher.Hash(model.Password);

            await Repository.Update(personalDataPatch, transaction, cancellationToken);
            _logger.LogInformation("Password successfully reset for personal data with Id: {Id}", model.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed for resetting password for personal data with Id: {Id}",
                model.Id);
            throw;
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Personal data not found for Id: {Id}", model.Id);
            throw;
        }
    }

    protected override async Task<PersonalDataModel> CreateAction(
        PersonalDataModel model,
        IWcTransaction? transaction = default,
        CancellationToken cancellationToken = default)
    {
        return await _transactionService.Execute(async (
            tr,
            token) =>
        {
            try
            {
                _logger.LogInformation("Creating personal data for Id: {Id}", model.Id);

                model.Email = model.Email.ToLower();

                model.Password = _passwordHasher.Hash(model.Password);

                var result = await base.CreateAction(model, tr, token);

                _logger.LogInformation("Successfully created personal data with Id: {Id}", result.Id);

                var emailLocalPart = Environment.GetEnvironmentVariable("ADMIN_EMAIL_LOCAL_PART") ?? "admin";
                var emailDomain = Environment.GetEnvironmentVariable("ADMIN_EMAIL_DOMAIN") ?? "admin.com";
                var expectedEmail = $"{emailLocalPart}@{emailDomain}".ToLower();
                var expectedPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "Admin@12345678";

                if (result.Email != expectedEmail || !_passwordHasher.Verify(expectedPassword, result.Password))
                {
                    return result;
                }

                result.Role = UserRole.Admin;
                _logger.LogInformation("Assigned role 'user.Admin' to personal data with Id: {Id}", result.Id);

                await UpdateAction(result, tr, token);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating personal data for Id: {Id}", model.Id);
                throw;
            }
        }, transaction, cancellationToken);
    }

    public override async Task<PersonalDataModel> Delete(
        Guid id,
        IWcTransaction? transaction = default,
        CancellationToken cancellationToken = default)
    {
        var personalData = (await Repository.Get(transaction: transaction, cancellationToken: cancellationToken))
            .SingleOrDefault(x => x.EmployeeId == id);

        return await base.DeleteAction(personalData!.Id, transaction, cancellationToken);
    }
}
