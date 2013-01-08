///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Locations.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details about Locations
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Locations
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Locations()
        { }

        public enum Location
        {
            LocationId, LocationName, LocationParentId
        }


        private int _LocationId;
        public int LocationId
        {
            get { return _LocationId; }
            set { _LocationId = value; }
        }


        private string _LocationName;
        public string LocationName
        {
            get { return _LocationName; }
            set { _LocationName = value; }
        }


        private int _LocationParentId;
        public int LocationParentId
        {
            get { return _LocationParentId; }
            set { _LocationParentId = value; }
        }

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public Locations(int LocationId,
            string LocationName,
            int LocationParentId)
        {
            this._LocationId = LocationId;
            this._LocationName = LocationName;
            this._LocationParentId = LocationParentId;
        }

    }
}
