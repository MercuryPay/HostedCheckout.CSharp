<%@ Page Language="C#" ClientTarget="uplevel"  AutoEventWireup="true" CodeFile="mMenu.aspx.cs" MasterPageFile="~/Mobile/mobile.master"
    Inherits="mMenu" MaintainScrollPositionOnPostback="true" Theme="MobileEComm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="cphHeader">
</asp:Content>
<asp:Content ID="bodyContent" runat="server" ContentPlaceHolderID="cphBody">
    <br />
    <div style="font-size: large; font-weight: bold">
        Mobile >> Mobile Menu
    </div>
    <br /><br />
    <div class="hotSpot" onclick="SetFocus('aOrder');">
        <a id="aOrder" href="mOrder.aspx">Place An Order</a>
    </div>
    <div class="hotSpot" onclick="SetFocus('aAddCard');">
        <a id="aAddCard" href="mAddCard.aspx">Save Card On File</a>
    </div>
    <hr />
    <div class="hotSpot" onclick="SetFocus('aShowUserAgent');">
        <a id="aShowUserAgent" onclick="ShowUserAgent();" href="mMenu.aspx">Show Browser/Device
            Type</a>
    </div>
    <script type="text/javascript" language="javascript">

        function ShowUserAgent() {

            alert(navigator.userAgent);

        }

    </script>
</asp:Content>
