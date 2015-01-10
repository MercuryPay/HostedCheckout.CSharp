using System;

/// <summary>
/// Used when the user is returning from a HostedCheckout payment page.
/// Specified by the ReturnURL field in initialize card info.  
///   Only needed when the HostedCheckout session times out.  
///   One could use the ProcessCompleteURL page as well.
/// </summary>
public partial class ReturnPageIFrame : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cardID, returnCode, returnMessage;

        if (Session["ReturnMethod"] == null)
            return;

        //get the return code.
        if (Session["ReturnMethod"].ToString() == "post")
        {
            cardID = Request.Form["CardID"];
            returnCode = Request.Form["ReturnCode"];
            returnMessage = Request.Form["ReturnMessage"];
        }
        else
        {
            cardID = Request.QueryString["CardID"];
            returnCode = Request.QueryString["ReturnCode"];
            returnMessage = "ReturnCode=" + returnCode + "." + " ReturnMethod=GET";
        }

        if (!String.IsNullOrEmpty(returnCode))
        {
            Session["ReturnCode"] = returnCode;
            this.lblStatus.Text = returnMessage;
        }

        Session[Constants.PaymentIDIFrameEComm] = null;
    }
}