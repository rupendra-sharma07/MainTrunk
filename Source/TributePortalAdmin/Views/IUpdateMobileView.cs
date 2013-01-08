
using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;

namespace TributesPortal.TributePortalAdmin.Views
{
    public interface IUpdateMobileView
    {
        int UserId { get; }
        string UserName { get; }
        bool IsMObileViewOn { get; }
        bool Error { set; }
    }
}
