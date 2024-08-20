using FluentValidation;
using WC.Library.Employee.Shared.Validators;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators;

public sealed class PersonalDataModelValidator : AbstractValidator<PersonalDataModel>
{
    public PersonalDataModelValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotNull()
            .SetValidator(new EmailValidator(nameof(PersonalDataModel.Email)));

        RuleFor(x => x.Password)
            .NotNull()
            .SetValidator(new PasswordValidator(nameof(PersonalDataModel.Password)));
    }
}
