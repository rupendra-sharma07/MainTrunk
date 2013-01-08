<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TributeAdmin.aspx.cs" Inherits="Tribute_TributeAdmin"
    Title="TributeAdmin" MasterPageFile="~/Shared/User.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="UserContentPlaceHolder" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div align="center" class="yt-ContentPrimaryContainer" style="width: 98%">
        <div>
            <div align="left">
                <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
                    Width="627px" HeaderText=" <h2>Oops - there was a problem with your sign up.</h2>                                                             <h3>Please correct the errors below:</h3>"
                    ForeColor="Black" Height="82px" />
            </div>
            <div class="yt-Panel-Primary">
                <div align="center">
                    <table style="width: 592px; height: 56px;" bgcolor="#ffffff">
                        <tr>
                            <td style="height: 22px; width: 454px;">
                                <asp:Label ID="lblHeader" runat="server" Text="You have been selected to be an administrator of the Tribute: "
                                    Width="438px" Height="6px" BackColor="White" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                            <td style="height: 22px">
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" Font-Bold="True">Tribute Name.</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 637px; height: 202px;">
                    <tr>
                        <td style="width: 105px; height: 217px;" valign="top">
                            <div>
                                <asp:Label ID="Label1" runat="server" Text="Already a member of Your Tribute?" Width="294px"></asp:Label>
                            </div>
                            <table class="yt-Error" style="width: 291px; height: 160px;">
                                <tr style="width: 111px">
                                    <td>
                                        <label for="TextBox1">
                                            <em class="required">* </em>Username:</label>
                                        <asp:TextBox ID="txtUserName" runat="server" Width="229px" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                            ErrorMessage="Enter User Name">!</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="width: 111px">
                                    <td>
                                        <label for="TextBox2">
                                            <em class="required">* </em>Password:</label>
                                        <asp:TextBox ID="txtPassword" runat="server" Width="229px" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                            ErrorMessage="Enter Password">!</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="width: 111px">
                                    <td>
                                    </td>
                                </tr>
                                <tr style="width: 111px">
                                    <td>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Width="111px" CausesValidation="False">Forgot Password?</asp:LinkButton>
                                        &nbsp;&nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btnSignIn" runat="server" Text="Sign In" OnClick="Button1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 105px; height: 217px;" valign="top" align="right">
                            <div>
                                <asp:Label ID="Label2" runat="server" Text="Not a member yet?" Width="294px"></asp:Label>
                            </div>
                            <table class="yt-Error" style="width: 291px; height: 160px;">
                                <tr style="width: 111px">
                                    <td>
                                    </td>
                                </tr>
                                <tr style="width: 111px">
                                    <td>
                                        &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" OnClick="LinkButton3_Click">Sign-Up</asp:LinkButton>&nbsp;
                                    </td>
                                </tr>
                                <tr style="width: 111px">
                                    <td style="height: 21px">
                                        <label>
                                            <em class="required"></em>to become a member of Your Tribute,and contribute to existing
                                            tributes – or createyour own!</label>
                                    </td>
                                </tr>
                                <tr style="width: 111px">
                                    <td>
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                    </td>
                                </tr>
                                <tr style="width: 111px">
                                    <td>
                                        <label for="TextBox2">
                                            <em class="required"><strong><span style="color: #000000">. </span></strong></em>
                                            It’s fast<br />
                                        </label>
                                        <label for="TextBox2">
                                            <em class="required"><span style="color: #000000"><strong>.</strong></span> </em>
                                            It’s free</label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!--yt-Form-->
            </div>
            <!--fieldset-->
        </div>
        <!--yt-ContentPrimary-->
    </div>
    <!--yt-ContentPrimaryContainer-->
    <!--yt-ContentSecondary-->
    <!--yt-upgrade-->
</asp:Content>
