using FluentValidation;
using WC.Service.EmailDomains.gRPC.Client.Clients;
using WC.Service.EmailDomains.gRPC.Client.Models.DoesEmailDomainExist;
using WC.Service.PersonalData.Domain.Models;

namespace WC.Service.PersonalData.Domain.Services.Validators;

public sealed class PersonalDataCheckEmailDomainValidator : AbstractValidator<PersonalDataModel>
{
    public PersonalDataCheckEmailDomainValidator(
        IGreeterEmailDomainsClient emailDomainsClient)
    {
        RuleFor(x => x.Email)
            .CustomAsync(async (
                email,
                context,
                cancellationToken) =>
            {
                var domain = email.Split('@')[1];

                var domainResponse = await emailDomainsClient.DoesEmailDomainWithDomainNameExist(
                    new DoesEmailDomainExistRequestModel { DomainName = domain }, cancellationToken);

                if (!domainResponse.Exists)
                {
                    context.AddFailure(nameof(PersonalDataModel.Email), "The email domain does not exist.");
                }
            });
    }
}
