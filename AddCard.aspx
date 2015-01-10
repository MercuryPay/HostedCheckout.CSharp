<%@ Page Language="C#" ClientTarget="uplevel" AutoEventWireup="true" CodeFile="AddCard.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="AddCard" MaintainScrollPositionOnPostback="true"
    Theme="SkinFile" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  
        <div style="font-size: large; font-weight: bold">
            eCommerce >> Add Card (Redirect)
        </div>
        <br />
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
            <asp:Button runat="server" ID="btnAddCard" Text="Add Card" ToolTip="" OnClick="btnAddCard_Click"
                Font-Size="Large" />
            <br />
        </div>
        <br />
        &nbsp; &nbsp;
        <br />
        <asp:Label runat="server" ID="lblError" ForeColor="DarkRed" />
        <br />
        <div style="text-align: center; font-size: medium; font-weight: normal;">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divAPIFields', 'aToggle'); return false;"
                ID="aToggle" Text="Show Initialize Fields"></asp:LinkButton>
        </div>
        <%--API Fields--%>
        <div id="divAPIFields" style="display: none">
            <br />
            <asp:Label ID="lblAPIFields" runat="server" Text="API Fields that Can Not be Controlled Via CSS"
                Font-Bold="true" Font-Italic="true"></asp:Label>
            <br />
            <br />
            <table cellpadding="5px">
                <tr>
                    <td align="right">
                        <asp:Label ID="Label8" runat="server" Text="Display Style (Must be Custom for CSS or API Styling to take effect.)"></asp:Label>&nbsp
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblDisplayStyle" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Mercury" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Custom"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label6" runat="server" Text="Return Method"></asp:Label>&nbsp
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
                    <td align="right">
                        <asp:Label ID="lblCardHolderName" runat="server" Text="Cardholder Name"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtCardHolderName" runat="server" Width="247px" Text="John Jones"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label7" runat="server" Text="Frequency"></asp:Label>&nbsp
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblFrequency" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="OneTime" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Recurring"></asp:ListItem>
                            </asp:RadioButtonList>
                            &nbsp;
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblMerchantID" Text="Merchant ID" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtMerchantID" runat="server"></asp:TextBox>
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
                    <td align="right">
                        <asp:Label ID="lblPassword" Text="Password" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblPageTitle" Text="Page Title" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtPageTitle" runat="server" Width="50%" Text="Durango Pizza - Powered by Mercury"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblPaymentButtonText" runat="server" Text="Payment Button Text"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPaymentButtonText" runat="server" Text="Update"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblCancelButtonText" runat="server" Text="Cancel Button Text"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCancelButton" runat="server" Text="Cancel"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblOperatorID" Text="Operator ID" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtOpID" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label10" runat="server" Text="Detect Kindle"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkDetectKindle" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label5" Text="Page Timeout Duration (0 is off, max 15 mins)" runat="server"></asp:Label>&nbsp
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
                    <td>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td>
                        <asp:Label ID="Label25" runat="server" Text="API 'Style' Fields (Ignored if CSS is Used)"
                            Font-Bold="true" Font-Italic="true"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="lblLogoURL" Text="Logo URL" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtLogoURL" runat="server" Width="466px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="lblSecureLogo" runat="server" Text="Security Logo"></asp:Label>&nbsp
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblSecurityLogo" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="On" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Off"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="lblJCB" runat="server" Text="JCB"></asp:Label>
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblJCB" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="On" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Off"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="lblDiners" runat="server" Text="Diners"></asp:Label>
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblDiners" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="On" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Off"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="Label1" Text="Font Size" runat="server"></asp:Label>
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblFontSize" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Small</asp:ListItem>
                                <asp:ListItem Selected="True">Medium</asp:ListItem>
                                <asp:ListItem>Large</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="Label2" Text="Font Color for Checkout Page" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMultiColor" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right" valign="top">
                        <asp:Label ID="Label3" Text="Font Family for Checkout Page" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblFontFamily" runat="server" onclick="CheckForOther('rblFontFamily' ,'divFontFam');">
                            <asp:ListItem Value="FontFamily1">FontFamily1 (Arial, Verdana, Geneva, Helvetica, sans-serif)</asp:ListItem>
                            <asp:ListItem Value="FontFamily2">FontFamily2 (Georgia, Times New Roman, Times, serif)</asp:ListItem>
                            <asp:ListItem Value="FontFamily3">FontFamily3 (Courier New, Courier, monospace)</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:RadioButtonList>
                        <div id="divFontFam" style="display: none">
                            <asp:TextBox ID="txtFontFamily" runat="server" Width="466px"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="Label4" Text="Background Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBGColor" runat="server" Width="50%" Text="white"></asp:TextBox>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="lblBtnBgColor" Text="Button Background Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBtnBgColor" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="lblBtnFgColor" Text="Button Text Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBtnTextColor" runat="server"></asp:TextBox>&nbsp&nbsp&nbsp
                    </td>
                </tr>
            </table>
            <div style="margin-left: 320px; font-size: medium; font-weight: normal;">
                <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
                <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divDeprecatedAPI', 'aToggleDep'); return false;"
                    ID="aToggleDep" Text="Deprecated Fields"></asp:LinkButton>
            </div>
            <br />
            <div id="divDeprecatedAPI" style="display: none">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text="Unused Fields that are Still in the API"
                                Font-Bold="true" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label14" Text="Submit Button Default Image" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubmitBtnDefault" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label15" Text="Submit Button Hover Image" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubmitBtnHover" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label16" Text="Cancel Button Default Image" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancelBtnDefault" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label18" Text="Cancel Button Hover Image" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <asp:TextBox ID="txtCancelBtnHover" runat="server" Width="466px"></asp:TextBox>&nbsp&nbsp&nbsp
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="Call Ack on Add Card Complete"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCallAck" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-left: 320px">
                <asp:Button runat="server" ID="Button1" Text="Add Card" ToolTip="" OnClick="btnAddCard_Click"
                    Font-Size="Large" />
            </div>
        </div>
        <script type="text/javascript">
            CheckForOther('rblFontFamily', 'divFontFam');
        </script>
</asp:Content>
