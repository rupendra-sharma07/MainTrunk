///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ShareTributePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for sharing a tribute by inviting people to 
///                  visit/view the tribute.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using System.Reflection;
using System.ComponentModel;


#endregion

namespace TributesPortal.Tribute.Views
{
    public class ShareTributePresenter : Presenter<IShareTribute>
    {

        #region CLASS VARIABLES

        private TributeController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A EventController object to call the method of controller</param>
        public ShareTributePresenter([CreateNew] TributeController controller)
        {
            _controller = controller;
        }

        #endregion


        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This Method will called every time the view loads
        /// </summary>
        public override void OnViewLoaded()
        {
            UserIsAdmin();
        }

        /// <summary>
        /// This method will call the first time the view loads
        /// </summary>
        public override void OnViewInitialized()
        {
            View.EventInvitationCategoryList = _controller.GetEventInvitationCategories(View.TributeType);

            View.EventThemeList = _controller.GetEventThemeInfo(View.EventThemeID, View.TributeType); 
        }

        /// <summary>
        /// This method will call the Event controller method to set the RSVP status of the event
        /// </summary>
        public string InviteGuest()
        {
            Events objEvent = new Events();

            objEvent.EventAwaiting = View.GuestList;
            objEvent.TributeId = View.TributeID;
            //objEvent.ServerURL = View.URL;

            // save the list of email in the database and also send the email to all
            if (objEvent.EventAwaiting.Count > 0)
            {
                for (int i = 0; i < objEvent.EventAwaiting.Count; i++)
                {
                    EmailMessages objEmail = EmailMessages.Instance;
                    
                    //string body;
                    string EmailHref = View.EmailBody;

                    #region COMMENT
                    //if (!body.StartsWith("<font") || body.StartsWith("Someone"))
                    //    body = "<font style='font-size: 12px; font-family:Lucida Sans;'>" + body;
                    //else
                    //    body = View.EmailBody;

                    //int index = body.IndexOf("Tribute.");
                    //string tributeLink = body.Substring(index + 8);
                    //body = body.Remove(index + 8);

                    //if (View.PersonalMessage != "")
                    //{
                    //    body += "<br/><br/>" + "Personal Message:<br/>" + View.PersonalMessage.Replace("\n", "<br/>");
                    //}

                    //body += "<br/><br/>" + tributeLink;

                    //body += "<font style='font-size: 12px; font-family:Lucida Sans;'><p>----<br/>" + "Your Tribute Team</p>";
                    #endregion

                    //Put the theme here
                    TributesPortal.BusinessLogic.EventManager objEventManager = new TributesPortal.BusinessLogic.EventManager();
                    EventTheme objEventTheme = objEventManager.GetEventThemeByID(View.EventThemeID);
                    string strThemeFullSizeImage = objEventTheme != null ? objEventTheme.ThemeFullSizeImage : "";
                    string strBackgroundColor = objEventTheme != null ? objEventTheme.ThemeBackgroundColor : "";
                    //Start
                    //body = "<html><head><title></title></head><body><table width='100%' style='font-family:Calibri; text-align:center;'>";
                    //body += "<tr style='font-size: 10px; color:#A6A6A6;'><td>Having trouble seeing this email? <a href='" + EmailHref + "'>Visit our Tribute website.</a></td></tr>";
                    //body += "<tr align='center'><td><table style=\"background:url('" + GetImageURL(strThemeFullSizeImage) + "'); height:650px; margin: 20px; border: 5px ridge #C0C0C0; font-size: 12px; font-family:Calibri; text-align:center;\" width='520'>";                
                    //body += "<tr align='center' style='height:370px;'><td></td></tr><tr style='font-size: 8px;'><td>&nbsp;</td></tr>";
                    //body += "<tr><td>" + View.PersonalMessage + "</td></tr>";
                    //body += "<tr><td><b>Website:</b></td></tr>";
                    //body += "<tr><td><a href='" + EmailHref + "'>" + EmailHref + "</a></td></tr>";
                    //body += "<tr style='font-size: 8px;'><td>&nbsp;</td></tr></table></td></tr><tr></tr>";
                    //body += "<tr style='font-size: 18px;'><td><a href='" + EmailHref + "'><b>Please visit our Website to view and contribute to our Tribute</b></a></td></tr><tr></tr>";
                    //body += "<tr style='font-size: 8px; color:#A6A6A6;'><td>This email has a unique link just for you, please do not forward it to others.</td></tr>";
                    //body += "<tr style='font-size: 8px; color:#A6A6A6;'><td>Your Tribute respects your privacy. To block further invites from the sender or any other privacy concerns please click here.</td></tr>";
                    //body += "<tr style='font-size: 8px; color:#A6A6A6;'><td>Your Tribute, Inc.</td></tr>";
                    //body += "<tr style='font-size: 8px; color:#A6A6A6;'><td>2875 North Lamb Blvd. Bldg 8, Las Vegas, NV 89115</td></tr>";
                    //body += "<tr><td><img src='" + GetImageURL("../assets/images/arrow_ffffff.gif") + "' style='display:none' /></td></tr>";
                    //body += "</table></body></html>";
                    //End

                    StringBuilder objEmailBody = new StringBuilder();
                    objEmailBody.Append("<html>");
                    objEmailBody.Append("<head>");
                    objEmailBody.Append("<title>Share Event Mail</title>");
                    objEmailBody.Append("</head>");
                    objEmailBody.Append("<body text='#000000' link='#000000'>");
                    objEmailBody.Append("<table width='700' border='0' align='center' cellpadding='0' cellspacing='0'>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><table width='100%' border='0' cellspacing='10' cellpadding='0'>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>Having trouble seeing this email? <a href='" + EmailHref + "'> Visit our Event webpage.</a></font></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("</table></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td><table width='520' height='650' border='0' align='center' cellpadding='0' cellspacing='10' bgcolor='" + strBackgroundColor + "'>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'><img src='" + GetImageURL(strThemeFullSizeImage) + "' alt='Theme Photo' /></font></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>&nbsp;</font></td>");
                    objEmailBody.Append("</tr>");                   
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>" + View.PersonalMessage + " </font></td>");
                    objEmailBody.Append("</tr>");                    
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'><strong>Website:</strong></font></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'><a href='" + EmailHref + "'>" + EmailHref + "</a></font></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>&nbsp;</font></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("</table>");
                    objEmailBody.Append("</td>");
                    objEmailBody.Append("</tr>");                    
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td><table width='100%' border='0' cellspacing='5' cellpadding='0'>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font color='#666666' size='1' face='Verdana, Arial, Helvetica, sans-serif'>This email has a unique link just for you, please do not forward it to others.</font></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font color='#666666' size='1' face='Verdana, Arial, Helvetica, sans-serif'>Your Tribute respects your privacy. For any privacy concerns please click here.</font></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font color='#666666' size='1' face='Verdana, Arial, Helvetica, sans-serif'>Your Tribute, Inc.</font></td>");
                    objEmailBody.Append("</tr>");
                    objEmailBody.Append("<tr>");
                    objEmailBody.Append("<td align='center'><font color='#666666' size='1' face='Verdana, Arial, Helvetica, sans-serif'>2875 North Lamb Blvd. Bldg 8, Las Vegas, NV 89115</font></td>");
                    objEmailBody.Append("</tr></table></td>");
                    objEmailBody.Append("</tr></table></body></html>");

                    bool val = objEmail.SendMessages(View.EmailFrom, objEvent.EventAwaiting[i].UserName, View.EmailSubject, CreateBody(objEmailBody.ToString()), EmailMessages.TextFormat.Html.ToString());
                }

                View.GuestCount = objEvent.EventAwaiting.Count;

                return null;
            }
            else
            {
                string str = "Please select a contact to Share the tribute.";
                return str;
            }
        }
        private string GetImageURL(string imageURL)
        {
            string retImageURL = string.Empty;
            return retImageURL = imageURL.Replace("../", WebConfig.AppBaseDomain);

        }
        // <summary>
        /// This methods calls the control menthod to load the EventTheme
        /// </summary>
        /// <param name="invitationCategoryID">Event Invitation Category ID</param>
        /// <param name="tributeType">TributeType i.e. Wedding, Memorial etc.</param>
        public void LoadEventThemes(int invitationCategoryID, string tributeType)
        {
            View.EventThemeList = _controller.GetEventThemeInfo(invitationCategoryID, tributeType);
        }

        /// <summary>
        /// This method calls the control menthod to load the EventTheme
        /// </summary>
        /// <param name="themeID">EventThemeID</param>
        public void LoadTheme(int themeID)
        {
            EventTheme objEventTheme = _controller.GetEventThemeInfoByID(themeID);

            View.EventThemePreview = objEventTheme.ThemePreviewImage;
        }

        #endregion


        #region PRIVATE METHODS
        /// <summary>
        /// Method to set user admin info in the session
        /// </summary>
        private void UserIsAdmin()
        {
            UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            objUserInfo.UserId = View.UserID;
            objUserInfo.TributeId = View.TributeID;
            objUserInfo.TypeName = PortalEnums.TributeContentEnum.ShareTribute.ToString();
            objUserInfo.IsAdmin = View.IsAdmin;

            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add(PortalEnums.AdminInfoEnum.UserAdminInfo_InviteGuest.ToString(), objUserInfo, StateManager.State.Session);
        }

        /// <summary>
        /// This method is used to create the Body of the mail
        /// </summary>
        /// <param name="strSubject">Body of the Mail</param>
        /// <returns>This method will return the body of the mail</returns>
        private string CreateBody(string strBody)
        {
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append("<br/>");
            sbBody.Append(strBody);
            sbBody.Append("<br/>");

            return sbBody.ToString();
        }

        #endregion

        #endregion

    }
}



