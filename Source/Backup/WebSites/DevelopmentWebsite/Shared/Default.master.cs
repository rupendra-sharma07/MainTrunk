//===============================================================================
// Microsoft patterns & practices
// Web Client Software Factory
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

#region USING DIRECTIVES
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
using TributesPortal.MultipleLangSupport;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
#endregion

public partial class DefaultMaster : System.Web.UI.MasterPage
{
    #region CLASS VARIABLES
    private string _typeName = string.Empty;
//    private SessionValue objSessionValue = null;
    #endregion

    #region EVENTS
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
           IbtnLf.Attributes.Add("onclick", "return SetSize(22);");
           IbtnMf.Attributes.Add("onclick", "return SetSize(18);");
           IbtnSf.Attributes.Add("onclick", "return SetSize(10);");
           lbtnAddToFav.Attributes.Add("onclick", "CreateBookmarkLink();");
        }
        hdnUrl.Value = Request.Url.ToString(); //for Creating a bookmark.
        
           SetMenuOptions();
        
    }

    protected void lbtnAddVideo_Click(object sender, EventArgs e)
    {
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.AddVideo.ToString()));
    }
    protected void lbtnBackToVideo_Click(object sender, EventArgs e)
    {
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.VideoGallery.ToString()));
    }
    protected void lbtnChangeSiteTheme_Click(object sender, EventArgs e)
    {
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.ChangeSiteTheme.ToString()));
    }
    protected void lbtnEmail_Click(object sender, EventArgs e)
    {
        SetValueForEmailInSession();
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.EmailUrl.ToString()), false);
    }
    protected void lbtnEditVideo_Click(object sender, EventArgs e)
    {
        Response.Redirect(Redirect.RedirectToPage(Redirect.PageList.ManageVideo.ToString()) + "?mode=edit", false);
    }
    protected void lbtnAddToFav_Click(object sender, EventArgs e)
    {
        
    }
    #endregion

    #region METHODS
    public void SetFontSize(int size)
    {
        //if (size != null)
        if (size >=0)   // by Ud
        {
            Session["size"] = size;
            string fontString = "document.getElementById('mainBody').style.fontSize='" + size + "';";
            fontString += "var elem = document.getElementById('aspnetForm').elements;";
            fontString += "for (var i = 0; i < document.forms[0].length; i++)";
            fontString += "{";
            fontString += "if(aspnetForm[i].type=='button'){aspnetForm[i].style.fontSize='" + size + "'}";
            fontString += "if(aspnetForm[i].type=='submit-one'){aspnetForm[i].style.fontSize='" + size + "'}";
            fontString += "if(aspnetForm[i].type=='checkbox'){aspnetForm[i].style.fontSize='" + size + "'}";
            fontString += "if(aspnetForm[i].type=='text'){aspnetForm[i].style.fontSize='" + size + "'}";
            fontString += "if(aspnetForm[i].type=='image'){aspnetForm[i].style.fontSize='" + size + "'}";
            fontString += "if(aspnetForm[i].type=='radio'){aspnetForm[i].style.fontSize='" + size + "'}";
            fontString += "if(aspnetForm[i].type=='label'){aspnetForm[i].style.fontSize='" + size + "'}";
            fontString += "if(aspnetForm[i].type=='password'){aspnetForm[i].style.fontSize='" + size + "'}";
            fontString += "}";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", fontString, true);
            //refreshwindow();

            //PageBase page = new PageBase();
            //page.setSize(size);
            //setSize

           
        }

    }

    //public void refreshwindow()
    //{
    //   ///string refresh = "<script language='javascript'> alert('hi')</script>";
    //   // string refresh = "alert('hi')";
    //    //Page.RegisterStartupScript("click", refresh);
    //   // ScriptManager.RegisterStartupScript(Page, this.GetType(), "", refresh, true);
    //   // string refresh = "window.location.reload();";
    //   // string refresh = "opener.location.href = opener.location.href;";
    //   // refresh += " close();";
    //   // ScriptManager.RegisterStartupScript(Page, this.GetType(), "", refresh, true);
       
    //}
    //protected void TextBox1_TextChanged(object sender, EventArgs e)
    //{

    //}
    //protected void IbtnLf_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["size"] = 22;
    //}
    //protected void IbtnMf_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["size"] = 18;
    //}
    //protected void IbtnSf_Click(object sender, ImageClickEventArgs e)
    //{
    //    Session["size"] = 14;
    //}


    
    /// <summary>
    /// Method to set the menu options for the Video Pages (Add Video, Video Gallery and Manage Video)
    /// </summary>
    /// 
    
    private void SetMenuOptions()
    {
        StateManager objStateManager = StateManager.Instance;

        #region FOR VIDEO PAGES
        if (Page.GetType().Name.ToLower() == "video_managevideo_aspx") //if page is Manage Video
        {
            _typeName = "ManageVideo";
            UserAdminOwnerInfo objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo", StateManager.State.Session);
            SetMenuItemsVisibility(_typeName, objUserInfo.IsAdmin, objUserInfo.IsOwner, objUserInfo.UserId, objUserInfo.Mode);
            SetMenuItemText(_typeName);
        }
        else if (Page.GetType().Name.ToLower() == "video_videogallery_aspx") //if page is Video Gallery
        {
            _typeName = "VideoGallery";
            UserAdminOwnerInfo objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo", StateManager.State.Session);
            SetMenuItemsVisibility(_typeName, objUserInfo.IsAdmin, objUserInfo.IsOwner, objUserInfo.UserId, string.Empty);
            SetMenuItemText(_typeName);
        }
        else if (Page.GetType().Name.ToLower() == "video_addvideo_aspx") //if page is Add Video
        {
            _typeName = "AddVideo";
            UserAdminOwnerInfo objUserInfo = (UserAdminOwnerInfo)objStateManager.Get("UserAdminOwnerInfo", StateManager.State.Session);
            SetMenuItemsVisibility(_typeName, objUserInfo.IsAdmin, objUserInfo.IsOwner, objUserInfo.UserId, string.Empty);
            SetMenuItemText(_typeName);
        }
        #endregion
        objStateManager.Add("TypeName", _typeName, StateManager.State.ViewState);
    }

    /// <summary>
    /// Method to set the visibility of controls based on the page and user profile
    /// </summary>
    /// <param name="typeName">Type of page</param>
    /// <param name="isUserAdmin">Is user admin</param>
    /// <param name="isUserOwner">Is user owner</param>
    /// <param name="userId">UserId</param>
    private void SetMenuItemsVisibility(string typeName, bool isUserAdmin, bool isUserOwner, int userId, string mode)
    {
        #region FOR MANAGE VIDEO
        if (typeName == "ManageVideo")
        {
            if (mode == "edit") //when Manage page moves to edit mode
            {
                #region FOR EDIT MODE
                login_and_signup.Visible = false;
                
                if (isUserAdmin)
                {
                    lbtnBackToVideo.Visible = true;
                    lbtnChangeSiteTheme.Visible = true;
                    lbtnAdminTributeSite.Visible = true;
                    lbtnEmail.Visible = false;
                    lbtnFacebook.Visible = false;
                    lbtnAddVideo.Visible = false;
                    lbtnEditVideo.Visible = false;
                    lbtnAddToFav.Visible = false;
                }
                else if (isUserOwner)
                {
                    lbtnBackToVideo.Visible = true;
                    lbtnAddToFav.Visible = true;
                    lbtnChangeSiteTheme.Visible = false;
                    lbtnAdminTributeSite.Visible = false;
                    lbtnEmail.Visible = false;
                    lbtnFacebook.Visible = false;
                    lbtnAddVideo.Visible = false;
                    lbtnEditVideo.Visible = false;
                }
                else
                {
                    lbtnBackToVideo.Visible = false;
                    lbtnAddToFav.Visible = false;
                    lbtnChangeSiteTheme.Visible = false;
                    lbtnAdminTributeSite.Visible = false;
                    lbtnEmail.Visible = false;
                    lbtnFacebook.Visible = false;
                    lbtnAddVideo.Visible = false;
                    lbtnEditVideo.Visible = false;
                }
                #endregion
            }
            else //when manage video page is in view mode
            {
                #region FOR VIEW MODE
                lbtnEmail.Visible = true;
                lbtnFacebook.Visible = true;
                lbtnAddVideo.Visible = false;
                lbtnBackToVideo.Visible = false;
                if (userId == 0)
                {
                    //lbtnLogin.Visible = true;
                    login_and_signup.Visible = true;
                    lbtnEditVideo.Visible = false;
                    lbtnChangeSiteTheme.Visible = false;
                    lbtnAdminTributeSite.Visible = false;
                    lbtnAddToFav.Visible = false;
                }
                else if (isUserAdmin)
                {
                    //lbtnEditVideo.Visible = true;
                    lbtnChangeSiteTheme.Visible = true;
                    lbtnAdminTributeSite.Visible = true;
                    lbtnAddToFav.Visible = false;
                    login_and_signup.Visible = false;
                    //lbtnLogin.Visible = false;
                }
                else if (isUserOwner)
                {
                    lbtnEditVideo.Visible = true;
                    lbtnAddToFav.Visible = true;
                    lbtnChangeSiteTheme.Visible = false;
                    lbtnAdminTributeSite.Visible = false;
                    login_and_signup.Visible = false;
                    //lbtnLogin.Visible = false;
                }
                else
                {
                    lbtnAddToFav.Visible = true;
                    lbtnEditVideo.Visible = false;
                    lbtnChangeSiteTheme.Visible = false;
                    lbtnAdminTributeSite.Visible = false;
                    login_and_signup.Visible = false;
                    //lbtnLogin.Visible = false;
                }
                #endregion
            }
        }
        #endregion

        #region FOR VIDEO GALLERY
        if (typeName == "VideoGallery")
        {
            lbtnEditVideo.Visible = false;
            lbtnBackToVideo.Visible = false;
            lbtnEmail.Visible = true;
            //lbtnLinkTo.Visible = true;
            lbtnFacebook.Visible = true;
            
            if (userId == 0)
            {
                //lbtnLogin.Visible = true;
                login_and_signup.Visible = true;
                lbtnAddVideo.Visible = false;
                lbtnChangeSiteTheme.Visible = false;
                lbtnAdminTributeSite.Visible = false;
                lbtnAddToFav.Visible = false;
            }
            else if (isUserAdmin)
            {
                lbtnAddVideo.Visible = true;
                lbtnChangeSiteTheme.Visible = true;
                lbtnAdminTributeSite.Visible = true;
                lbtnAddToFav.Visible = false;
                login_and_signup.Visible = false;
                //lbtnLogin.Visible = false;
            }
            else
            {
                lbtnAddToFav.Visible = true;
                lbtnAddVideo.Visible = true;
                lbtnChangeSiteTheme.Visible = false;
                lbtnAdminTributeSite.Visible = false;
                login_and_signup.Visible = false;
                //lbtnLogin.Visible = false;
            }
        }
        #endregion

        #region FOR ADD VIDEO
        if (typeName == "AddVideo")
        {
            lbtnAddVideo.Visible = false;
            lbtnEditVideo.Visible = false;
            
            if (userId == 0)
            {
                //lbtnLogin.Visible = true;
                login_and_signup.Visible = true;
                lbtnEmail.Visible = true;
                //lbtnLinkTo.Visible = true;
                lbtnFacebook.Visible = true;
                lbtnBackToVideo.Visible = false;
                lbtnChangeSiteTheme.Visible = false;
                lbtnAdminTributeSite.Visible = false;
                lbtnAddToFav.Visible = false;
            }
            else if (isUserAdmin)
            {
                lbtnBackToVideo.Visible = true;
                lbtnChangeSiteTheme.Visible = true;
                lbtnAdminTributeSite.Visible = true;
                lbtnAddToFav.Visible = false;
                //lbtnLogin.Visible = false;
                login_and_signup.Visible = false;
                lbtnEmail.Visible = false;
                //lbtnLinkTo.Visible = false;
                lbtnFacebook.Visible = false;
            }
            else
            {
                lbtnAddToFav.Visible = true;
                lbtnBackToVideo.Visible = true;
                lbtnChangeSiteTheme.Visible = false;
                lbtnAdminTributeSite.Visible = false;
                //lbtnLogin.Visible = false;
                login_and_signup.Visible = false;
                lbtnEmail.Visible = false;
                //lbtnLinkTo.Visible = false;
                lbtnFacebook.Visible = false;
            }
        }
        #endregion
    }

    /// <summary>
    /// Method to set the text for menu items.
    /// </summary>
    /// <param name="typeName">Page type</param>
    private void SetMenuItemText(string typeName)
    {
        #region FOR VIDEO PAGES
        if (typeName == "ManageVideo") //if page is Manage Video
        {
            lbtnEditVideo.Text = ResourceText.GetString("lbtnEditVideo_MV");
            lbtnEmail.Text = ResourceText.GetString("lbtnEmailVideo_MV");
            //lbtnLinkTo.Text = ResourceText.GetString("lbtnLinkToVideo_MV");
            lbtnFacebook.Text = ResourceText.GetString("lbtnFacebook_MV");
            //lbtnLogin.Text = ResourceText.GetString("lbtnLogin_MV");
            lbtnAddToFav.Text = ResourceText.GetString("lbtnAddToFav_MV");
            lbtnAdminTributeSite.Text = ResourceText.GetString("lbtnAdminTributeSite_MV");
            lbtnChangeSiteTheme.Text = ResourceText.GetString("lbtnChangeSiteTheme_MV");
            lbtnBackToVideo.Text = ResourceText.GetString("lbtnBackToVideo_MV");
            lblLogin.Text = ResourceText.GetString("lblLogin_MV_MP");
            lblOr.Text = ResourceText.GetString("lblOr_MV_MP");
        }
        else if (typeName == "VideoGallery") //if page is Video Gallery
        {
            lbtnAddVideo.Text = ResourceText.GetString("lbtnAddVideo_VG");
            lbtnEmail.Text = ResourceText.GetString("lbtnEmailVideo_VG");
            //lbtnLinkTo.Text = ResourceText.GetString("lbtnLinkToVideo_VG");
            lbtnFacebook.Text = ResourceText.GetString("lbtnFacebook_VG");
            //lbtnLogin.Text = ResourceText.GetString("lbtnLogin");
            lbtnAddToFav.Text = ResourceText.GetString("lbtnAddToFav_VG");
            lbtnAdminTributeSite.Text = ResourceText.GetString("lbtnAdminTributeSite_VG");
            lbtnChangeSiteTheme.Text = ResourceText.GetString("lbtnChangeSiteTheme_VG");
            lblLogin.Text = ResourceText.GetString("lblLogin_VG_MP");
            lblOr.Text = ResourceText.GetString("lblOr_VG_MP");
        }
        else if (typeName == "AddVideo") //if page is Add Video
        {
            lbtnBackToVideo.Text = ResourceText.GetString("lbtnBackToVideo_AV");
            lbtnChangeSiteTheme.Text = ResourceText.GetString("lbtnChangeSiteTheme_AV");
            lbtnAdminTributeSite.Text = ResourceText.GetString("lbtnAdminTributeSite_AV");
            lbtnAddToFav.Text = ResourceText.GetString("lbtnAddToFav_AV");
        }
        #endregion
    }

    /// <summary>
    /// Method to set the url to be mailed to user.
    /// </summary>
    private void SetValueForEmailInSession()
    {
        StateManager stateManager = StateManager.Instance;
        
        //TO DO: to be removed
        //SessionValue _objSessionValue = new SessionValue(1, "Gaurav", "Puri", "Gaurav Puri", "gpuri@in.sopragroup.com", 1, "Admin");
        //stateManager.Add("objSessionvalue", _objSessionValue, StateManager.State.Session);
        
        EmailLink objEmail = new EmailLink();
        objEmail.TypeName = stateManager.Get("TypeName", StateManager.State.ViewState).ToString();
        objEmail.UrlToEmail = Request.Url.ToString();
        stateManager.Add("objEmailLink", objEmail, StateManager.State.Session);
    }
    #endregion



    
}
