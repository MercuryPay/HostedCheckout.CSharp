using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

public partial class mAddCard : System.Web.UI.Page
{
    const string FREQUENCY = "FREQUENCY";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = string.Empty;

            if (Page.IsPostBack == false)
            {

                this.txtMerchantID.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["merchantID"].ToString();
                this.txtPassword.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["HCPassword"].ToString();
                this.txtLogoURL.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_LogoURL"].ToString();
                this.txtCancelBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnDefaultUrl"].ToString();
                this.txtCancelBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnHoverUrl"].ToString();
                this.txtSubmitBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnDefaultUrl"].ToString();
                this.txtSubmitBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnHoverUrl"].ToString();
                

                string cardID = Request.Form["CardID"];
                string returnCode = Request.Form["ReturnCode"];
                string returnMessage = Request.Form["ReturnMessage"];

                if (!String.IsNullOrEmpty(returnCode))
                    this.lblError.Text = returnMessage + " (" + returnCode + ")  Card ID: " + cardID;
            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " PageLoad Error: " + ex.Message;
        }
    }

    protected void btnAddCard_Click(object sender, EventArgs e)
    {
        try
        {
            string errorMessage = string.Empty;
            string addCardURL = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_HC_CardInfoURL"].ToString();

            HCService.InitCardInfoRequest hcRequest = new HCService.InitCardInfoRequest();

            Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;
            

            hcRequest.MerchantID = this.txtMerchantID.Text;
            hcRequest.Password = this.txtPassword.Text;
            hcRequest.Frequency = this.ddlFrequency.SelectedItem.Text;
            hcRequest.CardHolderName = this.txtName.Text;
            hcRequest.ProcessCompleteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_ProcessCompleteURL_CardInfo"].ToString();
            hcRequest.ReturnUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_ReturnURL_CardInfo"].ToString();
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
            hcRequest.LaneID = this.txtLaneID.Text;

            //Make the request to initialize the payment.
            HCService.HCService hcWS = new HCService.HCService();
            HCService.InitCardInfoResponse response = new HCService.InitCardInfoResponse();
            response = hcWS.InitializeCardInfo(hcRequest);

            if (response != null)
            {
                if (response.ResponseCode == 0)  //success
                {
                    Session["MerchantID"] = this.txtMerchantID.Text;
                    Session["PW"] = this.txtPassword.Text;
                    Session["HCUrl"] = addCardURL;
                    Session["CardID"] = response.CardID;

                    //save the freq in session for OrderComplete page to use.
                    Session[FREQUENCY] = this.ddlFrequency.SelectedItem.Text;

                    int responseCode = response.ResponseCode;

                    
                    GoToCheckout(addCardURL, response.CardID);
                    
                }
                else
                    this.lblError.Text = "InitializeCardInfo Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
            }
            else
                this.lblError.Text = "Response from InitializeCardInfo was Null";
        }
        catch (System.Threading.ThreadAbortException)
        {

        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " CardInfo Error: " + ex.Message;
        }
    }

    private void GoToCheckout(string addCardURL, string cardID)
    {
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Write("<html><head>");
        System.Web.HttpContext.Current.Response.Write("</head><body onload=\"document.frmCardInfo.submit()\">");
        System.Web.HttpContext.Current.Response.Write("<form name=\"frmCardInfo\" method=\"Post\" action=\"" + addCardURL + "\" >");
        System.Web.HttpContext.Current.Response.Write("<input name=\"CardID\" type=\"hidden\" value=\"" + cardID + "\">");
        
        string getOrPost = ddlReturnMethod.SelectedValue;

        if (!string.IsNullOrEmpty(getOrPost))
            System.Web.HttpContext.Current.Response.Write("<input name=\"ReturnMethod\" type=\"hidden\" value=\"" + getOrPost + "\">");

        System.Web.HttpContext.Current.Response.Write("</form>");
        System.Web.HttpContext.Current.Response.Write("</body></html>");
        System.Web.HttpContext.Current.Response.End();

     
    }

    protected void btnGoToPage_Click(object sender, EventArgs e)
    {
        GoToCheckout(Session["HCUrl"].ToString(), Session["CardID"].ToString());
    }

}
