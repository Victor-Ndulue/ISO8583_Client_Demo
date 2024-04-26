using ISO8583_Client_Demo.Helpers.Dtos;
using ISO8583_Client_Demo.Helpers.Enums;
using ISO8583_Client_Demo.Helpers.Methods;
using ISO8583_Client_Demo.Services.Interfaces;

namespace ISO8583_Client_Demo.Services.Implementations;

internal sealed class FinancialServices : IFinancialServices
{
    public async Task<string> CreditWorthyCheck(string serverDomainName, int serverPortNumber, AuthRequestDto authRequest)
    {
        var isoMsg = GenStaticMethods.GenerateIsoMessage<AuthRequestDto>(130, MessageType.AuthorizationRequest, authRequest);
        var asciiMsg = GenStaticMethods.GenerateAsciiMsg(isoMsg);
        var socket = await GenStaticMethods.ConnectToServerSocket(serverDomainName, serverPortNumber);
        if (socket is not null)
        {
            var socketResponse = await GenStaticMethods.ServerRequestHandler(socket, asciiMsg, 1024);
            var unpackedResponse = GenStaticMethods.UnpackIsoMsg(socketResponse);
            Console.WriteLine("Echo 'response' = {0}", unpackedResponse);
            if (unpackedResponse[39] == "00")
            {
                string sufFundMsg = "Fund sufficient";
                return sufFundMsg;
            }
            string insufFundMsg = "Insufficient fund";
            return insufFundMsg;
        }
        var serverErrorMsg = "Server busy. Retry";
        return serverErrorMsg;
    }

}
