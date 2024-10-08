﻿using Autofac;
using WC.Service.PersonalData.API.gRPC.Services;
using WC.Service.PersonalData.Domain;
using StartupBase = WC.Library.Web.Startup.StartupBase;

namespace WC.Service.PersonalData.API;

internal sealed class Startup : StartupBase
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
