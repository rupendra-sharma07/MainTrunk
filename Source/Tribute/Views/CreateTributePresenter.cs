///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.CreateTributePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for creating a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;

namespace TributesPortal.Tribute.Views
{
    public class CreateTributePresenter : Presenter<ICreateTribute>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private TributeController _controller;
        public CreateTributePresenter([CreateNew] TributeController controller)
        {
            _controller = controller;
        }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }
        public void TributeLoad(Tributes tributes)
        {
            View.Tributes = _controller.TributeList(tributes);
        }
        //public void TemplateLoad(Templates templates)
        //{
        //    View.Templates = _controller.TemplatesList(templates);
        //}
        public void CountryLoad(Locations countries)
        {
            View.Countries = _controller.CountryList(countries);
        }
        public void StateLoad(Locations states)
        {
            View.States = _controller.StateList(states);
        }
        public object CreateTribute(Tributes tributes)
        {
            return _controller.CreateTribute(tributes);
        }
        public string CheckAvailability(string strUrlFinal, int tributetype)
        {
            return _controller.CheckUrlExists(strUrlFinal,tributetype);
        }
        public void BindThemes(string strThemeId)
        {
            View.ThemeNames = _controller.BindTheme(strThemeId);
        }
        public override void OnViewInitialized()
        {
            // TODO: Implement code that will be executed the first time the view loads
        }
      

        // TODO: Handle other view events and set state in the view
    }
}




