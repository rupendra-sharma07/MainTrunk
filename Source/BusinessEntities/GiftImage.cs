///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.GiftImage.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Gift Images
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES

using System;
using System.Collections.Generic;
using System.Text;

#endregion


/// <summary>
///Tribute Portal-Gifts Entity Timeless Software
//============================================
// Copyright © Timeless Software  All rights reserved.

// This is the Gifts image Entity class for the Gift. it contain all the Images for the particular Tribute Type
/// </summary>
/// 
namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class GiftImage
    {
        #region ENUM

        public enum GiftEnum
        {
            ImageId,
            ImageUrl,
            ImageType,
            TributeType,
            IsActive,
            IsDeleted,
            Error
        }


        #endregion


        #region FIELDS

        private int _ImageId;
        private string _ImageUrl;
        private string _ImageType;
        private int _TributeType;

        private Errors _CustomError;

        #endregion


        #region PROPERTIES

        public int ImageId
        {
            get { return _ImageId; }
            set { _ImageId = value; }
        }
        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        public string ImageType
        {
            get { return _ImageType; }
            set { _ImageType = value; }
        }
        public int TributeType
        {
            get { return _TributeType; }
            set { _TributeType = value; }
        }

        public Errors CustomError
        {
            get { return _CustomError; }
            set { _CustomError = value; }
        }

        #endregion
    }
}
