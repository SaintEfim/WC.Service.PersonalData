using FluentValidation;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators;

public sealed class PersonalDataModelValidator : AbstractValidator<PersonalDataModel>
{
    public PersonalDataModelValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
