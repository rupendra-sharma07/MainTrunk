///Copyright       : Copyright (c) Optimus Information
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.CompleteGuestList.cs
///Author          : 
///Creation Date   : 
///Description     : 
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class CompleteGuestList
    {

        public enum GuestListEnum
        {
            RsvpDate,
            FirstName,
            LastName,
            PhoneNumber,
            Email,
            MealOption,
            RsvpStatus,
            Comment,
            GuestId,
            AdditionalGuestId,
            IsAskForMeal,
            RSVPId
        }

        /// <summary>
        /// Default Contructor
        /// <summary>
        public CompleteGuestList()
        { }

        public int _RsvpId;
        public int RsvpId
        {
            get { return _RsvpId; }
            set { _RsvpId = value; }
        }
        private String _RsvpDate;
        public String RsvpDate
        {
            get { return _RsvpDate; }
            set { _RsvpDate = value; }
        }

        private String _FirstName;
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private String _LastName;
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private String _PhoneNumber;
        public String PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        private String _Email;
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private String _MealOption;
        public String MealOption
        {
            get { return _MealOption; }
            set { _MealOption = value; }
        }

        private String _RsvpStatus;
        public String RsvpStatus
        {
            get { return _RsvpStatus; }
            set { _RsvpStatus = value; }
        }

        private String _Comment;
        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        private int _GuestId;
        public int GuestId
        {
            get { return _GuestId; }
            set { _GuestId = value; }
        }

        private int _AdditionalGuestId;
        public int AdditionalGuestId
        {
            get { return _AdditionalGuestId; }
            set { _AdditionalGuestId = value; }
        }

        private bool _IsAskForMeal;
        public bool IsAskForMeal
        {
            get { return _IsAskForMeal; }
            set { _IsAskForMeal = value; }
        }


        /// <summary>
        /// User defined Contructor
        /// <summary>
        public CompleteGuestList(String RsvpDate,
            String FirstName,
            String LastName,
            String PhoneNumber,
            String Email,
            String MealOption,
            String RsvpStatus,
            String Comment,
            int GuestId,
            int AdditionalGuestId,
            bool _IsAskForMeal,
            int RsvpId)
        {
            this._RsvpDate = RsvpDate;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._PhoneNumber = PhoneNumber;
            this._Email = Email;
            this._MealOption = MealOption;
            this._RsvpStatus = RsvpStatus;
            this._Comment = Comment;
            this._GuestId = GuestId;
            this._AdditionalGuestId = AdditionalGuestId;
            this._IsAskForMeal = IsAskForMeal;
            this .RsvpId = RsvpId ;
        }

    }
}
