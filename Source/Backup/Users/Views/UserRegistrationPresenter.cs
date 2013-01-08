///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.UserRegistrationPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for allowing a user to register on the site.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Users.Views
{
    public class UserRegistrationPresenter : Presenter<IUserRegistration>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
         private UsersController _controller;
         public UserRegistrationPresenter([CreateNew] UsersController controller)
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

        public void UserAvailability(UserRegistration objUserRegistration)
        {
            _controller.UserAvailability(objUserRegistration);
            View.UserAvailablity = objUserRegistration.Users.UserName.ToString();
                
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int EmailAvailable()
        {
            return _controller.EmailAvailable(View.UserEmail,View.ApplicationType);
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
            View.Locations = objList_; 

           // View.Locations = _controller.CountryList(location);
        }
        public void BusinessTypes()
        {
            View.BusinessType = _controller.BusinessTypes();
        }
        public void OnStateLoad(Locations location)
        {
            View.States = _controller.CountryList(location);
        }
        public object SavePersonalAccount(UserRegistration objUserRegistration)
        {
           return _controller.SavePersonalAccount(objUserRegistration);
        }

        // TODO: Handle other view events and set state in the view
    }
}




