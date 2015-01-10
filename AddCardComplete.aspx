<%@ Page Language="C#" ClientTarget="uplevel"  AutoEventWireup="true" CodeFile="AddCardComplete.aspx.cs"
    MasterPageFile="~/Site.master" Inherits="AddCardComplete" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type="text/javascript">
        
    
    </script>
    <div class="style1">
        <br /><br />
        <div style="text-align:center">
            <asp:Label runat="server" ID="lblStatus" Font-Size="Large" Font-Bold="true"></asp:Label>
            <br />
            <br />
        </div>
        <br />
        <div style="text-align: center; font-size: medium; font-weight: normal;">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divAPIFields', 'aToggle'); return false;"
                ID="aToggle" Text="Show Verification Fields"></asp:LinkButton>
        </div>
        <%--API Fields--%>
        <div id="divAPIFields" style="display: none">
            <div>
                <i>Card ID</i> &nbsp
                <asp:TextBox ID="txtCardID" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
            </div>
            <div style="margin-top: 3px;">
                <i>ReturnCode</i> &nbsp
                <asp:TextBox ID="txtReturnCode" runat="server" Width="25px" ReadOnly="true"></asp:TextBox>
            </div>
            <div style="margin-top: 3px;">
                <i>Return Msg</i> &nbsp
                <asp:TextBox ID="txtReturnMessage" runat="server" Width="553px" ReadOnly="true"></asp:TextBox>
            </div>
            <div style="margin-top: 3px;">
                <i>Token</i> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp
                <asp:Label ID="lbltoken" runat="server" Width="551px"></asp:Label>
            </div>
            <br />
            <div style="margin-top: 0px;">
                <i>Returned Values</i>
            </div>
            <asp:ListBox ID="lstResults" runat="server" Visible="false" Height="290px" Width="633px"
                Font-Size="11px"></asp:ListBox>
            <br />
            <br />
            <%--<asp:Button ID="btnAck" runat="server" Text="Acknowledge" OnClick="btnAck_Click" />&nbsp--%>
            &nbsp; &nbsp; &nbsp;<div runat="server" id="divCapturReturnVals" style="margin-top: 10px">
                <i>Capture Returned Values</i>
                <div>
                    <asp:ListBox ID="lstCaptureReturned" runat="server" Height="150px" Width="633px"
                        Font-Size="11px"></asp:ListBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
