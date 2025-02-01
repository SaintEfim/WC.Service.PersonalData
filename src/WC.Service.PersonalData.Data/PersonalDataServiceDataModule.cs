using Autofac;
using Sieve.Services;
using WC.Library.Data;
using WC.Service.PersonalData.Data.Profile;

namespace WC.Service.PersonalData.Data;

public class PersonalDataServiceDataModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterModule<WcLibraryDataModule>();

        builder.RegisterType<PersonalDataEntityFilterProfile>()
            .As<ISieveProcessor>()
            .InstancePerLifetimeScope();
    }
}
