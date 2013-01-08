///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.App_Code.Redirect.cs
///Author          : 
///Creation Date   : 
///Description     : This file is used to define methods to redirect pages
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;

/// <summary>
/// Summary description for Redirect
/// </summary>
public class Redirect
{
    public Redirect()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// List of Page Name.
    /// </summary>
    public enum PageList
    {
        WeddingTribute,
        GeneralErrorPage,
        Admin2UserRegistration,
        UserReg2AdminConf,
        UserLogin2ForgotPassword,
        UserLogin2AdminConformation,
        User2ChangeEmailPassword,
        Inner2LoginPage,
        AdvanceSearchTribute,
        BasicSearchTribute,
        AddVideo,
        VideoGallery,
        Login2HomePage,
        ManageVideo,
        EmailUrl,
        ChangeSiteTheme,
        UserAccounts,
        GuestBook,
        Story,
        AdminConfirmation,
        ManageNote,
        TributeNotes,
        UnderConstruction,
        TributeCreation,
        NoteFullView,
        Gift,
        Event,
        ManageEvent,
        EventFullView,
        InviteGuest,
        ManagePhotoAlbum,
        PhotoGallery,
        PhotoView,
        ManagePhoto,
        PhotoAlbum,
        EditPhotoAlbum,
        SearchResult,
        ShareTribute
    }

    /// <summary>
    /// This function is used to Redirect to new page.
    /// </summary>
    /// <param name="PageName"></param>
    /// <returns></returns>
    public static string RedirectToPage(string PageName)
    {
        string strXmlPath = AppDomain.CurrentDomain.BaseDirectory + "Common\\XML\\Redirect.xml";

        FileStream docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        XmlDocument contactDoc = new XmlDocument();
        //Load the Xml Document
        contactDoc.Load(docIn);

        //Get a node
        XmlNodeList pagename = contactDoc.GetElementsByTagName(PageName);

        //get the value
        return pagename.Item(0).InnerText.ToString();
    }


}
