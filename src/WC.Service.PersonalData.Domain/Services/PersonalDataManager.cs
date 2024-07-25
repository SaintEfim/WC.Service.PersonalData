using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using WC.Library.BCryptPasswordHash;
using WC.Library.Domain.Services;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services;

public class PersonalDataManager
    : DataManagerBase<PersonalDataManager, IPersonalDataRepository, PersonalDataModel, PersonalDataEntity>,
        IPersonalDataManager
{
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
    }

    protected override async Task<PersonalDataModel> CreateAction(
        PersonalDataModel model,
        CancellationToken cancellationToken = default)
    {
        model.Email = _passwordHasher.Hash(model.Email);
        model.Password = _passwordHasher.Hash(model.Password);

        return await base.CreateAction(model, cancellationToken);
    }
}
