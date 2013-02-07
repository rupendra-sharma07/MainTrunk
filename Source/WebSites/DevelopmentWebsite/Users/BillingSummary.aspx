<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillingSummary.aspx.cs" Inherits="Users_BillingSummary"
    Title="BillingSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <!--#include file="../analytics.asp"-->
</head>

<script language="javascript">
function Printwindow()
{
     document.form1.btnPrint.style.visibility = 'hidden';
     document.form1.btnClose.style.visibility = 'hidden';
       window.print();
       closeWindow();

}
function closeWindow()
{
 this.close();
}

</script>

<body>
    <form id="form1" runat="server">
    <div id="divShowModalPopup"></div>
        <div>
            <table>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblPaymentReceipt" Font-Size="Large" runat="server" Text="Payment Receipt"
                            Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:FormView ID="FormView1" runat="server" Width="256px">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td align="left">
                                <b>Tribute Details</b></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Tribute Name:</b></td>
                            <td align="left">
                                <asp:Label ID="ProductIDLabel" runat="server" Text='<%# Eval("Tributename") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Tribute Type:</b></td>
                            <td align="left">
                                <asp:Label ID="ProductNameLabel" runat="server" Text='<%# Eval("TypeDescription") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Package Type:</b></td>
                            <td align="left">
                                <asp:Label ID="CategoryIDLabel" runat="server" Text='<%# Eval("Packagename") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Expiry Date:</b></td>
                            <td align="left">
                                <asp:Label ID="QuantityPerUnitLabel" runat="server" Text='<%# Eval("Enddate","{0:MMMM dd,yyyy}") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Billing Address:</b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Name:</b></td>
                            <td align="left">
                                <asp:Label ID="UnitPriceLabel" runat="server" Text='<%# Eval("CardholdersName") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Address:</b></td>
                            <td align="left">
                                <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>City:</b></td>
                            <td align="left">
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("City") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>State:</b></td>
                            <td align="left">
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("State") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Country:</b></td>
                            <td align="left">
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Country") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Zip/Postal Code:</b></td>
                            <td align="left">
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("Zip") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Telephone:</b></td>
                            <td align="left">
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("Telephone") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Billing Details:</b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Billing Date:</b></td>
                            <td align="left">
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("StartDate","{0:MMMM dd,yyyy}") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Payment Type:</b></td>
                            <td align="left">
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("CreditCardType") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Credit Card:</b></td>
                            <td align="left">
                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("CreditCardNo") %>' /></td>
                        </tr>
                        <tr>
                            <td align="left">
                                <b>Amount Billed:</b></td>
                            <td align="left">
                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("AmountPaid") %>' /></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
            <br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <input id="btnPrint" type="button" onclick="Printwindow();" value="Print Receipt" />
            <input id="btnClose" type="button" onclick="closeWindow();" value="Close" /></div>
    </form>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>
</body>
</html>
