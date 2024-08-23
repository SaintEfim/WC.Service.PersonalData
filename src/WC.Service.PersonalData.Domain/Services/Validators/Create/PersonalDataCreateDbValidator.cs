using FluentValidation;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators.Create;

public sealed class PersonalDataCreateDbValidator : AbstractValidator<PersonalDataModel>
{
    public PersonalDataCreateDbValidator(
        IPersonalDataRepository personalDataRepository)
    {
        RuleFor(x => x)
            .CustomAsync(async (
                personalData,
                context,
                cancellationToken) =>
            {
                var existingPersonalData =
                    (await personalDataRepository.Get(cancellationToken: cancellationToken)).ToList();

                if (existingPersonalData.Any(x =>
                        personalData.Email.Equals(x.Email, StringComparison.CurrentCultureIgnoreCase)))
                {
                    context.AddFailure(nameof(PersonalDataModel.Email),
                        "An employee with this email is already registered.");
                }

                if (existingPersonalData.Any(x =>
                        personalData.Email.Equals(x.Email, StringComparison.CurrentCultureIgnoreCase)))
                {
                    context.AddFailure(nameof(PersonalDataModel.Email),
                        "An employee with this email is already registered.");
                }
            });
    }
}
