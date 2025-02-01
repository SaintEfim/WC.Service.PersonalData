using Microsoft.Extensions.Logging;
using Sieve.Services;
using WC.Service.PersonalData.Data.PostgreSql.Context;
using WC.Service.PersonalData.Data.Repositories;

namespace WC.Service.PersonalData.Data.PostgreSql.Repositories;

public class PersonalDataRepository : PersonalDataRepository<PersonalDataDbContext>
{
    public PersonalDataRepository(
        PersonalDataDbContext context,
        ILogger<PersonalDataRepository> logger,
        ISieveProcessor serviceProvider)
        : base(context, logger, serviceProvider)
    {
    }
}
