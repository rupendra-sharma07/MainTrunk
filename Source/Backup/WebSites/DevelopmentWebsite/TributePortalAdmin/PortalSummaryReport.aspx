<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortalSummaryReport.aspx.cs" Inherits="TributePortalAdmin_PortalSummaryReport"
    Title="PortalSummaryReport" MasterPageFile="PortalAdmin.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
<div id="divShowModalPopup"></div>
		<div style="width:70%">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
		    <tr>
		        <td>
		            <table width="99%"  cellpadding="0" cellspacing="0">
		                <tr>
		                    <td class="LabelHeader">
		                        Users Activity
		                    </td>
		                     <td class="LabelHeader">
		                        Today
		                    </td>
		                     <td class="LabelHeader">
		                        Last 30 Days
		                    </td>
		                    <td class="LabelHeader">
		                        Total Active Accounts
		                    </td>
		                </tr>
		                <tr>
		                    <td>
		                       
		                    </td>
		                    <td class="LabelText">
                                New</td>
		                    <td class="LabelText">
                                New</td>
		                    <td>
		                    </td>
		                </tr>
		                <tr>
		                    <td class="LabelText">
		                        Personal Accounts 
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblPersonalTodayNew" runat="server"></asp:Label><asp:Label ID="lblPersonalTodayExpired" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
		                        <asp:Label ID="lblPersonal30DaysNew" runat="server"></asp:Label><asp:Label ID="lblPersonal30DayExpired" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblPersonalTotalActiveAccounts" runat="server"></asp:Label>
		                    </td>
		                </tr>
		                 <tr>
		                    <td class="LabelText">
		                        Business Accounts 
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblBusinessTodayNew" runat="server"></asp:Label><asp:Label ID="lblBusinessTodayExpired" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
		                        <asp:Label ID="lblBusiness30DaysNew" runat="server"></asp:Label><asp:Label ID="lblBusiness30DaysExpired" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblBusinessTotalActiveAccounts" runat="server"></asp:Label>
		                    </td>
		                </tr>
		                 <tr>
		                    <td class="LabelText">
		                        Total Accounts 
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblTotalTodayNew" runat="server"></asp:Label><asp:Label ID="lblTotalTodayExpired" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
		                        <asp:Label ID="lblTotal30DaysNew" runat="server"></asp:Label><asp:Label ID="lblTotal30DaysExpired" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblTotalActiveAccounts" runat="server"></asp:Label>
		                    </td>
		                </tr>
		            </table> 
		        </td>
		    </tr>
		</table>
		</div>
		<div style="width:70%">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
		    <tr>
		        <td>
		            <table width="99%"  cellpadding="0" cellspacing="0">
		                <tr>
		                    <td class="LabelHeader">
		                        <%=WebsiteWord.ToUpper()%> ACTIVITY
		                    </td>
		                     <td class="LabelHeader">
		                        TODAY
		                    </td>
		                     <td class="LabelHeader">
		                        LAST 30 DAYS
		                    </td>
		                    <td class="LabelHeader">
		                        TOTAL ACTIVE <%=WebsiteWord.ToUpper()%>
		                    </td>
		                </tr>
		                <tr>
		                    <td>
		                       
		                    </td>
		                    <td class="LabelText">
		                        Trial | Expired | 1 Yr | Life
		                    </td>
		                    <td class="LabelText">
		                        Trial | Expired | 1 Yr | Life
		                    </td>
		                    <td class="LabelText">
		                        Trial | Expired | 1 Yr | Life
		                    </td>
		                </tr>
		                <tr>
		                    <td class="LabelText">
		                        <asp:Label ID="lblMemorialType" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblMemorialTodayTrial" runat="server"></asp:Label> | <asp:Label ID="lblMemorialTodayExpired" runat="server"></asp:Label> | <asp:Label ID="lblMemorialToday1Yr" runat="server"></asp:Label> | <asp:Label ID="lblMemorialTodayLife" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
                                <asp:Label ID="lblMemorialLast30DaysTrial" runat="server"></asp:Label> | <asp:Label ID="lblMemorialLast30DaysExpired" runat="server"></asp:Label> | <asp:Label ID="lblMemorialLast30Days1Yr" runat="server"></asp:Label> | <asp:Label ID="lblMemorialLast30DaysLife" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
                                <asp:Label ID="lblMemorialTotalTrial" runat="server"></asp:Label> | <asp:Label ID="lblMemorialTotalExpired" runat="server"></asp:Label> | <asp:Label ID="lblMemorialTotal1Yr" runat="server"></asp:Label> | <asp:Label ID="lblMemorialTotalLife" runat="server"></asp:Label>
		                    </td>
		                </tr>
		                 <tr>
		                    <td class="LabelText">
		                        <asp:Label ID="lblWeddingType" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblWeddingTodayTrial" runat="server"></asp:Label> | <asp:Label ID="lblWeddingTodayExpired" runat="server"></asp:Label> | <asp:Label ID="lblWeddingToday1Yr" runat="server"></asp:Label> | <asp:Label ID="lblWeddingTodayLife" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
                                <asp:Label ID="lblWeddingLast30DaysTrial" runat="server"></asp:Label> | <asp:Label ID="lblWeddingLast30DaysExpired" runat="server"></asp:Label> | <asp:Label ID="lblWeddingLast30Days1Yr" runat="server"></asp:Label> | <asp:Label ID="lblWeddingLast30DaysLife" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
                                <asp:Label ID="lblWeddingTotalTrial" runat="server"></asp:Label> | <asp:Label ID="lblWeddingTotalExpired" runat="server"></asp:Label> | <asp:Label ID="lblWeddingTotal1Yr" runat="server"></asp:Label> | <asp:Label ID="lblWeddingTotalLife" runat="server"></asp:Label>
		                    </td>
		                </tr>
		                <tr>
		                    <td class="LabelText">
		                        <asp:Label ID="lblNewBabyType" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblNewBabyTodayTrial" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyTodayExpired" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyToday1Yr" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyTodayLife" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
                                <asp:Label ID="lblNewBabyLast30DaysTrial" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyLast30DaysExpired" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyLast30Days1Yr" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyLast30DaysLife" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
                                <asp:Label ID="lblNewBabyTotalTrial" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyTotalExpired" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyTotal1Yr" runat="server"></asp:Label> | <asp:Label ID="lblNewBabyTotalLife" runat="server"></asp:Label>
		                    </td>
		                </tr>
		                <tr>
		                    <td class="LabelText">
		                        <asp:Label ID="lblAnniversaryType" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblAnniversaryTodayTrial" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryTodayExpired" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryToday1Yr" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryTodayLife" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
                                <asp:Label ID="lblAnniversaryLast30DaysTrial" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryLast30DaysExpired" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryLast30Days1Yr" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryLast30DaysLife" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
                                <asp:Label ID="lblAnniversaryTotalTrial" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryTotalExpired" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryTotal1Yr" runat="server"></asp:Label> | <asp:Label ID="lblAnniversaryTotalLife" runat="server"></asp:Label>
		                    </td>
		                </tr>
		                <tr>
		                    <td class="LabelText">
		                        <asp:Label ID="lblBirthdayType" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblBirthdayTodayTrial" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayTodayExpired" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayToday1Yr" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayTodayLife" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
                                <asp:Label ID="lblBirthdayLast30DaysTrial" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayLast30DaysExpired" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayLast30Days1Yr" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayLast30DaysLife" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
                                <asp:Label ID="lblBirthdayTotalTrial" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayTotalExpired" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayTotal1Yr" runat="server"></asp:Label> | <asp:Label ID="lblBirthdayTotalLife" runat="server"></asp:Label>
		                    </td>
		                </tr>
		                <tr>
		                    <td class="LabelText">
		                        <asp:Label ID="lblGraduationType" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblGraduationTodayTrial" runat="server"></asp:Label> | <asp:Label ID="lblGraduationTodayExpired" runat="server"></asp:Label> | <asp:Label ID="lblGraduationToday1Yr" runat="server"></asp:Label> | <asp:Label ID="lblGraduationTodayLife" runat="server"></asp:Label>
		                    </td>
		                     <td class="LabelText">
                                <asp:Label ID="lblGraduationLast30DaysTrial" runat="server"></asp:Label> | <asp:Label ID="lblGraduationLast30DaysExpired" runat="server"></asp:Label> | <asp:Label ID="lblGraduationLast30Days1Yr" runat="server"></asp:Label> | <asp:Label ID="lblGraduationLast30DaysLife" runat="server"></asp:Label>
		                    </td>
		                    <td class="LabelText">
                                <asp:Label ID="lblGraduationTotalTrial" runat="server"></asp:Label> | <asp:Label ID="lblGraduationTotalExpired" runat="server"></asp:Label> | <asp:Label ID="lblGraduationTotal1Yr" runat="server"></asp:Label> | <asp:Label ID="lblGraduationTotalLife" runat="server"></asp:Label>
		                    </td>
		                </tr>
		                <tr>
		                    <td class="LabelText">
		                        <asp:Label ID="lblTotal_Type" runat="server" Font-Bold="True"></asp:Label>
		                    </td>
		                    <td class="LabelText">
		                        <asp:Label ID="lblTotal_TodayTrial" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_TodayExpired" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_Today1Yr" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_TodayLife" runat="server" Font-Bold="True"></asp:Label>
		                    </td>
		                     <td class="LabelText">
                                <asp:Label ID="lblTotal_Last30DaysTrial" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_Last30DaysExpired" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_Last30Days1Yr" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_Last30DaysLife" runat="server" Font-Bold="True"></asp:Label>
		                    </td>
		                    <td class="LabelText">
                                <asp:Label ID="lblTotal_TotalTrial" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_TotalExpired" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_Total1Yr" runat="server" Font-Bold="True"></asp:Label><strong> | </strong><asp:Label ID="lblTotal_TotalLife" runat="server" Font-Bold="True"></asp:Label>
		                    </td>
		                </tr>
		            </table> 
		        </td>
		    </tr>
		</table>
		</div>
</asp:Content>
