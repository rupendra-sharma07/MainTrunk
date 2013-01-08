<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSummaryReport.aspx.cs"
    Inherits="TributePortalAdmin_UserSummaryReport" Title="UserSummaryReport" MasterPageFile="PortalAdmin.master" %>

<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" runat="Server">

    <script language="javascript" type="text/javascript">
function checkdate(input)
{
    var monthfield=input.split("/")[0];
    var dayfield=input.split("/")[1];
    var yearfield=input.split("/")[2];
    //alert(monthfield);
    var dayobj = new Date(yearfield, monthfield-1, dayfield);
    if ((dayobj.getMonth()+1!=monthfield)||(dayobj.getDate()!=dayfield)||(dayobj.getFullYear()!=yearfield))
        returnval = false;
    else
        returnval=true; 
    
    return returnval;
}

function CheckBeforeDate(source, args)
{
    var month = document.getElementById('<%=drpBeforeMonth.ClientID%>').value;
    var day = document.getElementById('<%=drpBeforeDay.ClientID%>').value;
    var year = document.getElementById('<%=txtBeforeYear.ClientID%>').value;
    
    var enteredDate = month + "/" + day + "/" + year;
    if (month != '' || day != '' || year != '')
    {
        if (!checkdate(enteredDate))
        {
            args.IsValid = false;
        }
	}
}

function CheckAfterDate(source, args)
{
    var month = document.getElementById('<%=drpAfterMonth.ClientID%>').value;
    var day = document.getElementById('<%=drpAfterDay.ClientID%>').value;
    var year = document.getElementById('<%=txtAfterYear.ClientID%>').value;
    
    var enteredDate = month + "/" + day + "/" + year;
    if (month != '' || day != '' || year != '')
    {
        if (!checkdate(enteredDate))
        {
            args.IsValid = false;
        }
	}
}

function ChkDate(source, args)
{
    var monthAfter = parseInt(document.getElementById('<%=drpAfterMonth.ClientID%>').value);
    var dayAfter = parseInt(document.getElementById('<%=drpAfterDay.ClientID%>').value);
    var yearAfter = parseInt(document.getElementById('<%=txtAfterYear.ClientID%>').value);
    var monthBefore = parseInt(document.getElementById('<%=drpBeforeMonth.ClientID%>').value);
    var dayBefore = parseInt(document.getElementById('<%=drpBeforeDay.ClientID%>').value);
    var yearBefore = parseInt(document.getElementById('<%=txtBeforeYear.ClientID%>').value);
    
    if (yearBefore < yearAfter)
    {
        args.IsValid = false;
    }
    else if (yearBefore == yearAfter)    
    {
        if (monthBefore < monthAfter)
            args.IsValid = false;
        else if (monthBefore == monthAfter)
        {
            if (dayBefore < dayAfter)
                args.IsValid = false;
        }
    }
}
function dispconfirm()
{
var r=confirm("Are you sure you want to delete the User?");
if (r==true)
  {
    return true;
  }
else
  {
    return false;
  }
}

function CheckFields()
{
    
    var userId = document.getElementById('<%=txtUserId.ClientID%>').value;
    var userName = document.getElementById('<%=txtUsername.ClientID%>').value;
    var firstName = document.getElementById('<%=txtFirstName.ClientID%>').value;
    var lastName = document.getElementById('<%=txtLastName.ClientID%>').value;
    var email = document.getElementById('<%=txtEmailAddress.ClientID%>').value;
    var city = document.getElementById('<%=txtCity.ClientID%>').value;
    var country = document.getElementById('<%=ddlCountry.ClientID%>').value;
    var stateProvince = document.getElementById('<%=ddlStateProvince.ClientID%>').value;
    var chkPersonal = document.getElementById('<%=chkPersonal.ClientID%>');
    var chkBusiness = document.getElementById('<%=chkBusiness.ClientID%>');
    var monthAfter = document.getElementById('<%=drpAfterMonth.ClientID%>').value;
    var dayAfter = document.getElementById('<%=drpAfterDay.ClientID%>').value;
    var yearAfter = document.getElementById('<%=txtAfterYear.ClientID%>').value;
    var monthBefore = document.getElementById('<%=drpBeforeMonth.ClientID%>').value;
    var dayBefore = document.getElementById('<%=drpBeforeDay.ClientID%>').value;
    var yearBefore = document.getElementById('<%=txtBeforeYear.ClientID%>').value;
    if (userId == '' && userName == '' && firstName == '' && lastName == '' && email == '' && city == '' && country == '-1' && stateProvince == '-1' && chkPersonal.checked == false && chkBusiness.checked == false && monthAfter == '' && dayAfter == '' && yearAfter == '' && monthBefore == ''  && dayBefore == '' && yearBefore == '')
    {
        alert("Please enter some search critera");
        return false;
    }
    else
    {
        return true;
    }
}


    </script>
