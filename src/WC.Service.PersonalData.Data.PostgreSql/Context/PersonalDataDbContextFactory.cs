using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WC.Library.Data.PostgreSql.Context;

namespace WC.Service.PersonalData.Data.PostgreSql.Context;

public sealed class PersonalDataDbContextFactory : PostgreSqlDbContextFactoryBase<PersonalDataDbContext>
{
    public PersonalDataDbContextFactory(
        IConfiguration configuration)
        : base(configuration)
    {
    }

    protected override string ConnectionString => "ServiceDB";
}
