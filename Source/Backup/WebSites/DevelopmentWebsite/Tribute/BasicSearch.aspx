<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasicSearch.aspx.cs" Inherits="Tribute_BasicSearch"
    Title="BasicSearch" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
<div id="divShowModalPopup"></div>
    <table style="background-color: #ffffff;">
        <tr>
            <td style="width: 904px">
                <asp:ValidationSummary ID="valsError" runat="server" />
            </td>
        </tr>
     </table>
        
    <table style="height: 30px; background-color: #ffffff;">
       <tr>
       <td style="height: 42px; width: 227px;">
            &nbsp; &nbsp; &nbsp; 
       </td>
         <td style="height: 42px; width: 134px;">
           <asp:Label ID="lblSearch" runat="server" Text="Search For :"></asp:Label>
         </td>
         <td style="width: 221px; height: 42px;">
            <asp:TextBox ID="txtSearch" runat="server" Width="193px">Enter the Name of a Tribute</asp:TextBox>
             <asp:RequiredFieldValidator ID="valSearch" runat="server" ControlToValidate="txtSearch"
                 ErrorMessage="Name is Required field">!</asp:RequiredFieldValidator>
          </td>
          <td style="width: 55px; height: 42px;">
            <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" />
          </td>
          <td style="height: 42px; width: 263px;">
            &nbsp; &nbsp; &nbsp; 
          </td>
       </tr>
    </table>
    
    <table style="height: 30px; background-color: #ffffff;">
       <tr>
        <td style="height: 42px; width: 240px;">
            &nbsp; &nbsp; &nbsp; 
         </td>
         <td style="height: 42px; width: 192px;">
             &nbsp;<asp:RadioButtonList ID="rdoTributeTypeGroup" runat="server">
             </asp:RadioButtonList></td>
         <td style="height: 42px; width: 192px;">
             &nbsp;</td>
          <td style="height: 42px; width: 212px;">
            &nbsp; &nbsp; &nbsp; 
          </td>
        </tr>
        
         <tr>
         <td style="height: 42px; width: 240px;">
            &nbsp; &nbsp; &nbsp; 
         </td>
          <td style="width: 192px; height: 42px;">
              &nbsp;</td>
         <td style="height: 42px; width: 250px;">
            <asp:HyperLink ID="lnkAdvanceSearch" runat="server" NavigateUrl="advancedsearch.aspx">Advance Search</asp:HyperLink> <%--COMDIFF: Is this path correct?--%>
          </td>
          <td style="height: 42px; width: 212px;">
            &nbsp; &nbsp; &nbsp; 
          </td>
          </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
