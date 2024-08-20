using Autofac;
using WC.Library.Data;

namespace WC.Service.PersonalData.Data;

public class PersonalDataServiceDataModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterModule<WcLibraryDataModule>();
    }
}
