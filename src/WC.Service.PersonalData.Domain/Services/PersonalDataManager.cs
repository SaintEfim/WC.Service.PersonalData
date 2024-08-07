using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using WC.Library.BCryptPasswordHash;
using WC.Library.Domain.Services;
using WC.Library.Domain.Validators;
using WC.Library.Shared.Exceptions;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services;

public class PersonalDataManager
    : DataManagerBase<PersonalDataManager, IPersonalDataRepository, PersonalDataModel, PersonalDataEntity>,
        IPersonalDataManager
{
    private readonly ILogger<PersonalDataManager> _logger;
    private readonly IBCryptPasswordHasher _passwordHasher;

    public PersonalDataManager(
        IMapper mapper,
        ILogger<PersonalDataManager> logger,
        IPersonalDataRepository repository,
        IEnumerable<IValidator> validators,
        IBCryptPasswordHasher passwordHasher)
        : base(mapper, logger, repository, validators)
    {
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task ResetPassword(
        PersonalDataModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Resetting password for personal data with Id: {Id}", model.Id);
            Validate<IDomainUpdateValidator>(model, cancellationToken);

            var personalModel = await Repository.GetOneById(model.Id, cancellationToken: cancellationToken);
            var personalDataPatch = Mapper.Map<PersonalDataEntity>(personalModel);
            personalDataPatch.Password = _passwordHasher.Hash(model.Password);

            await Repository.Update(personalDataPatch, cancellationToken);
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
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating personal data for Id: {Id}", model.Id);

            model.Email = _passwordHasher.Hash(model.Email.ToLower());
            model.Password = _passwordHasher.Hash(model.Password);

            var result = await base.CreateAction(model, cancellationToken);

            _logger.LogInformation("Successfully created personal data with Id: {Id}", result.Id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating personal data for Id: {Id}", model.Id);
            throw;
        }
    }
}
