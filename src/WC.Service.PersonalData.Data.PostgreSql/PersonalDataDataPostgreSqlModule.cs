using Autofac;
using Microsoft.EntityFrameworkCore;
using WC.Library.Data.Repository;
using WC.Service.PersonalData.Data.PostgreSql.Context;

namespace WC.Service.PersonalData.Data.PostgreSql;

public class PersonalDataDataPostgreSqlModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .AsImplementedInterfaces();

        builder.RegisterType<PersonalDataDbContextFactory>()
            .AsSelf()
            .SingleInstance();

        builder.Register(c => c.Resolve<PersonalDataDbContextFactory>()
                .CreateDbContext())
            .As<PersonalDataDbContext>()
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}
