using Autofac;
using WC.Service.PersonalData.gRPC.Client.Clients;

namespace WC.Service.PersonalData.gRPC.Client;

public class PersonalDataClientModule : Module
{
    protected override void Load(
        ContainerBuilder builder)
    {
        builder.RegisterType<GreeterPersonalDataClient>()
            .As<IGreeterPersonalDataClient>()
            .InstancePerLifetimeScope();
    }
}
