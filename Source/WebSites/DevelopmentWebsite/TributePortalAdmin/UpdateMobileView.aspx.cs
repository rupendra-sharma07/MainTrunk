using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using TributesPortal.TributePortalAdmin.Views;
using Microsoft.Practices.ObjectBuilder;

public partial class TributePortalAdmin_UpdateMobileView : System.Web.UI.Page,IUpdateMobileView
{
    private UpdateMobileViewPresenter _presenter;
    bool _isMobileViewOn = false;
    int _userid = 0;
    bool _error;
    string _Username = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUserIdOrUsername.Text = "";
            lblUpdatedRecord.Visible = false;
            rdoUserId.Checked = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            _isMobileViewOn = true;

           _presenter.UpdateMObiteView();
           UpdateStatus(_error);
        }
        catch (Exception ex)
        {
            UpdateStatus(false);
        }
    }
    protected void btnDisable_Click(object sender, EventArgs e)
    {
        try
        {
            _isMobileViewOn = false;

            _presenter.UpdateMObiteView();
            UpdateStatus(_error);
        }
        catch (Exception ex)
        {
            UpdateStatus(false);
        }
    }

    private void UpdateStatus(bool pStatus)
    {
        lblUpdatedRecord.Visible = true;
        if (pStatus)
        {
            if (rdoUserName.Checked)
                lblUpdatedRecord.Text = "Mobile view is successfully updated for UserName : " + _Username;
            else
                lblUpdatedRecord.Text = "Mobile view is successfully updated for UserId : " + _userid.ToString();
        }
        else
        {
            if (rdoUserName.Checked)
                lblUpdatedRecord.Text = "Mobile view could not be updated for UserName : " + _Username;
            else
                lblUpdatedRecord.Text = "Mobile view could not be updated for UserId : " + _userid.ToString();
        }
    }

    #region IUpdateMobileView Members

    int IUpdateMobileView.UserId
    {
        get
        {
            if (rdoUserId.Checked)
            {
                int.TryParse(txtUserIdOrUsername.Text, out _userid);
            }
            return _userid;
        }  
    }

    string IUpdateMobileView.UserName
    {
        get
        {
            if (rdoUserName.Checked)
            {
                _Username = txtUserIdOrUsername.Text;
            }
            return _Username;
        }
    }

    bool IUpdateMobileView.IsMObileViewOn
    {
        get { return _isMobileViewOn; } 
    }
    bool IUpdateMobileView.Error
    {
        set { _error = value; }
    }


    [CreateNew]
    public UpdateMobileViewPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtUserIdOrUsername.Text = "";
        lblUpdatedRecord.Visible = false;
    }
}
