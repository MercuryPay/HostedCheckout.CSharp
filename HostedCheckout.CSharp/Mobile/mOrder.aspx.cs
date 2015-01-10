using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

public partial class mOrder : System.Web.UI.Page
{
    const string FREQUENCY = "FREQUENCY";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = string.Empty;

            if (!Page.IsPostBack)
            {
                this.txtMerchantID.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["merchantID"].ToString();
                this.txtPassword.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["HCPassword"].ToString();
                this.txtLogoURL.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_LogoURL"].ToString();
                this.txtCancelBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnDefaultUrl"].ToString();
                this.txtCancelBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnHoverUrl"].ToString();
                this.txtSubmitBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnDefaultUrl"].ToString();
                this.txtSubmitBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnHoverUrl"].ToString();
                this.txtReturnURL.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_ReturnURL"].ToString();
                this.txtProcessCompleteURL.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_ProcessCompleteURL"].ToString();                

                string paymentID = Request.Form["PaymentID"];
                string returnCode = Request.Form["ReturnCode"];
                string returnMessage = Request.Form["ReturnMessage"];

                if (!String.IsNullOrEmpty(returnCode))
                    this.lblError.Text = returnMessage + " (" + returnCode + ")  Payment ID: " + paymentID;
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " PageLoad Error: " + ex.Message;
        }
    }

    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            string errorMessage = string.Empty;
            double amt = 0;

            HCService.InitPaymentRequest hcRequest = new HCService.InitPaymentRequest();

            hcRequest.MerchantID = this.txtMerchantID.Text;
            hcRequest.Password = this.txtPassword.Text;
            hcRequest.TranType = this.ddlTranType.SelectedItem.Text;
            Double.TryParse(this.txtTotalAmount.Text, out amt);
            hcRequest.TotalAmount = amt;
            hcRequest.Frequency = this.ddlFrequency.SelectedItem.Text;
            Random rand = new Random();
            hcRequest.Invoice = rand.Next(999999999).ToString();
            hcRequest.Memo = this.txtMemo.Text;
            Double.TryParse(this.txtTaxAmount.Text, out amt);
            hcRequest.TaxAmount = amt;
            hcRequest.CardHolderName = this.txtName.Text;
            hcRequest.AVSAddress = this.txtAddress.Text;
            hcRequest.AVSZip = this.txtZip.Text;
            hcRequest.CustomerCode = this.txtCustCode.Text;
            hcRequest.ProcessCompleteUrl = this.txtProcessCompleteURL.Text;
            hcRequest.ReturnUrl = this.txtReturnURL.Text;
            hcRequest.DisplayStyle = this.ddlDisplayStyle.SelectedItem.Text;
            hcRequest.BackgroundColor = this.txtBackgroundColor.Text;
            hcRequest.FontColor = this.txtFontColor.Text;
            hcRequest.LogoUrl = this.txtLogoURL.Text;
            hcRequest.PageTitle = this.txtPageTitle.Text;
            hcRequest.CancelButtonText = this.txtCancelButtonText.Text;
            hcRequest.SubmitButtonText = this.txtPaymentButtonText.Text;
            hcRequest.ButtonTextColor = this.txtButtonTextColor.Text;
            hcRequest.ButtonBackgroundColor = this.txtButtonBgColor.Text;
            hcRequest.JCB = this.ddlJCB.SelectedValue;
            hcRequest.Diners = this.ddlDiners.SelectedValue;
            
            hcRequest.CancelButtonDefaultImageUrl = this.txtCancelBtnDefault.Text;
            hcRequest.CancelButtonHoverImageUrl = this.txtCancelBtnHover.Text;
            hcRequest.SubmitButtonDefaultImageUrl = this.txtSubmitBtnDefault.Text;
            hcRequest.SubmitButtonHoverImageUrl = this.txtSubmitBtnHover.Text;
            hcRequest.PageTimeoutDuration = this.ddlPageTimeoutDuration.SelectedValue;
            hcRequest.PageTimeoutIndicator = this.ddlPageTimeoutIndicator.SelectedValue;
            hcRequest.TotalAmountBackgroundColor = this.txtTotalAmtBGColor.Text;
            hcRequest.AVSFields = this.rblAVSFields.SelectedValue;
            hcRequest.CVV = this.rblCVV.SelectedValue;
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
                    //save the freq in session for OrderComplete page to use.
                    Session[FREQUENCY] = this.ddlFrequency.SelectedItem.Text;
                    Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;
                    
                    Session["PaymentID"] = response.PaymentID;
                    
                    string url = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_HostedCheckoutURL"].ToString();
                    Session["HCUrl"] = url;

                    GoToCheckout(url, response.PaymentID);
                    
                }
                else
                    this.lblError.Text = "InitializePayment Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
            }
            else
                this.lblError.Text = "Response from InitializePayment was Null";
        }
        catch (System.Threading.ThreadAbortException)
        {

        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " Checkout Error: " + ex.Message;
        }
    }

    private void GoToCheckout(string hostedCheckoutURL, string paymentID)
    {
        string getOrPost = ddlReturnMethod.SelectedValue;

        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Write("<html><head>");
        System.Web.HttpContext.Current.Response.Write("</head><body onload=\"document.frmCheckout.submit()\">");
        System.Web.HttpContext.Current.Response.Write("<form name=\"frmCheckout\" method=\"post\" action=\""
            + hostedCheckoutURL + "\" >");
        System.Web.HttpContext.Current.Response.Write("<input name=\"PaymentID\" type=\"hidden\" value=\""
            + paymentID + "\">");

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
