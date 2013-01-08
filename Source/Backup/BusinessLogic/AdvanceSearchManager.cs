///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessLogic.AdvanceSearchManager.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the methods associated with advance search
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;
using TributesPortal.BusinessEntities;
using TributesPortal.ResourceAccess;

namespace TributesPortal.BusinessLogic
{
    public class AdvanceSearchManager
    {
        public IList<Locations> Locations(Locations location)
        {
            LocationResource objLocationResource = new LocationResource();

            IList<Locations> tmpLocation = objLocationResource.LocationList(location);

            // Add blank at first index as in country and list first place should be blank.
           // tmpLocation.Insert(0, new Locations(-1, "", -1));

            return tmpLocation;
        }
    }
}
