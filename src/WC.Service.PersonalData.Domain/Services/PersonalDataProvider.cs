using AutoMapper;
using Microsoft.Extensions.Logging;
using WC.Library.BCryptPasswordHash;
using WC.Library.Data.Services;
using WC.Library.Domain.Services;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services;

public class PersonalDataProvider
    : DataProviderBase<PersonalDataProvider, IPersonalDataRepository, PersonalDataModel, PersonalDataEntity>,
        IPersonalDataProvider
{
    private readonly ILogger<PersonalDataProvider> _logger;
    private readonly IBCryptPasswordHasher _passwordHasher;

    public PersonalDataProvider(
        IMapper mapper,
        ILogger<PersonalDataProvider> logger,
        IPersonalDataRepository repository,
        IBCryptPasswordHasher passwordHasher)
        : base(mapper, logger, repository)
    {
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<PersonalDataModel?> VerifyEmailAndPassword(
        PersonalDataModel model,
        IWcTransaction? transaction = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Starting verification for personal dara: {Email} and {Password}", model.Email,
                model.Password);

            var personalDataEntities =
                await Repository.Get(transaction: transaction, cancellationToken: cancellationToken);
            var personalData = personalDataEntities.SingleOrDefault(x =>
                _passwordHasher.Verify(model.Password, x.Password) && x.Email.Equals(model.Email, StringComparison.CurrentCultureIgnoreCase));

            if (personalData == null)
            {
                _logger.LogWarning(
                    "Verification failed for personal dara: {Email} and {Password} No matching data found.",
                    model.Email, model.Password);
                return null;
            }

            _logger.LogInformation("Verification succeeded for personal dara: {Email} and {Password}", model.Email,
                model.Password);

            return Mapper.Map<PersonalDataModel>(personalData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while verifying personal dara: {Email} and {Password}", model.Email,
                model.Password);
            throw;
        }
    }

    public Task<PersonalDataModel?> GetEmailEmployee(
        Guid employeeId,
        IWcTransaction? transaction = default,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
