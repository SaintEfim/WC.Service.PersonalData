using WC.Service.Employees.gRPC.Client;

namespace WC.Service.EmailDomains.gRPC.Server;

public class EmployeesClientConfiguration : IEmployeesClientConfiguration
{
    private readonly Lazy<string> _baseUrl;

    public EmployeesClientConfiguration(
        IConfiguration config)
    {
        _baseUrl = new Lazy<string>(() => config.GetValue<string>("EmployeeService:Url") ??
                                          throw new InvalidOperationException(
                                              "Employees REST API service URL must be specified"));
    }

    public string GetBaseUrl()
    {
        return _baseUrl.Value;
    }
}
