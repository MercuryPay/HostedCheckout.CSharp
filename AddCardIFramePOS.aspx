<%@ Page Language="C#" ClientTarget="uplevel" AutoEventWireup="true" CodeFile="AddCardIFramePOS.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="AddCardIFramePOS" MaintainScrollPositionOnPostback="true"
    Theme="SkinFile" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 255px;
            text-align: right;
        }
        .style3
        {
            width: 181px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="font-size: large; font-weight: bold">
        POS >> Add Card (iFrame)
    </div>
    <br />
    <div>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" />
    </div>
    <div id="divOrder" runat="server">
        <div class="pageHeader">
            My Cards On File
        </div>
        <br />
        <table width="100%">
            <tr>
                <td class="columnHeader">
                    Card Type
                </td>
                <td class="columnHeader">
                    Account
                </td>
                <td class="columnHeader">
                </td>
            </tr>
            <tr>
                <td>
                    Credit - Visa
                </td>
                <td>
                    XXXXXXXXXXXX5049
                </td>
                <td>
                    <asp:LinkButton ID="Linkbutton1" runat="server" PostBackUrl="~/Mobile/mAddCard.aspx"
                        Text="Remove" Font-Size="Small"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <div style="text-align: center">
            <asp:Button runat="server" ID="btnCheckout" Text="Add Card" OnClick="btnCheckOut_Click"
                Font-Size="Large" />
        </div>
        <br />
        <br />
        <div style="text-align: center; font-size: medium; font-weight: normal;">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divAPIFields', 'aToggle'); return false;"
                ID="aToggle" Text="Show Initialize Fields"></asp:LinkButton>
        </div>
        <br />
        <%--API Fields--%>
        <div id="divAPIFields" style="display: none">
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="API Fields that Can Not be Controlled Via CSS"
                Font-Bold="true" Font-Italic="true"></asp:Label>
            <br />
            <br />
            <table style="width: 90%; table-layout: fixed; margin-right: 131px;" cellpadding="3px">
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
                    <td style="text-align: right">
                        <asp:Label ID="lblCardID" runat="server" Text="CardID"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:Label ID="lblCardIDValue" runat="server" Text="" ToolTip="If a current PaymentID is already present then it will be used along with the original API settings from the list below. Any changes to settings below will not take effect unless you reset the ID."></asp:Label>&nbsp
                        <asp:Button ID="btnResetID" Text="Reset CardID" runat="server" OnClick="btnResetID_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label2" runat="server" Text="Return Method"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReturnMethod" runat="server" ToolTip="Indicates if the user will be sent back to this ecommerce site using an HTTP GET or HTTP FORM POST request.">
                            <asp:ListItem Value="post" Text="POST" Selected="True" />
                            <asp:ListItem Value="get" Text="GET" Selected="false" />
                            <asp:ListItem Value="" Text="" Selected="false" />
                        </asp:DropDownList>
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
                        Cardholder Name
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCardHolderName" runat="server" Width="183px"></asp:TextBox>
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
                            <asp:ListItem Selected="True">Swipe</asp:ListItem>
                            <asp:ListItem>Manual</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblPageTitle" Text="Page Title" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtPageTitle" runat="server" Width="50%" Text="Durango Pizza - Powered by Mercury"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblKeypad" Text="Keypad"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:RadioButtonList ID="rblKeypad" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>On</asp:ListItem>
                            <asp:ListItem  Selected="True">Off</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblPaymentButtonText" runat="server" Text="Process Button Text"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPaymentButtonText" runat="server" Text="Add Card"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblCancelButtonText" runat="server" Text="Cancel Button Text"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCancelButton" runat="server" Text="Cancel"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
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
                        <asp:Label ID="lblOperatorID" Text="Operator ID" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtOpID" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label10" Text="Card Entry Method" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardEntryMethod" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                        <asp:Label ID="Label11" Text="(Valid values are 'swipe', 'manual', 'both', '')" runat="server"></asp:Label>&nbsp
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label6" Text="Page Timeout Duration (0 is off, max 15 mins)" runat="server"></asp:Label>&nbsp
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
                    <td class="style3">
                        <asp:Label ID="Label12" Text="Force Tablet to Manual" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblForce" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>On</asp:ListItem>
                            <asp:ListItem Selected="True">Off</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr style="margin-top: 20px;">
                    <td colspan="2" style="background-color: #eeeeee;">
                        <asp:Label ID="Label25" runat="server" Text="API 'Style' Fields (Ignored if CSS is Used)"
                            Font-Bold="true" Font-Italic="true"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td class="style3">
                        <asp:Label ID="Label3" Text="Font Size for Checkout Page" runat="server"></asp:Label>
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
                        <asp:Label ID="Label4" Text="Font Color for Checkout Page" runat="server"></asp:Label>&nbsp
                    </td>
                    <td class="style2">
                        <asp:DropDownList ID="ddlMultiColor" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td valign="top" class="style3">
                        <asp:Label ID="Label5" Text="Font Family for Checkout Page" runat="server"></asp:Label>&nbsp
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
                        <asp:Label ID="lbl6" Text=" " runat="server"></asp:Label><br />
                        <asp:Label ID="Lbl7" Text="Background Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBGColor" runat="server" Width="50%" Text="white"></asp:TextBox>
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
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <div style="margin-left: 320px; font-size: medium; font-weight: normal; margin-top: 20px;">
                <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
                <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divDeprecatedAPI', 'aToggleDep'); return false;"
                    ID="aToggleDep" Text="Deprecated Fields"></asp:LinkButton>
            </div>
            <br />
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
                        <td>
                            <asp:Label ID="Label16" Text="Cancel Button Default Image" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancelBtnDefault" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label18" Text="Cancel Button Hover Image" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancelBtnHover" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-left: 300px">
                <br />
                <asp:Button runat="server" ID="Button1" Text="Add Card" OnClick="btnCheckOut_Click"
                    Font-Size="Large" />
            </div>
        </div>
    </div>
    <div style="width: 920px;">
        <iframe id='ifrm' src="http://localhost:49510/website/cardinfoPOSIFrame.aspx" height="650px"
            width="920px" scrolling="auto" frameborder="0" runat="server" style="text-align: right;
            display: none; border: 1px solid red; padding: 0px">Sorry, your browser doesn’t
            support iframes.</iframe>
        &nbsp;
    </div>
    <div id="divThanks" runat="server" style="text-align: center; width: 50%;">
        <asp:Label ID="lblStatus" runat="server" Text="Your card has been saved." Font-Bold="true"
            Font-Size="Large"></asp:Label>
        <br />
        <br />
        <br />
    </div>
    <div style="text-align: center; margin-top: 0px">
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" Font-Size="Large" />
        &nbsp;
        <asp:Button ID="btnContinue" runat="server" Text="Next" OnClick="btnContinue_Click"
            Font-Size="Large" />
        <br />
    </div>
</asp:Content>
