using Autofac;
using WC.Library.Web.Startup;
using WC.Service.EmailDomains.gRPC.Server.Services;
using WC.Service.PersonalData.Domain;

namespace WC.Service.EmailDomains.gRPC.Server;

internal sealed class Startup : StartupGrpcBase
{
    public Startup(
        WebApplicationBuilder builder)
        : base(builder)
    {
    }

    public override void ConfigureContainer(
        ContainerBuilder builder)
    {
        base.ConfigureContainer(builder);
        builder.RegisterModule<PersonalDataDomainModule>();
    }

    public override void Configure(
        WebApplication app)
    {
        base.Configure(app);
        app.MapGrpcService<GreeterPersonalDataService>();
    }
}
