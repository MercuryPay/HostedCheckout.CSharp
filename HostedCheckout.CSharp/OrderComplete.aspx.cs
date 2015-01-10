using System;

/// <summary>
/// the user will be redirected back to this page after the payment
///   is processed by HostedCheckout.
/// This page is specified in the ProcessCompleteURL 
///     of the InitializePayment method.
/// </summary>
public partial class OrderComplete : System.Web.UI.Page
{
    //viewstate constants
    const string PREAUTHRESPONSE = "PreAuthResponse";
    const string AUTHCODE = "AuthCode";
    const string INVOICE = "Invoice";
    const string AMOUNT = "Amount";
    const string TAX_AMOUNT = "Tax_Amount";
    const string REFNO = "RefNo";
    const string ACQREFDATA = "AcqRefData";
    const string TOKEN = "TOKEN";
    const string FREQUENCY = "FREQUENCY";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.divCapturReturnVals.Visible = false;

            if (Session["ReturnMethod"] == null)
                return;

            string paymentID, returnCode;

            //The demo supports both return methods of post or get.
            //determine the right return method and get the
            //  PaymentID and ReturnCode.
            if (Session["ReturnMethod"].ToString() == "post")
            {
                paymentID = Request.Form["PaymentID"];
                returnCode = Request.Form["ReturnCode"];
            }
            else
            {
                paymentID = Request.QueryString["PaymentID"];
                returnCode = Request.QueryString["ReturnCode"];
            }

            //get the return message.  (only available in form post)
            string returnMessage = Request.Form["ReturnMessage"];
            this.txtPaymentID.Text = paymentID;
            this.txtReturnCode.Text = returnCode;
            this.txtReturnMessage.Text = returnMessage;
            ViewState["PaymentID"] = paymentID;