<div id="divShowModalPopup"></div>
    <asp:Panel ID="pnlSearchUser" runat="server" style="float:left; width:70%" DefaultButton="btnSearch">    
        <table width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" />
                    <asp:Label runat="server" Visible="false" ID = "lblErrorMessage" ForeColor="Red"  />
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" class="LabelHeader">
                    <strong>User Search & Reports</strong> 
                </td>
            </tr>
            <tr>
                <td align="right" class="LabelText">
                    User Id</td>
                <td>
                    <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" class="LabelText">
                    User Name</td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="LabelText">
                    First Name / Business Name</td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="LabelText">
                    Last Name</td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="LabelText">
                    Email Address</td>
                <td>
                    <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="LabelText">
                    City</td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
            </tr>
             <tr>
                <td style="height: 28px" class="LabelText">
                    Country</td>
                <td style="height: 28px" class="LabelText">
                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="LabelText">
                    State</td>
                <td>
                    <asp:DropDownList ID="ddlStateProvince" runat="server">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="LabelText">
                    Account Type</td>
                <td class="LabelText">
                    <asp:CheckBox ID="chkPersonal" runat="server" />Personal
                    <asp:CheckBox ID="chkBusiness" runat="server" />Business</td>
            </tr>
            <tr>
                <td colspan="2" class="LabelText">
                    Created After:<asp:DropDownList ID="drpAfterMonth" runat="server">
                        <asp:ListItem Value="">--</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>&nbsp;<asp:DropDownList ID="drpAfterDay" runat="server">
                    </asp:DropDownList>&nbsp;<asp:TextBox ID="txtAfterYear" MaxLength="4" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:CustomValidator ID="cvCreatedAfter" runat="server" ClientValidationFunction="CheckAfterDate"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator></td>
            </tr>
            <tr>
                <td colspan="2" class="LabelText">
                    Created Before:<asp:DropDownList ID="drpBeforeMonth" runat="server">
                        <asp:ListItem Value="">--</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>&nbsp;<asp:DropDownList ID="drpBeforeDay" runat="server">
                    </asp:DropDownList>&nbsp;<asp:TextBox ID="txtBeforeYear" MaxLength="4" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:CustomValidator ID="cvCreatedBefore" runat="server" ClientValidationFunction="CheckBeforeDate"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator></td>
            </tr>
            <tr>
                <td>
                    <asp:CustomValidator ID="cvChkDate" runat="server" ClientValidationFunction="ChkDate"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />&nbsp;
                    <asp:Button ID="btnGenerateReport" runat="server" Text="Generate .XLS Report" OnClick="btnGenerateReport_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="Table1" width="100%" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblNoRecord" CssClass="LabelText" Visible="false" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="dgUsersList" Style="z-index: 101; left: 24px;" runat="server" BorderColor="#000003"
                    BorderWidth="1px" CellPadding="0" OnRowCommand="dgUsersList_Command"
                    Font-Names="Verdana" Font-Size="9pt" HeaderStyle-BackColor="#B6C6D3" AutoGenerateColumns="False"
                    Width="905px" Height="56px" BackColor="#EEEEEE" OnRowDataBound="dgUsersList_RowDataBound" OnRowDeleting="dgUsersList_RowDeleting">
                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" BackColor="#B6C6D3"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblUserId" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="../MyHome/AdminProfileSettings.aspx?id={0}"
                            DataTextField="UserId" HeaderText="USER ID" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="UserName" HeaderText="USER NAME" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstName" HeaderText="FIRST NAME / BUSINESS NAME" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LastName" HeaderText="LAST NAME" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="City" HeaderText="CITY" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StateName" HeaderText="STATE" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CountryName" HeaderText="COUNTRY" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AccountType" HeaderText="ACCOUNT TYPE" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreationDate" HeaderText="DATE CREATED" >
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="false" CommandName="Delete" CommandArgument='<%# Eval("UserId") %>'
                                    Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle Font-Names="Verdana" Font-Size="7pt" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
