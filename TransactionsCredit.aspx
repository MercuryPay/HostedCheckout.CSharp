<%@ Page Language="C#" ClientTarget="uplevel"  AutoEventWireup="true" CodeFile="TransactionsCredit.aspx.cs"
    Inherits="TransactionsCredit" MasterPageFile="~/Site.master" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="font-size: large; font-weight: bold">
        Transactions >> Credit
    </div>
    <div style="float: left; margin-left: 5px;">
        <asp:Label runat="server" ID="lblStatus" ForeColor="DarkRed" Text="" />
    </div>
    
    <br />
    
    <div style="float: left; margin-top: 10px; display: block; margin-left: 5px">
        <asp:Label runat="server" ID="Label2" Text="Merchant Information" Font-Bold="true"
            Font-Size="Medium" />
    </div>
    <br />
    <div style="float: left; width: 35%; margin-left: 500px; padding-left: 10px; margin-top: -20px">
        <asp:Label Text="Results" runat="server" Font-Size="Medium" Font-Bold="true" ID="lblResults"></asp:Label>
        <br />
        <asp:ListBox ID="lstResults" runat="server" Width="300px" Height="300px"></asp:ListBox>
    </div>
    <br />
    <div style="width: 70%; margin-top: -300px; float: left; padding-left: 10px; height: 100%">
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label ID="lblTranType" Text="Transaction Type:" runat="server" Font-Size="Medium"></asp:Label><br />
            </div>
            <asp:DropDownList ID="ddlTranType" runat="server" OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged"
                AutoPostBack="true" Width="246px" Font-Size="Medium">
                <asp:ListItem Text="PreAuthToken" Selected="True"></asp:ListItem>
                <asp:ListItem Text="PreAuthCaptureToken"></asp:ListItem>
                <asp:ListItem Text="SaleToken"></asp:ListItem>
                <asp:ListItem Text="VoidToken"></asp:ListItem>
                <asp:ListItem Text="ReturnToken"></asp:ListItem>
                <asp:ListItem Text="VoidReturnToken"></asp:ListItem>
                <asp:ListItem Text="AdjustToken"></asp:ListItem>
                <asp:ListItem Text="ReversalToken"></asp:ListItem>
                <asp:ListItem Text="BatchClear"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label ID="lblMerchantID" Text="Merchant ID:" runat="server" Font-Size="Medium"></asp:Label>
            </div>
            <asp:TextBox ID="txtMerchantID" runat="server" Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label ID="lblPassword" Text="Password:" runat="server" Font-Size="Medium"></asp:Label>
            </div>
            <asp:TextBox ID="txtPassword" runat="server" Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label ID="Label3" Text="Lane ID:" runat="server" Font-Size="Medium"></asp:Label>
            </div>
            <asp:TextBox ID="txtLaneID" runat="server" Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label Text="Token:" ID="lblToken" runat="server" Font-Size="Medium"></asp:Label>
            </div>
            <asp:TextBox ID="txtToken" runat="server" Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label ID="Label5" Text="Invoice No:" runat="server" Font-Size="Medium"></asp:Label>
            </div>
            <asp:TextBox ID="txtInvoice" runat="server" Text="12345" Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label runat="server" ID="lblName" Text="Cardholder Name:" Font-Size="Medium"></asp:Label>
            </div>
            <asp:TextBox runat="server" ID="txtName" Text="John Jones" Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label ID="lblAddress" runat="server" Text="AVS Address:" Font-Size="Medium"></asp:Label>
            </div>
            <asp:TextBox ID="txtAddress" runat="server" Text="4 Corporate Square" Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label Text="AVS Zip:" ID="lblZip" runat="server" Font-Size="Medium"></asp:Label>
            </div>
            <asp:TextBox runat="server" ID="txtZip" Text="30329" MaxLength="10" Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
        <div class="formLine" style="height: 17px">
            <div class="formLabel">
                <asp:Label Text="Amount:" ID="Label1" runat="server" Font-Size="Medium"></asp:Label>&nbsp;
            </div>
            <asp:TextBox Text="7.50" ID="txtItemAmt" runat="server" ToolTip="Note:  Use 23.54 for Partial Auth"  Font-Size="Medium" Width="240px"></asp:TextBox>
        </div>
    </div>
    <br />
    <br />
    <div class="formLine" style="margin: 10px 0 0 232px">
        <div class="formLabel" id="div1">
        </div>
        <asp:Button ID="btnProcess" runat="server" Text="Submit" 
            Font-Size="Large" onclick="btnProcess_Click" />
    </div>    
    <br />
</asp:Content>
