///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.AddToFavorite.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the entity to be used when adding a tribute to the favourites list
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public partial class AddToFavorite
    {
        #region ENUM
        public enum AddToFavoriteEnum
        {
            UserId,
            TributeId,
            EmailAlert,
            CreatedBy,
            CreatedDate,
            ModifiedBy,
            ModifiedDate,
            IsDeleted,
            IsActive
        }
        #endregion

        #region FIEDLS
        private int _userId;
        private int _tributeId;
        private int _createdBy;
        private int _modifiedBy;
        private bool _emailAlert;
        private bool _isActive;
        private bool _isDeleted;
        private DateTime _createdDate;
        private DateTime _modifiedDate;
        #endregion

        #region PROPERTIES
        public int UserId
        {
            set
            {
                _userId = value;
            }
            get
            {
                return _userId;
            }
        }
        public int TributeId
        {
            set
            {
                _tributeId = value;
            }
            get
            {
                return _tributeId;
            }
        }
        public int CreatedBy
        {
            set
            {
                _createdBy = value;
            }
            get
            {
                return _createdBy;
            }
        }
        public int ModifiedBy
        {
            set
            {
                _modifiedBy = value;
            }
            get
            {
                return _modifiedBy;
            }
        }
        public bool EmailAlert
        {
            set
            {
                _emailAlert = value;
            }
            get
            {
                return _emailAlert;
            }
        }
        public bool IsActive
        {
            set
            {
                _isActive = value;
            }
            get
            {
                return _isActive;
            }
        }
        public bool IsDeleted
        {
            set
            {
                _isDeleted = value;
            }
            get
            {
                return _isDeleted;
            }
        }
        public DateTime CreatedDate
        {
            set
            {
                _createdDate = value;
            }
            get
            {
                return _createdDate;
            }
        }
        public DateTime ModifiedDate
        {
            set
            {
                _modifiedDate = value;
            }
            get
            {
                return _modifiedDate;
            }
        }
        #endregion
    }
}
