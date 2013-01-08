///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Myhome.AdminMyfavorites.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page displays all the tributes that a user has added to his favourites list
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

public partial class MyHome_AdminMyfavorites : PageBase, IAdminMyfavorites
{

    
    static string tributeType = string.Empty;
    int UserID;
    static string tributeType2 = string.Empty;
    static IList<ParameterTypesCodes> _TributeTypes2;
    static int maxcount = int.Parse(WebConfig.Pagesize_myFavourite);
    static int tributeTypeid;

    //static int maxcount = 2;
    private AdminMyfavoritesPresenter _presenter;
    [CreateNew]
    public AdminMyfavoritesPresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
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

    public IList<ParameterTypesCodes> TributeTypes2
    {
        set
        {
            if (value.Count > 0)
            {
                _TributeTypes2 = value;
            }
        }
    }
    public IList<GetMyTributes> MyFavourites
    {
        set
        {
            HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
              
            if (value.Count > 0)
            {
                gvMyFavourites.DataSource = value;
                gvMyFavourites.DataBind();
                errormsg.Visible = false;
                DropDownList ddlist = (DropDownList)gvMyFavourites.HeaderRow.FindControl("ddlFavourite");
                if (ddlist != null)
                {
                    if (ddlist.Items.Count <= 0)
                    {
                        ddlist.DataSource = _TributeTypes2;
                        ddlist.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                        ddlist.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
                        ddlist.DataBind();
                        ddlist.SelectedIndex = ddlist.Items.IndexOf(ddlist.Items.FindByText(tributeType2));
                    }
                }
                if (value.Count >= maxcount)                    
                    paging.Visible = true;
                else
                    paging.Visible = false;
                if(startindex>=maxcount)
                    paging.Visible = true;
            }
            else
            {
                if (gvMyFavourites.Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Billing", "SetHeader('<h3></h3><ul><li>No tributes found for given search tribute type " + tributeType2 + "</li></ul>',2);", true);
                    EmptyGridFix(gvMyFavourites);
                }
                else
                {
                    errormsg.Visible = true;
                    paging.Visible = false;
                }
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.NavPrimary = Shared_Inner.AdminNavPrimaryEnum.favorites.ToString();
        this.Master.PageTitle = "My Favorites";

        if (!IsPostBack)
        {
            StateManager stateManagerP = StateManager.Instance;
            string PageName = "AdminMyfavorites";
            stateManagerP.Add(PortalEnums.SessionValueEnum.SearchPageName.ToString(), PageName, StateManager.State.Session);

            StateManager stateManager = StateManager.Instance;
            SessionValue objSessionvalue = (SessionValue)stateManager.Get("objSessionvalue", StateManager.State.Session);
            if (objSessionvalue != null)
            {
                UserID = objSessionvalue.UserId;
                //_presenter.loadfavourites(1,maxcount);                
                //bool visib = bool.Parse(Session["mytribute"].ToString());
                //limytribute.Visible = visib;
                _presenter.GetMyFavouritesCount(0);
            }
            else
            {
                Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.Inner2LoginPage.ToString()));
            }
            HtmlContainerControl lblErrMsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
            if (lblErrMsg.Visible == false && Session["IsTributeDeleted"] != null && Convert.ToBoolean(Session["IsTributeDeleted"]) == true)
            {
                lblErrMsg.InnerHtml = SetHeaderMessage("Tribute has been deleted by administrator.", "<h2>Oops - there is a problem with tribute.</h2> <h3>Please correct the errors below:</h3>");
                Session["IsTributeDeleted"] = null;
                lblErrMsg.Visible = true;
            }
        }
    }

