///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.UserEventsPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This page helps to display all the Events that a user has been invited to
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;

namespace TributesPortal.MyHome.Views
{
    public class UserEventsPresenter : Presenter<IUserEvents>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private MyHomeController _controller;
        public UserEventsPresenter([CreateNew] MyHomeController controller)
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
        public void GetUserEvevts(int startindex, int maxcount)
        {
            Events onjevents = new Events();
            onjevents.UserId = View.UserId;
            object[] objvalue ={ onjevents, startindex, maxcount };
            View.MyEvents = _controller.GetMyEvents(objvalue);
            //if (View.MyEvents.Count > maxcount)
            //    GetUsereventsCount();
        }

        public void GetUsereventsCount()
        {
            TributeVisitCount objcount = new TributeVisitCount();
            objcount.SectionTypeCodeId = View.UserId;
            object[] param ={ objcount };
            this._controller.GetUsereventsCount(param);
            if (objcount.CustomError == null)
            {
                View.TotalCount = objcount.Count;
            }
        }

        /// <summary>
        /// Method to get tribute details based on the tributeid.
        /// </summary>
        /// <param name="tributeId"></param>
        public void GetTributeDetails(int tributeId)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = 0;//View.UserId;
            object[] param ={ objtribute, tributeId };
            if (objtribute.CustomError == null)
            {
                View.TributeDetail = _controller.GetMyTribute(param, "Tribute");
            }
        }

    }
}




