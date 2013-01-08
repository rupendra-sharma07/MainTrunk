///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.UserInboxPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This page helps to display all the messages sent to the user inbox on the site
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
//using TributesPortal.ResourceAccess;
//using System.Data;
using TributesPortal.MultipleLangSupport;

namespace TributesPortal.MyHome.Views
{
    public class UserInboxPresenter : Presenter<IUserInbox>
    {
        private MyHomeController _controller;
        public UserInboxPresenter([CreateNew] MyHomeController controller)
        {
            _controller = controller;
        }

        public void GetMailMessage(int startindex,int maxcount)
        {
            object[] objValue ={ View.UserId, startindex,maxcount};
            View.UserInbox = _controller.GetMailMessage(objValue);
            loadfavourites();
            loadmytributes();
        }

        public IList<MailMessage> GetMailThread(int Parantid)
        {
            object[] objValue ={ Parantid };
            return _controller.GetMailThread(objValue);
        }


        public void GetuserSentMessages(int startindex, int maxcount)
        {
            object[] objValue ={ View.UserId, startindex, maxcount };
            View.UserSentItem = _controller.GetuserSentMessages(objValue);            
        }
        public void UpdateMessageStstus(string SeletedValues, int status)
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



        public void DeleteSentMessages(string SeletedValues, bool isdeleted)
        {
            MailMessage objmessage = new MailMessage();
            objmessage.IsDeleted = isdeleted;
            objmessage.ModifiedBy = View.UserId;
            object[] param ={ objmessage, SeletedValues };
            _controller.DeleteSentMessages(param);
        }

        public void SendMail()
        {
            _controller.SendMail(View.UserId, View.ToUserId, View.Subject, View.PostMessage);
        }
        public void SendMail_(int Messageid)
        {
            _controller.SendMailReply(View.UserId, View.SendbyUserId, View.Subject1, View.EmailBody, Messageid);
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

        public void loadmytributes()
        {
            GetMyTributes objtribute = new GetMyTributes();
            objtribute.UserId = View.UserId;
            object[] param ={ objtribute, 0, 1, 1 };
            if (objtribute.CustomError == null)
            {       
                List<GetMyTributes> Mytributes=new List<GetMyTributes>();
                Mytributes = _controller.GetMyTributes(param);
                if (Mytributes.Count > 0)
                    View.mytribute = true;
                else
                    View.mytribute = false;
            }
        }

        public void GetuserInboxTotalCount()
        {
            TributeVisitCount objcount = new TributeVisitCount();
            objcount.SectionTypeCodeId = View.UserId;
            object[] param ={ objcount };
            this._controller.GetuserInboxTotalCount(param);
            if (objcount.CustomError == null)
            {
                View.TotalCount = objcount.Count;
            }
        }

        public void GetuserSentMessagesCount()
        {
            TributeVisitCount objcount = new TributeVisitCount();
            objcount.SectionTypeCodeId = View.UserId;
            object[] param ={ objcount };
            this._controller.GetuserSentMessagesCount(param);
            if (objcount.CustomError == null)
            {
                View.TotalCount_ = objcount.Count;
            }
        }
        public void GetUserProfile(string _userid)
        {
            UserProfile objprofile = new UserProfile();
            objprofile.UserId = int.Parse(_userid);
            object[] param ={ objprofile };
            this._controller.GetUserProfile(param);
            //if (objprofile.State.Equals(""))
            //    View.UserAddress = "(" + objprofile.City + ", " + objprofile.Country + ")";
            //else
            //    View.UserAddress = "(" + objprofile.City +", "+ objprofile.State +" ," + objprofile.Country + ")";

            if (objprofile.City == string.Empty && objprofile.State == string.Empty)
                View.UserAddress = "(" + objprofile.Country + ")";
            else
                if (objprofile.City == string.Empty && objprofile.State != string.Empty)
                    View.UserAddress = "(" + objprofile.State + ", " + objprofile.Country + ")";
                else if (objprofile.City != string.Empty && objprofile.State == string.Empty)
                    View.UserAddress = "(" + objprofile.City + ", " + objprofile.Country + ")";
                else
                    View.UserAddress = "(" + objprofile.City + ", " + objprofile.State + ", " + objprofile.Country + ")";
        }

        public string GetUserAddress(string _userid)
        {
            string UserAddress = string.Empty;
            UserProfile objprofile = new UserProfile();
            objprofile.UserId = int.Parse(_userid);
            object[] param ={ objprofile };
            this._controller.GetUserProfile(param);
            //if (objprofile.State.Equals(""))
            //    UserAddress = "(" + objprofile.City + ", " + objprofile.Country + ")";
            //else
            //    UserAddress = "(" + objprofile.City + ", " + objprofile.State + ", " + objprofile.Country + ")";

            if (objprofile.City == string.Empty && objprofile.State == string.Empty)
                UserAddress = "(" + objprofile.Country + ")";
            else
                if (objprofile.City == string.Empty && objprofile.State != string.Empty)
                    UserAddress = "(" + objprofile.State + ", " + objprofile.Country + ")";
                else if (objprofile.City != string.Empty && objprofile.State == string.Empty)
                    UserAddress = "(" + objprofile.City + ", " + objprofile.Country + ")";
                else
                    UserAddress = "(" + objprofile.City + ", " + objprofile.State + ", " + objprofile.Country + ")";

            return UserAddress;


        }
        

        
    }
}