    protected void gvMyFavourites_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        if (e.CommandName.Equals("Remove"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Label hdnlTributeid = (Label)gvMyFavourites.Rows[index].FindControl("hdnlTributeid");
            LinkButton TributeName = (LinkButton)gvMyFavourites.Rows[index].FindControl("txtTributes");            
            this._presenter.DeleteMyFavourite(int.Parse(hdnlTributeid.Text), true);           
            _presenter.loadfavourites(1,maxcount,ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
            this._presenter.GetMyFavouritesCount(0);
            if (gvMyFavourites.Rows.Count <= 0)
            {
                paging.Visible = false;
            }
            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Billing", "SetHeader('<H2>Tribute deleted!</H2><P>Tribute " + TributeName.Text + "is deleted from your favourite list.</P>',1);", true);
         
          
        }
      /*  else if (e.CommandName.Equals("SelectUser"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Label lblUserId = (Label)gvMyFavourites.Rows[index].FindControl("lblUserId");
            
            string param1 = "Re:";
            string param2 = lblUserId.Text;            
            ScriptManager.RegisterClientScriptBlock(gvMyFavourites, GetType(), "Billing", "UserProfileModal_1(" + param2 + ");", true);
          
        }*/
        else if (e.CommandName.Equals("SelectTribute"))
        {
            int index = int.Parse(e.CommandArgument.ToString());
            Label lblTributeUrl = (Label)gvMyFavourites.Rows[index].FindControl("lblTributeUrl"); 
            Label hdnlTributeid = (Label)gvMyFavourites.Rows[index].FindControl("hdnlTributeid");   
            LinkButton txtTributes=(LinkButton)gvMyFavourites.Rows[index].FindControl("txtTributes");
            Label lblTypeDescription = (Label)gvMyFavourites.Rows[index].FindControl("lblTypeDescription");   
            
            Tributes objtribute = new Tributes();
            objtribute.TributeId = int.Parse(hdnlTributeid.Text);
            objtribute.TributeUrl = lblTributeUrl.Text;
            objtribute.TributeName = txtTributes.Text;
            objtribute.TypeDescription = lblTypeDescription.Text;
            StateManager stateTribure = StateManager.Instance;
            stateTribure.Add("TributeSession", objtribute, StateManager.State.Session);
            //Response.Redirect("../Tribute/TributeHomePage.aspx");

            if (WebConfig.ApplicationMode.Equals("local"))
            {
                Response.Redirect(Session["APP_PATH"].ToString() + lblTributeUrl.Text + "/");
            }
            else
            {
                //Use this line for server and comment the line written above
                Response.Redirect("http://" + lblTypeDescription.Text.Replace("New Baby", "newbaby") + "." + WebConfig.TopLevelDomain + "/" + lblTributeUrl.Text + "/");
            }
        }
    }
    protected void gvMyFavourites_Load(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");
        if (gvMyFavourites.Rows.Count > 0)
        {
            errormsg.Visible = false;
            DropDownList ddlist = (DropDownList)gvMyFavourites.HeaderRow.FindControl("ddlFavourite");
            ddlist.Attributes.Add("onchange", "HideHeader();");
            StringBuilder objstrb = new StringBuilder();
            //Notice.InnerHtml = "";
            //Notice.Attributes.Add("class", "");
            
        }
        else
        {
            //errormsg.Visible = true;
            msg.Visible = true;
            msg.InnerHtml = "<br/><br/><br/><br/><br/><br/><br/><br/><br/><center>You do not have any favorite tributes.</center>";
            paging.Visible = false;
           // ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Billing", "SetHeader('<h2>Oops - there is a problem.</h2> <h3>Please correct the errors below:</h3><ul><li>You dont have any favourite tribute.</li></ul>',2);", true);
        }
     
    }
    protected void ddlFavourite_IndexChanged(object sender, EventArgs e)
    {
        HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

        errormsg.Visible = false;
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Billing", "HideHeader();", true);
        DropDownList ddlist = (DropDownList)gvMyFavourites.HeaderRow.FindControl("ddlFavourite");
        if (ddlist != null)
        {
            if (ddlist.SelectedIndex != -1)
            {
                tributeType2 = ddlist.SelectedItem.Text;
                tributeTypeid = int.Parse(ddlist.SelectedValue);
                this._presenter.FavouriteType2(int.Parse(ddlist.SelectedValue),1,maxcount);
                this._presenter.GetMyFavouritesCount(int.Parse(ddlist.SelectedValue));
                SetCCSClass();
                SetFirstCSS();
            }
        }
    }
    protected void cbxFavEmailAlerts_CheckedChanged(object sender, EventArgs e)
    {
        HtmlContainerControl Notice = (HtmlContainerControl)this.Master.FindControl("Notice");

        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;
        Label hdnlTributeid = (Label)row.FindControl("hdnlTributeid");
        LinkButton TributeName = (LinkButton)row.FindControl("txtTributes");
        this._presenter.UpdateFavouriteEmailAlert(int.Parse(hdnlTributeid.Text), checkbox.Checked);
        //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "Billing", "SetHeader('<H2>Email alert updated!</H2><P>Email alert is updated for tribute " + TributeName.Text + "</P>',1);", true);
        if (checkbox.Checked)
            Notice.InnerHtml = SetHeaderMessage("You will now receive email updates for the tribute " + TributeName.Text, "<h2>Email alert updated!</h2>");
        else
            Notice.InnerHtml = SetHeaderMessage("You will no longer receive email updates for the tribute " + TributeName.Text, "<h2>Email alert updated!</h2>");

        Notice.Visible = true;
    }
    
    protected void lbtnSendMessage_Click(object sender, EventArgs e)
    {
        this._presenter.SendMail();
      //  txtarUserProfileMsg.Text = string.Empty;
    }
    protected void gvMyFavourites_RowCreated(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Retrieve the LinkButton control from the first column.
            //LinkButton addButton = (LinkButton)e.Row.Cells[0].Controls[0];
            //LinkButton lblACreated = (LinkButton)e.Row.FindControl("lblACreated");
            LinkButton txtTributes = (LinkButton)e.Row.FindControl("txtTributes");


            // Set the LinkButton's CommandArgument property with the
            // row's index.
            //lblACreated.CommandArgument = e.Row.RowIndex.ToString();
            txtTributes.CommandArgument = e.Row.RowIndex.ToString();

            //Label lblUserId = (Label)e.Row.FindControl("lblUserId");
            //string param2 = lblUserId.Text;
            //lblACreated.Attributes.Add("onclick", "UserProfileModal_1(" + param2 + ");return false;");

        }
    }

