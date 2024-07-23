using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WC.Library.Data.Repository;
using WC.Service.PersonalData.Data.Models;

namespace WC.Service.PersonalData.Data.Repositories;

public class PersonalDataRepository<TDbContext>
    : RepositoryBase<PersonalDataRepository<TDbContext>, TDbContext, PersonalDataEntity>,
        IPersonalDataRepository
    where TDbContext : DbContext
{
    protected PersonalDataRepository(
        TDbContext context,
        ILogger<PersonalDataRepository<TDbContext>> logger)
        : base(context, logger)
    {
    }
}
