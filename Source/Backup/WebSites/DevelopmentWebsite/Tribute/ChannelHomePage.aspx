<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChannelHomePage.aspx.cs"
    Inherits="Tribute_ChannelHomePage" Title="Your Tribute | Channel Homepage" MasterPageFile="~/Shared/TributePortalHome.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PortalPlaceHolder" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <div class="yt-Hero">
            <h2 id="hHeader" runat="server">
            </h2>
            <h3 id="hCreateLink" runat="server">
            </h3>
            <div class="yt-Panel-System yt-Panel-Share">
                <h2>
                    Create</h2>
                <div class="yt-Panel-Body">
                    <div class="yt-Panel-Photo">
                    </div>
                    <p id="pShare" runat="server">
                    </p>
                </div>
            </div>
            <div class="yt-Panel-System yt-Panel-Send">
                <h2>
                    Invite</h2>
                <div class="yt-Panel-Body">
                    <div class="yt-Panel-Photo">
                    </div>
                    <p id="pSend" runat="server">
                    </p>
                </div>
            </div>
            <div class="yt-Panel-System yt-Panel-Convey">
                <h2>
                    Add</h2>
                <div class="yt-Panel-Body">
                    <div class="yt-Panel-Photo">
                    </div>
                    <p id="pConvey" runat="server">
                    </p>
                </div>
            </div>
            <div class="yt-Hero-Options">
                <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx?Type=<%=tributeName%>" class="yt-CreateTributeButton">
                    Create a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                <%--COMDIFFRES: (we are assigining tributetype in trubutelink variable so it will not create any problem) Is this variable correct?--%>
                <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="yt-TakeTourButton">Take a
                    Tour</a>
            </div>
        </div>
        <!--yt-Hero-->
        <div class="hack-clearBoth">
        </div>
        <!--Added by ADogra-->
        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments") %>
        <%{ %>
        <div id="bigButtons" runat="server" class="bigButtons">
            <p>
                <a id="CreateBtn" runat="server" >Create Website</a>
            </p>
            <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="rightBigButton">Take a Tour</a>
        </div>
        <div class="hack-clearBoth">
        </div>
        <%}
           else
           { %>
        <div class="bigButtons">
            <%-- <asp:LinkButton ID="lnkCreateTribute" runat="server" Text="Create a Tribute"></asp:LinkButton>--%>
            <span id="p1" runat="server"><a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx?Type=<%=tributeName%>">
                Create <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a> </span><a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="rightBigButton">
                    Take a Tour</a>
        </div>
        <%} %>
        <!--Added by ADogra ends here-->
        <div id="adGrid">
            <div id="topQuote">
                <h3>
                    <asp:Label ID="lblTopQuote1" runat="server"></asp:Label></h3>
                <p>
                    <asp:Label ID="lblTopQuote2" runat="server"></asp:Label>
                </p>
            </div>
            <div class="adSpacer20">
            </div>
            <table width="96%" style="left: -20px;" cellpadding="0" cellspacing="0" class="gridTable">
                <tr>
                    <td width="33%">
                        <div id="smallRowOne">
                            <div id="divBodyBox1" runat="server">
                                <h3>
                                    Beautiful Themes</h3>
                                <p>
                                    Choose a theme created by our top designers. Multiple themes are available with
                                    new themes added all the time!</p>
                                <br />
                            </div>
                        </div>
                    </td>
                    <td width="33%">
                        <div id="smallRowOne2">
                            <div id="divBodyBox2" runat="server">
                                <h3>
                                    Personalized URL</h3>
                                <p>
                                    Choose a personalized URL to make it easy to view and share the
                                    <%=tributeName%>
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>. You can also purchase a custom domain name, such as
                                    <%=SiteName %></p>
                            </div>
                        </div>
                    </td>
                    <td width="33%">
                        <div id="smallRowOne3">
                            <div id="divBodyBox3" runat="server">
                                <h3>
                                    Facebook Integration</h3>
                                <p>
                                    One-step login using your Facebook account. Invite Facebook friends to view the
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> and RSVP to events. Easily share content on your wall in one click.</p>
                            </div>
                        </div>
                    </td>
                </tr>
                <%--  </table>--%>
                <%--      <table width="96%" cellpadding="0" cellspacing="0">--%>
                <tr style="vertical-align: top;">
                    <td width="33%">
                        <div id="smallRowTwo">
                            <div id="divBodyBox4" runat="server">
                                <h3>
                                    Unlimited Events</h3>
                                <p>
                                    <asp:Label ID="lblUnlimitedEvent" runat="server" Text="Label"></asp:Label></p>
                                <br />
                            </div>
                        </div>
                    </td>
                    <td width="33%">
                        <div id="smallRowTwo2">
                            <div id="divBodyBox5" runat="server">
                                <h3>
                                    Stylish Invitations</h3>
                                <p>
                                    <asp:Label ID="lblStylist" runat="server" Text="Label"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </td>
                    <td width="33%">
                        <div id="smallRowTwo3">
                            <div id="divBodyBox6" runat="server">
                                <h3>
                                    Advanced RSVP</h3>
                                <p>
                                    Easily import contacts (including Facebook friends) and invite them to your event(s).
                                    Collect meal preferences, manage and export your guest lists, and more.</p>
                                <!--<img src="images/col13.gif" alt="good people"/>-->
                            </div>
                        </div>
                    </td>
                </tr>
                <%-- </table>--%>
                <%--  <table width="96%" cellpadding="0" cellspacing="0" border="0">--%>
                <tr>
                    <td width="33%">
                        <div id="smallRowThree">
                            <div id="divBodyBox7" runat="server">
                                <h3>
                                    <asp:Label ID="lblGuestbook" runat="server" Text=""></asp:Label></h3>
                                <p>
                                    <asp:Label ID="lblNotes" runat="server" Text="Label"></asp:Label>
                                </p>
                                <%--  <%if ((tributeName.ToLower() == "new baby") || (tributeName.ToLower() == "wedding"))
                      { %>
                    <p>
                        Notes can be used like a blog to inform visitors of important thoughts and updates.
                        A note can also be used like a page on a website to include links, graphics and
                        more.</p>
                    <% }%>
                    <%else if (tributeName.ToLower() == "anniversary" || tributeName.ToLower() == "memorial")
                        { %>
                    <p>
                        Friends and family can leave personal messages in your loved one’s online guestbook. They can also give a free virtual gift with a short message.
                       </p>
                    <% }%>
                     <%else if (tributeName.ToLower() == "graduation" || tributeName.ToLower() == "birthday")
                        { %>
                    <p>
                       Friends and family can leave personal messages in the online guestbook. They can also give a free virtual gift to the <%=tributeName .ToLower () %> with a short message.
                       </p>
                    <% }%>--%>
                                <br />
                            </div>
                        </div>
                    </td>
                    <td width="33%">
                        <div id="smallRowThree2">
                            <div id="divBodyBox8" runat="server">
                                <h3>
                                    High Resolution Photos</h3>
                                <p>
                                    <asp:Label ID="lblHighResolution" runat="server"></asp:Label></p>
                            </div>
                        </div>
                    </td>
                    <td width="33%">
                        <div id="smallRowThree3">
                            <div id="divBodyBox9" runat="server">
                                <h3>
                                    Lifetime Storage</h3>
                                <p>
                                    No ongoing payments. No commitment. The
                                    <%=tributeName%>
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> and all of its content will remain online for life! We guarantee it!</p>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="hack-clearBoth">
            </div>
            <div class="testimonialBlock">
                <h1 id="h2Testimonial" runat="server">
                    <%=TestimonialTribute1Line %>
                    with Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</h1>
                <%--<h3 id="h3Testimonial" runat="server">
                    Create a <span class="bold">free</span> personalized <span class="bold">Tribute</span>
                    to <span class="bold">plan</span>, <span class="bold">share</span> and <span class="bold">
                        remember</span> <%= TestimonialTribute2Line%>.</h3>--%>
                <h2 id="h3Testimonial" runat="server" class="h2TestimonialTextSize">
                    <span class="h2TestimonialTextSize1">Create a </span><span class="h2TestimonialTextSize2">
                        free</span> <span class="h2TestimonialTextSize1">personalized </span><span class="h2TestimonialTextSize2">
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></span> <span class="h2TestimonialTextSize1">to </span><span class="h2TestimonialTextSize2">
                                plan</span><span class="h2TestimonialTextSize1">,</span> <span class="h2TestimonialTextSize2">
                                    share</span><span class="h2TestimonialTextSize1"> and </span><span class="h2TestimonialTextSize2">
                                        remember</span>
                    <%= TestimonialTribute2Line%>.</h2>
                <p class="HomePageFontSize">
                    Send online invitations with RSVP, share photos and videos, receive personal guestbook
                    messages and virtual gifts, plus much more!</p>
                <%-- <p id="pTestimonial" runat="server">
                    &quot;Your Tribute is so addictively easy-to-use. It’s so simple you can’t do anything
                    wrong. Simply the best! My first choice for planning an event. This website is so
                    great I can’t begin to tell you how much I love it. It really is the best ever!&quot;
                </p>
                <p class="author">
                    &minus; <i class="brown">Kevin and Jane</i>
                </p>--%>
            </div>
            <div class="divider">
            </div>
            <div id="divTributeBox" runat="server">
                <div class="rightTribute">
                    <p id="pCreateTributeButton" runat="server">
                        <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx?Type=<%=tributeName%>">Create <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                    </p>
                </div>
                <div class="leftTribute">
                    <h2 class="brown">
                        Create your own <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> for free!</h2>
                    <p class="brown">
                        Get started in seconds, no credit card, no commitment.</p>
                </div>
            </div>
        </div>
        <div class="hack-clearBoth">
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
