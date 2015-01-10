using System;
using TransactionServiceGift;

public static class GiftHelper
{
    public static GiftResponse ProcessGiftSale(string amount, string mid, string invoice, string cvv, string account, string pwd, bool partialAuth)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftSale request = new GiftSale()
        {
            PurchaseAmount = Convert.ToDouble(amount),
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftSale from eComm Demo",
            CVV = cvv,
            Account = account,
             PartialAuth = partialAuth,
        };

        return ts.GiftSale(request, pwd);
    }

    public static GiftResponse ProcessGiftIssue(string amount, string mid, string invoice, string account, string pwd)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftIssue request = new GiftIssue()
        {
            PurchaseAmount = Convert.ToDouble(amount),
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftIssue from eComm Demo",
            Account = account,
        };

        return ts.GiftIssue(request, pwd);
    }

    public static GiftResponse ProcessGiftReload(string amount, string mid, string invoice, string account, string cvv, string pwd)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftReload request = new GiftReload()
        {
            PurchaseAmount = Convert.ToDouble(amount),
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftReload from eComm Demo",
            Account = account,
            CVV = cvv,
        };

        return ts.GiftReload(request, pwd);
    }

    public static GiftResponse ProcessGiftVoidReload(string amount, string mid, string invoice, string account, string refNo, string cvv, string pwd)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftVoidReload request = new GiftVoidReload()
        {
            PurchaseAmount = Convert.ToDouble(amount),
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftReload from eComm Demo",
            Account = account,
            RefNo = refNo,
            CVV = cvv,
        };

        return ts.GiftVoidReload(request, pwd);
    }

    public static GiftResponse ProcessGiftVoidIssue(string amount, string mid, string invoice, string account, string refNo, string pwd)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftVoidIssue request = new GiftVoidIssue()
        {
            PurchaseAmount = Convert.ToDouble(amount),
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftReload from eComm Demo",
            Account = account,
            RefNo = refNo,
        };

        return ts.GiftVoidIssue(request, pwd);
    }

    public static GiftResponse ProcessGiftVoidSale(string amount, string mid, string invoice, string account, string refNo, string pwd)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftVoidSale request = new GiftVoidSale()
        {
            PurchaseAmount = Convert.ToDouble(amount),
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftReload from eComm Demo",
            Account = account,
            RefNo = refNo,
        };

        return ts.GiftVoidSale(request, pwd);
    }

    public static GiftResponse ProcessGiftBalance(string mid, string invoice, string cvv, string account, string pwd)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftBalance request = new GiftBalance()
        {         
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftBalance from eComm Demo",
            CVV = cvv,
            Account = account,
         
        };

        return ts.GiftBalance(request, pwd);
    }

    public static GiftResponse ProcessGiftReturn(string amount, string mid, string invoice, string cvv, string account, string pwd)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftReturn request = new GiftReturn()
        {
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftBalance from eComm Demo",
            CVV = cvv,
            Account = account,
            PurchaseAmount = Convert.ToDouble(amount),
        };

        return ts.GiftReturn(request, pwd);
    }
                                                    
    public static GiftResponse ProcessGiftVoidReturn(string amount, string mid, string invoice, string cvv, string account, string pwd, string refno)
    {
        TransactionServiceGift.TransactionServiceGift ts = new TransactionServiceGift.TransactionServiceGift();

        GiftVoidReturn request = new GiftVoidReturn()
        {
            MerchantID = mid,
            Invoice = invoice,
            TerminalName = "eCommDemo",
            OperatorID = "Demo",
            Memo = "GiftBalance from eComm Demo",
            CVV = cvv,
            Account = account,
            RefNo = refno,
            PurchaseAmount = Convert.ToDouble(amount),
        };

        return ts.GiftVoidReturn(request, pwd);
    }


}