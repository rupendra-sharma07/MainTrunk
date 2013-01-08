///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.EventManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the basic event methods
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using TributesPortal.Utilities;
using System.Security.Cryptography;

#endregion


/// <summary>
///Tribute Portal-Event Manager Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Manager class for the Event.
/// </summary>

namespace TributesPortal.BusinessLogic
{
    public class EventManager
    {
        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This method will call the Event Resource class method to get the 
        /// Image List, Event Type, Country List, and Event Detail
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Tribute and Event information</param>
        /// <returns>This method will return the Events object which contain the Event information</returns>
        public Events GetEventInfo(Events objEvent)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                return objEventRes.GetEventInfo(objEvent);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Resource class method to Add the event in the database
        /// And send the email to all admin about the event insertion for all public event
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Event information</param>
        public void SaveEvent(Events objEvent)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                object identity = objEventRes.SaveEvent(objEvent);

                // Send the email to all the adminstrator if event is public that event has been created
                if ((objEvent != null) && (objEvent.CustomError == null) && (identity != null)
                    && (int.Parse(identity.ToString()) != 0) && (objEvent.IsPrivate == false))
                {
                    string EmailSubject = objEvent.FirstName + " " + objEvent.LastName + " added a new event on Your " + WebConfig.ApplicationWordForInternalUse.ToString() + "...";
                    string EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objEvent.FirstName + " " + objEvent.LastName + " added a new event in the " + objEvent.TributeName + " " + objEvent.TributeType + " " + WebConfig.ApplicationWordForInternalUse.ToString() + ".</p><p> To view the event, follow the link below: <br/> " + objEvent.ServerURL + " </p>" + "<p>----" + "<br/>" + "Your " + WebConfig.ApplicationWord.ToString() + " Team</p></font>";

                    SendEmail(objEvent.TributeId, EmailSubject, EmailBody.Replace("##", identity.ToString()));
                }

                // If an event name an event type combination already exist in database then 
                // return an error message
                if (int.Parse(identity.ToString()) == 0)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = "This Event Name '" + objEvent.EventName + "' with this Event Type already exist";
                    objEvent.CustomError = objError;
                }
                else    // otherwise return the eventid
                {
                    objEvent.EventID = int.Parse(identity.ToString());
                }

