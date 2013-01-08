///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.TributeVisitCount.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the number of
///                  visits to a tribute
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class TributeVisitCount
    {
        private int _SectionTypeCodeId;
        private int _SectionTypeID;
        private int _Count;
        private Errors _ObjErrors;

        public int SectionTypeCodeId
        {
            get { return _SectionTypeCodeId; }
            set { _SectionTypeCodeId = value; }
        }
        public int SectionTypeID
        {
            get { return _SectionTypeID; }
            set { _SectionTypeID = value; }
        }
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
        public Errors CustomError
        {
            get { return _ObjErrors; }
            set { _ObjErrors = value; }
        }

    }
}



