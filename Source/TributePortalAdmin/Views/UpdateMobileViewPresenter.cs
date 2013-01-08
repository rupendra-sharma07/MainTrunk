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
    public class UpdateMobileViewPresenter : Presenter<IUpdateMobileView>
    {
        private TributePortalAdminController _controller;
        public UpdateMobileViewPresenter([CreateNew] TributePortalAdminController controller)
        {
            _controller = controller;
        }
        public void UpdateMObiteView()
        {
            this.View.Error = _controller.UpdateUserTributeMobileView(GetUserObject());
        }

        private Users GetUserObject()
        {
            Users ObjUsers = new Users();
            ObjUsers.UserId = this.View.UserId; 
            ObjUsers.UserName = this.View.UserName; 
            ObjUsers.IsMobileViewOn = this.View.IsMObileViewOn;
            return ObjUsers;

        }
    }
}
