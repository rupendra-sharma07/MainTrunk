using System;
using System.Collections.Generic;
using System.Text;

namespace TributesPortal.BusinessEntities
{
    [Serializable]
    public class EnableRSSFeedInfo
    {
        public enum EnableRSSFeedInfoEnum
        {
            UserId,
            UserName,
            AtomEmabled,
            EnableXMLFeed,
            UpdateOutput
        }

        public EnableRSSFeedInfo()
        { }


        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }


        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private bool _atomEnabled;
        public bool AtomEnabled
        {
            get { return _atomEnabled; }
            set { _atomEnabled = value; }
        }

        private int _UpdateOutput;
        public int UpdateOutput
        {
            get { return _UpdateOutput; }
            set { _UpdateOutput = value; }
        }

        private bool _EnableXMLFeed;
        public bool EnableXMLFeed
        {
            get { return _EnableXMLFeed; }
            set { _EnableXMLFeed = value; }
        }


        public EnableRSSFeedInfo(
            int UserId,
            string UserName,
            bool AtomEnabled,
            int UpdateOutput
        )
        {
            this._UserId = UserId;
            this._UserName = UserName;
            this._atomEnabled = AtomEnabled;
            this._UpdateOutput = UpdateOutput;
        }
        public EnableRSSFeedInfo(
            int UserId,
            string UserName,
            bool AtomEnabled,
            int UpdateOutput,
            bool EnableXMLFeed
        )
        {
            this._UserId = UserId;
            this._UserName = UserName;
            this._atomEnabled = AtomEnabled;
            this._UpdateOutput = UpdateOutput;
            this._EnableXMLFeed = EnableXMLFeed;
        }

    }
}