    protected void gvMyFavourite_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lblACreated = (LinkButton)e.Row.FindControl("lblACreated2");

            Label lblUserId = (Label)e.Row.FindControl("lblUserId");
            string param2 = lblUserId.Text;
            lblACreated.Attributes.Add("onclick", "UserProfileModal_1(" + param2 + ");return false;");
        }
    }

    #region IAdminMyfavorites Members


    public string PostMessage
    {
        get
        {
            return "<p></p>";
        }
    }

    public int ToUserId
    {
        get
        {
            return 0;
        }
    }

    public string Subject
    {
        get
        {
           
                return "No Subject.";
        }
    }

    #endregion

    #region IAdminMyfavorites Members


    public IList<GetMyTributes> MyFavourites_
    {
        set
        {
            HtmlContainerControl errormsg = (HtmlContainerControl)this.Master.FindControl("errormsg");

            gvMyFavourites.DataSource = value;
            gvMyFavourites.DataBind();
            if (value.Count > 0)
            {
              //  mesg = 1;
                errormsg.Visible = false;
                DropDownList ddlist = (DropDownList)gvMyFavourites.HeaderRow.FindControl("ddlFavourite");
                if (ddlist != null)
                {
                    if (ddlist.Items.Count <= 0)
                    {
                        ddlist.DataSource = _TributeTypes2;
                        ddlist.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                        ddlist.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
                        ddlist.DataBind();                     
                    }
                }
                //limyfavourite.Visible = true;
                //Session["myfavourite"] = true;
            }
            else
            {
                msg.InnerHtml = "<br/><br/><br/><br/><br/><br/><br/><br/><br/><center>You do not have any favorite tributes.</center>";
                msg.Visible = true;
            }
        }
    }

    #endregion

    #region IAdminMyfavorites Members


    public bool mytribute
    {
        set {
            this.Master.FindControl("limytribute").Visible = value;
            Session["mytribute"] = value;
        }
    }

    #endregion


    private void SetPaging(int _TotalCount)
    {
        double _count = double.Parse(_TotalCount.ToString()) / double.Parse(maxcount.ToString());
        _count = Math.Ceiling(_count);
        int count = int.Parse(_count.ToString());
        ArrayList alist = new ArrayList();
        if (count >= 2)
        {
            for (int i = 1; i <= count; i++)
            {
                alist.Add(i);
            }
            Paginglist.DataSource = alist;
            Paginglist.DataBind();
            SetFirstCSS();
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
        SetCCSClass();
        LinkButton lbtncount = (LinkButton)e.Item.FindControl("lbtncount");
        int count = int.Parse(lbtncount.Text);
        pagenumber = count;
        
       // _presenter.loadfavourites(count, maxcount);
        //tributeTypeid = int.Parse(ddlist.SelectedValue);
        this._presenter.FavouriteType2(tributeTypeid, count, maxcount);
        //this._presenter.GetMyFavouritesCount(tributeTypeid);
        lbtncount.CssClass = "yt-Selected";
        this.Master.FindControl("errormsg").Visible = false;
    }

    #region IAdminMyfavorites Members


    static int startindex = 0;
    public int TotalCount
    {
        set
        {
            startindex = value;
            SetPaging(value);
        }
    }

    #endregion

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
            i+=1;
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

    protected void EmptyGridFix(GridView grdView)
    {
        // normally executes after a grid load method
        if (grdView.DataSource == null)
        {

            GetMyTributes objtrib = new GetMyTributes();
            objtrib.TributeName = null;
            objtrib.TypeDescription = null;
            objtrib.Enddate = null;
            objtrib.EmailAlert = false;
            objtrib.TributeId=0;
            objtrib.UserId = 0;
            
            List<GetMyTributes> emptyTribyte = new List<GetMyTributes>();
            emptyTribyte.Add(objtrib);           
            grdView.DataSource = emptyTribyte;
            grdView.DataBind();
            DropDownList ddlist = (DropDownList)grdView.HeaderRow.FindControl("ddlFavourite");
            if (ddlist != null)
            {
                if (ddlist.Items.Count <= 0)
                {
                    ddlist.DataSource = _TributeTypes2;
                    ddlist.DataTextField = ParameterTypesCodes.Parameters.TypeDescription.ToString();
                    ddlist.DataValueField = ParameterTypesCodes.Parameters.TypeCode.ToString();
                    ddlist.DataBind();
                    ddlist.SelectedIndex = ddlist.Items.IndexOf(ddlist.Items.FindByText(tributeType2));
                }
            }

            // hide row
            grdView.Rows[0].Visible = false;
            grdView.Rows[0].Controls.Clear();
        }
        
    }

}