                SessionValue objSessionValue = null;
                StateManager objStateManager = StateManager.Instance;
                objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);
                if ((objEvent != null) && (objEvent.CustomError == null) && (identity != null)
                   && (int.Parse(identity.ToString()) != 0))
                {
                   object value =  objEventRes.SaveRsvpForCreator(objEvent,objSessionValue);
                   if (value != null)
                   {
                       if (int.Parse(value.ToString()) != 0)
                       {
                           //Insert the Hashcode for the Guest
                           string Hashcode = GetHashCode(int.Parse(value.ToString()));
                           objEventRes.InsertHashCodeForGuest(int.Parse(value.ToString()), Hashcode);
                       }
                   }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Resource class method to Update the event in the database
        /// And send the email to all admin about the event updation for all public event and also
        /// resend the mail to all guest list about teh event updation
        /// </summary>
        /// <param name="objEvent">An Event Object which contain updated Event information</param>
        public void UpdateEvent(Events objEvent)
        {
            try
            {
                EventResource objEventRes = new EventResource();

                // First get the Guest List for the event which are going to update
                Events guestList = objEventRes.GetEventGuestList(objEvent);

                // Now Update the Event Detail
                object identity = objEventRes.UpdateEvent(objEvent);

                // Send the email to all the adminstrator if event is public that event has been updated
                if ((objEvent != null) && (objEvent.CustomError == null) && (identity != null)
                    && (int.Parse(identity.ToString()) != 0) && (objEvent.IsPrivate == false))
                {
                    string EmailSubject = objEvent.FirstName + " " + objEvent.LastName + " updated the event \"" + objEvent.EventName + "\"";
                    string EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objEvent.FirstName + " " + objEvent.LastName + " updated the event \"" + objEvent.EventName + "\"<br/> <br/> To see more details, follow the link below: <br/> " + objEvent.ServerURL.ToLower() + "<br/>" + "----" + "<br/>" + "Your Tribute Team</p></font>";

                    SendEmail(objEvent.TributeId, EmailSubject, EmailBody.Replace("##", objEvent.EventID.ToString()));
                }

                // If an event name an event type combination already exist in database then 
                // return an error message
                if (int.Parse(identity.ToString()) == 0)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = "This Event Name '" + objEvent.EventName + "' with this Event Type already exist";
                    objEvent.CustomError = objError;
                }
                else   // Otherwise send the mail to all Guests about the updation
                {
                    // Send the email to all the Guest who are invited for the event that event has been updated
                    if (guestList.EventAwaiting != null)
                    {
                        for (int i = 0; i < guestList.EventAwaiting.Count; i++)
                        {
                            Events emailEvent = new Events();

                            emailEvent.EventID = objEvent.EventID;
                            emailEvent.EmailId = guestList.EventAwaiting[i].UserName.ToString();

                            string EmailSubject = objEvent.FirstName + " " + objEvent.LastName + " invited you to the event " + objEvent.TributeName;
                            string EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objEvent.FirstName + " " + objEvent.LastName + " invited you to the event '" + objEvent.EventName + "' in the " + objEvent.TributeName + " Tribute. <br/> <br/> To RSVP and see more details, follow the link below: <br/> <br/>";

                            string href = objEvent.InviteGuestURL + "?EventID=" + objEvent.EventID + "&TributeID=" + objEvent.TributeId + "&mode=emailPage" + "&Email=" + emailEvent.EmailId;
                            EmailBody += href + "'>" + "http://" + objEvent.TributeType.ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "/event.aspx?&EventId=" + objEvent.EventID + "</a>" + "<br/> <br/>" + "----" + "<br/>" + "Your Tribute Team</p></font>";

                            EmailMessages objEmail = EmailMessages.Instance;
                            bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", emailEvent.EmailId, EmailSubject, CreateBody(EmailBody), EmailMessages.TextFormat.Html.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Resource class method to get the event list from the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Tribute Id for which wants to get the event list</param>
        /// <returns>This method will return the list of Events object which contain the Event list</returns>
        public IList<Events> GetEventList(Events objEvent)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                return objEventRes.GetEventList(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Resource class method to get the event detail in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Event Id for which wants to get the detail of the event</param>
        /// <returns>This method will return the Events object which contain the Event information</returns>
        public Events GetFullEvent(Events objEvent)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                return objEventRes.GetFullEvent(objEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CompleteGuestList> GetCompleteGuestList(int EventId, string Hashcode, bool isCreator)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                return objEventRes.GetCompleteGuestList(EventId, Hashcode, isCreator);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event Resource class method to Delete the event in the database
        /// And send the email to all admin about the event deletion for all public event and also
        /// resend the mail to all guest list about teh event deletion
        /// </summary>
        /// <param name="objEvent">An Event Object which contain event id which wants to delete</param>
        public void DeleteEvent(Events objEvent)
        {
            try
            {
                EventResource objEventRes = new EventResource();

                // First get the Guest List for the event which are going to update
                Events guestList = objEventRes.GetEventGuestList(objEvent);
                TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                //string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

                // Now Delete the Event and Guest List
                objEventRes.DeleteEvent(objEvent);

                // Send the email to all the adminstrator that event has been updated
                if ((objEvent != null) && (objEvent.CustomError == null) && (objEvent.IsPrivate == false))
                {
                    string EmailSubject = string.Empty;

                    if (!string.IsNullOrEmpty(objEvent.LastName))
                        EmailSubject = objEvent.FirstName + " " + objEvent.LastName + " cancelled the event \"" + objEvent.EventName + "\"";
                    else
                        EmailSubject = objEvent.FirstName + " cancelled the event \"" + objEvent.EventName + "\"";
                    string EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objEvent.FirstName + " " + objEvent.LastName + " cancelled the event \"" + objEvent.EventName + "\"" + " in the " + objEvent.TributeName + " " + objEvent.TributeType + "  Tribute." +
                        "<br/><br/>" + "To view the tribute, follow the link below:" + "<br/>";
                    //string href = "<a href='http://" + objEvent.TributeType.Trim().ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "'> http://" + Servername + "/" + objEvent.TributeURL + "</a>" + "<br/> <br/>" + "----" + "<br/>" + "Your Tribute Team</p></font>";
                    string href = "<a href='http://" + objEvent.TributeType.Trim().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "'> http://" + objEvent.TributeType.Trim().ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "</a>" + "<br/> <br/>" + "----" + "<br/>" + "Your Tribute Team</p></font>";
                    EmailBody += href;

                    SendEmail(objEvent.TributeId, EmailSubject, EmailBody);
                }

                if (guestList.EventAwaiting != null)
                {
                    // Send the email to all the Guest who are invited for the event that event has been updated
                    for (int i = 0; i < guestList.EventAwaiting.Count; i++)
                    {
                        Events emailEvent = new Events();
                        emailEvent.EventID = objEvent.EventID;
                        emailEvent.EmailId = guestList.EventAwaiting[i].UserName.ToString();
                        string EmailSubject = string.Empty;

                        if (!string.IsNullOrEmpty(objEvent.LastName))
                            EmailSubject = objEvent.FirstName + " " + objEvent.LastName + " cancelled the event \"" + objEvent.EventName + "\"";
                        else
                            EmailSubject = objEvent.FirstName + " cancelled the event \"" + objEvent.EventName + "\"";

                        string EmailBody = "<font style='font-size: 12px; font-family:Lucida Sans;'><p>" + objEvent.FirstName + " " + objEvent.LastName + "  cancelled the event \"" + objEvent.EventName + "\"" + " in the " + objEvent.TributeName + " " + objEvent.TributeType + " Tribute." +
                        "<br/><br/>" + "To view the tribute, follow the link below:" + "<br/>";
                        //string href = "<a href='http://" + objEvent.TributeType.Trim().ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "'> http://" + Servername + "/" + objEvent.TributeURL + "</a>" + "<br/> <br/>" + "----" + "<br/>" + "Your Tribute Team</p></font>";
                        string href = "<a href='http://" + objEvent.TributeType.Trim().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "'> http://" + objEvent.TributeType.Trim().ToLower().Replace("new baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "</a>" + "<br/> <br/>" + "----" + "<br/>" + "Your Tribute Team</p></font>";
                        //string href = "<a href='http://" + objEvent.TributeType.Trim().ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "'> http://" + objEvent.TributeType.Trim().ToLower() + "." + WebConfig.TopLevelDomain + "/" + "/" + objEvent.TributeURL + "</a>" + "<br/> <br/>" + "----" + "<br/>" + "Your Tribute Team</p></font>";
                        EmailBody += href;

                        EmailMessages objEmail = EmailMessages.Instance;
                        bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", emailEvent.EmailId, EmailSubject, CreateBody(EmailBody), EmailMessages.TextFormat.Html.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will call the Event resource method to set the RSVP status for a Guest of the event
        /// </summary>
        /// <param name="objGuestToAdd">An CompleteGuestList Object which contain Guest and it's RSVP status</param>
        /// <param name="objEvent">An Events Object which contain event details</param>
        public void AddRsvp(CompleteGuestList objGuestToAdd, int EventId, int UserId)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                int RSVPId =objEventRes.AddRsvp(objGuestToAdd, EventId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method will call the Event resource method to save the RSVPs for a Guest of the event
        /// </summary>
        /// <param name="objGuestToAdd">An CompleteGuestList Object which contain Guest and it's RSVP status</param>
        //public void SaveRsvp(IList<CompleteGuestList> objGuestToAdd, int EventId, int counter, IList<CompleteGuestList> lstCompleteGuestList, int countRSVP)
        //{
        //    try
        //    {
        //        EventResource objEventRes = new EventResource();
        //        objEventRes.SaveRsvp(objGuestToAdd, EventId, counter, lstCompleteGuestList, countRSVP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}



        public IList<CompleteGuestList> SaveRsvp(IList<CompleteGuestList> objGuestToAdd, int EventId)
        {
            IList<CompleteGuestList> objCompleteGuestList = new List<CompleteGuestList>(); 
            try
            {
                EventResource objEventRes = new EventResource();
                objCompleteGuestList =objEventRes.SaveRsvp(objGuestToAdd, EventId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objCompleteGuestList;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objGuestToAdd"></param>
        /// <returns></returns>
        public object UpdateRsvp(CompleteGuestList objGuestToAdd)
        {
            object identity = null;
            try
            {
                EventResource objEventRes = new EventResource();
                identity = objEventRes.UpdateRsvp(objGuestToAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return identity;
        }

        public CompleteGuestList GetMealOptions(int GuestId)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                return objEventRes.GetMealOptionList(GuestId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CompleteGuestList> GetEmailIdsForEvent(int GuestId)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                return objEventRes.GetEmailIdsForEvent(GuestId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guestId"></param>
        /// <param name="additionalGuestId"></param>
        public void DeleteRsvp(int guestId, int additionalGuestId)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                objEventRes.DeleteRsvp(guestId, additionalGuestId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// This method will call the Event resource method to invite the guest for the event
        /// </summary>
        /// <param name="objEvent">An Event Object which contain event id and Guest List</param>
        public int InviteGuest(Events objEvent)
        {
            int count = 0;

            try
            {
                StateManager objStateManager = StateManager.Instance;
                SessionValue objSessionValue = (SessionValue)objStateManager.Get(PortalEnums.SessionValueEnum.objSessionvalue.ToString(), StateManager.State.Session);

                StateManager objStateManager_ = StateManager.Instance;
                Tributes objTribute = (Tributes)objStateManager_.Get(PortalEnums.SessionValueEnum.TributeSession.ToString(), StateManager.State.Session);

                TributesPortal.Utilities.StateManager stateManager = TributesPortal.Utilities.StateManager.Instance;
                string Servername = (string)stateManager.Get("SERVERNAME", TributesPortal.Utilities.StateManager.State.Session);

                EventResource objEventRes = new EventResource();
                Events objEventDetails = objEventRes.GetEventInfo(objEvent);

                //Added by amit
                objEventDetails.EventID = objEvent.EventID;
                objEventDetails.TributeId = objEvent.TributeId;
                objEventDetails.IsActive = true;
                objEventDetails.UserId = objEvent.UserId;
                objEventDetails.ModifiedBy = objEvent.UserId;
                objEventDetails.TributeType = objEvent.TributeType;
                objEventDetails.EventThemeID = objEvent.EventThemeID;
                objEventDetails.EventMessage = objEvent.EventMessage;
                objEventDetails.State = objEvent.State; 
               // objEventDetails.IsAskForMeal = objEventRes.IsAskForMeal;
                objEventRes.UpdateEvent(objEventDetails);
                

                string EmailSubject = objSessionValue.FirstName + " " + objSessionValue.LastName + " invited you to the event \"" + objEventDetails.EventName + "\"";
                for (int i = 0; i < objEvent.EventAwaiting.Count; i++)
                {
                    Events emailEvent = new Events();
                    emailEvent.EventID = objEvent.EventID;
                    emailEvent.EmailId = objEvent.EventAwaiting[i].UserName.ToString();
                    emailEvent.IsAskForMeal = objEventDetails.IsAskForMeal;
                    emailEvent.MealOptions = objEventDetails.MealOptions;
                    object identity = objEventRes.InviteGuest(emailEvent);
                    if (identity != null)
                    {
                        if (int.Parse(identity.ToString()) != 0)
                        {
                            //Insert the Hashcode for the Guest
                            string Hashcode = GetHashCode(int.Parse(identity.ToString()));
                            objEventRes.InsertHashCodeForGuest(int.Parse(identity.ToString()), Hashcode);
                            
                            EmailMessages objEmail = EmailMessages.Instance;
                           
                            EventTheme objTheme = GetEventThemeByID(objEventDetails.EventThemeID);

                            StringBuilder objEmailBody = new StringBuilder();
                            objEmailBody.Append("<html>");
                            objEmailBody.Append("<head>");
                            objEmailBody.Append("<title>Event Invitation Mail</title>");
                            objEmailBody.Append("</head>");
                            objEmailBody.Append("<body text='#000000' link='#000000'>");
                            objEmailBody.Append("<table width='700' border='0' align='center' cellpadding='0' cellspacing='0'>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><table width='100%' border='0' cellspacing='10' cellpadding='0'>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>Having trouble seeing this email? <a href='http://" + objEventDetails.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "/event.aspx?EventID=" + objEventDetails.EventID + "'> Visit our Event webpage.</a></font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("</table></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td><table width='520' height='650' border='0' align='center' cellpadding='0' cellspacing='10' bgcolor='" + objTheme.ThemeBackgroundColor + "'>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'><img src='" + GetImageURL(objTheme.ThemeFullSizeImage) + "' alt='Invitation Photo' /></font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>&nbsp;</font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><strong><font size='4' face='Verdana, Arial, Helvetica, sans-serif'>" + objEventDetails.EventName + " </font></strong></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>" + objEventDetails.EventMessage + " </font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'><strong>When:</strong></font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>" + DateTime.Parse(objEventDetails.EventDate.ToString()).ToString("MMMM dd, yyyy") + ", " + objEventDetails.EventStartTime + " - " + objEventDetails.EventEndTime + "</font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'><strong>Where:</strong></font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>" + objEventDetails.EventPlace + "</font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'><strong>Website:</strong></font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'><a href='http://" + objEventDetails.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "/'>http://" + objEventDetails.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "/</a></font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font size='2' face='Verdana, Arial, Helvetica, sans-serif'>&nbsp;</font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("</table>");
                            objEmailBody.Append("</td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td><table width='100%' border='0' cellspacing='10' cellpadding='0'>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><a href='http://" + objEventDetails.TributeType.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objEvent.TributeURL + "/event.aspx?EventID=" + objEventDetails.EventID + "&TributeID=" + objEventDetails.TributeId + "&Hashcode=" + Hashcode + "'><b><font size='4' face='Verdana, Arial, Helvetica, sans-serif'>Please visit our " + objEventDetails.TributeType + " Tribute to RSVP</font></b></a></td>");
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
                            objEmailBody.Append("<td align='center'><font color='#666666' size='1' face='Verdana, Arial, Helvetica, sans-serif'>Your Tribute respects your privacy. For any privacy concerns please <a href='"+ WebConfig.AppBaseDomain +"privacy.aspx'>click here.</a></font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font color='#666666' size='1' face='Verdana, Arial, Helvetica, sans-serif'>Your Tribute, Inc.</font></td>");
                            objEmailBody.Append("</tr>");
                            objEmailBody.Append("<tr>");
                            objEmailBody.Append("<td align='center'><font color='#666666' size='1' face='Verdana, Arial, Helvetica, sans-serif'>2875 North Lamb Blvd. Bldg 8, Las Vegas, NV 89115</font></td>");
                            objEmailBody.Append("</tr></table></td>");
                            objEmailBody.Append("</tr></table></body></html>");

                            bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", emailEvent.EmailId, EmailSubject, objEmailBody.ToString(), EmailMessages.TextFormat.Html.ToString());
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return count;
        }
        private string GetImageURL(string imageURL)
        {
            string retImageURL = string.Empty;
            return retImageURL = imageURL.Replace("../", WebConfig.AppBaseDomain);

        }
        private string GetHashCode(int Source)
        {
            byte[] tmpSource;
            byte[] tmpHash;

            //Create a byte array from source data
            tmpSource = ASCIIEncoding.ASCII.GetBytes(Source.ToString());

            //Compute hash based on source data
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            StringBuilder sHashcode = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length - 1; i++)
            {
                sHashcode.Append(tmpHash[i].ToString("X2"));
            }

            return sHashcode.ToString();
        }

        /// <summary>
        /// This method will call the Event resource method to get the all event invitation 
        /// categories for passed tributetype
        /// </summary>
        /// <param name="tributeType">TributeType</param>
        /// <returns>list of objects having EventInvitationCategory information</returns>
        public IList<EventInvitationCategory> EventInvitationCategories(string tributeType)
        {
            IList<EventInvitationCategory> objEventInvitationCategoryList = null;
            try
            {
                EventResource objEventRes = new EventResource();
                objEventInvitationCategoryList = objEventRes.EventInvitationCategories(tributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objEventInvitationCategoryList;
        }


        //Get CategoryList


        public IList<Themes> GetSubCategoryList(string strSubCategory)
        {
            IList<Themes> objThemesSubCategoryList = null;
            try
            {
                EventResource objEventRes = new EventResource();
                objThemesSubCategoryList = objEventRes.GetSubCategoryList(strSubCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objThemesSubCategoryList;
        }
        /// <summary>
        /// Get Themes for delete and  Update theme in Admin portal By Ashu
        /// </summary>
        /// <param name="strCategoryName"></param>
        /// <param name="strSubCategoryName"></param>
        public IList<Themes> GetThemesList(string strSubCategory, string strSubCategoryName, string applicationType)
        {
            IList<Themes> objThemesSubCategoryList = null;
            try
            {
                EventResource objEventRes = new EventResource();
                objThemesSubCategoryList = objEventRes.GetThemesList(strSubCategory, strSubCategoryName, applicationType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objThemesSubCategoryList;
        }
        /// <summary>
        /// Get Folder Name for Update and delete theme in Admin portal By Ashu
        /// </summary>
        /// <param name="themeid"></param>
        /// <returns></returns>
        public string  GetFoldername(int themeid)
        {
            string objFolderName = "";
            try
            {
                EventResource objEventRes = new EventResource();
                objFolderName = objEventRes.GetFoldername(themeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objFolderName;
        }
        /// <summary>
        /// To delete theme from database By Ashu
        /// </summary>
        /// <param name="themeId"></param>
         public void DeleteTheme(int themeId)
        {
            try
            {
                EventResource objEventRes = new EventResource();
                objEventRes.DeleteTheme(themeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }




         public IList<Themes> GetCategoryList(string applicationType)
        {
            IList<Themes> objEventThemesCategoryList = null;
            try
            {
                EventResource objEventRes = new EventResource();
                objEventThemesCategoryList = objEventRes.GetCategoryList(applicationType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objEventThemesCategoryList;
        }

        /// <summary>
        /// This method will call the Event resource method to get the event theme information
        /// for particular event invitation category and tributetype
        /// </summary>
        /// <param name="eventInvitationCategoryID">id of event invitation category</param>
        /// <param name="tributeType">tributetype</param>
        /// <returns>list of object having eventtheme information</returns>
        public IList<EventTheme> EventThemeInfo(int eventInvitationCategoryID, string tributeType)
        {
            IList<EventTheme> objEventThemeList = null;
            try
            {
                EventResource objEventRes = new EventResource();
                objEventThemeList = objEventRes.EventThemeInfo(eventInvitationCategoryID, tributeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objEventThemeList;
        }

        /// <summary>
        /// This method will call the Event resource method to get the event theme for particulat theme
        /// </summary>
        /// <param name="eventThemeID">id of the theme</param>
        /// <returns></returns>
        public EventTheme GetEventThemeByID(int eventThemeID)
        {
            EventTheme objEventTheme = null;
            try
            {
                EventResource objEventRes = new EventResource();
                objEventTheme = objEventRes.GetEventThemeByID(eventThemeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objEventTheme;
        }

        /// <summary>
        /// This method will call the event resource save event invitation category method
        /// </summary>
        /// <param name="objEventInvitationCategory"></param>
        /// <returns></returns>
        public object SaveInvitationCategory(EventInvitationCategory objEventInvitationCategory)
        {
            object identity = null;
            try
            {
                EventResource objEventRes = new EventResource();
                identity = objEventRes.SaveInvitationCategory(objEventInvitationCategory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return identity;
        }

        /// <summary>
        /// This method will call the event resource save event theme method 
        /// </summary>
        /// <param name="objEventTheme"></param>
        /// <returns></returns>
        public object SaveEventTheme(EventTheme objEventTheme)
        {
            object identity = null;
            try
            {
                EventResource objEventRes = new EventResource();
                identity = objEventRes.SaveEventTheme(objEventTheme);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return identity;
        }
        
             /// <summary>
        /// This method will call the event resource save category based theme method 
        /// </summary>
        /// <param name="objEventTheme"></param>
        /// <returns></returns>
        public object SaveCategoryBasedTheme(Themes objThemes)
        {
            object identity = null;
            try
            {
                EventResource objEventRes = new EventResource();
                identity = objEventRes.SaveCategoryBasedTheme(objThemes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return identity;
        }
        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method to send email to the list of users
        /// </summary>
        /// <param name="TribuetId">Tribute ID to get the list of admin</param>
        /// <param name="strSubject">Subject of the mail</param>
        public void SendEmail(int TribuetId, string strSubject, string strBody)
        {
            StoryResource objStoryRes = new StoryResource();

            UserInfo objUser = objStoryRes.GetTributeAdministrators(TribuetId, "Event");
            //Function to send the mail to the list of users who have added the Tribute in their list of favourites 
            UserInfo objUserFav = objStoryRes.GetFavouriteTributeUsers(TribuetId, "Event");

            EmailMessages objEmail = EmailMessages.Instance;

            if (objUser.UserEmail != "")
            {
                bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", objUser.UserEmail, strSubject, CreateBody(strBody), EmailMessages.TextFormat.Html.ToString());
            }

            //favourite mail
            //As per discussion with Rupendra: will send the mail in "To" field. 
            //ie a comma separated list of users in the "to" field
            if (objUserFav.UserEmail != "")
            {
                bool val = objEmail.SendMessages("Your Tribute<" + WebConfig.NoreplyEmail + ">", objUserFav.UserEmail, strSubject, CreateBody(strBody), EmailMessages.TextFormat.Html.ToString());
            }
        }

        /// <summary>
        /// This method is used to create the Body of the mail
        /// </summary>
        /// <param name="strSubject">Body of the Mail</param>
        /// <returns>This method will return the body of the mail</returns>
        private string CreateBody(string strBody)
        {
            StringBuilder sbBody = new StringBuilder();
            //sbBody.Append("<br/>");
            sbBody.Append(strBody);
            sbBody.Append("<br/>");

            return sbBody.ToString();
        }

        #endregion

        #endregion


        public int GetUserIdByTributeId(int tributeId)
        {
            int userId;
            try
            {
                EventResource objEventRes = new EventResource();
                userId = objEventRes.GetUserIdByTributeId(tributeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userId;
        }
    }//end class
}//end namespace
