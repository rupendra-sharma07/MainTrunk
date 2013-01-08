///Copyright       : Copyright (c) Optimus Information India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.TributePortalAdmin.Views.EnableRSSFeedPresenter.cs
///Author          : Laxman Hari Kulshrestha
///Creation Date   : 6:01 PM 3/29/2011


using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
namespace TributesPortal.TributePortalAdmin.Views
{
    public class EnableRSSFeedPresenter : Presenter<IEnableRSSFeed>
    {
        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        private TributePortalAdminController _controller;
        public EnableRSSFeedPresenter([CreateNew] TributePortalAdminController controller)
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

        /// <summary>
        /// LHK:Method to Enable RSS feed 
        /// </summary>
        /// <param name="objUser">Filled Users entity.</param>
        public void EnableRSSFeedForBussUser()
        {
            EnableRSSFeedInfo objEnableRSSFeedInfo = new EnableRSSFeedInfo();
            objEnableRSSFeedInfo = _controller.EnableRSSFeedForBussUser(GetRSSObject());
            this.View.UserId = objEnableRSSFeedInfo.UserId;
            this.View.UpdateOutput = objEnableRSSFeedInfo.UpdateOutput;
           
        }

        private EnableRSSFeedInfo GetRSSObject()
        {
            EnableRSSFeedInfo objEnableRSSFeedInfo = new EnableRSSFeedInfo();
            objEnableRSSFeedInfo.UserId = this.View.UserId;
            objEnableRSSFeedInfo.UserName = this.View.UserName;
            objEnableRSSFeedInfo.AtomEnabled = this.View.AtomEnabled;
            objEnableRSSFeedInfo.EnableXMLFeed = this.View.EnableXMLFeed;
            return objEnableRSSFeedInfo;
        }

        public void EnableXmlFeedForBussUser()
        {
            EnableRSSFeedInfo objEnableRSSFeedInfo = new EnableRSSFeedInfo();
            objEnableRSSFeedInfo = _controller.EnableXmlFeedForBussUser(GetRSSObject());
            this.View.UserId = objEnableRSSFeedInfo.UserId;
            this.View.UpdateOutput = objEnableRSSFeedInfo.UpdateOutput;
        }
    }
}
