using AutoMapper;
using BCrypt.Net;
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

    public async Task<PersonalDataModel?> VerifyEmailAndPassword(
        PersonalDataModel model,
        CancellationToken cancellationToken = default)
    {
        var personalDataEntities = await Repository.Get(cancellationToken: cancellationToken);

        try
        {
            var personalData = personalDataEntities.SingleOrDefault(x =>
                _passwordHasher.Verify(model.Password, x.Password) && _passwordHasher.Verify(model.Email, x.Email));

            return Mapper.Map<PersonalDataModel>(personalData);
        }
        catch (SaltParseException)
        {
            return null;
        }
    }
}
