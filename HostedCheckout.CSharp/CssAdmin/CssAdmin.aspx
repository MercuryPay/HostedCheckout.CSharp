<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CssAdmin.aspx.cs" Inherits="CssAdmin_CssAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <div style="margin-left: 10px">
        <div>
            <asp:Button ID="btnUpload" runat="server" Text="Upload CSS" Font-Size="Medium" OnClick="btnUpload_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnDownload" runat="server" Text="Download (formatted)" Font-Size="Medium"
                OnClick="btnDownload_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnDownloadUnFormatted" runat="server" Text="Download (unformatted)"
                Font-Size="Medium" OnClick="btnDownloadUnFormatted_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnRemove" runat="server" Text="Remove CSS" Font-Size="Medium" 
                OnClick="btnRemove_Click" />
        </div>
        <br />
        <div>
            <asp:Label ID="Label1" runat="server" Text="Merchant ID" Font-Size="Large" Width="150px"></asp:Label>&nbsp
            <asp:TextBox ID="txtMerchantID" runat="server" Width="197px"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Label ID="Label2" runat="server" Text="Password" Font-Size="Large" Width="150px"></asp:Label>&nbsp
            <asp:TextBox ID="txtPassword" runat="server" Width="325px"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Label ID="Label4" runat="server" Text="Response Code" Font-Size="Large" Width="150px"></asp:Label>&nbsp
            <asp:TextBox ID="txtCode" runat="server" Width="325px"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Label ID="Label3" runat="server" Text="Response Msg" Font-Size="Large" Width="150px"></asp:Label><br />
            <asp:TextBox ID="txtResponseMsg" runat="server" Width="751px" TextMode="MultiLine"
                Height="54px" ReadOnly="True"></asp:TextBox>
        </div>
        <br />
        <div style="float: left">
            <asp:Label ID="Label5" runat="server" Text="CSS" Font-Size="Large" Width="150px"></asp:Label><vr></vr>
        </div>
        <div style="float: right">
            <asp:Button ID="btnClear" runat="server" Text="Clear" Font-Size="X-Small" OnClick="btnClear_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Size="X-Small" OnClick="btnReset_Click" />&nbsp;&nbsp;
        </div>
        <asp:TextBox ID="txtCSS" runat="server" Height="462px" TextMode="MultiLine" Width="914px"
            Style="margin-top: 5px"></asp:TextBox>
</asp:Content>
