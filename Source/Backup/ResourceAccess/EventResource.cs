///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.ResourceAccess.EventResource.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the database methods associated with Events
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Collections.ObjectModel;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;
using System.Xml;

#endregion

/// <summary>
///Tribute Portal-Events Resource Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Resource Access class for the Events which is responsible for following opertion in the database
// 1) Add Event
// 2) View Event
// 3) Edit Event
// 4) Delete Event
// 5) Invite Guests List
/// </summary>

namespace TributesPortal.ResourceAccess 
{
    public class EventResource : PortalResourceAccess 
    {
        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This method will get the Image List, Event Type, Country List, and Event Detail from the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Tribute and Event information</param>
        /// <returns>This method will return the Events object which contain the Event information</returns>
        public Events GetEventInfo(Events objEvent)
        {
            Events objEventDetail = null;

            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objEventParam = { objEvent.TributeType, 0, objEvent.EventID, objEvent.UserId, objEvent.TributeId };

                if (objEventParam != null)
                {
                    DataSet dsEvents = new DataSet();
                    dsEvents = GetDataSet("usp_GetEventInfo", objEventParam);

                    // Populate the event detail in event object from the Dataset
                    objEventDetail = PopulateEventObject(dsEvents);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEventDetail;
        }

        /// <summary>
        /// This method will Add the event in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Event information</param>
        public object SaveEvent(Events objEvent)
        {
            object Identity = new object();

            try
            {
                if (objEvent != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { Events.EventsEnum.UserId.ToString(),  
                                          Events.EventsEnum.TributeId.ToString(),
                                          Events.EventsEnum.EventName.ToString(),
                                          Events.EventsEnum.EventDesc.ToString(),
                                          Events.EventsEnum.EventTypeName.ToString(),
                                          Events.EventsEnum.EventDate.ToString(),
                                          Events.EventsEnum.EventStartTime.ToString(),
                                          Events.EventsEnum.EventEndTime.ToString(),
                                          Events.EventsEnum.EventImage.ToString(),
                                          Events.EventsEnum.Location.ToString(),
                                          Events.EventsEnum.Address.ToString(),
                                          Events.EventsEnum.City.ToString(),
                                          Events.EventsEnum.State.ToString(),
                                          Events.EventsEnum.Country.ToString(),
                                          Events.EventsEnum.HostName.ToString(),
                                          Events.EventsEnum.PhoneNumber.ToString(),
                                          Events.EventsEnum.EmailId.ToString(),
                                          Events.EventsEnum.IsPrivate.ToString(),
                                          Events.EventsEnum.CreatedBy.ToString(),
                                          Events.EventsEnum.CreatedDate.ToString(),
                                          Events.EventsEnum.AllowAdditionalPeople.ToString(),
                                          Events.EventsEnum.SendEmailReminder.ToString(),
                                          Events.EventsEnum.ShowRsvpStatistics.ToString(),
                                          Events.EventsEnum.MealOptions.ToString(),
                                          Events.EventsEnum.EventMessage.ToString(),
                                          Events.EventsEnum.EventThemeID.ToString(),
                                          Events .EventsEnum .IsAskForMeal.ToString(),
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.Int64,
                                          DbType.Int64,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.DateTime,
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.Int64,
                                          DbType.Int64,
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.Boolean,
                                          DbType.Int64,
                                          DbType.DateTime,
                                          DbType.Boolean,
                                          DbType.Boolean,
                                          DbType.Boolean,
                                          DbType.String,
                                          DbType.String,
                                          DbType.Int64,
                                          DbType .Boolean,
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objEvent.UserId,
                                          objEvent.TributeId,
                                          objEvent.EventName.ToString(),
                                          objEvent.EventDesc.ToString(),
                                          objEvent.EventTypeName,
                                          objEvent.EventDate,
                                          objEvent.EventStartTime.ToString(),
                                          objEvent.EventEndTime.ToString(),
                                          objEvent.EventImage.ToString(),
                                          objEvent.Location.ToString(),
                                          objEvent.Address.ToString(),
                                          objEvent.City.ToString(),
                                          objEvent.State,
                                          objEvent.Country,
                                          objEvent.HostName.ToString(),
                                          objEvent.PhoneNumber.ToString(),
                                          objEvent.EmailId.ToString(),
                                          objEvent.IsPrivate,
                                          objEvent.CreatedBy,
                                          DateTime.Now,
                                          objEvent.AllowAdditionalPeople,
                                          objEvent.SendEmailReminder,
                                          objEvent.ShowRsvpStatistics,
                                          objEvent.MealOptions,
                                          objEvent.EventMessage,
                                          objEvent.EventThemeID,
                                          objEvent.IsAskForMeal,
                                        };

                    // call stored procedure to Insert teh event
                    Identity = InsertDataAndReturnId("usp_InsertEvent", strParam, enumType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Identity;
        }

        /// <summary>
        /// This method will Update the event in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Event information</param>
        public object UpdateEvent(Events objEvent)
        {
            object Identity = new object();

            try
            {
                if (objEvent != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { Events.EventsEnum.EventID.ToString(),  
                                          Events.EventsEnum.TributeId.ToString(),
                                          Events.EventsEnum.EventName.ToString(),
                                          Events.EventsEnum.EventDesc.ToString(),
                                          Events.EventsEnum.EventTypeName.ToString(),
                                          Events.EventsEnum.EventDate.ToString(),
                                          Events.EventsEnum.EventStartTime.ToString(),
                                          Events.EventsEnum.EventEndTime.ToString(),
                                          Events.EventsEnum.EventImage.ToString(),
                                          Events.EventsEnum.Location.ToString(),
                                          Events.EventsEnum.Address.ToString(),
                                          Events.EventsEnum.City.ToString(),
                                          Events.EventsEnum.State.ToString(),
                                          Events.EventsEnum.Country.ToString(),
                                          Events.EventsEnum.HostName.ToString(),
                                          Events.EventsEnum.PhoneNumber.ToString(),
                                          Events.EventsEnum.EmailId.ToString(),
                                          Events.EventsEnum.IsPrivate.ToString(),
                                          Events.EventsEnum.ModifiedBy.ToString(),
                                          Events.EventsEnum.ModifiedDate.ToString(),
                                          Events.EventsEnum.AllowAdditionalPeople.ToString(),
                                          Events.EventsEnum.SendEmailReminder.ToString(),
                                          Events.EventsEnum.ShowRsvpStatistics.ToString(),
                                          Events.EventsEnum.MealOptions.ToString(),
                                          Events.EventsEnum.EventMessage.ToString(),
                                          Events.EventsEnum.EventThemeID.ToString(),
                                          Events .EventsEnum .IsAskForMeal.ToString(),
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.Int64,
                                          DbType.Int64,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.DateTime,
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.Int64,
                                          DbType.Int64,
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.String, 
                                          DbType.Boolean,
                                          DbType.Int64,
                                          DbType.DateTime,
                                          DbType.Boolean,
                                          DbType.Boolean,
                                          DbType.Boolean,
                                          DbType.String,
                                          DbType.String,
                                          DbType.Int64,
                                          DbType .Boolean,
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objEvent.EventID,
                                          objEvent.TributeId,
                                          objEvent.EventName.ToString(),
                                          objEvent.EventDesc.ToString(),
                                          objEvent.EventTypeName,
                                          objEvent.EventDate,
                                          objEvent.EventStartTime.ToString(),
                                          objEvent.EventEndTime.ToString(),
                                          objEvent.EventImage.ToString(),
                                          objEvent.Location.ToString(),
                                          objEvent.Address.ToString(),
                                          objEvent.City.ToString(),
                                          objEvent.State,
                                          objEvent.Country,
                                          objEvent.HostName.ToString(),
                                          objEvent.PhoneNumber.ToString(),
                                          objEvent.EmailId.ToString(),
                                          objEvent.IsPrivate,
                                          objEvent.ModifiedBy,
                                          DateTime.Now,
                                          objEvent.AllowAdditionalPeople,
                                          objEvent.SendEmailReminder,
                                          objEvent.ShowRsvpStatistics,
                                          objEvent.MealOptions,
                                          objEvent.EventMessage,
                                          objEvent.EventThemeID,
                                          objEvent.IsAskForMeal,
                                        };

                    // call stored procedure to update the event
                    Identity = InsertDataAndReturnId("usp_UpdateEvent", strParam, enumType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Identity;
        }

        /// <summary>
        /// This method will get the event list from the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Tribute Id for which wants to get the event list</param>
        /// <returns>This method will return the list of Events object which contain the Event list</returns>
        public IList<Events> GetEventList(Events objEvent)
        {
            List<Events> lstEvent = new List<Events>();

            try
            {
                if (objEvent != null)
                {
                    // Create object to passed the paramter for the stored procedure
                    object[] objEventParam = { objEvent.TributeId, objEvent.UserId };

                    DataSet dsEvents = new DataSet();
                    dsEvents = GetDataSet("usp_GetEventList", objEventParam);

                    // Populate the event list object from the Dataset
                    lstEvent = PopulateEventListObject(dsEvents, objEvent);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstEvent;
        }

        /// <summary>
        /// This method will get the event detail in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain Event Id for which wants to get the detail of the event</param>
        /// <returns>This method will return the Events object which contain the Event information</returns>
        public Events GetFullEvent(Events objEvent)
        {
            Events tmpEvent = new Events();

            try
            {
                if (objEvent != null)
                {
                    // Create object to passed the paramter for the stored procedure
                    object[] objEventParam = { objEvent.EventID, objEvent.UserId, objEvent.TributeId };

                    DataSet dsEvents = new DataSet();
                    dsEvents = GetDataSet("usp_GetFullEvent", objEventParam);


                    // Get the User role for the tribute
                    if (dsEvents.Tables.Count > 0)
                    {
                        if (dsEvents.Tables[0].Rows.Count > 0)
                        {
                            objEvent.IsAdmin = GetUserRole(dsEvents.Tables[0]);
                        }
                    }

                    // Get the Tribute detail
                    if (dsEvents.Tables.Count > 1)
                    {
                        if (dsEvents.Tables[1].Rows.Count > 0)
                        {
                            DataRow drTribute = dsEvents.Tables[1].Rows[0];

                            objEvent.TributeName = drTribute["TributeName"].ToString();
                            objEvent.TributeType = drTribute["TypeDescription"].ToString();
                            objEvent.TributeURL = drTribute["TributeUrl"].ToString();
                        }
                    }

                    // Get the Event Detail
                    if (dsEvents.Tables.Count > 2)
                    {
                        if (dsEvents.Tables[2].Rows.Count > 0)
                        {
                            DataRow drEvents = dsEvents.Tables[2].Rows[0];

                            tmpEvent.EventName = drEvents["EventName"].ToString();
                            tmpEvent.EventTypeName = drEvents["EventTypeName"].ToString();
                            tmpEvent.EventDate = DateTime.Parse(drEvents["EventDate"].ToString());
                            tmpEvent.EventImage = drEvents["EventImage"].ToString();
                            tmpEvent.EventDesc = drEvents["EventDesc"].ToString();
                            tmpEvent.Location = drEvents["Location"].ToString();
                            tmpEvent.Address = drEvents["Address"].ToString();
                            tmpEvent.City = drEvents["City"].ToString();
                            tmpEvent.State = drEvents["StateName"].ToString();
                            tmpEvent.Country = drEvents["CountryName"].ToString();
                            tmpEvent.EventStartTime = drEvents["EventStartTime"].ToString() + " - " + drEvents["EventEndTime"].ToString();
                            tmpEvent.PhoneNumber = drEvents["PhoneNumber"].ToString();
                            tmpEvent.EmailId = drEvents["EmailId"].ToString();
                            tmpEvent.HostName = drEvents["HostName"].ToString();
                            tmpEvent.IsPrivate = bool.Parse(drEvents["IsPrivate"].ToString());
                            tmpEvent.UserName = GetUserName(drEvents);
                            tmpEvent.EventRsvp = drEvents["RSVPName"].ToString();
                            tmpEvent.UserId = int.Parse(drEvents["UserId"].ToString());
                            tmpEvent.IsActive = true;
                            tmpEvent.MealOptions = drEvents["MealOptions"].ToString();
                            tmpEvent.AllowAdditionalPeople = bool.Parse(drEvents["AllowAdditionalPeople"].ToString());
                            tmpEvent.ShowRsvpStatistics = bool.Parse(drEvents["ShowRsvpStatistics"].ToString());
                            tmpEvent.SendEmailReminder = bool.Parse(drEvents["SendEmailReminder"].ToString());
                            tmpEvent.IsAskForMeal = bool.Parse(drEvents["IsAskForMeal"].ToString());
                        }
                        else
                        {
                            tmpEvent.IsActive = false;
                        }
                    }

                    // Get the Event Guest List
                    if (dsEvents.Tables.Count > 3)
                    {
                        if (dsEvents.Tables[3].Rows.Count > 0)
                        {
                            IList<GuestList> Awaiting = new List<GuestList>();
                            IList<GuestList> Attending = new List<GuestList>();
                            IList<GuestList> MaybeAttending = new List<GuestList>();
                            IList<GuestList> NotAttending = new List<GuestList>();

                            foreach (DataRow drGuestList in dsEvents.Tables[3].Rows)
                            {
                                GuestList gtList = new GuestList();

                                if ((drGuestList["UserId"] != null) && (drGuestList["UserId"].ToString() != ""))
                                {
                                    gtList.UserId = int.Parse(drGuestList["UserId"].ToString());
                                    gtList.UserName = GetUserName(drGuestList) + ",";
                                }
                                else
                                {
                                    gtList.UserName = drGuestList["Email"].ToString() + ",";
                                }
                                
                                if (drGuestList["RSVPName"].ToString() == "Attending")
                                {
                                    Attending.Add(gtList);
                                }
                                else if (drGuestList["RSVPName"].ToString() == "Maybe Attending")
                                {
                                    MaybeAttending.Add(gtList);
                                }
                                else if (drGuestList["RSVPName"].ToString() == "Not Attending")
                                {
                                    NotAttending.Add(gtList);
                                }
                                else if (drGuestList["RSVPName"].ToString() == "Awaiting Response")
                                {
                                    Awaiting.Add(gtList);
                                }

                                gtList = null;
                            }

                            RemoveLastChar(MaybeAttending);
                            RemoveLastChar(Awaiting);
                            RemoveLastChar(Attending);
                            RemoveLastChar(NotAttending);

                            tmpEvent.EventMaybeAttending = MaybeAttending;
                            tmpEvent.EventAwaiting = Awaiting;
                            tmpEvent.EventAttending = Attending;
                            tmpEvent.EventNotAttending = NotAttending;
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmpEvent;
        }

        /// <summary>
        /// This method will Delete the event in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain event id which wants to delete</param>
        public void DeleteEvent(Events objEvent)
        {
            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objEventParam = { objEvent.EventID, objEvent.UserId };

                if (objEventParam != null)
                {
                    DataSet dsEvents = new DataSet();
                    Delete("usp_DeleteEvent", objEventParam);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void SaveRsvp(IList<CompleteGuestList> objGuestList, int EventId, int counter, IList<CompleteGuestList> lstCompleteGuestList, int countRSVP)
        //{
        //    try
        //    {
        //        int j = 1;
        //        objGuestList[0].GuestId = lstCompleteGuestList[0].GuestId;
        //        UpdateRsvp(objGuestList[0]);
        //        for (int i = countRSVP; i < lstCompleteGuestList.Count; i++)
        //        {
        //            objGuestList[j].GuestId = lstCompleteGuestList[i].GuestId;
        //            UpdateRsvp(objGuestList[j]);
        //            j++;
        //        }
        //        for (int count = counter; count < objGuestList.Count;count ++ )
        //        {
        //            //if (GuestInList.GuestId == 0)
        //            //{
        //                AddRsvp(objGuestList[count], EventId, 0);
        //            //}
        //            //else
        //            //{
        //            //    UpdateRsvp(GuestInList);
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        public IList<CompleteGuestList> SaveRsvp(IList<CompleteGuestList> objGuestList, int EventId)
        {
            IList<CompleteGuestList> objNewGuestList = new List<CompleteGuestList>(); ;
            try
            {
                foreach(CompleteGuestList obj in objGuestList)
                {
                    if (obj.GuestId == 0)
                    {
                        int RSVPGuestId = AddRsvp(obj, EventId, 0);
                        obj.GuestId = RSVPGuestId;
                    }
                    else
                        UpdateRsvp(obj);

                    objNewGuestList.Add(obj);
                  
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objNewGuestList;
        }

        

        /// <summary>
        /// This method will add an RSVP for a Guest of the event in the database
        /// </summary>
        /// <param name="objGuestToAdd">An CompleteGuestList Object which contain Guset and it's RSVP status</param>
        /// <param name="objEvent">An Events Object which contain event details</param>
        public int AddRsvp(CompleteGuestList objGuestToAdd, int EventId, int UserId)
        {
            int RSVPGuestId = 0;
            try
            {
                if (objGuestToAdd != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { Events.EventsEnum.EventID.ToString(),  
                                          Events.EventsEnum.UserId.ToString(),
                                          CompleteGuestList.GuestListEnum.FirstName.ToString(),
                                          CompleteGuestList.GuestListEnum.LastName.ToString(),
                                          CompleteGuestList.GuestListEnum.PhoneNumber.ToString(),
                                          CompleteGuestList.GuestListEnum.Email.ToString(),
                                          CompleteGuestList.GuestListEnum.MealOption.ToString(),
                                          CompleteGuestList.GuestListEnum.RsvpStatus.ToString(),
                                          CompleteGuestList.GuestListEnum.Comment.ToString(),
                                          CompleteGuestList.GuestListEnum.GuestId.ToString()
                                        };

                    // sets the types of parameters
                    DbType[] enumType = { DbType.Int64,
                                          DbType.Int64,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.Int64
                                        };

                    // sets the value of the paramter
                    object[] objValue = { EventId,
                                          UserId,
                                          objGuestToAdd.FirstName,
                                          objGuestToAdd.LastName,
                                          objGuestToAdd.PhoneNumber,
                                          objGuestToAdd.Email,
                                          objGuestToAdd.MealOption,
                                          objGuestToAdd.RsvpStatus,
                                          objGuestToAdd.Comment,
                                          objGuestToAdd.GuestId
                                        };

                    // call stored procedure to RSVP an event
                    RSVPGuestId = Convert .ToInt32(InsertDataAndReturnId("usp_AddRsvp", strParam, enumType, objValue));
                }
               
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RSVPGuestId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objGuestToAdd"></param>
        public object UpdateRsvp(CompleteGuestList objGuestToUpdate)
        {
            object Identity = new object();
            try
            {
                if (objGuestToUpdate != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { CompleteGuestList.GuestListEnum.FirstName.ToString(),
                                          CompleteGuestList.GuestListEnum.LastName.ToString(),
                                          CompleteGuestList.GuestListEnum.PhoneNumber.ToString(),
                                          CompleteGuestList.GuestListEnum.Email.ToString(),
                                          CompleteGuestList.GuestListEnum.MealOption.ToString(),
                                          CompleteGuestList.GuestListEnum.RsvpStatus.ToString(),
                                          CompleteGuestList.GuestListEnum.Comment.ToString(),
                                          CompleteGuestList.GuestListEnum.GuestId.ToString(),
                                          CompleteGuestList.GuestListEnum.AdditionalGuestId.ToString()
                                        };

                    // sets the types of parameters
                    DbType[] enumType = { DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.Int64,
                                          DbType.Int64
                                        };

                    // sets the value of the paramter
                    object[] objValue = { objGuestToUpdate.FirstName,
                                          objGuestToUpdate.LastName,
                                          objGuestToUpdate.PhoneNumber,
                                          objGuestToUpdate.Email,
                                          objGuestToUpdate.MealOption,
                                          objGuestToUpdate.RsvpStatus,
                                          objGuestToUpdate.Comment,
                                          objGuestToUpdate.GuestId,
                                          objGuestToUpdate.AdditionalGuestId
                                        };

                    // call stored procedure to RSVP an event
                    Identity = InsertDataAndReturnId("usp_UpdateRsvp", strParam, enumType, objValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Identity;
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
                // sets the value of the paramter
                object[] objValue = { guestId,
                                      additionalGuestId
                                    };

                // call stored procedure to delete Rsvp
                Delete("usp_DeleteRsvp", objValue);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will insert the invited guest list for the event in the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain event id and Guest List</param>
        public object InviteGuest(Events objEvent)
        {
            object Identity = new object();

            try
            {
                if (objEvent != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { Events.EventsEnum.EventID.ToString(),  
                                          Events.EventsEnum.EmailId.ToString(),                                       
                                         
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.Int64,
                                          DbType.String,
                                        
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objEvent.EventID,
                                          objEvent.EmailId,
                                        };

                    // call stored procedure to Insert the Guest List
                    Identity = InsertDataAndReturnId("usp_InsertEventGuestList", strParam, enumType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Identity;
        }

        public object SaveRsvpForCreator(Events objEvent, SessionValue objSessionValue)
        {
            object Identity = new object();

            try
            {
                if (objEvent != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { Events.EventsEnum.EventID.ToString(),  
                                          Events.EventsEnum.EmailId.ToString(),
                                          Events.EventsEnum.UserId.ToString(),
                                          Events.EventsEnum.FirstName.ToString(),
                                          Events.EventsEnum.LastName.ToString(),
                                          Events.EventsEnum.PhoneNumber.ToString()                                          
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.Int64,
                                          DbType.String,
                                          DbType.Int64,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objEvent.EventID,
                                          objSessionValue.UserEmail,
                                          objEvent.UserId,
                                          objEvent.FirstName,
                                          objEvent.LastName,
                                          objEvent.PhoneNumber                                          
                                        };

                    // call stored procedure to Insert the Guest List
                    Identity = InsertDataAndReturnId("usp_InsertCreatorEventGuestList", strParam, enumType, objValue);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Identity;
        }


        public void InsertHashCodeForGuest(int GuestId, string Hashcode)
        {
            object[] objParam = { GuestId, Hashcode };
            
            GetDataSet("usp_InsertHashCodeForGuest", objParam);
        }

        /// <summary>
        /// This method will get the Guest List for the event from the database
        /// </summary>
        /// <param name="objEvent">An Event Object which contain event id</param>
        /// <returns>returns a event object which contain the guest list</returns>
        public Events GetEventGuestList(Events objEvent)
        {
            Events tmpEvent = new Events();

            try
            {
                if (objEvent != null)
                {
                    DataSet dsGuest = new DataSet();

                    object[] objGuestParam = { objEvent.EventID };

                    dsGuest = GetDataSet("usp_GetEventGuestList", objGuestParam);

                    // Get the Event Guest list
                    if (dsGuest.Tables.Count > 0)
                    {
                        if (dsGuest.Tables[0].Rows.Count > 0)
                        {
                            IList<GuestList> lstGuest = new List<GuestList>();

                            foreach (DataRow drGuestList in dsGuest.Tables[0].Rows)
                            {
                                GuestList guest = new GuestList();
                                guest.UserName = drGuestList["Email"].ToString();
                                lstGuest.Add(guest);
                                guest = null;
                            }

                            tmpEvent.EventAwaiting = lstGuest;
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                    objEvent.CustomError = objError;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmpEvent;
        }


        //Deepak Code For meal Option
        public CompleteGuestList GetMealOptionList(int GuestId)
        {
            CompleteGuestList objCompleteGuestList = new CompleteGuestList();
            DataSet _objDataSet=null;

            if (GuestId !=0)
            {
                object[] objParam = { GuestId };
                _objDataSet = GetDataSet("usp_GetMealOptionsList", objParam);
            }
            if (_objDataSet.Tables[0].Rows.Count > 0)
            {
                objCompleteGuestList.MealOption = _objDataSet.Tables[0].Rows[0]["MealOptions"].ToString();
                objCompleteGuestList.IsAskForMeal =bool .Parse(_objDataSet.Tables[0].Rows[0]["IsAskForMeal"].ToString());
            }
            return objCompleteGuestList;
        }
        
         
        /// <summary>
        /// Deepak Code For Getting Email IDs for a particular Event
        /// </summary>
        /// <param name="GuestId"></param>
        /// <returns></returns>
        public List<CompleteGuestList> GetEmailIdsForEvent(int GuestId)
        {
            List<CompleteGuestList> lstCompleteGuestMails = new List<CompleteGuestList>();
            DataSet _objDataSet = null;

            if (GuestId != 0)
            {
                object[] objParam = { GuestId };
                _objDataSet = GetDataSet("usp_GetEmailsForEvent", objParam);

                if (_objDataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                    {
                        CompleteGuestList objCompleteGuestMail = new CompleteGuestList();
                        objCompleteGuestMail.Email = dr["Email"].ToString();
                        objCompleteGuestMail.GuestId = Convert.ToInt32(dr["GuestId"].ToString());
                        lstCompleteGuestMails.Add(objCompleteGuestMail);
                    }
                }
            }


            return lstCompleteGuestMails;
        }



        
        public List<CompleteGuestList> GetCompleteGuestList(int EventId, string Hashcode, bool isCreator)
        {
            List<CompleteGuestList> lstCompleteGuestList = new List<CompleteGuestList>();
            DataSet _objDataSet;

            if (isCreator)
            {
                if (Hashcode == null)
                {
                    object[] objParam = { EventId };
                    _objDataSet = GetDataSet("usp_GetCreatorGuestList", objParam);
                }
                else
                {
                    object[] objParam = { EventId, Hashcode };
                    _objDataSet = GetDataSet("usp_GetGuestListForHashCode", objParam);
                }
            }
            else
            {

                if (Hashcode == null)
                {
                    object[] objParam = { EventId };
                    _objDataSet = GetDataSet("usp_GetCompleteGuestList", objParam);
                }
                else
                {
                    object[] objParam = { EventId, Hashcode };
                    _objDataSet = GetDataSet("usp_GetGuestListForHashCode", objParam);
                }
            }

            if (_objDataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in _objDataSet.Tables[0].Rows)
                {
                    CompleteGuestList objCompleteGuestList = new CompleteGuestList();

                    objCompleteGuestList.RsvpDate = dr["RsvpDate"].ToString();
                    objCompleteGuestList.FirstName = dr["FirstName"].ToString();
                    objCompleteGuestList.LastName = dr["LastName"].ToString();
                    objCompleteGuestList.PhoneNumber = dr["PhoneNumber"].ToString();
                    objCompleteGuestList.Email = dr["Email"].ToString();
                    objCompleteGuestList.MealOption = dr["MealOption"].ToString();
                    objCompleteGuestList.RsvpStatus = dr["RsvpStatus"].ToString();
                    objCompleteGuestList.Comment = dr["Comment"].ToString();
                    objCompleteGuestList.GuestId = (dr["GuestId"].ToString() != string.Empty) ? int.Parse(dr["GuestId"].ToString()) : 0;
                    objCompleteGuestList.AdditionalGuestId = (dr["AdditionalGuestId"].ToString() != string.Empty) ? int.Parse(dr["AdditionalGuestId"].ToString()) : 0;

                    lstCompleteGuestList.Add(objCompleteGuestList);
                    objCompleteGuestList = null;
                }
            }
            return lstCompleteGuestList;
        }

        /// <summary>
        /// This methods will get the list of event  categories from the database for particular tributetype
        /// </summary>
        /// <param name="tributeType"></param>
        /// <returns></returns>
        public IList<EventInvitationCategory> EventInvitationCategories(string tributeType)
        {
            IList<EventInvitationCategory> objEventInvitationCategoryList = null;

            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objEventParam = { tributeType };

                if (objEventParam != null)
                {
                    DataSet dsInvitationCategory = new DataSet();
                    dsInvitationCategory = GetDataSet("usp_GetEventInvitationCategory", objEventParam);

                    // Populate the event detail in event object from the Dataset
                    objEventInvitationCategoryList = PopulateEventInvitationCategoryList(dsInvitationCategory);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEventInvitationCategoryList;
        }
        //get sub Category


        /// <summary>GetSubCategoryList
        /// This methods will get the list of event  categories from the database for particular tributetype
        /// </summary>
        /// <param name="tributeType"></param>
        /// <returns></returns>
        public IList<Themes> GetSubCategoryList(string strCategory)
        {
            IList<Themes> objThemeSubCategoryList = null;

            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objThemesParam = { strCategory };

                if (objThemesParam != null)
                {
                    DataSet dsCategoryList = new DataSet();
                    dsCategoryList = GetDataSet("usp_GetThemesSubCategory", objThemesParam);

                    // Populate the event detail in event object from the Dataset
                    objThemeSubCategoryList = PopulateThemeSubCategoryList(dsCategoryList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objThemeSubCategoryList;
        }
        /// <summary>
        /// Get Themes from database for delete and  Update theme in Admin portal By Ashu
        /// </summary>
        /// <param name="strCategoryName"></param>
        /// <param name="strSubCategoryName"></param>
        public IList<Themes> GetThemesList(string strCategory, string strSubCategory,string applicationType)
        {
            IList<Themes> objThemeNameList = null;

            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objThemesParam = { strCategory, strSubCategory, applicationType };

                if (objThemesParam != null)
                {
                    DataSet dsThemeList = new DataSet();
                    dsThemeList = GetDataSet("usp_GetThemesName", objThemesParam);

                    // Populate the event detail in event object from the Dataset
                    objThemeNameList = PopulateThemeList(dsThemeList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objThemeNameList;
        }
        /// <summary>
        /// Get Folder Name from database for Update and delete theme in Admin portal By Ashu
        /// </summary>
        /// <param name="themeid"></param>
        /// <returns></returns>
        public string  GetFoldername(int themeid)
        {
            string objFolderName = "";
            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objFolderParam = { themeid };

                if (objFolderName != null)
                {
                    DataSet dsFolderName = new DataSet();
                    dsFolderName = GetDataSet("usp_GetFolderName", objFolderParam);

                    // Populate the event detail in event object from the Dataset
                    objFolderName = PopulateFolderName(dsFolderName);
                }
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
        public void DeleteTheme(int themeid)
        {
            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objThemeParam = {themeid };

                if (objThemeParam != null)
                {
                    DeleteTheme("usp_DeleteThemeCategory", objThemeParam);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number >= 50000)
                {
                    Errors objError = new Errors();
                    objError.ErrorMessage = sqlEx.Message;
                }
                else
                {
                    throw sqlEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        

        /// <summary>
        /// This methods will get the list of event  categories from the database for particular tributetype
        /// </summary>
        /// <param name="tributeType"></param>
        /// <returns></returns>
        public IList<Themes> GetCategoryList(string applicationType)
        {
            IList<Themes> objThemeCategoryList = null;
            try
            {
                // Create object to passed the paramter for the stored procedure
                int ActiveStatus = 1;
                object[] objThemeParam = { ActiveStatus, applicationType };

                if (objThemeParam != null)
                {
                    DataSet dsCategoryList = new DataSet();
                    dsCategoryList = GetDataSet("usp_GetThemesCategory", objThemeParam);                    

                    // Populate the event detail in event object from the Dataset
                    objThemeCategoryList = PopulateThemeCategoryList(dsCategoryList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objThemeCategoryList;
        }


        /// <summary>
        /// This method will get the list of event theme from the database for particular event invitation
        /// category and tributetype
        /// </summary>
        /// <param name="eventInvitationCategoryID"></param>
        /// <param name="tributeType"></param>
        /// <returns></returns>
        public IList<EventTheme> EventThemeInfo(int eventInvitationCategoryID, string tributeType)
        {
            IList<EventTheme> eventThemeList = null;

            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objEventParam = { eventInvitationCategoryID, tributeType };

                if (objEventParam != null)
                {
                    DataSet dsEventTheme = new DataSet();
                    dsEventTheme = GetDataSet("usp_GetEventThemeInfo", objEventParam);

                    // Populate the event detail in event object from the Dataset
                    eventThemeList = PopulateEventThemeList(dsEventTheme);
                }
            }            
            catch (Exception ex)
            {
                throw ex;
            }

            return eventThemeList;
        }

        /// <summary>
        /// This method will bet the event theme from the database for particular theme
        /// </summary>
        /// <param name="eventThemeID"></param>
        /// <returns></returns>
        public EventTheme GetEventThemeByID(int eventThemeID)
        {
            EventTheme objEventTheme = null;

            try
            {
                // Create object to passed the paramter for the stored procedure
                object[] objEventParam = { eventThemeID };

                if (objEventParam != null)
                {
                    DataSet dsEventTheme = new DataSet();
                    dsEventTheme = GetDataSet("usp_GetEventThemeByID", objEventParam);

                    // Populate the event detail in event object from the Dataset
                    objEventTheme = PopulateEventTheme(dsEventTheme);
                }
            }            
            catch (Exception ex)
            {
                throw ex;
            }

            return objEventTheme;
        }

        /// <summary>
        /// This method will save the invitation category in the database
        /// </summary>
        /// <param name="objEventInvitationCategorye"></param>
        /// <returns></returns>
        public object SaveInvitationCategory(EventInvitationCategory objEventInvitationCategory)
        {
            object Identity = new object();

            try
            {
                if (objEventInvitationCategory != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { EventInvitationCategory.EventInvitationCategoryEnum.InvitationCategoryName.ToString(),
                                          EventInvitationCategory.EventInvitationCategoryEnum.TributeType.ToString()
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.String,
                                          DbType.Int64
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objEventInvitationCategory.InvitationCategoryName,
                                          objEventInvitationCategory.TributeType
                                        };

                    // call stored procedure to Insert the event
                    Identity = InsertDataAndReturnId("usp_InsertEventInvitationCategory", strParam, enumType, objValue);
                }
            }            
            catch (Exception ex)
            {
                throw ex;
            }

            return Identity;
        }
        


              /// <summary>
        /// This method will save the event theme information in the database
        /// </summary>
        /// <param name="objEventTheme"></param>
        /// <returns></returns>
        public object SaveCategoryBasedTheme(Themes objThemes)
        {
            object Identity = new object();

            try
            {
                if (objThemes != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { Themes.ThemesEnum.ThemeName.ToString(),
                                          Themes.ThemesEnum.Tributetype.ToString(),
                                          Themes.ThemesEnum.ThemeValue.ToString(),
                                          Themes.ThemesEnum.IsActive.ToString(),
                                          Themes.ThemesEnum.SubCategory.ToString(),                                          
                                          Themes.ThemesEnum.FolderName.ToString(),
                                          Themes.ThemesEnum.ApplicationType.ToString()
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.Boolean,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objThemes.ThemeName ,
                                          objThemes.Tributetype,
                                          objThemes.ThemeValue,
                                          objThemes.IsActive, 
                                          objThemes.SubCategory,
                                          objThemes.FolderName,
                                          objThemes.ApplicationType
                                        };

                    // call stored procedure to Insert the event
                    Identity = InsertDataAndReturnId("usp_CreateThemesOnCategory", strParam, enumType, objValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Identity;
        }



        /// <summary>
        /// This method will save the event theme information in the database
        /// </summary>
        /// <param name="objEventTheme"></param>
        /// <returns></returns>
        public object SaveEventTheme(EventTheme objEventTheme)
        {
            object Identity = new object();

            try
            {
                if (objEventTheme != null)
                {
                    // sets the name of the parameters
                    string[] strParam = { EventTheme.EventThemeEnum.EventThemeName.ToString(),
                                          EventTheme.EventThemeEnum.InvitationCategoryID.ToString(),
                                          EventTheme.EventThemeEnum.ThemeThumbnailImage.ToString(),
                                          EventTheme.EventThemeEnum.ThemePreviewImage.ToString(),
                                          EventTheme.EventThemeEnum.ThemeFullSizeImage.ToString(),
                                          EventTheme.EventThemeEnum.ThemeBackgroundColor.ToString()
                                        };


                    // sets the types of parameters
                    DbType[] enumType = { DbType.String,
                                          DbType.Int64,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String,
                                          DbType.String
                                        };


                    // sets the value of the paramter
                    object[] objValue = { objEventTheme.EventThemeName,
                                          objEventTheme.InvitationCategoryID,
                                          objEventTheme.ThemeThumbnailImage,
                                          objEventTheme.ThemePreviewImage,
                                          objEventTheme.ThemeFullSizeImage,
                                          objEventTheme.ThemeBackgroundColor
                                        };

                    // call stored procedure to Insert the event
                    Identity = InsertDataAndReturnId("usp_InsertEventTheme", strParam, enumType, objValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Identity;
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// This method will populate the Image List, Event Type, Country List, and Event Detail object from the dataset
        /// </summary>
        /// <param name="dsEvents">A Dataset object which contain event data</param>
        /// <returns>This method will return the Event object</returns>
        private Events PopulateEventObject(DataSet dsEvents)
        {
            try
            {
                Events objEvent = new Events();

                // Get the Event Type List
                if (dsEvents.Tables.Count > 0)
                {
                    if (dsEvents.Tables[0].Rows.Count > 0)
                    {
                        objEvent.EventTypeList = PopulateEventType(dsEvents.Tables[0]);
                    }
                }

                // Get the Image List
                if (dsEvents.Tables.Count > 1)
                {
                    if (dsEvents.Tables[1].Rows.Count > 0)
                    {
                        objEvent.ImageList = PopulateImageList(dsEvents.Tables[1]);
                    }
                }

                // Get the Country List
                if (dsEvents.Tables.Count > 2)
                {
                    if (dsEvents.Tables[2].Rows.Count > 0)
                    {
                        objEvent.CountryList = PopulateLocation(dsEvents.Tables[2]);
                    }
                }

                // Get the User role for the tribute
                if (dsEvents.Tables.Count > 3)
                {
                    if (dsEvents.Tables[3].Rows.Count > 0)
                    {
                        objEvent.IsAdmin = GetUserRole(dsEvents.Tables[3]);
                    }
                }

                // Get the User Detail
                if (dsEvents.Tables.Count > 4)
                {
                    if (dsEvents.Tables[4].Rows.Count > 0)
                    {
                        DataRow drUser = dsEvents.Tables[4].Rows[0];
                        objEvent.UserName = GetUserName(drUser);
                        objEvent.UserId = int.Parse(drUser["UserId"].ToString());
                    }
                }

                // Get the State List
                if (dsEvents.Tables.Count > 5)
                {
                    if (dsEvents.Tables[5].Rows.Count > 0)
                    {
                        objEvent.StateList = PopulateLocation(dsEvents.Tables[5]);
                    }
                }

                // Get the Event Detail
                if (dsEvents.Tables.Count > 6)
                {
                    if (dsEvents.Tables[6].Rows.Count > 0)
                    {
                        DataRow drEvents = dsEvents.Tables[6].Rows[0];

                        objEvent.EventName = drEvents["EventName"].ToString();
                        objEvent.EventTypeName = drEvents["EventTypeName"].ToString();
                        objEvent.EventDate = DateTime.Parse(drEvents["EventDate"].ToString());
                        objEvent.EventImage = drEvents["EventImage"].ToString();
                        objEvent.EventDesc = drEvents["EventDesc"].ToString();
                        objEvent.Location = drEvents["Location"].ToString();
                        objEvent.Address = drEvents["Address"].ToString();
                        objEvent.City = drEvents["City"].ToString();
                        objEvent.State = drEvents["State"].ToString();
                        objEvent.Country = drEvents["Country"].ToString();
                        objEvent.EventStartTime = drEvents["EventStartTime"].ToString();
                        objEvent.EventEndTime = drEvents["EventEndTime"].ToString();
                        objEvent.PhoneNumber = drEvents["PhoneNumber"].ToString();
                        objEvent.EmailId = drEvents["EmailId"].ToString();
                        objEvent.HostName = drEvents["HostName"].ToString();
                        objEvent.IsPrivate = bool.Parse(drEvents["IsPrivate"].ToString());
                        objEvent.UserName = GetUserName(drEvents);
                        objEvent.EventPlace = drEvents["EventPlace"].ToString();
                        objEvent.AllowAdditionalPeople = bool.Parse(drEvents["AllowAdditionalPeople"].ToString());
                        objEvent.SendEmailReminder = bool.Parse(drEvents["SendEmailReminder"].ToString());
                        objEvent.ShowRsvpStatistics = bool.Parse(drEvents["ShowRsvpStatistics"].ToString());
                        objEvent.MealOptions = drEvents["MealOptions"].ToString();
                        objEvent.EventMessage = drEvents["EventMessage"].ToString();
                        objEvent.IsAskForMeal = bool.Parse(drEvents["IsAskForMeal"].ToString());
                    }
                }


                return objEvent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will populate the Event Type object from the dataset
        /// </summary>
        /// <param name="dsTable">A DataTable which contain Event Type List</param>
        /// <returns>return a list of the event type</returns>
        private IList<string> PopulateEventType(DataTable dsTable)
        {
            IList<string> lstEventType = null;
            try
            {
                // Get the Event Type List
                if (dsTable.Rows.Count > 0)
                {
                    lstEventType = new List<string>();
                    foreach (DataRow drEventType in dsTable.Rows)
                    {
                        string objEventType = drEventType["EventType"].ToString();

                        lstEventType.Add(objEventType);
                        objEventType = null;
                    }
                }
                lstEventType.Add("other");

                return lstEventType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will populate the Image List object from the dataset
        /// </summary>
        /// <param name="dsTable">A DataTable which contain Image List</param>
        /// <returns>return a list of the Image</returns>
        private List<GiftImage> PopulateImageList(DataTable dsTable)
        {
            List<GiftImage> lstEventImage = null;
            try
            {
                string virtalPath = GetPath();

                // Get the Image List
                if (dsTable.Rows.Count > 0)
                {
                    lstEventImage = new List<GiftImage>();
                    foreach (DataRow drEventImage in dsTable.Rows)
                    {
                        GiftImage objEventImage = new GiftImage();

                        objEventImage.ImageId = int.Parse(drEventImage["ImageId"].ToString());
                        objEventImage.ImageUrl = virtalPath + drEventImage["ImageUrl"].ToString();

                        lstEventImage.Add(objEventImage);
                        objEventImage = null;
                    }
                }

                return lstEventImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will populate the Country List object from the dataset
        /// </summary>
        /// <param name="dsTable">A DataTable which contain Country List</param>
        /// <returns>return a list of the Country</returns>
        private List<Locations> PopulateLocation(DataTable dsTable)
        {
            List<Locations> lstCountry = null;
            try
            {
                // Get the Image List
                if (dsTable.Rows.Count > 0)
                {
                    lstCountry = new List<Locations>();
                    foreach (DataRow drCountry in dsTable.Rows)
                    {
                        Locations objCountry = new Locations();

                        objCountry.LocationId = int.Parse(drCountry["LocationId"].ToString());
                        objCountry.LocationName = drCountry["LocationName"].ToString();
                        objCountry.LocationParentId = int.Parse(drCountry["LocationParentId"].ToString());

                        if (objCountry.LocationName == "United States")
                        {
                            //lstCountry.Insert(0, objCountry);
                            lstCountry.Add(objCountry);
                            objCountry = null;
                        }
                    }
                    foreach (DataRow drCountry in dsTable.Rows)
                    {
                        Locations objCountry = new Locations();

                        objCountry.LocationId = int.Parse(drCountry["LocationId"].ToString());
                        objCountry.LocationName = drCountry["LocationName"].ToString();
                        objCountry.LocationParentId = int.Parse(drCountry["LocationParentId"].ToString());

                        if (objCountry.LocationName == "Canada")
                        {
                            //lstCountry.Insert(1, objCountry);
                            lstCountry.Add(objCountry);
                            objCountry = null;
                        }

                    }
                    foreach (DataRow drCountry in dsTable.Rows)
                    {
                        Locations objCountry = new Locations();

                        objCountry.LocationId = int.Parse(drCountry["LocationId"].ToString());
                        objCountry.LocationName = drCountry["LocationName"].ToString();
                        objCountry.LocationParentId = int.Parse(drCountry["LocationParentId"].ToString());
                        lstCountry.Add(objCountry);
                        objCountry = null;

                    }
                }

                return lstCountry;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will return the User Role from the dataset
        /// </summary>
        /// <param name="dsTable">A DataTable which contain User role</param>
        /// <returns>return user role as a boolean variable</returns>
        private bool GetUserRole(DataTable dsTable)
        {
            bool userRole = false;

            try
            {

                // Get the User role for the tribute
                if (dsTable.Rows.Count > 0)
                {
                    if (dsTable.Rows.Count > 0)
                    {
                        DataRow drAdmin = dsTable.Rows[0];
                        if (int.Parse(drAdmin["IsAdmin"].ToString()) == 0)
                        {
                            userRole = false;
                        }
                        else
                        {
                            userRole = true;
                        }
                    }
                }

                return userRole;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will get the list of events from the Dataset
        /// </summary>
        /// <param name="dsEvents">a datset which contain the event info</param>
        /// <param name="objEvent">A event object which will populate by the user role for the tribute</param>
        /// <returns></returns>
        private List<Events> PopulateEventListObject(DataSet dsEvents, Events objEvent)
        {
            List<Events> lstEvent = new List<Events>();

            try
            {
                // Get the User role for the tribute
                if (dsEvents.Tables.Count > 0)
                {
                    if (dsEvents.Tables[0].Rows.Count > 0)
                    {
                        objEvent.IsAdmin = GetUserRole(dsEvents.Tables[0]);
                    }
                }

                // Get the Tribute Detail
                if (dsEvents.Tables.Count > 1)
                {
                    if (dsEvents.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow drEvents in dsEvents.Tables[1].Rows)
                        {
                            Events tmpEvent = null;

                            // If user is anonymous user
                            if (objEvent.UserId == 0)           
                            {   
                                // then only get the events which are not private
                                if (bool.Parse(drEvents["IsPrivate"].ToString()) == false)
                                {
                                    tmpEvent = GetEventData(drEvents);
                                }   
                            }
                            // If user is admin of the tribute
                            else if (objEvent.IsAdmin == true)  
                            {
                                // then get all the public and private event
                                tmpEvent = GetEventData(drEvents); 
                            }
                            // If user is admin of the tribute
                            else if (objEvent.IsAdmin == false)     
                            {
                                // Then get all the events which are not private
                                if (bool.Parse(drEvents["IsPrivate"].ToString()) == false)
                                {
                                    tmpEvent = GetEventData(drEvents);
                                }
                                // and also the events which priavte and for which user is invited.
                                else if ((bool.Parse(drEvents["IsPrivate"].ToString()) == true) &&
                                         (drEvents["RsvpId"].ToString() != "") && (int.Parse(drEvents["RsvpId"].ToString()) != 0))
                                {

                                    tmpEvent = GetEventData(drEvents);
                                }
                            }

                            if (tmpEvent != null)
                            {
                                lstEvent.Add(tmpEvent);
                                tmpEvent = null;
                            }
                        }
                    }

                }

                return lstEvent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method will get the event data from the row
        /// </summary>
        /// <param name="drEvents">A DataRow which contain teh event data</param>
        /// <returns>returns a event object populate with the event detail</returns>
        private Events GetEventData(DataRow drEvents)
        {
            Events tmpEvent = new Events();
            string virtalPath = GetPath();

            try
            {
                tmpEvent.EventID = int.Parse(drEvents["EventID"].ToString());
                tmpEvent.EventName = drEvents["EventName"].ToString();
                string EventTime = "";
                EventTime = DateTime.Parse(drEvents["EventDate"].ToString()).ToString("MMMM dd, yyyy");
                EventTime += " from " + drEvents["EventStartTime"].ToString() + " - " + drEvents["EventEndTime"].ToString();
                tmpEvent.EventDateAndTime = EventTime;

                tmpEvent.EventImage = virtalPath + drEvents["EventImage"].ToString();
                tmpEvent.EventPlace = GetLocation(drEvents);
                tmpEvent.UserName = GetUserName(drEvents);
                tmpEvent.UserId = int.Parse(drEvents["UserId"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tmpEvent;
        }

        /// <summary>
        /// This method will return the location by combining the city, state and country
        /// </summary>  
        /// <param name="objStory">A Story object which contain teh city, state and country</param>
        /// <returns>Return the location</returns>
        private string GetLocation(DataRow drEvent)
        {
            string location = "";

            try
            {
               // objEvent.EventPlace = "Where: " + lblEventLocation.Text + "<br/> " + lblEventAddress.Text + "<br/> " + lblEventCity.Text + ", " + lblEventState.Text + "<br/>" + lblEventCountry.Text;
                if (drEvent["Location"].ToString() != "")
                {
                    location = drEvent["Location"].ToString();
                   location += "<br/> ";
                }

                if (drEvent["Address"].ToString() != "")
                {
                    location += drEvent["Address"].ToString();
                   location += "<br/> ";
                }

                if (drEvent["City"].ToString() != "")
                {
                    location += drEvent["City"].ToString();
                    location += ", ";
                }

                if (drEvent["StateName"].ToString() != "")
                {
                    location += drEvent["StateName"].ToString();
                    location += "<br/> ";
                }

                location += drEvent["CountryName"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return location;
        }

        /// <summary>
        /// This method will get the userName on the basis of the attribute IsUserNameVisiable
        /// </summary>
        /// <param name="drGift">A DataRow object which contain the City, State and Country</param>
        /// <returns>This method will return the string object which contain the userName</returns>
        private string GetUserName(DataRow drEvent)
        {
            string userName = "";

            try
            {
                //if user is registered User
                if ((drEvent["UserId"] != null) && (drEvent["UserId"].ToString() != ""))
                {
                    // if IsUserNameVisiable is True then get the userName
                    if (bool.Parse(drEvent["IsUserNameVisiable"].ToString()) == true)
                    {
                        // if user name exist means Registered user add the gift then get teh userName
                        if ((drEvent["UserName"] != null) && (drEvent["UserName"].ToString() != ""))
                        {
                            userName = drEvent["UserName"].ToString();
                        }
                    }
                    // if IsUserNameVisiable is false then get the Name of the user
                    else
                    {
                        // if User is Personal user then get the "FirstName + LastName"
                        if ((drEvent["UserType"] != null) && (int.Parse(drEvent["UserType"].ToString()) == 1))
                        {
                            userName = drEvent["FirstName"].ToString() + " " + drEvent["LastName"].ToString();
                        }
                        // if User is Business user then get the "CompanyName"
                        else if ((drEvent["UserType"] != null) && (int.Parse(drEvent["UserType"].ToString()) == 2))
                        {
                            userName = drEvent["CompanyName"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userName;
        }

        /// <summary>
        ///  This method will remove "," from the last guest name in the event guest list
        /// </summary>
        /// <param name="gtList">List of Guest grom which we want to remove , from the last guest name</param>
        private void RemoveLastChar(IList<GuestList> gtList)
        {
            string NewguestName = string.Empty;
            string guestName = string.Empty;
            int count = gtList.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    guestName = gtList[i].UserName;

                    if (guestName.EndsWith(","))
                        NewguestName = guestName.Substring(0, guestName.Length - 1);

                    if (guestName.Contains("@"))
                    {
                        int index = guestName.IndexOf('@');
                        NewguestName = guestName.Substring(0, index);
                    }
                    if (i + 1 != count)
                    {
                        NewguestName = NewguestName + ",";
                    }
                    GuestList guestObj = new GuestList();
                    guestObj.UserId = gtList[i].UserId;
                    guestObj.UserName = NewguestName;

                    gtList.Add(guestObj);
                }
                for (int i = 0; i < count; i++)
                {
                    gtList.Remove(gtList[0]);
                }
            }

        }

        /// <summary>
        /// This function is used to get the path for photos to get and save.
        /// </summary>
        /// <returns>Array of string containing drive name, root folder name and photo virtual directory name.</returns>
        public string GetPath()
        {
            string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\PhotoConfiguration.xml";

            FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            XmlDocument contactDoc = new XmlDocument();
            //Load the Xml Document
            contactDoc.Load(docIn);

            //Get a node
            XmlNodeList photoVirtualDirectory = contactDoc.GetElementsByTagName("PhotoVirtualDirectory");

            //get the value
            return photoVirtualDirectory.Item(0).InnerText;
        }






        /// <summary>
        /// This method loop through the dataset, extract all the values and prepare a list of EventInvitationCategory object
        /// </summary>
        /// <param name="dsInvitationCategory"></param>
        /// <returns></returns>
        private IList<EventInvitationCategory> PopulateEventInvitationCategoryList(DataSet dsInvitationCategory)
        {
            IList<EventInvitationCategory> lstInvitationCategory = new List<EventInvitationCategory>();
            EventInvitationCategory objCategory = null;
            try
            {                
                if (dsInvitationCategory.Tables.Count > 0)
                {
                    if (dsInvitationCategory.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drCategory in dsInvitationCategory.Tables[0].Rows)
                        {
                            objCategory = new EventInvitationCategory();
                            objCategory.InvitationCategoryID = int.Parse(drCategory["InvitationCategoryID"].ToString());
                            objCategory.InvitationCategoryName = drCategory["InvitationCategoryName"].ToString();

                            lstInvitationCategory.Add(objCategory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstInvitationCategory;
        }


        //populate sub category

        private IList<Themes> PopulateThemeSubCategoryList(DataSet dsThemesCategory)
        {
            IList<Themes> lstThemeSubCategory = new List<Themes>();
            Themes objThemes = null;
            try
            {
                if (dsThemesCategory.Tables.Count > 0)
                {
                    if (dsThemesCategory.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drSubCategory in dsThemesCategory.Tables[0].Rows)
                        {
                            objThemes = new Themes();
                            objThemes.SubCategory = drSubCategory["SubCategory"].ToString();

                            lstThemeSubCategory.Add(objThemes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstThemeSubCategory;
        }

        //Populate Themes
        private IList<Themes> PopulateThemeList(DataSet dsThemesName)
        {
            IList<Themes> lstThemeName = new List<Themes>();
            Themes objThemes = null;
            try
            {
                if (dsThemesName.Tables.Count > 0)
                {
                    if (dsThemesName.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drThemeName in dsThemesName.Tables[0].Rows)
                        {
                            objThemes = new Themes();
                            objThemes.ThemeId =Convert.ToInt16(drThemeName["Themeid"].ToString());
                            objThemes.ThemeName  = drThemeName["ThemeName"].ToString();

                            lstThemeName.Add(objThemes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstThemeName;
        }


        //Populate FolderName
        private string  PopulateFolderName(DataSet dsFolderName)
        {
            string lstFolderName = "";
            Themes objThemes = null;
            try
            {
                if (dsFolderName.Tables.Count > 0)
                {
                    if (dsFolderName.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drFolderName in dsFolderName.Tables[0].Rows)
                        {
                            lstFolderName = drFolderName["FolderName"].ToString(); ;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstFolderName;
        }

        //Populate Category

        private IList<Themes> PopulateThemeCategoryList(DataSet dsThemesCategory)
        {
            IList<Themes> lstThemesCategory = new List<Themes>();
            Themes objTheme = null;
            try
            {
                if (dsThemesCategory.Tables.Count > 0)
                {
                    if (dsThemesCategory.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drThemes in dsThemesCategory.Tables[0].Rows)
                        {
                            objTheme = new Themes();
                            objTheme.Tributetype = drThemes["TributeType"].ToString();

                            lstThemesCategory.Add(objTheme);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstThemesCategory;
        }

        /// <summary>
        /// This method loop through the dataset, extract all the values and prepare a list of EventTheme object
        /// </summary>
        /// <param name="dsEventTheme"></param>
        /// <returns></returns>
        private IList<EventTheme> PopulateEventThemeList(DataSet dsEventTheme)
        {
            IList<EventTheme> eventThemeList = new List<EventTheme>();
            EventTheme objEventTheme = null;
            try
            {                
                if (dsEventTheme.Tables.Count > 0)
                {
                    if (dsEventTheme.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drEventTheme in dsEventTheme.Tables[0].Rows)
                        {
                            objEventTheme = new EventTheme();
                            objEventTheme.EventThemeID = int.Parse(drEventTheme["EventThemeID"].ToString());
                            objEventTheme.EventThemeName = drEventTheme["EventThemeName"].ToString();
                            objEventTheme.InvitationCategoryID = int.Parse(drEventTheme["InvitationCategoryID"].ToString());
                            objEventTheme.ThemeFullSizeImage = drEventTheme["ThemeFullSizeImage"].ToString();
                            objEventTheme.ThemePreviewImage = drEventTheme["ThemePreviewImage"].ToString();
                            objEventTheme.ThemeThumbnailImage = drEventTheme["ThemeThumbnailImage"].ToString();

                            eventThemeList.Add(objEventTheme);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eventThemeList;
        }

        /// <summary>
        /// The methods extract the values from dataset and fill the EventTheme object
        /// </summary>
        /// <param name="dsEventTheme"></param>
        /// <returns></returns>
        private EventTheme PopulateEventTheme(DataSet dsEventTheme)
        {
            EventTheme objEventTheme = null;
            try
            {
                // Check data exist
                if (dsEventTheme.Tables.Count > 0)
                {
                    if (dsEventTheme.Tables[0].Rows.Count > 0)
                    {
                        DataRow drEventTheme = dsEventTheme.Tables[0].Rows[0];
                        objEventTheme = new EventTheme();
                        objEventTheme.EventThemeID = int.Parse(drEventTheme["EventThemeID"].ToString());
                        objEventTheme.EventThemeName = drEventTheme["EventThemeName"].ToString();
                        objEventTheme.InvitationCategoryID = int.Parse(drEventTheme["InvitationCategoryID"].ToString());
                        objEventTheme.ThemeFullSizeImage = drEventTheme["ThemeFullSizeImage"].ToString();
                        objEventTheme.ThemePreviewImage = drEventTheme["ThemePreviewImage"].ToString();
                        objEventTheme.ThemeThumbnailImage = drEventTheme["ThemeThumbnailImage"].ToString();
                        objEventTheme.ThemeBackgroundColor = drEventTheme["ThemeBackgroundColor"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objEventTheme;
        }

        #endregion

        #endregion 

    
        public int GetUserIdByTributeId(int tributeId)
        {
            int userId=0;
            try
            {
                if (tributeId != 0)
                {
                    DataSet dsTribute = new DataSet();

                    object[] objGuestParam = { tributeId };

                    dsTribute = GetDataSet("usp_GetUserIdByTributeId", objGuestParam);

                    // Get the Event Guest list
                    if (dsTribute.Tables.Count > 0)
                    {
                        if (dsTribute.Tables[0].Rows.Count > 0)
                        {
                            if(dsTribute.Tables[0].Rows[0]["UserTributeId"]!=null)
                                userId = int.Parse(dsTribute.Tables[0].Rows[0]["UserTributeId"].ToString());
                        }
                    }
                }
            }            
            catch (Exception ex)
            {
                throw ex;
            }

            return userId;
        }

        public int GetTributeIdOnTributeUrl(string tributeUrl, string ApplicationType)
        {
            int tributeId=0;
            try
            {
                if (tributeUrl != null)
                {
                    DataSet dsTribute = new DataSet();

                    object[] objGuestParam = { tributeUrl, ApplicationType };

                    dsTribute = GetDataSet("usp_GetTributeIdOnTributeUrl", objGuestParam);

                    if (dsTribute.Tables.Count > 0)
                    {
                        if (dsTribute.Tables[0].Rows.Count > 0)
                        {
                            if(dsTribute.Tables[0].Rows[0]["TributeId"]!=null)
                                tributeId = int.Parse(dsTribute.Tables[0].Rows[0]["TributeId"].ToString());
                        }
                    }
                }
            }            
            catch (Exception ex)
            {
                throw ex;
            }

            return tributeId;
        }
        
    }//end class
}//end namespace
