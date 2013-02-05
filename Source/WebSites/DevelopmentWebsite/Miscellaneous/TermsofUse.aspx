<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TermsofUse.aspx.cs" Inherits="Miscellaneous_TermsofUse"
    Title="TermsofUse" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<%@ Register Src="../UserControl/LeftFeaturedPanel.ascx" TagName="LeftFeaturedPanel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title>Terms of Use</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <!-- These url's will work on Remote server. Comment the above urls -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_21.css" />
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_DOMAIN"]%>assets/images/favicon.ico" />

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/modalbox.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <script type="text/javascript">
      App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
    </script>

    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container yt-Tour">
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="None" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Terms of Use
            </span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div class="yt-Panel-Primary yt-Panel-Primary-about">
                            <h2>
                                Terms of Use
                            </h2>
                            <center>
                                <b>YOUR
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    TERMS OF USE </b>
                                <p>
                                    Last Updated: May 2008
                                </p>
                            </center>
                            <br />
                            <p>
                                Please read the following Terms of Use carefully before using Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                (collectively, this “Website").
                            </p>
                            <h3 class="yt-Bullet">
                                1. Applicability & Acceptance of These Terms of Use
                            </h3>
                            <br />
                            <p>
                                By viewing, using, accessing, browsing, or submitting any content or material on
                                or to this Website, you agree to these Terms of Use as a binding legal agreement
                                between you and Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., without
                                limitation or qualification. Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. reserves
                                the right to modify these Terms of Use at any time without prior notice. You agree
                                that each visit you make to this Website shall be subject to the then-current Terms
                                of Use, and continued use of the Website now or following modifications in these
                                Terms of Use confirms that you have read, accepted, and agreed to be bound by such
                                modifications.
                            </p>
                            <br />
                            <br />
                            <h3 class="yt-Bullet">
                                2. User License
                            </h3>
                            <br />
                            <b>a. Scope </b>
                            <br />
                            <br />
                            <p>
                                Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. grants
                                you permission (which may be revoked at any time for any reason or no reason) to
                                view this Website and to download, email, or print individual pages of this Website
                                in accordance with these Terms of Use and solely for your own personal, non-commercial
                                use, provided you do not remove any trademark, copyright or other notice contained
                                on such pages. No other use is permitted. You may not, for example, incorporate
                                the information, content, or other material in any database, compilation, archive
                                or cache. You may not modify, copy, distribute, re-publish, transmit, display, perform,
                                reproduce, publish, reuse, resell, license, create derivative works from, transfer,
                                or sell any information, content, material, software, products or services obtained
                                from this Website, except as specifically noted above.
                                <br />
                                <br />
                                Except as specifically authorized by Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., you may
                                not deep-link to this Website for any purpose or access this Website with any robot,
                                spider, web crawler, extraction software, or any other automated process or device
                                to scrape, copy, or monitor any portion of this Website or any information, content,
                                or material on this Website. Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. reserves
                                all of its statutory and common law rights against any person or entity who violates
                                this paragraph. You may not link or frame to any pages of this Website or any content
                                contained therein, whether in whole or in part.
                                <br />
                                <br />
                                Any rights not expressly granted herein are reserved.
                                <br />
                                <br />
                            </p>
                            <b>b. User Conduct</b><br />
                            <br />
                            You agree that your use of this Website and/or services on this Website is subject
                            to all applicable local, state, national and international laws and regulations.
                            You also agree:
                            <br />
                            <ul>
                                <li>to comply with US law and local laws or rules regarding online conduct and acceptable
                                    Material, and regarding the transmission of technical data exported through the
                                    Website or any service provided by Your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. from the
                                    US or the country in which you reside; </li>
                                <li>not to use the Website or its services without the consent of a parent, guardian
                                    or educational supervisor if you are under the age of 13 (a “Minor”); </li>
                                <li>not to use the Website for illegal purposes; </li>
                                <li>not to commit any acts of infringement on the Website or with respect to content
                                    on the Website; </li>
                                <li>not to use the Website to engage in commercial activities; </li>
                                <li>not to create or maintain a Memorial
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>,
                                    Wedding
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Anniversary
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Birthday
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, New Baby
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    or Graduation
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    for or with any commercial or other purpose or intent that does not in good faith
                                    comport with the purpose or spirit of the Website, including but not limited to
                                    acquiring, designating, or choosing a Website name or title, or Website address
                                    or URL for resale or rental,, depriving any family member or friend of a deceased
                                    person from establishing or using a service in that person’s name, and linking to
                                    any commercial or other Website from a Memorial
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Wedding
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Anniversary
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Birthday
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, New Baby
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    or Graduation
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>; </li>
                                <li>not to attempt to gain unauthorized access to other computer systems from or through
                                    the Website; </li>
                                <li>not to interfere with another person’s use and enjoyment of the Website or another
                                    entity’s use and enjoyment of the Website; </li>
                                <li>not to use the Website for chain letters, junk mail, spamming, or use of distribution
                                    lists; </li>
                                <li>not to upload or transmits viruses or other harmful, disruptive or destructive files;
                                </li>
                                <li>not to disrupt, interfere with, or otherwise harm or violate the security of this
                                    Website, or any services, system resources, accounts, passwords, servers or networks
                                    connected to or accessible through this Website or affiliated or linked Websites,
                                </li>
                            </ul>
                            <b>c. Harm from Commercial Use</b><br />
                            <br />
                            You agree that the consequences of commercial use or re-publication may be so serious
                            and incalculable that monetary compensation may not be a sufficient or appropriate
                            remedy.
                            <h3 class="yt-Bullet">
                                3. Website Content</h3>
                            <br />
                            <b>a. Nature of User Material </b>
                            <br />
                            <br />
                            Some of the services offered by Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. on this
                            Website allow you and others to post, upload, transmit, display, publish, distribute,
                            or otherwise submit material to the Website (collectively, “Submit”), including,
                            for example, images, information, articles, illustrations, lyrics, photos, audio
                            files, poems, videos, or text (collectively, “Material”) for a
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website. You agree not to Submit any Material that:
                            <br />
                            <ul>
                                <li>contains vulgar, profane, abusive, hateful, or sexually explicit language, epithets
                                    or slurs, text or illustrations in poor taste, inflammatory attacks of a personal,
                                    sexual, racial or religious nature, or expressions of bigotry, racism, discrimination
                                    or hate; </li>
                                <li>is defamatory, threatening, disparaging, inflammatory, false, misleading, deceptive,
                                    fraudulent, inaccurate, or unfair, contains gross exaggeration or unsubstantiated
                                    claims, violates the privacy rights of any third party, is unreasonably harmful
                                    or offensive to any individual or community, contains any actionable statement,
                                    or tends to mislead or reflect unfairly on any other person, business or entity;
                                </li>
                                <li>unfairly interferes with any third party's uninterrupted use and enjoyment of this
                                    Website; </li>
                                <li>advertises, promotes or offers to trade any goods or services, except in areas specifically
                                    designated for such purpose; </li>
                                <li>is intended primarily to promote a cause or movement, whether political, religious
                                    or other; </li>
                                <li>contains copyrighted content (copyrighted articles, illustrations, images, lyrics,
                                    photos, audio, video, poems, text, or other content) without the express permission
                                    of the owner of the copyrights in the content; </li>
                                <li>constitutes, promotes or encourages illegal acts, the violation of any right of
                                    any individual or entity, the violation of any local, state, national or international
                                    law, rule, guideline or regulation, or otherwise creates liability; </li>
                                <li>discloses any personal identifying information relating to a Minor; </li>
                                <li>infringes any copyright, trademark, patent, trade secret, or other intellectual
                                    property right; </li>
                                <li>contains viruses or other harmful, disruptive or destructive files; </li>
                                <li>harms or is inappropriate for minors to view; </li>
                                <li>links to any commercial or other Website; </li>
                                <li>is not otherwise in compliance with these Terms of Use. </li>
                            </ul>
                            <b>b. User Representations and Warranties</b>
                            <br />
                            <br />
                            Each time you Submit Material to this Website, you represent and warrant that:
                            <br />
                            <ul>
                                <li>you have the right to Submit the Material to this Website, which means: </li>
                            </ul>
                            &nbsp;&nbsp;&nbsp;o you are the author of the Material, or
                            <br />
                            &nbsp;&nbsp;&nbsp;o the Material is not protected by copyright law, or
                            <br />
                            &nbsp;&nbsp;&nbsp;o you have express permission from the copyright owner to post
                            the Material on this Website; and
                            <br />
                            &nbsp;&nbsp;&nbsp;o you have the right to grant Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. the license
                            set out in these Terms of Use; and
                            <br />
                            <br />
                            <ul>
                                <li>the Material you Submit does not violate these Terms of Use. </li>
                            </ul>
                            <b>c. User License Grant to Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.</b><br />
                            <br />
                            You grant Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., its affiliates,
                            and related entities a royalty-free, perpetual, irrevocable, non-exclusive right
                            and license to use, copy, modify, display, archive, store, publish, transmit, perform,
                            distribute, reproduce and create derivative works from all Material you provide
                            to Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. in any
                            form, media, software or technology of any kind now existing or developed in the
                            future. Without limiting the generality of the previous sentence, you authorize
                            Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. to include
                            the Material you provide in a searchable format that may be accessed by users of
                            this Website and other Websites. You also grant Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. and its
                            affiliates and related entities the right to use your name and any other information
                            about you that you provide in connection with the use, reproduction or distribution
                            of such Material. You also grant Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. the right
                            to use any facts, ideas, concepts, know-how or techniques (“Information”) contained
                            in any Material or communication you send to us for any purpose whatsoever, including
                            but not limited to, developing, manufacturing and marketing products using such
                            Information. You grant all rights in this paragraph in consideration of your use
                            of this Website and our services of making Material you provide us available to
                            third parties, and without the need for additional compensation of any sort to you.
                            Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. does not
                            claim ownership of Material you Submit to the Website.
                            <br />
                            </br/> <b>d. Disclaimer of Responsibility for Material</b><br />
                            <br />
                            You acknowledge and agree that Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. does not
                            control the Material Submitted to
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Websites, or any other services permitting user-generated Material or content, and
                            disclaims any responsibility for such Material. Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. specifically
                            disclaims any duty, obligation, or responsibility, to review, screen, refuse to
                            post, remove, or edit any
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website, or other Material. In addition, Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. does not
                            represent or warrant that any other content or information accessible via this Website
                            is accurate, complete, or current. Price and availability information is subject
                            to change without notice. Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. assumes
                            no responsibility or liability for any errors or omissions in the content of this
                            Website.
                            <br />
                            <br />
                            <b>e. Review & Removal of Material</b><br />
                            <br />
                            (i) Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. reserves
                            the right (but disclaims any duty, obligation, or responsibility) to review, screen,
                            refuse to post, remove in their entirety, or edit (at any time and without prior
                            notice) any
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website, or any Material that Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. believes,
                            in its absolute and sole discretion, may violate Sections 2(b), 3(a), or 3(b) above.
                            Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. also reserves
                            the right (but disclaims any duty, obligation, or responsibility) to refuse to post,
                            remove in their entirety, or edit (at any time and without prior notice) any
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website, or Material on the Website for any reason or no reason whatsoever, in its
                            absolute and sole discretion. In cases in which payment has been made for a
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website that has been removed for reasons other than a breach of 2(b), 3(a) or 3(b)
                            above, Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., will
                            issue a full refund.
                            <br />
                            (ii) The Digital Millennium Copyright Act of 1998 (the "DMCA") provides recourse
                            for copyright owners who believe that material appearing on the Internet infringes
                            their rights under US copyright law. If you believe in good faith that content or
                            Material hosted by this Website infringes your copyright, you (or your agent) may
                            send Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. a notice
                            requesting that the Website content or Material be removed, or access to it blocked.
                            The notice must include the following information: (a) a physical or electronic
                            signature of a person authorized to act on behalf of the owner of an exclusive right
                            that is allegedly infringed; (b) identification of the copyrighted work claimed
                            to have been infringed (or if multiple copyrighted works located on the Website
                            are covered by a single notification, a representative list of such works); (c)
                            identification of the material that is claimed to be infringing or the subject of
                            infringing activity, and information reasonably sufficient to allow Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. to locate
                            the content or Material on the Website; (d) the name, address, telephone number
                            and email address (if available) of the complaining party; (e) a statement that
                            the complaining party has a good faith belief that use of the content or Material
                            in the manner complained of is not authorized by the copyright owner, its agent
                            or the law; and (f) a statement that the information in the notification is accurate
                            and, under penalty of perjury, that the complaining party is authorized to act on
                            behalf of the owner of an exclusive right that is allegedly infringed. If you believe
                            in good faith that a notice of copyright infringement has been wrongly filed against
                            you, the DMCA permits you to send us a counter-notice. Notices and counter-notices
                            must meet the then-current statutory requirements imposed by the DMCA; see <a href="http://www.loc.gov/copyright">
                                http://www.loc.gov/copyright</a> for details. Notices and counter-notices with
                            respect to the Website should be sent to Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc, 2875 North
                            Lamb Blvd, Bldg 8, Las Vegas, NV 89115, <a href="mailto:<%=TributesPortal.Utilities.WebConfig.PrivacyEmail%>">
                                <%=TributesPortal.Utilities.WebConfig.PrivacyEmail%>
                            </a>. Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. suggests
                            that you consult your legal advisor before filing a notice or counter-notice. Also,
                            be aware that there can be penalties for false claims under the DMCA. We reserve
                            the right to terminate the account of any user who is a copyright infringer.
                            <br />
                            <br />
                            <b>f. Proprietary Rights.</b><br />
                            <br />
                            You acknowledge and agree that this Website contains proprietary information and
                            content that is protected by intellectual property and other laws, and may not be
                            used except as provided in these Terms of Use without advance, written permission
                            of Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. All Website
                            design, text, graphics, interfaces, and images (and the selection and arrangements
                            thereof), and software, hypertext markup language (“HTML”), scripts, active server
                            pages, and other content and software used in the Website are ©2008 Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., all rights
                            reserved.
                            <br />
                            <br />
                            <h3 class="yt-Bullet">
                                4. Termination and Modifications to Website</h3>
                            <br />
                            Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. reserves
                            the right, in its sole and absolute discretion, to modify, suspend, or discontinue
                            at any time, with or without notice, this Website and/or services offered on or
                            through the Website (or any part thereof), including but not limited to the Website’s
                            features, look and feel, and functional elements and services relating to
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Websites, and other services provided by Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.
                            <h3 class="yt-Bullet">
                                5. Indemnity</h3>
                            <br />
                            You agree to indemnify and hold Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., its parents,
                            subsidiaries and affiliates, agents, officers, directors, or other employees harmless
                            from any claim, demand, or damage (whether direct, indirect, or consequential, including
                            reasonable attorneys’ fees, made by anyone in connection with your use of this Website,
                            with Material or Information you Submit on or through this Website, with any alleged
                            infringement of intellectual property or other right of any person or entity relating
                            to the Website, your violation of these Terms of Use, and any other acts or omissions
                            relating to this Website.
                            <h3 class="yt-Bullet">
                                6. DISCLAIMER OF WARRANTIES</h3>
                            <br />
                            THE INFORMATION, CONTENT, PRODUCTS, SERVICES, AND MATERIALS CONTAINED ON THIS WEBSITE
                            (WHETHER PROVIDED BY YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.,
                            YOU, OTHER USERS, AFFILIATE NEWSPAPERS, AND OTHER AFFILIATES OR THIRD PARTIES),
                            INCLUDING WITHOUT LIMITATION, MATERIAL, TEXT, PHOTOS, GRAPHICS, AUDIO FILES, VIDEO,
                            AND LINKS, ARE PROVIDED "AS IS" AND “AS AVAILABLE” WITHOUT WARRANTIES OF ANY KIND,
                            EITHER EXPRESS OR IMPLIED.
                            <br />
                            <br />
                            TO THE MAXIMUM EXTENT PERMITTED BY LAW, YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.
                            DISCLAIMS ALL REPRESENTATIONS AND WARRANTIES, EXPRESS OR IMPLIED, INCLUDING BUT
                            NOT LIMITED TO WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE,
                            TITLE, NONINFRINGEMENT, FREEDOM FROM COMPUTER VIRUS, AND IMPLIED WARRANTIES ARISING
                            FROM COURSE OF DEALING OR COURSE OF PERFORMANCE.
                            <h3 class="yt-Bullet">
                                7. LIMITATION OF LIABILITY.</h3>
                            <br />
                            (a) IN NO EVENT SHALL YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.
                            BE LIABLE TO YOU FOR ANY DIRECT, INDIRECT, SPECIAL, PUNITIVE, INCIDENTAL, EXEMPLARY
                            OR CONSEQUENTIAL DAMAGES, OR ANY LOSS OR DAMAGES WHATSOEVER (EVEN IF YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.
                            HAS BEEN PREVIOUSLY ADVISED OF THE POSSIBILITY OF SUCH DAMAGES), WHETHER IN AN ACTION
                            UNDER CONTRACT, NEGLIGENCE, OR ANY OTHER THEORY, IN ANY MANNER ARISING OUT OF OR
                            IN CONNECTION WITH THE USE, INABILITY TO USE, PERFORMANCE OF, OR SERVICES PROVIDED
                            ON, THIS WEBSITE. YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.
                            ASSUMES NO RESPONSIBILITY AND SHALL NOT BE LIABLE FOR ANY DAMAGES TO, OR VIRUSES
                            THAT MAY INFECT, YOUR COMPUTER EQUIPMENT OR OTHER PROPERTY ON ACCOUNT OF YOUR ACCESS
                            TO, USE OF, BROWSING OF, OR DOWNLOADING OF ANY MATERIAL FROM THIS WEBSITE. YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.
                            ASSUMES NO RESPONSIBILITY OR LIABILITY IN ANY MANNER ARISING OUT OF OR IN CONNECTION
                            WITH ANY INFORMATION, CONTENT, PRODUCTS, SERVICES, OR MATERIAL AVAILABLE ON OR THROUGH
                            THIS WEBSITE, AS WELL AS ANY THIRD PARTY WEBSITE PAGES OR ADDITIONAL WEBSITES LINKED
                            TO THIS WEBSITE, FOR ANY ERROR, DEFAMATION, LIBEL, SLANDER, OMISSION, FALSEHOOD,
                            OBSCENITY, PORNOGRAPHY, PROFANITY, DANGER, OR INACCURACY CONTAINED THEREIN. THESE
                            LIMITATIONS SHALL APPLY NOTWITHSTANDING ANY FAILURE OF ESSENTIAL PURPOSE OF ANY
                            LIMITED REMEDY. BECAUSE SOME JURISDICTIONS DO NOT ALLOW THE EXCLUSION OR LIMITATION
                            OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATIONS MAY
                            NOT APPLY TO YOU. IN NO EVENT SHALL YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.’S
                            TOTAL LIABILITY TO YOU FOR ALL DAMAGES, LOSSES AND CAUSES OF ACTION, WHETHER IN
                            CONTRACT, TORT (INCLUDING BUT NOT LIMITED TO, NEGLIGENCE) OR OTHERWISE, EXCEED (A)
                            THE AMOUNT PAID BY YOU TO YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.
                            OR A NEWSPAPER AFFILIATE, IF ANY, OR (B) $100 (WHICHEVER IS LESS).
                            <br />
                            <br />
                            (b) YOU AND YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.
                            AGREE THAT THE WARRANTY DISCLAIMERS AND LIMITATIONS OF LIABILITY IN THESE TERMS
                            OF USE ARE MATERIAL, BARGAINED-FOR BASES OF THIS AGREEMENT, AND THAT THEY HAVE BEEN
                            TAKEN INTO ACCOUNT IN DETERMINING THE CONSIDERATION TO BE GIVEN BY EACH PARTY UNDER
                            THIS AGREEMENT AND IN THE DECISION BY EACH PARTY TO ENTER INTO THIS AGREEMENT. YOU
                            AND YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>, INC.
                            AGREE THAT THE WARRANTY DISCLAIMERS AND LIMITATIONS OF LIABILITY IN THESE TERMS
                            OF USE ARE FAIR AND REASONABLE.
                            <br />
                            <br />
                            (c) IF YOU ARE DISSATISFIED WITH THE WEBSITE OR DO NOT AGREE TO ANY PROVISIONS OF
                            THESE TERMS OF USE, YOUR SOLE AND EXCLUSIVE REMEDY IS TO DISCONTINUE USING THE WEBSITE,
                            EXCEPT AS MAY BE PROVIDED FOR IN SECTION 7(A) ABOVE.
                            <h3 class="yt-Bullet">
                                8. Cancellation, Termination, Transfer, and Expiration of Account or Services
                            </h3>
                            <br />
                            (a) Your may cancel or terminate your password, account and/or use of any service
                            provided on or through this Website, with or without cause at any time, upon providing
                            written notice to Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. If you
                            cancel or terminate your account or any services or upon expiration of your account
                            or service, your cancellation/termination/expiration will take effect immediately.
                            Upon termination, your right to use your account or service immediately ceases.
                            Your notice to Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. of cancellation
                            or termination must be sent via email or conventional mail to Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.’s address
                            as set forth in these Terms of Use.
                            <br />
                            <br />
                            (b) Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., in its
                            sole and absolute discretion, and at any time and with or without prior notice to
                            you. may suspend, cancel, transfer, or terminate your password, account and/or use
                            of any services provided on or through this Website for any reason whatsoever (including
                            and without limitation, due to lack of use, commercial use, cybersquatting, Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Inc.’s resolution of any dispute among multiple persons claiming the right to use
                            the same or similar accounts or services, or Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.’s belief,
                            in its absolute and sole discretion, that you have violated or acted inconsistently
                            with the letter or spirit of these Terms of Use). In the event of a dispute or conflict
                            among, or complaint from, users of the Website about another’s right to establish,
                            use, or maintain an account or service on the Website, Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Inc. reserves for itself the sole right to determine whether or how to resolve such
                            dispute, conflict or complaint, with or without factual or other investigation.
                            <br />
                            <br />
                            (c) Upon expiration, or cancellation or termination by either party, of your account,
                            your password, and/or use of any service provided on or through this Website, you
                            will have no right to any Material or Information you Submitted, and Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. may temporarily
                            or permanently block access to, remove, deactivate, delete, and discard all such
                            Material or Information contained therein (including but not limited to any
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website). Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. accepts
                            no liability for removed or deleted Material or Information. In addition, any contracts
                            (verbal, written, or assumed) with respect to your account, Material or Information
                            you Submit, and/or use of any service on the Website, will be terminated at Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.’s discretion.
                            You agree that Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. shall
                            not be liable to you or any third-party for any termination of your access to any
                            service.
                            <br />
                            <br />
                            (d) Upon suspension, cancellation, or termination of your account or your use of
                            any service provided on or through this Website (for whatever reason), there shall
                            be no refund of money you paid to Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.
                            <h3 class="yt-Bullet">
                                9. Permanence of Material</h3>
                            <br />
                            Subject to its suspension, cancellation, and termination rights and rights to remove
                            Material, Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. represents
                            that each
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website sponsored as "lifetime" will remain on this Website for the duration of
                            Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.’s existence,
                            and that each
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website sponsored as "one year" will remain on this Website for one year from the
                            date of sponsorship, unless: (a) this Website ceases to exist in whole or in part
                            during the relevant period; or (b) acts or events beyond the reasonable control
                            of Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. cause
                            a deletion, loss of data, failure to store, or other interruption in or termination
                            of the availability of the Website or a
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website that you sponsored, or any other service to which you subscribed. You may
                            at any time ask Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. to delete
                            without charge a
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website, or other service that you sponsored or purchased.
                            <h3 class="yt-Bullet">
                                10. Minors</h3>
                            <br />
                            The consent of a parent, guardian or educational supervisor shall be required before
                            a Minor can use the Website. Use of any part of the Website is confirmation that
                            the person is an adult or a Minor who has received permission from a parent, guardian
                            or educational supervisor to use the Website. A parent, guardian or educational
                            supervisor will be responsible for any activities of a Minor on the Website regardless
                            of whether the Minor has received permission from the parent, guardian or educational
                            supervisor to use the Website.
                            <h3 class="yt-Bullet">
                                11. Your account, password, and security</h3>
                            <br />
                            (a) Some services offered on the Website (such as the creation of a
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            Website, or other service) may require you to register and/or create an account
                            with Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. During
                            this process, you must select an account designation and password and provide certain
                            personal information to Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., including
                            a valid email address. In consideration of the use of Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.’s services,
                            you agree to: (a) provide true, accurate, current and complete information about
                            yourself as prompted by the registration form, and (b) maintain and promptly update
                            the personal information you provide to keep it true, accurate, current and complete.
                            If you provide any information that is untrue, inaccurate, not current or incomplete,
                            or Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. has reasonable
                            grounds to suspect that such information is untrue, inaccurate, not current or incomplete,
                            Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. has the
                            right to suspend or terminate your account and refuse any and all current or future
                            use of the Service (or any portion thereof).
                            <br />
                            <br />
                            (b) You are responsible for maintaining the confidentiality and security of your
                            account and password, and you are fully responsible for all activities that occur
                            under your password or account, and for any other actions taken in connection with
                            the account or password. You agree to (a) immediately notify Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. of any
                            known or suspected unauthorized use(s) of your password or account, or any known
                            or suspected breach of security, including loss, theft, or unauthorized disclosure
                            of your password or credit card information; and (b) ensure that you exit from your
                            account at the end of each session. Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. will not
                            be liable for any injury, loss or damage of any kind arising from or relating to
                            your failure to comply with (a) and (b) or for any acts or omissions by you or someone
                            else using your account and/or password.
                            <h3 class="yt-Bullet">
                                12. Links and Farming.</h3>
                            <br />
                            As a courtesy to you, this Website may offer links to other Websites. Some of these
                            Websites may be affiliated with Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. while
                            others are not. Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. is not
                            responsible for the contents of any Website pages created and maintained by organizations
                            independent of Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. Visiting
                            any such third-party Website pages is at your own risk. Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. has no
                            control of these third-party Website pages, nor can it guarantee the accuracy, completeness,
                            or timeliness of information in third-party Website pages. Your use of such information
                            is voluntary, and your reliance on such information should be made only after independent
                            review. References to commercial products or services within any such third-party
                            Website pages do not constitute or imply an endorsement by Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. By using
                            this Website you acknowledge that Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. is responsible
                            neither for the availability of, nor the content located on or through any third-party
                            Website pages.
                            <h3 class="yt-Bullet">
                                13. Trademarks.</h3>
                            <br />
                            YOUR
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%>™ is
                            a common law trademark of Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. Such trademarks
                            and other marks, logos, and names of Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., used
                            on or in connection with this Website may not be used in connection with any product
                            or service that is not under Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.’s ownership
                            or control. Furthermore, such trademarks may not be used in any manner that is likely
                            to cause confusion among customers or in any manner that disparages or discredits
                            Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. All other
                            trademarks not owned by Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. or its
                            affiliates that appear on this Website are the property of their respective owners,
                            who may or may not be affiliated with, connected to, or sponsored by Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. or its
                            affiliates.
                            <h3 class="yt-Bullet">
                                14. Consideration.</h3>
                            <br />
                            You acknowledge that these Terms of Use are supported by reasonable and valuable
                            consideration, the receipt and adequacy of which are hereby acknowledged. Without
                            limiting the foregoing, you acknowledge that such consideration includes, without
                            limitation, your use of this Website and receipt or use of data, content, products,
                            services, Material and Information available at or through this Website, the possibility
                            of our review, use or display of your user-generated content, and the possibility
                            of publicity and promotion from our review, use or display of your user-generated
                            content.
                            <h3 class="yt-Bullet">
                                15. Third Party Software.</h3>
                            <br />
                            If you elect to download or access any third party software that Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. makes
                            available in connection with the Website, you understand that you may have to agree
                            to that third party provider’s terms of use or license before you use such software.
                            You also agree that the use of any third party software or content obtained in connection
                            with the services provided on the Website does not transfer to you any rights, title
                            or interest in or to such software or content, and you agree that you will not use
                            any such software except as expressly authorized under that third party provider's
                            terms of use or license agreement. By downloading software made available through
                            the Website you are deemed to agree to the third party provider’s terms of use or
                            license agreement, the terms of which are incorporated by reference herein for the
                            benefit of such third party providers. If you do not agree to the third party’s
                            terms of use or license agreement, do not download its software.
                            <h3 class="yt-Bullet">
                                16. Jurisdiction, Applicable Law, and Limitations.</h3>
                            <br />
                            This Website is created and controlled by Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. in the
                            State of Nevada, U.S.A. You agree that these Terms of Use will be governed by and
                            construed in accordance with the laws of the United States of America and the State
                            of Nevada, without regard to its conflicts of law provisions. Use of this Website
                            is unauthorized in any jurisdiction that does not give effect to all provisions
                            of these Terms of Use. Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. makes
                            no claims or assurances that the Website is appropriate or may be downloaded outside
                            of the United States. You agree that all legal proceedings arising out of or in
                            connection with these Terms of Use, or services available on or through the Website
                            must be filed in a federal or state court located in Las Vegas, Nevada, within one
                            year of the time in which the events giving rise to such claim began, or your claim
                            will be forever waived and barred. You expressly submit to the exclusive jurisdiction
                            of said courts and consent to extraterritorial service of process.
                            <h3 class="yt-Bullet">
                                17. General.</h3>
                            <br />
                            <b>a. Enforceability</b><br />
                            <br />
                            If any portion of these Terms of Use is found to be void, invalid or otherwise unenforceable,
                            then that portion shall be deemed to be superseded by a valid, enforceable provision
                            that matches the intent of the original provision as closely as possible. The remainder
                            of these Terms of Use shall continue to be enforceable and valid according to terms
                            contained herein.
                            <br />
                            <br />
                            <b>b. Entire Agreement<br />
                                <br />
                            </b>Except as expressly provided in a particular “Legal Notice” or other notice
                            on particular pages of the Website, these Terms of Use, which hereby incorporate
                            by reference the terms of Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.’s Privacy
                            Policy, constitute the entire agreement between you and Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., superseding
                            all prior agreements regarding this Website.
                            <br />
                            <br />
                            <b>c. No Waiver<br />
                                <br />
                            </b>The failure of Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. to exercise
                            or enforce any right or provision of the Terms of Use shall not constitute a waiver
                            of said right or provision. Neither party hereto shall be deemed to be in default
                            of any provision of the Terms of Use or for failure in performance resulting from
                            acts or events beyond the reasonable control of such party and arising without its
                            fault or negligence, including, but not be limited to, acts of God, civil or military
                            authority, interruption of electric or telecommunication services, civil disturbances,
                            acts of war or terrorists, strikes, fires, floods or other catastrophes.
                            <br />
                            <br />
                            <b>d. Headings & Construction<br />
                                <br />
                            </b>The section titles in the Terms of Use are for your convenience only and carry
                            no contractual or legal effect whatsoever. The language in these Terms of Use shall
                            be interpreted in accordance with its fair meaning and shall not be strictly interpreted
                            for or against either party.
                            <br />
                            <br />
                            <b>e. Contact Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc.<br />
                                <br />
                            </b>For purposes of providing notice of cancellation or termination, contact Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. at <a href="mailto:<%=TributesPortal.Utilities.WebConfig.InfoEmail%>">
                                <%=TributesPortal.Utilities.WebConfig.InfoEmail%>
                            </a>or Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc, 2875 North
                            Lamb Blvd, Bldg 8, Las Vegas, NV 89115.
                        </div>
                    </div>
                </div>
                <!--yt-ContentPrimary-->
                <div class="yt-ContentSecondary">
                    <div class="yt-Panel yt-Panel-Tools">
                        <div class="yt-Panel-Body">
                            <h2>
                                Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></h2>
                            <div class="yt-TourLinks">
                                <ul>
                                    <li><a href="about.aspx">About Your
                                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></a></li>
                                    <li class="yt-Selected"><a href="contact.aspx">Contact</a></li>
                                    <li><a href="advertise.aspx">Advertise</a></li>
                                    <li><a href="termsofuse.aspx">Terms of Use</a></li>
                                    <li><a href="privacy.aspx">Privacy Policy</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div id="divYM" runat="server" class="yt-Panel" visible="false">
                        <uc1:LeftFeaturedPanel ID="LeftFeaturedPanel1" runat="server" />
                    </div>
                </div>
                <!--yt-ContentSecondary-->
                <div class="hack-clearBoth">
                </div>
                <div class="yt-ContentContainerImage">
                </div>
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
 <!-- <div class="yt-Footer">-->
        <uc:Footer ID="Footer1" runat="server" />
        <!--  </div>-->
        <!--yt-Footer-->
        <!--yt-Container-->
        <div class="upgrade">
            <h2>
                Please Upgrade Your Browser</h2>
            <p>
                This site&#39;s design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                    title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
                but its content is accessible to any browser or Internet device.</p>
        </div>
        <!--yt-upgrade-->
    </form>

    <script type="text/javascript">
        executeBeforeLoad();
      <% if (ConfigurationManager.AppSettings["ApiKey"] != null) { %>  
        function update_user_is_connected() {
            header_user_is_connected();
            FB.XFBML.parse();
        }
        function update_user_is_not_connected() {
            header_user_is_not_connected();
            FB.XFBML.parse();
        }                  
//        FB.init('<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', "/xd_receiver.htm",
//                 {"ifUserConnected": update_user_is_connected,
//                  "ifUserNotConnected": update_user_is_not_connected});
window.fbAsyncInit = function() {
    FB.init({
        appId  : '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>',
        status : true, // check login status
        cookie : true, // enable cookies to allow the server to access the session
        xfbml  : true,  // parse XFBML
        //channelUrl  : 'http://www.yourdomain.com/channel.html', // Custom Channel URL
        oauth : true //enables OAuth 2.0
    });

    FB.getLoginStatus(function(response) {
        if (response.authResponse) update_user_is_connected();
        else update_user_is_not_connected();
    });

    // This will be triggered as soon as the user logs into Facebook (through your site)
    FB.Event.subscribe('auth.login', function(response) {
        update_user_is_connected();
    });
};

      <% } %>         
    </script>

</body>
</html>
