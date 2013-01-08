using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TributeService.BusinessEntities
{
    public partial class Users
    {
        #region CONSTRUCTOR
        public Users()
        { }
        #endregion

        #region FIEDLS
        private int _userId;
        private string _userName;
        private string _password;
        private string _firstName;
        private string _lastName;
        #endregion

        #region PROEPRTIES
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        #endregion
    }
}
