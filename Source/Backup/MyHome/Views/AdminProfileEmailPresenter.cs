///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.MyHome.Views.AdminProfileEmailPresenter.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Presenter class for the Admin Profile Email Settings.
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
    public class AdminProfileEmailPresenter : Presenter<IAdminProfileEmail>
    {
        private MyHomeController _controller;


        public AdminProfileEmailPresenter([CreateNew] MyHomeController controller)
        {
            _controller = controller;
        }
        public void SaveEmailNotification()
        {
            UserRegistration _objUserReg = new UserRegistration();
            EmailNotification _objEmaNoti = new EmailNotification();
            _objEmaNoti.UserId = View.UserId;
            _objEmaNoti.StoryNotify = View.StoryNotify;
            _objEmaNoti.NotesNotify = View.NotesNotify;
            _objEmaNoti.EventsNotify = View.EventsNotify;
            _objEmaNoti.GuestBookNotify = View.GuestBookNotify;
            _objEmaNoti.GiftsNotify = View.GiftsNotify;
            _objEmaNoti.PhotosNotify = View.PhotosNotify;
            _objEmaNoti.PhotoAlbumNotify = View.PhotoAlbumNotify;
            _objEmaNoti.VideosNotify = View.VideosNotify;
            _objEmaNoti.CommentsNotify = View.CommentsNotify;
            _objEmaNoti.MessagesNotify = View.MessagesNotify;
            _objEmaNoti.NewsLetterNotify = View.NewsLetterNotify;
            _objUserReg.EmailNotification = _objEmaNoti;
            object[] param ={ _objUserReg };
            this._controller.UpdateEmailNotofication(param);
            //UpdateEmailNotofication

        }
        public void SetEmailNotification()
        {
            UserRegistration _objUserReg = new UserRegistration();
            TributesPortal.BusinessEntities.Users objUsers = new TributesPortal.BusinessEntities.Users();
            objUsers.UserId = View.UserId;
            _objUserReg.Users = objUsers;
            _controller.GetEmailNotofication(_objUserReg);
            try
            {
                if (_objUserReg.EmailNotification != null)
                {
                    View.StoryNotify = _objUserReg.EmailNotification.StoryNotify;
                    View.NotesNotify = _objUserReg.EmailNotification.NotesNotify;
                    View.EventsNotify = _objUserReg.EmailNotification.EventsNotify;
                    View.GuestBookNotify = _objUserReg.EmailNotification.GuestBookNotify;
                    View.GiftsNotify = _objUserReg.EmailNotification.GiftsNotify;
                    View.PhotosNotify = _objUserReg.EmailNotification.PhotosNotify;
                    View.PhotoAlbumNotify = _objUserReg.EmailNotification.PhotoAlbumNotify;
                    View.VideosNotify = _objUserReg.EmailNotification.VideosNotify;
                    View.CommentsNotify = _objUserReg.EmailNotification.CommentsNotify;
                    View.MessagesNotify = _objUserReg.EmailNotification.MessagesNotify;
                    View.NewsLetterNotify = _objUserReg.EmailNotification.NewsLetterNotify;
                }
                else
                {
                    View.BannerMessage = "Email Notification is not for you";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
