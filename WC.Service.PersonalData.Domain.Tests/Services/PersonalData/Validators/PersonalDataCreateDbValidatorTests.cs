using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.TestHelper;
using Moq;
using WC.Library.Data.Services;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;
using WC.Service.PersonalData.Domain.Services.PersonalData.Validators.Create;

namespace WC.Service.PersonalData.Domain.Tests.Services.PersonalData.Validators;

public class PersonalDataCreateDbValidatorTests
{
    private static PersonalDataCreateDbValidator GetValidator(
        IMock<IPersonalDataRepository> repository)
    {
        var builder = new ContainerBuilder();

        builder.RegisterInstance(repository.Object);

        builder.RegisterType<PersonalDataCreateDbValidator>();

        builder.Populate([]);

        var container = builder.Build();
        return container.Resolve<PersonalDataCreateDbValidator>();
    }

    [Fact]
    public async Task PersonalData_Positive_Create_New_Record()
    {
        var personalData = PersonalData.PersonalDataModel();

        var repository = new Mock<IPersonalDataRepository>(MockBehavior.Strict);
        repository.Setup(x => x.Get(default,
                default,
                It.IsAny<IWcTransaction>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<PersonalDataEntity>())
            .Verifiable();

        var validator = GetValidator(repository);

        var result = await validator.TestValidateAsync(personalData);

        result.ShouldNotHaveAnyValidationErrors();

        repository.Verify();
    }

    [Fact]
    public async Task PersonalData_Negative_Create_New_Record_Has_Duplicate()
    {
        var personalData = PersonalData.PersonalDataModel();
        var personalDataEntity = PersonalData.PersonalDataEntity();

        var repository = new Mock<IPersonalDataRepository>(MockBehavior.Strict);
        repository.Setup(x => x.Get(default,
                default,
                It.IsAny<IWcTransaction>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<PersonalDataEntity> { personalDataEntity })
            .Verifiable();

        var validator = GetValidator(repository);

        var result = await validator.TestValidateAsync(personalData);

        result.ShouldHaveAnyValidationError()
            .WithErrorMessage("An employee with this email is already registered.")
            .When(x => x.PropertyName == nameof(PersonalDataModel.Email));

        repository.Verify();
    }
}
