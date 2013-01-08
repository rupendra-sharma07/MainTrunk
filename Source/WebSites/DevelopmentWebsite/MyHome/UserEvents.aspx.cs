///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.UserEvents.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays the list of events to which the user is invited to
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

public partial class MyHome_UserEvents : PageBase, IUserEvents
{
    private UserEventsPresenter _presenter;
    int UserID;
  
    static int maxcount = int.Parse(WebConfig.Pagesize_myEvents);
    //static int maxcount = 1;
    static int counter = 0;
    static bool visib = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Master.NavPrimary = Shared_Inner.AdminNavPrimaryEnum.events.ToString();
            this.Master.PageTitle = "Events";

            StateManager stateManagerP = StateManager.Instance;
            string PageName = "UserEvents";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                UserID = objSessionvalue.UserId;

                //if (GridViewEvents.Rows.Count > maxcount)
                this._presenter.GetUsereventsCount();
                this._presenter.GetUserEvevts(1, maxcount);
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }

        }
    }

    [CreateNew]
    public UserEventsPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    #region IUserEvents Members

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

    public IList<Events> MyEvents
    {
        set
        {
            if (value.Count > 0)
            {
                GridViewEvents.DataSource = value;
                GridViewEvents.DataBind();
                //if (_CountTotal > maxcount)
                //{
                //    PagingText.Visible = true;
                //    paging.Visible = true;
                //}
                //else
                //{
                //    PagingText.Visible = false;
                //    paging.Visible = false;
                //}
                //if (visib)
                //{
                //    PagingText.Visible = visib;
                //    paging.Visible = visib;
                //}

            }
            else
            {
                setmessage("<br/><br/><br/><br/><br/><br/><br/><br/><br/><center>You do not have any upcoming events.</center>", 3);
                paging.Visible = false;
            }

        }
    }


    private void setmessage(string msg, int type)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        if (type == 1)
        {
            errormsg.Attributes.Add("class", "yt-Error");
            errormsg.InnerHtml = msg;
            errormsg.Visible = true;
        }
        else if (type == 2)
        {
            errormsg.Attributes.Add("class", "yt-Notice");
            errormsg.InnerHtml = msg;
            errormsg.Visible = true;
        }
        else if (type == 3)
        {
            eventmsg.InnerHtml = msg;
            eventmsg.Visible = true;
        }
    }
    #endregion
    protected void GridViewEvents_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Retrieve the LinkButton control from the first column.
            //LinkButton addButton = (LinkButton)e.Row.Cells[0].Controls[0];

            LinkButton lbtnMyEvent = (LinkButton)e.Row.FindControl("lbtnMyEvent");


            // Set the LinkButton's CommandArgument property with the
            // row's index.

            lbtnMyEvent.CommandArgument = e.Row.RowIndex.ToString();

        }
    }
    protected void GridViewEvents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Event"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Label lbtnEventID = (Label)GridViewEvents.Rows[index].FindControl("lbtnEventID");
            Label lbltributeID = (Label)GridViewEvents.Rows[index].FindControl("lbltributeID");
            int ID = int.Parse(lbtnEventID.Text);
            int _TributeId = int.Parse(lbltributeID.Text);

            //Tributes objtribute = new Tributes();
            //objtribute.TributeId = _TributeId;            
            _presenter.GetTributeDetails(_TributeId);
            StateManager stateTribure = StateManager.Instance;
            //stateTribure.Add("TributeSession", objtribute, StateManager.State.Session);
            Tributes objTributeDetail = (Tributes)stateTribure.Get("TributeSession", StateManager.State.Session);

            //string URL=Redirect.RedirectToPage(Redirect.PageList.EventFullView.ToString()) + "?EventID=" + ID + "&TributeID=" + _TributeId;
            string URL = string.Empty;
            if (WebConfig.ApplicationMode.Equals("local"))
            {
                URL = Session["APP_PATH"].ToString() + objTributeDetail.TributeUrl + "/event.aspx?EventID=" + ID;
            }
            else
            {
                //Comment the line above and uncomment the line below for server.
                URL = "http://" + objTributeDetail.TypeDescription.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + objTributeDetail.TributeUrl + "/event.aspx?EventID=" + ID;
            }
            Response.Redirect(URL, false);
        }
    }


    #region IUserEvents Members

    static int Startcount = 0;
    protected int _CountTotal;
    public int TotalCount
    {
        set
        {
            // Startcount = value;
            _CountTotal = value;
            SetPaging(value);
        }
    }

    public IList<GetMyTributes> TributeDetail
    {
        set
        {
            Tributes objTribute = new Tributes();
            foreach (GetMyTributes obj in value)
            {
                objTribute.TributeId = obj.TributeId;
                objTribute.TributeName = obj.TributeName;
                objTribute.TypeDescription = obj.TypeDescription;
                objTribute.TributeUrl = obj.TributeUrl;
            }
            StateManager stateTribure = StateManager.Instance;
            stateTribure.Add("TributeSession", objTribute, StateManager.State.Session);
        }
    }

    #endregion

    //private void SetPaging(int _TotalCount)
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
    //        Paginglist.DataSource = alist;
    //        Paginglist.DataBind();
    //        SetFirstCSS();
    //    }
    //}

    private void SetPaging(int _TotalCount)
    {
        int Size_myEvents = int.Parse(WebConfig.Size_myEvents);
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
            PagingText.Visible = true;
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
            PagingText.Visible = true;
            paging.Visible = true;
        }
        else if (count < Size_myEvents && count >1)
        {
            counter = count;
            for (int i = 1; i <= count; i++)
            {
                alist.Add(i);
            }
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            SetFirstCSS();
            lbtnnext.Visible = false;
            PagingText.Visible = true;
            paging.Visible = true;
        }
        else
        {
            PagingText.Visible = false;
            paging.Visible = false;
        }
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

    static int count = 0;
    protected void Paginglist_ItemCommand(object source, DataListCommandEventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        SetCCSClass();
        LinkButton lbtncount = (LinkButton)e.Item.FindControl("lbtncount");
        count = int.Parse(lbtncount.Text);
        visib = true;
        _presenter.GetUserEvevts(count, maxcount);
        lbtncount.CssClass = "yt-Selected";
        errormsg.Visible = false;
        eventmsg.Visible = false;

    }

    protected void lbtnnext_Click(object sender, EventArgs e)
    {

        int Size_myEvents = int.Parse(WebConfig.Size_myEvents);
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
            _presenter.GetUserEvevts(_pcount, maxcount);
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
            _presenter.GetUserEvevts(_pcount, maxcount);
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
            _presenter.GetUserEvevts(j, maxcount);
            SetFirstCSS();
            lbtnnext.Visible = false;

        }
        counter += Size_myEvents;
        lbtnPrev.Visible = true;
    }
    protected void lbtnPrev_Click(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        int Size_myEvents = int.Parse(WebConfig.Size_myEvents);
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
            _presenter.GetUserEvevts(_pcount - Size_myEvents, maxcount);
            SetFirstCSS();
            errormsg.Visible = false;
            eventmsg.Visible = false;
            lbtnnext.Visible = true;
            lbtnPrev.Visible = false;
        }

    }
}


