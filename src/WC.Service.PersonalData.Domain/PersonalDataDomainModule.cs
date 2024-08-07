using Autofac;
using FluentValidation;
using WC.Library.BCryptPasswordHash;
using WC.Library.Domain.Services;
using WC.Service.EmailDomains.gRPC.Client;
using WC.Service.PersonalData.Data.PostgreSql;

namespace WC.Service.PersonalData.Domain;

public class PersonalDataDomainModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterModule<EmailDomainsClientModule>();
        builder.RegisterModule<PersonalDataDataPostgreSqlModule>();
        builder.RegisterModule<WcLibraryBCryptPasswordHasher>();

        builder.RegisterType<EmailDomainsClientConfiguration>()
            .As<IEmailDomainsClientConfiguration>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IDataProvider<>))
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IDataManager<>))
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IValidator<>))
            .AsImplementedInterfaces();
    }
}
