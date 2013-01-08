///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Miscellaneous.Views.ChangeSiteThemePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Change Site Theme.
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
#endregion

namespace TributesPortal.Miscellaneous.Views
{
    public class ChangeSiteThemePresenter : Presenter<IChangeSiteTheme>
    {

        // NOTE: Uncomment the following code if you want ObjectBuilder to inject the module controller
        //
        private MiscellaneousController _controller;
        public ChangeSiteThemePresenter([CreateNew] MiscellaneousController controller)
        {
            _controller = controller;
        }

        public override void OnViewLoaded()
        {
            // TODO: Implement code that will be executed every time the view loads
        }

        public override void OnViewInitialized()
        {
            LoadData();
            GetExistingTheme();
            // TODO: Implement code that will be executed the first time the view loads
        }

        // TODO: Handle other view events and set state in the view

        /// <summary>
        /// Method to load the list of themes based on tribute type
        /// </summary>
        public void LoadData()
        {
            Templates objTributeType = new Templates();
            objTributeType.TributeType = this.View.ThemeType;
            this.View.ThemesList = _controller.GetThemesList(objTributeType);
        }

        /// <summary>
        /// Method to get the theme for tribute
        /// </summary>
        public void GetExistingTheme()
        {
            Tributes objTribute = new Tributes();
            objTribute.TributeId = this.View.TributeId;
            this.View.ExistingTheme = _controller.GetThemeForTribute(objTribute).TemplateID;
        }

        /// <summary>
        /// Method to update tribute theme
        /// </summary>
        public void UpdateTributeTheme()
        {
            Tributes objTribute = new Tributes();
            objTribute.TributeId = this.View.TributeId;
            objTribute.ThemeId = this.View.ThemeId;
            objTribute.ModifiedBy = this.View.ModifiedBy;
            objTribute.ModifiedDate = this.View.ModifiedDate;

            _controller.UpdateTributeTheme(objTribute);

        }
    }
}




