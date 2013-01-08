///Copyright       : Copyright (c) Optimus Information
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.ModelPopup.GuestListFullDetails.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the full details of a guest in the guest list
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TributesPortal.Utilities;
using System.Text;
using TributesPortal.BusinessLogic;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;

public partial class ModelPopup_GuestListFullDetails : PageBase
{
    private int _guestId;
    private int _additionalGuestId;
    private static string _MealOptions;
    private bool _IsAskForMeal;

    protected static string MealOptions
    {
        get
        {
            return _MealOptions;
        }
        set
        {
            _MealOptions = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //string str = Request.Url.ToString();
            if (Request.QueryString["FirstName"] != null)
                txtFirstName.Text = Request.QueryString["FirstName"].ToString();
            if (Request.QueryString["LastName"] != null)
                txtLastName.Text = Request.QueryString["LastName"].ToString();
            if (Request.QueryString["PhoneNumber"] != null)
                txtPhoneNumber.Text = Request.QueryString["PhoneNumber"].ToString();
            if (Request.QueryString["Email"] != null)
                txtEmail.Text = Request.QueryString["Email"].ToString();
            if (Request.QueryString["Comment"] != null)
                txtComment.Text = Request.QueryString["Comment"].ToString();


            string strURL = Request.Url.ToString();           


            //Get Event Meals
            EventManager eventMgr = new EventManager();         
            
            _guestId = int.Parse(Request.QueryString["GuestId"].ToString());
         
            CompleteGuestList lstCompleteGuestList = eventMgr.GetMealOptions(_guestId);
            _MealOptions = Convert.ToString(lstCompleteGuestList.MealOption);
            _IsAskForMeal = Convert.ToBoolean(lstCompleteGuestList.IsAskForMeal.ToString());

            if (_IsAskForMeal == true)
            {
                if (Request.QueryString["MealOption"] != null || Request.QueryString["MealOption"] != "")
                {
                    if (_MealOptions != null && _MealOptions.Length > 0)
                    {
                        trMealOption.Attributes.Add("style", "display:block");
                        foreach (string item in _MealOptions.Split('#'))
                        {
                            ddlMealOption.Items.Add(item);
                        }
                        ddlMealOption.Items.Insert(0, new ListItem("Select a meal option:","0"));
                    }
                    else
                    {
                        trMealOption.Attributes.Add("style", "display:none");
                    }
                    if (ddlMealOption.Items.FindByValue(Request.QueryString["MealOption"].ToString()) != null)
                    {
                        ddlMealOption.Items.FindByValue(Request.QueryString["MealOption"].ToString()).Selected = true;
                    }
                }
            }
            else
            {
                trMealOption.Attributes.Add("style", "display:none");
            }
          

            if (Request.QueryString["RsvpStatus"] != null)
            {
                if (Request.QueryString["RsvpStatus"].ToString() == "Attending")
                    rdoEventRSVPAttending.Checked = true;
                if (Request.QueryString["RsvpStatus"].ToString() == "Maybe Attending")
                    rdoEventRSVPMaybe.Checked = true;
                if (Request.QueryString["RsvpStatus"].ToString() == "Not Attending")
                    rdoEventRSVPNot.Checked = true;
            }

            if (Request.QueryString["GuestId"] != null)
                _guestId = int.Parse(Request.QueryString["GuestId"].ToString());

            if (Request.QueryString["AdditionalGuestId"] != null)
                _additionalGuestId = int.Parse(Request.QueryString["AdditionalGuestId"].ToString());
        }
    }

    protected void lbtnDeleteRsvp_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["GuestId"] != null)
            _guestId = int.Parse(Request.QueryString["GuestId"].ToString());

        if (Request.QueryString["AdditionalGuestId"] != null)
            _additionalGuestId = int.Parse(Request.QueryString["AdditionalGuestId"].ToString());

        EventManager eventMgr = new EventManager();
        eventMgr.DeleteRsvp(_guestId, _additionalGuestId);

        //ScriptManager.RegisterClientScriptBlock(lbtnDeleteRsvp, GetType(), "ClosePopup", "parent.modalClose_();", true);
        //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "ClosePopup", "parent.modalClose_();", true);
        StringBuilder strb = new StringBuilder();
        //strb.Append("parent.modalClose_();");
        strb.Append("parent.window.location.reload(true);");
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", strb.ToString(), true);
    }

    protected void lbtnSaveRsvp_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["GuestId"] != null)
            _guestId = int.Parse(Request.QueryString["GuestId"].ToString());

        if (Request.QueryString["AdditionalGuestId"] != null)
            _additionalGuestId = int.Parse(Request.QueryString["AdditionalGuestId"].ToString());

        string rsvpStatus = string.Empty;
        if (rdoEventRSVPAttending.Checked)
            rsvpStatus = "Attending";
        else if (rdoEventRSVPMaybe.Checked)
            rsvpStatus = "Maybe Attending";
        else if (rdoEventRSVPNot.Checked)
            rsvpStatus = "Not Attending";

        CompleteGuestList objGuestToAdd = new CompleteGuestList();
        objGuestToAdd.GuestId = _guestId;
        objGuestToAdd.AdditionalGuestId = _additionalGuestId;
        objGuestToAdd.FirstName = txtFirstName.Text;
        objGuestToAdd.LastName = txtLastName.Text;
        objGuestToAdd.PhoneNumber = txtPhoneNumber.Text;

        //Deepak Code for check existing Emails 20100423
        EventManager eventMgr = new EventManager();    
        List<CompleteGuestList> lstCompleteGuestList = eventMgr.GetEmailIdsForEvent(_guestId);
        string CurrentUserEmail = Request.QueryString["Email"].ToString();
        bool MailCheckflag = true;
      
        foreach (CompleteGuestList  obj in lstCompleteGuestList)
        {
              if (MailCheckflag == true )
              {
                    if (txtEmail.Text == obj.Email)
                    {                   
                        if (txtEmail .Text == CurrentUserEmail)
                        {
                             MailCheckflag = true;
                        }
                        else
                        {
                            MailCheckflag = false;
                            break ;
                        }
                    }
              }
        }

        if (MailCheckflag == true)
        {           
            objGuestToAdd.Email = txtEmail.Text;

            if (_MealOptions != null && _MealOptions.Length > 0)
            {
                if (!Equals(ddlMealOption.SelectedItem.Value, "0"))
                {
                    objGuestToAdd.MealOption = ddlMealOption.SelectedItem.Value;
                }
            }
            objGuestToAdd.RsvpStatus = rsvpStatus;
            objGuestToAdd.Comment = txtComment.Text;


            eventMgr.UpdateRsvp(objGuestToAdd);
            lblErrMsg.Visible = false;
            //ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "ClosePopup", "parent.modalClose_();", true);
            //this.Page.ClientScript.RegisterStartupScript(GetType(), "ClosePopup", "parent.modalClose_();", true);
            StringBuilder strb = new StringBuilder();
            //strb.Append("parent.modalClose_();");
            strb.Append("parent.window.location.reload(true);");
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "HidePanel", strb.ToString(), true);
        }
        else
        {
           // ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Increse", "OpenGuestListFullDetails1();", true);
            lblErrMess.Text = "The person with the email address " + txtEmail.Text + " has already been invited to this event.";
            lblErrMess.Visible = true;
            
        }
       
       
    }
}