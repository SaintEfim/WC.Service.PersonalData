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
