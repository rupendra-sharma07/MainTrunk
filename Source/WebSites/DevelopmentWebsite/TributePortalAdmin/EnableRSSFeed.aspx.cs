
///Copyright       : Copyright (c) Optimus Info India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.EnableRSSFeed.aspx.cs
///Author          : Laxman Hari Kulshrestha
///Creation Date   : 
///Description     : This page allows the admin to enable RSS Feed for a bussiness user.

#region USING DIRECTIVES
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.TributePortalAdmin.Views;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
using TributesPortal.Utilities;
#endregion

public partial class TributePortalAdmin_EnableRSSFeed : System.Web.UI.Page, IEnableRSSFeed
{
    #region CLASS VARIABLES
    private EnableRSSFeedPresenter _presenter;
    string _userName = string.Empty;
    int _userId = 0;
    bool _atomEnabled = false;
    int _upadetOutput = 0;
    string _feedWord = "XML";
    bool _EnableXMLFeed = false;


    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
        lblNoRecord.Visible = false;
        lblUpdatedFeed.Visible = false;
        lblLinkText.Visible = false;
        lblLinkUrl.Visible = false;
        if (rdoBtnRSS.Checked == true)
        {
            _feedWord = "Atom";

        }
        else if (rdoBtnXML.Checked == true)
        {
            _feedWord = "XML";
        }

        //Button cmdSubmit = btnSubmit as Button;
        Button cmdSubmit = btnSubmit as Button;
        if (!this.IsPostBack)
        {
            rdoUserId.Checked = true;
            rdoBtnXML.Checked = true;
            //lblUserIdOrUserName.Text = "UserId/UserName";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        _atomEnabled = true;
        _EnableXMLFeed = true;
        if (rdoUserId.Checked == true)
        {
            int.TryParse(txtUserIdOrUsername.Text.Trim(), out _userId);
        }
        if (rdoUserName.Checked == true)
        {
            UserName = txtUserIdOrUsername.Text.ToString();
        }
        if ((_userId > 0) || !(string.IsNullOrEmpty(_userName)))
        {
            if (rdoBtnXML.Checked == true)
                this._presenter.EnableXmlFeedForBussUser();
            else if (rdoBtnRSS.Checked == true)
                this._presenter.EnableRSSFeedForBussUser();
        }
        else
        {
            lblUpdatedFeed.Visible = true;
            lblUpdatedFeed.Text = "Please enter a valid " + GetField();
        }
        lblLinkText.Text = _feedWord + " Feed URL : ";
    }

    
    protected void btnDisable_Click(object sender, EventArgs e)
    {
        _atomEnabled = false;
        _EnableXMLFeed = false;
        if (rdoUserId.Checked == true)
        {
            int.TryParse(txtUserIdOrUsername.Text.Trim(), out _userId);
        }
        if (rdoUserName.Checked == true)
        {
            UserName = txtUserIdOrUsername.Text.ToString();
        }
        if ((_userId > 0) || !(string.IsNullOrEmpty(_userName)))
        {
            if (rdoBtnRSS.Checked == true)
                this._presenter.EnableRSSFeedForBussUser();
            else if (rdoBtnXML.Checked == true)
                this._presenter.EnableXmlFeedForBussUser();
        }
        else
        {
            lblUpdatedFeed.Visible = true;
            lblUpdatedFeed.Text = "Please enter a valid " + GetField();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtUserIdOrUsername.Text = "";
        rdoUserId.Checked = true;
        rdoUserName.Checked = false;
        //lblUserIdOrUserName.Text = "UserId/UserName";
        lblUpdatedFeed.Visible = false;

    }

    protected void RadioSelection_Changed(object sender, EventArgs e)
    {
        if (rdoBtnRSS.Checked == true)
        {
            _feedWord = "Atom";
            
        }
        else if (rdoBtnXML.Checked == true)
        {
            _feedWord = "XML";
        }
    }
    
    #endregion

    #region Properties
    [CreateNew]
    public EnableRSSFeedPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }




    public string UserName
    {
        get
        {
            return _userName;
        }
        set
        {
            _userName = value;
        }
    }
    public int UserId 
    { 
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
        } 
    }

    public bool AtomEnabled
    {
        get
        {
            return _atomEnabled;
        }
        set
        {
            _atomEnabled = value;
        } 
    }

    public bool EnableXMLFeed
    {
        get { return _EnableXMLFeed; }
        set { _EnableXMLFeed = value; }
    }

    public string FeedWord
    {
        get
        {
            return _feedWord;
        }
        set
        {
            _feedWord = value;
        }
    }

    public int UpdateOutput
    {
        get
        {
            return _upadetOutput;
        }
        set 
        { 
            _upadetOutput = value;
            lblNoRecord.Visible = true;
            lblUpdatedFeed.Visible = true;
            if (_upadetOutput == 0)
            {
                lblUpdatedFeed.Text = "No user exists with the mentioned details.";
            }
            else if (_upadetOutput == 1)
            {
                if (rdoBtnXML.Checked == true)
                {
                    if (_EnableXMLFeed == true)
                    {
                        lblLinkText.Visible = true;
                        lblLinkUrl.Visible = true;
                        lblUpdatedFeed.Text = _feedWord + " Feed is now enabled for this User!";
                        lblLinkUrl.Text = WebConfig.AppBaseDomain + "tribute/feed.aspx?BusinessUserId=" + _userId.ToString() + "&Start=1&PageSize=10";
                    }
                    else if (_EnableXMLFeed == false)
                    {
                        lblUpdatedFeed.Text = _feedWord + " Feed is now disabled for this User!";
                    }

                }
                else if (rdoBtnRSS.Checked == true)
                {
                    if (_atomEnabled == true)
                    {
                        lblLinkText.Visible = true;
                        lblLinkUrl.Visible = true;
                        lblUpdatedFeed.Text = _feedWord + " Feed is now enabled for this User!";
                        lblLinkUrl.Text = WebConfig.AppBaseDomain + "tribute/feed.aspx?userid=" + _userId.ToString();
                    }
                    else if (_atomEnabled == false)
                    {
                        lblUpdatedFeed.Text = _feedWord + " Feed is now disabled for this User!";
                    }
                }
            }
            
        }
    }

   #endregion

    #region METHODS

    private string GetField()
    {
        string FieldSelected= string.Empty;
        if (rdoUserId.Checked == true)
            FieldSelected = "UserId";
        if (rdoUserName.Checked == true)
            FieldSelected = "UserName";

        return FieldSelected;
        
    }


    #endregion

    
}
