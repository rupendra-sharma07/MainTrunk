<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterHome.ascx.cs" Inherits="UserControl_FooterHome" %>

<script type="text/javascript" language="javascript" src="../Common/JavaScript/FooterControl.js"></script>

<script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>

<script src="https://platform.twitter.com/widgets.js" type="text/javascript"></script>

<script type="text/javascript" language="Javascript">
    function echeck(str) {

        var at = "@";
        var dot = ".";
        var lat = str.indexOf(at);
        var lstr = str.length;

        if (str.indexOf(dot) == lstr - 1) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(at) == -1) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(at, (lat + 1)) != -1) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(dot, (lat + 2)) == -1) {
            alert("Invalid E-mail ID");
            return false;
        }

        if (str.indexOf(" ") != -1) {
            alert("Invalid E-mail ID");
            return false;
        }

        return true;
    }


    function validateEmail(email) {

        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var address = email.value;
        if (reg.test(address) == false) {

            alert('Invalid Email Address');
            return false;
        }
    }

    function ValidateForm() {
        var emailID = document.getElementById("<%= txtEmailAddress.ClientID%>");
        if ((emailID.value == null) || (emailID.value == "")) {
            alert("Please Enter your Email ID");
            emailID.focus();
            return false;
        }
        if (echeck(emailID.value) == false) {
            emailID.value = "";
            emailID.focus();
            return false;
        }
        return true;
    }

    function ValidateForm1() {
        var emailID = document.getElementById("<%= TextBox1.ClientID%>");
        if ((emailID.value == null) || (emailID.value == "")) {
            alert("Please Enter your Email ID");
            emailID.focus();
            return false;
        }
        if (echeck(emailID.value) == false) {
            emailID.value = "";
            emailID.focus();
            return false;
        }
        return true;
    }
    
</script>

