<%@ Page Language="C#" ClientTarget="uplevel"  AutoEventWireup="true" CodeFile="mAddCardComplete.aspx.cs"
    MasterPageFile="~/Mobile/mobile.master" Inherits="mAddCardComplete" Theme="MobileEComm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cphHeader">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <div>
        <div class="pageHeader">
            Add Card Complete
        </div>
        <br />
        <br />
        <div style="text-align: center">
            <asp:Label runat="server" ID="lblStatus" Text="The card has been saved."></asp:Label>
        </div>
        <br />
        <br />
        <br />
        <div class="separator" />
        <br />
        <div class="hotSpot" style="text-align: center">
            <a id="aVerify" href="#" onclick="toggle_visibility('divAPIFields','aVerify');" >Show Verification Fields</a>
        </div>
        <div id="divAPIFields" style="display: none">
            <div class="fieldLabel">
                Card ID
            </div>
            <div>
                <asp:TextBox ID="txtCardID" runat="server"></asp:TextBox>
            </div>
            <div class="fieldLabel">
                ReturnCode
            </div>
            <div>
                <asp:TextBox ID="txtReturnCode" runat="server"></asp:TextBox>
            </div>
            <div class="fieldLabel">
                Return Msg
            </div>
            <div>
                <asp:TextBox ID="txtReturnMessage" runat="server"></asp:TextBox>
            </div>
            <div class="fieldLabel">
                Token
            </div>
            <div>
                <asp:TextBox ID="txttoken" runat="server"></asp:TextBox>
            </div>
            <div class="fieldLabel">
                Returned Values
            </div>
            <div>
                <asp:ListBox ID="lstResponseValues" runat="server" Height="220px"></asp:ListBox>
            </div>
            <br />
            <br />
        </div>
    </div>

</asp:Content>
