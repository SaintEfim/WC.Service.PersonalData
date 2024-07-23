using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WC.Library.Data.PostgreSql.Context;

namespace WC.Service.PersonalData.Data.PostgreSql.Context;

public sealed class PersonalDataDbContextFactory : PostgreSqlDbContextFactoryBase<PersonalDataDbContext>
{
    public PersonalDataDbContextFactory(
        IConfiguration configuration,
        IHostEnvironment environment)
        : base(configuration, environment)
    {
    }

    protected override string ConnectionString => "ServiceDB";
}