            if (!String.IsNullOrEmpty(paymentID))
            {
                this.btnPreAuthCapture.Visible = true;
                VerifyCredit(paymentID);
            }
        }
    }

    /// <summary>
    /// Now that the payment is processed, provide a PreAuthCapture button just for demo purposes.
    /// It depicts how to process a Capture using the token after a PreAuth.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPreAuthCapture_Click(object sender, EventArgs e)
    {

        //get a reference to the TWS web service
        TransactionService.TransactionService transaction = new TransactionService.TransactionService();

        //create the request
        TransactionService.CreditPreAuthCaptureToken preAuthCaptureRequest = new TransactionService.CreditPreAuthCaptureToken()
        {
            Token = Session[TOKEN].ToString(),
            Frequency = Session[FREQUENCY].ToString(),
            PurchaseAmount = Convert.ToDouble(ViewState[AMOUNT]),
            MerchantID = Session["MerchantID"].ToString(),
            Invoice = ViewState[INVOICE].ToString(),
            TerminalName = "",
            OperatorID = System.Web.Configuration.WebConfigurationManager.AppSettings["OperatorID"].ToString(),
            AcqRefData = ViewState[ACQREFDATA].ToString(),
            AuthCode = ViewState[AUTHCODE].ToString(),
            AuthorizeAmount = Convert.ToDouble(ViewState[AMOUNT]),
            GratuityAmount = 0,
            TaxAmount = Convert.ToDouble(ViewState[TAX_AMOUNT]),
            Memo = "PreAuthCaptureToken from Shopping Cart",
            CustomerCode = "ABC",            
        };

        string password = Session["PW"].ToString();

        //call the capture method with the token
        TransactionService.CreditResponse response = transaction.CreditPreAuthCaptureToken(preAuthCaptureRequest, password);

        if (response != null)
        {
            //display results on page.
            this.lstCaptureReturned.Items.Clear();

            this.lstCaptureReturned.Items.Add("Status: " + response.Status);
            this.lstCaptureReturned.Items.Add("Message: " + response.Message);
            this.lstCaptureReturned.Items.Add("AVSResult: " + response.AVSResult);
            this.lstCaptureReturned.Items.Add("CVVResult: " + response.CVVResult);
            this.lstCaptureReturned.Items.Add("AuthCode: " + response.AuthCode);
            this.lstCaptureReturned.Items.Add("BatchNo: " + response.BatchNo);
            this.lstCaptureReturned.Items.Add("RefNo: " + response.RefNo);
            this.lstCaptureReturned.Items.Add("Invoice: " + response.Invoice);
            this.lstCaptureReturned.Items.Add("AcqRefData: " + response.AcqRefData);
            this.lstCaptureReturned.Items.Add("CardType: " + response.CardType);
            

            this.divCapturReturnVals.Visible = true;

            if (response.Status == "Approved")
                this.lblStatus.Text = "The card has been charged";
            else
            {
                this.lblStatus.Text = "The status of the capture is: " + response.Status;
            }
        }
        else
        {
            this.lblStatus.Text = "The payment was not captured";
        }
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
                    ViewState[TAX_AMOUNT] = response.TaxAmount;
                    Session[TOKEN] = response.Token;

                    //display the return information on the page for demo purposes.
                    this.lstVerifyFields.ClearSelection();

                    this.lstVerifyFields.Items.Add("Status: " + response.Status);
                    this.lstVerifyFields.Items.Add("StatusMessage: " + response.StatusMessage);
                    this.lstVerifyFields.Items.Add("DisplayMessage: " + response.DisplayMessage);
                    this.lstVerifyFields.Items.Add("AvsResult: " + response.AvsResult);
                    this.lstVerifyFields.Items.Add("CvvResult: " + response.CvvResult);
                    this.lstVerifyFields.Items.Add("CardType: " + response.CardType);
                    this.lstVerifyFields.Items.Add("AuthCode: " + response.AuthCode);

                    this.lstVerifyFields.Items.Add("RefNo: " + response.RefNo);
                    this.lstVerifyFields.Items.Add("Invoice: " + response.Invoice);
                    this.lstVerifyFields.Items.Add("AcqRefData: " + response.AcqRefData);
                    string amount = String.Format("{0:C}", response.Amount);
                    this.lstVerifyFields.Items.Add("Amount: " + amount);
                    this.lstVerifyFields.Items.Add("MaskAccount: " + response.MaskedAccount);
                    this.lstVerifyFields.Items.Add("Exp Date: " + response.ExpDate);
                    this.lstVerifyFields.Items.Add("TransDateTime: " + response.TransPostTime);
                    this.lstVerifyFields.Items.Add("TranType: " + response.TranType);
                    this.lstVerifyFields.Items.Add("PaymentIDExpired: " + response.PaymentIDExpired.ToString());
                    this.lstVerifyFields.Items.Add("CustomerCode: " + response.CustomerCode);
                    amount = String.Format("{0:C}", response.TaxAmount);
                    this.lstVerifyFields.Items.Add("TaxAmount: " + amount);
                    this.lstVerifyFields.Items.Add("Memo: " + response.Memo);
                    this.lstVerifyFields.Items.Add("VoiceAuthCode: " + response.VoiceAuthCode);
                    amount = String.Format("{0:C}", response.AuthAmount);
                    this.lstVerifyFields.Items.Add("AuthAmount: " + amount);
                    this.lstVerifyFields.Items.Add("ProcessData: " + response.ProcessData);
                    this.lstVerifyFields.Items.Add("Cardholder Name: " + response.CardholderName);
                    this.lstVerifyFields.Items.Add("OperatorID:" + response.OperatorID);
                    this.lstVerifyFields.Items.Add("Terminal Name:" + response.TerminalName);
                    this.lstVerifyFields.Items.Add("LaneID:" + response.LaneID);

                    //get the token
                    if (!String.IsNullOrEmpty(response.Token))
                        this.lbltoken.Text = response.Token;

                    this.lstVerifyFields.Visible = true;

                    if (response.Status == "Approved")
                        if (response.TranType != "ZeroAuth")
                            this.lblStatus.Text = "Your order has been processed. Your pizza will be ready for pickup in 20 minutes!";
                        else
                            this.lblStatus.Text = "ZeroAuth Approved";
                    else
                    {
                        this.lblStatus.Text = response.DisplayMessage;
                    }
                }
                else
                {
                    this.lblStatus.Text = "Verify Payment not successful -- response.ResponseCode = " + response.ResponseCode;
                }

                //call ack if the demo says to do so.  It's required but this flag is for QA testing purposes only.
                if ((Session["CallAck"] != null) && (Session["CallAck"].ToString() == "True"))
                {
                    //call ack whether successful or not.
                    int ack = UIHelper.ProcessPaymentAck(hcVerifyRequest.PaymentID, hcVerifyRequest.MerchantID, hcVerifyRequest.Password);

                    this.lstVerifyFields.Items.Add("Acknowledgement called. Return Code: " + ack);
                }
            }
            else
            {
                this.lblStatus.Text = "We were unable to process your payment.  Response from VerifyPayment was null.";
            }
        }
        catch (Exception ex)
        {
            this.lblStatus.Text = "Exception occurred: " + ex.Message;
        }
    }

}
