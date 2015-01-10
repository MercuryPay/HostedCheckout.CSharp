<%@ Page Language="C#" ClientTarget="uplevel"  AutoEventWireup="true" CodeFile="AddCardCompleteIFrame.aspx.cs"
    Inherits="AddCardCompleteIFrame" %>

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
            

        }
    </script>
    <div class="style1" style="font-size: medium;">
        <br />
        <br />
        <div style="text-align: center">
            <asp:Label runat="server" ID="lblStatus" Font-Size="Medium" Font-Bold="true"></asp:Label>
            <br />
        </div>
        <br />
        <div style="text-align: center; font-size: medium; font-weight: normal;">
            <%--  having a LinkButton rather that an an anchor keeps the 'MaintainScrollPosition' attribute functioning  --%>
            <asp:LinkButton runat="server" OnClientClick="toggle_visibility('divAPIFields', 'aToggle'); return false;"
                ID="aToggle" Text="Show Verification Fields"></asp:LinkButton>
        </div>
        <br />
        <%--API Fields--%>
        <div id="divAPIFields" style="display: none">
            <div>
                <i>Payment ID</i> &nbsp
                <asp:TextBox ID="txtPaymentID" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
            </div>
            <div style="margin-top: 3px;">
                <i>ReturnCode</i> &nbsp
                <asp:TextBox ID="txtReturnCode" runat="server" Width="25px" ReadOnly="true"></asp:TextBox>
            </div>
            <div style="margin-top: 3px; vertical-align: top;">
                <div style="margin-top: 3px; vertical-align: top;">
                    <i>Return Msg</i> &nbsp
                </div>
                <asp:TextBox ID="txtReturnMessage" runat="server" Width="330px" ReadOnly="true" TextMode="MultiLine"
                    Height="24px"></asp:TextBox>
            </div>
            <div style="margin-top: 5px;">
                <i>Token</i> &nbsp; &nbsp;
                <asp:Label ID="lbltoken" runat="server" Font-Italic="false" Font-Size="14px"></asp:Label>
            </div>
            <div style="margin-top: 0px;">
                <i>Returned Values</i>
            </div>
            <asp:ListBox ID="lstCardInfoResults" runat="server" Visible="false" Height="100px"
                Width="390px" Font-Size="11px"></asp:ListBox>
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