<div class="hack-clearBoth">
</div>
<div class="yt-FooterContainer">
    <div id="yourtributeFooter" runat="server" class="yt-FooterFH">
        <div class="yt-FooterNewsletter">
            <div style="float: left; height: 40px; width: 350px;">
                <a href="<%= HomeUrl() %>" title="<%= LogoTitle() %>" id="yt-logoImage"></a>
            </div>
            <div style="float: left; height: 30px; width: 350px;">
                <h6 class="OceanBlue-MT">
                    Subscribe to our Newsletter</h6>
            </div>
            <div style="float: left; height: 52px; width: 350px;">
                <asp:TextBox ID="txtEmailAddress" runat="server" Text="Enter your email address"
                    CssClass="yt-Txt"></asp:TextBox>
                <asp:Button ID="btnSubscribe" runat="server" Text="Subscribe" CssClass="yt-Btn" OnClick="btnSubscribe_Click" />
                <!--Script to clear the text box on focus-->

                <script type="text/javascript">

                    var txtbox = document.getElementById("<%= txtEmailAddress.ClientID%>");
                    txtbox.onfocus = function() {
                        if (this.defaultValue == this.value);
                        this.value = "";
                    };
                </script>

            </div>
            <div style="min-height: 35px; width: 350px; color: Red;">
                <asp:Label ID="lblResult" runat="server"></asp:Label>
            </div>
            <div>
                <!-- AddThis Button BEGIN -->
                <div>
                    <div style="float: left; width: 70px">
                        <g:plusone size="medium" href="https://www.yourtribute.com"></g:plusone>
                    </div>
                    <div style="float: left;">
                        <div class="fb-like" data-href="https://www.facebook.com/yourtribute" data-send="false"
                            data-layout="button_count" data-show-faces="false" data-font="lucida grande">
                        </div>
                    </div>
                    <div style="float: left; width: 150px">
                        <a href="https://twitter.com/yourtribute" class="twitter-follow-button">Follow @yourtribute</a>
                    </div>
                </div>
                <!-- AddThis Button END -->
            </div>
        </div>
        <div class="yt-FooterSitemap-MT">
            <div class="yt-FooterNavColumn-MT">
                <h6 class="Purple-MT">
                    Explore</h6>
                <ul>
                    <li class="FooterLi"><a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx">Tour</a></li>
                    <li class="FooterLi"><a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx">Features</a></li>
                    <li class="FooterLi"><a href="<%=Session["APP_BASE_DOMAIN"]%>themes.aspx">Themes</a></li>
                    <li class="FooterLi"><a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Pricing &
                        Sign Up</a></li>
                    <li class="FooterLi"><a href="<%=Session["APP_BASE_DOMAIN"]%>advancedsearch.aspx">Advanced
                        Search</a></li>
                    <li class="FooterLi"><a href="http://support.yourtribute.com" target="_blank">Help</a></li>
                </ul>
            </div>
            <div class="yt-FooterNavColumn-MT">
                <h6 class="Purple-MT">
                    Connect</h6>
                <ul>
                    <li class="FooterLi"><a href="<%=Session["APP_BASE_DOMAIN"]%>about.aspx">About Us</a></li>
                    <li class="FooterLi"><a href="<%=Session["APP_BASE_DOMAIN"]%>contact.aspx">Contact Us</a></li>
                    <li class="FooterLi"><a href="<%=Session["APP_BASE_DOMAIN"]%>advertise.aspx">Advertising</a></li>
                    <li class="FooterLi"><a href="http://resources.yourtribute.com/" target="_blank">Funeral
                        Resources</a></li>
                    <li class="FooterLi"><a href="http://resources.yourtribute.com/grief-and-loss/" target="_blank">
                        Grief and Loss</a></li>
                    <li class="FooterLi"><a href="http://resources.yourtribute.com/funeral-planning/"
                        target="_blank">Funeral Planning</a></li>
                </ul>
            </div>
            <div class="yt-FooterNavColumn-MT">
                <h6 class="Purple-MT">
                    Follow</h6>
                <ul>
                    <li class="FooterLi">
                        <div class="facebook-yt">
                        </div>
                        <div class="Footer_links">
                            <a href="http://www.facebook.com/yourtribute" target="_blank">Facebook</a></div>
                    </li>
                    <li class="FooterLi">
                        <div class="twitter-yt">
                        </div>
                        <div class="Footer_links">
                            <a href="http://www.twitter.com/yourtribute" target="_blank">Twitter</a></div>
                    </li>
                    <li class="FooterLi">
                        <div class="googlePlus-yt">
                        </div>
                        <div class="Footer_links">
                            <a href="https://plus.google.com/b/109473191564708020938/" target="_blank">Google+</a></div>
                    </li>
                    <li class="FooterLi">
                        <div class="pinterest-yt">
                        </div>
                        <div class="Footer_links">
                            <a href="http://www.pinterest.com/yourtribute/" target="_blank">Pinterest</a></div>
                    </li>
                    <li class="FooterLi">
                        <div class="youtube-yt">
                        </div>
                        <div class="Footer_links">
                            <a href="http://www.youtube.com/user/yourtribute" target="_blank">YouTube</a></div>
                    </li>
                    <li class="FooterLi">
                        <div class="blog-yt">
                        </div>
                        <div class="Footer_links">
                            <a href="<%=TributesPortal.Utilities.WebConfig.BlogUrlMain%>" target="_blank">Blog</a></div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div id="yourmomentsFooter" runat="server" class="ym-Footer" visible="false">
        <div class="ym-FooterNewsletter">
            <h6>
                Your Moments Newsletter</h6>
            <p>
                Subscribe to our newsletter to receive special offers, company news and website
                tips and tricks:
            </p>
            <asp:TextBox ID="TextBox1" runat="server" Text="Enter your email address" CssClass="ym-Txt"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Subscribe" CssClass="ym-Btn" OnClientClick="return ValidateForm1();"
                OnClick="btnSubscribe_Click" />
            <!--Script to clear the text box on focus-->

            <script type="text/javascript">

                var txtbox = document.getElementById("<%= txtEmailAddress.ClientID%>");
                txtbox.onfocus = function() {
                    if (this.defaultValue == this.value)
                        this.value = "";
                };
                            
            </script>

            <div>
                <a href="https://www.bbb.org/online/consumer/cks.aspx?id=10906081881" target="_blank">
                    <img alt="BBB" src="../assets/images/copyrightImage.gif" class="copyrightImg" />
                </a>
            </div>
        </div>
        <div class="ym-FooterSitemap">
            <div class="ym-FooterNavColumn">
                <h6>
                    Explore</h6>
                <ul>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx">Tour</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx">Features</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>examples.aspx">Examples</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Pricing</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>advancedsearch.aspx">Search</a></li>
                </ul>
            </div>
            <div class="ym-FooterNavColumn">
                <h6>
                    Company</h6>
                <ul>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>about.aspx">About</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>contact.aspx">Contact</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>advertise.aspx">Advertise</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>termsofuse.aspx">Terms of Use</a></li>
                    <li><a href="<%=Session["APP_BASE_DOMAIN"]%>privacy.aspx">Privacy Policy</a></li>
                    <li><a href="http://support.yourtribute.com" target="_blank">Help</a></li>
                </ul>
            </div>
        </div>
        <div class="ym-FooterSocial">
            <div class="ym-FooterNavColumn ym-FootCol3">
                <h6>
                    Follow Us</h6>
                <ul>
                    <li class="twitter"><a href="http://twitter.com/yourtribute" target="_blank">Twitter</a></li>
                    <li class="facebook"><a href="http://www.facebook.com/yourtribute" target="_blank">Facebook</a></li>
                    <li class="blog"><a href="<%=TributesPortal.Utilities.WebConfig.BlogUrlMain%>" target="_blank">
                        Blog</a></li>
                </ul>
            </div>
            <div class="ym-FooterNavColumn">
                <h6>
                    Your Moments</h6>
                <% if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
                   { %>
                <ul class="ym-FooterPetalImages">
                    <li><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=New Baby&Theme='BabyDefault'">
                        New Baby</a></li>
                    <li><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Wedding&Theme='WeddingDefault'">
                        Wedding</a></li>
                    <li><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Anniversary&Theme='AnniversaryDefault'">
                        Anniversary</a></li>
                    <li><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Graduation&Theme='GraduationDefault'">
                        Graduation</a></li>
                    <li><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Birthday&Theme='BirthdayDefault'">
                        Birthday</a></li>
                </ul>
                <% }
                   else
                   { %>
                <ul class="ym-FooterPetalImages">
                    <li><a href="http://newbaby.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">
                        New Baby</a></li>
                    <li><a href="http://wedding.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">
                        Wedding</a></li>
                    <li><a href="http://anniversary.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">
                        Anniversary</a></li>
                    <li><a href="http://graduation.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">
                        Graduation</a></li>
                    <li><a href="http://birthday.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">
                        Birthday</a></li>
                </ul>
                <% } %>
            </div>
        </div>
    </div>
</div>
<div id="CopyRightDiv" style="font-size: 10px; float: right">
    <div id="CopyRightSize" runat="server">
        <div style="float: left">
            &copy;<asp:Label ID="lblCopyRight" runat="server" Text=""></asp:Label>&nbsp;<span
                id="copyRight" runat="server">Your Tribute, Inc. All Rights Reserved.</span>
        </div>
        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourtribute")%>
        <%{%>
        <div class="copyRightDivLinks">
            <ul class="yt-FloatLeft">
                <li id="copyRightDivLiHome"><a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a></li>
                <li id="copyRightDivLiTerms"><a href="<%=Session["APP_BASE_DOMAIN"]%>termsofuse.aspx">
                    Terms of Use</a></li>
                <li id="copyRightDivLiPrivacy"><a href="<%=Session["APP_BASE_DOMAIN"]%>privacy.aspx">
                    Privacy Policy</a></li>
            </ul>
        </div>
        <% } %>
        <span id="copyRightYM" runat="server" visible="false">Your Moments, Inc. All Rights
            Reserved.</span></div>
</div>

