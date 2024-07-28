using FluentValidation;
using WC.Service.PersonalData.Data.Repositories;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators.Create;

public sealed class PersonalDataCreateDbValidator : AbstractValidator<PersonalDataModel>
{
    public PersonalDataCreateDbValidator(
        IPersonalDataRepository personalDataRepository)
    {
        RuleFor(x => x.Email)
            .CustomAsync(async (
                email,
                context,
                cancellationToken) =>
            {
                var existingPersonalData = await personalDataRepository.Get(cancellationToken: cancellationToken);

                if (existingPersonalData.Any(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                {
                    context.AddFailure(nameof(PersonalDataModel.Email), "The email already exists.");
                }
            });
    }
}
