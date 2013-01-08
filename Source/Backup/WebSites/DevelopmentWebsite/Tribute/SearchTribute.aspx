<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchTribute.aspx.cs" Inherits="Tribute_SearchTribute"
    Title="SearchTribute" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
	 <div id="divShowModalPopup"></div>
	<table style="height: 30px; background-color: #ffffff;">
       <tr>
           <td style="width: 511px; height: 34px;">
           </td>
           <td style="width: 188px; height: 34px;">
               <asp:DropDownList ID="ddlTributeType" runat="server" OnSelectedIndexChanged="ddlTributeType_SelectedIndexChanged" AutoPostBack="True">
               </asp:DropDownList></td>
           <td style="width: 197px; height: 34px;">
               <asp:DropDownList ID="ddlSort" runat="server" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged" AutoPostBack="True">
               </asp:DropDownList>
           </td>
       </tr>
   </table>

   <table style="height: 30px; background-color: #ffffff;">
       <tr>
            <td style="width: 902px; height: 22px;">
                <asp:ValidationSummary ID="valsError" runat="server" />
           </td>
       </tr>
   </table>
   
    <table style="height: 30px; background-color: #ffffff;">
       <tr>
           <td style="width: 896px">
            <asp:Label ID="lblTotalRecords" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
           <td style="width: 896px">
           <asp:Label ID="lblDisplayedRecords" runat="server" ></asp:Label>
           </td>
        </tr>
   </table>
    
    <table style="height: 30px; background-color: #ffffff;">
        <tr>
           <td style="width: 188px">
           </td>
            <td>
              <asp:label ID="lblPage" runat="server">Page:</asp:label>
            </td>
            <td>
                   <asp:LinkButton ID="lbtnPre" runat="server" CausesValidation="false" Enabled="false" OnClick="lbtnPre_Click" >Prev</asp:LinkButton>
            </td>                            
            <td  align="right">
                <asp:DataList ID="dlstIndex" runat="server" RepeatDirection="Horizontal" OnItemCommand="dlstIndex_ItemCommand">
                        <ItemTemplate>
                        <asp:LinkButton ID="lbtnIndex" Text='<%# Eval("Count") %>'  runat="server" CausesValidation="false">LinkButton</asp:LinkButton>
                        </ItemTemplate>
                </asp:DataList>
            </td>
            <td style="width: 26px">
                   <asp:LinkButton ID="lbtnNext" runat="server" CausesValidation="false" OnClick="lbtnNext_Click" >Next</asp:LinkButton>
            </td>
            <td style="width: 146px">
            </td>
        </tr>
    </table>
    
    <table style="height: 30px; background-color: #ffffff; width: 909px;">
       <tr>
           <td>
            <asp:Repeater ID="repSearchTribute" runat="server" Visible="False">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td align="left"  valign="middle" >
                                <img id="imgTributeImage" alt="" height="100" src='<%#Eval("TributeImage")%>' width="100" />
                            </td>
                            
                            <td align="center" valign="middle">
                                <asp:LinkButton ID="lbtnTributeName" Text= '<%#Eval("TributeName")%>' runat="server">LinkButton</asp:LinkButton>
                                <br><br>
                                <asp:Label ID="lblTributeType" runat="server" Text= '<%#Eval("TributeType")%>'></asp:Label>                            
                            </td>                        
                            
                            <td align="center" valign="middle" >
                               <br><br>&nbsp; &nbsp;
                                <asp:Label ID="lblLocation" runat="server" Text= '<%#Eval("Location")%>'></asp:Label>                                   
                            </td>
                            
                            <td align="center" valign="middle" >
                               <br><br>&nbsp; &nbsp;
                                <asp:Label ID="lblHits" runat="server" Text= '<%#Eval("CreatedDate")%>'></asp:Label>                                   
                            </td>
                            
                            <td align="right" valign="top">
                                <asp:Label ID="lblDate1" runat="server" Text= '<%#Eval("Date1")%>'></asp:Label>                               
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
           </td>
       </tr>
   </table>    
   
   <table style="height: 30px; background-color: #ffffff;">
       <tr>
           <td style="width: 188px">
           </td>
           <td style="width: 324px">
            <asp:Label ID="lblOption" runat="server" ></asp:Label>
            </td>
           <td>
            <asp:HyperLink ID="lnkAdvanceSearch" runat="server" NavigateUrl="advancedsearch.aspx"></asp:HyperLink> <%--COMDIFF: Is this path correct?--%>
           </td>
           <td style="width: 230px">
           </td>
        </tr>
   </table>
</asp:Content>
