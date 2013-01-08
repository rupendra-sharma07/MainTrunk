///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.AdminMytributesHome.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays all the tributes that a user has created
///Audit Trail     : Date of Modification  Modified By         Description

using System;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.MyHome.Views;
using TributesPortal.Utilities;
using TributesPortal.BusinessEntities;
using System.Collections.Generic;
using System.Text;

public partial class MyHome_AdminMytributesHome : PageBase, IAdminMytributesHome
{
    static IList<ParameterTypesCodes> _TributeTypes;
    static string tributeType = string.Empty;
    int UserID;
    static int TributeId;
    static int maxcount = int.Parse(WebConfig.Pagesize_myTributes);
    static int tributetypeid;

    //private int startindex = 1;
    private int pagenumber = 1;

    private AdminMytributesHomePresenter _presenter;

    [CreateNew]
    public AdminMytributesHomePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlContainerControl lblErrMsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        HtmlContainerControl Notice = (HtmlContainerControl)this.Master.FindControl("Notice");

        //lblErrMsg.Visible = false;
        //Notice.Visible = false;

        this.Master.NavPrimary = Shared_Inner.AdminNavPrimaryEnum.tributes.ToString();
        
        
        if (!IsPostBack)
        {
            this.Master.PageTitle = ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments" ? "My Websites" : "My Tributes";

            StateManager stateManagerP = StateManager.Instance;
            string PageName = "AdminMytributesHome";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (Request.QueryString["PageNo"] != null)
            {
                pagenumber = int.Parse(Request.QueryString["PageNo"].ToString());
            }
            else
            {
                pagenumber = 1;
            }
            if (objSessionvalue != null)
            {
                UserID = objSessionvalue.UserId;

                _presenter.onload(pagenumber, maxcount,ConfigurationManager.AppSettings["ApplicationType"]);
                _presenter.GetMyTributeCount(0);

            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }

        }

        if (lblErrMsg.Visible == false && Session["IsTributeDeleted"] != null && Convert.ToBoolean(Session["IsTributeDeleted"]) == true)
        {
            lblErrMsg.InnerHtml = SetHeaderMessage("Tribute has been deleted by administrator.", "<h2>Oops - there is a problem with tribute.</h2> <h3>Please correct the errors below:</h3>");
            Session["IsTributeDeleted"] = null;
            lblErrMsg.Visible = true;
        }

