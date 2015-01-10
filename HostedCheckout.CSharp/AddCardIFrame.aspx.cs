 using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

/// <summary>
/// this is a demo page for adding a card on file by embedding 
///   Mercury's HostedCheckout CardInfo iFrame page
/// </summary>
public partial class AddCardIFrame : System.Web.UI.Page
{

    /// <summary>
    /// load the page and hide the iFrame to start.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session[Constants.CardIDIFrameEComm] == null)
        {
            this.lblCardIDValue.Text = "none";
            this.btnResetID.Enabled = false;
        }
        else
        {
            this.lblCardIDValue.Text = Session[Constants.CardIDIFrameEComm].ToString();
            this.btnResetID.Enabled = true;
        }

        if (Page.IsPostBack == false)
        {
            this.ifrm.Attributes.Add("onload", "this.style.display='block';");
            this.ifrm.Attributes.Add("src", "");

            //hide the iFrame and nav buttons for now
            this.ifrm.Visible = false;
            this.btnContinue.Visible = false;
            this.btnBack.Visible = false;

            populateDdlMultiColor();
            colorManipulation();

            ddlMultiColor.Items.FindByValue("Transparent").Selected = false;
            ddlMultiColor.Items.FindByValue("Black").Selected = true;

            this.txtMerchantID.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["merchantID"].ToString();
            this.txtPassword.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["HCPassword"].ToString();
            this.txtSubmitBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnDefaultUrl"].ToString();
            this.txtSubmitBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnHoverUrl"].ToString();
            this.txtCancelBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnDefaultUrl"].ToString();
            this.txtCancelBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnHoverUrl"].ToString();
            

            this.lblError.Visible = false;
            this.rblDisplayStyle.SelectedValue = "Custom";
            this.lblError.Visible = false;
            this.divThanks.Visible = false;
        }
    }

    protected void btnResetID_Click(object sender, EventArgs e)
    {

        this.lblCardIDValue.Text = "null";
        Session[Constants.CardIDIFrameEComm] = null;
    }

    /// <summary>
    /// After the user enters the card number on the iFrame
    ///   and submits the informaion, it will tell the user that
    ///   the transaction was successful.
    /// This Next or Continue button allows the user to continue.  When pressed,
    ///   call the VerifyCardInfo method to get the token.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinue_Click(object sender, EventArgs e)
    {

        HCService.HCService hcWS = new HCService.HCService();
        HCService.CardInfoRequest hcRequest = new HCService.CardInfoRequest();

        if ((Session[Constants.CardIDIFrameEComm] != null) && (Session["MerchantID"] != null) && (Session["PW"] != null))
        {
            hcRequest.CardID = Session[Constants.CardIDIFrameEComm].ToString();
            hcRequest.MerchantID = Session["MerchantID"].ToString();
            hcRequest.Password = Session["PW"].ToString();

            //Verify the Card Info and get your token
            HCService.CardInfoResponse response = new HCService.CardInfoResponse();
            response = hcWS.VerifyCardInfo(hcRequest);

            //the cardID has already been used.
            if (response.CardIDExpired)
            {
                this.ifrm.Attributes.Add("src", "");
                this.ifrm.Visible = false;
                this.divThanks.Visible = true;
                this.btnContinue.Visible = false;
                this.btnCheckout.Visible = true;
                this.lblError.Visible = false;
                this.btnBack.Visible = false;
                Session[Constants.CardIDIFrameEComm] = null;
            }
            else
            {
                this.lblError.Text = "Please press the 'Update' button before selecting 'Next'";
                this.lblError.Visible = true;
            }
        }
        else
        {
            this.ifrm.Attributes.Add("src", "");
            this.ifrm.Visible = false;
            this.divCardInfo.Visible = true;
            this.btnBack.Visible = false;
            this.btnContinue.Visible = false;
            this.btnCheckout.Visible = true;
            this.lblError.Visible = false;
            Session[Constants.CardIDIFrameEComm] = null;
        }
    }

    /// <summary>
    /// Initialize the CardInfo process, display iFrame and nav buttons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        string cardID = null;
        string hostedCheckoutURL = System.Web.Configuration.WebConfigurationManager.AppSettings["HC_CardInfoURLIFrame"].ToString();
        HCService.InitCardInfoResponse response;

        Session["MerchantID"] = this.txtMerchantID.Text;
        Session["PW"] = this.txtPassword.Text;
        Session["HCUrl"] = hostedCheckoutURL;


        if (Session[Constants.CardIDIFrameEComm] != null)
            cardID = Session[Constants.CardIDIFrameEComm].ToString();

        if (cardID == null)
        {
            //Build request for the HCService initialize requests
            HCService.InitCardInfoRequest hcRequest = new HCService.InitCardInfoRequest();

            hcRequest.MerchantID = this.txtMerchantID.Text;
            hcRequest.Password = this.txtPassword.Text;
            hcRequest.Frequency = this.rblFrequency.SelectedItem.Text;
            hcRequest.CardHolderName = this.txtCardHolderName.Text;
            hcRequest.ReturnUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ReturnURLIFrame_CardInfo"].ToString();
            hcRequest.ProcessCompleteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ProcessCompleteURLIFrame_CardInfo"].ToString();
            hcRequest.BackgroundColor = this.txtBGColor.Text;
            hcRequest.FontColor = this.ddlMultiColor.SelectedItem.Text;
            hcRequest.FontSize = this.rblFontSize.SelectedItem.Text;
            hcRequest.FontFamily = GetFontFamily();
            hcRequest.DisplayStyle = this.rblDisplayStyle.SelectedItem.Text;
            hcRequest.SecurityLogo = this.rblSecurityLogo.SelectedItem.Text;
            hcRequest.SubmitButtonText = this.txtPaymentButtonText.Text;
            hcRequest.CancelButtonText = this.txtCancelButton.Text;
            hcRequest.OperatorID = this.txtOpID.Text;
            hcRequest.ButtonBackgroundColor = this.txtBtnBgColor.Text;
            hcRequest.ButtonTextColor = this.txtBtnTextColor.Text;
            hcRequest.JCB = this.rblJCB.SelectedItem.Value;
            hcRequest.Diners = this.rblDiners.SelectedItem.Value;
            hcRequest.SubmitButtonDefaultImageUrl = this.txtSubmitBtnDefault.Text;
            hcRequest.SubmitButtonHoverImageUrl = this.txtSubmitBtnHover.Text;
            hcRequest.CancelButtonDefaultImageUrl = this.txtCancelBtnDefault.Text;
            hcRequest.CancelButtonHoverImageUrl = this.txtCancelBtnHover.Text;
            hcRequest.CancelButton = this.rblCancelButton.SelectedValue;
            hcRequest.PageTimeoutDuration = this.ddlPageTimeoutDuration.SelectedValue;
            hcRequest.LaneID = this.txtLaneID.Text;
            

            //Make the request to initialize the card info.
            HCService.HCService hcWS = new HCService.HCService();
            response = new HCService.InitCardInfoResponse();
            response = hcWS.InitializeCardInfo(hcRequest);

            if (response == null)
            {
                return;
            }

            if (response.ResponseCode != 0)
            {
                this.lblError.Text = "InitializeCardInfo Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
                this.lblError.Visible = true;
                return;
            }

            cardID = response.CardID;
            Session[Constants.CardIDIFrameEComm] = response.CardID;

        }

        Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;
        string getOrPost = ddlReturnMethod.SelectedValue;

        //adjust iFrame height as needed.
        if (this.rblOrderTotal.SelectedItem.Text.ToLower() == "on")
            this.ifrm.Attributes.Add("height", "400px");
        else
            this.ifrm.Attributes.Add("height", "340px");

        //Set the URL for the iFrame.
        //  Include ReturnMethod if specified in demo.  defaults to post.
        if (string.IsNullOrEmpty(getOrPost))
            this.ifrm.Attributes.Add("src", hostedCheckoutURL + "?CardID=" + cardID);
        else
            this.ifrm.Attributes.Add("src", hostedCheckoutURL + "?CardID=" + cardID + "&ReturnMethod=" + getOrPost);


        this.ifrm.Visible = true;            

        this.divCardInfo.Visible = false;
        this.btnCheckout.Visible = false;

        //show the nav buttons
        this.btnContinue.Visible = true;
        if (this.rblCancelButton.SelectedValue.ToLower() != "on")
            this.btnBack.Visible = true;

        Session["IFrameStatus"] = "CHECKOUT";
        
    }

    /// <summary>
    /// handle the back button.  hide the iFrame if the user goes back.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.ifrm.Attributes.Add("src", "");
        this.ifrm.Visible = false;
        this.divCardInfo.Visible = true;
        this.btnBack.Visible = false;
        this.btnContinue.Visible = false;
        this.btnCheckout.Visible = true;
        this.lblError.Visible = false;
        this.lblCardIDValue.Text = "null";
        Session[Constants.CardIDIFrameEComm] = null;
    }

    private void populateDdlMultiColor()
    {
        ddlMultiColor.DataSource = finalColorList();
        ddlMultiColor.DataBind();
    }

    private List<string> finalColorList()
    {
        string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
        string[] systemEnvironmentColors =
            new string[(
            typeof(System.Drawing.SystemColors)).GetProperties().Length];

        int index = 0;

        foreach (MemberInfo member in (
            typeof(System.Drawing.SystemColors)).GetProperties())
        {
            systemEnvironmentColors[index++] = member.Name;
        }

        List<string> finalColorList = new List<string>();

        foreach (string color in allColors)
        {
            if (Array.IndexOf(systemEnvironmentColors, color) < 0)
            {
                finalColorList.Add(color);
            }
        }
        return finalColorList;
    }

    private void colorManipulation()
    {
        int row;
        for (row = 0; row < ddlMultiColor.Items.Count - 1; row++)
        {
            ddlMultiColor.Items[row].Attributes.Add("style",
                "background-color:" + ddlMultiColor.Items[row].Value);
        }
        ddlMultiColor.BackColor =
            Color.FromName(ddlMultiColor.SelectedItem.Text);
    }

    protected string GetFontFamily()
    {
        if (this.rblFontFamily.SelectedValue == "Other")
            return this.txtFontFamily.Text;
        else
            return this.rblFontFamily.SelectedValue;
    }
}