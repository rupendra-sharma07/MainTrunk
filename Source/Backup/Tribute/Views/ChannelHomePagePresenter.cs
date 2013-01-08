///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ChannelHomePagePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for displaying the channel home pages.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.Tribute;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Tribute.Views
{
    public class ChannelHomePagePresenter : Presenter<IChannelHomePage>
    {
         private TributeController _controller;
        public ChannelHomePagePresenter([CreateNew] TributeController controller)
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

            //to get the list of featured tributes
          //  FeaturedTribute objFeaturedTribute = null;
          //  View.FeaturedTributes = _controller.GetFeaturedTributes(objFeaturedTribute);
        }

        /// <summary>
        /// This method will call the AllTribute Manager class method for getting the most popular tribute
        /// on the basis on number of hits
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// most popular tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the popular tribute list</returns>
        public void GetPopularTribute(string TributeName)
        {
            int tributeType = _controller.GetTributeIdCode(TributeName);
            View.FeaturedTributes = _controller.GetPopularTribute(tributeType);
        }
    }
}
