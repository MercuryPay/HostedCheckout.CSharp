using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

public partial class ShoppingCartPOS : System.Web.UI.Page
{
    const string FREQUENCY = "FREQUENCY";

    /// <summary>
    /// This page is a simulated POS shopping cart that is used
    ///   to redirect to HostedCheckout POS page for a payment
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                this.txtCancelBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnDefaultUrl"].ToString();
                this.txtCancelBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnHoverUrl"].ToString();
                this.txtSubmitBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnDefaultUrl"].ToString();
                this.txtSubmitBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnHoverUrl"].ToString();
                

                UpdateTotal();

                string paymentID = Request.Form["PaymentID"];
                string returnCode = Request.Form["ReturnCode"];
                string returnMessage = Request.Form["ReturnMessage"];

                if (!String.IsNullOrEmpty(returnCode))
                    this.lblError.Text = returnMessage + " (" + returnCode + ")  Payment ID: " + paymentID;
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
    ///   over to HostedCheckout POS page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            string errorMessage = string.Empty;

            //Get input amounts
            double dAmt1 = 0;
            double dAmt2 = 0;
            double dTaxAmt = 0;

            Double.TryParse(this.txtItem1Amt.Text, out dAmt1);
            Double.TryParse(this.txtItem2Amt.Text, out dAmt2);
            Double.TryParse(this.txtTaxAmt.Text, out dTaxAmt);

            string hostedCheckoutURL = System.Web.Configuration.WebConfigurationManager.AppSettings["hostedCheckoutURL_POS"].ToString();

            //Build request for the HCService initialize requests
            HCService.InitPaymentRequest hcRequest = new HCService.InitPaymentRequest();

            hcRequest.MerchantID = this.txtMerchantID.Text;
            hcRequest.Password = this.txtPassword.Text;
            hcRequest.TranType = this.rblTranType.SelectedItem.Text;
            hcRequest.TotalAmount = dAmt1 + dAmt2 + dTaxAmt;
            hcRequest.Frequency = this.rblFrequency.SelectedItem.Text;

            hcRequest.Invoice = this.txtInvoice.Text;
            hcRequest.TaxAmount = Convert.ToDouble(txtTaxAmt.Text);

            hcRequest.CardHolderName = this.txtName.Text;
            hcRequest.AVSAddress = this.txtAddress.Text;
            hcRequest.AVSZip = this.txtZip.Text;
            hcRequest.CustomerCode = this.txtCustomerCode.Text;
            hcRequest.Memo = this.txtMemo.Text;
            hcRequest.ReturnUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["returnURL_POS"].ToString();
            hcRequest.ProcessCompleteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["processCompleteURL_POS"].ToString();
            hcRequest.BackgroundColor = this.txtBGColor.Text;
            hcRequest.FontColor = this.ddlMultiColor.SelectedItem.Text;
            hcRequest.FontSize = this.rblFontSize.SelectedItem.Text;
            hcRequest.FontFamily = GetFontFamily();
            hcRequest.PageTitle = this.txtPageTitle.Text;
            hcRequest.DisplayStyle = this.rblDisplayStyle.SelectedItem.Text;
            hcRequest.CancelButtonText = this.txtCancelButton.Text;
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
            hcRequest.CancelButtonDefaultImageUrl = this.txtCancelBtnDefault.Text;
            hcRequest.CancelButtonHoverImageUrl = this.txtCancelBtnHover.Text;
            hcRequest.SubmitButtonDefaultImageUrl = this.txtSubmitBtnDefault.Text;
            hcRequest.SubmitButtonHoverImageUrl = this.txtSubmitBtnHover.Text;
            hcRequest.PageTimeoutDuration = this.ddlPageTimeoutDuration.SelectedValue;
            hcRequest.PageTimeoutIndicator = this.rblPageTimeoutIndicator.SelectedValue;
            hcRequest.TotalAmountBackgroundColor = this.txtTotalAmtBgColor.Text;            
            hcRequest.CVV = this.rblCVV.SelectedValue;
            if (this.rblForce.SelectedIndex != -1)
                hcRequest.ForceManualTablet = this.rblForce.SelectedItem.Text;
            hcRequest.LaneID = this.txtLaneID.Text;

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
                    Session[FREQUENCY] = this.rblFrequency.SelectedItem.Text;
                    Session["HCUrl"] = hostedCheckoutURL;
                    Session["PaymentID"] = response.PaymentID;

                    int responseCode = response.ResponseCode;
                    
                    GoToCheckout(hostedCheckoutURL, response.PaymentID);
                    
                }
                else
                    this.lblError.Text = "InitializePayment Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
            }
            else
                this.lblError.Text = "Response from InitializePayment was Null";
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

    protected void btnZeroAmt_Click(object sender, EventArgs e)
    {
        this.txtItem1Amt.Text = "0.00";
        this.txtItem2Amt.Text = "0.00";
        this.txtTaxAmt.Text = "0.00";
        this.txtTotalAmt.Text = "0.00";
        this.rblTranType.SelectedIndex = 4;
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

    private void GoToCheckout(string hostedCheckoutURL, string paymentID)
    {
        Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;
        string getOrPost = ddlReturnMethod.SelectedValue;        

        //redirect to the HostedCheckoutPOS page with paymentID
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
