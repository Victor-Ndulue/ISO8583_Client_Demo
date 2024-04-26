using BIM_ISO8583.NET;
using ISO8583_Client_Demo.Helpers.Dtos;
using ISO8583_Client_Demo.Helpers.Enums;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ISO8583_Client_Demo.Helpers.Methods;

internal class GenStaticMethods
{
    internal static string GenerateIsoMessage<T>(byte n_DataElement, MessageType mTI, T payLoad)
    {
        string[] DE = new string[n_DataElement];
        DateTime currentDateTime = DateTime.UtcNow.AddHours(1);
        string transmissionDateTime = ConvertDateTimeFormat(currentDateTime);
        string sysTraceAuditNum = GenerateSysTraceAuditNum();
        string localTime = GetTimeOnly(currentDateTime);
        string localDate = GetDateOnly(currentDateTime);
        string retrievalRefNumber = GenerateRandomNumber(3, 13);
        ISO8583 iso8583 = new();
        switch (mTI)
        {
            #region Authorization Request Handler
            case MessageType.AuthorizationRequest:
                string MTI = "0100";
                if (payLoad is AuthRequestDto authRequest)
                {
                    string track2Data = (authRequest.AccountNumber + "D" +
                        authRequest.CardExpDate +
                        authRequest.ServiceRestrictionCode);
                    DE[2] = authRequest.AccountNumber;
                    DE[3] = authRequest.ProCode;
                    DE[4] = authRequest.AmountTransaction;
                    DE[7] = transmissionDateTime;
                    DE[11] = sysTraceAuditNum;
                    DE[12] = localTime;
                    DE[13] = localDate;
                    DE[14] = authRequest.CardExpDate;
                    DE[18] = authRequest.MerchantType;
                    DE[22] = authRequest.PosEntryMode;
                    if (authRequest.CardSequenceNumber is not null) DE[23] = authRequest.CardSequenceNumber;
                    DE[25] = authRequest.PosConditionCode;
                    if (authRequest.PosPinCaptureCode is not null) DE[26] = authRequest.PosPinCaptureCode;
                    DE[28] = authRequest.AmountTransactionFee;
                    DE[32] = authRequest.AcqInstIdCode;
                    DE[35] = track2Data;
                    DE[37] = retrievalRefNumber;
                    if (authRequest.ServiceRestrictionCode is not null) DE[40] = authRequest.ServiceRestrictionCode;
                    DE[41] = authRequest.CardAcceptorTerminalId;
                    DE[42] = authRequest.CardAcceptorIdCode;
                    DE[43] = authRequest.CardAcceptorNameLocation;
                    DE[49] = authRequest.CurrencyCodeTransaction;
                    if (authRequest.PinData is not null) DE[52] = authRequest.PinData;
                    if (authRequest.SecRelatedCtrlInfo is not null) DE[53] = authRequest.SecRelatedCtrlInfo;
                    if (authRequest.AdditionalAmounts is not null) DE[54] = authRequest.AdditionalAmounts;
                    if (authRequest.IntCircuitCardSysRelData is not null) DE[55] = authRequest.IntCircuitCardSysRelData;
                    if (authRequest.MsgReasonCode is not null) DE[56] = authRequest.MsgReasonCode;
                    if (authRequest.TransEchoData is not null) DE[59] = authRequest.TransEchoData;
                    if (authRequest.PaymtInfo is not null) DE[60] = authRequest.PaymtInfo;
                    if (authRequest.PrivFieldMgtData is not null) DE[62] = authRequest.PrivFieldMgtData;
                    if (authRequest.AccId1 is not null) DE[102] = authRequest.AccId1;
                    if (authRequest.AccId2 is not null) DE[103] = authRequest.AccId2;
                    DE[123] = authRequest.PosDataCode;
                    if (authRequest.NearFieldCommData is not null) DE[124] = authRequest.NearFieldCommData;
                    if (authRequest.SecMsgHashValue is not null) DE[128] = authRequest.SecMsgHashValue;
                    else DE[64] = authRequest.SecMsgHashValue;
                    return iso8583.Build(DE, MTI);
                }
                else
                {
                    // Handle the case when payLoad is not of type AuthRequestDto
                    // For example, you can throw an exception or return a default value
                    throw new ArgumentException("Payload is not of type AuthRequestDto");
                }
                #endregion
        }
        throw new ArgumentException("Invalid MessageType or Payload type");

    }
    internal static byte[] GenerateAsciiMsg(string isoMsg)
    {
        return Encoding.ASCII.GetBytes($"{isoMsg}<EOF>");
    }
    internal static string[] UnpackIsoMsg(string isoMsg)
    {
        ISO8583 iso8583 = new ISO8583();

        string[] DE;

        DE = iso8583.Parse(isoMsg);

        return DE;
    }
    internal static async Task<Socket> ConnectToServerSocket(string serverDomainName, int serverPortNumber)
    {
        try
        {
            IPAddress serverIpAddress = Dns.GetHostAddresses(serverDomainName)[1];
            IPAddress remoteIpAddress = Dns.GetHostAddresses("localHost")[1];
            IPEndPoint serverEP = new IPEndPoint(serverIpAddress, serverPortNumber);
            Socket socket = new Socket(remoteIpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            await socket.ConnectAsync(serverEP);
            return socket;
        }
        catch (Exception ex)
        {
            //Implement logging for error tracking
            return null;
        }
    }
    internal static async Task<string> ServerRequestHandler(Socket socket, byte[] asciiMsg, int responseByteLength)
    {
        var byteReceptor = new byte[responseByteLength];
        //Check for values of "lengthOfSentBytes" and "lengthOfReceivedBytes" to remove redundant assignment.
        var lengthOfSentBytes = await socket.SendAsync(asciiMsg);
        var lengthOfReceivedBytes = await socket.ReceiveAsync(byteReceptor);
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
        socket.Dispose();
        //Potential replace of "lengthOfReceivedBytes" with "responseByteLength."
        var serverResponse = Encoding.ASCII.GetString(byteReceptor, 0, lengthOfReceivedBytes);
        return serverResponse;
    }
    private static string ConvertDateTimeFormat(DateTime dateTime)
    {
        var month = dateTime.Month;
        var day = dateTime.Day;
        var hr = dateTime.Hour;
        var minute = dateTime.Minute;
        var second = dateTime.Second;
        var stringDateTime = string.Concat(month, day, hr, minute, second);
        return stringDateTime;
    }
    private static string GenerateSysTraceAuditNum()
    {
        string randomNumber = GenerateRandomNumber(2, 7);
        return randomNumber;
    }
    private static string GenerateRandomNumber(byte start, byte end)
    {
        Random random = new();
        var randomNumber = random.Next(start, end);
        return randomNumber.ToString();

    }
    private static string GetTimeOnly(DateTime dateTime)
    {
        var hr = dateTime.Hour;
        var min = dateTime.Minute;
        var sec = dateTime.Second;
        var stringTime = string.Concat(hr, min, sec);
        return stringTime;
    }
    private static string GetDateOnly(DateTime dateTime)
    {
        var month = dateTime.Month;
        var day = dateTime.Day;
        var stringDate = string.Concat(month, day);
        return stringDate;
    }
}
