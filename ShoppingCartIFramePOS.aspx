<%@ Page Language="C#" ClientTarget="uplevel" AutoEventWireup="true" CodeFile="ShoppingCartIFramePOS.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="ShoppingCartIFramePOS" MaintainScrollPositionOnPostback="true"
    StylesheetTheme="POS" Theme="SkinFile" %>

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
            width: 220px;
            text-align: right;
        }
        .style4
        {
            height: 33px;
            width: 181px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="font-size: large; font-weight: bold">
        POS >> Payment (iFrame)
    </div>
    <div>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" />
        <br />
    </div>
    <div id="divOrder" runat="server">
        <div>
            <table width="80%" cellpadding="0" cellspacing="0">
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
                            &nbsp;<asp:Button runat="server" ID="btnZeroAmt" OnClick="btnZeroAmt_Click" Text="Zeros" SkinID="SmallButton"/>
                    </td>
                </tr>
                <tr>
                    <td>
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
        <div style="text-align: center; font-size: medium; font-weight: normal;">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divAPIFields', 'aToggle'); return false;"
                ID="aToggle" Text="Show Initialize Fields"></asp:LinkButton>
        </div>
        <br />
        <%--API Fields--%>
        <div id="divAPIFields" style="display: none">
            <br />
            <asp:Label ID="lblAPIFields" runat="server" Text="API Fields that Can Not be Controlled Via CSS"
                Font-Bold="true" Font-Italic="true"></asp:Label>
            <br />
            <br />
            <table style="width: 90%; table-layout: fixed; margin-right: 131px;" cellpadding="2px">
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label8" runat="server" Text="Display Style (Must be custom for CSS to take effect)"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblDisplayStyle" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Mercury" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Custom"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblPaymentID" runat="server" Text="PaymentID"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:Label ID="lblPaymentIDValue" runat="server" Text="" ToolTip="If a current PaymentID is already present then it will be used along with the original API settings from the list below. Any changes to settings below will not take effect unless you reset the ID."></asp:Label>&nbsp
                        <asp:Button ID="btnResetID" Text="Reset PaymentID" runat="server" OnClick="btnResetID_Click"
                            SkinID="SmallButton" />
                    </td>
                </tr>
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
                            <asp:ListItem Text="ZeroAuth"></asp:ListItem>
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
                    <td align="right">
                        <asp:Label ID="Label28" Text="Lane ID" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtLaneID" runat="server"></asp:TextBox>
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
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="Label27" Text="CVV Display"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblCVV" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">On</asp:ListItem>
                            <asp:ListItem>Off</asp:ListItem>
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
                        <asp:Label ID="lblCancelButtonText" runat="server" Text="Cancel Button Text"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCancelButton" runat="server" Text="Cancel"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label22" Text="Cancel Button" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblCancelButton" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="On"></asp:ListItem>
                                <asp:ListItem Text="Off" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
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
                        <asp:Label ID="Label12" Text="Card Entry Method" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardEntryMethod" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                        <asp:Label ID="Label13" Text="(Valid values are 'swipe', 'manual', 'both', '')" runat="server"></asp:Label>&nbsp
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" class="style3">
                        <asp:Label ID="Label19" Text="Page Timeout Duration (0 is off, max 15 mins)" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPageTimeoutDuration" runat="server">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="0 (Default)" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            <asp:ListItem Text="16 (Invalid)" Value="16"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label21" Text="Page Timeout Indicator" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblPageTimeoutIndicator" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="On"></asp:ListItem>
                                <asp:ListItem Text="Off" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label10" Text="Force Tablet to Manual" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblForce" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>On</asp:ListItem>
                            <asp:ListItem>Off</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td colspan="2">
                        <asp:Label ID="Label17" runat="server" Text="API 'Style' Fields (Ignored if CSS is Used)"
                            Font-Bold="true" Font-Italic="true"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
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
                <tr style="background-color: #eeeeee;">
                    <td class="style3">
                        <asp:Label ID="Label2" Text="Font Color for Checkout Page" runat="server"></asp:Label>&nbsp
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="ddlMultiColor" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
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
                <tr style="background-color: #eeeeee;">
                    <td valign="top" class="style3">
                        <asp:Label ID="Label5" Text=" " runat="server"></asp:Label><br />
                        <asp:Label ID="Label4" Text="Background Color for Checkout Page" runat="server"></asp:Label>&nbsp
                    </td>
                    <td class="style2">
                        <br />
                        <asp:TextBox ID="txtBGColor" runat="server" Width="50%" Text="white"></asp:TextBox>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td style="text-align: right" class="style3">
                        <asp:Label ID="Label20" Text="Total Amount Background Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalAmtBgColor" runat="server" Text="#ADBB81"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td class="style3">
                        <asp:Label ID="lblBtnBgColor" Text="Button Background Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBtnBgColor" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td class="style3">
                        <asp:Label ID="lblBtnFgColor" Text="Button Text Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBtnTextColor" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
            </table>
            <div style="margin: 20px 0 20px 320px; font-size: medium; font-weight: normal;">
                <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
                <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divDeprecatedAPI', 'aToggleDep'); return false;"
                    ID="aToggleDep" Text="Deprecated Fields"></asp:LinkButton>
            </div>
            <div id="divDeprecatedAPI" style="display: none">
                <table>
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
                        <td style="text-align: right" class="style3">
                            <asp:Label ID="Label16" Text="Cancel Button Default Image" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancelBtnDefault" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <asp:Label ID="Label18" Text="Cancel Button Hover Image" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancelBtnHover" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-left: 300px; margin-top: 20px;">
                <asp:Button runat="server" ID="Button1" Text="Checkout" OnClick="btnCheckOut_Click"
                    Width="170px" />&nbsp;
            </div>
        </div>
        <hr />
        Testing Notes: For PartialAuth, Use 23.54 for PurchaseAmount with Visa=4005550000000480
    </div>
    <br />
    <br />
    <div style="width: 940px; margin: 0 0 0 -5px;">
        <iframe id='ifrm' src="http://localhost:49510/Website/CheckoutPOSIFrame.aspx" height="720px"
            width="100%" scrolling="auto" frameborder="1" runat="server" style="text-align: center;
            display: none;">Sorry, your browser doesn’t support iframes.</iframe>
        &nbsp;
    </div>
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
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Font-Size="Large" />
        &nbsp;
        <asp:Button ID="btnContinue" runat="server" Text="Next" OnClick="btnContinue_Click"
            Font-Size="Large" />
        <br />
    </div>
</asp:Content>
