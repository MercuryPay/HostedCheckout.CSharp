<%@ Page Language="C#" ClientTarget="uplevel" AutoEventWireup="true" CodeFile="mAddCard.aspx.cs"
    MasterPageFile="~/Mobile/mobile.master" Inherits="mAddCard" MaintainScrollPositionOnPostback="true"
    Theme="MobileEComm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cphHeader">
</asp:Content>
<asp:Content ID="bodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <div>
        <div style="font-size: large; font-weight: bold; margin-left: 5px;">
            Mobile >> Add Card (Redirect)
        </div>
        <br />
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
        <div style="text-align: center;">
            <asp:Button class="button" runat="server" ID="btnAddCard" Text="Add Card" ToolTip="Press to add a new card."
                OnClick="btnAddCard_Click" />
        </div>
        <br />
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
        <div class="hotSpot" onclick="SetFocus('txtName');">
            <div class="fieldLabel">
                CardHolder Name
            </div>
            <div>
                <asp:TextBox runat="server" ID="txtName" Text="John Jones"></asp:TextBox>
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
            <asp:Button class="button" runat="server" ID="btnBottomAddCard" Text="Add Card" OnClick="btnAddCard_Click" />
        </div>
    </div>
</asp:Content>
