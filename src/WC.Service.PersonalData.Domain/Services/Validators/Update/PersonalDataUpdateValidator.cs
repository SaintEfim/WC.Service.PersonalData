using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WC.Library.Domain.Validators;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators.Update;

public sealed class PersonalDataUpdateValidator
    : AbstractValidator<PersonalDataModel>,
        IDomainUpdateValidator
{
    public PersonalDataUpdateValidator(
        IServiceProvider provider)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .SetValidator(provider.GetService<PersonalDataModelValidator>());

        RuleFor(x => x)
            .SetValidator(provider.GetService<PersonalDataUpdateDbValidator>());
    }
}
