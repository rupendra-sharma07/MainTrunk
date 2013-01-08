///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Paging.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle Paging
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class Paging
    {

        private int _RecordCount;

        public int RecordCount
        {

            get { return _RecordCount; }
            set { _RecordCount = value; }

        }

        public Paging(int RecordCount)
        {
            _RecordCount = RecordCount;
        }

    }
}
