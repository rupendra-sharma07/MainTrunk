///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       :TributesPortal.Users.Views.MyHomePresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for viewing the my home page.
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;

namespace TributesPortal.Users.Views
{
    public class MyHomePresenter : Presenter<IMyHome>
    {

        private UsersController _controller;
        public MyHomePresenter([CreateNew] UsersController controller)
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

        public void onload(string applicationType)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute,0};
            if (objtribute.CustomError == null)
            {
                GetTributeLists(applicationType);
                View.Mytributes = _controller.GetMyTributes(param);
            }
        }
        public void TributeByType(int TributeTypeId)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute, TributeTypeId };
            if (objtribute.CustomError == null)
            {
                //GetTributeLists();
                View.Mytributes = _controller.GetMyTributes(param);
            }
        
        }
        public void FavouriteType2(int TributeTypeId)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute, TributeTypeId };
            if (objtribute.CustomError == null)
            {
                //GetTributeLists();
                View.MyFavourites = _controller.GetMyFavourites(param);
            }

        }
        public void loadfavourites(string applicationType)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute, 0 };
            if (objtribute.CustomError == null)
            {
                GetTributeLists2(applicationType);
                View.MyFavourites = _controller.GetMyFavourites(param);
            }
        }

        public void GetTributeLists2(string applicationType)
        {
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            List<ParameterTypesCodes> objTributeTypes = new List<ParameterTypesCodes>();
            objBusTypes = _controller.GetListofTributes(applicationType);
            ParameterTypesCodes onjTypeCode;
            if (objBusTypes.Count > 0)
            {
                onjTypeCode = new ParameterTypesCodes();
                onjTypeCode.TypeCode = 0;
                onjTypeCode.TypeDescription = "Just Show Me...";
                objTributeTypes.Add(onjTypeCode);
                for (int i = 0; i < objBusTypes.Count; i++)
                {
                    onjTypeCode = new ParameterTypesCodes();
                    onjTypeCode.TypeCode = objBusTypes[i].TypeCode;
                    onjTypeCode.TypeDescription = objBusTypes[i].TypeDescription;
                    objTributeTypes.Add(onjTypeCode);
                }
            }
            View.TributeTypes2 = objTributeTypes;

        }

        public void GetTributeLists(string applicationType)
        {            
            List<ParameterTypesCodes> objBusTypes = new List<ParameterTypesCodes>();
            List<ParameterTypesCodes> objTributeTypes = new List<ParameterTypesCodes>();
            objBusTypes = _controller.GetListofTributes(applicationType);
            ParameterTypesCodes onjTypeCode;
            if (objBusTypes.Count > 0)
            {
                onjTypeCode = new ParameterTypesCodes();
                onjTypeCode.TypeCode = 0;
                onjTypeCode.TypeDescription = "Just Show Me...";
                objTributeTypes.Add(onjTypeCode);
                for(int i = 0; i < objBusTypes.Count; i++)
                {
                    onjTypeCode = new ParameterTypesCodes();
                    onjTypeCode.TypeCode = objBusTypes[i].TypeCode;
                    onjTypeCode.TypeDescription = objBusTypes[i].TypeDescription;
                    objTributeTypes.Add(onjTypeCode);
                }
            }
            View.TributeTypes = objTributeTypes;

        }

        public void UpdateEmailAlerts(int tributeid,bool emailalert)
        {
            Tributes objTribute = new Tributes();
            objTribute.UserTributeId=View.UserId;
            objTribute.TributeId = tributeid;
            objTribute.IsActive = emailalert;
            object[] _tribute ={ objTribute };
            _controller.UpdateEmailAlerts(_tribute);
        }
        public void UpdateFavouriteEmailAlert(int tributeid, bool emailalert)
        {
            Tributes objTribute = new Tributes();
            objTribute.UserTributeId=View.UserId;
            objTribute.TributeId = tributeid;
            objTribute.IsActive = emailalert;
            object[] _tribute ={ objTribute };
            _controller.UpdateFavouriteEmailAlert(_tribute);
        }
        
        public void DeleteMyFavourite(int tributeid, bool IsDeleted)
        {
            Tributes objTribute = new Tributes();
            objTribute.UserTributeId=View.UserId;
            objTribute.TributeId = tributeid;
            objTribute.IsDeleted = IsDeleted;
            object[] _tribute ={ objTribute };
            _controller.DeleteMyFavourite(_tribute);
        }
        public void GetuserSentMessages()
        { 
            object[] objValue ={ View.UserId };
            View.UserSentItem = _controller.GetuserSentMessages(objValue);        
        }

        public void  GetMailMessage()
        {
            object[] objValue ={ View.UserId };
            View.UserInbox = _controller.GetMailMessage(objValue);
        }
        public void UpdateMessageStstus(string SeletedValues,int status)
        {
            MailMessage objmessage = new MailMessage();
            objmessage.Status = status;
            objmessage.ModifiedBy = View.UserId;
            object[] param ={ objmessage, SeletedValues };
            _controller.UpdateMessageStstus(param);            
        }
        public void DeleteMessages(string SeletedValues, bool isdeleted)
        {
            MailMessage objmessage = new MailMessage();
            objmessage.IsDeleted = isdeleted;
            objmessage.ModifiedBy = View.UserId;
            object[] param ={ objmessage, SeletedValues };
            _controller.DeleteMessages(param);            
        }
        public void GetUserEvevts()
        {
            Events onjevents=new Events();
            onjevents.UserId=View.UserId;
            object[] objvalue ={ onjevents };
            View.MyEvents = _controller.GetMyEvents(objvalue);
        }
        //GetMyEvents

        public void SendMail()
        {

            _controller.SendMail(View.UserId, View.SendbyUserId, View.Subject, View.EmailBody);
        }
        
    }
}
