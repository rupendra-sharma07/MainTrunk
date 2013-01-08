///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.UserSummaryReport.aspx.cs
///Author          : 
///Creation Date   : 
///Description     : This page allows the admin to view the user summary report
///Audit Trail     : Date of Modification  Modified By         Description

#region USING DIRECTIVES
using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO.Compression;
using System.Xml;
using Microsoft.Practices.ObjectBuilder;
using TributesPortal.TributePortalAdmin.Views;
using TributesPortal.BusinessEntities;
using TributesPortal.Utilities;
using ICSharpCode.SharpZipLib.Zip;
#endregion

public partial class TributePortalAdmin_CategoryBasedThemeUpload : System.Web.UI.Page,ICategoryBasedThemeUpload 
{
    #region Variables Declarations
    private CategoryBasedThemeUploadPresenter  _presenter;  
    private const string THEMEPATH = "../assets/Themes/";
    private string _strFolderName = string.Empty;
    #endregion

    #region Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblErrorMessage.Text = string.Empty;
            bindDDL();          
            this.pnlInvitationSubCategory.Visible = false;
            pnlUploadTheme.Attributes.Add("style", "display:block;");
            UpdateThemePanel.Attributes.Add("style", "display:none;");
            DeleteThemepanel.Attributes.Add("style", "display:none;");

        }       
    }
    void bindDDL()
    {
        this._presenter.LoadCategory(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
        this._presenter.LoadSubCategory("Anniversary");
        this._presenter.LoadDeleteSubCategory(ddlThemeCategory.SelectedItem.Text.Trim());
        this._presenter.LoadUpdateSubCategory(ddlThemeUpdateCategory.SelectedItem.Text.Trim());
        ddlUpdateTheme.Items.Clear();
        ddlTheme.Items.Clear();
        ddlUpdateTheme.Items.Add("----------------");
        ddlTheme.Items.Add("----------------");
        txtThemeName.Text = string.Empty;
        txtThemeValue.Text = string.Empty;
    }

    protected void lnkAddTheme_onClick(object sender, EventArgs e)
    {
        DivAddTheme.Attributes["class"] = "DivThemeSelected";
        DivUpdateTheme.Attributes["class"] = "DivTheme";
        DivDeleteTheme.Attributes["class"] = "DivTheme";
        lnkAddTheme.Attributes.Add("style", "text-decoration:none; color:black;");
        lnkUpdateTheme.Attributes.Add("style", "text-decoration:none; color:white;");
        lnkDeleteTheme.Attributes.Add("style", "text-decoration:none; color:white;");
        pnlUploadTheme.Attributes.Add("style", "display:block;");
        UpdateThemePanel.Attributes.Add("style", "display:none;");
        DeleteThemepanel.Attributes.Add("style", "display:none;");
        lblErrorMessage.Text = string.Empty;
        bindDDL();   
        
    }

    protected void lnkUpdateTheme_onClick(object sender, EventArgs e)
    {
        DivAddTheme.Attributes["class"] = "DivTheme";
        DivUpdateTheme.Attributes["class"] = "DivThemeSelected";
        DivDeleteTheme.Attributes["class"] = "DivTheme";
        lnkAddTheme.Attributes.Add("style", "text-decoration:none; color:white;");
        lnkUpdateTheme.Attributes.Add("style", "text-decoration:none; color:black;");
        lnkDeleteTheme.Attributes.Add("style", "text-decoration:none; color:white;");
        pnlUploadTheme.Attributes.Add("style", "display:none;");
        UpdateThemePanel.Attributes.Add("style", "display:block;");
        DeleteThemepanel.Attributes.Add("style", "display:none;");
        lblUpdateErrormsg.Text = string.Empty;
        bindDDL();   
    }

    protected void lnkDeleteTheme_onClick(object sender, EventArgs e)
    {
        DivAddTheme.Attributes["class"] = "DivTheme";
        DivUpdateTheme.Attributes["class"] = "DivTheme";
        DivDeleteTheme.Attributes["class"] = "DivThemeSelected";
        lnkAddTheme.Attributes.Add("style", "text-decoration:none; color:white;");
        lnkUpdateTheme.Attributes.Add("style", "text-decoration:none; color:white;");
        lnkDeleteTheme.Attributes.Add("style", "text-decoration:none; color:black;");
        pnlUploadTheme.Attributes.Add("style", "display:none;");
        UpdateThemePanel.Attributes.Add("style", "display:none;");
        DeleteThemepanel.Attributes.Add("style", "display:block;");
        lblDeleteErrorMsg.Text = string.Empty;
        bindDDL();   
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErrorMessage.Text  = string.Empty;
        this._presenter.LoadSubCategory(ddlCategory.SelectedItem.Text.ToString());
        pnlUploadTheme.Attributes.Add("style", "display:block;");
        UpdateThemePanel.Attributes.Add("style", "display:none;");
        DeleteThemepanel.Attributes.Add("style", "display:none;");
    }

    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddTheme_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = string.Empty;
            if (fileUpload.HasFile)
            {
                string strCategory = ddlCategory.SelectedItem.Text.Trim();
                string strSubCategory = ddlSubCategory.SelectedItem.Text.Trim();
                string strThemeValue = txtThemeValue.Text.Trim();
                string catPrefix = string.Empty;
                switch (strCategory.ToLower())
                {
                    case "anniversary":
                        catPrefix = "Ann";
                        break;
                    case "birthday":
                        catPrefix = "Birth";
                        break;
                    case "graduation":
                        catPrefix = "Grad";
                        break;
                    case "memorial":
                        catPrefix = "Mem";
                        break;
                    case "new baby":
                        catPrefix = "Baby";
                        break;
                    case "wedding":
                        catPrefix = "Wed";
                        break;
                    case "video":
                        catPrefix = "Video";
                        break;
                    default:
                        break;
                }
               _strFolderName = String.Format("{0}_{1}_{2}", catPrefix, strSubCategory, strThemeValue);//(strCategory + strSubCategory + strThemeValue).ToString();

                string newPath = System.IO.Path.Combine(Server.MapPath(THEMEPATH), _strFolderName);

                filename = Path.GetFileName(fileUpload.FileName);
                FileInfo finfo = new FileInfo(fileUpload.FileName);
                string fileExtension = finfo.Extension.ToLower();
                if (fileExtension == ".zip")
                {
                    if (!Directory.Exists(newPath))
                    {
                        DirectoryInfo TargetDir = new DirectoryInfo(newPath);
                        TargetDir.Create();
                    }

                    fileUpload.SaveAs(System.IO.Path.Combine(newPath, filename));

                    UnZipFiles(System.IO.Path.Combine(newPath, filename), newPath, true);

                    _presenter.SaveTheme(WebConfig.ApplicationType);
                    

                    lblErrorMessage.Text = "Theme has been added successfully!";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                    ResetAddTheme();
                }
                else
                {
                    lblErrorMessage.Text = "Please select zip file!";
                    lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
        }
    }
    private void ResetAddTheme()
    {
        this._presenter.LoadCategory(ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower());
        this._presenter.LoadSubCategory("Anniversary");
        txtThemeName.Text = string.Empty;
        txtThemeValue.Text = string.Empty;
    }

   
    private void UnZipFiles(string zipPathAndFile, string outputFolder, bool deleteZipFile)
    {
        ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile));        
        ZipEntry theEntry;
        string tmpEntry = String.Empty;
        while ((theEntry = s.GetNextEntry()) != null)
        {
            string directoryName = outputFolder;
            string fileName = Path.GetFileName(theEntry.Name);                     
            if (fileName != String.Empty)
            {
                if (theEntry.Name.IndexOf(".ini") < 0)
                {
                    string fullPath = directoryName + "\\" + theEntry.Name;
                    fullPath = fullPath.Replace("\\ ", "\\");
                    string fullDirPath = Path.GetDirectoryName(fullPath);
                    if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                    FileStream streamWriter = File.Create(fullPath);
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
        }
        s.Close();
        if (deleteZipFile)
            File.Delete(zipPathAndFile);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddInvitationCategory_Click(object sender, EventArgs e)
    {
        this.pnlInvitationSubCategory.Visible = true;
        txtInvitationSubCategory.Text = string.Empty;      
    }
  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        if (txtInvitationSubCategory.Text.Length > 0)
        {
            ddlSubCategory.Items.Add(txtInvitationSubCategory.Text);
        }
        this.pnlInvitationSubCategory.Visible = false;
        txtInvitationSubCategory.Text = string.Empty;      
    }

       /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelCategory_Click(object sender, EventArgs e)
    {
        pnlInvitationSubCategory.Visible = false;
        lblErrorMessage.Text = string.Empty;
    }


    // Made by Ashu(04july,2011) to delete the themes.


    protected void ddlThemeCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErrorMessage.Text = string.Empty;
        this._presenter.LoadDeleteSubCategory(ddlThemeCategory.SelectedItem.Text.ToString());
        DeleteThemepanel.Attributes.Add("style", "display:block;");
        UpdateThemePanel.Attributes.Add("style", "display:none;");
        pnlUploadTheme.Attributes.Add("style", "display:none;");
        ddlTheme.Items.Clear();
        ddlTheme.Items.Add("----------------");
    }
    protected void ddlThemeSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErrorMessage.Text = string.Empty;
        if (ddlThemeSubCategory.SelectedIndex > 0)
            this._presenter.LoadThemes(ddlThemeCategory.SelectedItem.Text.ToString(), ddlThemeSubCategory.SelectedItem.Text.ToString(),WebConfig.ApplicationType);
        else
        {
            ddlTheme.Items.Clear();
            ddlTheme.Items.Add("----------------");
        }
        DeleteThemepanel.Attributes.Add("style", "display:block;");
        UpdateThemePanel.Attributes.Add("style", "display:none;");
        pnlUploadTheme.Attributes.Add("style", "display:none;");
    }


    protected void btnDeletetheme_Onclick(object sender, EventArgs e)
    {
        string strCategory = ddlThemeCategory.SelectedItem.Text.Trim();
        string strSubCategory = ddlThemeSubCategory.SelectedItem.Text.Trim();
        string strThemeName = ddlTheme.SelectedItem.Text.Trim();
        int themeId;
        int.TryParse(ddlTheme.SelectedValue.ToString(), out themeId);
        this._presenter.GetThemeFolderName(themeId);
        _strFolderName = hdnFolderName.Value;
        if (_strFolderName != "" && ddlThemeSubCategory.SelectedIndex > 0 && ddlTheme.SelectedIndex > 0)
        {
            string newPath = System.IO.Path.Combine(Server.MapPath(THEMEPATH), _strFolderName);
            if (Directory.Exists(newPath))
            {
                DeleteFolder(newPath);
                DirectoryInfo TargetDir = new DirectoryInfo(newPath);
                TargetDir.Delete();
                CreateXMLToResetAdminSession();
            }
            _presenter.DeleteTheme(themeId);
            ResetTheme();
            lblDeleteErrorMsg.Text = "Theme has been deleted successfully!";
            lblDeleteErrorMsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblDeleteErrorMsg.Text = "Theme does not exist!";
            lblDeleteErrorMsg.ForeColor = System.Drawing.Color.Red;
        }

    }
   

   private void DeleteFolder(string FolderName)
    {
        DirectoryInfo dir = new DirectoryInfo(FolderName);
        FileInfo[] getFiles = dir.GetFiles();
        DirectoryInfo[] getDir = dir.GetDirectories();
        if (getFiles.Length > 0)
        {
            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
        }
        if (getDir.Length > 0)
        {

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                DeleteFolder(di.FullName);
                di.Delete();
            }
        }
    }
    

    protected void btnCanceltheme_Onclick(object sender, EventArgs e)
    {
        ResetTheme();
    }
    private void ResetTheme()
    {
        ddlThemeCategory.SelectedIndex = 0;
        ddlThemeSubCategory.Items.Clear();
        ddlThemeSubCategory.Items.Add("----------------");
        this._presenter.LoadDeleteSubCategory(ddlThemeCategory.SelectedItem.Text.Trim());        
        ddlTheme.Items.Clear();
        ddlTheme.Items.Add("----------------");
    }


    /* Made By Ashu(05July,2011) to Update the Themes */

    protected void ddlThemeUpdateCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblUpdateErrormsg.Text = string.Empty;
        this._presenter.LoadUpdateSubCategory(ddlThemeUpdateCategory.SelectedItem.Text.ToString());
        DeleteThemepanel.Attributes.Add("style", "display:none;");
        UpdateThemePanel.Attributes.Add("style", "display:block;");
        pnlUploadTheme.Attributes.Add("style", "display:none;");
        ddlUpdateTheme.Items.Clear();
        ddlUpdateTheme.Items.Add("----------------");
    }
    protected void ddlThemeUpdateSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblUpdateErrormsg.Text = string.Empty;
        if (ddlThemeUpdateSubCategory.SelectedIndex > 0)
            this._presenter.LoadUpdateThemes(ddlThemeUpdateCategory.SelectedItem.Text.ToString(), ddlThemeUpdateSubCategory.SelectedItem.Text.ToString(),WebConfig.ApplicationType);
        else
            ddlUpdateTheme.SelectedIndex = 0;
        DeleteThemepanel.Attributes.Add("style", "display:none;");
        UpdateThemePanel.Attributes.Add("style", "display:block;");
        pnlUploadTheme.Attributes.Add("style", "display:none;");
    }

    protected void btnUpdatetheme_Onclick(object sender, EventArgs e)
    {
        string strCategory = ddlThemeUpdateCategory.SelectedItem.Text.Trim();
        string strSubCategory = ddlThemeUpdateSubCategory.SelectedItem.Text.Trim();
        string strThemeName = ddlUpdateTheme.SelectedItem.Text.Trim();
        int themeId;
        int.TryParse(ddlUpdateTheme.SelectedValue.ToString(), out themeId);
        this._presenter.GetThemeFolderName(themeId);
        _strFolderName = hdnFolderName.Value;
        if (_strFolderName != "" && ddlThemeUpdateSubCategory.SelectedIndex > 0 && ddlUpdateTheme.SelectedIndex > 0)
        {
            string newPath = System.IO.Path.Combine(Server.MapPath(THEMEPATH), _strFolderName);
            if (Directory.Exists(newPath))
            {
                DeleteFolder(newPath);

                string filename = Path.GetFileName(UpdatefileUpload.FileName);

                FileInfo finfo = new FileInfo(UpdatefileUpload.FileName);
                string fileExtension = finfo.Extension.ToLower();
                if (fileExtension == ".zip")
                {
                    if (!Directory.Exists(newPath))
                    {
                        DirectoryInfo TargetDir = new DirectoryInfo(newPath);
                        TargetDir.Create();
                    }

                    UpdatefileUpload.SaveAs(System.IO.Path.Combine(newPath, filename));

                    UnZipFiles(System.IO.Path.Combine(newPath, filename), newPath, true);
                    ResetUpdateTheme();
                    lblUpdateErrormsg.Text = "Theme has been updated successfully!";
                    lblUpdateErrormsg.ForeColor = System.Drawing.Color.Green;
                    CreateXMLToResetAdminSession();
                }
                else
                {
                    lblUpdateErrormsg.Text = "Please select zip file !";
                    lblUpdateErrormsg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        else
        {
            lblUpdateErrormsg.Text = "Theme does not exist!";
            lblUpdateErrormsg.ForeColor = System.Drawing.Color.Red;
        }
       
    }

    protected void btnUpdateCanceltheme_Onclick(object sender, EventArgs e)
    {
        ResetUpdateTheme();
    }
    private void ResetUpdateTheme()
    {
        ddlThemeUpdateCategory.SelectedIndex = 0;
        ddlThemeUpdateSubCategory.Items.Clear();
        ddlThemeUpdateSubCategory.Items.Add("----------------");
        this._presenter.LoadUpdateSubCategory(ddlThemeUpdateCategory.SelectedItem.Text.Trim());
        ddlUpdateTheme.Items.Clear();
        ddlUpdateTheme.Items.Add("----------------");
    }

    /* Made by Ashu(21july,2011) to reset the admin session */


    void CreateXMLToResetAdminSession()
    {
        int UserID;
        string UserName = "";
        string FirstName = "";
        string LastName = "";
        bool IsUserNameVisible;

        if (Session["objSessionvalueAdmin"] != null)
        {
            UserID = ((TributesPortal.BusinessEntities.SessionValue)(Session["objSessionvalueAdmin"])).UserId;
            UserName = ((TributesPortal.BusinessEntities.SessionValue)(Session["objSessionvalueAdmin"])).UserName;
            FirstName = ((TributesPortal.BusinessEntities.SessionValue)(Session["objSessionvalueAdmin"])).FirstName;
            LastName = ((TributesPortal.BusinessEntities.SessionValue)(Session["objSessionvalueAdmin"])).LastName;
            IsUserNameVisible = ((TributesPortal.BusinessEntities.SessionValue)(Session["objSessionvalueAdmin"])).IsUsernameVisiable;


            string fileName = Server.MapPath("~/Common") + "\\User.xml";
            FileStream fs;
            if (!File.Exists(fileName))
            {
                fs = File.Create(fileName);
                fs.Close();
                fs.Dispose();
                XmlTextWriter textWriter = new XmlTextWriter(fileName, null);
                textWriter.WriteStartDocument();
                textWriter.WriteStartElement("User"); textWriter.WriteEndElement();
                textWriter.WriteEndDocument();
                textWriter.Close();
            }

            AddNodes(fileName, UserID, FirstName, LastName, UserName, IsUserNameVisible);
        }
    }
    void AddNodes(string fileName, int UserID,string FirstName,string LastName, string UserName,bool IsUserNameVisible)
    {
        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                                FileShare.ReadWrite);
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(fs);
        XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "User", null);
       
        ArrayList strArr = new ArrayList();
        strArr.Add("UserID");
        strArr.Add("UserName");
        strArr.Add("FirstName");
        strArr.Add("LastName");
        strArr.Add("IsUserNameVisible");
        foreach (string str in strArr)
        {
           string  Name = str;
           XmlElement Blankelement = xmldoc.CreateElement(Name.Trim());
           if (str == "UserID")
               Blankelement.InnerText =UserID.ToString();
           else if (str == "UserName")
               Blankelement.InnerText = UserName;
           else if (str == "FirstName")
               Blankelement.InnerText = FirstName;
           else if (str == "LastName")
               Blankelement.InnerText = LastName;
           else if (str == "IsUserNameVisible")
               Blankelement.InnerText = IsUserNameVisible.ToString();
            node.AppendChild(Blankelement);
        }
        XmlNodeList nodeList = xmldoc.GetElementsByTagName("User");
        nodeList[0].AppendChild(node);

        FileStream fsxml = new FileStream(fileName, FileMode.Truncate,
                                          FileAccess.Write,
                                          FileShare.ReadWrite);
        xmldoc.Save(fsxml);
        fsxml.Close();
        fsxml.Dispose();
    }


    /* --------------------------------------------------------- */


    #endregion    

    #region Properties
    [CreateNew]
    public CategoryBasedThemeUploadPresenter  Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    #region IAdminThemeUpload Members
    
    public string ThemeName
    {
        get { return txtThemeName.Text; }
    }
   
    public string ThemeValue
    {
        get { return txtThemeValue.Text; }
    }

    public string SubCategoryName
    {

        set
        {
            ddlSubCategory.SelectedItem.Text  = value;
        }

        get
        {
            return ddlSubCategory.SelectedItem.Text.ToString();
        }
    }
   
    public string CategoryName
    {
            set
            {
                ddlCategory.SelectedItem.Text  = value;
            }

            get
            {
                return ddlCategory.SelectedItem.Text.ToString();
            }
      

    }


    public string FolderName
    {        
        get
        {
            return _strFolderName;
        }
        set
        {
            hdnFolderName.Value = value;
        }
    }
   
    public IList<Themes> CategoryList
    {
        set
        {
            ddlCategory.DataSource  = value;
            ddlCategory.DataTextField = Themes.ThemesEnum.Tributetype.ToString();
            ddlCategory.DataBind();
            ddlThemeCategory.DataSource = value;
            ddlThemeCategory.DataTextField = Themes.ThemesEnum.Tributetype.ToString();
            ddlThemeCategory.DataBind();
            ddlThemeUpdateCategory.DataSource = value;
            ddlThemeUpdateCategory.DataTextField = Themes.ThemesEnum.Tributetype.ToString();
            ddlThemeUpdateCategory.DataBind();
            
        }
    }

    public IList<Themes> SubCategoryList
    {
        set
        {
            ddlSubCategory.DataSource = value;
            ddlSubCategory.DataTextField = Themes.ThemesEnum.SubCategory.ToString();            
            ddlSubCategory.DataBind();

          

        }
    }

    public IList<Themes> SubCategoryDeleteList
    {
        set
        {
            ddlThemeSubCategory.Items.Clear();
            ddlThemeSubCategory.Items.Add("----------------");
            for (int i = 0; i < value.Count; i++)
            {
                ddlThemeSubCategory.Items.Add(new ListItem(value[i].SubCategory.ToString()));
            }
        }
    }
    public IList<Themes> SubCategoryUpdateList
    {
        set
        {
            ddlThemeUpdateSubCategory.Items.Clear();
            ddlThemeUpdateSubCategory.Items.Add("----------------");
            for (int i = 0; i < value.Count; i++)
            {
                ddlThemeUpdateSubCategory.Items.Add(new ListItem(value[i].SubCategory.ToString()));
            }

        }
    }
    public IList<Themes> ThemeUpdateList
    {
        set
        {
            ddlUpdateTheme.Items.Clear();
            ddlUpdateTheme.Items.Add("----------------");
            for (int i = 0; i < value.Count; i++)
            {
                ddlUpdateTheme.Items.Add(new ListItem(value[i].ThemeName.ToString(), value[i].ThemeId.ToString()));
            }

        }
    }
    public IList<Themes> ThemeDeleteList
    {
        set
        {
            ddlTheme.Items.Clear();
            ddlTheme.Items.Add("----------------");
            for (int i = 0; i < value.Count; i++)
            {
                ddlTheme.Items.Add(new ListItem(value[i].ThemeName.ToString(), value[i].ThemeId.ToString()));
            }
        }
    }


#endregion
    #endregion
}
