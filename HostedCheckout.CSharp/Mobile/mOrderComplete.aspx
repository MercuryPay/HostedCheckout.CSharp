<%@ Page Language="C#" ClientTarget="uplevel"  AutoEventWireup="true" CodeFile="mOrderComplete.aspx.cs" MasterPageFile="~/Mobile/mobile.master"
    Inherits="mOrderComplete" MaintainScrollPositionOnPostback="true" Theme="MobileEComm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cphHeader">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <div>
        <div class="pageHeader">
            Order Complete
        </div>
        <br />
        <br />
        <div style="text-align: center">
            <asp:Label runat="server" ID="lblStatus" Text="Thank your for your order!  <br/>You may pick up your pizza in approximately 20 minutes."></asp:Label>
        </div>
        <br />
        <br />
        <br />
        <div class="separator" />
        <br />
        <div class="hotSpot" style="text-align: center">
            <a href="mOrder.aspx" >Return to Order</a>
        </div>
        <div class="hotSpot" style="text-align: center">
            <a href="#" id="aVerify" onclick="toggle_visibility('divAPIFields','aVerify');" >Show Verification Fields</a>
        </div>
        <br />
        <div id="divAPIFields" style="display: none">
            <div class="fieldLabel">
                Payment ID
            </div>
            <div>
                <asp:TextBox ID="txtPaymentID" runat="server"></asp:TextBox>
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
