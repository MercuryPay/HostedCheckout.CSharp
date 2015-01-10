<%@ Page Language="C#" ClientTarget="uplevel" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Theme="MobileEComm"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="Form1" runat="server">
    <div style="text-align:center; font-family:Sans-Serif,arial; font-size:large">
        <div class="pageHeader">
            Product Environment
        </div>
        <br />
        <div>
            <asp:button Text="Desktop Web Browser"  
                runat="server" ID="lbnDesktop" PostBackUrl="~/shoppingcart.aspx"></asp:button>
        </div>
        <br />
        <div>
            <asp:button Text="Mobile Web Browser" 
                runat="server" ID="lbnMobile" PostBackUrl="~/Mobile/mMenu.aspx"></asp:button>
        </div>
    </div>
    </form>
</body>
</html>
