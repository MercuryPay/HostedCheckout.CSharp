<%@ Page Language="C#" ClientTarget="uplevel" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="ShoppingCart" MaintainScrollPositionOnPostback="true"
    Theme="SkinFile" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="style1">
        <div style="font-size: large; font-weight: bold">
            eCommerce >> Payment (Redirect)
        </div>
        <br />
        <div style="margin-left: 235px; font-size: medium; font-weight: normal;">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divBillingInfo', 'lbtnToggleBilling'); return false;"
                ID="lbtnToggleBilling" Text="Show Billing Info"></asp:LinkButton>
        </div>
        <br />
        <div id="divBillingInfo" style="display: none">
            <table width="100%" cellpadding="2px">
                <tr style="color: Black; font-weight: bold">
                    <td>
                        <asp:Label Text="Billing Info" ID="Label19" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 220px; text-align: right">
                        <asp:Label runat="server" ID="lblName" Text="Cardholder Name:" Font-Size="Medium"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" Text="John Jones" Font-Size="Large"> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblAddress" runat="server" Text="Billing Address:" Font-Size="Medium"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" Text="4 Corporate Square" Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label Text="City:" ID="lblCityState" runat="server" Font-Size="Medium"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCity" Text="Atlanta" Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label Text="State:" ID="lblState" runat="server" Font-Size="Medium"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtState" Text="GA" MaxLength="2" Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label Text="Postal Code:" ID="lblZip" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtZip" Text="30329" MaxLength="10" Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div id="divOrderSummary" runat="server">
            <table width="100%" cellpadding="2px">
                <tr style="color: Black; font-weight: bold;">
                    <td style="width: 220px">
                        <asp:Label Text="Order Summary" ID="Label17" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label Text="1 Pepperoni Pizza:" ID="Label11" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
                    </td>
                    <td>
                        $<asp:TextBox Text="7.50" ID="txtItem1Amt" runat="server" Width="40px" Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label Text="1 Bread Stix:" ID="Label12" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
                    </td>
                    <td>
                        $<asp:TextBox Text="0.50" ID="txtItem2Amt" runat="server" Width="40px" Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label Text="Tax:" ID="Label13" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
                    </td>
                    <td>
                        $<asp:TextBox Text="0.64" ID="txtTaxAmt" runat="server" Width="40px" Font-Size="Large"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr style="font-weight: bold; color: black">
                    <td>
                        <asp:Label Text="Total" ID="Label20" runat="server" Font-Size="Medium" Font-Bold="true"></asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:Label Text="8.64" Font-Bold="true" runat="server" ID="txtTotalAmt" Font-Size="Large"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp
                        <asp:Button runat="server" ID="btnUpdateAmt" OnClick="btnUpdateAmt_Click" Text="Update" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div style="margin-left: 220px">
            <asp:Button runat="server" ID="btnRemotePost" Text="Submit Payment" OnClick="btnCheckOut_Click"
                Font-Size="Large" />&nbsp; &nbsp;
        </div>
        <br />
        <asp:Label runat="server" ID="lblError" ForeColor="DarkRed" />
        <br />
        <asp:Button ID="btnGoToPage" runat="server" Text="Continue...." OnClick="btnGoToPage_Click"
            Visible="false" />
        <br />
        <div style="margin-left: 232px; font-size: medium; font-weight: normal;">
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
            <table cellpadding="5px">
                <tr>
                    <td align="right">
                        <asp:Label ID="Label8" runat="server" Text="Display Style (must be set to Custom for CSS to take effect)"></asp:Label>&nbsp
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
                    <td style="text-align: right">
                        <asp:Label ID="Label5" runat="server" Text="Return Method"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReturnMethod" runat="server" ToolTip="Indicates if the user will be sent back to this ecommerce site using an HTTP GET or HTTP FORM POST request.">
                            <asp:ListItem Value="post" Text="POST" Selected="True" />
                            <asp:ListItem Value="get" Text="GET" Selected="false" />
                            <asp:ListItem Value="" Text="" Selected="false" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trTranType" runat="server">
                    <td align="right">
                        <asp:Label ID="Label6" runat="server" Text="Transaction Type"></asp:Label>&nbsp
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="ddlTranType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="PreAuth" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Sale"></asp:ListItem>
                                <asp:ListItem Text="ZeroAuth"></asp:ListItem>
                            </asp:RadioButtonList>                            
                            <br />
                        </div>
                    </td>
                </tr>
                <tr style="height:20px;"></tr>
                <tr id="trFrequency" runat="server">
                    <td align="right">
                        <asp:Label ID="Label7" runat="server" Text="Frequency"></asp:Label>&nbsp
                    </td>
                    <td>
                        <div style="float: left; height: 24px;">
                            <asp:RadioButtonList ID="rblFrequency" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="OneTime" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Recurring"></asp:ListItem>
                            </asp:RadioButtonList>
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
                        <asp:Label ID="lblPassword" Text="Password" runat="server"></asp:Label><br />
                    </td>
                    <td>
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
                    <td align="right">
                        <asp:Label ID="lblInvoice" Text="Invoice" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtInvoice" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblMemo" Text="Memo" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtMemo" runat="server" MaxLength="40" Width="50%" Text="HC EComm Credit Pmt Redirect"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trCustomerCode" runat="server">
                    <td align="right">
                        <asp:Label ID="lblCustomerCode" Text="Customer Code" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustomerCode" runat="server" Text="CUSTCODE123"></asp:TextBox>
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
                        <asp:TextBox ID="txtPaymentButtonText" runat="server" Text="Submit"></asp:TextBox>
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
                    <td style="text-align: right">
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
                    <td align="right">
                        <asp:Label ID="Label4" Text="Page Timeout Duration (0 is off, max 15 mins)" runat="server"></asp:Label>&nbsp
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
                <tr id="Tr1" runat="server">
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
                <tr id="trKindle" runat="server">
                    <td align="right">
                        <asp:Label ID="Label10" runat="server" Text="Detect Kindle"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkDetectKindle" runat="server" Checked="true" />
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
                        <asp:Label ID="lblLogoURL" Text="HC Logo URL" runat="server"></asp:Label><br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtLogoURL" runat="server" Width="455px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td style="text-align: right">
                        <asp:Label ID="Label24" runat="server" Text="Security Logo"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rblSecurityLogo" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="On" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Off"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trJCB" runat="server" style="background-color: #eeeeee;">
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
                <tr id="trDiners" runat="server" style="background-color: #eeeeee;">
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
                        <asp:Label ID="Label2" Text="Font Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMultiColor" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td valign="top" align="right">
                        <asp:Label ID="Label3" Text="Font Family" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <div>
                            <asp:RadioButtonList ID="rblFontFamily" runat="server" onclick="CheckForOther('rblFontFamily' ,'divFontFam');">
                                <asp:ListItem Value="FontFamily1">FontFamily1 (Arial, Verdana, Geneva, Helvetica, sans-serif)</asp:ListItem>
                                <asp:ListItem Value="FontFamily2">FontFamily2 (Georgia, Times New Roman, Times, serif)</asp:ListItem>
                                <asp:ListItem Value="FontFamily3">FontFamily3 (Courier New, Courier, monospace)</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div id="divFontFam" style="display: none">
                            <asp:TextBox ID="txtFontFamily" runat="server" Width="455px"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td valign="top" align="right">
                        <asp:Label ID="lblBGColor" Text="Background Color" runat="server"></asp:Label>&nbsp
                    </td>
                    <td>
                        <asp:TextBox ID="txtBGColor" runat="server" Width="50%" Text="white"></asp:TextBox>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="lblButtonTextColor" runat="server" Text="Button Text Color"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtButtonTextColor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="lblButtonBgColor" runat="server" Text="Button Background Color"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtButtonBgColor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="background-color: #eeeeee;">
                    <td align="right">
                        <asp:Label ID="Label23" runat="server" Text="Total Amount Background Color"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalAmtBgColor" runat="server" Text="#ADBB81"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
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
                    <tr id="trAck" runat="server">
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="Call Ack on Order Complete"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCallAck" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label22" Text="Button Type" runat="server"></asp:Label>&nbsp
                        </td>
                        <td>
                            <div style="float: left; height: 24px;">
                                <asp:RadioButtonList ID="rblButtonType" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="True" OnSelectedIndexChanged="rblButtonType_SelectedIndexChanged">
                                    <asp:ListItem Text="Standard" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Image"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
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
                </table>
                <br />
            </div>
            <div style="margin-left: 300px;">
                <asp:Button runat="server" ID="Button1" Text="Submit Payment" OnClick="btnCheckOut_Click"
                    Font-Size="Large" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //      These will run every time page reloads so that if 'Other' is checked, textbox will be there.     
        CheckForOther('rblFontFamily', 'divFontFam');
    </script>
</asp:Content>
