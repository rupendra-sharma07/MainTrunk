///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.AdvanceSearchPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for Advance Search.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;

#endregion

namespace TributesPortal.Tribute.Views
{
    public class AdvanceSearchPresenter : Presenter<IAdvanceSearch>
    {

        #region CLASS VARIABLES

        private TributeController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A TributeController object to call the method of controller</param>
        public AdvanceSearchPresenter([CreateNew] TributeController controller)
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
        }

        /// <summary>
        /// This method will call the first time the view loads
        /// </summary>
        public override void OnViewInitialized()
        {
            // Get the All country list
            GetCountryList(null);

            // Get the Tribute type list
            //GetTributeTypeList(applicationType);
        }

        /// <summary>
        /// This method will call the SearchTribute Controller class method for getting the list of country
        /// </summary>
        /// <param name="location"></param>
        public void GetCountryList(string location)
        {
          //  View.Country = _controller.GetCountryList(Locationid(location));

            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();
            objList = (List<Locations>)_controller.GetCountryList(Locationid(location));
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
            View.Country = objList_; 
        }

        /// <summary>
        /// This method will call the SearchTribute Controller class method for getting the list of cstate
        /// </summary>
        /// <param name="location"></param>
        public void GetStateList(string location)
        {
            View.State = _controller.GetCountryList(Locationid(location));
        }

        /// <summary>
        /// This method will call the SearchTribute Controller class method for getting the list of tribute type
        /// </summary>
        public void GetTributeTypeList(string applicationType)
        {
            View.TributeTypeList = _controller.GetTributeTypeList(applicationType);
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// This Function will set the Parent ID in the Location 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
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

        #endregion

        #endregion

    }
}
