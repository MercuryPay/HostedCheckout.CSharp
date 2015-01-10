<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SplitScreenIFramePOS.aspx.cs"
    Inherits="ShoppingCartIFramePOS" MaintainScrollPositionOnPostback="true" StylesheetTheme="POS"
    Theme="SkinFile" %>

<%--
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            height: 33px;
        }
        .style2
        {
            height: 33px;
            width: 17px;
        }
        .style3
        {
            width: 181px;
            text-align: right;
        }
        .style4
        {
            height: 33px;
            width: 181px;
        }
    </style>
</asp:Content>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Durango Pizza</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/demo.js" type="text/javascript"></script>
</head>
<%--<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">--%>
<body>
    <form id="frmCheckout" runat="server">
    <div style="margin-left: 270px">
        <asp:Label runat="server" ID="lblError" ForeColor="Red" />
        <br />
    </div>
    <div id="divOrder" runat="server" style="float: left; width: 511px; border-right: 1px solid red">
        <div style="margin-left: 10px;">
            <table width="90%" cellpadding="0" cellspacing="0">
                <tr style="color: Black; font-weight: bold; font-size: medium">
                    <td>
                        Order Summary
                        <br />
                        <br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="height: 50px;">
                    <td style="width: 400px; text-align: right; padding-right: 5px;">
                        <asp:Label runat="server" Text="1 Pepperoni Pizza: " SkinID="LabelLarge"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox Text="7.50" ID="txtItem1Amt" runat="server" Width="80px" SkinID="TextLarge"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 50px;">
                    <td style="width: 400px; text-align: right; padding-right: 5px;">
                        <asp:Label runat="server" Text="Tax:" SkinID="LabelLarge"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox Text="0.64" ID="txtTaxAmt" runat="server" Width="80px" SkinID="TextLarge"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr style="font-weight: bold; color: black; font-size: medium">
                    <td>
                        Total
                    </td>
                    <td>
                        <asp:Label Text="8.64" Font-Bold="true" runat="server" ID="txtTotalAmt" Font-Size="X-Large"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnUpdateAmt" OnClick="btnUpdateAmt_Click"
                            Text="Update" SkinID="SmallButton" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnCheckout" Text="Checkout" OnClick="btnCheckOut_Click"
                            Width="170px" />&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <br />
        &nbsp;
        <div style="text-align: left; font-size: medium; font-weight: normal; width: 50%; margin-left: 10px">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divAPIFields', 'aToggle'); return false;"
                ID="aToggle" Text="Show Initialize Fields"></asp:LinkButton>
        </div>
        <br />
        <%--API Fields--%>
        <div id="divAPIFields" style="display: none">
            <br />
            <asp:Label ID="lblAPIFields" runat="server" Text="API Fields for InitializePayment Web Method"
                Font-Bold="true" Font-Italic="true"></asp:Label>
            <br />
            <br />
            <table style="width: 90%; table-layout: fixed; margin-right: 131px;" cellpadding="2px">
                <tr>
                    <td style="text-align: right" class="style3">
                        <asp:Label runat="server" ID="lblName" Text="Cardholder Name:"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" Text="John Jones"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style3">
                        <asp:Label ID="lblAddress" runat="server" Text="AVS Address:"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" Text="4 Corporate Square"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style3">
                        <asp:Label Text="AVS Zip:" ID="lblZip" runat="server"></asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtZip" Text="30329" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label11" runat="server" Text="Return Method"></asp:Label>&nbsp
                    </td>
                    <td class="style1">
                        <asp:DropDownList ID="ddlReturnMethod" runat="server" ToolTip="Indicates if the user will be sent back to this ecommerce site using an HTTP GET or HTTP FORM POST request.">
                            <asp:ListItem Value="post" Text="POST" Selected="True" />
                            <asp:ListItem Value="get" Text="GET" Selected="false" />
                            <asp:ListItem Value="" Text="" Selected="false" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label6" runat="server" Text="Transaction Type"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblTranType" runat="server" RepeatDirection="Horizontal"
                            onclick="CheckForVoiceAuth();">
                            <asp:ListItem Text="Sale" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="PreAuth"></asp:ListItem>
                            <asp:ListItem Text="VoiceAuth"></asp:ListItem>
                            <asp:ListItem Text="Return"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style3">
                        <div id="divVoiceAuth1" style="display: none">
                            <asp:Label ID="lblVoiceAuthCode" runat="server" Text="VoiceAuth Code"></asp:Label>
                        </div>
                    </td>
                    <td>
                        <div id="divVoiceAuth2" style="display: none">
                            <asp:TextBox ID="txtVoiceAuthCode" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label8" runat="server" Text="Display Style"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblDisplayStyle" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Mercury" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Custom"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label7" runat="server" Text="Frequency"></asp:Label>&nbsp
                    </td>
                    <td class="style2">
                        <asp:RadioButtonList ID="rblFrequency" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="OneTime" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Recurring"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblMerchantID" Text="Merchant ID" runat="server"></asp:Label><br />
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtMerchantID" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblCustomeCode" Text="Customer Code" runat="server"></asp:Label><br />
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCustomerCode" runat="server" Text="CUSTCODE123"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblPassword" Text="Password" runat="server"></asp:Label><br />
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        Default Swipe<br />
                    </td>
                    <td class="style2">
                        <asp:RadioButtonList ID="rblDefaultSwipe" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Swipe</asp:ListItem>
                            <asp:ListItem Selected="True">Manual</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label1" Text="Font Size for Checkout Page" runat="server"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:RadioButtonList ID="rblFontSize" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Small</asp:ListItem>
                            <asp:ListItem Selected="True">Medium</asp:ListItem>
                            <asp:ListItem>Large</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label2" Text="Font Color for Checkout Page" runat="server"></asp:Label>&nbsp
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="ddlMultiColor" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="style3">
                        <asp:Label ID="Label3" Text="Font Family for Checkout Page" runat="server"></asp:Label>&nbsp
                    </td>
                    <td class="style2">
                        <asp:RadioButtonList ID="rblFontFamily" runat="server" onclick="CheckForOther('rblFontFamily' ,'divFontFam');">
                            <asp:ListItem Value="FontFamily1">FontFamily1 (Arial, Verdana, Geneva, Helvetica, sans-serif)</asp:ListItem>
                            <asp:ListItem Value="FontFamily2">FontFamily2 (Georgia, Times New Roman, Times, serif)</asp:ListItem>
                            <asp:ListItem Value="FontFamily3">FontFamily3 (Courier New, Courier, monospace)</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:RadioButtonList>
                        <div id="divFontFam" style="display: none">
                            <asp:TextBox ID="txtFontFamily" runat="server" Width="455px"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="style3">
                        <asp:Label ID="Label5" Text=" " runat="server"></asp:Label><br />
                        <asp:Label ID="Label4" Text="Background Color for Checkout Page" runat="server"></asp:Label>&nbsp
                    </td>
                    <td class="style2">
                        <br />
                        <asp:TextBox ID="txtBGColor" runat="server" Width="50%" Text="white"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblKeypad" Text="Keypad"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:RadioButtonList ID="rblKeypad" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>On</asp:ListItem>
                            <asp:ListItem Selected="True">Off</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblAVSFields" Text="AVSFields"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblAVSFields" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">Off</asp:ListItem>
                            <asp:ListItem>Both</asp:ListItem>
                            <asp:ListItem>Zip</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        PartialAuth
                    </td>
                    <td class="style2">
                        <asp:RadioButtonList ID="rblPartialAuth" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>On</asp:ListItem>
                            <asp:ListItem Selected="True">Off</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblPaymentButtonText" runat="server" Text="Process Button Text"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPaymentButtonText" runat="server" Text="Process"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblOperatorID" Text="Operator ID" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtOpID" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblTerminalID" Text="Terminal Name" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTermName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblBtnBgColor" Text="Button Background Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBtnBgColor" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblBtnFgColor" Text="Button Text Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBtnTextColor" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label12" Text="Card Entry Method" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardEntryMethod" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                        <asp:Label ID="Label13" Text="(Valid values are 'swipe', 'manual', 'both', '')" runat="server"></asp:Label>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label14" Text="Submit Button Default Image" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubmitBtnDefault" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label15" Text="Submit Button Hover Image" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubmitBtnHover" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <br />
                        <asp:Button runat="server" ID="Button1" Text="Checkout" OnClick="btnCheckOut_Click"
                            Width="170px" />&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        &nbsp; &nbsp; Testing Notes: For PartialAuth, Use 23.54 for PurchaseAmount <br />&nbsp;&nbsp;&nbsp;with Visa=4005550000000480
    </div>
   <div style="width: 511px; margin: 0 0 0 0; overflow: hidden; border-right: 1px solid red">
        <iframe id='ifrm' src="http://localhost:49510/Website/CheckoutPOSIFrame.aspx" height="420px"
            width="590px" scrolling="no" frameborder="0" runat="server" style="text-align: center; margin-left: -65px;
            display: none; z-index: -1">Sorry, your browser doesn’t support iframes.</iframe>
        &nbsp;
    </div>
    <%-- <div style="height: 580px; width: 600px; margin-left: 150px; margin-top: 200px;">
        <iframe id='ifrm' src="http://localhost:49510/website/checkoutPosIFrame.aspx" height="580px"
            width="600px" scrolling="no" frameborder="0" runat="server" style="text-align: center;
            display: none;">Sorry, your browser doesn’t support iframes.</iframe>
        &nbsp;
    </div>--%>

    <div id="divThanks" runat="server" style="text-align: center;">
        <asp:Label ID="Label9" runat="server" Text="Thank you for choosing Durango Pizza!"
            Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <br />
        <%--<asp:Button ID="btnCapture" runat="server" Text="PreAuthCapture" OnClick="btnCapture_Click" /><br />
            <asp:Label ID="lblCaptureResult" runat="server" Text="" Font-Size="Medium"></asp:Label>--%>
    </div>
    <div style="margin-left: 252px; margin-top: 0px">
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="&lt;&lt; Back"
            Font-Size="Large" />
        &nbsp;
        <asp:Button ID="btnContinue" runat="server" Text="Next >>" OnClick="btnContinue_Click"
            Font-Size="Large" />
        <br />
    </div>
    </form>
</body>
</html>
<%--</asp:Content>--%>
