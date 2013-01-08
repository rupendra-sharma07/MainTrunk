///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.TributeSummaryReportPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing the summary reports by the portal admin.
///Audit Trail     : Date of Modification  Modified By         Description
#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.TributePortalAdmin.Views
{
    public class TributeSummaryReportPresenter : Presenter<ITributeSummaryReport>
    {
        #region CLASS VARIABLES
        private TributePortalAdminController _controller;
        #endregion

        #region CONSTRUCTOR
        public TributeSummaryReportPresenter([CreateNew] TributePortalAdminController controller)
        {
            _controller = controller;
        }
        #endregion

        #region METHODS
        
        /// <summary>
        /// Method to get the list of country.
        /// </summary>
        /// <param name="location">Location Entity</param>
        public void OnCountryLoad(Locations location)
        {
            List<Locations> objList = new List<Locations>();
            List<Locations> objList_ = new List<Locations>();
            objList = (List<Locations>)_controller.CountryList(location);
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
                    Locations objloc = new Locations();
                    objloc.LocationId = objList[i].LocationId;
                    objloc.LocationName = objList[i].LocationName;
                    objloc.LocationParentId = objList[i].LocationParentId;
                    objList_.Add(objloc);
                }
            }
            this.View.Locations = objList_;

            // View.Locations = _controller.CountryList(location);
        }

        /// <summary>
        /// Method to load list of states based on the selected country.
        /// </summary>
        /// <param name="location">Location entity.</param>
        public void OnStateLoad(Locations location)
        {
            this.View.States = _controller.CountryList(location);
        }

        /// <summary>
        /// Method to get the list of tributes based on the selection criteria.
        /// </summary>
        /// <param name="objTribute">TributeSearch entity containting searcg criteria. </param>
        public void SearchTribute(TributeSearch objTribute)
        {

            List<TributeSearch> objTributeList = this._controller.SearchTributes(objTribute);
            foreach (TributeSearch obj in objTributeList)
            {
                if (!obj.IsActive)
                    obj.TributeStatus = "Expired";
            }
            this.View.TributeList = objTributeList;
        }

        /// <summary>
        /// Method to delete tribute.
        /// </summary>
        /// <param name="tributeId">Tribute Id</param>
        /// <param name="userId">User Id</param>
        public void DeleteTribute(int tributeId, int userId)
        {
            Tributes objTribute = new Tributes();
            objTribute.TributeId = tributeId;
            objTribute.UserTributeId = userId;

            this._controller.DeleteTribute(objTribute);
        }
        #endregion
    }
}




