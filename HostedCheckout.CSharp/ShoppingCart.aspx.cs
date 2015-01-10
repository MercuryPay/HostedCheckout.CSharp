using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

/// <summary>
/// demo page to simulate a shopping cart.
/// Used to redirect the user to HostedCheckout to collect payment.
/// </summary>
public partial class ShoppingCart : System.Web.UI.Page
{
    const string FREQUENCY = "FREQUENCY";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                populateDdlMultiColor();
                colorManipulation();

                ddlMultiColor.Items.FindByValue("Transparent").Selected = false;
                ddlMultiColor.Items.FindByValue("Black").Selected = true;

                this.txtMerchantID.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["merchantID"].ToString();
                this.txtPassword.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["HCPassword"].ToString();
                this.txtLogoURL.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["logoURL"].ToString();

                UpdateTotal();

                string paymentID = Request.Form["PaymentID"];
                if (paymentID == null)
                    paymentID = string.Empty;

                string returnCode = Request.Form["ReturnCode"];
                string returnMessage = Request.Form["ReturnMessage"];

                if (!String.IsNullOrEmpty(returnCode))
                {
                    this.lblError.Text = returnMessage + " (" + returnCode + ")  PaymentID: " + paymentID;
                }

                this.rblDisplayStyle.Items[1].Selected = true;

