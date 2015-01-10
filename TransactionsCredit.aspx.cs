using System;
using TransactionService;

/// <summary>
/// this page is strictly to demonstrate the credit 
///     transactions that are available in the background
///     through the Transaction Web Service 
/// </summary>
public partial class TransactionsCredit : System.Web.UI.Page
{
    const string FREQUENCY = "FREQUENCY";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtMerchantID.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["merchantID"].ToString();
            this.txtPassword.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["HCPassword"].ToString();

            if (System.Web.Configuration.WebConfigurationManager.AppSettings["TestToken"] == null)
            {
                this.lblStatus.Text = "TestToken key is not set up in web.config. Set this up using a valid token returned from a PreAuth.  Use the Shopping Cart Page, press 'Submit Payment'.  The token will be returned in the results window on the OrderComplete page.";                
            }
            else
                this.txtToken.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["TestToken"].ToString();
        }
    }

    protected void ProcessPreAuth()
    {
        this.lblResults.Text = "PreAuthToken Results";
        
        TransactionService.TransactionService ts = new TransactionService.TransactionService();
        TransactionService.CreditPreAuthToken preAuthRequest = new TransactionService.CreditPreAuthToken()

         {
             Token = this.txtToken.Text,
             Frequency = "OneTime",
             Amount = Convert.ToDouble(this.txtItemAmt.Text),
             MerchantID = this.txtMerchantID.Text,
             Invoice = this.txtInvoice.Text,
             TerminalName = "",
             OperatorID = "",
             Memo = "PreAuth from Ecomm Test Transaction",
             Address = this.txtAddress.Text,
             Zip = this.txtZip.Text,
             CardHolderName = this.txtName.Text,
             CVV = "123",
             PartialAuth = true,
             LaneID = this.txtLaneID.Text,
         };

        string password = this.txtPassword.Text;
        TransactionService.CreditResponse response = ts.CreditPreAuthToken(preAuthRequest, password);

        if (response != null)
        {
            DisplayTransactionResults(response);

            if (response.Status == "Approved")
            {
                this.lblStatus.Text = "The PreAuth has been approved";
                ViewState["AuthCode"] = response.AuthCode;
                ViewState["AcqRefData"] = response.AcqRefData;
                ViewState["RefNo"] = response.RefNo;
                ViewState["ProcessData"] = response.ProcessData;
            }
            else
            {
                ViewState["AuthCode"] = null;
                ViewState["AcqRefData"] = null;
                ViewState["RefNo"] = null;
                ViewState["ProcessData"] = null;
                this.lblStatus.Text = "The status of the preAuth is: " + response.Status;
            }
        }
        else
        {
            this.lblStatus.Text = "The PreAuth did not process.";
        }
    }

    protected void ProcessPreAuthCapture()
    {
        this.lblResults.Text = "PreAuthCaptureToken Results";
        if ((ViewState["AuthCode"] == null) || (ViewState["AcqRefData"] == null) || ViewState["RefNo"] == null)
        {
            this.lblStatus.Text = "Unable to execute PreAuthCapture without first running a successful PreAuth.";
            return;
        }

        TransactionService.TransactionService ts = new TransactionService.TransactionService();
        TransactionService.CreditPreAuthCaptureToken request = new TransactionService.CreditPreAuthCaptureToken()
        {
            Token = this.txtToken.Text,
            Frequency = "OneTime",
            PurchaseAmount = Convert.ToDouble(this.txtItemAmt.Text),
            MerchantID = this.txtMerchantID.Text,
            Invoice = this.txtInvoice.Text,
            TerminalName = "",
            OperatorID = "",
            Memo = "PreAuth from Ecomm Test Transaction",
            AuthCode = ViewState["AuthCode"].ToString(),
            AcqRefData = ViewState["AcqRefData"].ToString(),
            CardHolderName = this.txtName.Text,
            CustomerCode = "ABC",
            TaxAmount = .5,
            LaneID = this.txtLaneID.Text,
        };

        string password = this.txtPassword.Text;
        TransactionService.CreditResponse response = ts.CreditPreAuthCaptureToken(request, password);

        if (response != null)
        {
            DisplayTransactionResults(response);

            if (response.Status == "Approved")
            {
                this.lblStatus.Text = "The card has been charged";
                ViewState["AuthCode"] = response.AuthCode;
                ViewState["AcqRefData"] = response.AcqRefData;
                ViewState["RefNo"] = response.RefNo;
                ViewState["ProcessData"] = response.ProcessData;
            }
            else
            {
                this.lblStatus.Text = "The status of the capture is: " + response.Status;
                ViewState["AuthCode"] = null;
                ViewState["AcqRefData"] = null;
                ViewState["RefNo"] = null;
                ViewState["ProcessData"] = null;
            }
        }
        else
        {
            this.lblStatus.Text = "The payment was not captured";
        }
    }

    protected void ProcessVoid()
    {
        this.lblResults.Text = "VoidSaleToken Results";

        if (ViewState["AuthCode"] == null)
        {
            this.lblStatus.Text = "Unable to execute VoidSale without first running a successful PreAuth or Sale.";
            this.lstResults.Items.Clear();
            return;
        }

        TransactionService.TransactionService ts = new TransactionService.TransactionService();
        TransactionService.CreditVoidSaleToken request = new TransactionService.CreditVoidSaleToken()
        {

            Token = this.txtToken.Text,
            Frequency = "OneTime",
            PurchaseAmount = Convert.ToDouble(this.txtItemAmt.Text),
            MerchantID = this.txtMerchantID.Text,
            Invoice = this.txtInvoice.Text,
            TerminalName = "",
            OperatorID = "",
            Memo = "Void from Ecomm Test Transaction",
            AuthCode = ViewState["AuthCode"].ToString(),
            CardHolderName = this.txtName.Text,
            RefNo = ViewState["RefNo"].ToString(),
            LaneID = this.txtLaneID.Text,
        };

        string password = this.txtPassword.Text;

        TransactionService.CreditResponse response = ts.CreditVoidSaleToken(request, password);

        if (response != null)
        {
            this.DisplayTransactionResults(response);

            if (response.Status == "Approved")
                this.lblStatus.Text = "The payment has been voided.";
            else
            {
                this.lblStatus.Text = "The status of the void is: " + response.Status;
            }

            ViewState["AuthCode"] = null;
            ViewState["AcqRefData"] = null;
            ViewState["RefNo"] = null;
        }
        else
        {
            this.lblStatus.Text = "The void was not processed.";
        }
    }

    protected void ProcessSale()
    {
        this.lblResults.Text = "SaleToken Results";

        TransactionService.TransactionService ts = new TransactionService.TransactionService();
        TransactionService.CreditSaleToken request = new TransactionService.CreditSaleToken()
        {
            Token = this.txtToken.Text,
            Frequency = "OneTime",
            PurchaseAmount = Convert.ToDouble(this.txtItemAmt.Text),
            MerchantID = this.txtMerchantID.Text,
            Invoice = this.txtInvoice.Text,
            TerminalName = "",
            OperatorID = "",
            Memo = "Sale from Ecomm Test Transaction",
            Address = this.txtAddress.Text,
            Zip = this.txtZip.Text,
            CardHolderName = this.txtName.Text,
            CVV = "123",
            CustomerCode = "ABC",
            TaxAmount = .5,
            PartialAuth = true,
            LaneID = this.txtLaneID.Text,
           
        };

        string password = this.txtPassword.Text;
        TransactionService.CreditResponse response = ts.CreditSaleToken(request, password);

        if (response != null)
        {
            this.DisplayTransactionResults(response);

            if (response.Status == "Approved")
            {
                ViewState["AuthCode"] = response.AuthCode;
                ViewState["AcqRefData"] = response.AcqRefData;
                ViewState["RefNo"] = response.RefNo;
                ViewState["ProcessData"] = response.ProcessData;

                this.lblStatus.Text = "The card has been charged";
            }
            else
            {
                ViewState["AuthCode"] = null;
                ViewState["AcqRefData"] = null;
                ViewState["RefNo"] = null;
                ViewState["ProcessData"] = null;

                this.lblStatus.Text = "The status of the sale is: " + response.Status;
            }
        }
        else
        {
            this.lblStatus.Text = "The sale did not process.";
        }
    }

    protected void ProcessReturn()
    {
        if ((ViewState["AuthCode"] == null) || (ViewState["RefNo"] == null))
        {
            this.lblStatus.Text = "Unable to execute Return without first running a successful PreAuth or Sale.";
            this.lstResults.Items.Clear();
            return;
        }

        this.lblResults.Text = "ReturnToken Results";

        TransactionService.TransactionService ts = new TransactionService.TransactionService();
        TransactionService.CreditReturnToken request = new TransactionService.CreditReturnToken()
        {
            Token = this.txtToken.Text,
            Frequency = "OneTime",
            PurchaseAmount = Convert.ToDouble(this.txtItemAmt.Text),
            MerchantID = this.txtMerchantID.Text,
            Invoice = this.txtInvoice.Text,
            TerminalName = "",
            OperatorID = "",
            Memo = "Return from Ecomm Test Transaction",
            CardHolderName = this.txtName.Text,
            LaneID = this.txtLaneID.Text,
        };

        string password = this.txtPassword.Text;
        TransactionService.CreditResponse response = ts.CreditReturnToken(request, password);

        if (response != null)
        {
            DisplayTransactionResults(response);

            if (response.Status == "Approved")
                this.lblStatus.Text = "Return Successful";
            else
                this.lblStatus.Text = "The status of the Return is: " + response.Status;
        }
        else
        {
            this.lblStatus.Text = "The Return did not process.";
        }
    }

    protected void ProcessVoidReturn()
    {
        if ((ViewState["AuthCode"] == null) || (ViewState["RefNo"] == null))
        {
            this.lstResults.Items.Clear();
            this.lblStatus.Text = "Unable to execute Void Return without first running a successful PreAuthCapture or Sale.";
            return;
        }

        this.lblResults.Text = "VoidReturnToken Results";

        TransactionService.TransactionService ts = new TransactionService.TransactionService();
        TransactionService.CreditVoidReturnToken request = new TransactionService.CreditVoidReturnToken()
        {
            Token = this.txtToken.Text,
            Frequency = "OneTime",
            PurchaseAmount = Convert.ToDouble(this.txtItemAmt.Text),
            MerchantID = this.txtMerchantID.Text,
            Invoice = this.txtInvoice.Text,
            TerminalName = "",
            OperatorID = "",
            Memo = "VoidReturn from Ecomm Test Transaction",
            CardHolderName = this.txtName.Text,
            AuthCode = ViewState["AuthCode"].ToString(),
            RefNo = ViewState["RefNo"].ToString(),
            LaneID = this.txtLaneID.Text,

        };

        string password = this.txtPassword.Text;
        TransactionService.CreditResponse response = ts.CreditVoidReturnToken(request, password);

        if (response != null)
        {
            this.DisplayTransactionResults(response);

            if (response.Status == "Approved")
                this.lblStatus.Text = "Void Return Successful";
            else
                this.lblStatus.Text = "The status of the Void Return is: " + response.Status + "<br/> Try: 1. Sale or PreAuthCapture   2: Return   3. Void Return";

            ViewState["AuthCode"] = null;
            ViewState["AcqRefData"] = null;
            ViewState["RefNo"] = null;
        }
        else
        {
            this.lblStatus.Text = "The Void Return did not process. Try: 1. Sale or PreAuthCapture  2: Return  3. Void Return";
        }
    }

    protected void ProcessAdjust()
    {
        TransactionService.TransactionService preAuth = new TransactionService.TransactionService();
        TransactionService.TransactionService targetAdjust = new TransactionService.TransactionService();
        CreditResponse preAuthResponse;
        CreditResponse adjustResponse;
        string password = this.txtPassword.Text;

        CreditPreAuthCaptureToken preAuthCaptureToken = new CreditPreAuthCaptureToken()
        {
            AcqRefData = "KbMCC1757080329",
            AuthCode = "123456",
            AuthorizeAmount = 5,
            GratuityAmount = 2,
            TaxAmount = 1,
            Memo = "CreditPreAuthCaptureToken",
            Token = this.txtToken.Text,
            Frequency = "OneTime",
            PurchaseAmount = 9,
            MerchantID = this.txtMerchantID.Text,
            Invoice = "54321",
            TerminalName = "",
            OperatorID = "",
            CardHolderName = "John Smith",
            LaneID = this.txtLaneID.Text,
        };

        preAuthResponse = preAuth.CreditPreAuthCaptureToken(preAuthCaptureToken, password);

        CreditAdjustToken adjustToken = new CreditAdjustToken()
        {
            Memo = "CreditAdjustToken Test",
            Token = preAuthResponse.Token,
            Frequency = "OneTime",
            PurchaseAmount = 8.50,
            MerchantID = this.txtMerchantID.Text,
            RefNo = preAuthResponse.RefNo,
            Invoice = preAuthResponse.Invoice,
            OperatorID = "test",
            AuthCode = preAuthResponse.AuthCode,
            GratuityAmount = 1.50,
            LaneID = this.txtLaneID.Text,
        };

        adjustResponse = targetAdjust.CreditAdjustToken(adjustToken, password);

        if (adjustResponse != null)
        {
            DisplayTransactionResults(adjustResponse );

            if (adjustResponse.Status == "Approved")
                this.lblStatus.Text = "Adjust Successful -- Adjusted amount from $9 to $8.50 and gratuity from $2 to $1.50";
            else
                this.lblStatus.Text = "The status of the Adjust is: " + adjustResponse.Status;

            ViewState["AuthCode"] = null;
            ViewState["AcqRefData"] = null;
            ViewState["RefNo"] = null;
        }
        else
        {
            this.lblStatus.Text = "The Adjust did not process.";
        }
    }

    protected void ProcessReversal()
    {
        if ((ViewState["AuthCode"] == null) || (ViewState["RefNo"] == null) || (ViewState["AcqRefData"] == null) || (ViewState["ProcessData"] == null))
        {
            this.lstResults.Items.Clear();
            this.lblStatus.Text = "Unable to execute Reversal without first running a successful PreAuth, PreAuthCapture or Sale.";
            return;
        }

        this.lblResults.Text = "ReversalToken Results";

        TransactionService.TransactionService ts = new TransactionService.TransactionService();
        TransactionService.CreditReversalToken request = new TransactionService.CreditReversalToken()
        {
            Token = this.txtToken.Text,
            Frequency = "OneTime",
            PurchaseAmount = Convert.ToDouble(this.txtItemAmt.Text),
            MerchantID = this.txtMerchantID.Text,
            Invoice = this.txtInvoice.Text,
            TerminalName = "",
            OperatorID = "",
            Memo = "Reversal from Ecomm Test Transaction",
            CardHolderName = this.txtName.Text,
            AuthCode = ViewState["AuthCode"].ToString(),
            RefNo = ViewState["RefNo"].ToString(),
            AcqRefData = ViewState["AcqRefData"].ToString(),
            ProcessData = ViewState["ProcessData"].ToString(),
            LaneID = this.txtLaneID.Text,
        };

        string password = this.txtPassword.Text;
        TransactionService.CreditResponse response = ts.CreditReversalToken(request, password);

        if (response != null)
        {
            DisplayTransactionResults(response);

            if (response.Status == "Approved" && response.Message == "REVERSED")
                this.lblStatus.Text = "Reversal Successful";
            else
                this.lblStatus.Text = "The status/Message of the Reversal is: " + response.Status + "/" + response.Message + "<br/> Try: 1. Sale or PreAuthCapture   2: Reversal";

            ViewState["AuthCode"] = null;
            ViewState["AcqRefData"] = null;
            ViewState["RefNo"] = null;
        }
        else
        {
            this.lblStatus.Text = "The Void Return did not process. Try: 1. Sale or PreAuthCapture  2: Return  3. Void Return";
        }
     }

    protected void ProcessBatchClear()
    {
        this.lblResults.Text = "BatchClear Results";
        TransactionService.TransactionService ts = new TransactionService.TransactionService();
        TransactionService.BatchClear preAuthRequest = new TransactionService.BatchClear()

        {
            MerchantID = this.txtMerchantID.Text,
            TerminalName = "",
            OperatorID = "",
            Memo = "BatchClear from Ecomm Test Transaction",             
        };
        
        string password = this.txtPassword.Text;
        TransactionService.BatchClearResponse response = ts.BatchClear(preAuthRequest, password);

        if (response != null)
        {
            if (response.Status == "Approved")
            {
                this.lblStatus.Text = "BatchClear has been approved";                
            }
            else
            {                
                this.lblStatus.Text = "The status of the BatchClear is: " + response.Status;
            }
        }
        else
        {
            this.lblStatus.Text = "The BatchClear did not process.";
        }
    }

    /// <summary>
    /// display the creditResponse fields in lstResults for a transaction call
    /// </summary>
    /// <param name="response"></param>
    private void DisplayTransactionResults(TransactionService.CreditResponse response)
    {

        this.lstResults.Items.Clear();
        this.lstResults.Items.Add("Status: " + response.Status);
        this.lstResults.Items.Add("Message: " + response.Message);
        this.lstResults.Items.Add("AVSResult: " + response.AVSResult);
        this.lstResults.Items.Add("CVVResult: " + response.CVVResult);
        this.lstResults.Items.Add("AuthCode: " + response.AuthCode);
        this.lstResults.Items.Add("BatchNo: " + response.BatchNo);
        this.lstResults.Items.Add("RefNo: " + response.RefNo);
        this.lstResults.Items.Add("Invoice: " + response.Invoice);
        this.lstResults.Items.Add("AcqRefData: " + response.AcqRefData);
        this.lstResults.Items.Add("CardType: " + response.CardType);
        this.lstResults.Items.Add("AuthorizeAmount: " + response.AuthorizeAmount);
        this.lstResults.Items.Add("PurchaseAmount: " + response.PurchaseAmount);
        this.lstResults.Items.Add("GratuityAmount: " + response.GratuityAmount);
        this.lstResults.Items.Add("Account: " + response.Account);
        this.lstResults.Items.Add("ProcessData: " + response.ProcessData);
        this.lstResults.Items.Add("Token: " + response.Token); 
    }

    protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblStatus.Text = String.Empty;
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        switch (ddlTranType.SelectedValue)
        {
            case "PreAuthToken":
                ProcessPreAuth();
                break;
            case "PreAuthCaptureToken":
                ProcessPreAuthCapture();
                break;                       
            case "SaleToken":
                ProcessSale();
                break;
            case "VoidToken":
                ProcessVoid();
                break;
            case "ReturnToken":
                ProcessReturn();
                break;
            case "VoidReturnToken":
                ProcessVoidReturn();
                break;
            case "AdjustToken":
                ProcessAdjust();
                break;          
            case "ReversalToken":
                ProcessReversal();
                break;
            case "BatchClear":
                ProcessBatchClear();
                break;                
        }
    }
}