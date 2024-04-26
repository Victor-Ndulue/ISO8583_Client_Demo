using ISO8583_Client_Demo.Extensions;
using ISO8583_Client_Demo.Helpers.Methods;
using ISO8583_Client_Demo.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("POS Terminal Running...");
//Resolving the services
var serviceCollection = new ServiceCollection();
ServiceExtension.AddFinanceService(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();
var finServices = serviceProvider.GetService<IFinancialServices>();

char cont;
do
{
    DateTime dateTimeStarted = DateTime.UtcNow.AddHours(1);
    DateTime dateTimeEnd = dateTimeStarted.AddSeconds(60);
    dateTimeStarted = DateTime.UtcNow.AddHours(1);
    var timeOutMsg = "Service timeout due to delay";
    if (dateTimeEnd < dateTimeStarted)
    {
        Console.WriteLine(timeOutMsg);
        break;
    }
    else
    {
        var welcomeMessage = "Select Transaction option\n" +
            "Enter \n" +
            "'1' to Check Credit Worth\n" +
            "'2' to Check Balance\n" +
            "Any other key to terminate";
        string goodByeMsg = "Thank you for choosing us. Cheers!!!";
        Console.WriteLine(welcomeMessage);
        var selection = Console.ReadLine();
        switch (selection)
        {
            case "1":
                var posInput = AuthPayloadInputReader.ReadUserInput();
                var serverDomain = posInput.serverDomain;
                int serverPort = posInput.serverPort;
                var authRequest = posInput.authRequestPayload;
                var response = await finServices.CreditWorthyCheck(serverDomain, serverPort, authRequest);
                Console.WriteLine(response + "\n" + goodByeMsg);
                break;
            default:
                Console.WriteLine(goodByeMsg);
                break;
        }
        Console.WriteLine("Enter 'y' to continue or any other key to exit.");
        cont = Console.ReadLine().FirstOrDefault();
    }
}
while (cont is 'y');
