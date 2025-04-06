using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.TestHelper;
using Moq;
using WC.Service.EmailDomains.gRPC.Client.Clients;
using WC.Service.EmailDomains.gRPC.Client.Models.DoesEmailDomainExist;
using WC.Service.PersonalData.Domain.Models;
using WC.Service.PersonalData.Domain.Services.PersonalData.Validators;

namespace WC.Service.PersonalData.Domain.Tests.Services.PersonalData.Validators;

public class PersonalDataCheckEmailDomainValidatorTests
{
    private static PersonalDataCheckEmailDomainValidator GetValidator(
        IMock<IGreeterEmailDomainsClient> client)
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(client.Object);
        builder.RegisterType<PersonalDataCheckEmailDomainValidator>();
        builder.Populate([]);
        var container = builder.Build();
        return container.Resolve<PersonalDataCheckEmailDomainValidator>();
    }

    [Fact]
    public async Task PersonalDataCheckEmailDomain_Positive_ValidDomain()
    {
        var personalData = PersonalData.PersonalDataModel();
        personalData.Email = "user@valid.com";

        var client = new Mock<IGreeterEmailDomainsClient>(MockBehavior.Strict);
        client.Setup(x => x.DoesEmailDomainWithDomainNameExist(
                It.Is<DoesEmailDomainExistRequestModel>(r => r.DomainName == "valid.com"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DoesEmailDomainExistResponseModel { Exists = true })
            .Verifiable();

        var validator = GetValidator(client);
        var result = await validator.TestValidateAsync(personalData);

        result.ShouldNotHaveAnyValidationErrors();

        client.Verify();
    }

    [Fact]
    public async Task PersonalDataCheckEmailDomain_Negative_InvalidDomain()
    {
        var personalData = PersonalData.PersonalDataModel();
        personalData.Email = "user@invalid.com";

        var client = new Mock<IGreeterEmailDomainsClient>(MockBehavior.Strict);
        client.Setup(x => x.DoesEmailDomainWithDomainNameExist(
                It.Is<DoesEmailDomainExistRequestModel>(r => r.DomainName == "invalid.com"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DoesEmailDomainExistResponseModel { Exists = false })
            .Verifiable();

        var validator = GetValidator(client);
        var result = await validator.TestValidateAsync(personalData);

        result.ShouldHaveAnyValidationError()
            .WithErrorMessage("The email domain does not exist.")
            .When(x => x.PropertyName == nameof(PersonalDataModel.Email));

        client.Verify();
    }
}
