HostedCheckout.CSharp
====================

This demo site is hosted at:  http://durangopizzahc.azurewebsites.net

Visual Studio asp.net website application that shows many features of our Hosted Checkout platform.

>There are 3 steps to process a payment with Mercury's Hosted Checkout platform.

##Step 1: Initialize Payment


###Process: Initialize Payment Transaction

```

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
hcRequest.ButtonBackgroundColor = this.txtButtonBgColor.Text;
hcRequest.JCB = this.rblJCB.SelectedValue;
hcRequest.Diners = this.rblDiners.SelectedValue;
hcRequest.AVSFields = this.rblAVSFields.SelectedValue;
hcRequest.CVV = this.rblCVV.SelectedValue;
hcRequest.PageTimeoutDuration = this.ddlPageTimeoutDuration.SelectedValue;
hcRequest.PageTimeoutIndicator = this.rblPageTimeoutIndicator.SelectedValue;
hcRequest.TotalAmountBackgroundColor = this.txtTotalAmtBgColor.Text;
hcRequest.SecurityLogo = this.rblSecurityLogo.SelectedItem.Text;
hcRequest.LaneID = this.txtLaneID.Text;

HCService.HCService hcWS = new HCService.HCService();
HCService.InitPaymentResponse response = new HCService.InitPaymentResponse();
response = hcWS.InitializePayment(hcRequest);
```

###Parse: Response

```
if (response != null)
{
  if (response.ResponseCode == 0)  //success
  {
    var paymentId = response.PaymentID;
  }
}

```

##Step 2: Display HostedCheckout

>Display the HostedCheckout Web page

There are many ways to do this, one of them is to write a form that submits itself when the onload event of the body is fired.

```
System.Web.HttpContext.Current.Response.Clear();
System.Web.HttpContext.Current.Response.Write("<html><head>");
System.Web.HttpContext.Current.Response.Write("</head><body onload=\"document.frmCheckout.submit()\">");
System.Web.HttpContext.Current.Response.Write("<form name=\"frmCheckout\" method=\"Post\" action=\"" + hostedCheckoutURL + "\" >");
System.Web.HttpContext.Current.Response.Write("<input name=\"PaymentID\" type=\"hidden\" value=\"" + paymentID + "\">");
System.Web.HttpContext.Current.Response.Write("</form>");
System.Web.HttpContext.Current.Response.Write("</body></html>");
System.Web.HttpContext.Current.Response.End();
```

##Step 3: Verify Payment

###Process: Verify Transaction

```
HCService.PaymentInfoRequest hcVerifyRequest = new HCService.PaymentInfoRequest();

hcVerifyRequest.MerchantID = Session["MerchantID"].ToString();
hcVerifyRequest.Password = Session["PW"].ToString();
hcVerifyRequest.PaymentID = paymentID;

HCService.HCService hcWS = new HCService.HCService();
HCService.PaymentInfoResponse response = new HCService.PaymentInfoResponse();
response = hcWS.VerifyPayment(hcVerifyRequest);
```

###Parse: Response

>Approved transactions will have a CmdStatus equal to "Approved".

```
if (response != null)
{
  if (response.ResponseCode == 0)
  {
    var authCode = response.AuthCode;
    var invoice = response.Invoice;
    var refNo = response.RefNo;
    var acqRefData = response.AcqRefData;
    var amount = response.Amount;
    var taxAmount = response.TaxAmount;
    var token = response.Token;
  }
}
```

###©2015 Mercury Payment Systems, LLC - all rights reserved.

Disclaimer:
This software and all specifications and documentation contained herein or provided to you hereunder (the "Software") are provided free of charge strictly on an "AS IS" basis. No representations or warranties are expressed or implied, including, but not limited to, warranties of suitability, quality, merchantability, or fitness for a particular purpose (irrespective of any course of dealing, custom or usage of trade), and all such warranties are expressly and specifically disclaimed. Mercury Payment Systems shall have no liability or responsibility to you nor any other person or entity with respect to any liability, loss, or damage, including lost profits whether foreseeable or not, or other obligation for any cause whatsoever, caused or alleged to be caused directly or indirectly by the Software. Use of the Software signifies agreement with this disclaimer notice.

![Analytics](https://ga-beacon.appspot.com/UA-60858025-19/HostedCheckout.CSharp/readme?pixel)
