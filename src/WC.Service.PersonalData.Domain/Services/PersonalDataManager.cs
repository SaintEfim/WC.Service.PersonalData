using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using WC.Library.Domain.Services;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services;

public class PersonalDataManager
    : DataManagerBase<PersonalDataManager, IPersonalDataRepository, PersonalDataModel, PersonalDataEntity>,
        IPersonalDataManager
{
    public PersonalDataManager(
        IMapper mapper,
        ILogger<PersonalDataManager> logger,
        IPersonalDataRepository repository,
        IEnumerable<IValidator> validators)
        : base(mapper, logger, repository, validators)
    {
    }
}
