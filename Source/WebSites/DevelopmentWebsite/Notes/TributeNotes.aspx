<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TributeNotes.aspx.cs" Inherits="Notes_TributeNotes"
    Title="Notes" MasterPageFile="~/Shared/Story.master" EnableEventValidation="false" %>
<%@ MasterType  virtualPath="~/Shared/Story.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <div class="yt-Breadcrumbs">
  <%--  <script type="text/javascript" src="<%=Session["APP_PATH"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_PATH"]%>assets/scripts/modalbox.js"></script>--%>
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <span class="selected">Notes</span>
    </div>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
<div id="divShowModalPopup"></div>
    <div class="yt-ContentPrimary"  id="divContentContainer1">
        <div class="yt-Panel-Primary">
            <h2>
                Notes</h2>
             <div class="yt-Error" id="Error" runat="server" visible="false"></div>
            
            <div class="yt-NoteList">
                <div id="divAddNote" runat="server" class="yt-Form-Buttons">
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnAddNote" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lbtnAddNote_Click"></asp:LinkButton>
                    </div>
                </div>
                <div id="divForTopPaging" runat="server" class="yt-ListHead">
                    <span id="spanRecordCountTop" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spanPageTop" runat="server"></span><span id="spanPagingTop" runat="server">
                        </span>
                    </div>
                </div>
                <div id="divNoRecord" runat="server">
                </div>
                <div id="divNotes" runat="server" class="yt-ListBody">
                    <asp:DataList ID="dlNotes" runat="server" OnDeleteCommand="dlComments_DeleteCommand"
                        OnItemDataBound="dlComments_ItemDataBound">
                        <ItemTemplate>
                            <div class="yt-ListItem">
                                <h4>
                                    <a href="note.aspx?noteId=<%#Eval("NotesId")%>">
                                        <%#Eval("Title")%>
                                    </a>
                                </h4>
                                <h6>
                                    <a href="javascript:void(0);" onclick="UserProfileModal_1('<%# Eval("CreatedBy")%>');"
                                        class="yt-ListName">
                                        <%#Eval("UserName")%>
                                    </a>
                                    <%#Eval("Location")%>
                                </h6>
                                <span class="yt-ItemPhoto" runat="server" id="itemProfilePicSpn">
                                </span>
                                <img class="yt-ItemPhoto" runat="server" id="itemProfilePicImg" src="" />
                                <p class="yt-ItemDate">
                                    <%=at %>
                                    <%#Eval("CreationTime")%>
                                    <%=on %>
                                    <%#Eval("CreationDate")%>
                                    .
                                </p>
                                <p>
                                    <%#Eval("PostMessage")%>
                                </p>
                                <div class="yt-ItemTools">
                                    <a href="note.aspx?noteId=<%#Eval("NotesId")%>" class="yt-More">
                                        <%=clickHere%>
                                    </a><a href="note.aspx?noteId=<%#Eval("NotesId")%>" class="yt-Comments">
                                        <%=comments%>
                                        (<%#Eval("CommentCount")%>)</a>
                                </div>
                                <asp:LinkButton ID="btnDelete" CommandArgument='<%# Eval("NotesId") %>' runat="server"
                                    CssClass="yt-MiniButton yt-DeleteButton" Text="Delete" CommandName="Delete" CausesValidation="false" />
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div id="divForBottomPaging" runat="server" class="yt-ListFoot">
                    <span id="spanRecordCountBottom" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spanPageBottom" runat="server"></span><span id="spanPagingBottom" runat="server">
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
