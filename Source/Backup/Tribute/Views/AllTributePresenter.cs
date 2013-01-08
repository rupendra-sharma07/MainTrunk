///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.AllTributePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the All Trubute ( Recently Created Tribute and Most Popular Tribute).
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;

#endregion

namespace TributesPortal.Tribute.Views
{
    public class AllTributePresenter : Presenter<IAllTribute>
    {

        #region CLASS VARIABLES
        
        private TributeController _controller;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// This is the constructor of the class
        /// </summary>
        /// <param name="controller">A TributeController object to call the method of controller</param>
        public AllTributePresenter([CreateNew] TributeController controller)
         {
         	_controller = controller;
         }

         #endregion


        #region PUBLIC METHODS

         /// <summary>
         /// This method will call every time the view loads
         /// </summary>
        public override void OnViewLoaded()
        {
        }

        /// <summary>
        /// This method will call the first time the view loads
        /// </summary>
        public override void OnViewInitialized()
        {
        }

        /// <summary>
        /// This method will call the AllTribute Manager class method for getting the Recently created tribute
        /// on the basis of last created tribute
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// Recently created tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the recently created tribute list</returns>
        public void GetRecentlyCreatedTribute(int tributeType)
        {
            View.SearchTributesList = _controller.GetRecentlyCreatedTribute(tributeType);
        }

        /// <summary>
        /// This method will call the AllTribute Manager class method for getting the most popular tribute
        /// on the basis on number of hits
        /// </summary>
        /// <param name="tributeType">A int object which contain the tribute type for which we want to get the 
        /// most popular tribute. by default it will be 1 ( for All Tribute)</param>
        /// <returns>This method will return the recently created tribute list</returns>
        public void GetPopularTribute(int tributeType)
        {
            View.SearchTributesList = _controller.GetPopularTribute(tributeType);
        }

        #endregion
    }
}




