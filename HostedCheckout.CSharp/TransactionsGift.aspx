<%@ Page Language="C#" ClientTarget="uplevel"  AutoEventWireup="true" CodeFile="TransactionsGift.aspx.cs"
    Inherits="TransactionsGift" MasterPageFile="~/Site.master" Theme="SkinFile"  %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="font-size: large; font-weight: bold">
        Transactions >> Gift
    </div>
    <br />
    <br />
    <div style="float: left">
        <asp:Label runat="server" ID="lblStatus" ForeColor="DarkRed" Width="450px" />
    </div>
    <br />
    <div style="float: left; margin-left: 10px">
        <asp:Label runat="server" ID="Label2" Text="Transaction Data" Font-Bold="true" Font-Size="Medium" />
    </div>
    <div style="float: left; width: 35%; margin-left: 500px; padding-left: 10px; margin-top: -20px">
        <asp:Label Text="Results" runat="server" Font-Size="Medium" Font-Bold="true" ID="lblResults"></asp:Label>
        <br />
        <asp:ListBox ID="lstResults" runat="server" Width="300px" Height="100%" Rows="20">
        </asp:ListBox>
    </div>
    <div style="width: 70%; margin-top: -325px; float: left; padding-left: 10px; height: 100%">
        <div class="formLine" style="height: 20px">
            <div class="formLabel">
                <asp:Label ID="lblTranType" Text="Transaction Type:" runat="server" Font-Size="Medium"></asp:Label><br />
            </div>
            <asp:DropDownList ID="ddlTranType" runat="server"
                AutoPostBack="true" Width="182px" Font-Size="Medium" 
                onselectedindexchanged="ddlTranType_SelectedIndexChanged">
                <asp:ListItem Text="Sale" Value="GiftSale" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Issue" Value="GiftIssue"></asp:ListItem>
                <asp:ListItem Text="Reload" Value="GiftReload"></asp:ListItem>
                <asp:ListItem Text="VoidReload" Value="GiftVoidReload"></asp:ListItem>
                <asp:ListItem Text="VoidIssue" Value="GiftVoidIssue"></asp:ListItem>
                <asp:ListItem Text="VoidSale" Value="GiftVoidSale"></asp:ListItem>
                <asp:ListItem Text="Balance" Value="GiftBalance"></asp:ListItem>
                <asp:ListItem Text="Return" Value="GiftReturn"></asp:ListItem>
                <asp:ListItem Text="VoidReturn" Value="GiftVoidReturn"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="formLine" id="divAmt" style="height: 20px">
            <div class="formLabel">
                <asp:Label Text="Amount:" ID="Label1" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
            </div>
            <asp:TextBox ID="txtItemAmt" runat="server" Text="0.10" Font-Size="Medium"></asp:TextBox>
            <br />
        </div>
        <div class="formLine" style="height: 20px">
            <div class="formLabel">
                <asp:Label Text="Card Number:" ID="lblCardNum" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
            </div>
            <asp:TextBox Text="" ID="txtCardNum" runat="server"  Font-Size="Medium"></asp:TextBox>
            <br />
        </div>
        <div class="formLine" id="divCVV" style="height: 20px">
            <div class="formLabel" id="divCVVLabel">
                <asp:Label Text="CVV" ID="lblCVV" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
            </div>
            <asp:TextBox Text="" ID="txtCVV" runat="server"  Font-Size="Medium"></asp:TextBox>
            <br />
        </div>
        <div class="formLine" style="height: 20px">
            <div class="formLabel">
                <asp:Label ID="Label5" Text="Invoice No:" runat="server" Font-Size="Medium"></asp:Label><br />
            </div>
            <asp:TextBox ID="txtInvoice" runat="server" Text="" Enabled="false" Font-Size="Medium"></asp:TextBox>&nbsp;
            <asp:CheckBox ID="chkRandom" runat="server" Text="Random" TextAlign="Left" AutoPostBack="true"
                Checked="true" OnCheckedChanged="chkRandom_CheckedChanged1" Font-Size="Medium" />
            <br />
        </div>
        <div class="formLine" style="width: 300px; height: 20px" id="divPartialAuth">
            <div class="formLabel">
                <asp:Label ID="Label4" Text="Partial Auth:" runat="server" Font-Size="Medium"></asp:Label>
            </div>
            <asp:CheckBox ID="chkPartialAuth" runat="server" Checked="false"  Font-Size="Large" ToolTip="When checked, it will approve even if purchase amount is greater than balance on card. Developer must ask for additional tender." />
            <br />
        </div>
        <div class="formLine" id="divRefNo" style="height: 20px" >
            <div class="formLabel" id="divRefNoLabel">
                <asp:Label Text="RefNo (from Reload)" ID="lblRefNo" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
            </div>
            <asp:TextBox Text="" ID="txtRefNo" runat="server" Font-Size="Medium"></asp:TextBox>
            <br />
        </div>        
    </div>
    <div class="formLine" style="margin: -30px 0 0 232px">        
        <div class="formLabel" id="div1">
        </div>
        <asp:Button ID="btnProcess" runat="server" Text="Submit" OnClick="btnProcess_Click"
            Font-Size="Large" />
    </div>
    &nbsp;
    <br />
</asp:Content>
