<%@ Page Language="C#" ClientTarget="uplevel"  AutoEventWireup="true" CodeFile="OrderCompleteIFramePOS.aspx.cs"
    Inherits="OrderCompleteIFrame" StylesheetTheme="POS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/demo.js" type="text/javascript"></script>
</head>
<body style="background-color: white;">
    <form id="form1" runat="server">
    <script type="text/javascript">
        window.onload = function () {


            var ctl = parent.document.getElementById('ctl00_MainContent_btnContinue');
            if (ctl != null)
                ctl.style.display = 'inline';

            ctl = parent.document.getElementById('ctl00_MainContent_ifrm');
            if (ctl != null) {
               
                ctl.style.width = '600px';
            }

        }
    </script>
    <div style="font-size: large; width: 600px;">
        <div>
            <asp:Label runat="server" ID="lblStatus" SkinID="LabelLarge" Width="600px" ForeColor="DarkRed"></asp:Label>
        </div>
        <div style="text-align: center; font-size: medium; font-weight: normal;">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divAPIFields', 'aToggle'); return false;"
                ID="aToggle" Text="Show Verification Fields"></asp:LinkButton>
        </div>        
        <br />
        <%--API Fields--%>
        <div id="divAPIFields" style="display: none">
            <div>
                <asp:Label runat="server" Text="Payment ID"></asp:Label>
                &nbsp&nbsp&nbsp
                <asp:TextBox ID="txtPaymentID" runat="server" Width="400px" ReadOnly="true"></asp:TextBox>
            </div>
            <div style="margin-top: 3px;">
                <asp:Label runat="server" Text="ReturnCode"></asp:Label>&nbsp;&nbsp&nbsp&nbsp&nbsp
                <asp:TextBox ID="txtReturnCode" runat="server" Width="69px" ReadOnly="true"></asp:TextBox>
            </div>
            <div style="margin-top: 3px; vertical-align: top;">
                <asp:Label runat="server" Text="Return Message"></asp:Label>&nbsp
                <asp:TextBox ID="txtReturnMessage" runat="server" Width="400px" ReadOnly="true"></asp:TextBox>
            </div>
            <div style="margin-top: 5px;">
                <asp:Label runat="server" Text="Token" SkinID="LabelLarge"></asp:Label>&nbsp
                <asp:TextBox ID="lbltoken" runat="server" Font-Italic="false" Enabled="false" Width="400px"></asp:TextBox>
            </div>
            <div style="margin-top: 0px;">
                <asp:Label runat="server" Text="Returned Values" SkinID="LabelLarge"></asp:Label>
            </div>
            <asp:ListBox ID="lstPreAuthReturned" runat="server" Visible="false" Height="200px"
                Width="500px" Font-Size="12px"></asp:ListBox>
            <%--<asp:Button ID="btnAck" runat="server" Text="Acknowledge" OnClick="btnAck_Click" />&nbsp
        <asp:Button runat="server" ID="btnRemotePost" Text="PreAuthCapture" OnClick="btnPreAuthCapture_Click" />&nbsp;--%>
            <%--&nbsp; &nbsp;<div runat="server" id="divCapturReturnVals" style="margin-top: 10px">
            <i>Capture Returned Values</i>
            <div>
                <asp:ListBox ID="lstCaptureReturned" runat="server" Height="150px" Width="633px"
                    Font-Size="11px"></asp:ListBox>
            </div>
        </div>--%>
        </div>
    </div>
    </form>
</body>
</html>
