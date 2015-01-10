using System;

/// <summary>
/// the user will be redirected back to this page after the payment
///     is processed by the HostedCheckout POS iFrame.  
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
            string paymentID, returnCode, returnMessage;

            if (Session["ReturnMethod"] == null)
                return;

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
            
            this.txtPaymentID.Text = paymentID;
            this.txtReturnCode.Text = returnCode;
            //get the return message.  (only available in form post)
            this.txtReturnMessage.Text = returnMessage;
            ViewState["PaymentID"] = paymentID;
          
            if (returnCode == "0") // Only call verification if form post indicates success.
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

                        this.lstPreAuthReturned.ClearSelection();

                        this.lstPreAuthReturned.Items.Add("Status: " + response.Status);
                        this.lstPreAuthReturned.Items.Add("StatusMessage: " + response.StatusMessage);
                        this.lstPreAuthReturned.Items.Add("DisplayMessage: " + response.DisplayMessage);
                        this.lstPreAuthReturned.Items.Add("AvsResult: " + response.AvsResult);
                        this.lstPreAuthReturned.Items.Add("CvvResult: " + response.CvvResult);
                        this.lstPreAuthReturned.Items.Add("CardType: " + response.CardType);
                        this.lstPreAuthReturned.Items.Add("AuthCode: " + response.AuthCode);

                        this.lstPreAuthReturned.Items.Add("RefNo: " + response.RefNo);
                        this.lstPreAuthReturned.Items.Add("Invoice: " + response.Invoice);
                        this.lstPreAuthReturned.Items.Add("AcqRefData: " + response.AcqRefData);
                        string amount = String.Format("{0:C}", response.Amount);
                        this.lstPreAuthReturned.Items.Add("Amount: " + amount);
                        amount = String.Format("{0:C}", response.AuthAmount);
                        this.lstPreAuthReturned.Items.Add("AuthAmount: " + amount);
                        this.lstPreAuthReturned.Items.Add("MaskAccount: " + response.MaskedAccount);
                        this.lstPreAuthReturned.Items.Add("Exp Date: " + response.ExpDate);
                        this.lstPreAuthReturned.Items.Add("TransDateTime: " + response.TransPostTime);
                        this.lstPreAuthReturned.Items.Add("TranType: " + response.TranType);
                        this.lstPreAuthReturned.Items.Add("PaymentIDExpired: " + response.PaymentIDExpired.ToString());
                        this.lstPreAuthReturned.Items.Add("CustomerCode: " + response.CustomerCode);
                        this.lstPreAuthReturned.Items.Add("TaxAmount: " + response.TaxAmount.ToString());
                        this.lstPreAuthReturned.Items.Add("Memo: " + response.Memo.ToString());
                        this.lstPreAuthReturned.Items.Add("Cardholder Name: " + response.CardholderName);
                        this.lstPreAuthReturned.Items.Add("OperatorID:" + response.OperatorID);
                        this.lstPreAuthReturned.Items.Add("Terminal Name:" + response.TerminalName);
                        this.lstPreAuthReturned.Items.Add("Lane ID: " + response.LaneID);

                        if (!String.IsNullOrEmpty(response.Token))
                            this.lbltoken.Text = response.Token;

                        this.lstPreAuthReturned.Visible = true;
                        
                        if (response.Status == "Approved")
                            if (response.TranType != "ZeroAuth")
                                this.lblStatus.Text = "Your order has been processed. Press Next to continue...";
                            else
                                this.lblStatus.Text = "ZeroAuth Approved. Press Next to continue....";
                        else
                        {
                            this.lblStatus.Text = response.DisplayMessage; ;
                        }

                        //call ack whether successful or not.
                        int ack = UIHelper.ProcessPaymentAck(hcVerifyRequest.PaymentID, hcVerifyRequest.MerchantID, hcVerifyRequest.Password);

                        this.lstPreAuthReturned.Items.Add("Acknowledgement called. Return Code: " + ack);

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
            else
            {
                this.lblStatus.Text = "There was a problem processing your order.";
                this.lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}