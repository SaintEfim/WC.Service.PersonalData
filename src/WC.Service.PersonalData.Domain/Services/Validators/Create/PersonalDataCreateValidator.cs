using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WC.Library.Domain.Validators;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators.Create;

public sealed class PersonalDataCreateValidator
    : AbstractValidator<PersonalDataModel>,
        IDomainCreateValidator
{
    public PersonalDataCreateValidator(
        IServiceProvider provider)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .SetValidator(provider.GetService<PersonalDataModelValidator>());

        RuleFor(x => x)
            .SetValidator(provider.GetService<PersonalDataCreateDbValidator>());
    }
}
