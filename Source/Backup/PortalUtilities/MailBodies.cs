///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Utilities.MailBodies.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to create the mail bodies to be used within the application
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Text;
using System.Web;
using TributesPortal.BusinessEntities;


namespace TributesPortal.Utilities
{
    public class MailBodies
    {


        /// <summary>
        /// Mail body for Tribute Creation free.
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <returns></returns>
        public string CreationFree(String Creator, string TributeType, string TributeName, string TributeUrl, string TributeId)
        {
            SessionValue objSessionValue = null;
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);

            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p> Dear " + Creator + ",</p>");
            objStrb.Append("<p>The " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for " + TributeName + " has been created! </p>");
            if (objSessionvalue != null)
            {
                if (objSessionvalue.UserType == 1)
                    objStrb.Append("<p>Included is a 7-day free trial of all premium " + WebConfig.ApplicationWordForInternalUse.ToLower() + " features - Notes, Events, Photos and Videos. You can upgrade your " + WebConfig.ApplicationWordForInternalUse + " to a premium account at any time in your \"'<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'>My Profile</a>\" area.</p>");
                else
                    objStrb.Append("<p>Included is a 30-day free trial of all premium " + WebConfig.ApplicationWordForInternalUse.ToLower() + " features - Notes, Events, Photos and Videos. You can upgrade your " + WebConfig.ApplicationWordForInternalUse + " to a premium account at any time in your \"'<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'>My Profile</a>\" area.</p>");
            }
            else
                objStrb.Append("<p>Included is a 30-day free trial of all premium " + WebConfig.ApplicationWordForInternalUse.ToLower() + " features - Notes, Events, Photos and Videos. You can upgrade your " + WebConfig.ApplicationWordForInternalUse + " to a premium account at any time in your \"'<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'>My Profile</a>\" area.</p>");

            objStrb.Append("<p><Strong>Managing Your " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " </Strong><br/>");
            objStrb.Append("• Sign in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx'>http://www." + WebConfig.TopLevelDomain + "</a> <br/>");
            objStrb.Append("• View your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " at <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a> <br/>");
            objStrb.Append("• Add, modify or delete content at any time—in your \"<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'>My Profile</a>\" area, you can add additional administrators to assist in managing your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " and choose to be notified by email when visitors add content to your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " <br/>");
            objStrb.Append("• Find help, managing your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " or adding content, at the bottom of any page </p>");
            objStrb.Append("<p>-----<br/>");
            objStrb.Append("Your " + WebConfig.ApplicationWord + " Team</p></font>");
            return objStrb.ToString();
        }




        /// <summary>
        /// Mail body for Tribute Creation free. : Created for new tribute packages in website upgrade phase 1 by UD
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <returns></returns>
        public string CreationObituaryFree(String Creator, string TributeType, string TributeName, string TributeUrl, string TributeId)
        {
            // Changes for new tribute packages in website upgrade phase 1 by UD
            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<p style='font-size: 16px; font-family:Lucida Sans;'><b>Thank you for creating an Obituary with Your " + WebConfig.ApplicationWord.ToLower() + ".</b></p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>You have taken the first step towards");
            objStrb.Append(" building a heartfelt online memorial for<br/> your loved one. Below you will find links to articles ");
            objStrb.Append(" and other information that<br/> will help you get the most out of Your Tribute.</p>");
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;'>");
            objStrb.Append("<b>Your Obituary:</b> <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></p>");
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Start off on the right track</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("Read our <a href='http://support.yourtribute.com/forums/21551817-step-by-step-tutorials'>Step-By-Step Tutorials</a>");
            objStrb.Append(" to help you get started, then view our <a href='http://support.yourtribute.com/forums/97320-faqs'>FAQs</a>");
            objStrb.Append(" and<br/> browse our online <a href='http://support.yourtribute.com/forums/94844-how-to-guides'>How-To Guides</a>.");
            objStrb.Append(" Still need help? Email our friendly <a href='http://support.yourtribute.com/anonymous_requests/new'>Customer<br/> Support Team</a>.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Contribute to your Obituary</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("Log in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx'>http://www." + WebConfig.TopLevelDomain + "/log_in.aspx</a> using your <br/>");
            objStrb.Append(" email and password you used when signing up. You can also find your account <br/>");
            objStrb.Append("information in your welcome email. <br/></p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>We recommend that you begin by adding the obituary and writing your loved <br/>");
            objStrb.Append("one's story. Next, create photo albums and add family photos then share your<br/>");
            objStrb.Append("favorite videos. Finally, share the memorial with your friends and family and <br/>");
            objStrb.Append("invite them to add their memories.</p>");
            
            
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Keep up with Your Tribute</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("View our <a href='http://blog.yourtribute.com/'>Blog</a> and sign up to receive our ");
            objStrb.Append("<a href='http://eepurl.com/rlagp'>NewsLetter</a> for helpful tips, feature <br/>");
            objStrb.Append("updates, company news and special offers. You can also follow us on your <br/>");
            objStrb.Append("favorite social websites: <a href='http://www.facebook.com/yourtribute'>Facebook</a>,");
            objStrb.Append(" <a href='http://www.twitter.com/yourtribute'>Twitter</a>, ");
            objStrb.Append("<a href='https://plus.google.com/u/0/109473191564708020938/posts'>Google+</a>");
            objStrb.Append(" and <a href='http://pinterest.com/yourtribute/'>Pinterest</a>.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Upgrade your Obituary</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("At any time you can upgrade your Obituary by clicking the upgrade link on your <br/>");
            objStrb.Append("obituaries homepage. For a small one-time fee you can permanently remove the <br/>");
            objStrb.Append("ads from your obituary, or upgrade to a Memorial Tribute to take advantage of <br/>");
            objStrb.Append("all features and ensure that your loved one’s memorial remains online for years <br/>");
            objStrb.Append("to come. </p>");

            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;'><br/>Sincerely,<br/>");
            objStrb.Append("The Your Tribute Team</p>");
            
            return objStrb.ToString();
        }

          
       



        /// <summary>
        /// Mail body for premium obituary package : Created for new tribute packages in website upgrade phase 1 by UD
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <param name="TributeId"></param>
        /// <returns></returns>
        public string CreationPremiumObituary(String Creator, string TributeType, string TributeName, string TributeUrl, string TributeId)
        {
            
            StringBuilder objStrb = new StringBuilder();

            objStrb.Append("<p style='font-size: 16px; font-family:Lucida Sans;'><b>Thank you for creating an Obituary with Your " + WebConfig.ApplicationWord.ToLower() + ".</b></p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>You have taken the first step towards");
            objStrb.Append(" building a heartfelt online memorial for<br/> your loved one. Below you will find links to articles ");
            objStrb.Append(" and other information that<br/> will help you get the most out of Your Tribute.</p>");
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;'>");
            objStrb.Append("<b>Your Obituary:</b> <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></p>");
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Start off on the right track</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("Read our <a href='http://support.yourtribute.com/forums/21551817-step-by-step-tutorials'>Step-By-Step Tutorials</a>");
            objStrb.Append(" to help you get started, then view our <a href='http://support.yourtribute.com/forums/97320-faqs'>FAQs</a>");
            objStrb.Append(" and<br/> browse our online <a href='http://support.yourtribute.com/forums/94844-how-to-guides'>How-To Guides</a>.");
            objStrb.Append(" Still need help? Email our friendly <a href='http://support.yourtribute.com/anonymous_requests/new'>Customer<br/> Support Team</a>.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Contribute to your Obituary</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("Log in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx'>http://www." + WebConfig.TopLevelDomain + "/log_in.aspx</a> using your <br/>");
            objStrb.Append(" email and password you used when signing up. You can also find your account <br/>");
            objStrb.Append("information in your welcome email. <br/></p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>We recommend that you begin by adding the obituary and writing your loved <br/>");
            objStrb.Append("one's story. Next, create photo albums and add family photos then share your<br/>");
            objStrb.Append("favorite videos. Finally, share the memorial with your friends and family and <br/>");
            objStrb.Append("invite them to add their memories.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Keep up with Your Tribute</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("View our <a href='http://blog.yourtribute.com/'>Blog</a> and sign up to receive our ");
            objStrb.Append("<a href='http://eepurl.com/rlagp'>NewsLetter</a> for helpful tips, feature <br/>");
            objStrb.Append("updates, company news and special offers. You can also follow us on your <br/>");
            objStrb.Append("favorite social websites: <a href='http://www.facebook.com/yourtribute'>Facebook</a>,");
            objStrb.Append(" <a href='http://www.twitter.com/yourtribute'>Twitter</a>, ");
            objStrb.Append("<a href='https://plus.google.com/u/0/109473191564708020938/posts'>Google+</a>");
            objStrb.Append(" and <a href='http://pinterest.com/yourtribute/'>Pinterest</a>.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Upgrade your Obituary</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("At any time you can upgrade your Obituary by clicking the upgrade link on your <br/>");
            objStrb.Append("obituaries homepage. Upgrade to a Memorial Tribute to take advantage of all <br/>");
            objStrb.Append("features and ensure that your loved one’s memorial remains online for years to<br/>");
            objStrb.Append("come. </p>");

            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;'><br/>Sincerely,<br/>");
            objStrb.Append("The Your Tribute Team</p>");


            return objStrb.ToString();
        }



        /// <summary>
        /// mail body for memorial tribute 1 year  : Created for new tribute packages in website upgrade phase 1 by UD
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <param name="TributeId"></param>
        /// <returns></returns>
        public string CreationMemorialTribute1Year(String Creator, string TributeType, string TributeName, string TributeUrl, string TributeId)
        {
            StringBuilder objStrb = new StringBuilder();

            objStrb.Append("<p style='font-size: 16px; font-family:Lucida Sans;'>Thank you for creating a Memorial Tribute with Your " + WebConfig.ApplicationWord.ToLower() + ".</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>You have taken the first step towards");
            objStrb.Append(" building a heartfelt online memorial for<br/> your loved one. Below you will find links to articles ");
            objStrb.Append(" and other information that<br/> will help you get the most out of Your Tribute.</p>");
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;'>");
            objStrb.Append("<b>Your Memorial Tribute:</b> <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></p>");
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Start off on the right track</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("Read our <a href='http://support.yourtribute.com/forums/21551817-step-by-step-tutorials'>Step-By-Step Tutorials</a>");
            objStrb.Append(" to help you get started, then view our <a href='http://support.yourtribute.com/forums/97320-faqs'>FAQs</a>");
            objStrb.Append(" and<br/> browse our online <a href='http://support.yourtribute.com/forums/94844-how-to-guides'>How-To Guides</a>.");
            objStrb.Append(" Still need help? Email our friendly <a href='http://support.yourtribute.com/anonymous_requests/new'>Customer<br/> Support Team</a>.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Contribute to your Memorial Tribute</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("Log in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx'>http://www." + WebConfig.TopLevelDomain + "/log_in.aspx</a> using your <br/>");
            objStrb.Append(" email and password you used when signing up. You can also find your account <br/>");
            objStrb.Append("information in your welcome email. <br/></p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>We recommend that you begin by adding the obituary and writing your loved <br/>");
            objStrb.Append("one's story. Next, create photo albums and add family photos then share your<br/>");
            objStrb.Append("favorite videos. Finally, share the memorial with your friends and family and <br/>");
            objStrb.Append("invite them to add their memories.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Keep up with Your Tribute</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("View our <a href='http://blog.yourtribute.com/'>Blog</a> and sign up to receive our ");
            objStrb.Append("<a href='http://eepurl.com/rlagp'>NewsLetter </a>for helpful tips, feature <br/>");
            objStrb.Append("updates, company news and special offers. You can also follow us on your <br/>");
            objStrb.Append("favorite social websites: <a href='http://www.facebook.com/yourtribute'>Facebook</a>,");
            objStrb.Append(" <a href='http://www.twitter.com/yourtribute'>Twitter</a>, ");
            objStrb.Append("<a href='https://plus.google.com/u/0/109473191564708020938/posts'>Google+ </a>");
            objStrb.Append("and <a href='http://pinterest.com/yourtribute/'>Pinterest</a>.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Extend your Memorial Tribute</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("At any time you can extend your Memorial Tribute clicking the upgrade link on your <br/>");
            objStrb.Append("memorial tribute’s homepage. Add another year, or upgrade to a<br/>");
            objStrb.Append(" lifetime Memorial Tribute, to ensure that your loved one’s memorial remains<br/>");
            objStrb.Append("online for years to come.</p>");

            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;'><br/>Sincerely,<br/>");
            objStrb.Append("The Your Tribute Team</p>");

            return objStrb.ToString();
        }


        /// <summary>
        /// mail body for memorial life time : Created for new tribute packages in website upgrade phase 1 by UD
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <param name="TributeId"></param>
        /// <returns></returns>
        public string CreationMemorialLifeTime(String Creator, string TributeType, string TributeName, string TributeUrl, string TributeId)
        {
            StringBuilder objStrb = new StringBuilder();

            objStrb.Append("<p style='font-size: 16px; font-family:Lucida Sans;'><b>Thank you for creating a Memorial Tribute with Your " + WebConfig.ApplicationWord.ToLower() + ".</b></p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>You have taken the first step towards");
            objStrb.Append(" building a heartfelt online memorial for<br/> your loved one. Below you will find links to articles ");
            objStrb.Append(" and other information that<br/> will help you get the most out of Your Tribute.</p>");
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;'>");
            objStrb.Append("<b>Your Memorial Tribute:</b> <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></p>");
            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Start off on the right track</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("Read our <a href='http://support.yourtribute.com/forums/21551817-step-by-step-tutorials'>Step-By-Step Tutorials</a>");
            objStrb.Append(" to help you get started, then view our <a href='http://support.yourtribute.com/forums/97320-faqs'>FAQs </a>");
            objStrb.Append(" and<br/> browse our online <a href='http://support.yourtribute.com/forums/94844-how-to-guides'>How-To Guides</a>.");
            objStrb.Append(" Still need help? Email our friendly <a href='http://support.yourtribute.com/anonymous_requests/new'>Customer<br/> Support Team</a>.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Contribute to your Memorial Tribute</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("Log in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx'>http://www." + WebConfig.TopLevelDomain + "/log_in.aspx</a> using your <br/>");
            objStrb.Append(" email and password you used when signing up. You can also find your account <br/>");
            objStrb.Append("information in your welcome email. <br/></p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>We recommend that you begin by adding the obituary and writing your loved <br/>");
            objStrb.Append("one's story. Next, create photo albums and add family photos then share your<br/>");
            objStrb.Append("favorite videos. Finally, share the memorial with your friends and family and <br/>");
            objStrb.Append("invite them to add their memories.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;color: #7CC3EA;font-weight: bold;margin-bottom: -11px;'>Keep up with Your Tribute</p>");
            objStrb.Append("<p style='font-size: 12px; font-family:Lucida Sans;'>");
            objStrb.Append("View our <a href='http://blog.yourtribute.com/'>Blog</a> and sign up to receive our ");
            objStrb.Append("<a href='http://eepurl.com/rlagp'>NewsLetter</a> for helpful tips, feature <br/>");
            objStrb.Append("updates, company news and special offers. You can also follow us on your <br/>");
            objStrb.Append("favorite social websites: <a href='http://www.facebook.com/yourtribute'>Facebook</a>,");
            objStrb.Append(" <a href='http://www.twitter.com/yourtribute'>Twitter</a>, ");
            objStrb.Append("<a href='https://plus.google.com/u/0/109473191564708020938/posts'>Google+</a>");
            objStrb.Append(" and <a href='http://pinterest.com/yourtribute/'>Pinterest</a>.</p>");


            objStrb.Append("<p style='font-size: 14px; font-family:Lucida Sans;'><br/>Sincerely,<br/>");
            objStrb.Append("The Your Tribute Team</p>");

            return objStrb.ToString();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <returns></returns>
        public string Creation1Year(String Creator, string TributeType, string TributeName, string TributeUrl, string TributeId)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>Dear " + Creator + ",</p>");
            objStrb.Append("<p>Your " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for " + TributeName + " has been created! </p>");
            objStrb.Append("<p>You have chosen the 1 year billing option. You can upgrade your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " to a lifetime package at any time in your '<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'>My Profile</a>' area.</p>");
            objStrb.Append("<p><Strong>Managing Your " + TributeType + " Tribute </Strong><br/>");
            objStrb.Append("• Sign in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "'>http://www." + WebConfig.TopLevelDomain + "</a> <br/>");
            if (HttpContext.Current.Session["FromVideoTributeCreation"] != null)
            {
                objStrb.Append("• View your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " at <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + TributeId + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + TributeId + "</a> <br/>");

            }
            else
            {
                objStrb.Append("• View your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " at <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a> <br/>");
            }
            objStrb.Append("• Add, modify or delete content at any time—in your \"<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'>My Profile</a>\" area, you can add additional administrators to assist in managing your tribute and choose to be notified by email when visitors add content to your tribute <br/>");
            objStrb.Append("• Find help, managing your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " or adding content, at the bottom of any page </p>");
            if (HttpContext.Current.Session["FromVideoTributeCreation"] == null)
            {
                objStrb.Append("<p><Strong>Getting Started</Strong><br/>");
                objStrb.Append("We recommend that you begin by adding content to your " + WebConfig.ApplicationWordForInternalUse.ToLower() + ":<br/>");
                objStrb.Append("1. Create the <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/story.aspx'>Story</a>—communicate information about the people <br/>");
                objStrb.Append("2. Post an <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/events.aspx'>Event</a>—invite guests using the RSVP feature <br/>");
                objStrb.Append("3. Add <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/photos.aspx'>Photos</a> and <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/videos.aspx'>Videos</a>—share with your friends and family <p>");
            }
            objStrb.Append("<p>-----<br/>");
            objStrb.Append("Your " + WebConfig.ApplicationWord + " Team</p></font>");
            return objStrb.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <returns></returns>
        public string CreationLifeTime(String Creator, string TributeType, string TributeName, string TributeUrl, string TributeId)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>Dear " + Creator + ",</p>");
            objStrb.Append("<p>The " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for " + TributeName + " has been created!</p>");
            objStrb.Append("<p>You have chosen the lifetime billing option - your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will never expire.</p>");
            objStrb.Append("<p><Strong>Managing Your " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " </Strong><br/>");
            objStrb.Append("• Sign in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "'>http://www." + WebConfig.TopLevelDomain + "</a> <br/>");
            if (HttpContext.Current.Session["FromVideoTributeCreation"] != null)
            {
                objStrb.Append("• View your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " at <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + TributeId + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + TributeId + "</a> <br/>");
            }
            else
            {
                objStrb.Append("• View your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " at <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a> <br/>");
            }
            objStrb.Append("• Add, modify or delete content at any time—in your \"<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'>My Profile</a>\" area, you can add additional administrators to assist in managing your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " and choose to be notified by email when visitors add content to your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " <br/>");
            objStrb.Append("•  Find help, managing your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " or adding content, at the bottom of any page </p>");
            if (HttpContext.Current.Session["FromVideoTributeCreation"] == null)
            {
                objStrb.Append("<p><Strong>Getting Started</Strong><br/>");
                objStrb.Append("We recommend that you begin by adding content to your " + WebConfig.ApplicationWordForInternalUse.ToLower() + ":<br/>");
                objStrb.Append("1. Create the <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/story.aspx'>Story</a>—communicate information about the people <br/>");
                objStrb.Append("2. Post an <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/events.aspx'>Event</a>—invite guests using the RSVP feature <br/>");
                objStrb.Append("3. Add <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/photos.aspx'>Photos</a> and <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/videos.aspx'>Videos</a>—share with your friends and family </p>");
            }
            objStrb.Append("<p>-----<br/>");
            objStrb.Append("Your " + WebConfig.ApplicationWord + " Team</p></font>");
            return objStrb.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <returns></returns>
        public string Creation1YearAuto(String Creator, string TributeType, string TributeName, string TributeUrl, string TributeId)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>Congratulations " + Creator + ",</p>");
            objStrb.Append("<p>Your " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for " + TributeName + " has been created! </p>");
            objStrb.Append("<p>You have chosen the 1 year billing option, which will automatically renew each year. You can turn off auto-renewal or upgrade your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " to a lifetime package at any time in your \"<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'><U>My Profile</U></a>\" area.</p>");
            objStrb.Append("<p><Strong>Managing Your " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " </Strong><br/>");
            objStrb.Append("• Sign in to your account at <a href='http://www." + WebConfig.TopLevelDomain + "'>http://www." + WebConfig.TopLevelDomain + "</a> <br/>");
            if (HttpContext.Current.Session["FromVideoTributeCreation"] != null)
            {
                objStrb.Append("• View your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " at <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + TributeId + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + TributeId + "</a> <br/>");
            }
            else
            {
                objStrb.Append("• View your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " at <a href='http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'> http://" + TributeType.Replace(" ", "").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a> <br/>");
            }
            objStrb.Append("• Add, modify or delete content at any time—in your \"<a href='http://www." + WebConfig.TopLevelDomain + "/log_in.aspx?PageName=myprofile'>My Profile</a>\" area, you can add additional administrators to assist in managing your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " and choose to be notified by email when visitors add content to your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " <br/>");
            objStrb.Append("•  Find help, managing your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " or adding content, at the bottom of any page </p>");
            if (HttpContext.Current.Session["FromVideoTributeCreation"] == null)
            {
                objStrb.Append("<p><Strong>Getting Started</Strong> <br/>");
                objStrb.Append("We recommend that you begin by adding content to your " + WebConfig.ApplicationWordForInternalUse.ToLower() + ": <br/>");
                objStrb.Append("1. Create the <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/story.aspx'>Story</a>—communicate information about the people <br/>");
                objStrb.Append("2. Post an <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/events.aspx'>Event</a>—invite guests using the RSVP feature <br/>");
                objStrb.Append("3. Add <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/photos.aspx'>Photos</a> and <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "/videos.aspx'>Videos</a>—share with your friends and family </p>");
            }
            objStrb.Append("<p>-----<br/>");
            objStrb.Append("Your " + WebConfig.ApplicationWord + " Team</p></font>");
            return objStrb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <returns></returns>
        //(string Adminmail, string OtherAdminMails,string TributeId, string TributeName,string TributeType,string TributeUrl)
        public string AdminRequest(string userName, string TributeType, string TributeName, string TributeUrl, string TributeId, string email)
        {

            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);


            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + userName + " would like to add you as an administrator to the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + ".");
            objStrb.Append(" As a " + WebConfig.ApplicationWordForInternalUse.ToLower() + " administrator you can add, modify or delete any of the " + WebConfig.ApplicationWordForInternalUse.ToLower() + "s content. </p>");
            objStrb.Append("<p>To accept the administrator request, follow the link below: <br/>");
            objStrb.Append("<a href='http://" + Servername + "/log_in.aspx?TributeUrl=" + TributeUrl + "&TributeType=" + TributeType + "&Email=" + email + "' >http://www." + WebConfig.TopLevelDomain + "</a></p>");
            objStrb.Append("<p>If you do not want to become an administrator, please disregard this message.</p>");
            objStrb.Append("<p>-----<br/>");
            objStrb.Append("Your " + WebConfig.ApplicationWord + " Team</p></font>");
            return objStrb.ToString();


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <returns></returns>
        public string AdminRequestConfirm(String Creator, string TributeType, string TributeName, string TributeUrl)
        {
            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><P>Congratulations " + Creator + ",</P><br/>");
            objStrb.Append("<P>You were successfully added as an administrator to the James William Carmichael Memorial " + WebConfig.ApplicationWordForInternalUse + "!</P>");
            objStrb.Append("<P>Managing Your " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " </P>");
            objStrb.Append("<P>• Sign in to your account at http://www." + WebConfig.TopLevelDomain + " </P>");
            objStrb.Append("<P>• View your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " at http://memorial." + WebConfig.TopLevelDomain + "/jamescarmichael </P>");
            objStrb.Append("<P>• Add, modify or delete content at any time—in your “My Profile” area, you can add additional administrators to assist in managing your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " and choose to be notified by email when visitors add content to your " + WebConfig.ApplicationWordForInternalUse.ToLower() + " </P>");
            objStrb.Append("<P>• Find help, managing your tribute or adding content, at the bottom of any page </P>");
            objStrb.Append("<P>Getting Started</P>");
            objStrb.Append("<P>We recommend that you begin by adding content to your " + WebConfig.ApplicationWordForInternalUse.ToLower() + ":</P>");
            objStrb.Append("<P>1. Create the Story—communicate information about the people </P>");
            objStrb.Append("<P>2. Post an Event—invite guests using the RSVP feature</P>");
            objStrb.Append("<P>3. Add Photos and Videos—share with your friends and family</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></font>");
            return objStrb.ToString();

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public string ForgetPassword(String Creator, String Password)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><P>Dear " + Creator + ",</P>");
            objStrb.Append("<P>Your password for Your " + WebConfig.ApplicationWord + " is: " + Password + "</P>");
            objStrb.Append("<P> To reset your password, please sign in to your account at <a href='http://" + Servername + "/log_in.aspx'> http://www." + WebConfig.TopLevelDomain + ".</a>  </P>");
            objStrb.Append("Click \"My Profile\" and then select the \"Change Email/Password\" link. ");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></font>");
            return objStrb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Creator"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public string ChangeEmail(String Creator, String email)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>Dear " + Creator + ",</P><br/>");
            objStrb.Append("<P>Your email for Your " + WebConfig.ApplicationWord + " is: " + email + "</P>");
            objStrb.Append("<P> To reset your password, please sign in to your account at <a href='http://" + Servername + "/Users/log_in.aspx'> http://www." + WebConfig.TopLevelDomain + ".</a>  </P>");
            objStrb.Append("<P>Click \"My Profile\" and then select the \"Change Email/Password\" link. </P>");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }

        public string ChangeEmailPassword(String Creator, String email, string Password)
        {
            TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
            string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>Dear " + Creator + ",</P><br/>");
            objStrb.Append("<P>Your email for Your " + WebConfig.ApplicationWord + " is: " + email + "</P>");
            objStrb.Append("<P>Your password for Your " + WebConfig.ApplicationWord + " is: " + Password + "</P>");
            objStrb.Append("<P> To reset your password, please sign in to your account at <a href='http://" + Servername + "/Users/log_in.aspx'> http://www." + WebConfig.TopLevelDomain + ".</a>  </P>");
            objStrb.Append("<P>Click \"My Profile\" and then select the \"Change Email/Password\" link. </P>");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sponsor"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <param name="Expiry"></param>
        /// <returns></returns>
        public string TributeSponsor1Year(String Sponsor, int TributeId, string TributeType, string TributeName, string TributeUrl, string Expiry)
        {
            StateManager stateManager = StateManager.Instance;
            string SentFrom = (string)stateManager.Get("SentFrom", StateManager.State.Session);
            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><P>" + Sponsor + " sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for 1 year.</P>");
            objStrb.Append("<P>The " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will now expire on " + Expiry + "</P>");
            objStrb.Append("<P>To view the " + WebConfig.ApplicationWordForInternalUse.ToLower() + ", follow the link below:</P>");
            //if (HttpContext.Current.Session["SentFrom"] == "VideoTributeSpons")
            if (SentFrom == "VideoTributeSpons")
            {
                objStrb.Append("<P> <a href='http://video." + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + TributeId.ToString() + "' >http://video." + "" + WebConfig.TopLevelDomain + "//Video/VideoTribute.aspx?tributeId=" + TributeId.ToString() + "</a></P><br/>");
            }
            else
            {
                objStrb.Append("<P> <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + "" + WebConfig.TopLevelDomain + "/" + TributeUrl + "' >http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + "" + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></P><br/>");
            }
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></font>");
            return objStrb.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sponsor"></param>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <param name="Expiry"></param>
        /// <returns></returns>
        public string TributeSponsorLife(String Sponsor, int Tributeid, string TributeType, string TributeName, string TributeUrl, string Expiry)
        {
            StateManager stateManager = StateManager.Instance;
            string SentFrom = (string)stateManager.Get("SentFrom", StateManager.State.Session);
            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><P>" + Sponsor + " sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for Life.</P><br/>");
            objStrb.Append("<P>The " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will never expire!</P>");
            objStrb.Append("<BR>To view the " + WebConfig.ApplicationWordForInternalUse.ToLower() + ", follow the link below:<br/>");
            //if (HttpContext.Current.Session["SentFrom"] == "VideoTributeSpons")
            if (SentFrom == "VideoTributeSpons")
            {
                objStrb.Append("<P> <a href='http://video." + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + Tributeid.ToString() + "' >http://video." + "" + WebConfig.TopLevelDomain + "/Video/VideoTribute.aspx?tributeId=" + Tributeid.ToString() + "</a></P><br/>");
            }
            else
            {
                objStrb.Append("<P> <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + "" + WebConfig.TopLevelDomain + "/" + TributeUrl + "' >http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + "" + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></P><br/>");
            }
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <param name="Expiry"></param>
        /// <returns></returns>
        public string TributeSponsorAnon1Year(string TributeType, string TributeName, string TributeUrl, string Expiry)
        {
            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>An anonymous person has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for 1 year.</P><br/>");
            objStrb.Append("<P>The " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will now expire " + Expiry + "</P>");
            objStrb.Append("<BR>To view the tribute, follow the link below:<br/>");
            objStrb.Append("<BR> <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + "" + WebConfig.TopLevelDomain + "/" + TributeUrl + "' ></a>http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + "" + WebConfig.TopLevelDomain + "/" + TributeUrl + "<br/>");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TributeType"></param>
        /// <param name="TributeName"></param>
        /// <param name="TributeUrl"></param>
        /// <param name="Expiry"></param>
        /// <returns></returns>
        public string TributeSponsorAnonLife(string TributeType, string TributeName, string TributeUrl, string Expiry)
        {
            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>An anonymous person has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for Life.</P><br/>");
            objStrb.Append("<P>The " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will never expire!</P>");
            objStrb.Append("<P>To view the " + WebConfig.ApplicationWordForInternalUse.ToLower() + ", follow the link below:</P>");
            objStrb.Append("<P> <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + "" + WebConfig.TopLevelDomain + "/" + TributeUrl + "' >http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + "" + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></P><br/>");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }

        public string TributeSponsorAnon1Year(string sponsor, string TributeType, string TributeName, string TributeUrl, string Expiry)
        {
            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>" + sponsor + " has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for 1 year.</P><br/>");
            objStrb.Append("<P>The " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will now expire on " + Expiry + "</P>");
            objStrb.Append("<BR>To view the " + WebConfig.ApplicationWordForInternalUse.ToLower() + ", follow the link below:<br/>");
            objStrb.Append("<BR> <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'>http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a><br/>");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }

        public string TributeSponsorAnonLife(string sponsor, string TributeType, string TributeName, string TributeUrl, string Expiry)
        {
            StringBuilder objStrb = new StringBuilder();
            objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>" + sponsor + " has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for Life.</P><br/>");
            objStrb.Append("<P>The " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will never expire!</P>");
            objStrb.Append("<P>To view the " + WebConfig.ApplicationWordForInternalUse.ToLower() + ", follow the link below:</P>");
            objStrb.Append("<P> <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'>http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></P><br/>");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }


        public string TributeSponsor1YearWithMessage(string sponsor, string Message, string TributeType, string TributeName, string TributeUrl, string Expiry)
        {
            StringBuilder objStrb = new StringBuilder();
            if (Message != string.Empty)
            {
                objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>" + sponsor + " has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for 1 year with the following message:</P>");
                objStrb.Append("<P><I>" + Message + "</I></P>");
            }
            else
            {
                objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>" + sponsor + " has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for 1 year.</P>");
            }

            objStrb.Append("<br/><P>The " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will now expire on " + Expiry + "</P>");
            objStrb.Append("<BR>To view the " + WebConfig.ApplicationWordForInternalUse.ToLower() + ", follow the link below:<br/>");
            objStrb.Append("<BR> <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'>http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a><br/>");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }

        public string TributeSponsorLifeWithMessage(string sponsor, string Message, string TributeType, string TributeName, string TributeUrl, string Expiry)
        {
            StringBuilder objStrb = new StringBuilder();
            if (Message != string.Empty)
            {
                objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>" + sponsor + " has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for Life with the following message:</P>");
                objStrb.Append("<P><I>" + Message + "</I></P>");
            }
            else
            {
                objStrb.Append("<font style='font-size: 12px; font-family:Lucida Sans;'><p><P>" + sponsor + " has sponsored the " + TributeName + " " + TributeType + " " + WebConfig.ApplicationWordForInternalUse + " for Life.</P><br/>");
            }

            objStrb.Append("<br/><P>The " + WebConfig.ApplicationWordForInternalUse.ToLower() + " will never expire!</P>");
            objStrb.Append("<P>To view the " + WebConfig.ApplicationWordForInternalUse.ToLower() + ", follow the link below:</P>");
            objStrb.Append("<P> <a href='http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "'>http://" + TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + TributeUrl + "</a></P><br/>");
            objStrb.Append("<P>-----</P>");
            objStrb.Append("<P>Your " + WebConfig.ApplicationWord + " Team</P></p></font>");
            return objStrb.ToString();
        }

    }

}
