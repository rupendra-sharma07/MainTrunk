///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.FeaturedTribute.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the Featured Tribute object
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class FeaturedTribute
    {
        #region FIELDS
        private int _featuredTributeId;
        private int _userTributeId;
        private int _tributeId;
        private string _tributeName;
        private string _tributeImage;
        private string _city;
        private string _state;
        private string _country;
        private string _createdDate;
        private string _noRecord;
        private Errors objErr;
        #endregion

        #region PROPERTIES
        public int FeaturedTributeId
        {
            get { return _featuredTributeId; }
            set { _featuredTributeId = value; }
        }
        public int UserTributeId
        {
            get { return _userTributeId; }
            set { _userTributeId = value; }
        }
        public int TributeId
        {
            get { return _tributeId; }
            set { _tributeId = value; }
        }
        public string TributeName
        {
            get { return _tributeName; }
            set { _tributeName = value; }
        }
        public string TributeImage
        {
            get { return _tributeImage; }
            set { _tributeImage = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        public string NoRecord
        {
            get { return _noRecord; }
            set { _noRecord = value; }
        }
        public Errors CustomError
        {
            get { return objErr; }
            set { objErr = value; }
        }
        #endregion
    }
}
