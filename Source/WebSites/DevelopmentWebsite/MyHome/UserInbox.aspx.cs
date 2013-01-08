///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.UserInbox.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the messages sent to the users by other users on the site
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
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.MyHome.Views;
using TributesPortal.MultipleLangSupport;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Facebook;
using Facebook.Web;


public partial class MyHome_UserInbox : PageBase, IUserInbox
{

    #region Variables
     int UserID;
    private UserInboxPresenter _presenter;
    static IList<MailMessage> OpenInbox;
    static IList<MailMessage> OpenInbox_;
    static string Messageid = string.Empty;
    static string Messagetype = string.Empty;
    static string Messageid_ = string.Empty;
    static string Messagetype_ = string.Empty;
    protected static int SendbyUser;
    //static int TributeId;    //by Ud 
    static int maxcount = int.Parse(WebConfig.Pagesize_myInbox);
    static int counter = 0;
    static int counter_ = 0;
    static bool visib = false; 
    static bool visib_ = false; 
    static int ParantMsgId = 0;
    //protected string _userName = string.Empty;
    #endregion Variables


    [CreateNew]
    public UserInboxPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        errormsg.Visible = false;
        this.Master.NavPrimary = Shared_Inner.AdminNavPrimaryEnum.inbox.ToString();
        this.Master.PageTitle = "Inbox";

