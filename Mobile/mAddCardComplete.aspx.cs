using System;

public partial class mAddCardComplete : System.Web.UI.Page
{
    //viewstate constants
    const string TOKEN = "TOKEN";
    const string FREQUENCY = "FREQUENCY";
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string cardID = string.Empty;
            string returnCode = string.Empty;

            if (Session["ReturnMethod"] == null)
                return;

            if (Session["ReturnMethod"].ToString() == "post")
            {
                cardID = Request.Form["CardID"];
                returnCode = Request.Form["ReturnCode"];
            }
            else
            {
                cardID = Request.QueryString["CardID"];
                returnCode = Request.QueryString["ReturnCode"];
            }

            string returnMessage = Request.Form["ReturnMessage"];
            this.txtCardID.Text = cardID;
            this.txtReturnCode.Text = returnCode;
            this.txtReturnMessage.Text = returnMessage;
            ViewState["CardID"] = cardID;

            if (returnCode == "0") // Only call verification if form post indicates success.
            {
                HCService.CardInfoRequest hcVerifyRequest = new HCService.CardInfoRequest();

                hcVerifyRequest.MerchantID = Session["MerchantID"].ToString();
                hcVerifyRequest.Password = Session["PW"].ToString();
                hcVerifyRequest.CardID = cardID;

                //Make the request to verify the payment.
                HCService.HCService hcWS = new HCService.HCService();
                HCService.CardInfoResponse response = new HCService.CardInfoResponse();
                response = hcWS.VerifyCardInfo(hcVerifyRequest);

                if (response != null)
                {
                    if (response.ResponseCode == 0)
                    {

                        Session[TOKEN] = response.Token;

                        this.lstResponseValues.ClearSelection();
                        this.lstResponseValues.Items.Add("Status: " + response.Status);
                        this.lstResponseValues.Items.Add("StatusMessage: " + response.StatusMessage);
                        this.lstResponseValues.Items.Add("DisplayMessage: " + response.DisplayMessage);
                        this.lstResponseValues.Items.Add("CardType: " + response.CardType);
                        this.lstResponseValues.Items.Add("MaskAccount: " + response.MaskedAccount);
                        this.lstResponseValues.Items.Add("ExpDate: " + response.ExpDate);
                        this.lstResponseValues.Items.Add("TranType: " + response.TranType);
                        this.lstResponseValues.Items.Add("CardIDExpired: " + response.CardIDExpired.ToString());
                        this.lstResponseValues.Items.Add("Cardholder Name: " + response.CardHolderName);
                        this.lstResponseValues.Items.Add("CardUsage: " + response.CardUsage);
                        this.lstResponseValues.Items.Add("OperatorID:" + response.OperatorID);
                        this.lstResponseValues.Items.Add("LaneID:" + response.LaneID);

                        if (!String.IsNullOrEmpty(response.Token))
                            this.txttoken.Text = response.Token;
                        
                        this.lstResponseValues.Visible = true;

                        if (response.Status == "Approved")
                            this.lblStatus.Text = "Your card has been saved.";
                        else
                        {
                            this.lblStatus.Text = response.DisplayMessage; ;
                        }
                    }
                    else
                    {
                        this.lblStatus.Text = "Verify Card Info not successful -- response.ResponseCode = " + response.ResponseCode;
                    }

                    //call ack whether successful or not.
                    int ack = UIHelper.ProcessCardInfoAck(hcVerifyRequest.CardID, hcVerifyRequest.MerchantID, hcVerifyRequest.Password);

                    this.lstResponseValues.Items.Add("Acknowledgement called. Return Code: " + ack);

                }
                else
                {
                    this.lblStatus.Text = "We were unable to save the card.";
                }
            }
            else
            {
                this.lblStatus.Text = "Initial return code not successful -- Form Post ReturnCode = " + returnCode;
            }
        }
    }

}
