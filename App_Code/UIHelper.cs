
public static class UIHelper
{
    public static int ProcessPaymentAck(string paymentID, string merchantID, string pwd)
    {

        HCService.PaymentInfoRequest hcAckRequest = new HCService.PaymentInfoRequest();

        hcAckRequest.MerchantID = merchantID;
        hcAckRequest.Password = pwd;
        hcAckRequest.PaymentID = paymentID;

        HCService.HCService hcWS = new HCService.HCService();

        return hcWS.AcknowledgePayment(hcAckRequest);
    }

    public static int ProcessCardInfoAck(string paymentID, string merchantID, string pwd)
    {

        HCService.CardInfoRequest hcAckRequest = new HCService.CardInfoRequest();

        hcAckRequest.MerchantID = merchantID;
        hcAckRequest.Password = pwd;
        hcAckRequest.CardID = paymentID;

        HCService.HCService hcWS = new HCService.HCService();

        return hcWS.AcknowledgeCardInfo(hcAckRequest);
    }

}