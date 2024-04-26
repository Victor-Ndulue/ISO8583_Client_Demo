using ISO8583_Client_Demo.Services.Implementations;
using ISO8583_Client_Demo.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ISO8583_Client_Demo.Extensions;

public static class ServiceExtension
{
    public static void AddFinanceService(this ServiceCollection services)
    {
        services.AddTransient<IFinancialServices, FinancialServices>();
    }
}
