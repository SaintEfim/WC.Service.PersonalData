using AutoMapper;
using Microsoft.Extensions.Logging;
using WC.Library.BCryptPasswordHash;
using WC.Library.Domain.Services;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services;

public class PersonalDataProvider
    : DataProviderBase<PersonalDataProvider, IPersonalDataRepository, PersonalDataModel, PersonalDataEntity>,
        IPersonalDataProvider
{
    private readonly IBCryptPasswordHasher _passwordHasher;

    public PersonalDataProvider(
        IMapper mapper,
        ILogger<PersonalDataProvider> logger,
        IPersonalDataRepository repository,
        IBCryptPasswordHasher passwordHasher)
        : base(mapper, logger, repository)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> DoesEmailAndPasswordExist(
        PersonalDataModel model,
        CancellationToken cancellationToken = default)
    {
        var personalDataEntities = await Repository.Get(cancellationToken: cancellationToken);
        var personalData = personalDataEntities.SingleOrDefault(x =>
            _passwordHasher.Verify(x.Email, model.Email) && _passwordHasher.Verify(x.Password, model.Password) &&
            x.Id == model.Id);

        return personalData != null;
    }
}
