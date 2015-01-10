using System;

/// <summary>
/// THis page handles the user when they return to the web site 
///   after adding their card information on the HostedCheckout CardInfo Page.
/// This is the Page that should be listed in the ProcessCompleteURL when
///   the InitializeCardInfo process was started
/// </summary>
public partial class AddCardComplete : System.Web.UI.Page
{

    //viewstate constants
    const string TOKEN = "TOKEN";
    const string FREQUENCY = "FREQUENCY";
    
    /// <summary>
    /// Upon page load, get the CardID and call VerifyCardInfo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            this.divCapturReturnVals.Visible = false;

            string cardID, returnCode, returnMessage;

            if (Session["ReturnMethod"] == null)
                return;

            //Determine which return method was used so 
            //  we know where to get the CardID from (URL Query Parm or Form Post Hidden field)
            //Both ways is supported in the demo for testing but the developer will only use one of these 2 methods.

            if (Session["ReturnMethod"].ToString() == "post")
            {
                //get the cardid from the form post
                cardID = Request.Form["CardID"];
                returnCode = Request.Form["ReturnCode"];
                returnMessage = Request.Form["ReturnMessage"];
            }
            else
            {
                //get the cardID from the URL
                cardID = Request.QueryString["CardID"];
                returnCode = Request.QueryString["ReturnCode"];
                returnMessage = String.Empty;
            }

            this.txtCardID.Text = cardID;
            this.txtReturnCode.Text = returnCode;
            this.txtReturnMessage.Text = returnMessage;
            ViewState["CardID"] = cardID;

            if (returnCode == "0") // Only call verification if success.
            {

                //build the request for VerifyCardInfo
                HCService.CardInfoRequest hcVerifyRequest = new HCService.CardInfoRequest();

                hcVerifyRequest.MerchantID = Session["MerchantID"].ToString();
                hcVerifyRequest.Password = Session["PW"].ToString();
                hcVerifyRequest.CardID = cardID;

                //Call VerifyCardInfo web service method.
                HCService.HCService hcWS = new HCService.HCService();
                HCService.CardInfoResponse response = new HCService.CardInfoResponse();
                response = hcWS.VerifyCardInfo(hcVerifyRequest);

                if (response != null)
                {
                    if (response.ResponseCode == 0)
                    {
                        //save the token in the session for later use in the demo
                        Session[TOKEN] = response.Token;

                        //display the response information on the page
                        this.lstResults.ClearSelection();
                        this.lstResults.Items.Add("Status: " + response.Status);
                        this.lstResults.Items.Add("StatusMessage: " + response.StatusMessage);
                        this.lstResults.Items.Add("DisplayMessage: " + response.DisplayMessage);
                        this.lstResults.Items.Add("CardType: " + response.CardType);
                        this.lstResults.Items.Add("MaskAccount: " + response.MaskedAccount);
                        this.lstResults.Items.Add("Exp Date: " + response.ExpDate);
                        this.lstResults.Items.Add("TranType: " + response.TranType);
                        this.lstResults.Items.Add("CardIDExpired: " + response.CardIDExpired.ToString());
                        this.lstResults.Items.Add("Cardholder Name: " + response.CardHolderName);
                        this.lstResults.Items.Add("CardUsage: " + response.CardUsage);
                        this.lstResults.Items.Add("OperatorID:" + response.OperatorID);
                        this.lstResults.Items.Add("LaneID:" + response.LaneID);

                        if (!String.IsNullOrEmpty(response.Token))
                            this.lbltoken.Text = response.Token;

                        this.lstResults.Visible = true;

                        if (response.Status == "Approved")
                            this.lblStatus.Text = "Your card has been saved.";
                        else
                        {
                            this.lblStatus.Text = response.DisplayMessage;
                        }
                    }
                    else
                    {
                        //the call to VerifyCardInfo failed.  Display the response code.
                        this.lblStatus.Text = "Verify Card Info not successful -- response.ResponseCode = " + response.ResponseCode;
                    }
                    
                    //this is for QA only.  Don't call Ack if it is false.
                    if ((Session["CallAck"] != null) && (Session["CallAck"].ToString() == "True"))
                    {
                        //call ack whether successful or not.
                        int ack = UIHelper.ProcessCardInfoAck(hcVerifyRequest.CardID, hcVerifyRequest.MerchantID, hcVerifyRequest.Password);

                        this.lstResults.Items.Add("Acknowledgement called. Return Code: " + ack);
                    }
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
