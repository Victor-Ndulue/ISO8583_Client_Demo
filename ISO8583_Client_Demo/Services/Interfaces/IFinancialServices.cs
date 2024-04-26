using ISO8583_Client_Demo.Helpers.Dtos;

namespace ISO8583_Client_Demo.Services.Interfaces;

public interface IFinancialServices
{
    Task<string> CreditWorthyCheck(string serverDomainName, int serverPortNumber,AuthRequestDto authRequest);
}
