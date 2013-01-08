///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.ChannelHomePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for displaying the channel homepage.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.Tribute;
using TributesPortal.BusinessEntities;
/// <summary>
///Tribute Portal-Channel Home Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.
/// </summary>

namespace TributesPortal.Tribute.Views
{
    public class ChannelHomePresenter : Presenter<IChannelHome>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private TributeController _controller;
        public ChannelHomePresenter([CreateNew] TributeController controller)
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
            FeaturedTribute objFeaturedTribute = null;
            View.FeaturedTributes = _controller.GetFeaturedTributes(objFeaturedTribute);
        }


        // TODO: Handle other view events and set state in the view

    }
}




