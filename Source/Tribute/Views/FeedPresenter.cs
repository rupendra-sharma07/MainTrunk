///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.Tribute.Views.TributeCreationPresenter.cs
///Author          : Laxman Hari Kulshrestha
///Creation Date   : 3:13 PM 3/30/2011
///Description     : This is the Presenter class for creating a tribute.
///Audit Trail     : Date of Modification  Modified By         Description

using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
namespace TributesPortal.Tribute.Views
{
    public class FeedPresenter: Presenter<IFeed>
    {
        private TributeController _controller;
        public FeedPresenter([CreateNew] TributeController controller)
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

        //public void GetMyTributes(object[] param)
        //{
        //    //IList<GetMyTributes> ObjTrbList = new List<GetMyTributes>();
        //    //ObjTrbList = (List<GetMyTributes>)_controller.GetMyTributes(param);
        //    View.ObjTributeList = _controller.GetMyTributes(param);
        //    //return ObjTrbList;
        //}

        public void GetTributesFeed(object[] param)
        {
            View.ObjTributeList = _controller.GetTributesFeed(param);
        }
        public Users GetUserDetailsOnUserId(int userId)
        {
            return _controller.GetUserDetailsOnUserId(userId);
        }

        public void GetYourTributeFeedOnTributeName(object[] objprm)
        {
            View.ObjTributeList = _controller.GetYourTributeFeedOnTributeName(objprm);
        }

        public void GetYourTributesFeed(object[] objparam)
        {
            View.ObjTributeList = _controller.GetYourTributesFeed(objparam);
        }

        public int GetTotalActiveObituaries(int _businessUserId)
        {
            return _controller.GetTotalActiveObituaries(_businessUserId);
        }

        public int GetTotalActiveObituariesOnTributeName(object[] objprm)
        {
            return _controller.GetTotalActiveObituariesOnTributeName(objprm);
        }
    }
}
