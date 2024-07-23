using AutoMapper;
using Microsoft.Extensions.Logging;
using WC.Library.Domain.Services;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services;

public class PersonalDataProvider
    : DataProviderBase<PersonalDataProvider, IPersonalDataRepository, PersonalDataModel, PersonalDataEntity>,
        IPersonalDataProvider
{
    public PersonalDataProvider(
        IMapper mapper,
        ILogger<PersonalDataProvider> logger,
        IPersonalDataRepository repository)
        : base(mapper, logger, repository)
    {
    }
}
