using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

/// <summary>
/// this is a demo POS page for adding a card on file by embedding 
///   Mercury's HostedCheckout CardInfoPOS iFrame page
/// </summary>
public partial class AddCardIFramePOS : System.Web.UI.Page
{
    const string FREQUENCY = "FREQUENCY";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session[Constants.CardIDIFramePOS] == null)
            {
                this.lblCardIDValue.Text = "none";
                this.btnResetID.Enabled = false;
            }
            else
            {
                this.lblCardIDValue.Text = Session[Constants.CardIDIFramePOS].ToString();
                this.btnResetID.Enabled = true;
            }

            if (Page.IsPostBack == false)
            {

                this.ifrm.Attributes.Add("onload", "this.style.display='block';");
                this.ifrm.Attributes.Add("src", "");

                //hide the iFrame 
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
                

                string cardID = Request.Form["CardID"];
                string returnCode = Request.Form["ReturnCode"];
                string returnMessage = Request.Form["ReturnMessage"];

                if (!String.IsNullOrEmpty(returnCode))
                    this.lblError.Text = returnMessage + " (" + returnCode + ")  Card ID: " + cardID;

                this.rblDisplayStyle.Items[1].Selected = true;

                this.lblError.Visible = false;
                this.rblDisplayStyle.SelectedValue = "Custom";
                this.divThanks.Visible = false;
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " PageLoad Error: " + ex.Message;
        }
    }

    protected void btnResetID_Click(object sender, EventArgs e)
    {

        this.lblCardIDValue.Text = "null";
        Session[Constants.CardIDIFramePOS] = null;
    }

    /// <summary>
    /// Initialize the CardInfo process, display iFrame and nav buttons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            HCService.InitCardInfoResponse response;
            Session["ReturnCode"] = "0";

            string errorMessage = string.Empty;
            string addCardURL = System.Web.Configuration.WebConfigurationManager.AppSettings["HC_CardInfoURL_POSIFrame"].ToString();
            string cardID = null;

            Session["MerchantID"] = this.txtMerchantID.Text;
            Session["PW"] = this.txtPassword.Text;

            if (Session[Constants.CardIDIFramePOS] != null)
                cardID = Session[Constants.CardIDIFramePOS].ToString();

            if (cardID == null)
            {
                HCService.InitCardInfoRequest hcRequest = new HCService.InitCardInfoRequest
                {
                    MerchantID = this.txtMerchantID.Text,
                    Password = this.txtPassword.Text,
                    Frequency = this.rblFrequency.SelectedItem.Text,
                    CardHolderName = this.txtCardHolderName.Text,
                    ReturnUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ReturnURLIFrame_CardInfo_POS"].ToString(),
                    ProcessCompleteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ProcessCompleteURLIFrame_CardInfo"].ToString(),
                    BackgroundColor = this.txtBGColor.Text,
                    FontColor = this.ddlMultiColor.SelectedItem.Text,
                    FontFamily = GetFontFamily(),
                    FontSize = this.rblFontSize.SelectedItem.Text,
                    LogoUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["logoURL"].ToString(),
                    DisplayStyle = this.rblDisplayStyle.SelectedItem.Text,
                    SubmitButtonText = this.txtPaymentButtonText.Text,
                    CancelButtonText = this.txtCancelButton.Text,
                    DefaultSwipe = this.rblDefaultSwipe.SelectedValue,
                    Keypad = this.rblKeypad.SelectedItem.Text,
                    ButtonBackgroundColor = this.txtBtnBgColor.Text,
                    ButtonTextColor = this.txtBtnTextColor.Text,
                    CardEntryMethod = this.txtCardEntryMethod.Text,
                    SubmitButtonDefaultImageUrl = this.txtSubmitBtnDefault.Text,
                    SubmitButtonHoverImageUrl = this.txtSubmitBtnHover.Text,
                    CancelButtonDefaultImageUrl = this.txtCancelBtnDefault.Text,
                    CancelButtonHoverImageUrl = this.txtCancelBtnHover.Text,
                    CancelButton = this.rblCancelButton.SelectedValue,
                    PageTimeoutDuration = this.ddlPageTimeoutDuration.SelectedValue,
                    ForceManualTablet = this.rblForce.SelectedIndex != -1 ? this.rblForce.SelectedItem.Text : "",
                    LaneID =  this.txtLaneID.Text,
                    
                };

                //Make the request to initialize the card info.
                HCService.HCService hcWS = new HCService.HCService();
                response = new HCService.InitCardInfoResponse();
                response = hcWS.InitializeCardInfo(hcRequest);

                if (response == null)
                    return;

                if (response.ResponseCode != 0)
                {
                    this.lblError.Text = "InitializeCardInfo Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
                    this.lblError.Visible = true;
                    return;

                }

                cardID = response.CardID;
                Session[Constants.CardIDIFramePOS] = response.CardID;

            }
            //save the frequency in session for OrderComplete page to use.
            Session[FREQUENCY] = this.rblFrequency.SelectedItem.Text;
            Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;
            string getOrPost = ddlReturnMethod.SelectedValue;

            //Set the URL for the iFrame.
            //  Include ReturnMethod if specified in demo.  defaults to post.
            if (string.IsNullOrEmpty(getOrPost))
                this.ifrm.Attributes.Add("src", addCardURL + "?CardID=" + cardID);
            else
                this.ifrm.Attributes.Add("src", addCardURL + "?CardID=" + cardID + "&ReturnMethod=" + getOrPost);

            //show the iFrame
            this.ifrm.Visible = true;                

            this.divOrder.Visible = false;

            //show the nav buttons
            this.btnContinue.Visible = true;

            if (this.rblCancelButton.SelectedValue.ToLower() == "off")
                this.btnBack.Visible = true;
            
        }
        catch (System.Threading.ThreadAbortException)
        {

        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " Checkout Error: " + ex.Message;
        }
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
        this.divOrder.Visible = true;
        this.btnBack.Visible = false;
        this.btnContinue.Visible = false;
        this.btnCheckout.Visible = true;
        this.lblError.Visible = false;
        Session[Constants.CardIDIFramePOS] = null;
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

        if ((Session[Constants.CardIDIFramePOS] != null) && (Session["MerchantID"] != null) && (Session["PW"] != null))
        {
            hcRequest.CardID = Session[Constants.CardIDIFramePOS].ToString();
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
                if (Session["ReturnCode"].ToString() != "0")
                    this.lblStatus.Text = "Your card has not been saved.";
                this.btnContinue.Visible = false;
                this.btnCheckout.Visible = true;
                this.lblError.Visible = false;
                this.btnBack.Visible = false;
                Session[Constants.CardIDIFramePOS] = null;
            }
            else
            {
                this.lblError.Text = "Enter card data before selecting 'Next'";
                this.lblError.Visible = true;
            }
        }
        else
        {
            this.ifrm.Attributes.Add("src", "");
            this.ifrm.Visible = false;
            this.divOrder.Visible = true;
            this.btnBack.Visible = false;
            this.btnContinue.Visible = false;
            this.btnCheckout.Visible = true;
            this.lblError.Visible = false;
            Session[Constants.CardIDIFramePOS] = null;

        }
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
