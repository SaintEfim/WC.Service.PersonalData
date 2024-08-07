using Microsoft.Extensions.Configuration;
using WC.Service.EmailDomains.gRPC.Client;

namespace WC.Service.PersonalData.Domain;

public class EmailDomainsClientConfiguration : IEmailDomainsClientConfiguration
{
    private readonly Lazy<string> _baseUrl;

    public EmailDomainsClientConfiguration(
        IConfiguration config)
    {
        _baseUrl = new Lazy<string>(() => config.GetValue<string>("Services:email-domains") ??
                                          throw new InvalidOperationException(
                                              "Email Domains REST API service URL must be specified"));
    }

    public string GetBaseUrl()
    {
        return _baseUrl.Value;
    }
}
