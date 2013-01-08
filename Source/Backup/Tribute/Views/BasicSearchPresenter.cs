///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.BasicSearchPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for deploying a basic search for a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

namespace TributesPortal.Tribute.Views
{
    public class BasicSearchPresenter : Presenter<IBasicSearch>
    {

        private TributeController _controller;

        public BasicSearchPresenter([CreateNew] TributeController controller)
        {
            _controller = controller;
        }

        // Implement code that will be executed every time the view loads
        public override void OnViewLoaded()
        {
            
        }

        // Implement code that will be executed the first time the view loads
        public override void OnViewInitialized()
        {
            // Get the Tribute type list
            //GetTributeTypeList();
        }

        public void GetTributeTypeList(string applicationType)
        {
            View.TributeTypeList = _controller.GetTributeTypeList(applicationType);
        }
    }
}




