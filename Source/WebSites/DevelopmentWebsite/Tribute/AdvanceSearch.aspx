<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdvanceSearch.aspx.cs" Inherits="Tribute_AdvanceSearch"
    Title="Advanced Search" MasterPageFile="~/Shared/SearchResult.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    
    function ValidBeforeDate(source, args)
    {   
        var day = Number(document.getElementById('<%= ddlBeforeDay.ClientID %>').value);
        var month = Number(document.getElementById('<%= ddlBeforeMonth.ClientID %>').value);
        var year = Number(document.getElementById('<%= txtBeforeYear.ClientID %>').value);
        
        args.IsValid = SearchValidDate(day, month, year);
        CheckYear(year, args);
    }
    
    function ValidAfterDate(source, args)
    {   
        var day = Number(document.getElementById('<%= ddlAfterDay.ClientID %>').value);
        var month = Number(document.getElementById('<%= ddlAfterMonth.ClientID %>').value);
        var year = Number(document.getElementById('<%= txtAfterYear.ClientID %>').value);
        
        args.IsValid = SearchValidDate(day, month, year);
        CheckYear(year, args);
    }
    
    function DateCompare(source, args)
    {
       var day1 = Number(document.getElementById('<%= ddlAfterDay.ClientID %>').value);
        var month1 = Number(document.getElementById('<%= ddlAfterMonth.ClientID %>').value);
        var year1 = document.getElementById('<%= txtAfterYear.ClientID %>').value;
        
        var day2 = Number(document.getElementById('<%= ddlBeforeDay.ClientID %>').value);
        var month2 = Number(document.getElementById('<%= ddlBeforeMonth.ClientID %>').value);
        var year2 = document.getElementById('<%= txtBeforeYear.ClientID %>').value;
    
        args.IsValid = SearchCompareDates(day1, month1, year1, day2, month2, year2);
    }
    
    function CheckYear(year, args)
    {
        if (args.IsValid && year != "")
        {
            if (year < 1753 || year > 9999)
                args.IsValid = false;
        }
    }
    
    </script>

    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Advanced Search</span>
    </div>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <asp:Panel ID="Panel1" runat="server" Width="100%" DefaultButton="lbtnSearch">
        <div class="yt-ContentPrimary">
            <div>
                <asp:ValidationSummary CssClass="yt-Error" ID="ValidationSummary1" runat="server"
                    ForeColor="Black" HeaderText=" <h2>Oops - there was a problem with your Search.</h2><br/><h3>Please correct the error(s) below:</h3>" />
            </div>
            <div id="lblNotice" runat="server" class="yt-Notice" visible="false">
            </div>
            <div class="yt-Panel-Primary">
                <h2>
                    <asp:Label ID="lblAdvanceSearch" runat="server" Text="Advance Search"></asp:Label></h2>
                <p id="lblRequired" runat="server" class="yt-requiredFields">
                    <strong>Required fields are indicated with <em class="required">* </em></strong>
                </p>
                <fieldset class="yt-Form">
                    <div class="yt-Form-Field">
                        <label id="lblTributeName" runat="server" for="txtTributeName">
                            <em class="required">* </em>Tribute Name:</label>
                        <asp:TextBox ID="txtTributeName" runat="server" CssClass="yt-Form-Input-XLong" MaxLength="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valTributeName" runat="server" ControlToValidate="txtTributeName"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Name is required field">!</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revTributeNameSpecialchar" Font-Bold="True" Font-Size="Medium"
                            ForeColor="#FF8000" ControlToValidate="txtTributeName" ValidationExpression='^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$'
                            runat="server">!</asp:RegularExpressionValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label id="lblTributeType" runat="server" for="ddlType">
                            Tribute Type:</label>
                        <asp:DropDownList ID="ddlTributeType" runat="server" CssClass="yt-Form-DropDown">
                        </asp:DropDownList>
                    </div>
                    <div class="yt-Form-Field">
                        <label id="lblCountry" runat="server" for="ddlCountry">
                            Country:</label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="yt-Form-DropDown-Long"
                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div class="yt-Form-Field">
                        <label id="lblState" runat="server" for="ddlStateProvince">
                            State / Province:</label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="yt-Form-DropDown-Long">
                            <asp:ListItem Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="yt-Form-Field">
                        <label id="lblCity" runat="server" for="txtCity">
                            City:</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="yt-Form-Input-Long" MaxLength="50"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="valCity" runat="server" ControlToValidate="txtCity"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Enter Valid City"
                            ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$">!</asp:RegularExpressionValidator>
                    </div>
                    <fieldset class="yt-Date-Fields">
                        <legend id="lblCreatedAfter" runat="server">Created After:</legend>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlAfterMonth" runat="server" CssClass="yt-Form-DropDown">
                            </asp:DropDownList>
                            <label id="lblAfterMonth" runat="server" for="ddlMonth">
                                Month</label>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlAfterDay" runat="server" CssClass="yt-Form-DropDown-Short">
                                <asp:ListItem Value="00" Text=""></asp:ListItem>
                                <asp:ListItem Value="01" Text="1"></asp:ListItem>
                                <asp:ListItem Value="02" Text="2"></asp:ListItem>
                                <asp:ListItem Value="03" Text="3"></asp:ListItem>
                                <asp:ListItem Value="04" Text="4"></asp:ListItem>
                                <asp:ListItem Value="05" Text="5"></asp:ListItem>
                                <asp:ListItem Value="06" Text="6"></asp:ListItem>
                                <asp:ListItem Value="07" Text="7"></asp:ListItem>
                                <asp:ListItem Value="08" Text="8"></asp:ListItem>
                                <asp:ListItem Value="09" Text="9"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            </asp:DropDownList>
                            <label id="lblAfterDay" runat="server" for="ddlDay">
                                Day</label>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:TextBox ID="txtAfterYear" runat="server" CssClass="yt-Form-Input-Short" MaxLength="4"></asp:TextBox>
                            <asp:Label ID="error1" runat="server" Font-Bold="True"
                        Font-Size="Medium" ForeColor="#FF8000" Text="!" Visible="false"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please provide a valid Created After Year"
                                ControlToValidate="txtAfterYear" ValidationGroup="TributeDetails" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            <%--<asp:CustomValidator ID="valAfterDate" runat="server" ClientValidationFunction="ValidAfterDate"
                                ErrorMessage="Date is not in valid form" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000">!</asp:CustomValidator>--%>
                            <label id="lblAfterYear" runat="server" for="txtYear">
                                Year</label>
                        </div>
                    </fieldset>
                    <fieldset class="yt-Date-Fields">
                        <legend id="lblCreatedBefore" runat="server">Created Before:</legend>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlBeforeMonth" runat="server" CssClass="yt-Form-DropDown">
                            </asp:DropDownList>
                            <label id="lblBeforeMonth" runat="server" for="ddlMonth">
                                Month</label>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlBeforeDay" runat="server" CssClass="yt-Form-DropDown-Short">
                                <asp:ListItem Value="00" Text=""></asp:ListItem>
                                <asp:ListItem Value="01" Text="1"></asp:ListItem>
                                <asp:ListItem Value="02" Text="2"></asp:ListItem>
                                <asp:ListItem Value="03" Text="3"></asp:ListItem>
                                <asp:ListItem Value="04" Text="4"></asp:ListItem>
                                <asp:ListItem Value="05" Text="5"></asp:ListItem>
                                <asp:ListItem Value="06" Text="6"></asp:ListItem>
                                <asp:ListItem Value="07" Text="7"></asp:ListItem>
                                <asp:ListItem Value="08" Text="8"></asp:ListItem>
                                <asp:ListItem Value="09" Text="9"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            </asp:DropDownList>
                            <label id="lblBeforeDay" runat="server" for="ddlDay">
                                Day</label>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:TextBox ID="txtBeforeYear" runat="server" CssClass="yt-Form-Input-Short" MaxLength="4"></asp:TextBox>
                            <asp:Label ID="error2" runat="server" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000" Text="!" Visible="false"></asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please provide a valid Tribute Year."
                                ControlToValidate="txtBeforeYear" ValidationGroup="TributeDetails" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            <%--<asp:CustomValidator ID="valBeforeDate" runat="server" ClientValidationFunction="ValidBeforeDate"
                                ErrorMessage="Date is not in valid form" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000">!</asp:CustomValidator>
                            <asp:CustomValidator ID="valDateCompare" runat="server" ClientValidationFunction="DateCompare"
                                ErrorMessage="Created before date should be greater than the created after date">!</asp:CustomValidator>--%>
                            <label id="lblBeforeYear" runat="server" for="txtYear2">
                                Year</label>
                        </div>
                    </fieldset>
                    <div class="yt-Form-Buttons">
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="yt-Button yt-ArrowButton"
                                OnClick="lbtnSearch_Click">Search</asp:LinkButton>
                        </div>
                    </div>
                </fieldset>
            </div>
            <!--yt-TributeProcess-->
        </div>
        <!--yt-ContentPrimary-->
    </asp:Panel>
</asp:Content>
