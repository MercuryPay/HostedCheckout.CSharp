<%@ Page Language="C#" ClientTarget="uplevel" AutoEventWireup="true" CodeFile="mOrder.aspx.cs"
    MasterPageFile="~/Mobile/mobile.master" Inherits="mOrder" MaintainScrollPositionOnPostback="true"
    Theme="MobileEComm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cphHeader">
</asp:Content>
<asp:Content ID="bodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <div>
        <div style="font-size: large; font-weight: bold">
            Mobile >> Payment (Redirect)
        </div>
        <br />
        <br />
        <div class="pageHeader">
            Order Summary
        </div>
        <br />
        <table width="100%">
            <tr>
                <td class="columnHeader" style="text-align: center">
                    Qty
                </td>
                <td class="columnHeader">
                    Description
                </td>
                <td class="columnHeader" style="text-align: right">
                    Amount
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    1
                </td>
                <td>
                    Large Pepperoni
                </td>
                <td style="text-align: right">
                    7.50
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    1
                </td>
                <td>
                    Caesar Salad
                </td>
                <td style="text-align: right">
                    1.99
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                </td>
                <td>
                    Tax
                </td>
                <td style="text-align: right">
                    0.48
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                </td>
                <td class="total">
                    Total
                </td>
                <td class="total" style="text-align: right">
                    9.97
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <div style="text-align: center;">
            <asp:Button class="button" runat="server" ID="btnCheckout" Text="Place Order" ToolTip="Press to pay and submit your order."
                OnClick="btnCheckOut_Click" />
        </div>
        <asp:Label runat="server" ID="lblError" class="ErrorLabel" />
        <br />
        <asp:Button ID="btnGoToPage" runat="server" Text="Continue...." OnClick="btnGoToPage_Click"
            Visible="false" Width="100px" />
    </div>
    <div class="hotSpot" style="text-align: center;">
        <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divAPIFields', 'lbtnToggle'); return false;"
            ID="lbtnToggle" Text="Show Initialize Fields"></asp:LinkButton>
    </div>
    <%--API Fields--%>
    <div id="divAPIFields" style="display: none;">
        <div class="hotSpot" onclick="SetFocus('ddlDisplayStyle');">
            <div class="fieldLabel">
                Display Style
            </div>
            <div>
                <asp:DropDownList ID="ddlDisplayStyle" runat="server">
                    <asp:ListItem Value="Mercury" Text="Mercury" Selected="false" />
                    <asp:ListItem Value="Custom" Text="Custom" Selected="true" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('ddlReturnMethod');">
            <div class="fieldLabel">
                Return Method
            </div>
            <div>
                <asp:DropDownList ID="ddlReturnMethod" runat="server" ToolTip="Indicates if the user will be sent back to this ecommerce site using an HTTP GET or HTTP FORM POST request.">
                    <asp:ListItem Value="post" Text="POST" Selected="True" />
                    <asp:ListItem Value="get" Text="GET" Selected="false" />
                    <asp:ListItem Value="" Text="" Selected="false" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtMerchantID');">
            <div class="fieldLabel">
                Merchant ID
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtMerchantID" type="numeric"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtPassword');">
            <div class="fieldLabel">
                Password
            </div>
            <div>
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtLaneID');">
            <div class="fieldLabel">
                Lane ID
            </div>
            <div>
                <asp:TextBox ID="txtLaneID" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('ddlTranType');">
            <div class="fieldLabel">
                Transaction Type
            </div>
            <div>
                <asp:DropDownList ID="ddlTranType" runat="server">
                    <asp:ListItem Text="PreAuth" Selected="false"></asp:ListItem>
                    <asp:ListItem Text="Sale" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="ZeroAuth" Selected="false"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtTotalAmount');">
            <div class="fieldLabel">
                Total Amount
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtTotalAmount" Text="9.97" type="numeric"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('ddlFrequency');">
            <div class="fieldLabel">
                Frequency
            </div>
            <div>
                <asp:DropDownList ID="ddlFrequency" runat="server">
                    <asp:ListItem Text="OneTime" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Recurring"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtInvoice');">
            <div class="fieldLabel">
                Invoice
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtInvoice" Text="123456" type="numeric"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtMemo');">
            <div class="fieldLabel">
                Memo
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtMemo" Text="HCMobile"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtTaxAmount');">
            <div class="fieldLabel">
                Tax Amount
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtTaxAmount" Text="0.48" type="numeric"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtName');">
            <div class="fieldLabel">
                CardHolder Name
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtName" Text="John Jones"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtAddress');">
            <div class="fieldLabel">
                Address
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtAddress" Text="4 Corporate Square"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtZip');">
            <div class="fieldLabel">
                AVS Zip:
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtZip" Text="30329" type="numeric"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtCustomerCode');">
            <div class="fieldLabel">
                Customer Code
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtCustCode" Text="CUSTCODE123"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtPageTitle');">
            <div class="fieldLabel">
                Page Title
            </div>
            <div>
                <asp:TextBox ID="txtPageTitle" runat="server" Text="Durango Pizza"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtPaymentButtonText');">
            <div class="fieldLabel">
                Submit Payment Button Text
            </div>
            <div>
                <asp:TextBox ID="txtPaymentButtonText" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtCancelButtonText');">
            <div class="fieldLabel">
                Cancel Button Text
            </div>
            <div>
                <asp:TextBox ID="txtCancelButtonText" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtReturnURL');">
            <div class="fieldLabel">
                Return URL
            </div>
            <div>
                <asp:TextBox ID="txtReturnURL" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('txtProcessCompleteURL');">
            <div class="fieldLabel">
                Process Complete URL
            </div>
            <div>
                <asp:TextBox ID="txtProcessCompleteURL" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('ddlPageTimeoutDuration');">
            <div class="fieldLabel">
                <asp:Label ID="Label4" Text="Page Timeout Duration (0 is off, max 15 mins)" runat="server"></asp:Label>&nbsp
            </div>
            <div>
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
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('ddlPageTimeoutIndicator');">
            <div class="fieldLabel">
                <asp:Label ID="Label21" Text="Page Timeout Indicator" runat="server"></asp:Label>&nbsp
            </div>
            <div>
                <asp:DropDownList ID="ddlPageTimeoutIndicator" runat="server">
                    <asp:ListItem Value="on" Text="On" />
                    <asp:ListItem Value="off" Text="Off" Selected="True" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="hotSpot" onclick="SetFocus('rblAVSFields');">
            <div class="fieldLabel">
                AVS Fields
            </div>
            <div>
                <asp:DropDownList ID="rblAVSFields" runat="server">
                    <asp:ListItem Selected="True">Off</asp:ListItem>
                    <asp:ListItem>Both</asp:ListItem>
                    <asp:ListItem>Zip</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div>
            <div class="fieldLabel">
                CVV Display
            </div>                            
            <div>
                <asp:DropDownList ID="rblCVV" runat="server" >
                    <asp:ListItem Selected="True">On</asp:ListItem>
                    <asp:ListItem>Off</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="hotSpot" style="background-color: #cccccc; padding: 20px 40px 30px 40px;">
            <asp:Label ID="Label25" runat="server" Text="API 'Style' Fields (Ignored if CSS is Used)"
                Font-Bold="true" Font-Italic="true"></asp:Label>
            <div class="hotSpot" onclick="SetFocus('txtLogoURL');">
                <div class="fieldLabel">
                    Logo URL
                </div>
                <div>
                    <asp:TextBox ID="txtLogoURL" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('ddlJCB');">
                <div class="fieldLabel">
                    Show JCB
                </div>
                <div>
                    <asp:DropDownList ID="ddlJCB" runat="server">
                        <asp:ListItem Value="on" Text="On" Selected="True" />
                        <asp:ListItem Value="off" Text="Off" Selected="false" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('ddlDiners');">
                <div class="fieldLabel">
                    Show Diners
                </div>
                <div>
                    <asp:DropDownList ID="ddlDiners" runat="server">
                        <asp:ListItem Value="on" Text="On" Selected="True" />
                        <asp:ListItem Value="off" Text="Off" Selected="false" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('txtBackgroundColor');">
                <div class="fieldLabel">
                    Background Color
                </div>
                <div>
                    <asp:TextBox ID="txtBackgroundColor" runat="server" Text="white"></asp:TextBox>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('txtTotalAmtBGColor');">
                <div class="fieldLabel">
                    Total Amount Background Color
                </div>
                <div>
                    <asp:TextBox ID="txtTotalAmtBGColor" runat="server" Text="white"></asp:TextBox>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('txtFontColor');">
                <div class="fieldLabel">
                    Font Color
                </div>
                <div>
                    <asp:TextBox ID="txtFontColor" runat="server" Text="DarkRed"></asp:TextBox>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('txtButtonTextColor');">
                <div class="fieldLabel">
                    Button Text Color
                </div>
                <div>
                    <asp:TextBox ID="txtButtonTextColor" runat="server" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('txtButtonBgColor');">
                <div class="fieldLabel">
                    Button Background Color
                </div>
                <div>
                    <asp:TextBox ID="txtButtonBgColor" runat="server" Text=""></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="hotSpot" style="text-align: center;">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divDeprecatedAPI', 'aToggleDep'); return false;"
                ID="aToggleDep" Text="Deprecated Fields"></asp:LinkButton>
        </div>
        <br />
        <div id="divDeprecatedAPI" style="display: none">
            <div class="hotSpot" onclick="SetFocus('txtSubmitBtnDefault');">
                <div class="fieldLabel">
                    Submit Button Default Image
                </div>
                <div>
                    <asp:TextBox ID="txtSubmitBtnDefault" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('txtSubmitBtnHover');">
                <div class="fieldLabel">
                    Submit Button Hover Image
                </div>
                <div>
                    <asp:TextBox ID="txtSubmitBtnHover" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('txtCancelBtnDefault');">
                <div class="fieldLabel">
                    Cancel Button Default Image
                </div>
                <div>
                    <asp:TextBox ID="txtCancelBtnDefault" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="hotSpot" onclick="SetFocus('txtCancelBtnHover');">
                <div class="fieldLabel">
                    Cancel Button Hover Image
                </div>
                <div>
                    <asp:TextBox ID="txtCancelBtnHover" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <div style="text-align: center;">
            <asp:Button class="button" runat="server" ID="btnBottomSubmit" Text="Place Order"
                OnClick="btnCheckOut_Click" />
        </div>
    </div>
</asp:Content>
