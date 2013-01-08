///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Stories.cs
///Author          : 
///Creation Date   : 
///Description     : This is the Story Entity class for the Story. it contain all the variable required for the 
///                     Personal Detail, story and Moreabout section of the story
///Audit Trail     : Date of Modification  Modified By         Description


#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

#endregion


/// <summary>
///Tribute Portal-Story Entity Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Story Entity class for the Story. it contain all the variable required for the 
// Personal Detail, story and Moreabout section of the story
/// </summary>
/// 

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Stories
    {

        #region ENUM

        public enum StoriesEnum
        {
            UserId,
            TributeId,
            TributeName,
            TributeImage,
            Date1,
            Date2,
            PostMessage,
            MessageWithoutHtml,
            MessageAddedModifiedBy,
            Age,
            City,
            State,
            Country,
            CreatedBy,
            ModifiedBy,
            CreatedDate,
            ModifiedDate,
            Operation,
            IsActive,
            IsDeleted,
            Error
        }

        public enum OperationEnum
        {
            Add,
            Update
        }

        public enum StorySectionEnum
        {
            Story,

            [Description("More About")]
            MoreAbout,

            [Description("Other Topic")]
            OtherTopic
        }

        public enum CSSClassEnum
        {
            [Description("yt-SectionWrapper yt-Story-PersonalDetails")]
            PersonalDetail,

            [Description("yt-SectionWrapper yt-Story-PersonalDetails yt-Disabled")]
            PersonalDetailDisable,

            //[Description("yt-SectionWrapper yt-Obituary-Text")]
            //Obituary,

            [Description("yt-SectionWrapper yt-Story-Text")]
            Story,

            [Description("yt-SectionWrapper yt-Story-Text yt-Disabled")]
            StoryDisable,

            [Description("yt-SectionWrapper yt-Story-More")]
            MoreAbout,

            [Description("yt-SectionWrapper yt-Story-More yt-Disabled")]
            MoreAboutDisable
        }

        public enum StoryMaintainState
        {
            Story_Admin,
            Location,
            StoryPage_CurrentState,
            StoryImageURL
        }

        public enum ObituaryMaintainState
        {
            TributeId,
            PostMessage,
            MessageWithoutHtml,
            MessageAddedModifiedBy
        }

        #endregion


        #region FIELDS

        private int _UserId;
        private int _TributeId;

        private string _TributeName;
        private string _TributeType;
        private string _TributeImage;
        private Nullable<DateTime> _Date1;
        private Nullable<DateTime> _Date2;
        private string _Age;
        private string _City;
        private Nullable<int> _State;
        private Nullable<int> _Country;
        private string _StateName;
        private string _CountryName;
        private bool _IsExist;

        private IList<StoryMoreAbout> _MoreAboutSection;
        private string _Operation;

        private int _CreatedBy;
        private Nullable<DateTime> _CreatedDate;
        private int _ModifiedBy;
        private Nullable<DateTime> _ModifiedDate;

        private bool _IsAdmin;
        private Errors _CustomError;
        private string _FirstName;
        private string _LastName;
        private string _UrlToEmail;
        private string _postMessage;
        private string _messageWithoutHtml;
        private int _messageAddedModifiedBy;

        #endregion


        #region PROPERTIES

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public int TributeId
        {
            get { return _TributeId; }
            set { _TributeId = value; }
        }

        public string Operation
        {
            get { return _Operation; }
            set { _Operation = value; }
        }

        public string TributeName
        {
            get { return _TributeName; }
            set { _TributeName = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string UrlToEmail
        {
            get { return _UrlToEmail; }
            set { _UrlToEmail = value; }
        }


        public string TributeType
        {
            get { return _TributeType; }
            set { _TributeType = value; }
        }

        public string TributeImage
        {
            get { return _TributeImage; }
            set { _TributeImage = value; }
        }

        public string Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        public Nullable<DateTime> Date1
        {
            get { return _Date1; }
            set { _Date1 = value; }
        }

        public Nullable<DateTime> Date2
        {
            get { return _Date2; }
            set { _Date2 = value; }
        }

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public Nullable<int> State
        {
            get { return _State; }
            set { _State = value; }
        }

        public Nullable<int> Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }

        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        public IList<StoryMoreAbout> MoreAboutSection
        {
            get { return _MoreAboutSection; }
            set { _MoreAboutSection = value; }
        }

        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public Nullable<DateTime> CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        public Nullable<DateTime> ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }

        public Errors CustomError
        {
            get { return _CustomError; }
            set { _CustomError = value; }
        }

        public bool IsExist
        {
            get { return _IsExist; }
            set { _IsExist = value; }
        }

        public string PostMessage
        {
            get { return _postMessage; }
            set { _postMessage = value; }
        }

        public string MessageWithoutHtml
        {
            get { return _messageWithoutHtml; }
            set { _messageWithoutHtml = value; }
        }

        public int MessageAddedModifiedBy
        {
            get { return _messageAddedModifiedBy; }
            set { _messageAddedModifiedBy = value; }
        }

        #endregion

    }
}
