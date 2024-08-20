using FluentValidation;
using WC.Library.BCryptPasswordHash;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators.Update;

public sealed class PersonalDataUpdateDbValidator : AbstractValidator<PersonalDataModel>
{
    public PersonalDataUpdateDbValidator(
        IBCryptPasswordHasher passwordHasher,
        IPersonalDataRepository personalDataRepository)
    {
        RuleFor(x => x)
            .CustomAsync(async (
                model,
                context,
                cancellationToken) =>
            {
                var existingPersonalData = (await personalDataRepository.Get(cancellationToken: cancellationToken))
                    .Where(x => x.EmployeeId == model.EmployeeId || passwordHasher.Verify(model.Email, x.Email))
                    .ToList();

                if (existingPersonalData.Any(x => passwordHasher.Verify(model.Email, x.Email)))
                {
                    context.AddFailure(nameof(PersonalDataModel.Email),
                        "An employee with this email is already registered.");
                }
            });
    }
}
