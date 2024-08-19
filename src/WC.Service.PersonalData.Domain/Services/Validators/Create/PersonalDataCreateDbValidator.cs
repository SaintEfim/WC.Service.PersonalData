using FluentValidation;
using WC.Library.BCryptPasswordHash;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators.Create;

public sealed class PersonalDataCreateDbValidator : AbstractValidator<PersonalDataModel>
{
    public PersonalDataCreateDbValidator(
        IBCryptPasswordHasher passwordHasher,
        IPersonalDataRepository personalDataRepository)
    {
        RuleFor(x => x.EmployeeId)
            .CustomAsync(async (
                employeeId,
                context,
                cancellationToken) =>
            {
                var existingPersonalData = await personalDataRepository.Get(cancellationToken: cancellationToken);

                if (existingPersonalData.Any(x => x.EmployeeId == employeeId))
                {
                    context.AddFailure(nameof(PersonalDataModel.EmployeeId),
                        "Personal data is already linked to the employee.");
                }
            });

        RuleFor(x => x.Email)
            .CustomAsync(async (
                email,
                context,
                cancellationToken) =>
            {
                var existingPersonalData = await personalDataRepository.Get(cancellationToken: cancellationToken);

                if (existingPersonalData.Any(x => passwordHasher.Verify(email, x.Email)))
                {
                    context.AddFailure(nameof(PersonalDataModel.Email), "The email already exists.");
                }
            });
    }
}
