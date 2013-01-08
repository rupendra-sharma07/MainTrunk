#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
#endregion

namespace TributeService.BusinessEntities
{
    public partial class VideoStatus
    {
        #region FIEDLS
        private int _userId;
        private string _tokenId;
        private bool _status;
        private string _fileName;
        private bool _isActive;
        private bool _isDeleted;
        private int _createdBy;
        private DateTime _createdDate;
        #endregion

        #region PROPERTIES
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string TokenId
        {
            get { return _tokenId; }
            set { _tokenId = value; }
        }
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        public int CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        #endregion
    }
}
