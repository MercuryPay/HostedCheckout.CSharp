using System;

/// <summary>
/// THis page handles the user after the card is added within the 
///     Hosted Checkout iFrame.  The user is redirected to this page.
/// This is the Page that should be listed in the ProcessCompleteURL when
///   the InitializeCardInfo process was started
/// </summary>
public partial class AddCardCompleteIFrame : System.Web.UI.Page
{
    //viewstate constants
    const string TOKEN = "TOKEN";

    /// <summary>
    /// Upon page load, get the CardID and call VerifyCardInfo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string cardID, returnCode, returnMessage;

            if (Session["ReturnMethod"] == null)
                return;

            //Determine which return method was used so 
            //  we know where to get the CardID from (URL Query Parm or Form Post Hidden field)
            //Both ways is supported in the demo for testing but the developer will only use one of these 2 methods.
            if (Session["ReturnMethod"].ToString() == "post")
            {
                cardID = Request.Form["CardID"];
                returnCode = Request.Form["ReturnCode"];
                returnMessage = Request.Form["ReturnMessage"];
            }
            else
            {
                cardID = Request.QueryString["CardID"];
                returnCode = Request.QueryString["ReturnCode"];
                returnMessage = "ReturnCode=" + returnCode + "." + " ReturnMethod=GET";
            }

            this.txtPaymentID.Text = cardID;
            this.txtReturnCode.Text = returnCode;
            this.txtReturnMessage.Text = returnMessage;

            if (returnCode == "0") // Only call verification if form post indicates success.
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
                        this.lstCardInfoResults.ClearSelection();
                        this.lstCardInfoResults.Items.Add("Status: " + response.Status);
                        this.lstCardInfoResults.Items.Add("StatusMessage: " + response.StatusMessage);
                        this.lstCardInfoResults.Items.Add("DisplayMessage: " + response.DisplayMessage);
                        this.lstCardInfoResults.Items.Add("ReturnMethod: " + Session["ReturnMethod"].ToString());
                        this.lstCardInfoResults.Items.Add("CardUsage: " + response.CardUsage);
                        this.lstCardInfoResults.Items.Add("MaskAccount: " + response.MaskedAccount);
                        this.lstCardInfoResults.Items.Add("Exp Date: " + response.ExpDate);
                        this.lstCardInfoResults.Items.Add("TranType: " + response.TranType);
                        this.lstCardInfoResults.Items.Add("CardIDExpired: " + response.CardIDExpired.ToString());
                        this.lstCardInfoResults.Items.Add("OperatorID:" + response.OperatorID);
                        this.lstCardInfoResults.Items.Add("LaneID:" + response.LaneID);

                        if (!String.IsNullOrEmpty(response.Token))
                            this.lbltoken.Text = response.Token;

                        this.lstCardInfoResults.Visible = true;

                        if (response.Status == "Approved")
                            this.lblStatus.Text = "Your card has been saved. Press Next to continue...";
                        else
                        {
                            this.lblStatus.Text = response.DisplayMessage; ;
                        }
                    }
                    else
                    {
                        //the call to VerifyCardInfo failed.  Display the response code.
                        this.lblStatus.Text = "Card save not successful -- response.ResponseCode = " + response.ResponseCode;
                    }
                    //call ack whether successful or not.
                    int ack = UIHelper.ProcessCardInfoAck(hcVerifyRequest.CardID, hcVerifyRequest.MerchantID, hcVerifyRequest.Password);

                    this.lstCardInfoResults.Items.Add("Acknowledgement called. Return Code: " + ack);
                }
                else
                {
                    this.lblStatus.Text = "We were unable to save your card.";
                }
            }
            else
            {
                this.lblStatus.Text = "There was a problem saving your card.";
                this.lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        Session[Constants.CardIDIFrameEComm] = null;
    }
}