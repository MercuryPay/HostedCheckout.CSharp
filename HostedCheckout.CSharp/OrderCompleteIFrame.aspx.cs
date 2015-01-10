using System;

/// <summary>
/// the user will be redirected back to this page after the payment
///     is processed by the HostedCheckout iFrame.  
/// This page is specified in the ProcessCompleteURL 
///     of the InitializePayment method.
/// </summary>
public partial class OrderCompleteIFrame : System.Web.UI.Page
{
    //viewstate constants
    const string PREAUTHRESPONSE = "PreAuthResponse";
    const string AUTHCODE = "AuthCode";
    const string INVOICE = "Invoice";
    const string AMOUNT = "Amount";
    const string REFNO = "RefNo";
    const string ACQREFDATA = "AcqRefData";
    const string TOKEN = "TOKEN";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["ReturnMethod"] == null)
                return;

            string paymentID, returnCode, returnMessage;

            //The demo supports both return methods of post or get.
            //determine the right return method and get the
            //  PaymentID and ReturnCode.
            if (Session["ReturnMethod"].ToString() == "post")
            {
                paymentID = Request.Form["PaymentID"];
                returnCode = Request.Form["ReturnCode"];
                returnMessage = Request.Form["ReturnMessage"];
            }
            else
            {
                paymentID = Request.QueryString["PaymentID"];
                returnCode = Request.QueryString["ReturnCode"];
                returnMessage = "ReturnCode=" + returnCode + "." + " ReturnMethod=GET";
            }

            //get the return message.  (only available in form post)
            this.txtPaymentID.Text = paymentID;
            this.txtReturnCode.Text = returnCode;
            this.txtReturnMessage.Text = returnMessage;
            ViewState["PaymentID"] = paymentID;

            if (!String.IsNullOrEmpty(paymentID))
            {
                VerifyCredit(paymentID);
            }
        }

        Session[Constants.PaymentIDIFrameEComm] = null;

    }

    private void VerifyCredit(string paymentID)
    {
        try
        {
            //now call VerifyPayment to get the information about the payment and the token.
            HCService.PaymentInfoRequest hcVerifyRequest = new HCService.PaymentInfoRequest();

            hcVerifyRequest.MerchantID = Session["MerchantID"].ToString();
            hcVerifyRequest.Password = Session["PW"].ToString();
            hcVerifyRequest.PaymentID = paymentID;

            //Make the request to verify the payment.
            HCService.HCService hcWS = new HCService.HCService();
            HCService.PaymentInfoResponse response = new HCService.PaymentInfoResponse();
            response = hcWS.VerifyPayment(hcVerifyRequest);

            if (response != null)
            {
                if (response.ResponseCode == 0)
                {

                    ViewState[AUTHCODE] = response.AuthCode;
                    ViewState[INVOICE] = response.Invoice;
                    ViewState[REFNO] = response.RefNo;
                    ViewState[ACQREFDATA] = response.AcqRefData;
                    ViewState[AMOUNT] = response.Amount;
                    Session[TOKEN] = response.Token;

                    this.lstReturned.ClearSelection();

                    this.lstReturned.Items.Add("Status: " + response.Status);
                    this.lstReturned.Items.Add("StatusMessage: " + response.StatusMessage);
                    this.lstReturned.Items.Add("DisplayMessage: " + response.DisplayMessage);
                    this.lstReturned.Items.Add("AvsResult: " + response.AvsResult);
                    this.lstReturned.Items.Add("CvvResult: " + response.CvvResult);
                    this.lstReturned.Items.Add("CardType: " + response.CardType);
                    this.lstReturned.Items.Add("AuthCode: " + response.AuthCode);

                    this.lstReturned.Items.Add("RefNo: " + response.RefNo);
                    this.lstReturned.Items.Add("Invoice: " + response.Invoice);
                    this.lstReturned.Items.Add("AcqRefData: " + response.AcqRefData);
                    string amount = String.Format("{0:C}", response.Amount);
                    this.lstReturned.Items.Add("Amount: " + amount);
                    this.lstReturned.Items.Add("MaskAccount: " + response.MaskedAccount);
                    this.lstReturned.Items.Add("Exp Date: " + response.ExpDate);
                    this.lstReturned.Items.Add("TransDateTime: " + response.TransPostTime);
                    this.lstReturned.Items.Add("TranType: " + response.TranType);
                    this.lstReturned.Items.Add("PaymentIDExpired: " + response.PaymentIDExpired.ToString());
                    this.lstReturned.Items.Add("CustomerCode: " + response.CustomerCode);
                    this.lstReturned.Items.Add("TaxAmount: " + response.TaxAmount.ToString());
                    this.lstReturned.Items.Add("Memo: " + response.Memo.ToString());
                    amount = String.Format("{0:C}", response.AuthAmount);
                    this.lstReturned.Items.Add("AuthAmount: " + amount);
                    this.lstReturned.Items.Add("Cardholder Name: " + response.CardholderName);
                    this.lstReturned.Items.Add("OperatorID:" + response.OperatorID);
                    this.lstReturned.Items.Add("Terminal Name:" + response.TerminalName);
                    this.lstReturned.Items.Add("Lane ID:" + response.LaneID);

                    if (!String.IsNullOrEmpty(response.Token))
                        this.lbltoken.Text = response.Token;

                    this.lstReturned.Visible = true;

                    if (response.Status == "Approved")
                        if (response.TranType != "ZeroAuth")
                            this.lblStatus.Text = "Your order has been processed. Your pizza will be ready for pickup in 20 minutes!";
                        else
                            this.lblStatus.Text = "ZeroAuth Approved";
                    else
                    {
                        this.lblStatus.Text = response.DisplayMessage; ;
                    }

                    //call ack 
                    int ack = UIHelper.ProcessPaymentAck(hcVerifyRequest.PaymentID, hcVerifyRequest.MerchantID, hcVerifyRequest.Password);

                    this.lstReturned.Items.Add("Acknowledgement Return Code: " + ack);

                }
                else
                {
                    this.lblStatus.Text = "Verify Payment not successful -- response.ResponseCode = " + response.ResponseCode;
                }
            }
            else
            {
                this.lblStatus.Text = "We were unable to process your payment.";
            }
        }
        catch (Exception ex)
        {
            this.lblStatus.Text = "Exception occurred: " + ex.Message;
        }
    }

}

