///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Tribute.ChannelHomePage.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the channel home page
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.Tribute.Views;
using System.Text;
using TributesPortal.Utilities;

public partial class Tribute_ChannelHomePage : PageBase, IChannelHomePage
{
    private ChannelHomePagePresenter _presenter;
    public string trubutetype = string.Empty;
    public string tributefile = string.Empty;
    public string trubutelink = string.Empty;
    public string tributeName = string.Empty;
    public string tributeNameSmall = string.Empty;
    public string SiteName = string.Empty;
    public string TestimonialTribute2Line = string.Empty;
    public string TestimonialTribute1Line = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            CreateBtn.HRef = Session["APP_BASE_DOMAIN"] + "pricing.aspx";
            this._presenter.OnViewInitialized();

            if (Request.QueryString["Type"] != null)
            {
                if (Request.QueryString["Type"].ToString().ToLower().Equals("new baby"))
                {
                    trubutetype = "New Baby";
                    trubutelink = "newbaby";
                }
                else
                {
                    trubutetype = Request.QueryString["Type"].ToString();
                    //trubutelink = trubutetype;
                    trubutelink = "newbaby";
                }

                HtmlGenericControl descriptionMeta = new HtmlGenericControl("meta");
                descriptionMeta.Attributes.Add("name", "description");
                HtmlGenericControl keywordsMeta = new HtmlGenericControl("meta");
                keywordsMeta.Attributes.Add("name", "keywords");

                //lnkCreateTribute.PostBackUrl = Session["APP_BASE_DOMAIN"] + "create.aspx?Type=" + trubutelink;

                // Wedding
                if (trubutetype.ToLower().Equals("wedding"))
                {
                    tributefile = "weddings";
                    this.Page.Title = "Marriage & Wedding Websites - Create a personalized wedding website with Your " + ConfigurationManager.AppSettings["ApplicationWord"].ToString() + ".";
                    descriptionMeta.Attributes.Add("content", "Create a personalized wedding website to announce an engagement or wedding. Share wedding photos, videos, info and more. Send wedding shower invitations to friends and family. Make an online wedding website free for 30 days to celebrate your marriage.");
                    keywordsMeta.Attributes.Add("content", "Wedding Site, Marriage Site, Wedding Web Site, Wedding Website");

                    //p1.Attributes.Add("class", "actionbuttonPink");
                    //                    lnkCreateTribute.CssClass = "leftBigButtonPink";

                    pCreateTributeButton.Attributes.Add("class", "actionbuttonPink");
                    CreateBtn.Attributes.Add("class", "leftBigButtonPink");
                    p1.Attributes.Add("class", "actionbuttonPink");
                    divTributeBox.Attributes.Add("class", "tryTributeBoxPink rounded");
                    h2Testimonial.Attributes.Add("class", "testimonial pinkHome");

                    divBodyBox1.Attributes.Add("class", "third box_WedBT");
                    divBodyBox2.Attributes.Add("class", "third box_WedPU");
                    divBodyBox3.Attributes.Add("class", "third last box_WedFI");
                    divBodyBox4.Attributes.Add("class", "third box_WedUE");
                    divBodyBox5.Attributes.Add("class", "third box_WedSI");
                    divBodyBox6.Attributes.Add("class", "third last box_AnnAR");
                    divBodyBox7.Attributes.Add("class", "third box_WedCN bottom");
                    divBodyBox8.Attributes.Add("class", "third box_AnnHR bottom");
                    divBodyBox9.Attributes.Add("class", "third last box_AnnLS bottom");

                    lblTopQuote1.Text = "A <span class='bold'>Wedding " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "</span> has <span class='bold'>more features</span>" +
                                        ", yet is <span class='bold'>faster </span>and <span class='bold'>easier </span>to " +
                                        "create, than a <span class='bold'>Wedding website</span>.";
                    lblTopQuote2.Text = "A Wedding " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " includes many of the features of popular online invitation, " +
                                        "photo sharing, blogging and social networking websites, in an easy-to-use intuitive " +
                                        "interface. All Wedding " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "s remain online for life to provide an everlasting " +
                                        "record of the significant occasion.";

                    tributeName = "Wedding";
                    tributeNameSmall = "wedding";
                    lblGuestbook.Text = "Custom Notes (Pages)";
                    SiteName = "www.jonmarrywedding.com";
                    lblUnlimitedEvent.Text = "Manage all of the wedding related events in one place. Create events for the wedding shower, rehearsal dinner, wedding ceremony, reception and more.";
                    lblStylist.Text = "Send free online save-the-date cards, wedding invitations and more!";
                    lblNotes.Text = " Notes can be used like a blog to inform visitors of important thoughts and updates. A note can also be used like a page on a website to include links, graphics and  more.";
                    lblHighResolution.Text = "Upload photos from before and after the wedding and let the guests do the same!";

                    TestimonialTribute2Line = " <span class='h2TestimonialTextSize1'>a </span><span class='bold'>wedding</span>";
                    TestimonialTribute1Line = "Celebrate a marriage";
                }

                // New Baby
                if (trubutetype.ToLower().Equals("new baby"))
                {
                    tributefile = "newbabies";
                    this.Page.Title = "Newborn & Baby Websites - Create a personalized new baby website with Your " + ConfigurationManager.AppSettings["ApplicationWord"].ToString() + ".";
                    descriptionMeta.Attributes.Add("content", "Create a personalized baby website to announce a pregnancy or birth. Share baby photos, videos and childhood milestones. Send baby shower invitations to friends and family. Make an online baby website free for 30 days to celebrate your newborn baby.");
                    keywordsMeta.Attributes.Add("content", "Baby Websites, Baby Website, Infants Website, Baby Web Site");

                    //lnkCreateTribute.CssClass = "leftBigButtonYellow";
                    pCreateTributeButton.Attributes.Add("class", "actionbuttonYellow");
                    CreateBtn.Attributes.Add("class", "leftBigButtonYellow");
                    p1.Attributes.Add("class", "actionbuttonYellow"); divTributeBox.Attributes.Add("class", "tryTributeBoxYellow rounded");
                    h2Testimonial.Attributes.Add("class", "testimonial yellowHome");

                    divBodyBox1.Attributes.Add("class", "third box_NewBT");
                    divBodyBox2.Attributes.Add("class", "third box_NewPU");
                    divBodyBox3.Attributes.Add("class", "third last box_NewFI");
                    divBodyBox4.Attributes.Add("class", "third box_NewUE");
                    divBodyBox5.Attributes.Add("class", "third box_NewSI");
                    divBodyBox6.Attributes.Add("class", "third last box_AnnAR");
                    divBodyBox7.Attributes.Add("class", "third box_NewGG bottom");
                    divBodyBox8.Attributes.Add("class", "third box_AnnHR bottom");
                    divBodyBox9.Attributes.Add("class", "third last box_AnnLS bottom");

                    lblTopQuote1.Text = "A <span class='bold'>New Baby " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "</span> has <span class='bold'>more features</span>" +
                                        ", yet is <span class='bold'>faster </span>and <span class='bold'>easier </span>to " +
                                        "create, than a <span class='bold'>Baby website</span>.";
                    lblTopQuote2.Text = "A New Baby " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " includes many of the features of popular online invitation, " +
                                        "photo sharing, blogging and social networking websites, in an easy-to-use intuitive " +
                                        "interface. All New Baby " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "s remain online for life to provide an everlasting " +
                                        "record of the significant occasion.";

                    tributeName = "newbaby";
                    lblGuestbook.Text = "Custom Notes (Pages)";
                    tributeNameSmall = "new baby";
                    SiteName = "www.babyelizabeth.com";
                    lblUnlimitedEvent.Text = "Manage all of the baby related events in one place. Create events for the baby shower, a baptism, first birthday and more.";
                    lblStylist.Text = "Send free online baby announcements, baby shower invitations and more!";
                    lblNotes.Text = " Notes can be used like a blog to inform visitors of important thoughts and updates. A note can also be used like a page on a website to include links, graphics and  more.";
                    lblHighResolution.Text = "Upload photos from the pregnancy, baby shower, and of the new baby boy or girl.";

                    TestimonialTribute2Line = " <span class='h2TestimonialTextSize1'>a </span><span class='bold'>new baby</span>";
                    TestimonialTribute1Line = "Celebrate a birth";
                }

                // Anniversary
                if (trubutetype.ToLower().Equals("anniversary"))
                {
                    tributefile = "anniversaries";
                    this.Page.Title = "Anniversary Websites - Create a personalized anniversary website with Your " + ConfigurationManager.AppSettings["ApplicationWord"].ToString();
                    descriptionMeta.Attributes.Add("content", "Create a personalized wedding anniversary website to celebrate a 10th, 25th, 40th, 50th or other milestone anniversary.  Share photos, videos and more with friends and family. Make an anniversary website free for 30 days to celebrate your marriage.");
                    keywordsMeta.Attributes.Add("content", "Anniversary Website, Anniversary Site, Anniversary Websites");

                    //lnkCreateTribute.CssClass = "leftBigButtonGreen";
                    p1.Attributes.Add("class", "actionbuttonGreen");
                    pCreateTributeButton.Attributes.Add("class", "actionbuttonGreen");
                    CreateBtn.Attributes.Add("class", "leftBigButtonGreen");
                    divTributeBox.Attributes.Add("class", "tryTributeBoxGreen rounded");
                    h2Testimonial.Attributes.Add("class", "testimonial greenHome");

                    divBodyBox1.Attributes.Add("class", "third box_AnnBT");
                    divBodyBox2.Attributes.Add("class", "third box_AnnPU");
                    divBodyBox3.Attributes.Add("class", "third last box_AnnFI");
                    divBodyBox4.Attributes.Add("class", "third box_AnnUE");
                    divBodyBox5.Attributes.Add("class", "third box_AnnSI");
                    divBodyBox6.Attributes.Add("class", "third last box_AnnAR");
                    divBodyBox7.Attributes.Add("class", "third box_AnnGG bottom");
                    divBodyBox8.Attributes.Add("class", "third box_AnnHR bottom");
                    divBodyBox9.Attributes.Add("class", "third last box_AnnLS bottom");

                    lblTopQuote1.Text = "An <span class='bold'>Anniversary " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "</span> has <span class='bold'>more features</span>" +
                                        ", yet is <span class='bold'>faster </span>and <span class='bold'>easier </span>to " +
                                        "create, than a<span class='bold'> website</span>.";
                    lblTopQuote2.Text = "An Anniversary " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " includes many of the features of popular online invitation, " +
                                        "photo sharing, blogging and social networking websites, in an easy-to-use intuitive " +
                                        "interface. All Anniversary " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "s remain online for life to provide an everlasting " +
                                        "record of the significant occasion.";
                    tributeName = "Anniversary";
                    tributeNameSmall = "anniversary";
                    lblGuestbook.Text = "Guestbook & Gifts";
                    SiteName = "www.billandjune50th.com";
                    lblUnlimitedEvent.Text = "Manage all of the anniversary related events in one place. Create events for an anniversary party, dinner, holiday and more.";
                    lblStylist.Text = "Send free online anniversary invitations, thank you cards and more!";
                    lblNotes.Text = "Friends and family can leave personal messages in the online guestbook. They can also give a free virtual gift to the couple with a short message.";
                    lblHighResolution.Text = "Upload photos from the past and present and let friends and family do the same.";

                    TestimonialTribute2Line = " <span class='h2TestimonialTextSize1'>an</span> <span class='bold'>anniversary</span>";
                    TestimonialTribute1Line = "Celebrate a milestone";
                }

                // Birthday
                if (trubutetype.ToLower().Equals("birthday"))
                {
                    tributefile = "birthdays";
                    this.Page.Title = "Party & Birthday Websites - Create a personalized birthday website with Your " + ConfigurationManager.AppSettings["ApplicationWord"].ToString() + ".";
                    descriptionMeta.Attributes.Add("content", "Create a personalized birthday website to celebrate a 30th, 40th, 50th or other milestone birthday.  Share photos and send birthday party invitations to friends and family. Make a birthday website free for 30 days to celebrate your special day.");
                    keywordsMeta.Attributes.Add("content", "Birthday Website, Birthday Site, Birthday Pages");

                    ////lnkCreateTribute.CssClass = "leftBigButtonOrange";
                    pCreateTributeButton.Attributes.Add("class", "actionbuttonOrange");
                    CreateBtn.Attributes.Add("class", "leftBigButtonOrange");
                    p1.Attributes.Add("class", "actionbuttonOrange");
                    divTributeBox.Attributes.Add("class", "tryTributeBoxOrange rounded");
                    h2Testimonial.Attributes.Add("class", "testimonial orangeHome");

                    divBodyBox1.Attributes.Add("class", "third box_BirBT");
                    divBodyBox2.Attributes.Add("class", "third box_BirPU");
                    divBodyBox3.Attributes.Add("class", "third last box_BirFI");
                    divBodyBox4.Attributes.Add("class", "third box_BirUE");
                    divBodyBox5.Attributes.Add("class", "third box_BirSI");
                    divBodyBox6.Attributes.Add("class", "third last box_AnnAR");
                    divBodyBox7.Attributes.Add("class", "third box_BirGG bottom");
                    divBodyBox8.Attributes.Add("class", "third box_AnnHR bottom");
                    divBodyBox9.Attributes.Add("class", "third last box_AnnLS bottom");

                    lblTopQuote1.Text = "A <span class='bold'>Birthday " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "</span> has <span class='bold'>more features</span>" +
                                        ", yet is <span class='bold'>faster </span>and <span class='bold'>easier </span>to " +
                                        "create, than a<span class='bold'> website</span>.";
                    lblTopQuote2.Text = "A Birthday " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " includes many of the features of popular online invitation, " +
                                        "photo sharing, blogging and social networking websites, in an easy-to-use intuitive " +
                                        "interface. All Birthday " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "s remain online for life to provide an everlasting " +
                                        "record of the significant occasion.";
                    tributeName = "Birthday";
                    tributeNameSmall = "birthday";
                    lblGuestbook.Text = "Guestbook & Gifts";
                    lblUnlimitedEvent.Text = " Manage all of the birthday related events in one place. Create events for a birthday party, dinner party, vacation and more.";
                    lblStylist.Text = "Send free online birthday invitations, thank you cards and more!";
                    lblNotes.Text = "Friends and family can leave personal messages in the online guestbook. They can also give a free virtual gift to the birthday person with a short message.";
                    SiteName = "www.jensweet16.com";
                    lblHighResolution.Text = "Upload photos from the past and the party and let friends and family do the same.";

                    TestimonialTribute2Line = "<span class='h2TestimonialTextSize1'>a </span> <span class='bold'>birthday</span>";
                    TestimonialTribute1Line = "Celebrate another year";
                }

                // Graduation
                if (trubutetype.ToLower().Equals("graduation"))
                {
                    tributefile = "graduations";
                    this.Page.Title = "Reunion & Graduation Websites - Create a personalized grad website with Your " + ConfigurationManager.AppSettings["ApplicationWord"].ToString() + ".";
                    descriptionMeta.Attributes.Add("content", "Create a personalized graduation website for an elementary, high school, college or other graduate. Share grad photos and send graduation invitations to friends and family. Make a graduation website free for 30 days to celebrate your achievement.");
                    keywordsMeta.Attributes.Add("content", "Graduate Website, Graduation Website, Graduation Site");

                    ////lnkCreateTribute.CssClass = "leftBigButtonSkyBlue";
                    pCreateTributeButton.Attributes.Add("class", "actionbuttonskyBlue");
                    CreateBtn.Attributes.Add("class", "leftBigButtonSkyBlue");
                    p1.Attributes.Add("class", "actionbuttonskyBlue");
                    divTributeBox.Attributes.Add("class", "tryTributeBoxskyBlue rounded");
                    h2Testimonial.Attributes.Add("class", "testimonial skyBlueHome");

                    divBodyBox1.Attributes.Add("class", "third box_GraBT");
                    divBodyBox2.Attributes.Add("class", "third box_GraPU");
                    divBodyBox3.Attributes.Add("class", "third last box_GraFI");
                    divBodyBox4.Attributes.Add("class", "third box_GraUE");
                    divBodyBox5.Attributes.Add("class", "third box_GraSI");
                    divBodyBox6.Attributes.Add("class", "third last box_AnnAR");
                    divBodyBox7.Attributes.Add("class", "third box_GraGG bottom");
                    divBodyBox8.Attributes.Add("class", "third box_AnnHR bottom");
                    divBodyBox9.Attributes.Add("class", "third last box_AnnLS bottom");


                    lblTopQuote1.Text = "A <span class='bold'>Graduation " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "</span> has <span class='bold'>more features</span>" +
                                        ", yet is <span class='bold'>faster </span>and <span class='bold'>easier </span>to " +
                                        "create, than a<span class='bold'> website</span>.";
                    lblTopQuote2.Text = "A Graduation " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " includes many of the features of popular online invitation, " +
                                        "photo sharing, blogging and social networking websites, in an easy-to-use intuitive " +
                                        "interface. All Graduation " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "s remain online for life to provide an everlasting " +
                                        "record of the significant occasion.";

                    tributeName = "Graduation";
                    tributeNameSmall = "graduation";
                    lblGuestbook.Text = "Guestbook & Gifts";
                    SiteName = "www.kellysmith.com";
                    lblStylist.Text = "Send free online graduation invitations, thank you cards and more!";
                    lblUnlimitedEvent.Text = " Manage all of the graduation related events in one place. Create events for a grad party, commencement, grad dinner and more.";
                    lblNotes.Text = "Friends and family can leave personal messages in the online guestbook. They can also give a free virtual gift to the graduate with a short message.";
                    lblHighResolution.Text = "Upload photos from the school and the grad party and let friends and family do the same.";

                    TestimonialTribute2Line = " <span class='h2TestimonialTextSize1'>a </span><span class='bold'>graduation</span>";
                    TestimonialTribute1Line = "Celebrate an achievement";
                }

                // Memorial
                if (trubutetype.ToLower().Equals("memorial"))
                {
                    string strMemorial = "http://" + WebConfig.TopLevelDomain + "/";
                    Response.Redirect(strMemorial);
                    tributefile = "memorials";
                    this.Page.Title = "Your Tribute’s Free Obituaries Online | Permanent Online Memorials";
                    descriptionMeta.Attributes.Add("content", "Free Obituaries Online – Let Your Tribute help you celebrate the lives of your deceased loved ones through our free obituaries online and more. Sign up and share precious memories online with your friends and family today.");
                    keywordsMeta.Attributes.Add("content", "free obituaries online");

                    //lnkCreateTribute.CssClass = "leftBigButtonPurple";
                    pCreateTributeButton.Attributes.Add("class", "actionbuttonPurple");
                    CreateBtn.Attributes.Add("class", "leftBigButtonPurple");
                    p1.Attributes.Add("class", "actionbuttonPurple");
                    divTributeBox.Attributes.Add("class", "tryTributeBoxPurple rounded");
                    h2Testimonial.Attributes.Add("class", "testimonial purpleHome");

                    divBodyBox1.Attributes.Add("class", "third box_MemBT");
                    divBodyBox2.Attributes.Add("class", "third box_MemPU");
                    divBodyBox3.Attributes.Add("class", "third last box_MemFI");
                    divBodyBox4.Attributes.Add("class", "third box_MemUE");
                    divBodyBox5.Attributes.Add("class", "third box_MemSI");
                    divBodyBox6.Attributes.Add("class", "third last box_AnnAR");
                    divBodyBox7.Attributes.Add("class", "third box_MemGG bottom");
                    divBodyBox8.Attributes.Add("class", "third box_AnnHR bottom");
                    divBodyBox9.Attributes.Add("class", "third last box_AnnLS bottom");

                    lblTopQuote1.Text = "A <span class='bold'>Memorial " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "</span> has <span class='bold'>more features</span>" +
                                        ", yet is <span class='bold'>faster </span>and <span class='bold'>easier </span>to " +
                                        "create, than a <span class='bold'>Memorial website</span>.";
                    lblTopQuote2.Text = "A Memorial " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " includes many of the features of popular online invitation, " +
                                        "photo sharing, blogging and social networking websites, in an easy-to-use intuitive " +
                                        "interface. All Memorial " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "s remain online for life to provide an everlasting " +
                                        "record of the significant occasion.";

                    tributeName = "Memorial";
                    tributeNameSmall = "memorial";
                    lblGuestbook.Text = "Guestbook & Gifts";
                    lblUnlimitedEvent.Text = " Manage all of the memorial related events in one place. Create events for the funeral service, burial, reception and more.";
                    lblStylist.Text = "Send free online invitations to the funeral service, thank you cards and more.";
                    lblNotes.Text = "Friends and family can leave personal messages in your loved one’s online guestbook. They can also give a free virtual gift with a short message.";
                    SiteName = "www.evelynmsmith.com";
                    lblHighResolution.Text = "Upload photos of your loved one and let friends and family do the same.";

                    TestimonialTribute2Line = "<span class='h2TestimonialTextSize1'>a </span><span class='bold'>memorial</span>";
                    TestimonialTribute1Line = "Remember a life";
                }

                // Adds the additional meta data
                this.Page.Header.Controls.Add(descriptionMeta);
                this.Page.Header.Controls.Add(keywordsMeta);

                if (trubutetype == "New Baby")
                    Session["tributeType"] = "newbaby";
                else
                    Session["tributeType"] = trubutetype;

                //Page.Title = "Your Tribute | "+trubutetype;
                this._presenter.GetPopularTribute(trubutetype);
                SetTributeList(trubutetype);
                SetTextBasedOnTributes(trubutetype);
            }

        }
        this._presenter.OnViewLoaded();
    }

    [CreateNew]
    public ChannelHomePagePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    private void SetTributeList(string trubutetype)
    {
        StringBuilder objStrib = new StringBuilder();
        //actribute.InnerHtml = "Create a " + trubutetype.ToLower() + " tribute";
        //actribute.HRef = Session["APP_BASE_DOMAIN"] + "create.aspx?Type=" + trubutetype;

        if (!WebConfig.ApplicationMode.Equals("local"))
        {
            string strAnniversary = "http://anniversary." + WebConfig.TopLevelDomain + "/";
            string strBirthday = "http://birthday." + WebConfig.TopLevelDomain + "/";
            string strGraduation = "http://graduation." + WebConfig.TopLevelDomain + "/";
            string strNewBaby = "http://newbaby." + WebConfig.TopLevelDomain + "/";
            string strMemorial = "http://memorial." + WebConfig.TopLevelDomain + "/";
            string strWedding = "http://wedding." + WebConfig.TopLevelDomain + "/";
            //Use this commented part for server and comment the line written below this commented part
            if (trubutetype.Equals("New Baby"))
            {
                objStrib.Append("<li><a href=" + strAnniversary + ">Anniversary</a></li>");
                objStrib.Append("<li><a href=" + strBirthday + ">Birthday</a></li>");
                objStrib.Append("<li><a href=" + strGraduation + ">Graduation</a></li>");
                objStrib.Append("<li><a href=" + strWedding + ">Wedding</a></li>");
                objStrib.Append("<li class='last'><a href=" + strMemorial + ">Memorial</a></li>");
            }
            else if (trubutetype.Equals("Anniversary"))
            {
                objStrib.Append("<li><a href=" + strNewBaby + ">New Baby</a></li>");
                objStrib.Append("<li><a href=" + strBirthday + ">Birthday</a></li>");
                objStrib.Append("<li><a href=" + strGraduation + ">Graduation</a></li>");
                objStrib.Append("<li><a href=" + strWedding + ">Wedding</a></li>");
                objStrib.Append("<li class='last'><a href=" + strMemorial + ">Memorial</a></li>");
            }
            else if (trubutetype.Equals("Birthday"))
            {
                objStrib.Append("<li><a href=" + strNewBaby + ">New Baby</a></li>");
                objStrib.Append("<li><a href=" + strAnniversary + ">Anniversary</a></li>");
                objStrib.Append("<li><a href=" + strGraduation + ">Graduation</a></li>");
                objStrib.Append("<li><a href=" + strWedding + ">Wedding</a></li>");
                objStrib.Append("<li class='last'><a href=" + strMemorial + ">Memorial</a></li>");
            }
            else if (trubutetype.Equals("Graduation"))
            {
                objStrib.Append("<li><a href=" + strNewBaby + ">New Baby</a></li>");
                objStrib.Append("<li><a href=" + strAnniversary + ">Anniversary</a></li>");
                objStrib.Append("<li><a href=" + strBirthday + ">Birthday</a></li>");
                objStrib.Append("<li><a href=" + strWedding + ">Wedding</a></li>");
                objStrib.Append("<li class='last'><a href=" + strMemorial + ">Memorial</a></li>");
            }
            else if (trubutetype.Equals("Wedding"))
            {
                objStrib.Append("<li><a href=" + strNewBaby + ">New Baby</a></li>");
                objStrib.Append("<li><a href=" + strAnniversary + ">Anniversary</a></li>");
                objStrib.Append("<li><a href=" + strBirthday + ">Birthday</a></li>");
                objStrib.Append("<li><a href=" + strGraduation + ">Graduation</a></li>");
                objStrib.Append("<li class='last'><a href=" + strMemorial + ">Memorial</a></li>");
            }
            else if (trubutetype.Equals("Memorial"))
            {
                objStrib.Append("<li><a href=" + strNewBaby + ">New Baby</a></li>");
                objStrib.Append("<li><a href=" + strAnniversary + ">Anniversary</a></li>");
                objStrib.Append("<li><a href=" + strBirthday + ">Birthday</a></li>");
                objStrib.Append("<li><a href=" + strGraduation + ">Graduation</a></li>");
                objStrib.Append("<li class='last'><a href=" + strWedding + ">Wedding</a></li>");
            }
            //uiTributeList.InnerHtml = objStrib.ToString();
        }
        else
        {
            //
            if (trubutetype.Equals("New Baby"))
            {
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Anniversary&Theme='AnniversaryDefault'>Anniversary</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Birthday&Theme='BirthdayDefault'>Birthday</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Graduation&Theme='GraduationDefault'>Graduation</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Wedding&Theme='WeddingDefault'>Wedding</a></li>");
                objStrib.Append("<li class='last'><a href=channelhomepage.aspx?Type=Memorial&Theme='MemorialDefault'>Memorial</a></li>");
            }
            else if (trubutetype.Equals("Anniversary"))
            {
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=NewBaby&Theme='BabyDefault'>New Baby</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Birthday&Theme='BirthdayDefault'>Birthday</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Graduation&Theme='GraduationDefault'>Graduation</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Wedding&Theme='WeddingDefault'>Wedding</a></li>");
                objStrib.Append("<li class='last'><a href=channelhomepage.aspx?Type=Memorial&Theme='MemorialDefault'>Memorial</a></li>");
            }
            else if (trubutetype.Equals("Birthday"))
            {
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=NewBaby&Theme='BabyDefault'>New Baby</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Anniversary&Theme='AnniversaryDefault'>Anniversary</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Graduation&Theme='GraduationDefault'>Graduation</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Wedding&Theme='WeddingDefault'>Wedding</a></li>");
                objStrib.Append("<li class='last'><a href=channelhomepage.aspx?Type=Memorial&Theme='MemorialDefault'>Memorial</a></li>");
            }
            else if (trubutetype.Equals("Graduation"))
            {
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=NewBaby&Theme='BabyDefault'>New Baby</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Anniversary&Theme='AnniversaryDefault'>Anniversary</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Birthday&Theme='BirthdayDefault'>Birthday</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Wedding&Theme='WeddingDefault'>Wedding</a></li>");
                objStrib.Append("<li class='last'><a href=channelhomepage.aspx?Type=Memorial&Theme='MemorialDefault'>Memorial</a></li>");
            }
            else if (trubutetype.Equals("Wedding"))
            {
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=NewBaby&Theme='BabyDefault'>New Baby</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Anniversary&Theme='AnniversaryDefault'>Anniversary</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Birthday&Theme='BirthdayDefault'>Birthday</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Graduation&Theme='GraduationDefault'>Graduation</a></li>");
                objStrib.Append("<li class='last'><a href=channelhomepage.aspx?Type=Memorial&Theme='MemorialDefault'>Memorial</a></li>");
            }
            else if (trubutetype.Equals("Memorial"))
            {
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=NewBaby&Theme='BabyDefault'>New Baby</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Anniversary&Theme='AnniversaryDefault'>Anniversary</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Birthday&Theme='BirthdayDefault'>Birthday</a></li>");
                objStrib.Append("<li><a href=channelhomepage.aspx?Type=Graduation&Theme='GraduationDefault'>Graduation</a></li>");
                objStrib.Append("<li class='last'><a href=channelhomepage.aspx?Type=Wedding&Theme='WeddingDefault'>Wedding</a></li>");
            }
            //uiTributeList.InnerHtml = objStrib.ToString();
            //
        }
    }

    private void SetTextBasedOnTributes(string tributeType)
    {
        if (tributeType.ToLower().Equals("wedding"))
        {
            hHeader.InnerText = "Celebrate a Marriage with a Wedding " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "";
            hCreateLink.InnerHtml = "Create a personalized website for a wedding in minutes. It’s as easy as 1-2-3.";
            pShare.InnerText = "Create a FREE Wedding " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " in minutes. It’s quick, fun, and easy. No HTML knowledge required!";
            pSend.InnerText = "Easily manage all of the wedding related events. Import contacts and send FREE wedding invitations with RSVP.";
            pConvey.InnerText = "Share stories and fond memories. Add photos and videos and receive virtual gifts and guestbook messages.";
        }
        if (tributeType.ToLower().Equals("new baby"))
        {
            hHeader.InnerText = "Celebrate a Birth with a New Baby " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "";
            hCreateLink.InnerHtml = "Create a personalized website for a new baby in minutes. It’s as easy as 1-2-3.";
            pShare.InnerText = "Create a FREE New Baby " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " in minutes. It’s quick, fun, and easy. No HTML knowledge required!";
            pSend.InnerText = "Easily manage all of the baby related events. Send free baby shower invitations and birth announcements.";
            pConvey.InnerText = "Share stories about the pregnancy and baby. Add photos, videos and receive virtual gifts and guestbook messages.";
        }
        if (tributeType.ToLower().Equals("anniversary"))
        {
            hHeader.InnerText = "Celebrate a Milestone with an Anniversary " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "";
            hCreateLink.InnerHtml = "Create a personalized website for an anniversary in minutes. It’s as easy as 1-2-3.";
            pShare.InnerText = "Create a FREE Anniversary " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " in minutes. It’s quick, fun, and easy. No HTML knowledge required!";
            pSend.InnerText = "Easily manage all of the anniversary related events. Import contacts and send FREE online invitations with RSVP.";
            pConvey.InnerText = "Share stories and fond memories. Add photos and videos and receive virtual gifts and guestbook messages.";
        }
        if (tributeType.ToLower().Equals("birthday"))
        {
            hHeader.InnerText = "Celebrate another year with a Birthday " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "";
            hCreateLink.InnerHtml = "Create a personalized website for a birthday in minutes. It’s as easy as 1-2-3.";
            pShare.InnerText = "Create a FREE Birthday " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " in minutes. It’s quick, fun, and easy. No HTML knowledge required!";
            pSend.InnerText = "Easily manage all of the birthday related events. Import contacts and send FREE birthday invitations with RSVP.";
            pConvey.InnerText = "Share stories and fond memories. Add photos and videos and receive virtual gifts and guestbook messages.";
        }
        if (tributeType.ToLower().Equals("graduation"))
        {
            hHeader.InnerText = "Celebrate an Achievement with a Graduation " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "";
            hCreateLink.InnerHtml = "Create a personalized website for a graduation in minutes. It’s as easy as 1-2-3.";
            pShare.InnerText = "Create a FREE Graduation " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " in minutes. It’s quick, fun, and easy. No HTML knowledge required!";
            pSend.InnerText = "Easily manage all of the graduation related events. Import contacts and send FREE grad invitations with RSVP.";
            pConvey.InnerText = "Share stories and memories. Add photos and videos and receive virtual gifts and guestbook messages.";
        }
        if (tributeType.ToLower().Equals("memorial"))
        {
            hHeader.InnerText = "Remember a Life with a Memorial " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + "";
            hCreateLink.InnerHtml = "Create a personalized website for your loved one in minutes. It’s as easy as 1-2-3.";
            pShare.InnerText = "Create a FREE Memorial " + ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() + " for your loved one in minutes. It’s quick and easy. No HTML knowledge required!";
            pSend.InnerText = "Easily manage all of the memorial related events. Import contacts and send FREE invitations and thank you cards.";
            pConvey.InnerText = "Share stories about your loved one. Add photos and videos and receive virtual gifts and personal guestbook messages.";
        }
    }
    #region IChannelHomePage Members

    public System.Collections.Generic.IList<TributesPortal.BusinessEntities.SearchTribute> FeaturedTributes
    {
        set
        {
            if (value.Count > 0)
            {
                int[] randomTributeNo = new int[3];

                StringBuilder objStrib = new StringBuilder();
                int counter = 0;
                if (value.Count > 3)
                {
                    Random ramdomIndex = new Random();
                    int count = 0;
                    while (count < 3)
                    {
                        int randomNumber = ramdomIndex.Next(1, value.Count + 1);
                        bool isRandomNumberExists = false;
                        for (int indexTribute = 0; indexTribute < 3; indexTribute++)
                        {
                            if (randomTributeNo[indexTribute] == randomNumber)
                            {
                                isRandomNumberExists = true;
                                break;
                            }
                        }
                        if (!isRandomNumberExists)
                        {
                            randomTributeNo[count] = randomNumber;
                            count++;
                        }
                    }

                    counter = 3;
                }
                else
                    counter = value.Count;

                for (int i = 0; i < counter; i++)
                {
                    int indexTribute = randomTributeNo[i] - 1;
                    if (indexTribute < 0)
                        indexTribute = i;
                    string Createdby = value[indexTribute].TributeName;
                    string TributeImage = value[indexTribute].TributeImage;
                    string TributeId = value[indexTribute].TributeID.ToString();
                    string Country = value[indexTribute].Country.ToString();
                    string CreatedDate = value[indexTribute].CreatedDate;
                    string State = value[indexTribute].State.ToString();
                    string location = value[indexTribute].Location;
                    string tributeUrl = value[indexTribute].TributeUrl;
                    string tributeType = value[indexTribute].TributeType;
                    objStrib.Append("<li>");
                    objStrib.Append("<div class='vcard'>");
                    objStrib.Append("</div>");
                    objStrib.Append("<a href='" + Session["APP_PATH"] + tributeUrl + " ' class='yt-Thumb'>");
                    objStrib.Append("<img class='photo' src='" + TributeImage + "' width='48' height='48' alt='' /></a>");
                    objStrib.Append("<div class='fn'>");
                    objStrib.Append("<a href='" + Session["APP_PATH"] + tributeUrl + "'>" + Createdby + "</a></div>");
                    objStrib.Append("<span class='locality'>" + location + "</span>");
                    objStrib.Append("<span class='yt-Date'>Created: " + CreatedDate + "</span>");
                    objStrib.Append("</li>");
                }
                //Literal1.Text = objStrib.ToString();
            }
        }
    }

    #endregion
}


