///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UpdateTribute.cs
///Author          : LHK
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Video Tokens
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class UpdateTribute
    {
        public enum UpdateTributeEnum
        {
            TributeId,
            TributeName,
            TributeType,
            TributeUrl,
            CreatedBy,
            PackageId,
            PackageName,
            EndDate,
            Date1,
            Date2
        }

        #region FIELDS
        private int _tributeId;
        private string _tributeName;
        private int _tributeType;
        private string _tributeUrl;
        private string _typeDescription;
        private int _createdBy;
        private int _PackageId;
        private string _PackageName;
        private Nullable<DateTime> _EndDate;
        private Nullable<DateTime> _date1;
        private Nullable<DateTime> _date2;
        #endregion

        #region PROPERTIES
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
        
        public int TributeType
        {
            get { return _tributeType; }
            set { _tributeType = value; }
        }

        public string TypeDescription
        {
            get { return _typeDescription; }
            set { _typeDescription = value; }
        }

        public string TributeUrl
        {
            get { return _tributeUrl; }
            set { _tributeUrl = value; }
        }

         public int CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

         public int PackageId
        {
            get { return _PackageId; }
            set { _PackageId = value; }
        }

         public string PackageName
         {
             get { return _PackageName; }
             set { _PackageName = value; }
         }

        public Nullable<DateTime> EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public Nullable<DateTime> Date1
        {
            get { return _date1; }
            set { _date1 = value; }
        }

        public Nullable<DateTime> Date2
        {
            get { return _date2; }
            set { _date2 = value; }
        }

        public UpdateTribute()
        {}

        public UpdateTribute(int tributeId, string tributeName, int tributeType, string tributeUrl, int createdBy, int packageId, string packageName, Nullable<DateTime> endDate, Nullable<DateTime> date1, Nullable<DateTime> date2)
        {
            this.TributeId = tributeId;
            this.TributeName = tributeName;
            this.TributeType = tributeType;
            this.TributeUrl = TributeUrl;
            this.CreatedBy = createdBy;
            this.PackageId = packageId;
            this.PackageName = packageName;
            this.EndDate = endDate;
            this.Date1 = date1;
            this.Date2 = date2;

        }
        #endregion
    }
}
