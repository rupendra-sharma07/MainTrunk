using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class LinkVideoMemorialTribute
    {
        public enum LinkVdeoMemorialTributeEnum
        {
            UserId, VideoTributeId, MemTributeId
        }


        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        private int _VideoTributeId;
        public int VideoTributeId
        {
            get { return _VideoTributeId; }
            set { _VideoTributeId = value; }
        }

        private int _MemTributeId;
        public int MemTributeId
        {
            get { return _MemTributeId; }
            set { _MemTributeId = value; }
        }

        /// <summary>
        /// User defined Contructor
        /// <summary>
        public LinkVideoMemorialTribute(int UserId,
            int VideoTributeId,
            int MemTributeId)
        {
            this._UserId = UserId;
            this._VideoTributeId = VideoTributeId;
            this._MemTributeId = MemTributeId;
        }

    
    }//end class
}//end namespace
