///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.FacadeManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file instantiates all the business managers
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
///Tribute Portal-Facade Manager Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.
/// </summary>
namespace TributesPortal.BusinessLogic
{
    [Serializable]
    public class FacadeManager
    {
        private  static UserManager objUserMgr;   
        public static   UserManager UserManager
        {
            get { return objUserMgr=new UserManager(); }
        }

        private static CustomerManager objCustomerMgr;

        public static CustomerManager CustManager
        {
            get { return objCustomerMgr = new CustomerManager(); }
        }

        private static CommentManager objCommentMgr;

        public static CommentManager CommentMgr
        {
            get { return objCommentMgr = new CommentManager(); }
        }

        //For user info Manager
        private static UserInfoManager objUserInfoMager;
        public static UserInfoManager UserInfoManager
        {
            get { return objUserInfoMager = new UserInfoManager(); }
        }

        private static BillingManager objBillingManager;
        public static BillingManager BillingManager
        {
            get { return objBillingManager = new BillingManager(); }
        }

        //For Featured Tribute
        private static FeaturedTributeManager objFeaturedTributeMgr;
        public static FeaturedTributeManager FeaturedTributeManager
        {
            get
            {   return objFeaturedTributeMgr = new FeaturedTributeManager(); }
        }
        private static TributeManager objTributeManager;

        public static TributeManager TributeManager
        {
            get { return objTributeManager=new TributeManager(); }
            
        }

        public static VideoManager objVideoManager;
        public static VideoManager VideoManager
        {
            get { return objVideoManager = new VideoManager(); }

        }

        public static SearchTributeManager objSearchTributeManager;
        public static SearchTributeManager SearchTributeManager
        {
            get { return objSearchTributeManager = new SearchTributeManager(); }

        }

        public static AdvanceSearchManager objAdvanceSearchManager;
        public static AdvanceSearchManager AdvanceSearchManager
        {
            get { return objAdvanceSearchManager = new AdvanceSearchManager(); }

        }

        public static AllTributeManager objAllTributeManager;
        public static AllTributeManager AllTributeManager
        {
            get { return objAllTributeManager = new AllTributeManager(); }

        }

        public static StoryManager objStoryManager;
        public static StoryManager StoryManager
        {
            get { return objStoryManager = new StoryManager(); }

        }

        public static MessagingSystemManager objMessagingSystemManager;
        public static MessagingSystemManager MessagingSystemManager
        {
            get { return objMessagingSystemManager = new MessagingSystemManager(); }
        }

        public static TributeThemeManager objTributeThemeManager;
        public static TributeThemeManager TributeThemeManager
        {
            get { return objTributeThemeManager = new TributeThemeManager(); }
        }

        public static NotesManager objNotesManager;
        public static NotesManager NotesManager
        {
            get { return objNotesManager = new NotesManager(); }
        }

        public static GiftManager objGiftManager;
        public static GiftManager GiftManager
        {
            get { return objGiftManager = new GiftManager(); }
        }

        public static EventManager objEventManager;
        public static EventManager EventManager
        {
            get { return objEventManager = new EventManager(); }
        }

        public static PhotoManager objPhotoManager;
        public static PhotoManager PhotoManager
        {
            get { return objPhotoManager = new PhotoManager(); }
        }


        public static CouponManager objCouponManager;
        public static CouponManager CouponManager
        {
            get { return objCouponManager = new CouponManager(); }
        }

        public static TributeActivityManager objTributeActivityManager;
        public static TributeActivityManager TributeActivityManager
        {
            get { return objTributeActivityManager = new TributeActivityManager(); }
        }
    }
}
