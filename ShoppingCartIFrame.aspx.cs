using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

/// <summary>
/// this is a demo page for processing a payment using
///   Mercury's embedded HostedCheckout iFrame page
/// </summary>
public partial class ShoppingCartIFrame : System.Web.UI.Page
{

    /// <summary>
    /// load the page and hide the iFrame to start.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session[Constants.PaymentIDIFrameEComm] == null)
        {
            this.lblPaymentIDValue.Text = "none";
            this.btnResetID.Enabled = false;
        }
        else
        {
            this.lblPaymentIDValue.Text = Session[Constants.PaymentIDIFrameEComm].ToString();
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
            this.txtCancelBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnDefaultUrl"].ToString();
            this.txtCancelBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnHoverUrl"].ToString();
            this.txtSubmitBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnDefaultUrl"].ToString();
            this.txtSubmitBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnHoverUrl"].ToString();            

            UpdateTotal();

            this.lblError.Visible = false;
            this.rblDisplayStyle.SelectedValue = "Custom";
            this.lblError.Visible = false;
            this.divThanks.Visible = false;
            Random rand = new Random();
            this.txtInvoice.Text = rand.Next(999999999).ToString();

        }
    }

    protected void btnResetID_Click(object sender, EventArgs e)
    {
        this.lblPaymentIDValue.Text = "null";
        Session[Constants.PaymentIDIFrameEComm] = null;
    }

    /// <summary>
    /// After the user enters the card number on the iFrame
    ///   and submits the payment, it will tell the user that
    ///   the transaction was successful.
    /// This Next or Continue button allows the user to continue.  When pressed,
    ///   call the VerifyPayment method to get the token.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinue_Click(object sender, EventArgs e)
    {

        ContinueForCredit();
        this.btnContinue.Visible = false;
    }

    /// <summary>
    /// Initialize the Payment process, display iFrame and nav buttons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnCheckOut_Click(object sender, EventArgs e)
    {

        try
        {
            PayViaCredit();

            //adjust iFrame height as needed.
            if (this.rblOrderTotal.SelectedItem.Text.ToLower() == "on")
                this.ifrm.Attributes.Add("height", "450px");
            else
                this.ifrm.Attributes.Add("height", "340px");

            Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;

            //show iFrame and nav buttons
            this.ifrm.Visible = true;

            this.divOrder.Visible = false;
            this.btnCheckout.Visible = false;
            this.btnContinue.Visible = true;
            if (this.rblCancelButton.SelectedValue.ToLower() == "off")
                this.btnBack.Visible = true;
            Session["IFrameStatus"] = "CHECKOUT";

        }
        catch (System.Threading.ThreadAbortException)
        { }
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
        Session[Constants.PaymentIDIFrameEComm] = null;
        this.btnResetID.Enabled = false;
        this.lblPaymentIDValue.Text = "null";

        //reset invoice # so don't get duplicate transaction.
        Random rand = new Random();
        this.txtInvoice.Text = rand.Next(999999999).ToString();
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

    protected void btnUpdateAmt_Click(object sender, EventArgs e)
    {
        UpdateTotal();
    }

    protected void UpdateTotal()
    {
        double dAmt1 = 0.0;
        double dAmt2 = 0.0;
        double dTaxAmt = 0.0;

        Double.TryParse(this.txtItem1Amt.Text, out dAmt1);
        Double.TryParse(this.txtItem2Amt.Text, out dAmt2);
        Double.TryParse(this.txtTaxAmt.Text, out dTaxAmt);

        this.txtTotalAmt.Text = String.Format("{0:C}", dAmt1 + dAmt2 + dTaxAmt);
    }

    private void ContinueForCredit()
    {
        HCService.HCService hcWS = new HCService.HCService();
        HCService.PaymentInfoRequest hcRequest = new HCService.PaymentInfoRequest();

        if ((Session[Constants.PaymentIDIFrameEComm] != null) && (Session["MerchantID"] != null) && (Session["PW"] != null))
        {
            hcRequest.PaymentID = Session[Constants.PaymentIDIFrameEComm].ToString();
            hcRequest.MerchantID = Session["MerchantID"].ToString();
            hcRequest.Password = Session["PW"].ToString();

            //Verify the Payment and get your token
            HCService.PaymentInfoResponse response = new HCService.PaymentInfoResponse();
            response = hcWS.VerifyPayment(hcRequest);

            if (response.PaymentIDExpired)
            {
                this.ifrm.Attributes.Add("src", "");
                this.ifrm.Visible = false;
                this.divThanks.Visible = true;
                this.btnContinue.Visible = false;
                this.btnCheckout.Visible = true;
                this.lblError.Visible = false;
                this.btnBack.Visible = false;
                //erase the payment ID from session since it is used and expired.
                Session[Constants.PaymentIDIFrameEComm] = null;
            }
            else
            {
                this.lblError.Text = "Please press the 'Submit Payment' button before selecting 'Next'";
                this.btnContinue.Visible = true;
                this.lblError.Visible = true;
            }
        }
        else
        {
            this.ifrm.Attributes.Add("src", "");
            this.ifrm.Visible = false;
            this.divOrder.Visible = true;
            this.btnBack.Visible = false;
            //this.btnContinue.Visible = false;
            this.btnCheckout.Visible = true;
            this.lblError.Visible = false;
            Session[Constants.PaymentIDIFrameEComm] = null;

        }


    }

    private void PayViaCredit()
    {
        //Get input amounts
        double dAmt1 = 0;
        double dAmt2 = 0;
        double dTaxAmt = 0;
        string paymentID = null;

        //variables to Make the request to initialize the payment.
        HCService.HCService hcWS = new HCService.HCService();
        HCService.InitPaymentResponse response = new HCService.InitPaymentResponse();

        Session["MerchantID"] = this.txtMerchantID.Text;
        Session["PW"] = this.txtPassword.Text;

        if (Session[Constants.PaymentIDIFrameEComm] != null)
            paymentID = Session[Constants.PaymentIDIFrameEComm].ToString();

        if (paymentID == null)
        {
            Double.TryParse(this.txtItem1Amt.Text, out dAmt1);
            Double.TryParse(this.txtItem2Amt.Text, out dAmt2);
            Double.TryParse(this.txtTaxAmt.Text, out dTaxAmt);

            //Build request for the HCService initialize requests
            HCService.InitPaymentRequest hcRequest = new HCService.InitPaymentRequest();

            hcRequest.MerchantID = this.txtMerchantID.Text;
            hcRequest.Password = this.txtPassword.Text;
            hcRequest.TranType = this.ddlTranType.SelectedItem.Text;
            hcRequest.TotalAmount = dAmt1 + dAmt2 + dTaxAmt;
            hcRequest.Frequency = this.rblFrequency.SelectedItem.Text;
            hcRequest.Invoice = this.txtInvoice.Text;
            hcRequest.TaxAmount = Convert.ToDouble(txtTaxAmt.Text);
            hcRequest.CardHolderName = this.txtName.Text;
            hcRequest.AVSAddress = this.txtAddress.Text;
            hcRequest.AVSZip = this.txtZip.Text;
            hcRequest.CustomerCode = this.txtCustomerCode.Text;
            hcRequest.Memo = this.txtMemo.Text;
            hcRequest.ReturnUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ReturnURLIFrame"].ToString();
            hcRequest.ProcessCompleteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["processCompleteURLIFrame"].ToString();
            hcRequest.BackgroundColor = this.txtBGColor.Text;
            hcRequest.FontColor = this.ddlMultiColor.SelectedItem.Text;
            hcRequest.FontSize = this.rblFontSize.SelectedItem.Text;
            hcRequest.FontFamily = GetFontFamily();
            hcRequest.DisplayStyle = this.rblDisplayStyle.SelectedItem.Text;
            hcRequest.OrderTotal = this.rblOrderTotal.SelectedItem.Text;
            hcRequest.SubmitButtonText = this.txtPaymentButtonText.Text;
            hcRequest.CancelButtonText = this.txtCancelButton.Text;
            hcRequest.ButtonTextColor = this.txtButtonTextColor.Text;
            hcRequest.ButtonBackgroundColor = this.txtButtonBgColor.Text;
            hcRequest.JCB = this.rblJCB.SelectedItem.Value;
            hcRequest.Diners = this.rblDiners.SelectedItem.Value;
            hcRequest.CancelButtonDefaultImageUrl = this.txtCancelBtnDefault.Text;
            hcRequest.CancelButtonHoverImageUrl = this.txtCancelBtnHover.Text;
            hcRequest.SubmitButtonDefaultImageUrl = this.txtSubmitBtnDefault.Text;
            hcRequest.SubmitButtonHoverImageUrl = this.txtSubmitBtnHover.Text;
            hcRequest.TotalAmountBackgroundColor = this.txtTotalAmtBgColor.Text;
            hcRequest.SecurityLogo = this.rblSecurityLogo.SelectedItem.Text;
            hcRequest.AVSFields = this.rblAVSFields.SelectedValue;
            hcRequest.CVV = this.rblCVV.SelectedValue;
            hcRequest.LaneID = this.txtLaneID.Text;

            if (this.ddlPageTimeoutDuration.SelectedValue != string.Empty)
            {
                hcRequest.PageTimeoutDuration = this.ddlPageTimeoutDuration.SelectedValue;
            }
            hcRequest.PageTimeoutIndicator = this.rblPageTimeoutIndicator.SelectedValue;
            hcRequest.CancelButton = this.rblCancelButton.SelectedValue;

            response = hcWS.InitializePayment(hcRequest);

            if (response == null)
                return;

            if (response.ResponseCode != 0)
            {
                this.lblError.Text = "InitializePayment Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
                this.lblError.Visible = true;
                return;
            }

            paymentID = response.PaymentID;
            Session[Constants.PaymentIDIFrameEComm] = paymentID;
            

        }

        string hostedCheckoutURL = System.Web.Configuration.WebConfigurationManager.AppSettings["hostedCheckoutURLIFrame"].ToString();

        //Set the URL for the iFrame.
        //  Include ReturnMethod if specified in demo.  defaults to post.
        string getOrPost = ddlReturnMethod.SelectedValue;
        if (string.IsNullOrEmpty(getOrPost))
            this.ifrm.Attributes.Add("src", hostedCheckoutURL + "?pid=" + paymentID);
        else
            this.ifrm.Attributes.Add("src", hostedCheckoutURL + "?pid=" + paymentID + "&ReturnMethod=" + getOrPost);

    }

    protected void rblPmtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowAPIFieldsForPmtType("credit");
    }

    private void ShowAPIFieldsForPmtType(string pmtType)
    {
        string display = string.Empty;

        switch (pmtType)
        {
            case "credit":
                display = "";
                this.txtMemo.Text = "HC EComm Credit Pmt iFrame";
                this.ddlTranType.Items.Clear();
                this.ddlTranType.Items.Add("Sale");
                this.ddlTranType.Items.Add("PreAuth");
                this.ddlTranType.Items.Add("ZeroAuth");
                this.ddlTranType.SelectedValue = "PreAuth";
                this.ifrm.Attributes.Add("style", "text-align: center; display: none;border: 1px solid #A3221C; height: 450px");
                break;
        }

        this.trCustomerCode.Attributes.Add("style", display);
        this.trDiners.Attributes.Add("style", display);
        this.trFrequency.Attributes.Add("style", display);
        this.trJCB.Attributes.Add("style", display);
    }
}