<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminConfirmation.aspx.cs" Inherits="Tribute_AdminConfirmation"
    Title="AdminConfirmation" MasterPageFile="~/Shared/User.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="UserContentPlaceHolder" runat="server">

<script language="javascript">
 function ValidateConfirmation(source, args)
 {
//        args.IsValid = document.getElementById('<%= rdbDecline.ClientID %>').checked;        
        if((!document.getElementById('<%= rdbAccept.ClientID %>').checked)&&(!document.getElementById('<%= rdbDecline.ClientID %>').checked))
        {
            args.IsValid=false;
        }
         
 } 


</script>
<div id="divShowModalPopup"></div>

    <div class="yt-ContentPrimaryContainer" style="width: 98%;text-align:center;">
		    <div>			    
                <div style="text-align:left">
                    <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server" Width="627px" 
                    HeaderText=" <h2>Oops - there was a problem with your sign up.</h2>                                                             <h3>Please correct the errors below:</h3>" ForeColor="Black" Height="82px" />                       
                 </div>
                
                <div class="yt-Panel-Primary"> 
                <div style="text-align:center">
                   <table style="width: 606px; height: 56px;background-color: #ffffff;">
                   <tr>
                   <td style="height: 22px; width: 454px;">
                   <asp:Label ID="lblHeader" runat="server" Text="You have been selected to be an administrator of the Tribute: " Width="438px" Height="6px" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
                   </td>
                   <td style=" height: 22px; width: 132px;">
                           <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" Font-Bold="True">Tribute Name.</asp:LinkButton>
                   </td>
                   </tr>
                    </table>
                    
                    
                  </div>               
                   	<table   style="width: 637px; height: 202px;">
               
                <tr >                
                    <td style="width: 105px; height: 217px;" valign="top">    
                      <div>                     &nbsp;</div>   
                    <table class="yt-Error" style="width: 617px; height: 160px;">
                        <tr style="width: 111px">
                            <td style="width: 285px">
                        <asp:Label ID="Label1" runat="server" Text="A tribute administrator has complete control over the tribute. Administrators can modify the tribute settings (theme, privacy, tribute details) and can manage content (delete photos, guestbook entries, comments)" Width="612px"></asp:Label></td>
                        </tr>
                    <tr style="width: 111px">
                    <td style="width: 285px" >
                    </td>
                    </tr>
                    <tr style="width: 111px">
                    <td style="width: 285px" >
                        &nbsp; &nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="Would you like to become an administrator?"></asp:Label>
                     
                    </td>
                    </tr>
                    <tr style="width: 111px">
                    <td style="width: 285px" >                     &nbsp; &nbsp; &nbsp;
                        <asp:RadioButton ID="rdbAccept" runat="server" Text="Accept " Width="113px" GroupName="AdminConfirmation" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rdbDecline" runat="server" Text="Decline" Width="99px" GroupName="AdminConfirmation" />&nbsp;
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateConfirmation"
                            ErrorMessage="Please select Accept or Decline">!</asp:CustomValidator></td>
                    </tr>
                    <tr style="width: 111px">
                    <td style="width: 285px" >                     &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                        <asp:Button ID="Button1" runat="server" Text="Submit" /></td>
                    </tr>
                    </table>     
                   </td>                    
                   </tr>         
                </table>		      			    
    			    <!--yt-Form-->
                    </div><!--fieldset-->
          </div><!--yt-ContentPrimary-->
	    </div><!--yt-ContentPrimaryContainer-->  <!--yt-ContentSecondary-->
	    
	   
</asp:Content>
