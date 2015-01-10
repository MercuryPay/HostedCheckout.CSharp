using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

/// <summary>
/// this is a demo page for adding a card on file by Redirecting to Mercury's HostedCheckout CardInfo page 
/// </summary>
public partial class AddCard : System.Web.UI.Page
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
                this.txtLogoURL.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["LogoURL"].ToString();
                this.txtCancelBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnDefaultUrl"].ToString();
                this.txtCancelBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["CancelBtnHoverUrl"].ToString();
                this.txtSubmitBtnDefault.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnDefaultUrl"].ToString();
                this.txtSubmitBtnHover.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["SubmitBtnHoverUrl"].ToString();                

                string cardID = Request.Form["CardID"];
                string returnCode = Request.Form["ReturnCode"];
                string returnMessage = Request.Form["ReturnMessage"];

                if (!String.IsNullOrEmpty(returnCode))
                    this.lblError.Text = returnMessage + " (" + returnCode + ")  Card ID: " + cardID;
                this.rblDisplayStyle.Items[1].Selected = true;

            }
        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " PageLoad Error: " + ex.Message;
        }
    }

    /// <summary>
    /// Initialize the CardInfo process and Redirect to the HostedCheckout CardInfo page 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddCard_Click(object sender, EventArgs e)
    {
        try
        {
            string errorMessage = string.Empty;
            
            //get the HostedCheckout CardInfo URL to redirect to from the web.config file.
            string addCardURL = System.Web.Configuration.WebConfigurationManager.AppSettings["HC_CardInfoURL"].ToString();

            //for demo purposes, if it's a Kindle, redirect to the Mobile version of the HostedCheckout CardIno page.
            //Smaller Tablets work best with the mobile optimized page.
            if (this.chkDetectKindle.Checked)
            {
                string agent = Request.UserAgent.ToLower();
                if (agent.Contains("silk") || (agent.Contains("kindle")))
                    addCardURL = System.Web.Configuration.WebConfigurationManager.AppSettings["Mobile_HC_CardInfoURL"].ToString();
            }

            //Initialize the CardInfo process
            HCService.InitCardInfoRequest hcRequest = new HCService.InitCardInfoRequest
            {
                MerchantID = this.txtMerchantID.Text,
                Password = this.txtPassword.Text,
                Frequency = this.rblFrequency.SelectedItem.Text,
                CardHolderName = this.txtCardHolderName.Text,
                ReturnUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["returnURL_CardInfo"].ToString(),
                ProcessCompleteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ProcessCompleteURL_CardInfo"].ToString(),
                BackgroundColor = this.txtBGColor.Text,
                FontColor = this.ddlMultiColor.SelectedItem.Text,
                FontFamily = GetFontFamily(),
                FontSize = this.rblFontSize.SelectedValue,
                PageTitle = "Durango Pizza - Powered by Mercury",
                LogoUrl = this.txtLogoURL.Text,
                DisplayStyle = this.rblDisplayStyle.SelectedValue,
                SecurityLogo = this.rblSecurityLogo.SelectedValue,
                CancelButtonText = this.txtCancelButton.Text,
                SubmitButtonText = this.txtPaymentButtonText.Text,
                OperatorID = this.txtOpID.Text,
                ButtonBackgroundColor = this.txtBtnBgColor.Text,
                ButtonTextColor = this.txtBtnTextColor.Text,
                JCB = this.rblJCB.SelectedValue,
                Diners = this.rblDiners.SelectedValue,
                CancelButtonDefaultImageUrl = this.txtCancelBtnDefault.Text,
                CancelButtonHoverImageUrl = this.txtCancelBtnHover.Text,
                SubmitButtonDefaultImageUrl = this.txtSubmitBtnDefault.Text,
                SubmitButtonHoverImageUrl = this.txtSubmitBtnHover.Text,
                PageTimeoutDuration = this.ddlPageTimeoutDuration.SelectedValue,
                LaneID = this.txtLaneID.Text,                
            };

            //Call the web service to initialize the CardInfo request.
            HCService.HCService hcWS = new HCService.HCService();
            HCService.InitCardInfoResponse response = new HCService.InitCardInfoResponse();
            response = hcWS.InitializeCardInfo(hcRequest);

            //get the CardID from the response and redirect to HostedCheckout
            if (response != null)
            {
                if (response.ResponseCode == 0)  //success
                {
                    //for demo purposes only, save some of the information in session for the order complete page to use.
                    Session["MerchantID"] = this.txtMerchantID.Text;
                    Session["PW"] = this.txtPassword.Text;
                    Session["ReturnMethod"] = this.ddlReturnMethod.SelectedValue;
                    Session["CallAck"] = this.chkCallAck.Checked.ToString();
                    Session[FREQUENCY] = this.rblFrequency.SelectedItem.Text;
                    Session["HCUrl"] = addCardURL;
                    Session["CardID"] = response.CardID;
                    
                    GoToCheckout(addCardURL, response.CardID);
                }
                else
                {
                    //InitializeCardInfo failed.
                    this.lblError.Text = "InitializeCardInfo Failed. Response Code: " + response.ResponseCode + "<br/>Response Message: " + response.Message;
                }
            }
            else
                //something seriously wrong. probably couldn't connect to the web service at all
                this.lblError.Text = "Response from InitializeCardInfo was Null";  
        }
        catch (System.Threading.ThreadAbortException)
        {
            //eat the exception and continue on.  this happens due to the response redirection above and is harmless
        }
        catch (Exception ex)
        {
            this.lblError.Text = this.lblError.Text + " Card Info Error: " + ex.Message;
        }
    }

    private void GoToCheckout(string addCardURL, string cardID)
    {
        string getOrPost = ddlReturnMethod.SelectedValue;

        //build the response html to redirect over to HostedCheckout CardIno URL.  
        //Pass CardID and optional ReturnMethod
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.Write("<html><head>");
        System.Web.HttpContext.Current.Response.Write("</head><body onload=\"document.frmCardInfo.submit()\">");
        System.Web.HttpContext.Current.Response.Write("<form name=\"frmCardInfo\" method=\"Post\" action=\"" + addCardURL + "\" >");
        System.Web.HttpContext.Current.Response.Write("<input name=\"CardID\" type=\"hidden\" value=\"" + cardID + "\">");

        if (!string.IsNullOrEmpty(getOrPost))
            System.Web.HttpContext.Current.Response.Write("<input name=\"ReturnMethod\" type=\"hidden\" value=\"" + getOrPost + "\">");

        System.Web.HttpContext.Current.Response.Write("</form>");
        System.Web.HttpContext.Current.Response.Write("</body></html>");
        System.Web.HttpContext.Current.Response.End();

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

    protected void rblFontFamily_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.txtFontFamily.Visible = rblFontFamily.SelectedIndex == 3;
        colorManipulation();
    }

    protected void btnAddCardIFrame_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CardInfoIFrame.aspx");
    }

    protected void btnGoToPage_Click(object sender, EventArgs e)
    {
        GoToCheckout(Session["HCUrl"].ToString(), Session["CardID"].ToString());
    }

}

