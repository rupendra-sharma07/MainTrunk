///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Story.Views.ViewStoryPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Story.
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
using TributesPortal.MultipleLangSupport;

#endregion


/// <summary>
///Tribute Portal-Story Presenter Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// 
/// </summary>

namespace TributesPortal.Story.Views
{
    public class ViewStoryPresenter : Presenter<IViewStory>
    {
        #region CLASS VARIABLES

        private StoryController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A StoryController object to call the method of controller</param>
        public ViewStoryPresenter([CreateNew] StoryController controller)
        {
            _controller = controller;
        }

        #endregion


        #region METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// This method will call every time the view loads
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
        }


        /// <summary>
        /// This method will call the Story Controller class method to get the Tribute Detail, Story, 
        /// and List of topic in More About section. and populate all the control in UI
        /// </summary>
        public void GetStoryDetail()
        {
            try
            {
                Stories objStory = new Stories();

                objStory.TributeId = View.TributeID;
                objStory.UserId = View.UserID;

                Stories objStoryList = _controller.GetStoryDetail(objStory);

                if (objStoryList != null)
                {
                    PopulateValueInControl(objStoryList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Controller class to get the Country list
        /// and populate the country drop down list on UI
        /// </summary>
        public void GetCountryList()
        {
            try
            {
               // View.CountryList = _controller.GetCountryList(Locationid(null));

                List<Locations> objList = new List<Locations>();
                List<Locations> objList_ = new List<Locations>();
                objList = (List<Locations>)_controller.GetCountryList(Locationid(null));
                if (objList.Count > 0)
                {
                    for (int i = 0; i < objList.Count; i++)
                    {
                        if (objList[i].LocationName.Equals("United States"))
                        {
                            Locations objloc = new Locations();
                            objloc.LocationId = objList[i].LocationId;
                            objloc.LocationName = objList[i].LocationName;
                            objloc.LocationParentId = objList[i].LocationParentId;
                            objList_.Add(objloc);
                        }
                    }
                    for (int i = 0; i < objList.Count; i++)
                    {
                        if (objList[i].LocationName.Equals("Canada"))
                        {
                            Locations objloc = new Locations();
                            objloc.LocationId = objList[i].LocationId;
                            objloc.LocationName = objList[i].LocationName;
                            objloc.LocationParentId = objList[i].LocationParentId;
                            objList_.Add(objloc);
                        }
                    }
                    for (int i = 0; i < objList.Count; i++)
                    {
                        Locations objloc = new Locations();
                        objloc.LocationId = objList[i].LocationId;
                        objloc.LocationName = objList[i].LocationName;
                        objloc.LocationParentId = objList[i].LocationParentId;
                        objList_.Add(objloc);
                    }
                }
                View.CountryList = objList_; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Controller class to get the State list for selected country
        /// and populate the state drop down list on UI
        /// </summary>
        public void GetStateList()
        {
            try
            {
                View.StateList = _controller.GetCountryList(Locationid(View.Country));

              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Controller class method to update the tribute detail
        /// </summary>
        public void UpdateTributeDetail()
        {
            try
            {
                _controller.UpdateTributeDetail(CreateTributeDetailObject());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateObituaryDetail()
        {
            Stories objsTribute = new Stories();
            try
            {
                objsTribute.TributeId = View.TributeID;
                objsTribute.PostMessage = View.ObPostMessage;
                objsTribute.MessageWithoutHtml = View.ObMessageWithoutHtml;
                objsTribute.MessageAddedModifiedBy = View.UserID;
                _controller.UpdateObituaryDetail(objsTribute);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///  This method will call the Story Controller class method to Add and update the Story detail 
        /// </summary>
        public void UpdateStoryDetail()
        {
            try
            {
                _controller.UpdateStoryDetail(CreateStoryeDetailObject());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This Method will call the Story Controller class method to get the Topic list 
        /// and and fill it to the UI control
        /// </summary>
        public void GetTopic()
        {
            try
            {
                object[] objStoryParam = { View.TributeType, View.TributeID, View.SecondaryTitle };

                IList<StoryMoreAbout> tmpList = _controller.GetTopic(objStoryParam);
                
                StoryMoreAbout tmpMoreAbout = new StoryMoreAbout();
                tmpMoreAbout.SecondaryTitle = ResourceText.GetString("lblSelectTopc_ST");
                tmpList.Insert(0, tmpMoreAbout);

                View.TopicList = tmpList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        /// <summary>
        ///  This method will call the Story Controller class method to update and add 
        ///  new topic in More about section 
        /// </summary>
        public void SaveTopic()
        {
            try
            {
                _controller.UpdateStoryDetail(CreateMoreAboutObject());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Controller class method to Delete the Topic from the More about section
        /// </summary>
        /// <param name="objStory">stories object which contain the Section id and Userbioagraphy ID
        ///                        of the topic which topic user wants to delete
        /// </param>
        public void DeleteTopic()
        {
            try
            {
                Stories objStory = new Stories();
                objStory.TributeId = View.TributeID;

                objStory.MoreAboutSection = new List<StoryMoreAbout>();
                
                StoryMoreAbout objMoreAbout = new StoryMoreAbout();
                objMoreAbout.SectionId = View.SectionId;
                objMoreAbout.UserBiographyId = View.UserBiographyId;
                objMoreAbout.SecondaryTitle = View.SecondaryTitle;

                objStory.MoreAboutSection.Add(objMoreAbout);

                _controller.DeleteTopic(objStory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This Method Will Visible the Edit controls and invisible the View Controls 
        /// of the Personal Detail section
        /// </summary>
        public void VisiblePersonalDetailEdit()
        {
            // Display Personal Detail Edit mode and Hide the view mode
            View.CSSClassStory = GetEnumValueDescription(Stories.CSSClassEnum.StoryDisable);
            View.CSSClassMoreAbout = GetEnumValueDescription(Stories.CSSClassEnum.MoreAboutDisable);

            View.IsVisibleStoryView = true;
            View.IsVisibleMoreAboutView = true;
            View.IsVisiblePersonalDetailView = false;
            View.IsVisibleEdit = false;

            object[] objLocation = View.LocationViewState; //GetValuesFromViewState(Stories.StoryMaintainState.Location.ToString());

            GetCountryList();
            if (objLocation != null)
            {
                View.City = objLocation[0].ToString();

                // Display country and state list
                View.Country = objLocation[2].ToString();

                GetStateList();
                if ((objLocation[1] != null) && (objLocation[1].ToString() != ""))
                {
                    View.State = objLocation[1].ToString();
                }
            }
        }

        /// <summary>
        /// This Method Will Visible the Edit controls and invisible the View Controls 
        /// of the Story Detail section
        /// </summary>
        public void VisibleStoryEdit()
        {
            // Display Personal Detail Edit mode and Hide the view mode
            View.CSSClassPersonalDetail = GetEnumValueDescription(Stories.CSSClassEnum.PersonalDetailDisable);
            View.CSSClassMoreAbout = GetEnumValueDescription(Stories.CSSClassEnum.MoreAboutDisable);

            View.IsVisiblePersonalDetailView = true;
            View.IsVisibleMoreAboutView = true;
            View.IsVisibleStoryView = false;
            View.IsVisibleEdit = false;
        }


        public void VisibleObituaryEdit()
        {
            //LHK:CSS changes to be made. Display Personal Detail Edit mode and Hide the view mode
            View.CSSClassPersonalDetail = GetEnumValueDescription(Stories.CSSClassEnum.PersonalDetailDisable);
            View.CSSClassMoreAbout = GetEnumValueDescription(Stories.CSSClassEnum.MoreAboutDisable);

            View.IsVisiblePersonalDetailView = true;
            View.IsVisibleMoreAboutView = true;
            View.IsVisibleObituaryView = false;
            View.IsVisibleStoryView = false;
            View.IsVisibleEdit = false;
        }
        /// <summary>
        /// This Method Will Visible the Edit controls and invisible the View Controls 
        /// of the More About section
        /// </summary>
        public void VisibleMoreAboutEdit()
        {
            // Display Personal Detail Edit mode and Hide the view mode
            View.CSSClassPersonalDetail = GetEnumValueDescription(Stories.CSSClassEnum.PersonalDetailDisable);
            View.CSSClassStory = GetEnumValueDescription(Stories.CSSClassEnum.StoryDisable);
            View.CSSClassMoreAboutRows = GetEnumValueDescription(Stories.CSSClassEnum.MoreAboutDisable);

            View.IsVisiblePersonalDetailView = true;
            View.IsVisibleStoryView = true;
            View.IsVisibleMoreAboutView = false;
            View.IsVisibleEdit = false;
        }


        /// <summary>
        /// This Method Will Visible the Edit controls and invisible the View Controls 
        /// of the Personal Detail, Story and more about section
        /// </summary>
        public void VisibleAllViewMode()
        {
            View.CSSClassPersonalDetail = GetEnumValueDescription(Stories.CSSClassEnum.PersonalDetail);
            View.CSSClassStory = GetEnumValueDescription(Stories.CSSClassEnum.Story);
            View.CSSClassMoreAbout = GetEnumValueDescription(Stories.CSSClassEnum.MoreAbout);
            View.CSSClassMoreAboutRows = GetEnumValueDescription(Stories.CSSClassEnum.MoreAbout);

            View.IsVisiblePersonalDetailView = true;
            View.IsVisibleStoryView = true;
            View.IsVisibleMoreAboutView = true;
            View.IsVisibleEdit = View.IsAdmin;
        }

        /// <summary>
        /// This Method Will Visible the Add new topic section
        /// </summary>
        public void VisibleAddNewTopic()
        {
            View.CSSClassPersonalDetail = GetEnumValueDescription(Stories.CSSClassEnum.PersonalDetailDisable);
            View.CSSClassStory = GetEnumValueDescription(Stories.CSSClassEnum.StoryDisable);
            View.CSSClassMoreAbout = GetEnumValueDescription(Stories.CSSClassEnum.MoreAboutDisable);
            View.CSSClassMoreAboutRows = GetEnumValueDescription(Stories.CSSClassEnum.MoreAboutDisable); 

            View.IsVisiblePersonalDetailView = true;
            View.IsVisibleObituaryView = true;
            View.IsVisibleStoryView = true;
            View.IsVisibleMoreAboutView = true;

            View.IsVisibleAddNewTopic = true;
            View.IsVisibleEdit = false;
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This method will populate the values in the controls
        /// </summary>
        /// <param name="objStory">A Stories object which contain all the values</param>
        private void PopulateValueInControl(Stories objStory)
        {
            bool isStory = false;

            try
            {
                // Populate the Personal Detail Section
                View.TributeName = objStory.TributeName;
                View.Location = GetLocation(objStory);
                View.TributeImage = objStory.TributeImage;
                View.StoryImagePrevURL = objStory.TributeImage;
                if(objStory.Age != null )
                {
                View.Age = objStory.Age;
                }

                PopulateDateInControl(objStory.Date1, objStory.Date2, objStory.TributeType);

                View.ObPostMessage = objStory.PostMessage;
                View.ObMessageWithoutHtml = objStory.MessageWithoutHtml;
                // Populate the story Section
                if (objStory.MoreAboutSection != null)
                {
                    foreach (StoryMoreAbout moreabout in objStory.MoreAboutSection)
                    {
                        if (moreabout.PrimaryTitle == Stories.StorySectionEnum.Story.ToString())
                        {
                            isStory = true;
                            View.StoryDetail = moreabout.SectionAnswer;

                            object[] objValue = { moreabout.SectionId, moreabout.UserBiographyId };
                            View.storyViewState = objValue;
                            //AddValuesInViewState(objValue, Stories.StorySectionEnum.Story.ToString());
                            objStory.MoreAboutSection.Remove(moreabout);

                            break;
                        }
                    }
                }

                if (isStory == false)
                {
                    // Initialize the story object by null in the View State
                    View.storyViewState = null;
                    //AddValuesInViewState(null, Stories.StorySectionEnum.Story.ToString());
                    View.StoryDetail = "";
                }

                // Populate the More About Section
                if (objStory.MoreAboutSection != null)
                {
                    View.MoreAbout = objStory.MoreAboutSection;
                }

                View.IsAdmin = objStory.IsAdmin;

                // If user is admin then only show the edit button
                View.IsVisibleEdit = objStory.IsAdmin;

                object[] objLocation = { objStory.City, objStory.State, objStory.Country };
                View.LocationViewState = objLocation;
                //AddValuesInViewState(objLocation, Stories.StoryMaintainState.Location.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method will return the location by combining the city, state and country
        /// </summary>
        /// <param name="objStory">A Story object which contain teh city, state and country</param>
        /// <returns>Return the location</returns>
        private string GetLocation(Stories objStory)
        {
            string location = "";

            try
            {
                if (objStory.City != "")
                {
                    location = objStory.City;
                }

                if (objStory.StateName != "")
                {
                    if (location != "")
                    {
                        location += ", ";
                    }

                    location += objStory.StateName;
                }
                
                if (location != "")
                {
                    location += ", ";
                }

                location += objStory.CountryName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return location;
        }


        /// <summary>
        /// This Method will call Setdate method to populate values in the Date controls 
        /// on the basis of the tribute type
        /// </summary>
        /// <param name="tributeDate1">This is DateTime object which contain First Date(Wedding, Birth, Aniversery)</param>
        /// <param name="tributeDate2">This is DateTime object which contains the Death Death</param>
        /// <param name="tributeType">This is string object which conatins the type of teh tribute</param>
        private void PopulateDateInControl(Nullable<DateTime> tributeDate1, Nullable<DateTime> tributeDate2, string tributeType)
        {
            try
            {

                if (tributeType == PortalEnums.TributeTypeEnum.Memorial.ToString())
                {
                    // set require field for date2
                    View.isRequiredFieldDate1 = false;
                    View.isRequiredFieldDate2 = true;

                    SetDate(tributeDate1, tributeDate2, ResourceText.GetString("lblDate1Born_ST"),
                            ResourceText.GetString("lblDate2Died_ST"), tributeType);
                }
                else if (tributeType == GetEnumValueDescription(PortalEnums.TributeTypeEnum.NewBaby))
                {
                    View.isRequiredFieldDate1 = false;
                    View.isRequiredFieldDate2 = false;

                    SetDate(tributeDate1, tributeDate2, ResourceText.GetString("lblDate1Born_ST"),
                            ResourceText.GetString("lblDate1Due_ST"), tributeType);
                }
                else if (tributeType == PortalEnums.TributeTypeEnum.Anniversary.ToString())
                {
                    View.isRequiredFieldDate1 = true;

                    SetDate(tributeDate1, null, ResourceText.GetString("lblDate1Aniversery_ST"), "", tributeType);
                }
                else if (tributeType == PortalEnums.TributeTypeEnum.Wedding.ToString())
                {
                    View.isRequiredFieldDate1 = true;

                    SetDate(tributeDate1, null, ResourceText.GetString("lblDate1Wedding_ST"), "", tributeType);
                }
                else if (tributeType == PortalEnums.TributeTypeEnum.Birthday.ToString())
                {
                    View.isRequiredFieldDate1 = true;

                    SetDate(tributeDate1, null, ResourceText.GetString("lblDate1Born_ST"), "", tributeType);
                }
                else if (tributeType == PortalEnums.TributeTypeEnum.Graduation.ToString())
                {
                    View.isRequiredFieldDate1 = true;

                    SetDate(tributeDate1, null, ResourceText.GetString("lblDate1Graduation_ST"), "", tributeType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This Method will populate values and text in the Date controls 
        /// </summary>
        /// <param name="tributeDate1">This is DateTime object which contain First Date(Wedding, Birth, Aniversery)</param>
        /// <param name="tributeDate2">This is DateTime object which contains the Death Death</param>
        /// <param name="text1">This is string object which contain text for the first Date</param>
        /// <param name="text2">This is string object which contain text for the Second Date</param>
        private void SetDate(Nullable<DateTime> tributeDate1, Nullable<DateTime> tributeDate2, string text1, string text2, string tributeType)
        {
            if (tributeDate1.ToString() != "")
            {
                DateTime BornDate = DateTime.Parse(tributeDate1.ToString());

                if ( (tributeType == PortalEnums.TributeTypeEnum.Birthday.ToString()) && (BornDate.Year.ToString() == "1780") )
                {
                   View.Date1Value = BornDate.ToString("MMMM dd");
                }
                else
                {
                    View.Date1Value = BornDate.ToString("MMMM dd, yyyy");
                    View.Date1Year = BornDate.Year.ToString();
                }
                View.Date1Text = text1;

                View.Date1Day = BornDate.Day.ToString();
                View.Date1Month = BornDate.Month.ToString();
            }
            else
            {
                if (tributeType == GetEnumValueDescription(PortalEnums.TributeTypeEnum.NewBaby))
                {
                    if (tributeDate2.ToString() != "")
                    {
                        DateTime DueDate = DateTime.Parse(tributeDate2.ToString());

                        View.Date1Value = DueDate.ToString("MMMM dd, yyyy");
                        View.Date1Text = text2;

                        View.Date1Day = DueDate.Day.ToString();
                        View.Date1Month = DueDate.Month.ToString();
                        View.Date1Year = DueDate.Year.ToString();

                        View.Date2VisibleView = false;
                        View.Date2VisibleEdit = true;

                        View.isVisibleDate2 = true;
                    }

                    return;
                }
                else
                {
                    //invisible the date1 in case of memorial
                    View.Date1VisibleView  = false;
                }
            }

            if (tributeDate2.ToString() != "")
            {
                DateTime DeathDate = DateTime.Parse(tributeDate2.ToString());

                View.Date2Value = DeathDate.ToString("MMMM dd, yyyy");
                View.Date2Text = text2;

                View.Date2Day = DeathDate.Day.ToString();
                View.Date2Month = DeathDate.Month.ToString();
                View.Date2Year = DeathDate.Year.ToString();
            }
            else
            {
                if (tributeType == GetEnumValueDescription(PortalEnums.TributeTypeEnum.NewBaby))
                {
                    View.isVisibleDate2 = false;
                }
                else
                {
                    View.Date2VisibleEdit = false;
                }

                View.Date2VisibleView = false;

                return;
            }
            
        }


        /// <summary>
        /// This method will get the desciption value from the enum
        /// </summary>
        /// <param name="value">A object variable for which want to get the description value</param>
        /// <returns>A string variable which returns the description value</returns>
        public string GetEnumValueDescription(object value)
        {
            // Get the type from the object.
            Type pobjType = value.GetType();

            // Get the member on the type that corresponds to the value passed in.
            FieldInfo pobjFieldInfo = pobjType.GetField(Enum.GetName(pobjType, value));

            // Now get the attribute on the field.
            DescriptionAttribute pobjAttribute = (DescriptionAttribute)
            (pobjFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]);

            // Return the description.
            return pobjAttribute.Description;
        }


        /// <summary>
        /// This method will create a Location object 
        /// </summary>
        /// <param name="ID">A int variable which contain location ID</param>
        /// <returns>This method will return the Location object</returns>
        private Locations Locationid(string ID)
        {
            try
            {
                Locations objLocations = new Locations();

                if (ID != null)
                {
                    objLocations.LocationParentId = int.Parse(ID);
                }
                else
                {
                    objLocations.LocationParentId = 0;
                }

                return objLocations;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method will Populate the Tribute Deatil object to update values in the database
        /// </summary>
        /// <returns></returns>
        private Stories CreateTributeDetailObject()
        {
            Stories objTribute = new Stories();

            try
            {
                objTribute.TributeId = View.TributeID;
                objTribute.TributeName = View.TributeName;
                objTribute.TributeImage = View.TributeImage;

                if ((View.Date1Day != "0") && (View.Date1Month != "0") && (View.Date1Year != "") &&
                    (View.Date1Day != null) && (View.Date1Month != null) && (View.Date1Year != null) )
                {
                    objTribute.Date1 = FormatDate(View.Date1Day, View.Date1Month, View.Date1Year);
                }
                if ((View.Date2Day != "0") && (View.Date2Month != "0") && (View.Date2Year != "") &&
                    (View.Date2Day != null) && (View.Date2Month != null) && (View.Date2Year != null))
                {
                    objTribute.Date2 = FormatDate(View.Date2Day, View.Date2Month, View.Date2Year);
                }

                objTribute.PostMessage = this.View.ObPostMessage;
                objTribute.MessageWithoutHtml = this.View.ObMessageWithoutHtml;

                objTribute.City = View.City;

                if (View.State == "")
                {
                    objTribute.State = null;
                }
                else
                {
                    objTribute.State = int.Parse(View.State);   
                }

                if (View.Country == "")
                {
                    objTribute.Country = null;
                }
                else
                {
                    objTribute.Country = int.Parse(View.Country);
                }
                objTribute.ModifiedBy = View.UserID;
                objTribute.ModifiedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objTribute;
        }


        /// <summary>
        /// This method will Populate the Story Deatil object to update values in the database
        /// </summary>
        /// <returns></returns>
        private Stories CreateStoryeDetailObject()
        {
            Stories objStory = new Stories();
            objStory.MoreAboutSection = new List<StoryMoreAbout>();

            try
            {
                objStory.UserId = View.UserID;
                objStory.TributeId = View.TributeID;

                StoryMoreAbout objMoreAbout = new StoryMoreAbout();

                object[] objStoryParam = View.storyViewState; 
                if (objStoryParam != null)
                {
                    objMoreAbout.SectionId = int.Parse(objStoryParam[0].ToString());
                    objMoreAbout.UserBiographyId = int.Parse(objStoryParam[1].ToString());
                    objMoreAbout.ModifiedBy = View.UserID;
                    objMoreAbout.ModifiedDate = DateTime.Now;
                    objStory.Operation = Stories.OperationEnum.Update.ToString();
                }
                else
                {
                    objMoreAbout.CreatedBy = View.UserID;
                    objMoreAbout.CreatedDate = DateTime.Now;
                    objStory.Operation = Stories.OperationEnum.Add.ToString();
                }

                objMoreAbout.PrimaryTitle = View.PrimaryTitle;
                objMoreAbout.SecondaryTitle = View.SecondaryTitle;
                objMoreAbout.SectionAnswer = View.StoryDetail;

                objStory.MoreAboutSection.Add(objMoreAbout);

                objStory.FirstName = View.FirstName;
                objStory.LastName = View.LastName;
                objStory.UrlToEmail = View.UrlToEmail;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objStory;
        }


        /// <summary>
        /// This method will Populate the More About object to update values in the database
        /// </summary>
        /// <returns></returns>
        private Stories CreateMoreAboutObject()
        {
            Stories objStory = new Stories();
            StoryMoreAbout objMoreAbout = new StoryMoreAbout();

            try
            {
                objStory.UserId = View.UserID;
                objStory.TributeId = View.TributeID;
                objStory.MoreAboutSection = new List<StoryMoreAbout>();
                objStory.Operation = View.Operation;

                if (objStory.Operation == Stories.OperationEnum.Add.ToString())
                {
                    objMoreAbout.CreatedBy = View.UserID;
                    objMoreAbout.CreatedDate = DateTime.Now;
                }
                else if (objStory.Operation == Stories.OperationEnum.Update.ToString())
                {
                    objMoreAbout.ModifiedBy = View.UserID;
                    objMoreAbout.ModifiedDate = DateTime.Now;
                }
                objMoreAbout.PrimaryTitle = View.PrimaryTitle;

                string otherTopic = GetEnumValueDescription(Stories.StorySectionEnum.OtherTopic);
                if (View.SecondaryTitle == otherTopic)
                {
                    objMoreAbout.SecondaryTitle = View.NewTopic;
                }
                else
                {
                    objMoreAbout.SecondaryTitle = View.SecondaryTitle;
                }

                objMoreAbout.SectionId = View.SectionId;
                objMoreAbout.UserBiographyId = View.UserBiographyId;
                objMoreAbout.SectionAnswer = View.SectionAnswer;

                objStory.MoreAboutSection.Add(objMoreAbout);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objStory;
        }


        /// <summary>
        /// This method will format a Date object by passed date, month and year value
        /// </summary>
        /// <param name="day">A string object which contain the day</param>
        /// <param name="month">A string object which contain the month</param>
        /// <param name="year">A string object which contain the Year</param>
        /// <returns>This method will return the DateTime object</returns>
        private DateTime FormatDate(string day, string month, string year)
        {
            DateTime Date1;

            // Format the created after Date and time
            string afterDate = month + "/" + day + "/" + year;

            try
            {
                Date1 = DateTime.Parse(afterDate.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Date1;
        }


       /// <summary>
        /// This method will save the object in the View State
       /// </summary>
       /// <param name="objValue">A object which contain value which want to add in view state</param>
        /// <param name="key">A string varaible which is key for setting the values from view state</param>
        private void AddValuesInViewState(object[] objValue, string key)
        {
            StateManager objStatetmgr = StateManager.Instance;

            objStatetmgr.Add(key, objValue, StateManager.State.ViewState);
        }


        /// <summary>
        /// This method will get the Values in the View State for the passed key
        /// </summary>
        /// <param name="key">A string varaible which is key for getting the values from view state</param>
        /// <returns>This method will return a object which conatin View State value for the passed key</returns>
        private object[] GetValuesFromViewState(string key)
        {
            StateManager objStatetmgr = StateManager.Instance;

            object[] objValue = (object[])objStatetmgr.Get(key, StateManager.State.ViewState);

            return objValue;
        }


        /// <summary>
        /// Method to get user is admin or owner
        /// </summary>
        private void UserIsAdmin()
        {
            UserAdminOwnerInfo objUserInfo = new UserAdminOwnerInfo();
            objUserInfo.UserId = View.UserID;
            objUserInfo.TributeId = View.TributeID;
            objUserInfo.TypeName = PortalEnums.TributeContentEnum.Story.ToString();
            objUserInfo.IsAdmin = View.IsAdmin;

            StateManager objStateManager = StateManager.Instance;
            objStateManager.Add(PortalEnums.AdminInfoEnum.UserAdminInfo_Story.ToString(), objUserInfo, StateManager.State.Session);
        }


        #endregion

        #endregion
    }
}