        if (!IsPostBack)
        {
            //lnkAdvanceSearch.NavigateUrl = Session["APP_BASE_DOMAIN"] + "advancedsearch.aspx";
            StateManager stateManagerP = StateManager.Instance;
            string PageName = "UserInbox";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                //ConnectSession sess = new ConnectSession(
                //    ConfigurationManager.AppSettings["APIKey"],
                //    ConfigurationManager.AppSettings["Secret"]);
                //if (sess.IsConnected())
                //{
                //    try
                //    {
                //        Facebook.Rest.Api api = new Facebook.Rest.Api(sess);                        //Display user data captured from the Facebook API.  
                //        Facebook.Schema.user user = api.Users.GetInfo();

                //        spanLogout.InnerHtml = "<a href=\"#\" onclick=\"FB.Connect.logoutAndRedirect('" + Session["APP_BASE_DOMAIN"] + "Logout.aspx')\">" +
                //          "   <img id=\"fb_logout_image\" src=\"http://static.ak.fbcdn.net/images/fbconnect/logout-buttons/logout_small.gif\" alt=\"Connect\"/>" +
                //          "</a>";
                //        if (objSessionvalue.UserName == null ||
                //            string.Empty.Equals(objSessionvalue.UserName))
                //        {
                //            objSessionvalue.UserName = user.first_name + " " + user.last_name;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        killFacebookCookies();
                //        ShowMessage("Your Facebook session has timed out. Please clear private data in browser and log in again");
                //        // todo: delete facebook cookies and show message?
                //    }
                //}
                //else
                //{
                //    spanLogout.InnerHtml =
                //        "<fb:login-button onlogin=\"window.location='" +
                //        Session["APP_BASE_DOMAIN"] +
                //        "log_in.aspx?location='+encodeURIComponent(location.href)\" v=\"2\">" +
                //        "</fb:login-button><a href='Logout.aspx'>Log out</a>";
                //}
                UserID = objSessionvalue.UserId;
                //_userName = objSessionvalue.UserName;
                //Usernamelong.InnerText = objSessionvalue.UserName;                
                _presenter.GetMailMessage(1, maxcount);
                _presenter.GetuserInboxTotalCount();
                _presenter.GetuserSentMessagesCount();
                MultiViewInbox.ActiveViewIndex = 0;
                ////set visibility of my tribute
                //if (Session["mytribute"] != null)
                //{                    
                //    limytribute.Visible = bool.Parse(Session["mytribute"].ToString());
                //}

                ////set visibility of my favourite tribute.
                //if (Session["myfavourite"] != null)
                //{
                //    limyfavourite.Visible = bool.Parse(Session["myfavourite"].ToString());
                //}                
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }

        }

        // Set the controls value
        //SetControlsValue();
    }

    ///// <summary>
    ///// This method will return the selected tribute type
    ///// </summary>
    ///// <returns>A string which contain the tribute type</returns>
    //private string GetTributeType()
    //{
    //    string tributeType = "";

    //    if (rdoSearch_All.Checked == true)
    //    {
    //        tributeType = lblSearch_All.InnerText;
    //    }
    //    else if (rdoSearch_Anniversary.Checked == true)
    //    {
    //        tributeType = lblSearch_Anniversary.InnerText;
    //    }
    //    else if (rdoSearch_Birthday.Checked == true)
    //    {
    //        tributeType = lblSearch_Birthday.InnerText;
    //    }
    //    else if (rdoSearch_Graduation.Checked == true)
    //    {
    //        tributeType = lblSearch_Graduation.InnerText;
    //    }
    //    else if (rdoSearch_Memorial.Checked == true)
    //    {
    //        tributeType = lblSearch_Memorial.InnerText;
    //    }
    //    else if (rdoSearch_NewBaby.Checked == true)
    //    {
    //        tributeType = lblSearch_NewBaby.InnerText;
    //    }
    //    else if (rdoSearch_Wedding.Checked == true)
    //    {
    //        tributeType = lblSearch_Wedding.InnerText;
    //    }

    //    return tributeType;
    //}

    //// Added By Parul
    //protected void btnGo_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        // Create SearchTribute object
    //        SearchTribute objSearchTribute = new SearchTribute();

    //        // Assign the search parameter to this object
    //        objSearchTribute.TributeType = GetTributeType();
    //        objSearchTribute.SearchString = txtSearchKeyword.Text.ToString();
    //        objSearchTribute.SearchType = PortalEnums.SearchEnum.Basic.ToString();
    //        objSearchTribute.SortOrder = "DESC";

    //        // Create StateManager object and add search paramter in the session
    //        StateManager objStateMgr = StateManager.Instance;
    //        objStateMgr.Add(PortalEnums.SearchEnum.Search.ToString(), objSearchTribute, StateManager.State.Session);

    //        // Redirect to the Search Result page
    //        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.SearchResult.ToString()));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //}
    protected void GridViewInbox_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        errormsg.Visible = false;
        InboxMsg.Visible = false;
        divSentMsg.Visible = false;
        if (e.CommandName.Equals("Subject"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Label lblBody = (Label)GridViewInbox.Rows[index].FindControl("lblBody");
            Label lblACreated = (Label)GridViewInbox.Rows[index].FindControl("lblACreated");
            LinkButton lbtnSubject = (LinkButton)GridViewInbox.Rows[index].FindControl("lbtnSubject");
            LinkButton lbtnSender = (LinkButton)GridViewInbox.Rows[index].FindControl("lbtnSender");
            Label lblUserImage = (Label)GridViewInbox.Rows[index].FindControl("lblUserImage");
            Label lbtnMessageId = (Label)GridViewInbox.Rows[index].FindControl("lbtnMessageId");
            Label lblSendByUserId = (Label)GridViewInbox.Rows[index].FindControl("lblSendByUserId");
            Label lblParantMsgId = (Label)GridViewInbox.Rows[index].FindControl("lblParantMsgId");

            ParantMsgId = (int.Parse(lblParantMsgId.Text));
           
            if(index ==0)
                lbtnPreviousMessage.Enabled = false;
            if((index+1) == OpenInbox.Count)
                lbtnNextMessage.Enabled = false;
            if (OpenInbox.Count == 1)
            {
                lbtnPreviousMessage.Enabled = false;
                lbtnNextMessage.Enabled = false;
            }
            
            SetThreadHtml(ParantMsgId);
            HiddenField1.Value = lblSendByUserId.Text;
            SendbyUser = int.Parse(lblSendByUserId.Text);
            _presenter.GetUserProfile(lblSendByUserId.Text);
            //mailbody.InnerHtml = lblBody.Text;
          //  lblMassageDate.InnerText = lblACreated.Text;
          //  lbtnSentBy.Text = lbtnSender.Text;
            lblMessageSubject.InnerText = lbtnSubject.Text;
          //  ImageUser.ImageUrl = lblUserImage.Text;         
            Messageid = lbtnMessageId.Text;
            Messagetype = "Inbox";
            _presenter.UpdateMessageStstus(lbtnMessageId.Text, 0);         
            divtxtPostMessage.Visible = true;
            divbuttons.Visible = true;
            MultiViewInbox.ActiveViewIndex = 2;

        }
      /*  else if (e.CommandName.Equals("SelectUser"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Label lblSendByUserId = (Label)GridViewInbox.Rows[index].FindControl("lblSendByUserId");

            string param1 = "Re:";
            string param2 = lblSendByUserId.Text;            
            //ScriptManager.RegisterClientScriptBlock(GridViewInbox, GetType(), "Billing", "UserProfileModal_1(" + param2 + "," + '"' + param1 + '"' + ");", true);
            //ScriptManager.RegisterClientScriptBlock(GridViewInbox, GetType(), "Billing", "UserProfileModal_1(" + param2 + ");", true);
          //  ClientScript.RegisterStartupScript(GetType(), "Billing", "UserProfileModal_1(" + param2 + ");", true);
            
        } */
    }
    protected void GridViewInbox_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Retrieve the LinkButton control from the first column.            
            LinkButton lbtnSubject = (LinkButton)e.Row.FindControl("lbtnSubject");
            LinkButton lbtnSender = (LinkButton)e.Row.FindControl("lbtnSender");
            // Set the LinkButton's CommandArgument property with the  row's index.
            
            lbtnSubject.CommandArgument = e.Row.RowIndex.ToString();
            lbtnSender.CommandArgument = e.Row.RowIndex.ToString();

            Label lblSendByUserId = (Label)e.Row.FindControl("lblSendByUserId");
            string param2 = lblSendByUserId.Text;
            lbtnSender.Attributes.Add("onclick", "UserProfileModal_1(" + param2 + ");return false;");

        }
    }

    protected void GridViewInbox_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnSender = (LinkButton)e.Row.FindControl("lbtnSender");

            Label lblUserId = (Label)e.Row.FindControl("lblSendByUserId");
            string param2 = lblUserId.Text;
            lbtnSender.Attributes.Add("onclick", "UserProfileModal_1(" + param2 + ");return false;");
        }
    }

    protected void GridViewSentMessages_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        errormsg.Visible = false;
        InboxMsg.Visible = false;
        divSentMsg.Visible = false;
        if (e.CommandName.Equals("Subject"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Label lblBody = (Label)GridViewSentMessages.Rows[index].FindControl("lblBody");
            Label lblACreated = (Label)GridViewSentMessages.Rows[index].FindControl("lblACreated");
            LinkButton lbtnSubject = (LinkButton)GridViewSentMessages.Rows[index].FindControl("lbtnSubject");
            LinkButton lbtnSender = (LinkButton)GridViewSentMessages.Rows[index].FindControl("lbtnSender");
            Label lblUserImage = (Label)GridViewSentMessages.Rows[index].FindControl("lblUserImage");
            Label lbtnMessageId = (Label)GridViewSentMessages.Rows[index].FindControl("lbtnMessageId");
            Label lblSendByUserId = (Label)GridViewSentMessages.Rows[index].FindControl("lblSendByUserId");
            Label lblParantSentMsgId = (Label)GridViewSentMessages.Rows[index].FindControl("lblParantSentMsgId");


            SetThreadHtml(int.Parse(lblParantSentMsgId.Text));
            SendbyUser = int.Parse(lblSendByUserId.Text);
            _presenter.GetUserProfile(lblSendByUserId.Text);
          //  mailbody.InnerHtml = lblBody.Text;
         //   lblMassageDate.InnerText = lblACreated.Text;
          //  lbtnSentBy.Text = lbtnSender.Text;
            lblMessageSubject.InnerHtml = lbtnSubject.Text;
          //  ImageUser.ImageUrl = lblUserImage.Text;
            Messageid_ = lbtnMessageId.Text;
            Messagetype_ = "SentMsg";            
            MultiViewInbox.ActiveViewIndex = 2;            
            divtxtPostMessage.Visible = false;
            divbuttons.Visible = false;
        }
        /*else if (e.CommandName.Equals("SelectUser"))
        {            
            int index = int.Parse(e.CommandArgument.ToString());
            Label lblSendByUserId = (Label)GridViewSentMessages.Rows[index].FindControl("lblSendByUserId");


            string param1 = "Re:";
            string param2 = lblSendByUserId.Text;
            ScriptManager.RegisterClientScriptBlock(GridViewSentMessages, GetType(), "Billing", "UserProfileModal_1(" + param2 + ");", true);
            //ScriptManager.RegisterClientScriptBlock(GridViewSentMessages, GetType(), "Billing", "UserProfileModal_1(" + param2 + ");", true);

        } */
    }
    protected void GridViewSentMessages_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton lbtnSender = (LinkButton)e.Row.FindControl("lbtnSender");
            LinkButton lbtnSubject = (LinkButton)e.Row.FindControl("lbtnSubject");
            lbtnSender.CommandArgument = e.Row.RowIndex.ToString();
            lbtnSubject.CommandArgument = e.Row.RowIndex.ToString();

            Label lblSendByUserId = (Label)e.Row.FindControl("lblSendByUserId");
            string param2 = lblSendByUserId.Text;
            lbtnSender.Attributes.Add("onclick", "UserProfileModal_1(" + lbtnSender.Text + ");return false;");

        }
    }
    protected void lbtnInbox_Click(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        errormsg.Visible = false;
        InboxMsg.Visible = false;
        divSentMsg.Visible = false;
        _presenter.GetuserInboxTotalCount();
        _presenter.GetMailMessage(1,maxcount);
        MultiViewInbox.ActiveViewIndex = 0;
        lbtnPreviousMessage.Enabled = true;
        lbtnNextMessage.Enabled = true;
        SetCCSClass();
        SetFirstCSS();
    }
    protected void lbtnSentMessages_Click(object sender, EventArgs e)
    {
        _presenter.GetMailMessage(1, maxcount);
        _presenter.GetuserSentMessages(1, maxcount);
        _presenter.GetuserSentMessagesCount();
        MultiViewInbox.ActiveViewIndex = 1;
        lbtnPreviousMessage.Enabled = true;
        lbtnNextMessage.Enabled = true;
        SetCCSClass_();
        SetFirstCSS_();
    }
    protected void lbtnsentmessages1_Click(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        errormsg.Visible = false;
        InboxMsg.Visible = false;
        divSentMsg.Visible = false;
        _presenter.GetuserSentMessages(1, maxcount);
        _presenter.GetuserSentMessagesCount();
        MultiViewInbox.ActiveViewIndex = 1;
        lbtnPreviousMessage.Enabled = true;
        lbtnNextMessage.Enabled = true;
        SetCCSClass_();
        SetFirstCSS_();
    }
    protected void lbtnMarkasRead_Click(object sender, EventArgs e)
    {
        if (!GetSelectedValue().Equals(string.Empty))
        {
            _presenter.UpdateMessageStstus(GetSelectedValue(), 0);
            _presenter.GetMailMessage(1, maxcount);
            ddlSelectInbox.SelectedIndex = 0;
            setmessage("<h2>Message(s) marked read</h2><P>Selected mesages marked as read.</P>",2);
        }
        else
        {
            setmessage("<h2>Oops - there is a problem in inbox.</h2> <h3>Please correct the errors below:</h3><ul><li>Select messages to mark as read.</li></ul>",1);
            
        }
    }
    protected void lbtnMarkasUnRead_Click(object sender, EventArgs e)
    {
        if (!GetSelectedValue().Equals(string.Empty))
        {
            _presenter.UpdateMessageStstus(GetSelectedValue(), 1);
            _presenter.GetMailMessage(1, maxcount);
            ddlSelectInbox.SelectedIndex = 0;
            setmessage("<h2>Message(s) marked unread</h2><P>Selected mesages marked as unread.</P>",2);
        }
        else
        {
            setmessage("<h2>Oops - there is a problem in inbox.</h2> <h3>Please correct the errors below:</h3><ul><li>Select messages to mark as unread.</li></ul>", 1);
            
        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
     
        if (!GetSelectedValue().Equals(string.Empty))
        {
            _presenter.DeleteMessages(GetSelectedValue(), true);
            _presenter.GetuserInboxTotalCount();
            _presenter.GetMailMessage(1, maxcount);
            ddlSelectInbox.SelectedIndex = 0;
            setmessage("<h2>Message(s) deleted</h2><P>Selected messages are deleted.</P>",2);
        }
        else
        {
            setmessage("<h2>Oops - there is a problem in inbox.</h2> <h3>Please correct the errors below:</h3><ul><li>Select messages to delete.</li></ul>", 1);
            
        }
    }
    protected void lbtnInbox2_Click(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        errormsg.Visible = false;
        InboxMsg.Visible = false;
        divSentMsg.Visible = false;
        _presenter.GetuserInboxTotalCount();
        _presenter.GetMailMessage(1, maxcount);        
        MultiViewInbox.ActiveViewIndex = 0;
        lbtnPreviousMessage.Enabled = true;
        lbtnNextMessage.Enabled = true;
        SetCCSClass();
        SetFirstCSS();
    }
    protected void lbtnPreviousMessage_Click(object sender, EventArgs e)
    {
        if (divtxtPostMessage.Visible == true)
        {
            if (OpenInbox.Count > 1)
            {
                SetPreviousValue(Messageid, Messagetype, OpenInbox);
            }
            else
            {
                lbtnPreviousMessage.Enabled = false;
            }
        }
        else
        {
            if (OpenInbox_.Count > 1)
            {
                SetPreviousValue(Messageid_, Messagetype_, OpenInbox_);
            }
            else
            {
                lbtnPreviousMessage.Enabled = false;
            }
        }
    }
    protected void lbtnNextMessage_Click(object sender, EventArgs e)
    {
        if (divtxtPostMessage.Visible == true)
        {
            if (OpenInbox.Count > 1)
            {
                SetNextValue(Messageid, Messagetype, OpenInbox);
                
            }
            else
            {
                lbtnNextMessage.Enabled = false;
            }
        }
        else
        {
            if (OpenInbox_.Count > 1)
            {
                SetNextValue(Messageid_, Messagetype_, OpenInbox_);
            }
            else
            {
                lbtnNextMessage.Enabled = false;
            }
        }
    }
    protected void lbtnMarkUnReadOpenMsg_Click(object sender, EventArgs e)
    {
        _presenter.UpdateMessageStstus(Messageid, 1);
        _presenter.GetMailMessage(1, maxcount);
        ddlSelectInbox.SelectedIndex = 0;
        //SetVisibility(false);
        MultiViewInbox.ActiveViewIndex = 0;
        setmessage("<h2>Message marked unread</h2><P>Messages marked as unread.</P>",2);
        //ShowMessage("Message is marked unread successfully.", false);
    }
    protected void lbtnDeleteOpenMsg_Click(object sender, EventArgs e)
    {
        try
        {
            _presenter.DeleteMessages(Messageid, true);
            _presenter.GetMailMessage(1, maxcount);
            _presenter.GetuserInboxTotalCount();
            ddlSelectInbox.SelectedIndex = 0;
            MultiViewInbox.ActiveViewIndex = 0;
           // this._presenter.GetMailMessage(1, maxcount);
            setmessage("<h2>Message deleted</h2><P>Messages is deleted.</P>",2);
         
        }
        catch (Exception ex)
        {
            setmessage("<h2>Oops - there is a problem in message deletion.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + ".</li></ul>", 1);
         
        }
    }
    protected void ltnSendMessages_Click(object sender, EventArgs e)
    {   

        try
        {
            _presenter.SendMail_(ParantMsgId);
            txtPostMessage.Text = string.Empty;
            MultiViewInbox.ActiveViewIndex = 0;
            this._presenter.GetMailMessage(1, maxcount);
            setmessage("<h2>Message delivered</h2><P>Your message was sent successfully.</P>", 5);
        }
        catch(Exception ex)
        {
            setmessage("<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>" + ex.Message + ".</li></ul>", 1);
         
        }
    }
    protected void ddlSelectSent_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridViewSentMessages.Rows)
        {
            if (ddlSelectSent.SelectedItem.Text == "All")
            {
                CheckBox cbxinbox = (CheckBox)row.FindControl("cbxinbox");
                cbxinbox.Checked = true;
            }
            else if (ddlSelectSent.SelectedItem.Text == "None")
            {
                CheckBox cbxinbox = (CheckBox)row.FindControl("cbxinbox");
                cbxinbox.Checked = false;
            }
        }
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        if (!GetSelectedSentValue().Equals(string.Empty))
        {
            _presenter.DeleteSentMessages(GetSelectedSentValue(), true);
            _presenter.GetuserSentMessagesCount();
            _presenter.GetuserSentMessages(1,maxcount);
             ddlSelectSent.SelectedIndex = 0;
             setmessage("<h2>Message(s) deleted</h2><P>Selected messages are deleted.</P>", 2);
        }
        else
        {
            setmessage("<h2>Oops - there is a problem in sent messages.</h2> <h3>Please correct the errors below:</h3><ul><li>Select messages to delete.</li></ul>",1);
         
        }
    }
    protected void ddlSelectInbox_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridViewInbox.Rows)
        {
            if (ddlSelectInbox.SelectedItem.Text == "All")
            {
                CheckBox cbxinbox = (CheckBox)row.FindControl("cbxinbox");
                cbxinbox.Checked = true;
            }
            else if (ddlSelectInbox.SelectedItem.Text == "None")
            {
                CheckBox cbxinbox = (CheckBox)row.FindControl("cbxinbox");
                cbxinbox.Checked = false;
            }
        }
    }
    protected void popuplbtnSendMessage_Click(object sender, EventArgs e)
    {
       
       
        try
        {
            this._presenter.SendMail();
           // txtarUserProfileMsg.Text = string.Empty;
            this._presenter.GetuserSentMessages(1,maxcount);
            MultiViewInbox.ActiveViewIndex = 1;

        }
        catch
        {
            //ShowMessage("There is a problem in mail sending,pls try later.", true);
        }
    }

    #region IUserInbox Members

    public int UserId
    {
        get
        {
            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
                UserID = objSessionvalue.UserId;

            return UserID;
        }
    }

    public int SendbyUserId
    {
        get
        {
            return SendbyUser;
        }
    }

    public string Subject
    {
        get
        {
            //if (txtSubject.Text.Length > 0)
            //    return txtSubject.Text;
            //else
                return "No Subject.";
        }
    }

    public string EmailBody
    {
        get {
            return txtPostMessage.Text;
        }
    }

    public IList<MailMessage> UserInbox
    {
        set
        {
            if (value.Count > 0)
            {                
                OpenInbox = null;
                GridViewInbox.DataSource = value;
                GridViewInbox.DataBind();
                GridViewInboxRowFormating();
                OpenInbox = value;
                inboxtools.Visible = true;
                if (value.Count >= maxcount)                
                    paging.Visible = true;
                else
                    paging.Visible = false;
                if (startindex >= maxcount)
                    paging.Visible = true;
                
                
            }
            else
            {
                GridViewInbox.DataSource = value;
                GridViewInbox.DataBind();
                setmessage("<br/><br/><br/><br/><br/><br/><br/><br/><br/><center>You do not have any messages.</center>", 3);
                inboxtools.Visible=false;
                paging.Visible = false;
            }
        }
    }

    private void setmessage(string msg,int type)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        if (type == 1)
        {
            errormsg.Attributes.Add("class", "yt-Error");
            errormsg.InnerHtml = msg;
            errormsg.Visible = true;
        }
        //else if (type == 2)
        //{
        //    errormsg.Attributes.Add("class", "yt-Notice");
        //    errormsg.InnerHtml = msg;
        //    errormsg.Visible = true;
        //}
        else if (type == 3)
        {
            InboxMsg.InnerHtml = msg;
            InboxMsg.Visible = true;
        }
        else if (type == 4)
        {
            divSentMsg.InnerHtml = msg;
            divSentMsg.Visible = true;
        }
        else if (type == 5)
        {
            errormsg.Attributes.Add("class", "yt-Notice");
            errormsg.InnerHtml = msg;
            errormsg.Visible = true;
        }
        
    }
    private void GridViewInboxRowFormating()
    {
        foreach (GridViewRow row in GridViewInbox.Rows)
        {
            Label lblStatus = (Label)row.FindControl("lblStatus");
            if (lblStatus.Text == "1")
            {
                row.CssClass = "yt-Inbox-Unread";

            }
        }
    }

    public IList<MailMessage> UserSentItem
    {
        set
        {
            if (value.Count > 0)
            {
                OpenInbox_ = null;
                
                GridViewSentMessages.DataSource = value;
                GridViewSentMessages.DataBind();
                OpenInbox_ = value;
                senttools.Visible = true;                
                if (value.Count >= maxcount)
                    paging1.Visible = true;
                else
                    paging1.Visible = false;
                if (startindex2 >= maxcount)
                    paging1.Visible = true;
                
            }
            else
            {
                GridViewSentMessages.DataSource = value;
                GridViewSentMessages.DataBind();
                senttools.Visible = false;
                paging1.Visible = false;
                //setmessage("<ul><li>You dont have any message in Sent items.</li></ul>",1);
                setmessage("<br/><br/><br/><br/><br/><br/><br/><br/><br/><center>You do not have any sent messages.</center>", 4);
            }
        }
    }

    #endregion

    private string GetSelectedValue()
    {
        string _selectedValues = String.Empty;
        foreach (GridViewRow row in GridViewInbox.Rows)
        {
            CheckBox cbxinbox = (CheckBox)row.FindControl("cbxinbox");
            if (cbxinbox.Checked)
            {
                Label lbtnMessageId = (Label)row.FindControl("lbtnMessageId");
                if (_selectedValues.Equals(string.Empty))
                {

                    _selectedValues = lbtnMessageId.Text;
                }
                else
                {
                    _selectedValues += "," + lbtnMessageId.Text;
                }
            }
        }
        return _selectedValues;

    }
    private string GetSelectedSentValue()
    {
        string _selectedValues = String.Empty;
        foreach (GridViewRow row in GridViewSentMessages.Rows)
        {
            CheckBox cbxinbox = (CheckBox)row.FindControl("cbxinbox");
            if (cbxinbox.Checked)
            {
                Label lbtnMessageId = (Label)row.FindControl("lbtnMessageId");
                if (_selectedValues.Equals(string.Empty))
                {

                    _selectedValues = lbtnMessageId.Text;
                }
                else
                {
                    _selectedValues += "," + lbtnMessageId.Text;
                }
            }
        }
        return _selectedValues;

    }
    private void SetPreviousValue(string msgid, string type, IList<MailMessage> OpenInbox2)
    {
        txtPostMessage.Text = string.Empty;
        ArrayList alist = new ArrayList();
        int index = 0;
        if (OpenInbox2.Count > 0)
        {
            for (int i = 0; i < OpenInbox2.Count; i++)
            {
                alist.Add(OpenInbox2[i].MessageId.ToString());
            }
        }
        if (alist.Contains(msgid))
        {
            index = alist.IndexOf(msgid);
            index -= 1;
            if (index == 0)
            {
                lbtnPreviousMessage.Enabled = false;
            }

        }
        if (index > -1)
        {

            lbtnNextMessage.Enabled = true;
            string value = alist[index].ToString();
            if (type.Equals("Inbox"))
                SetInboxData(value);
            else
                SetSentData(value);
        }
        else
        {
            lbtnPreviousMessage.Enabled = false;
        }
    }

    private void SetNextValue(string msgid, string type, IList<MailMessage> OpenInbox1)
    {
        txtPostMessage.Text = string.Empty;
        ArrayList alist = new ArrayList();
        int index = 0;
        if (OpenInbox1.Count > 0)
        {
            for (int i = 0; i < OpenInbox1.Count; i++)
            {
                alist.Add(OpenInbox1[i].MessageId.ToString());
            }

        }
        if (alist.Contains(msgid))
        {
            index = alist.IndexOf(msgid);
            index += 1;
        }
        if (index < OpenInbox1.Count)
        {
            if (OpenInbox1.Count - 1 == index)
            {
                lbtnNextMessage.Enabled = false;
            }
            lbtnPreviousMessage.Enabled = true;
            string value = alist[index].ToString();
            if (type.Equals("Inbox"))
                SetInboxData(value);
            else
                SetSentData(value);
        }
        else
        {
            lbtnNextMessage.Enabled = false;
        }
    }

    private void SetInboxData(string value)
    {
        foreach (GridViewRow row in GridViewInbox.Rows)
        {
            Label lbtnMessageId = (Label)row.FindControl("lbtnMessageId");
            if (lbtnMessageId.Text.Equals(value))
            {

                Label lblBody = (Label)row.FindControl("lblBody");
                Label lblACreated = (Label)row.FindControl("lblACreated");
                LinkButton lbtnSubject = (LinkButton)row.FindControl("lbtnSubject");
                LinkButton lbtnSender = (LinkButton)row.FindControl("lbtnSender");
                Label lblUserImage = (Label)row.FindControl("lblUserImage");
                Label lblSendByUserId = (Label)row.FindControl("lblSendByUserId");
                Label lblParantMsgId = (Label)row.FindControl("lblParantMsgId");


                SetThreadHtml(int.Parse(lblParantMsgId.Text));
                HiddenField1.Value = lblSendByUserId.Text;
                SendbyUser = int.Parse(lblSendByUserId.Text);
                _presenter.GetUserProfile(lblSendByUserId.Text);
               // mailbody.InnerHtml = lblBody.Text;
               // lblMassageDate.InnerText = lblACreated.Text;
               // lbtnSentBy.Text = lbtnSender.Text;
                lblMessageSubject.InnerText = lbtnSubject.Text;
               // ImageUser.ImageUrl = lblUserImage.Text;                
                Messageid = lbtnMessageId.Text;
                Messagetype = "Inbox";
                _presenter.UpdateMessageStstus(lbtnMessageId.Text, 0);                
                MultiViewInbox.ActiveViewIndex = 2;
            }
        }

    }
    private void SetSentData(string value)
    {
        foreach (GridViewRow row in GridViewSentMessages.Rows)
        {
            Label lbtnMessageId = (Label)row.FindControl("lbtnMessageId");
            if (lbtnMessageId.Text.Equals(value))
            {

                Label lblBody = (Label)row.FindControl("lblBody");
                Label lblACreated = (Label)row.FindControl("lblACreated");
                LinkButton lbtnSubject = (LinkButton)row.FindControl("lbtnSubject");
                LinkButton lblSender = (LinkButton)row.FindControl("lbtnSender");
                Label lblUserImage = (Label)row.FindControl("lblUserImage");
                Label lblSendByUserId = (Label)row.FindControl("lblSendByUserId");

                Label lblParantSentMsgId = (Label)row.FindControl("lblParantSentMsgId");

                SetThreadHtml(int.Parse(lblParantSentMsgId.Text));

                SendbyUser = int.Parse(lblSendByUserId.Text);
                _presenter.GetUserProfile(lblSendByUserId.Text);
              //  mailbody.InnerHtml = lblBody.Text;
             //   lblMassageDate.InnerText = lblACreated.Text;
              //  lbtnSentBy.Text = lblSender.Text;
                lblMessageSubject.InnerText = lbtnSubject.Text;
               // ImageUser.ImageUrl = lblUserImage.Text;                
                Messageid_ = lbtnMessageId.Text;
                Messagetype_ = "SentMsg";
                _presenter.UpdateMessageStstus(lbtnMessageId.Text, 0);                
                MultiViewInbox.ActiveViewIndex = 2;
            }
        }

    }
    protected void lbtnSentBy_Click(object sender, EventArgs e)    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        errormsg.Visible = false;
        InboxMsg.Visible = false;
        
        string param2 = SendbyUser.ToString();
     //   ScriptManager.RegisterClientScriptBlock(lbtnSentBy, GetType(), "Billing", "UserProfileModal_1(" + param2 + ");", true);

    }

    #region IUserInbox Members

    public string PostMessage
    {
        get {
            return "<P>";
        }
    }

    public int ToUserId
    {
        get
        {
            return int.Parse(hfToUserid.Value);
        }
    }

    #endregion

    #region IUserInbox Members


    public string Subject1
    {
        get
        {
            return  lblMessageSubject.InnerText;
        }
    }

    #endregion

    #region IUserInbox Members


    public bool mytribute
    {
        set {
            this.Master.FindControl("limytribute").Visible = value;
            Session["mytribute"] = value;
        }
    }

    public bool myfavourite
    {
        set
        {
            this.Master.FindControl("limyfavourite").Visible = value;
            Session["myfavourite"] = value;
        }
    }

    #endregion   

    static int startindex = 0;
    static int Startcount = 0;
    static int Startcount_ = 0;
    public int TotalCount
    {
        set
        {
            startindex = value;
            SetPaging(value);
        }
    }    


    private void SetPaging(int _TotalCount)
    {
        int Size_myEvents = int.Parse(WebConfig.Size_myInbox);
        double _count = double.Parse(_TotalCount.ToString()) / double.Parse(maxcount.ToString());
        _count = Math.Ceiling(_count);
        int count = int.Parse(_count.ToString());
        Startcount = count;
        ArrayList alist = new ArrayList();
        if (count > Size_myEvents)
        {
            counter = Size_myEvents;
            for (int i = 1; i <= Size_myEvents; i++)
            {
                alist.Add(i);
            }
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            SetFirstCSS();
            lbtnnext.Visible = true;
            lbtnPrev.Visible = false;
            paging.Visible = true;
        }
        else if (count == Size_myEvents)
        {
            counter = Size_myEvents;
            for (int i = 1; i <= Size_myEvents; i++)
            {
                alist.Add(i);
            }
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            SetFirstCSS();
            lbtnnext.Visible = false;
            lbtnPrev.Visible = false;
            paging.Visible = true;
        }
        else
        {
            paging.Visible = false;
        }
    }


    
    static int pagenumber = 0;
    protected void Paginglist_ItemCommand(object source, DataListCommandEventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        SetCCSClass();
        LinkButton lbtncount = (LinkButton)e.Item.FindControl("lbtncount");
        int count = int.Parse(lbtncount.Text);
        pagenumber = count;       
        visib = true;
        _presenter.GetMailMessage(count, maxcount);       
        lbtncount.CssClass = "yt-Selected";
        errormsg.Visible = false;
        InboxMsg.Visible = false;
        divSentMsg.Visible = false;
    }
    private void SetFirstCSS()
    {
        int i = 1;
        foreach (DataListItem item in Paginglist.Items)
        {
            if (i == 1)
            {
                LinkButton lbtncount = (LinkButton)item.FindControl("lbtncount");
                lbtncount.CssClass = "yt-Selected";
            }
            i += 1;
        }
    }

    private void SetCCSClass()
    {
        foreach (DataListItem item in Paginglist.Items)
        {
            LinkButton lbtncount = (LinkButton)item.FindControl("lbtncount");
            lbtncount.CssClass = "";

        }
    }


    #region IUserInbox Members

    static int startindex2 = 0;
    public int TotalCount_
    {
        set {
            startindex2 = value;
            SetPaging_(value);
        }
    }

    //private void SetPaging_(int _TotalCount)
    //{
    //    double _count = double.Parse(_TotalCount.ToString()) / double.Parse(maxcount.ToString());
    //    _count = Math.Ceiling(_count);
    //    int count = int.Parse(_count.ToString());
    //    ArrayList alist = new ArrayList();
    //    if (count >= 2)
    //    {
    //        for (int i = 1; i <= count; i++)
    //        {
    //            alist.Add(i);
    //        }
    //        Paginglist1.DataSource = alist;
    //        Paginglist1.DataBind();
    //        SetFirstCSS_();
    //    }
    //}

    private void SetPaging_(int _TotalCount)
    {
        int Size_myEvents = int.Parse(WebConfig.Size_myInbox);
        double _count = double.Parse(_TotalCount.ToString()) / double.Parse(maxcount.ToString());
        _count = Math.Ceiling(_count);
        int count = int.Parse(_count.ToString());
        Startcount_ = count;
        ArrayList alist = new ArrayList();
        if (count > Size_myEvents)
        {
            counter_ = Size_myEvents;
            for (int i = 1; i <= Size_myEvents; i++)
            {
                alist.Add(i);
            }
            Paginglist1.DataSource = alist;
            Paginglist1.DataBind();
            SetFirstCSS_();
            lbtnNext1.Visible = true;
            lbtnPrev1.Visible = false;
            paging1.Visible = true;
        }
        else if (count == Size_myEvents)
        {
            counter_ = Size_myEvents;
            for (int i = 1; i <= Size_myEvents; i++)
            {
                alist.Add(i);
            }
            Paginglist1.DataSource = alist;
            Paginglist1.DataBind();
            SetFirstCSS_();
            lbtnNext1.Visible = false;
            lbtnPrev1.Visible = false;
            paging1.Visible = true;
        }
        else
        {
            paging1.Visible = false;
        }
    }


    protected void Paginglist1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        SetCCSClass_();
        LinkButton lbtncount = (LinkButton)e.Item.FindControl("lbtncount");
        int count = int.Parse(lbtncount.Text);
        visib_ = true;
        _presenter.GetuserSentMessages(count, maxcount);       
        lbtncount.CssClass = "yt-Selected";
        errormsg.Visible = false;
        InboxMsg.Visible = false;
        divSentMsg.Visible = false;
    }

    private void SetFirstCSS_()
    {
        int i = 1;
        foreach (DataListItem item in Paginglist1.Items)
        {
            if (i == 1)
            {
                LinkButton lbtncount = (LinkButton)item.FindControl("lbtncount");
                lbtncount.CssClass = "yt-Selected";
            }
            i += 1;
        }
    }

    private void SetCCSClass_()
    {
        foreach (DataListItem item in Paginglist1.Items)
        {
            LinkButton lbtncount = (LinkButton)item.FindControl("lbtncount");
            lbtncount.CssClass = "";

        }
    }
    #endregion
    protected void lbtnPrev_Click(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        int Size_myEvents = int.Parse(WebConfig.Size_myInbox);
        counter -= Size_myEvents;
        int _pcount = 0;
        int i = 1;
        foreach (DataListItem item in Paginglist.Items)
        {
            if (i == 1)
            {
                LinkButton lbtncount = (LinkButton)item.FindControl("lbtncount");
                _pcount = int.Parse(lbtncount.Text);
            }
            i += 1;
        }
        ArrayList alist = new ArrayList();
        //        if (_pcount > 3)
        if (_pcount > Size_myEvents + 1)
        {
            for (int j = _pcount - Size_myEvents; j < _pcount; j++)
            {
                alist.Add(j);
            }
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            SetFirstCSS();
            lbtnnext.Visible = true;
            lbtnPrev.Visible = true;
        }
        if (_pcount == Size_myEvents + 1)
        {
            for (int j = _pcount - Size_myEvents; j < _pcount; j++)
            {
                alist.Add(j);
            }
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            SetCCSClass();
            _presenter.GetMailMessage(_pcount - Size_myEvents, maxcount);
            SetFirstCSS();
            errormsg.Visible = false;
            InboxMsg.Visible = false;
            lbtnnext.Visible = true;
            lbtnPrev.Visible = false;
            divSentMsg.Visible = false;
        }
    }
    protected void lbtnnext_Click(object sender, EventArgs e)
    {
        int Size_myEvents = int.Parse(WebConfig.Size_myInbox);
        int _pcount = 0;
        int i = 1;
        foreach (DataListItem item in Paginglist.Items)
        {
            if (i == Size_myEvents)
            {
                LinkButton lbtncount = (LinkButton)item.FindControl("lbtncount");
                _pcount = int.Parse(lbtncount.Text);
            }
            i += 1;
        }
        _pcount += 1;
        ArrayList alist = new ArrayList();
        if (Startcount - counter > Size_myEvents)
        {
            for (int k = _pcount; k < _pcount + Size_myEvents; k++)
            {
                alist.Add(k);
            }
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            SetCCSClass();
            _presenter.GetMailMessage(_pcount, maxcount);
            SetFirstCSS();
            lbtnnext.Visible = true;
            visib = true;
        }
        else if (Startcount - counter == Size_myEvents)
        {
            for (int k = _pcount; k < _pcount + Size_myEvents; k++)
            {
                alist.Add(k);
            }
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            SetCCSClass();
            visib = true;
            _presenter.GetMailMessage(_pcount, maxcount);
            SetFirstCSS();
            lbtnnext.Visible = false;

        }
        else
        {
            int j = 0;
            j = 1 + counter;
            alist.Add(j);
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            visib = true;
            _presenter.GetMailMessage(j, maxcount);
            SetFirstCSS();
            lbtnnext.Visible = false;

        }
        counter += Size_myEvents;
        lbtnPrev.Visible = true;
    }
    protected void lbtnNext1_Click(object sender, EventArgs e)
    {
        int Size_myEvents = int.Parse(WebConfig.Size_myInbox);
        int _pcount = 0;
        int i = 1;
        foreach (DataListItem item in Paginglist1.Items)
        {
            if (i == Size_myEvents)
            {
                LinkButton lbtncount = (LinkButton)item.FindControl("lbtncount");
                _pcount = int.Parse(lbtncount.Text);
            }
            i += 1;
        }
        _pcount += 1;
        ArrayList alist = new ArrayList();
        if (Startcount_ - counter_ > Size_myEvents)
        {
            for (int k = _pcount; k < _pcount + Size_myEvents; k++)
            {
                alist.Add(k);
            }
            Paginglist1.DataSource = alist;
            Paginglist1.DataBind();
            SetCCSClass_();
            _presenter.GetuserSentMessages(_pcount, maxcount);
            SetFirstCSS_();
            lbtnNext1.Visible = true;
            visib_ = true;
        }
        else if (Startcount_ - counter_ == Size_myEvents)
        {
            for (int k = _pcount; k < _pcount + Size_myEvents; k++)
            {
                alist.Add(k);
            }
            Paginglist1.DataSource = alist;
            Paginglist1.DataBind();
            SetCCSClass_();
            visib_ = true;
            _presenter.GetuserSentMessages(_pcount, maxcount);
            SetFirstCSS_();
            lbtnNext1.Visible = false;

        }
        else
        {
            int j = 0;
            j = 1 + counter_;
            alist.Add(j);
            Paginglist1.DataSource = alist;
            Paginglist1.DataBind();
            visib_ = true;
            _presenter.GetuserSentMessages(j, maxcount);
            SetFirstCSS_();
            lbtnNext1.Visible = false;

        }
        counter_ += Size_myEvents;
        lbtnPrev1.Visible = true;
    }
    protected void lbtnPrev1_Click(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        int Size_myInbox = int.Parse(WebConfig.Size_myInbox);
        counter_ -= Size_myInbox;
        int _pcount = 0;
        int i = 1;
        foreach (DataListItem item in Paginglist1.Items)
        {
            if (i == 1)
            {
                LinkButton lbtncount = (LinkButton)item.FindControl("lbtncount");
                _pcount = int.Parse(lbtncount.Text);
            }
            i += 1;
        }
        ArrayList alist = new ArrayList();
        if (_pcount > Size_myInbox + 1)
        {
            for (int j = _pcount - Size_myInbox; j < _pcount; j++)
            {
                alist.Add(j);
            }
            Paginglist1.DataSource = alist;
            Paginglist1.DataBind();
            SetFirstCSS_();
            lbtnNext1.Visible = true;
            lbtnPrev1.Visible = true;
        }
        if (_pcount == Size_myInbox + 1)
        {
            for (int j = _pcount - Size_myInbox; j < _pcount; j++)
            {
                alist.Add(j);
            }
            Paginglist1.DataSource = alist;
            Paginglist1.DataBind();
            SetCCSClass_();
            _presenter.GetuserSentMessages(_pcount - Size_myInbox, maxcount);
            SetFirstCSS_();
            errormsg.Visible = false;
            InboxMsg.Visible = false;
            lbtnNext1.Visible = true;
            lbtnPrev1.Visible = false;
            divSentMsg.Visible = false;
        }
    }

    #region IUserInbox Members


    public string UserAddress
    {
        set {
            string Address = value;
        }
    }

    #endregion

    ///// <summary>
    ///// This Function will set the value of the control and error messages from the resource File
    ///// Added By Parul
    ///// </summary>
    //private void SetControlsValue()
    //{
    //    try
    //    {
    //        //Text for labels from the resource file
    //        lblFindTribute.Text = ResourceText.GetString("lblFindTribute_MP");                      // Find a Tribute
    //        lblSearchFor.InnerText = ResourceText.GetString("lblSearchFor_MP");                     // Search for:
    //        //txtSearchKeyword.Text = ResourceText.GetString("txtSearchKeyword_MP");                  // Enter the name of a Tribute
    //        lblSearch_All.InnerText = ResourceText.GetString("lblSearch_All_MP");                   // All Tributes
    //        lblSearch_Anniversary.InnerText = ResourceText.GetString("lblSearch_Anniversary_MP");   // Anniversary Tributes
    //        lblSearch_Birthday.InnerText = ResourceText.GetString("lblSearch_Birthday_MP");         // Birthday Tribute
    //        lblSearch_Graduation.InnerText = ResourceText.GetString("lblSearch_Graduation_MP");     // Graduation Tributes
    //        lblSearch_Memorial.InnerText = ResourceText.GetString("lblSearch_Memorial_MP");         // Memorial Tributes
    //        lblSearch_NewBaby.InnerText = ResourceText.GetString("lblSearch_NewBaby_MP");           // New Baby Tributes
    //        lblSearch_Wedding.InnerText = ResourceText.GetString("lblSearch_Wedding_MP");           // Wedding Tributes
    //        lnkAdvanceSearch.Text = ResourceText.GetString("lnkAdvanceSearch_MP");                  // Advanced Search
    //        lnkClose.InnerText = ResourceText.GetString("lnkClose_MP");                             // Close

    //        txtSearchKeyword.Attributes.Add("onclick", "this.select();");
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void SetThreadHtml(int Parantid)
    {
        litrelMailThread.Text = string.Empty; 
        IList<MailMessage> MailThread = _presenter.GetMailThread(Parantid);
        if (MailThread.Count > 0)
        {
             StringBuilder objstrb = new StringBuilder();
            for (int i = 0; i < MailThread.Count; i++)
            {
                string Address = _presenter.GetUserAddress(MailThread[i].SendByUserId.ToString());  
                objstrb.Append("");
                objstrb.Append("<br /><br /><h5>From:</h5>");
                objstrb.Append("<div class=yt-SenderInfo>");
                objstrb.Append("<h6>");
                objstrb.Append("<a href='javascript:void(0);' onclick='UserProfileModal_1(" + MailThread[i].SendByUserId + ");' class='yt-ListName'>" + MailThread[i].SendByUser + "</a> ");
                objstrb.Append(Address+"</h6>");
                objstrb.Append("<div class='yt-ItemPhoto'>");
                objstrb.Append(" <img src='" + CommonUtilities.GetPath()[2].ToString() + MailThread[i].UserImage + "' alt='Photo' width='48' height='48' />");
                objstrb.Append("</div>");
                objstrb.Append("<p class='yt-ItemDate'>" + MailThread[i].SendDate.ToString("MMMM dd,yyyy") + "</p>");               
                objstrb.Append("</div>");
                objstrb.Append("<P>");
                objstrb.Append(MailThread[i].Body.Replace("\r\n","<BR/>"));
                objstrb.Append("</P>");

            }
            litrelMailThread.Text = objstrb.ToString();
        }
        //
    }

    protected void GridViewSentMessages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnSender = (LinkButton)e.Row.FindControl("lbtnSender");

            Label lblUserId = (Label)e.Row.FindControl("lblSendByUserId");
            string param2 = lblUserId.Text;
            lbtnSender.Attributes.Add("onclick", "UserProfileModal_1(" + param2 + ");return false;");
        }
    }
}


 
