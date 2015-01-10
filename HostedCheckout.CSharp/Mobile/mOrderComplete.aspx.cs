using System;

public partial class mOrderComplete : System.Web.UI.Page
{
    //viewstate constants
    const string PREAUTHRESPONSE = "PreAuthResponse";
    const string AUTHCODE = "AuthCode";
    const string INVOICE = "Invoice";
    const string AMOUNT = "Amount";
    const string REFNO = "RefNo";
    const string ACQREFDATA = "AcqRefData";
    const string TOKEN = "TOKEN";
    const string FREQUENCY = "FREQUENCY";
    
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            string paymentID, returnCode;

            if (Session["ReturnMethod"] == null)
                return;

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

            if (String.IsNullOrEmpty(returnCode))
            {
                returnCode = "-1";
            }

            string returnMessage = Request.Form["ReturnMessage"];

            this.txtPaymentID.Text = paymentID;
            this.txtReturnCode.Text = returnCode;
            this.txtReturnMessage.Text = returnMessage;
            ViewState["PaymentID"] = paymentID;

            if (returnCode == "0") // Only call verification if form post indicates success.
            {
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

                        this.lstResponseValues.ClearSelection();

                        this.lstResponseValues.Items.Add("ResponseCode: " + response.ResponseCode);
                        this.lstResponseValues.Items.Add("Form Post ReturnCode: " +  returnCode);
                        this.lstResponseValues.Items.Add("Status: " + response.Status);
                        this.lstResponseValues.Items.Add("StatusMessage: " + response.StatusMessage);
                        this.lstResponseValues.Items.Add("DisplayMessage: " + response.DisplayMessage);
                        this.lstResponseValues.Items.Add("AvsResult: " + response.AvsResult);
                        this.lstResponseValues.Items.Add("CvvResult: " + response.CvvResult);
                        this.lstResponseValues.Items.Add("CardType: " + response.CardType);
                        this.lstResponseValues.Items.Add("AuthCode: " + response.AuthCode);

                        this.lstResponseValues.Items.Add("RefNo: " + response.RefNo);
                        this.lstResponseValues.Items.Add("Invoice: " + response.Invoice);
                        this.lstResponseValues.Items.Add("AcqRefData: " + response.AcqRefData);
                        string amount = String.Format("{0:C}", response.Amount);
                        this.lstResponseValues.Items.Add("Amount: " + amount);
                        this.lstResponseValues.Items.Add("MaskAccount: " + response.MaskedAccount);
                        this.lstResponseValues.Items.Add("Exp Date: " + response.ExpDate);
                        this.lstResponseValues.Items.Add("TransDateTime: " + response.TransPostTime);
                        this.lstResponseValues.Items.Add("TranType: " + response.TranType);
                        this.lstResponseValues.Items.Add("PaymentIDExpired: " + response.PaymentIDExpired.ToString());
                        this.lstResponseValues.Items.Add("CustomerCode: " + response.CustomerCode);
                        amount = String.Format("{0:C}", response.TaxAmount);
                        this.lstResponseValues.Items.Add("TaxAmount: " + amount);
                        this.lstResponseValues.Items.Add("Memo: " + response.Memo);
                        this.lstResponseValues.Items.Add("VoiceAuthCode: " + response.VoiceAuthCode);
                        amount = String.Format("{0:C}", response.AuthAmount);
                        this.lstResponseValues.Items.Add("AuthAmount: " + amount);
                        this.lstResponseValues.Items.Add("ProcessData: " + response.ProcessData);
                        this.lstResponseValues.Items.Add("Cardholder Name: " + response.CardholderName);
                        this.lstResponseValues.Items.Add("OperatorID:" + response.OperatorID);
                        this.lstResponseValues.Items.Add("Terminal Name:" + response.TerminalName);
                        this.lstResponseValues.Items.Add("Lane ID:" + response.LaneID);


                        if (!String.IsNullOrEmpty(response.Token))
                            this.txttoken.Text = response.Token;
                        
                        this.lstResponseValues.Visible = true;

                        if (response.Status == "Declined")
                        {
                            this.lblStatus.Text = "The payment was declined and your order has not been processed.  Please try again."  ;
                        }

                        if (returnCode == "105")
                        {
                            this.lblStatus.Text = "The payment page automatically timed out.  Payment was not processed.";
                        }

                        //call ack whether successful or not.
                        int ack = UIHelper.ProcessPaymentAck(hcVerifyRequest.PaymentID, hcVerifyRequest.MerchantID, hcVerifyRequest.Password);

                        this.lstResponseValues.Items.Add("Acknowledgement called. Return Code: " + ack);
                    }
                    else
                    {
                        this.lblStatus.Text = "We were unable to process your payment";
                    }
                }
                else
                {
                    this.lblStatus.Text = "We were unable to process your payment.";
                }
            }
            else
            {
                this.lblStatus.Text = "We were unable to process your payment";
            }
        }
    }

}
