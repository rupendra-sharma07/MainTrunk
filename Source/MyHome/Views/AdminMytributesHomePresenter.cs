///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.Modules.Myhome.AdminMytributesHome.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page helps to display all the tributes that a user has created
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
    public class AdminMytributesHomePresenter : Presenter<IAdminMytributesHome>
    {


        private MyHomeController _controller;
        public AdminMytributesHomePresenter([CreateNew] MyHomeController controller)
        {
            _controller = controller;
        }
        public void onload(int startindex, int maxrecord, string applicationType)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param = { objtribute, 0, startindex, maxrecord };
            if (objtribute.CustomError == null)
            {
                GetTributeLists(applicationType);
                View.Mytributes_ = _controller.GetMyTributes(param);
                loadfavourites();
            }
        }

        public void loadfavourites()
        {
            //GetMyTributes objtribute = new GetMyTributes();
            //objtribute.UserId = View.UserId;
            //object[] param ={ objtribute, 0, 1, 1 };
            //if (objtribute.CustomError == null)
            //{
            //    List<GetMyTributes> MyFavourites = new List<GetMyTributes>();
            //    MyFavourites = _controller.GetMyFavourites(param);
            //    if (MyFavourites.Count > 0)
            //        View.myfavourite = true;
            //    else
            //        View.myfavourite = false;
            //}
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
                if (WebConfig.ApplicationType.ToLower().Equals("yourtribute"))
                    objTributeTypes.Add(new ParameterTypesCodes("", 1, "All Tributes"));
                else
                    objTributeTypes.Add(new ParameterTypesCodes("", 1, "All Websites"));

                for (int i = 0; i < objBusTypes.Count; i++)
                {
                    onjTypeCode = new ParameterTypesCodes();
                    onjTypeCode.TypeCode = objBusTypes[i].TypeCode;
                    onjTypeCode.TypeDescription = objBusTypes[i].TypeDescription;
                    objTributeTypes.Add(onjTypeCode);
                }
            }
            View.TributeTypes = objTributeTypes;

        }

        /// <summary>
        /// gets the list of tributes created by a user based on the type of tribute selected by the user
        /// </summary>
        /// <param name="TributeTypeId"></param>
        /// <param name="startindex"></param>
        /// <param name="maxrecord"></param>
        public void TributeByType(int TributeTypeId, int startindex, int maxrecord)
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param = { objtribute, TributeTypeId, startindex, maxrecord };
            if (objtribute.CustomError == null)
            {
                //GetTributeLists();
                View.Mytributes = _controller.GetMyTributes(param);
            }

        }
        public void UpdateEmailAlerts(int tributeid, bool emailalert)
        {
            Tributes objTribute = new Tributes();
            objTribute.UserTributeId = View.UserId;
            objTribute.TributeId = tributeid;
            objTribute.IsActive = emailalert;
            object[] _tribute = { objTribute };
            _controller.UpdateEmailAlerts(_tribute);
        }

        /// <summary>
        /// return the count of tributes created by a user
        /// </summary>
        /// <param name="tribuyetrype"></param>
        public void GetMyTributeCount(int tribuyetrype)
        {
            TributeVisitCount objcount = new TributeVisitCount();
            objcount.SectionTypeID = View.UserId;
            objcount.SectionTypeCodeId = tribuyetrype;
            object[] param = { objcount };
            this._controller.GetMyTributeCount(param);
            if (objcount.CustomError == null)
            {
                View.TotalCount = objcount.Count;
            }
        }

        public bool IsTributesExists(int TributeId)
        {
            return this._controller.IsTributeExists(TributeId);
        }
        //AG:
        public void UpdateTributePackage(int tributeId, string tributePackageType)
        {
            this._controller.UpdateTributePackage(tributeId, tributePackageType);
        }
    }
}
