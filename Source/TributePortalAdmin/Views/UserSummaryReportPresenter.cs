///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.UserSummaryReportPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing the user summary reports by the portal admin.
///Audit Trail     : Date of Modification  Modified By         Description
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public class UserSummaryReportPresenter : Presenter<IUserSummaryReport>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        
         private TributePortalAdminController _controller;
         public UserSummaryReportPresenter([CreateNew] TributePortalAdminController controller)
         {
         		_controller = controller;
         }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }

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
            View.Locations = objList_;

            // View.Locations = _controller.CountryList(location);
        }
        public void OnStateLoad(Locations location)
        {
            View.States = _controller.CountryList(location);
        }

        /// <summary>
        /// Method to get the list of users
        /// </summary>
        /// <param name="objUser">Filled Users entity.</param>
        public void SearchUsers(Users objUser)
        {
            this.View.UsersList = _controller.SearchUsers(objUser);
        }

        /// <summary>
        /// Method to delete users.
        /// </summary>
        /// <param name="userId">User entity containing userid to be deleted.</param>
        public void DeleteUsers(int userId)
        {
            Users objUser = new Users();
            objUser.UserId = userId;
            objUser.IsDeleted = false; //changed to false 05-Feb-09 -- ANKI
            try
            {
                this._controller.DeleteUser(objUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}




