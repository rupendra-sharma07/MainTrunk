using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using System.Reflection;
using System.ComponentModel;
using TributesPortal.MultipleLangSupport;

namespace TributesPortal.Video.Views
{
    public class EditPersonalDetailsPresenter : Presenter<IEditPersonalDetails>
    {
        #region CLASS VARIABLES

        private VideoController _controller;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A StoryController object to call the method of controller</param>
        public EditPersonalDetailsPresenter([CreateNew] VideoController controller)
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

            //UserIsAdmin();

          

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


        //<summary>
        //This method will call the Story Controller class to get the State list for selected country
        //and populate the state drop down list on UI
        //</summary>
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
        /// This method will call the Video Controller class method to populate the tribute detail
        /// </summary>
        public void populateTributeDetail(int TributeId)
        {
            try
            {
                // int temp = 35872;
                //_controller.GetTributeFieldDetails(temp);

                Tributes objTrb = new Tributes();
                objTrb = _controller.GetEditTributeFieldDetails(TributeId);

                if (objTrb.TributeName != null)
                {                    
                    this.View.TributeName     = objTrb.TributeName;
                    if (!((objTrb.Date1.ToString() == null) || (objTrb.Date1.ToString() == "")))
                    {
                        DateTime DateofBirth = Convert.ToDateTime(objTrb.Date1.ToString());
                        this.View.Date1Day = DateofBirth.Day.ToString();
                        this.View.Date1Month = DateofBirth.Month.ToString();
                        this.View.Date1Year = DateofBirth.Year.ToString();
                        this.View.Age = CalculateAge(DateofBirth, Convert.ToDateTime(objTrb.Date2.ToString()));//(float.Parse(DateofDeath.Year.ToString()) - float.Parse(DateofBirth.Year.ToString())).ToString();
                    }
                    else
                    {
                        this.View.Date1Day = "0";
                        this.View.Date1Month = "0";
                        this.View.Date1Year = "";
                    }
                    DateTime DateofDeath      = Convert.ToDateTime(objTrb.Date2.ToString());
                    this.View.Date2Day        = DateofDeath.Day.ToString();
                    this.View.Date2Month      = DateofDeath.Month.ToString();
                    this.View.Date2Year       = DateofDeath.Year.ToString();                    
                    this.View.City            = objTrb.City;
                    this.View.Country         = objTrb.Country.ToString();
                    this.View.State           = objTrb.State.ToString();
                  

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// This method will call the Story Controller class method to update the tribute detail
        /// </summary>
        public void UpdateVideoTributeDetail(int tributeId, int userId)
        {
            try
            {
                _controller.UpdateVideoTributeDetail(CreateTributeDetailObject(tributeId,userId));
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
        private Tributes CreateTributeDetailObject(int TributeId,int UserId)
        {
            Tributes objTribute = new Tributes();

            try
            {
              
                objTribute.TributeId   = TributeId;
                objTribute.TributeName = View.TributeName;                

                if ((View.Date1Day != "0") && (View.Date1Month != "0") && (View.Date1Year != "") &&
                    (View.Date1Day != null) && (View.Date1Month != null) && (View.Date1Year != null))
                {
                    objTribute.Date1 = FormatDate(View.Date1Day, View.Date1Month, View.Date1Year);
                }
                if ((View.Date2Day != "0") && (View.Date2Month != "0") && (View.Date2Year != "") &&
                    (View.Date2Day != null) && (View.Date2Month != null) && (View.Date2Year != null))
                {
                    objTribute.Date2 = FormatDate(View.Date2Day, View.Date2Month, View.Date2Year);
                }

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
                    int value = 0;
                    objTribute.Country = value;
                }
                else
                {
                    objTribute.Country = int.Parse(View.Country);
                }
                objTribute.ModifiedBy = UserId;
                objTribute.ModifiedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objTribute;
        }
        /// <summary>
        /// This method will call the Story Controller class to get the Country list
        /// and populate the country drop down list on UI
        /// </summary>
        public void GetCountriesList()
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





        #endregion

        #region PRIVATE METHODS

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

        public int CalculateAge(DateTime DOB, DateTime DOD)
        {

            if (DOB !=null)
            {
                int YearsPassed = DOD.Year - DOB.Year;
                TimeSpan span = DOD.Subtract(DOB);
                int ageyears = span.Days / 365;
                return ageyears;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #endregion

    }
}
