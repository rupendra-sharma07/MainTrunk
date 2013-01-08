///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.AdminMyfavoritesPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This page helps to display all the tributes that a user has added to his favourites
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
//using TributesPortal.Users;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;

namespace TributesPortal.MyHome.Views
{
    public class AdminMyfavoritesPresenter : Presenter<IAdminMyfavorites>
    {
        private MyHomeController _controller;
        public AdminMyfavoritesPresenter([CreateNew] MyHomeController controller)
        {
            _controller = controller;
        }
        public void FavouriteType2(int TributeTypeId, int startindex, int maxcount)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute, TributeTypeId, startindex, maxcount };
            if (objtribute.CustomError == null)
            {
                //GetTributeLists();
                View.MyFavourites = _controller.GetMyFavourites(param);
            }

        }

        public void loadmytributes()
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute, 0,1,1 };
            if (objtribute.CustomError == null)
            {
                List<GetMyTributes> Mytributes = new List<GetMyTributes>();
                Mytributes = _controller.GetMyTributes(param);
                if (Mytributes.Count > 0)
                    View.mytribute = true;
                else
                    View.mytribute = false;
            }
        }

        public void loadfavourites(int startindex, int maxcount, string applicationType)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute, 0, startindex, maxcount };
            if (objtribute.CustomError == null)
            {
                GetTributeLists2(applicationType);
                View.MyFavourites_ = _controller.GetMyFavourites(param);
                loadmytributes();
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
        public void UpdateFavouriteEmailAlert(int tributeid, bool emailalert)
        {
            Tributes objTribute = new Tributes();
            objTribute.UserTributeId = View.UserId;
            objTribute.TributeId = tributeid;
            objTribute.IsActive = emailalert;
            object[] _tribute ={ objTribute };
            _controller.UpdateFavouriteEmailAlert(_tribute);
        }

        public void DeleteMyFavourite(int tributeid, bool IsDeleted)
        {
            Tributes objTribute = new Tributes();
            objTribute.UserTributeId = View.UserId;
            objTribute.TributeId = tributeid;
            objTribute.IsDeleted = IsDeleted;
            object[] _tribute ={ objTribute };
            _controller.DeleteMyFavourite(_tribute);
        }

        public void SendMail()
        {
            _controller.SendMail(View.UserId, View.ToUserId, View.Subject, View.PostMessage);
        }

        public void GetMyFavouritesCount(int tributetype)
        {
            TributeVisitCount objcount = new TributeVisitCount();
            objcount.SectionTypeCodeId = View.UserId;
            objcount.SectionTypeID = tributetype;
            object[] param ={ objcount };
            this._controller.GetMyFavouritesCount(param);
            if (objcount.CustomError == null)
            {
                View.TotalCount = objcount.Count;
            }
        }
    }
}