                Random rand = new Random();
                this.txtInvoice.Text = rand.Next(999999999).ToString();
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " PageLoad Error: " + ex.Message;
        }
    }

    /// <summary>
    /// Checkout button to initialize the payment and redirect
    ///   over to HostedCheckout.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {

        try
        {
            PayViaCredit();
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " Checkout Error: " + ex.Message;
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
        double dAmt2 = 0.0;
        double dTaxAmt = 0.0;

        Double.TryParse(this.txtItem1Amt.Text, out dAmt1);
        Double.TryParse(this.txtItem2Amt.Text, out dAmt2);
        Double.TryParse(this.txtTaxAmt.Text, out dTaxAmt);

        this.txtTotalAmt.Text = String.Format("{0:C}", dAmt1 + dAmt2 + dTaxAmt);


    }
    protected void rblButtonType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rblButtonType.SelectedValue == "Image")
        {
            this.txtCancelBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnDefaultUrl"].ToString();
            this.txtCancelBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnHoverUrl"].ToString();
            this.txtSubmitBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnDefaultUrl"].ToString();
            this.txtSubmitBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnHoverUrl"].ToString();
        }
        else
        {
            this.txtCancelBtnDefault.Text = "";
            this.txtCancelBtnHover.Text = "";
            this.txtSubmitBtnDefault.Text = "";
            this.txtSubmitBtnHover.Text = "";
        }
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
                this.txtMemo.Text = "HC EComm Credit Pmt Redirect";
                this.ddlTranType.Items.Clear();
                this.ddlTranType.Items.Add("Sale");
                this.ddlTranType.Items.Add("PreAuth");
                this.ddlTranType.Items.Add("ZeroAuth");
                this.ddlTranType.SelectedValue = "PreAuth";
                break;
        }

        this.trAck.Attributes.Add("style", display);
        this.trCustomerCode.Attributes.Add("style", display);
        this.trDiners.Attributes.Add("style", display);
        this.trFrequency.Attributes.Add("style", display);
        this.trJCB.Attributes.Add("style", display);
        this.trKindle.Attributes.Add("style", display);
    }

    private void PayViaCredit()
    {
        //Get input amounts
        double dAmt1 = 0;
        double dAmt2 = 0;
        double dTaxAmt = 0;



        Double.TryParse(this.txtItem1Amt.Text, out dAmt1);
        Double.TryParse(this.txtItem2Amt.Text, out dAmt2);
        Double.TryParse(this.txtTaxAmt.Text, out dTaxAmt);

        double totalAmount = dAmt1 + dAmt2 + dTaxAmt;


        //get the HostedCheckout URL
        string hostedCheckoutURL = System.Web.Configuration.WebConfigurationManager.AppSettings["hostedCheckoutURL"].ToString();

        //for demo purposes, if it's a Kindle, redirect to the Mobile version of the HostedCheckout page.
        //Smaller Tablets work best with the mobile optimized page.
        if (this.chkDetectKindle.Checked)
        {
            string agent = Request.UserAgent.ToLower();
            if (agent.Contains("silk") || (agent.Contains("kindle")))
                hostedCheckoutURL = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_HostedCheckoutURL"].ToString();
        }

        //Build request for the HCService initialize requests
        HCService.InitPaymentRequest hcRequest = new HCService.InitPaymentRequest();

        hcRequest.MerchantID = this.txtMerchantID.Text;
        hcRequest.Password = this.txtPassword.Text;
        hcRequest.TranType = this.ddlTranType.SelectedValue;
        hcRequest.TotalAmount = dAmt1 + dAmt2 + dTaxAmt;
        hcRequest.Frequency = this.rblFrequency.SelectedItem.Text;
        hcRequest.Invoice = this.txtInvoice.Text;
        hcRequest.Memo = this.txtMemo.Text;
        hcRequest.PageTitle = this.txtPageTitle.Text;
        hcRequest.TaxAmount = Convert.ToDouble(txtTaxAmt.Text);

        hcRequest.CardHolderName = this.txtName.Text;
        hcRequest.AVSAddress = this.txtAddress.Text;
        hcRequest.AVSZip = this.txtZip.Text;
        hcRequest.CustomerCode = this.txtCustomerCode.Text;
        hcRequest.Memo = this.txtMemo.Text;
        hcRequest.ReturnUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["returnURL"].ToString();
        hcRequest.ProcessCompleteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["processCompleteURL"].ToString();
        hcRequest.BackgroundColor = this.txtBGColor.Text;
        hcRequest.FontColor = this.ddlMultiColor.SelectedItem.Text;
        hcRequest.FontSize = this.rblFontSize.SelectedValue;
        hcRequest.FontFamily = GetFontFamily();
        hcRequest.PageTitle = this.txtPageTitle.Text;
        hcRequest.LogoUrl = this.txtLogoURL.Text;
        hcRequest.DisplayStyle = this.rblDisplayStyle.SelectedValue;
        hcRequest.CancelButtonText = this.txtCancelButton.Text;
        hcRequest.SubmitButtonText = this.txtPaymentButtonText.Text;
        hcRequest.ButtonTextColor = this.txtButtonTextColor.Text;
        hcRequest.ButtonBackgroundColor = this.txtButtonBgColor.Text;
        hcRequest.JCB = this.rblJCB.SelectedValue;
        hcRequest.Diners = this.rblDiners.SelectedValue;
        hcRequest.AVSFields = this.rblAVSFields.SelectedValue;
        hcRequest.CVV = this.rblCVV.SelectedValue;

        if (this.rblButtonType.SelectedValue == "Image")
        {
            hcRequest.CancelButtonDefaultImageUrl = this.txtCancelBtnDefault.Text;
            hcRequest.CancelButtonHoverImageUrl = this.txtCancelBtnHover.Text;
            hcRequest.SubmitButtonDefaultImageUrl = this.txtSubmitBtnDefault.Text;
            hcRequest.SubmitButtonHoverImageUrl = this.txtSubmitBtnHover.Text;
        }

        hcRequest.PageTimeoutDuration = this.ddlPageTimeoutDuration.SelectedValue;
        hcRequest.PageTimeoutIndicator = this.rblPageTimeoutIndicator.SelectedValue;
        hcRequest.TotalAmountBackgroundColor = this.txtTotalAmtBgColor.Text;
        hcRequest.SecurityLogo = this.rblSecurityLogo.SelectedItem.Text;
        hcRequest.LaneID = this.txtLaneID.Text;

        //Make the request to initialize the payment.
        HCService.HCService hcWS = new HCService.HCService();
        HCService.InitPaymentResponse response = new HCService.InitPaymentResponse();
        response = hcWS.InitializePayment(hcRequest);

        //get the paymentID from the response and redirect to HostedCHeckout
        if (response != null)
        {
            if (response.ResponseCode == 0)  //success
            {
                Session["MerchantID"] = this.txtMerchantID.Text;
                Session["PW"] = this.txtPassword.Text;
                Session[FREQUENCY] = this.rblFrequency.SelectedItem.Text;
                Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;
                Session["CallAck"] = this.chkCallAck.Checked.ToString();
                Session["HCUrl"] = hostedCheckoutURL;
                Session["PaymentID"] = response.PaymentID;
                GoToCheckout(hostedCheckoutURL, response.PaymentID);

            }
            else
                this.lblError.Text = "InitializePayment Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
        }
        else
            this.lblError.Text = "Response from InitializePayment was Null";
    }

    private void GoToCheckout(string hostedCheckoutURL, string paymentID)
    {
        string getOrPost = ddlReturnMethod.SelectedValue;

        //build the response html to redirect over to HostedCheckout URL.  
        //Pass PaymentID and optional ReturnMethod
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Write("<html><head>");
        System.Web.HttpContext.Current.Response.Write("</head><body onload=\"document.frmCheckout.submit()\">");
        System.Web.HttpContext.Current.Response.Write("<form name=\"frmCheckout\" method=\"Post\" action=\"" + hostedCheckoutURL + "\" >");
        System.Web.HttpContext.Current.Response.Write("<input name=\"PaymentID\" type=\"hidden\" value=\"" + paymentID + "\">");
        if (!string.IsNullOrEmpty(getOrPost))
            System.Web.HttpContext.Current.Response.Write("<input name=\"ReturnMethod\" type=\"hidden\" value=\"" + getOrPost + "\">");
        System.Web.HttpContext.Current.Response.Write("</form>");
        System.Web.HttpContext.Current.Response.Write("</body></html>");
        System.Web.HttpContext.Current.Response.End();

    }


    protected void btnGoToPage_Click(object sender, EventArgs e)
    {
        GoToCheckout(Session["HCUrl"].ToString(), Session["PaymentID"].ToString());
    }
}
