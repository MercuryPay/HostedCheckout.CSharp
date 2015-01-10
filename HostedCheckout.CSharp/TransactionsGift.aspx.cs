using System;
using TransactionServiceGift;
using System.Web.Configuration;

/// <summary>
/// this page is strictly to demonstrate the gift 
///     transactions that are available in the background
///     through the Transaction Web Service 
/// </summary>
public partial class TransactionsGift : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                //use default values from web.config
                this.txtCardNum.Text = WebConfigurationManager.AppSettings["GiftAccount"].ToString();
                this.txtCVV.Text = WebConfigurationManager.AppSettings["GiftCVV"].ToString();
                this.txtItemAmt.Text = WebConfigurationManager.AppSettings["GiftAmount"].ToString();

                //init viewstate variables so no null errors
                ViewState["RefNo"] = string.Empty;
                ViewState["Invoice"] = string.Empty;

                DisplayFieldsForTranType(ddlTranType.SelectedValue);
            }
            catch (NullReferenceException)
            {
                this.lblStatus.Text = "Missing web.config setting(s). Check for GiftAccount, GiftCVV, GiftAmount.";
            }
        }
    }

    /// <summary>
    /// display the transaction results
    /// </summary>
    /// <param name="response"></param>
    private void DisplayTransactionResults(GiftResponse response, string tran)
    {

        this.lstResults.Items.Clear();

        if (response != null)
        {
            this.lstResults.Items.Add("Status: " + response.Status);
            this.lstResults.Items.Add("Message: " + response.Message);
            string amount = String.Format("{0:C}", response.BalanceAmount);            
            this.lstResults.Items.Add("BalanceAmount: " + amount);

            amount = String.Format("{0:C}", response.AuthorizeAmount);            
            this.lstResults.Items.Add("AuthorizeAmount: " + amount);

            amount = String.Format("{0:C}", response.PurchaseAmount);            
            this.lstResults.Items.Add("PurchaseAmount: " + amount);

            this.lstResults.Items.Add("CVVResult: " + response.CVVResult);
            this.lstResults.Items.Add("Account: " + response.Account);
            this.lstResults.Items.Add("Invoice: " + response.Invoice);
            this.lstResults.Items.Add("RefNo: " + response.RefNo);
            this.lstResults.Items.Add("CardType: " + response.CardType);

            if (response.Status == "Approved")
            {
                this.lblStatus.Text = "The " + tran + " has been approved";
            }
            else
            {
                this.lblStatus.Text = "The status of the " + tran + " is: " + response.Status;
            }
        }
        else
        {
            this.lblStatus.Text = "The " + tran + " did not process.  Response was null.";
        }
    }

    /// <summary>
    /// process the selected transaction
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProcess_Click(object sender, EventArgs e)
    {

        string invoice;

        try
        {

            //generate a random invoice number for demo purposes or use the one entered
            if (chkRandom.Checked)
            {
                Random random = new Random();
                invoice = random.Next(0, 999999999).ToString();
            }
            else
                invoice = this.txtInvoice.Text;

            string mid = System.Web.Configuration.WebConfigurationManager.AppSettings["MerchantID"].ToString();
            string pwd = System.Web.Configuration.WebConfigurationManager.AppSettings["HCPassword"].ToString();

            GiftResponse response = new GiftResponse();

            switch (ddlTranType.SelectedValue)
            {
                case "GiftSale":
                    response = GiftHelper.ProcessGiftSale(txtItemAmt.Text, mid, invoice, txtCVV.Text, txtCardNum.Text, pwd, this.chkPartialAuth.Checked);
                    break;
                case "GiftIssue":
                    response = GiftHelper.ProcessGiftIssue(txtItemAmt.Text, mid, invoice, txtCardNum.Text, pwd);
                    break;
                case "GiftVoidIssue":
                    response = GiftHelper.ProcessGiftVoidIssue(txtItemAmt.Text, mid, ViewState["Invoice"].ToString(), txtCardNum.Text, ViewState["RefNo"].ToString(),pwd);
                    break;
                case "GiftBalance":
                    response = GiftHelper.ProcessGiftBalance(mid, invoice, txtCVV.Text, txtCardNum.Text,  pwd);
                    break;
                case "GiftReload":
                    response = GiftHelper.ProcessGiftReload(txtItemAmt.Text, mid, invoice, txtCardNum.Text, this.txtCVV.Text, pwd);
                    break;
                case "GiftVoidReload":
                    response = GiftHelper.ProcessGiftVoidReload(txtItemAmt.Text, mid, invoice, txtCardNum.Text, ViewState["RefNo"].ToString(), this.txtCVV.Text, pwd);
                    break;
                case "GiftVoidSale":
                    response = GiftHelper.ProcessGiftVoidSale(txtItemAmt.Text, mid, invoice, txtCardNum.Text, ViewState["RefNo"].ToString(), pwd);
                    break;
                case "GiftReturn":
                    response = GiftHelper.ProcessGiftReturn(txtItemAmt.Text, mid, invoice, txtCVV.Text, txtCardNum.Text, pwd);
                    break;
                case "GiftVoidReturn":                          
                    response = GiftHelper.ProcessGiftVoidReturn(txtItemAmt.Text, mid, invoice, txtCVV.Text, txtCardNum.Text, pwd, ViewState["RefNo"].ToString());
                    break;
                default:
                    this.lblStatus.Text = "Transaction Type not known.";
                    break;
            }

            //save response fields in case a void follows this.
            ViewState["Invoice"] = response.Invoice;
            ViewState["RefNo"] = response.RefNo;

            //hide or show fields based on tran type
            DisplayFieldsForTranType(ddlTranType.SelectedValue);

            //display response results
            DisplayTransactionResults(response, ddlTranType.SelectedItem.Text);
        }
        catch (Exception ex)
        {
            this.lblStatus.Text = ex.Message;
        }
    }

    protected void chkRandom_CheckedChanged1(object sender, EventArgs e)
    {
        if (chkRandom.Checked)
        {
            txtInvoice.Enabled = false;
            txtInvoice.Text = string.Empty;
        }
        else
        {
            txtInvoice.Enabled = true;
            txtInvoice.Text = "1"; //default to 1
        }
    }

    protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
    {

        //reset fields
        lblStatus.Text = string.Empty;
        lstResults.Items.Clear();
        this.lblResults.Text = ddlTranType.SelectedItem.Text + " Results";

        this.txtItemAmt.Text = string.Empty;
        this.txtInvoice.Text = string.Empty;
        this.txtRefNo.Text = string.Empty;

        this.chkRandom.Visible = true;
        this.txtInvoice.Enabled = false;

        //set appropriate values
        switch (ddlTranType.SelectedValue)
        {
            case "GiftSale":
                {
                    this.txtItemAmt.Text = "0.10";
                    break;
                }
            case "GiftIssue":
                {
                    this.txtItemAmt.Text = "250.00";
                    break;
                }
            case "GiftVoidIssue":
                {
                    this.txtItemAmt.Text = "250.00";
                    this.txtRefNo.Text = ViewState["RefNo"].ToString();
                    this.txtInvoice.Text = ViewState["Invoice"].ToString();
                    break;
                }
            case "GiftReload":
                {
                    this.txtItemAmt.Text = "0.10";
                    break;
                }
            case "GiftVoidReload":
            case "GiftVoidSale":
            case "GiftVoidReturn":
                {
                    this.txtItemAmt.Text = "0.10";
                    this.txtRefNo.Text = ViewState["RefNo"] != null?ViewState["RefNo"].ToString():"";
                    this.txtInvoice.Text = ViewState["Invoice"] != null ? ViewState["Invoice"].ToString(): "";
                    break;
                }
            case "GiftReturn":
                this.txtItemAmt.Text = "0.10";
                break;
        }

        //hide or show fields based on tran type
        DisplayFieldsForTranType(ddlTranType.SelectedValue);
    }
    
    private void DisplayFieldsForTranType(string tranType)
    {
        switch (tranType)
        {
            case "GiftSale":
            case "GiftReturn":
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowCVV", "document.getElementById('divCVV').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowCVVLabel", "document.getElementById('divCVVLabel').setAttribute('class', 'formLabel');", true);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowRefNo", "document.getElementById('divRefNo').setAttribute('class', 'hideMe');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowRefNoLabel", "document.getElementById('divRefNoLabel').setAttribute('class', 'hideMe');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAmount", "document.getElementById('divAmt').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPartialAuth", "document.getElementById('divPartialAuth').setAttribute('class', 'formLine');", true);

                    this.chkRandom.Visible = true;
                    this.txtInvoice.Enabled = false;
                    break;
                }
            case "GiftIssue":
            case "GiftReload":
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideCVV", "document.getElementById('divCVV').setAttribute('class', 'hideMe');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideCVVLabel", "document.getElementById('divCVVLabel').setAttribute('class', 'hideMe');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowRefNo", "document.getElementById('divRefNo').setAttribute('class', 'hideMe');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowRefNoLabel", "document.getElementById('divRefNoLabel').setAttribute('class', 'hideMe');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAmount", "document.getElementById('divAmt').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPartialAuth", "document.getElementById('divPartialAuth').setAttribute('class', 'formLine');", true);

                    this.chkRandom.Visible = true;
                    this.txtInvoice.Enabled = false;
                    break;
                }
            case "GiftVoidIssue":
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideCVV", "document.getElementById('divCVV').setAttribute('class', 'hideMe');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideCVVLabel", "document.getElementById('divCVVLabel').setAttribute('class', 'hideMe');", true);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowRefNo", "document.getElementById('divRefNo').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowRefNoLabel", "document.getElementById('divRefNoLabel').setAttribute('class', 'formLabel');", true);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAmount", "document.getElementById('divAmt').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPartialAuth", "document.getElementById('divPartialAuth').setAttribute('class', 'formLine');", true);

                    this.chkRandom.Visible = false;
                    this.txtInvoice.Enabled = true;
                    break;
                }

            case "GiftVoidReload":
            case "GiftVoidSale":
            case "GiftVoidReturn":
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowCVV", "document.getElementById('divCVV').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowCVVLabel", "document.getElementById('divCVVLabel').setAttribute('class', 'formLabel');", true);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowRefNo", "document.getElementById('divRefNo').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowRefNoLabel", "document.getElementById('divRefNoLabel').setAttribute('class', 'formLabel');", true);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowAmount", "document.getElementById('divAmt').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPartialAuth", "document.getElementById('divPartialAuth').setAttribute('class', 'formLine');", true);

                    this.chkRandom.Visible = false;
                    this.txtInvoice.Enabled = true;
                    break;
                }
            case "GiftBalance":
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowCVV", "document.getElementById('divCVV').setAttribute('class', 'formLine');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowCVVLabel", "document.getElementById('divCVVLabel').setAttribute('class', 'formLabel');", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideRefNo", "document.getElementById('divRefNo').setAttribute('class', 'hideMe');", true);

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideAmount", "document.getElementById('divAmt').setAttribute('class', 'hideMe');", true);                                       
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePartialAuth", "document.getElementById('divPartialAuth').setAttribute('class', 'hideMe');", true);
                break;
        }
    }
}