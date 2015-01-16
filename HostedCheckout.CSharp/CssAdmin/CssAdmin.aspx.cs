using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Globalization;

public partial class CssAdmin_CssAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtResponseMsg.Text = "";

        if (!IsPostBack)
        {
            string cssURL = System.Web.Configuration.WebConfigurationManager.AppSettings["CssURL"].ToString();
            if (!String.IsNullOrEmpty(cssURL))
            {
                string msg = string.Empty;
                if (IsValidCssUrl(cssURL, out msg))
                    this.txtCSS.Text = ReadCss(cssURL);
                else
                    this.txtCSS.Text = msg;
            }

            this.txtMerchantID.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["merchantID"].ToString();
            this.txtPassword.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["HCPassword"].ToString();
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtResponseMsg.Text = "";
            HCService.CssUploadRequest request = new HCService.CssUploadRequest();
            request.MerchantID = this.txtMerchantID.Text;
            request.Password = this.txtPassword.Text;            
            request.Css = this.txtCSS.Text;

            //Call the web service to initialize the UploadCSS request.
            HCService.HCService hcWS = new HCService.HCService();
            HCService.CssAdminResponse response = new HCService.CssAdminResponse();
            response = hcWS.UploadCSS(request);

            if (response != null)
            {
                this.txtCode.Text = response.ResponseCode.ToString();
                this.txtResponseMsg.Text = HttpUtility.HtmlDecode(response.Message);
            }
            else
                //something seriously wrong. probably couldn't connect to the web service at all
                this.txtResponseMsg.Text = "Response from UploadCss was Null";
        }
        catch (Exception)
        { }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {

        try
        {
            this.txtResponseMsg.Text = "";
            HCService.CssDownloadRequest request = new HCService.CssDownloadRequest();
            request.MerchantID = this.txtMerchantID.Text;
            request.Password = this.txtPassword.Text;
            request.Formatting = "on";

            //Call the web service to initialize the DownloadCSS request.
            HCService.HCService hcWS = new HCService.HCService();
            HCService.CssDownloadResponse response = new HCService.CssDownloadResponse();
            response = hcWS.DownloadCSS(request);

            if (response != null)
            {
                this.txtCode.Text = response.ResponseCode.ToString();
                this.txtResponseMsg.Text = response.Message;
                this.txtCSS.Text = HttpUtility.HtmlDecode(response.Css);
            }
            else
                //something seriously wrong. probably couldn't connect to the web service at all
                this.txtResponseMsg.Text = "Response from DownloadCss was Null";
        }
        catch (Exception)
        { }
    
    }

    protected void btnDownloadUnFormatted_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtResponseMsg.Text = "";
            HCService.CssDownloadRequest request = new HCService.CssDownloadRequest();
            request.MerchantID = this.txtMerchantID.Text;
            request.Password = this.txtPassword.Text;
            request.Formatting = "off";

            //Call the web service to initialize the DownloadCSS request.
            HCService.HCService hcWS = new HCService.HCService();
            HCService.CssDownloadResponse response = new HCService.CssDownloadResponse();
            response = hcWS.DownloadCSS(request);

            if (response != null)
            {
                this.txtCode.Text = response.ResponseCode.ToString();
                this.txtResponseMsg.Text = response.Message;
                this.txtCSS.Text = HttpUtility.HtmlDecode(response.Css);
            }
            else
                //something seriously wrong. probably couldn't connect to the web service at all
                this.txtResponseMsg.Text = "Response from DownloadCss was Null";
        }
        catch (Exception)
        { }
    
    }



    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtResponseMsg.Text = "";
            HCService.CssRemoveRequest request = new HCService.CssRemoveRequest();
            request.MerchantID = this.txtMerchantID.Text;
            request.Password = this.txtPassword.Text;
           

            //Call the web service to initialize the DownloadCSS request.
            HCService.HCService hcWS = new HCService.HCService();
            HCService.CssAdminResponse response = new HCService.CssAdminResponse();
            response = hcWS.RemoveCSS(request);

            if (response != null)
            {
                this.txtCode.Text = response.ResponseCode.ToString();
                this.txtResponseMsg.Text = response.Message;
                this.txtCSS.Text = String.Empty;
            }
            else
                //something seriously wrong. probably couldn't connect to the web service at all
                this.txtResponseMsg.Text = "Response from RemoveCss was Null";
        }
        catch (Exception)
        { }
    }

    public static bool IsValidCssUrl(string CssURL, out string msg)
    {
        msg = "Valid CSS";

        HttpWebResponse response;

        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            bool retVal = false;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(CssURL);
            req.Method = "HEAD";
            req.Headers.Add("Accept-Encoding: gzip,deflate,sdch");

            response = (HttpWebResponse)req.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.ContentType.StartsWith("text/css", true, CultureInfo.InvariantCulture))
                {
                    retVal = true;

                }
                else
                    msg = "'" + CssURL + "' is not a valid CSS.";
            }

            response.Close();
            return retVal;

        }
        catch (Exception ex)
        {
            msg = ex.Message;
            return false;
        }

    }

    private string ReadCss(string cssUrl)
    {
        string rawCss;
        try
        {
            if (String.IsNullOrEmpty(cssUrl))
                return string.Empty;

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(cssUrl);
            req.Method = "GET";
            req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();            

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                rawCss = reader.ReadToEnd();
            }

            response.Close();
            return rawCss;
        }
        catch (Exception ex)
        {
            return "Unable to Read CSS from URL '" + cssUrl + "'.  Exception:" + ex.Message;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtCSS.Text = string.Empty;
        this.txtResponseMsg.Text = string.Empty;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        string cssURL = System.Web.Configuration.WebConfigurationManager.AppSettings["CssURL"].ToString();
        if (!String.IsNullOrEmpty(cssURL))
        {
            string msg = string.Empty;
            if (IsValidCssUrl(cssURL, out msg))
                this.txtCSS.Text = ReadCss(cssURL);
            else
                this.txtCSS.Text = msg;
        }
        this.txtResponseMsg.Text = string.Empty;

    }
}