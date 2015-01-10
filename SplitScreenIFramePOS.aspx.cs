using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

/// <summary>
/// this is a demo POS page for collecting a payment by embedding 
///   Mercury's HostedCheckout POS iFrame page
/// </summary>
public partial class ShoppingCartIFramePOS : System.Web.UI.Page
{
    const string FREQUENCY = "FREQUENCY";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                this.ifrm.Attributes.Add("onload", "this.style.display='block';");
                this.ifrm.Attributes.Add("src", "");

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

                UpdateTotal();

                string paymentID = Request.Form["PaymentID"];
                string returnCode = Request.Form["ReturnCode"];
                string returnMessage = Request.Form["ReturnMessage"];

                if (!String.IsNullOrEmpty(returnCode))
                    this.lblError.Text = returnMessage + " (" + returnCode + ")  Payment ID: " + paymentID;
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

    /// <summary>
    /// Initialize the Payment process, display iFrame and nav buttons
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            string errorMessage = string.Empty;

            double dAmt1 = 0;
            double dTaxAmt = 0;

            Double.TryParse(this.txtItem1Amt.Text, out dAmt1);
            Double.TryParse(this.txtTaxAmt.Text, out dTaxAmt);

            string hostedCheckoutURL = System.Web.Configuration.WebConfigurationManager.AppSettings["HostedCheckoutURL_POSIFrame"].ToString(); 

            //Build request for the HCService initialize requests
            HCService.InitPaymentRequest hcRequest = new HCService.InitPaymentRequest();

            hcRequest.MerchantID = this.txtMerchantID.Text;
            hcRequest.Password = this.txtPassword.Text;
            hcRequest.TranType = this.rblTranType.SelectedItem.Text;
            hcRequest.TotalAmount = dAmt1 + dTaxAmt;
            hcRequest.Frequency = this.rblFrequency.SelectedItem.Text;

            //for demo purposes, use a random invoice number to avoid duplicates.
            Random rand = new Random();
            hcRequest.Invoice = rand.Next(999999999).ToString();

            hcRequest.TaxAmount = Convert.ToDouble(txtTaxAmt.Text);
            hcRequest.CardHolderName = this.txtName.Text;
            hcRequest.AVSAddress = this.txtAddress.Text;
            hcRequest.AVSZip = this.txtZip.Text;
            hcRequest.CustomerCode = this.txtCustomerCode.Text;
            hcRequest.Memo = "Durango Pizza 1.0";
            hcRequest.ReturnUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ReturnURLIFrame"].ToString();
            hcRequest.ProcessCompleteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ProcessCompleteURLIFramePOS"].ToString();
            hcRequest.BackgroundColor = this.txtBGColor.Text;
            hcRequest.FontColor = ddlMultiColor.SelectedItem.Text;
            hcRequest.FontSize = this.rblFontSize.SelectedItem.Text;
            hcRequest.FontFamily = GetFontFamily();
            hcRequest.DisplayStyle = this.rblDisplayStyle.SelectedItem.Text;
            hcRequest.SubmitButtonText = this.txtPaymentButtonText.Text;
            hcRequest.AVSFields = this.rblAVSFields.SelectedValue;
            hcRequest.DefaultSwipe = this.rblDefaultSwipe.SelectedValue;
            if (this.rblTranType.SelectedItem.Text == "VoiceAuth")
                hcRequest.VoiceAuthCode = this.txtVoiceAuthCode.Text;
            hcRequest.Keypad = this.rblKeypad.SelectedItem.Text;
            hcRequest.PartialAuth = this.rblPartialAuth.SelectedItem.Text;
            hcRequest.OperatorID = this.txtOpID.Text;
            hcRequest.TerminalName = this.txtTermName.Text;
            hcRequest.ButtonBackgroundColor = this.txtBtnBgColor.Text;
            hcRequest.ButtonTextColor = this.txtBtnTextColor.Text;
            hcRequest.CardEntryMethod = this.txtCardEntryMethod.Text;
            hcRequest.SubmitButtonDefaultImageUrl = this.txtSubmitBtnDefault.Text;
            hcRequest.SubmitButtonHoverImageUrl = this.txtSubmitBtnHover.Text;

            //Make the request to initialize the payment.
            HCService.HCService hcWS = new HCService.HCService();
            HCService.InitPaymentResponse response = new HCService.InitPaymentResponse();
            response = hcWS.InitializePayment(hcRequest);

            if (response != null)
            {
                if (response.ResponseCode == 0)  //success
                {
                    Session["MerchantID"] = this.txtMerchantID.Text;
                    Session["PW"] = this.txtPassword.Text;
                    Session["PaymentID"] = response.PaymentID;

                    //save the freq in session for OrderComplete page to use.
                    Session[FREQUENCY] = this.rblFrequency.SelectedItem.Text;

                    int responseCode = response.ResponseCode;

                    Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;
                    string getOrPost = ddlReturnMethod.SelectedValue;

                    if (string.IsNullOrEmpty(getOrPost))
                        this.ifrm.Attributes.Add("src", hostedCheckoutURL + "?pid=" + response.PaymentID);
                    else
                        this.ifrm.Attributes.Add("src", hostedCheckoutURL + "?pid=" + response.PaymentID + "&ReturnMethod=" + getOrPost);

                    this.ifrm.Visible = true;
                    //this.divOrder.Visible = false;
                    this.btnContinue.Visible = true;
                    this.btnBack.Visible = true;
                }
                else
                    this.lblError.Text = "InitializePayment Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
                    this.lblError.Visible = true;
            }
            else
                this.lblError.Text = "Response from InitializePayment was Null";
                this.lblError.Visible = true;
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
    }

    /// <summary>
    /// After the user enters the card number on the iFrame
    ///   and submits the payment, it will tell the user that
    ///   the transaction was successful.
    /// This Next or Continue button allows the user to continue.  When pressed,
    ///   call the VerifyCardInfo method to get the token.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinue_Click(object sender, EventArgs e)
    {

        HCService.HCService hcWS = new HCService.HCService();
        HCService.PaymentInfoRequest hcRequest = new HCService.PaymentInfoRequest();

        if ((Session["PaymentID"] != null) && (Session["MerchantID"] != null) && (Session["PW"] != null))
        {
            hcRequest.PaymentID = Session["PaymentID"].ToString();

            hcRequest.MerchantID = Session["MerchantID"].ToString();
            hcRequest.Password = Session["PW"].ToString();

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

    protected void btnUpdateAmt_Click(object sender, EventArgs e)
    {
        UpdateTotal();
    }

    protected void UpdateTotal()
    {
        double dAmt1 = 0.0;

        double dTaxAmt = 0.0;

        Double.TryParse(this.txtItem1Amt.Text, out dAmt1);

        Double.TryParse(this.txtTaxAmt.Text, out dTaxAmt);

        this.txtTotalAmt.Text = String.Format("{0:C}", dAmt1 + dTaxAmt);


    }
}
