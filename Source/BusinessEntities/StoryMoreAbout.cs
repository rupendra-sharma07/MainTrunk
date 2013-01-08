///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.StoryMoreAbout.cs
///Author          : 
///Creation Date   : 
///Description     : This is the StoryMoreAbout Entity class for the Story.it contain all the variable required for the 
//                   Moreabout section of the story
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;

#endregion


/// <summary>
///Tribute Portal-StoryMoreAbout Entity Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the StoryMoreAbout Entity class for the Story.it contain all the variable required for the 
// Moreabout section of the story
/// </summary>
/// 

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class StoryMoreAbout
    {
        #region ENUM
        public enum StoriesMoreAboutEnum
        {
            UserId,
            SectionId,
            UserBiographyId,
            PrimaryTitle,
            SecondaryTitle,
            SectionAnswer,
            CreatedBy,
            ModifiedBy,
            CreatedDate,
            ModifiedDate,
            IsActive,
            IsDeleted,
            Error
        }
        #endregion


        #region FIELDS

        private int _UserId;

        private int _SectionId;
        private int _UserBiographyId;
        private string _PrimaryTitle;
        private string _SecondaryTitle;
        private string _SectionAnswer;

        private int _CreatedBy;
        private Nullable<DateTime> _CreatedDate;
        private int _ModifiedBy;
        private Nullable<DateTime> _ModifiedDate;

        private Errors _CustomError;

        #endregion


        #region PROPERTIES

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public int SectionId
        {
            get { return _SectionId; }
            set { _SectionId = value; }
        }

        public int UserBiographyId
        {
            get { return _UserBiographyId; }
            set { _UserBiographyId = value; }
        }

        public string PrimaryTitle
        {
            get { return _PrimaryTitle; }
            set { _PrimaryTitle = value; }
        }

        public string SecondaryTitle
        {
            get { return _SecondaryTitle; }
            set { _SecondaryTitle = value; }
        }

        public string SectionAnswer
        {
            get { return _SectionAnswer; }
            set { _SectionAnswer = value; }
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

        #endregion
    }
}
