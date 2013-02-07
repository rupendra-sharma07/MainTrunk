#region USING DIRECTIVES

using System;
using System.Data;
///Copyright       : Copyright (c) Optimus Info India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.AddRemoveCredits.aspx.cs
///Author          : Mohit Gupta
///Creation Date   : 
///Description     : This page allows the admin to add or Remove Credits


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
#endregion


public partial class TributePortalAdmin_AddRemoveCredits : System.Web.UI.Page, IAddRemoveCredit
{
    #region CLASS VARIABLES
    private AddRemoveCreditPresenter _presenter;
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {

        Button cmdSubmit = btnSubmit as Button;
        // cmdSubmit.Attributes.Add("onClick", "javascript:return CheckFields();");
      
        lblNoRecord.Text = "No record exists for the entered criteria.";
       

       
        if (!this.IsPostBack)
        {
            rdoUserId.Checked = true;
            rdoAdd.Checked = true;
            this._presenter.OnViewInitialized();
            lblErrorMessage.Visible = false;

        }
        this._presenter.OnViewLoaded();    
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int _usrId = 0;
        bool IsValid = false;
        IsValid = int.TryParse(txtUserIdOrUsername.Text.ToString(), out _usrId);
        if ((IsValid && rdoUserId.Checked) || (rdoUserName.Checked))
            this._presenter.AddOrDebitCredits(GetAddRemoveCreditObject());
        else
        {
            lblUpdatedCredit.Visible = true;
            lblUpdatedCredit.Text = "Enter a valid UserId.";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = false;
        rdoUserId.Checked = true;
        rdoAdd.Checked = true;
        txtCreditCount.Text = "";
        txtUserIdOrUsername.Text = "";
    }

   
    #endregion

    #region Properties
    [CreateNew]
    public AddRemoveCreditPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

  

    

    public AddRemoveCreditInfo AddRemoveEntries
    {
        set
        {

        }
    }

    public double updatedCreditCount
    {
        set
        {
            if (rdoAdd.Checked)
            {
                if (double.Parse(value.ToString()) < 0)
                {
                    lblUpdatedCredit.Text = "Credits are now less than 0";
                }
                else
                    lblUpdatedCredit.Text = "Current Credit Count in the account : "+ value.ToString();
            }

            if (rdoRemove.Checked)
            {
                if (int.Parse(value.ToString()) < 0)
                {
                    lblUpdatedCredit.Text = " Cannot deduct credits. Current credit count in the account is less than value entered. <br/>Please enter a lesser value";
                }
                else
                {
                    lblUpdatedCredit.Text = "Current Credit Count in the account : " + value.ToString();
                }
            }
        }
    }



    #endregion

    #region METHODS
    
    /// <summary>
    /// Method to get the User object to be searched.
    /// </summary>
    /// <returns>Filled Users entity.</returns>
    private AddRemoveCreditInfo GetAddRemoveCreditObject()
    {
        AddRemoveCreditInfo objAddRemoveCreditInfo = new AddRemoveCreditInfo();
        objAddRemoveCreditInfo.ApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower();
        if (rdoUserId.Checked)
        {
            objAddRemoveCreditInfo.UserId = int.Parse(txtUserIdOrUsername.Text.ToString());
            //1 for if UserId Is opted
            objAddRemoveCreditInfo.UserIdOptedOruserName = 1;
            objAddRemoveCreditInfo.UserName = null;
        }
        else if (rdoUserName.Checked)
        {
            objAddRemoveCreditInfo.UserName = txtUserIdOrUsername.Text.ToString();
            //2 for if UserName Is opted
            objAddRemoveCreditInfo.UserIdOptedOruserName = 2;
            objAddRemoveCreditInfo.UserId = 0;
        }
        // 1 is for Adding Credit and 2 for deducting Credit
        if (rdoAdd.Checked)
        {
            objAddRemoveCreditInfo.CreditOrDebit = 1;
        }
        else if (rdoRemove.Checked)
        {
            objAddRemoveCreditInfo.CreditOrDebit = 2;
        }
        if (txtCreditCount.Text != string.Empty && txtCreditCount.Text != "")
        {
            objAddRemoveCreditInfo.CreditCount = double.Parse(txtCreditCount.Text);
        }
        return objAddRemoveCreditInfo;
    }

    #endregion









    
}