        //// Set the controls value
        //SetControlsValue();
    }

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

    protected void grvMyTributes_Load(object sender, EventArgs e)
    {
       

    }

    protected void grvMyTributes_RowCommand(object sender, GridViewCommandEventArgs e)
    {

       HtmlContainerControl lblErrMsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        HtmlContainerControl Notice = (HtmlContainerControl)this.Master.FindControl("Notice");

        lblErrMsg.Visible = false;
        Notice.Visible = false;



        int RowIndex = int.Parse(e.CommandArgument.ToString());
        Label lblTributeId = (Label)grvMyTributes.Rows[RowIndex].FindControl("lbtnTributeid");
                
        if (!_presenter.IsTributesExists(Convert.ToInt32(lblTributeId.Text)))
            {
                Session["IsTributeDeleted"] = true;
                // Added by Ashu on Oct 3, 2011 for rewrite URL 
                var sApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString();
                if (sApplicationType.ToLower() == "yourmoments")
                    Response.Redirect("~/moments.aspx");
                else
                    Response.Redirect("~/tributes.aspx");
                return;
            }

        if (e.CommandName.Equals("Select"))
        {
            //object onj=  e.CommandSource.
            int index = int.Parse(e.CommandArgument.ToString());
            Label lbtnTributeid = (Label)grvMyTributes.Rows[index].FindControl("lbtnTributeid");
            Label lblTypeDescription = (Label)grvMyTributes.Rows[index].FindControl("lblTypeDescription");
            LinkButton lbtntributeName = (LinkButton)grvMyTributes.Rows[index].FindControl("lbtntributeName");
            Label lblTributeUrl = (Label)grvMyTributes.Rows[index].FindControl("lblTributeUrl");

            LinkButton lbtnExpires1 = (LinkButton)grvMyTributes.Rows[index].FindControl("lbtnExpires");

            Session["Package"] = lblTypeDescription.Text;

            TributeId = int.Parse(lbtnTributeid.Text);
            Tributes objtribute = new Tributes();
            objtribute.TributeId = int.Parse(lbtnTributeid.Text);
            objtribute.TributeName = lbtntributeName.Text;
            objtribute.TypeDescription = lblTypeDescription.Text;
            objtribute.TributeUrl = lblTributeUrl.Text;
            //AG:
            objtribute.TributePackageType = lbtnExpires1.Text;

            Session["UName"] = objtribute.TributeName;
            StateManager stateTribure = StateManager.Instance;
            stateTribure.Add("TributeSession", objtribute, StateManager.State.Session);
            
            _presenter.UpdateTributePackage(objtribute.TributeId, objtribute.TributePackageType);

            if (lblTypeDescription.Text.ToLower() == "video" )
            {
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    Response.Redirect(Session["APP_PATH"].ToString() + "video/videotribute.aspx?tributeId=" + TributeId.ToString());
                }
                else
                {
                    //Use this line for server and comment the line written above
                    Response.Redirect("http://video." + WebConfig.TopLevelDomain + "/video/videotribute.aspx?tributeId=" + TributeId.ToString());
                }
            }

            else
            {
                Session["TributeType"] = lblTypeDescription.Text.ToString();
                if (WebConfig.ApplicationMode.Equals("local"))
                {
                    StringBuilder sbq = new StringBuilder();
                    sbq.Append("TributeType=");
                    sbq.Append(HttpUtility.UrlEncode(objtribute.TypeDescription));
                    Response.Redirect(Session["APP_PATH"].ToString() + lblTributeUrl.Text + "/?" + sbq.ToString());
                }
                else
                {
                    //Use this line for server and comment the line written above
                    Response.Redirect("http://" + lblTypeDescription.Text.Replace("New Baby", "newbaby").ToLower() + "." + WebConfig.TopLevelDomain + "/" + lblTributeUrl.Text + "/");
                }
            }
        }
        else if (e.CommandName.Equals("Manage"))
        {
            int index = int.Parse(e.CommandArgument.ToString());

            Label lbtnTributeid = (Label)grvMyTributes.Rows[index].FindControl("lbtnTributeid");
            TributeId = int.Parse(lbtnTributeid.Text);
            Session["TributeId"] = TributeId;


            LinkButton lbtntributeName = (LinkButton)grvMyTributes.Rows[index].FindControl("lbtntributeName");
            Session["lbtntributeName"] = lbtntributeName.Text;


            Label lblTypeDescription = (Label)grvMyTributes.Rows[index].FindControl("lblTypeDescription");
            Session["lblTypeDescription"] = lblTypeDescription.Text;


            Label lblACreated = (Label)grvMyTributes.Rows[index].FindControl("lblACreated");
            Session["lblACreated"] = lblACreated.Text;

            Label lblRenewaldate = (Label)grvMyTributes.Rows[index].FindControl("lblRenewaldate");
            Session["lblRenewaldate"] = lblRenewaldate.Text;

            LinkButton lbtnExpires = (LinkButton)grvMyTributes.Rows[index].FindControl("lbtnExpires");
            Session["lbtnExpires"] = lbtnExpires.Text;

            Label lblExpiredOn = (Label)grvMyTributes.Rows[index].FindControl("lblExpiredOn");
            Session["lblExpiredOn"] = lblExpiredOn.Text;

            Label txtVisits = (Label)grvMyTributes.Rows[index].FindControl("txtVisits");
            Session["txtVisits"] = txtVisits.Text;


            CheckBox cbxEmailAlerts = (CheckBox)grvMyTributes.Rows[index].FindControl("cbxEmailAlerts");
            Session["cbxEmailAlerts"] = cbxEmailAlerts.Checked;

            Session["Sentby"] = "Manage";


            Label lblTributeUrl = (Label)grvMyTributes.Rows[index].FindControl("lblTributeUrl");
            Session["lblTributeUrl"] = lblTributeUrl.Text;

            Tributes objtribute = new Tributes();
            objtribute.TributeId = int.Parse(lbtnTributeid.Text);
            objtribute.TributeName = lbtntributeName.Text;
            objtribute.TypeDescription = lblTypeDescription.Text;
            objtribute.TributeUrl = lblTributeUrl.Text;
            StateManager stateTribure = StateManager.Instance;
            stateTribure.Add("TributeSession", objtribute, StateManager.State.Session);

            // Added by Ashu on Oct 4, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "AdminMyMomentsPrivacy.aspx");
            else
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "adminmytributesprivacy.aspx");


        }
        else if (e.CommandName.Equals("Expires"))
        {
            int index = int.Parse(e.CommandArgument.ToString());

            Label lbtnTributeid = (Label)grvMyTributes.Rows[index].FindControl("lbtnTributeid");
            TributeId = int.Parse(lbtnTributeid.Text);
            Session["TributeId"] = TributeId;


            LinkButton lbtntributeName = (LinkButton)grvMyTributes.Rows[index].FindControl("lbtntributeName");
            Session["lbtntributeName"] = lbtntributeName.Text;


            Label lblTypeDescription = (Label)grvMyTributes.Rows[index].FindControl("lblTypeDescription");
            Session["lblTypeDescription"] = lblTypeDescription.Text;


            Label lblACreated = (Label)grvMyTributes.Rows[index].FindControl("lblACreated");
            Session["lblACreated"] = lblACreated.Text;

            Label lblRenewaldate = (Label)grvMyTributes.Rows[index].FindControl("lblRenewaldate");
            Session["lblRenewaldate"] = lblRenewaldate.Text;


            Label lblExpiredOn = (Label)grvMyTributes.Rows[index].FindControl("lblExpiredOn");
            Session["lblExpiredOn"] = lblExpiredOn.Text;

            LinkButton lbtnExpires = (LinkButton)grvMyTributes.Rows[index].FindControl("lbtnExpires");
            Session["lbtnExpires"] = lbtnExpires.Text;


            Label txtVisits = (Label)grvMyTributes.Rows[index].FindControl("txtVisits");
            Session["txtVisits"] = txtVisits.Text;


            CheckBox cbxEmailAlerts = (CheckBox)grvMyTributes.Rows[index].FindControl("cbxEmailAlerts");
            Session["cbxEmailAlerts"] = cbxEmailAlerts.Checked;
            Session["Sentby"] = "Expires";


            Label lblTributeUrl = (Label)grvMyTributes.Rows[index].FindControl("lblTributeUrl");
            Session["lblTributeUrl"] = lblTributeUrl.Text;

            Tributes objtribute = new Tributes();
            objtribute.TributeId = int.Parse(lbtnTributeid.Text);
            objtribute.TributeName = lbtntributeName.Text;
            objtribute.TypeDescription = lblTypeDescription.Text;
            objtribute.TributeUrl = lblTributeUrl.Text;
            StateManager stateTribure = StateManager.Instance;
            stateTribure.Add("TributeSession", objtribute, StateManager.State.Session);
            // Added by Ashu on Oct 4, 2011 for rewrite URL 
            if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "AdminMyMomentsPrivacy.aspx");
            else
                Response.Redirect(Session["APP_BASE_DOMAIN"] + "adminmytributesprivacy.aspx");

        }


    }

    protected void grvMyTributes_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Retrieve the LinkButton control from the first column.
            LinkButton lbtntributeName = (LinkButton)e.Row.FindControl("lbtntributeName");
            LinkButton lbtnExpires = (LinkButton)e.Row.FindControl("lbtnExpires");


            // Set the LinkButton's CommandArgument property with the
            // row's index.
            lbtntributeName.CommandArgument = e.Row.RowIndex.ToString();
            lbtnExpires.CommandArgument = e.Row.RowIndex.ToString();
           
        }
    }

    protected void grvMyTributes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cbxEmailAlerts_CheckedChanged(object sender, EventArgs e)
    {
        HtmlContainerControl lblErrMsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        HtmlContainerControl Notice = (HtmlContainerControl)this.Master.FindControl("Notice");

        lblErrMsg.Visible = false;
        Notice.Visible = false;
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;
        Label lbtnTributeid = (Label)row.FindControl("lbtnTributeid");
        LinkButton TributeName = (LinkButton)row.FindControl("lbtntributeName");
        this._presenter.UpdateEmailAlerts(int.Parse(lbtnTributeid.Text), checkbox.Checked);
        if (checkbox.Checked)
            Notice.InnerHtml = SetHeaderMessage("You will now receive email updates for the tribute " + TributeName.Text, "<h2>Email alert updated!</h2>");
        else
            Notice.InnerHtml = SetHeaderMessage("You will no longer receive email updates for the tribute " + TributeName.Text, "<h2>Email alert updated!</h2>");

        Notice.Visible = true;

    }

    protected void FilterDropDown_IndexChanged(object sender, EventArgs e)
    {
        HtmlContainerControl lblErrMsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        HtmlContainerControl Notice = (HtmlContainerControl)this.Master.FindControl("Notice");

        lblErrMsg.Visible = false;
        Notice.Visible = false;
        DropDownList ddlist = (DropDownList)grvMyTributes.HeaderRow.FindControl("FilterDropDown");
        if (ddlist != null)
        {
            if (ddlist.SelectedIndex != -1)
            {
                tributeType = ddlist.SelectedItem.Text;
                // startindex = (pagenumber - 1) * maxcount;   
                tributetypeid = int.Parse(ddlist.SelectedValue);
                this._presenter.TributeByType(int.Parse(ddlist.SelectedValue), 1, maxcount);
                this._presenter.GetMyTributeCount(int.Parse(ddlist.SelectedValue));
                //COMDIFFRES: (Paging implementation has been changed, hence these two functions are not used now) Two functions SetCCSClass(), SetFirstCSS() has been deleted from in .in version 
            }
        }
    }

    private void GridViewInboxRowFormating()
    {
        foreach (GridViewRow row in grvMyTributes.Rows)
        {
            LinkButton lblStatus = (LinkButton)row.FindControl("lbtnExpires");
            if (lblStatus.Text == "Expired!")
            {
                lblStatus.CssClass = "yt-WarningText";
            }
        }
    }

    #region IAdminMytributesHome Members


    public IList<GetMyTributes> Mytributes
    {
        set
        {
            HtmlContainerControl lblErrMsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
            HtmlContainerControl Notice = (HtmlContainerControl)this.Master.FindControl("Notice");

            lblErrMsg.Visible = false;
            Notice.Visible = false;
            if (value.Count > 0)
            {
                grvMyTributes.DataSource = value;
                grvMyTributes.DataBind();
                GridViewInboxRowFormating();
                // SetPaging(100);
                DropDownList ddlist = (DropDownList)grvMyTributes.HeaderRow.FindControl("FilterDropDown");
                if (ddlist != null)
                {
                    if (ddlist.Items.Count <= 0)
                    {
                        ddlist.DataSource = _TributeTypes;
                        ddlist.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                        ddlist.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
                        ddlist.DataBind();
                        ddlist.SelectedIndex = ddlist.Items.IndexOf(ddlist.Items.FindByText(tributeType));
                    }
                }
                this.Master.FindControl("limytribute").Visible = true;
            }
            else
            {
                EmptyGridFix(grvMyTributes);
            }
        }
    }

    #endregion

    #region IAdminMytributesHome Members


    public IList<ParameterTypesCodes> TributeTypes
    {
        set
        {
            if (value.Count > 0)
            {
                _TributeTypes = value;
            }
        }
    }

    #endregion

    #region IAdminMytributesHome Members

    public IList<GetMyTributes> Mytributes_
    {

        set
        {
            HtmlContainerControl lblErrMsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
            HtmlContainerControl Notice = (HtmlContainerControl)this.Master.FindControl("Notice");

            lblErrMsg.Visible = false;
            Notice.Visible = false;
            if (value.Count > 0)
            {
                grvMyTributes.DataSource = value;
                grvMyTributes.DataBind();
                GridViewInboxRowFormating();
                DropDownList ddlist = (DropDownList)grvMyTributes.HeaderRow.FindControl("FilterDropDown");
                if (ddlist != null)
                {
                    if (ddlist.Items.Count <= 0)
                    {
                        ddlist.DataSource = _TributeTypes;
                        ddlist.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                        ddlist.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
                        ddlist.DataBind();
                        //ddlist.SelectedIndex = ddlist.Items.IndexOf(ddlist.Items.FindByText(tributeType));
                    }
                }
                this.Master.FindControl("limytribute").Visible = true;
                Session["mytribute"] = true;
            }
            else
            {
                this.Master.FindControl("limytribute").Visible = false;
                Session["mytribute"] = false;
                Response.Redirect("~/favorites.aspx");
            }

        }

    }

    #endregion

    #region IAdminMytributesHome Members


    public bool myfavourite
    {
        set
        {
            this.Master.FindControl("limyfavourite").Visible = value;
            Session["myfavourite"] = value;
        }
    }

    #endregion

    #region IAdminMytributesHome Members

    public int TotalCount
    {
        set
        {
            SetPaging(value);
        }
    }
    #endregion

    /// <summary>
    /// set the correct paging values and display next/previous buttons accordingly
    /// </summary>
    /// <param name="_TotalCount"></param>
    private void SetPaging(int _TotalCount)
    {
        //COMDIFFRES: (Paging implementation has been changed, hence the function definition)this function definition is different from .com version 
        int pageCount = 0;
        int pagesize = int.Parse(WebConfig.PagingSize_guestBook);
        if (_TotalCount % pagesize == 0)
        {
            pageCount = _TotalCount / pagesize;
        }
        else
        {
            pageCount = (_TotalCount / pagesize) + 1;
        }

        CommonUtilities objUtilities = new CommonUtilities();
        // Added by Ashu on Oct 3, 2011 for rewrite URL 
        var sApplicationType = ConfigurationManager.AppSettings["ApplicationType"].ToString();
        if (sApplicationType.ToLower() == "yourmoments")
            paging.InnerHtml = objUtilities.DrawPaging(pagenumber, pageCount, "moments.aspx");
        else
            paging.InnerHtml = objUtilities.DrawPaging(pagenumber, pageCount, "tributes.aspx");
     


    }

    //COMDIFFRES: Paginglist_ItemCommand() has been deleted from current version

    protected void EmptyGridFix(GridView grdView)
    {
        // normally executes after a grid load method
        if (grdView.DataSource == null)
        {

            GetMyTributes objtrib = new GetMyTributes();
            objtrib.TributeName = null;
            objtrib.TypeDescription = null;
            objtrib.Enddate = null;
            objtrib.StartDate = DateTime.Now;
            objtrib.Renewaldate = DateTime.Now;
            objtrib.Visit = 0;
            objtrib.TributeUrl = null;
            objtrib.EmailAlert = false;
            objtrib.TributeId = 0;
            objtrib.UserId = 0;

            List<GetMyTributes> emptyTribyte = new List<GetMyTributes>();
            emptyTribyte.Add(objtrib);
            grdView.DataSource = emptyTribyte;
            grdView.DataBind();
            DropDownList ddlist = (DropDownList)grdView.HeaderRow.FindControl("FilterDropDown");
            if (ddlist != null)
            {
                if (ddlist.Items.Count <= 0)
                {
                    ddlist.DataSource = _TributeTypes;
                    ddlist.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                    ddlist.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
                    ddlist.DataBind();
                    ddlist.SelectedIndex = ddlist.Items.IndexOf(ddlist.Items.FindByText(tributeType));
                }
            }

            // hide row
            grdView.Rows[0].Visible = false;
            grdView.Rows[0].Controls.Clear();
        }

    }
}
