using ISO8583_Client_Demo.Helpers.Dtos;

namespace ISO8583_Client_Demo.Helpers.Methods;

public class AuthPayloadInputReader
{
    public static Response ReadUserInput()
    {
        Console.Write("Kindly enter server Domain Name. Example: 'localhost'");
        var serverDomain = Console.ReadLine();
        Console.Write("Kindly enter server port number. Example '8080'");
        var serverPort = int.Parse(Console.ReadLine());
        Console.Write("Kindly enter primary account number. Example '0123456789'");
        var accNumber = Console.ReadLine();
        Console.Write("Kindly enter processing code. Example '311000'");
        var proCode = Console.ReadLine();
        Console.Write("Kindly enter transaction amount. Example '100000'");
        var amountTrans = (int.Parse(Console.ReadLine()) * 100).ToString();
        Console.Write("Kindly enter card expiry date. Example '24/04' in the format YYMM");
        var cardExpDate = Console.ReadLine(); /**/
        Console.Write("Kindly enter merchant type code. Example '6011'");
        var merchantType = Console.ReadLine();
        Console.Write("Kindly enter pos entry mode. Example '011'");
        var posEntryMode = Console.ReadLine();
        Console.Write("Kindly enter card sequence number. Example '777'");
        var cardSeqNumber = Console.ReadLine();
        Console.Write("Kindly enter pos condition code. Example '00'");
        var posConditionCode = Console.ReadLine();
        Console.Write("Kindly enter pos pin capture code. Example '12'");
        var posPinCaptureCode = Console.ReadLine();
        Console.Write("Kindly enter amount transaction fee. Example 'D1000'");
        var amtTransFee = Console.ReadLine();
        Console.Write("Kindly enter acquiring institute id code. Example '123b567890'");
        var acqInstIdCode = Console.ReadLine();
        Console.Write("Kindly enter service restriction code. Example '907'");
        var servRestrictionCode = Console.ReadLine();
        Console.Write("Kindly enter card acceptor terminal Id. Example '234567891Q'");
        var cardAcceptorTermId = Console.ReadLine();
        Console.Write("Kindly enter card acceptor Id code. Example '0012345678@$'");
        var cardAcceptorIdCode = Console.ReadLine();
        Console.Write("Kindly enter card acceptor location. Example '34 Joseph Lambo Street, Lagos State ,L, N,'");
        var cardAcptorNameLocation = Console.ReadLine();
        Console.Write("Kindly enter transaction currency code. Example '566'");
        var currencyCodeTrans = Console.ReadLine();
        Console.Write("Kindly enter card pin. Example '1234'");
        var pinData = Console.ReadLine();
        Console.Write("Kindly enter security related control information. Sorry. Example not available");
        var secRelatedCtrlInfo = Console.ReadLine();
        Console.Write("Kindly enter additional amounts. Sorry. You may have to figure it out yourself");
        var addAmnts = Console.ReadLine();
        Console.Write("Kindly enter integrated circuit card system related data. Sorry. Example unavailable");
        var intCirCardSysRelData = Console.ReadLine();
        Console.Write("Kindly enter message reason code. Sorry. Example missing.");
        var msgReasonCode = Console.ReadLine();
        Console.Write("Kindly enter trans echo. Example 'Echo test'");
        var transEchoData = Console.ReadLine();
        Console.Write("Kindly enter pos data code. Example '010101114004101'");
        var posDataCode = Console.ReadLine();
        AuthRequestDto authRequestPayload = new AuthRequestDto
        {
            AccountNumber = accNumber,
            ProCode = proCode,
            AmountTransaction = amountTrans,
            CardExpDate = cardExpDate,
            MerchantType = merchantType,
            PosEntryMode = posEntryMode,
            CardSequenceNumber = cardSeqNumber,
            PosConditionCode = posConditionCode,
            PosPinCaptureCode = posPinCaptureCode,
            AmountTransactionFee = amtTransFee,
            AcqInstIdCode = acqInstIdCode,
            ServiceRestrictionCode = servRestrictionCode,
            CardAcceptorTerminalId = cardAcceptorTermId,
            CardAcceptorIdCode = cardAcceptorIdCode,
            CardAcceptorNameLocation = cardAcptorNameLocation,
            CurrencyCodeTransaction = currencyCodeTrans,
            PinData = pinData,
            SecRelatedCtrlInfo = secRelatedCtrlInfo,
            AdditionalAmounts = addAmnts,
            IntCircuitCardSysRelData = intCirCardSysRelData,
            MsgReasonCode = msgReasonCode,
            TransEchoData = transEchoData,
            PaymtInfo = null,
            PrivFieldMgtData = null,
            AccId1 = null,
            AccId2 = null,
            PosDataCode = posDataCode,
            NearFieldCommData = null,
            SecMsgHashValue = null,
        };
        return new Response(serverDomain, serverPort, authRequestPayload);
    }
    public record Response(string serverDomain, int serverPort, AuthRequestDto authRequestPayload);
}

#region
//var authRequest = new AuthRequestDto
//{
//    AccountNumber = "0123456789",
//    ProCode = "311000",
//    AmountTransaction = "100000",
//    CardExpDate = "2408",
//    MerchantType = "6011",
//    PosEntryMode = "011",
//    //CardSequenceNumber = "777",
//    PosConditionCode = "00",
//    PosPinCaptureCode = "12",
//    AmountTransactionFee = "D1000",
//    AcqInstIdCode = "123b567890",
//    //ServiceRestrictionCode ="907",
//    CardAcceptorTerminalId = "234567891Q#4",
//    CardAcceptorIdCode = "0012345678@$",
//    CardAcceptorNameLocation = "34 Joseph Lambo Street, Lagos State ,L, N,",
//    CurrencyCodeTransaction = "566",
//    //PinData = "",
//    //SecRelatedCtrlInfo = "",
//    //AdditionalAmounts = "",
//    //IntCircuitCardSysRelData = "",
//    //MsgReasonCode ="",
//    TransEchoData = "Echo",
//    //PaymtInfo = "",
//    //PrivFieldMgtData ="",
//    //AccId1 ="",
//    //AccId2 ="",
//    PosDataCode = "010101114004101",
//    //NearFieldCommData ="",
//    SecMsgHashValue = ""
//};
#endregion