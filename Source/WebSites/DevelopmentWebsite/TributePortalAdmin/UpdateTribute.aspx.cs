///Copyright       : Copyright (c) Optimus India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.TributePortalAdmin.UpdateTribute.aspx.cs
///Author          : Laxman Hari Kulshrestha
///Creation Date   : 24/07/2012
///Description     : This page allows the admin to update an existing tribute
///Audit Trail     : Date of Modification  Modified By         Description


using System;
using System.Web.UI;
using TributesPortal.Utilities;
using TributesPortal.TributePortalAdmin.Views;
using System.Collections.Generic;
using TributesPortal.BusinessEntities;
using Microsoft.Practices.ObjectBuilder;
using System.Text.RegularExpressions;

public partial class TributePortalAdmin_UpdateTribute : PageBase, IUpdateTribute
{
    #region Variables Declarations
    private UpdateTributePresenter _presenter;
    private const string THEMEPATH = "../assets/Themes/";
    private string _strFolderName = string.Empty;
    private int _tributeid;
    private string objNewExpiry;
    private UpdateTribute _objUpdateTribute;

    private string _expiryDate;

    private string _changeType;
    private DateTime _modifiedDate;
    private string _oldValue;
    private string _newValue;
    private DateTime _newdate;
    private DateTime _oldDate;
    private int _oldPackageId;
    private int _newPackageId;
    private string _oldTributeType;
    private string _newTributeType;
    private int _newTributeTypeId;
    private string _newTributeUrl;
    private DateTime? _NewPackageUpdateDate;
    private string _PackageName;
    private bool _updateStatus;
    private AdminTributeUpdate _objAdminTributeUpdate;
    private IList<AdminTributeUpdate> _objAdminTributeUpdateList;
    public string appWord = WebConfig.ApplicationWordForInternalUse;
    private bool _dirError;
    private bool _dbError;
    bool IsVideoTribute;
    
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Update " + WebConfig.ApplicationWordForInternalUse;
        if (!Page.IsPostBack)
        {
            TableCommon.Visible = true;
            TableDetails.Visible = false;
            trGridDetails.Visible = false;
            trExpiryMsg.Visible = false;
            trUpdateMsg.Visible = false;
            UpdateExpiryOn();
            lblDate1.Text = "Date 1 - ";
            lblDate2.Text = "Date 2 - ";
        }
        lblErrorUpdateExpiry.Text = "";
    }

    protected void lnkUpdateExpiry_onClick(object sender, EventArgs e)
    {
        pnlUpdateExpiry.Visible = false;
        divExpiry.Visible = true;
        divSwitch.Visible = false;
        divPackage.Visible = false;
        TableCommon.Visible = true;
        TableDetails.Visible = false;
        UpdateExpiryOn();
        ResetAll();
    }

    protected void lnkSwitchTribute_onClick(object sender, EventArgs e)
    {
        divSwitch.Visible = true;
        divExpiry.Visible = false;
        divPackage.Visible = false;

        TableCommon.Visible = true;
        TableDetails.Visible = false;
        TableSwitch.Visible = false;
        SwitchTributeOn();
        ResetAll();
        lblErrorSwitchTribute.Text = "";
        
    }

    protected void lnkDegradePackage_onClick(object sender, EventArgs e)
    {
        divSwitch.Visible = false;
        divExpiry.Visible = false;
        divPackage.Visible = true;
        pnlDegradePackage.Visible = false;
        TableCommon.Visible = true;
        TableDetails.Visible = false;
        DegradePackageOn();
        ResetAll();
        
    }

    protected void lbtnShowTrans_onClick(object sender, EventArgs e)
    {
        divExpiry.Visible = false;
        divSwitch.Visible = false;
        divPackage.Visible = false;
        TableCommon.Visible = false;
        TableDetails.Visible = true;
        DisplayTransactionOn();
        ResetAll();
        GetAllTransactions();
    }

    protected void btnGetExpiry_Click(object sender, EventArgs e)
    {
        lblCommonError.Visible = false;
        lblCommonError.Text = "";
        lblErrorUpdateExpiry.Text = "";
        lblErrorSwitchTribute.Text = "";
        lblErrorDegradePackage.Text = "";
        bool validTributeId;
        validTributeId = (int.TryParse(txtTributeId.Text, out _tributeid));
        if (validTributeId && (_tributeid > 0))
        {
            _presenter.GetTributeDetailsOnTributeId();
            IList<UpdateTribute> objListUT = new List<UpdateTribute> { _objUpdateTribute };
            if (_objUpdateTribute != null && _objUpdateTribute.TributeId > 0)
            {
                trGridDetails.Visible = true;
                trExpiryMsg.Visible = true;
                trUpdateMsg.Visible = true;
                gridDetails.DataSource = objListUT;
                gridDetails.DataBind();
                if (_objUpdateTribute.EndDate != null)
                    lblExpiryDateMsg.Text = string.Format("The Tribute has Package : {0} and the Expiry Date is <b>{1}</b> (MM/DD/YYYY)", _objUpdateTribute.PackageName, _objUpdateTribute.EndDate);
                else
                {
                    trUpdateMsg.Visible = false;
                    lblExpiryDateMsg.Text = string.Format("The Tribute has Package : {0} and the Expiry Date is <b>Never</b>", _objUpdateTribute.PackageName);
                }
                if (divExpiry.Visible)
                {
                    pnlUpdateExpiry.Visible = true;
                    pnlDegradePackage.Visible = false;
                }
                #region divSwitch
                else if (divSwitch.Visible)
                {
                    if (_objUpdateTribute.TributeType == 8)
                    {
                        divAlertVideo.InnerHtml = "The selected Tribute is of <br>Video</br> type so it can not be switched to any other type.";
                    }
                    else
                    {
                        divAlertVideo.Visible = false;
                        string[] listTributeTypes = { " -- ", "New Baby", "Birthday", "Graduation", "Wedding", "Anniversary", "Memorial" };
                        TableSwitch.Visible = true;
                        ddTributeTypes.DataSource = listTributeTypes;
                        ddTributeTypes.DataBind();

                        txtboxNewTributeUrl.Text = _objUpdateTribute.TributeUrl;
                        ddTributeTypes.SelectedValue = _objUpdateTribute.TypeDescription;
                        lblDate1.Visible = false;
                        lblDate2.Visible = false;
                        txtDate1.Visible = false;
                        txtDate2.Visible = false;
                        txtDate1.Text = "";
                        txtDate2.Text = "";

                        if (_objUpdateTribute.Date1 != null)
                        {
                            lblDate1.Visible = true;
                            txtDate1.Visible = true;
                            txtDate1.Text = _objUpdateTribute.Date1.ToString();
                        }
                        if (_objUpdateTribute.Date2 != null)
                        {
                            lblDate2.Visible = true;
                            txtDate2.Visible = true;
                            txtDate2.Text = _objUpdateTribute.Date2.ToString();
                        }
                    }
                }
                #endregion
                #region divPackage
                else if (divPackage.Visible)
                {
                    if (_objUpdateTribute.TributeType != 8)
                    {
                        divTribute.Visible = true;
                        divVideoTribute.Visible = false;
                        string[] TributePackages = { " -- ", "Memorial Tribute (Lifetime)", "Tribute Yearly ", "Photo Tribute (LifeTime)", "Photo Tribute Yearly ", "Tribute Free Trial (Announcement)" };
                        lstBoxTrbPackage.DataSource = TributePackages;
                        pnlUpdateExpiry.Visible = false;
                        pnlDegradePackage.Visible = true;
                        TablePackage.Visible = true;
                    }
                    else
                    {
                        divTribute.Visible = false;
                        divVideoTribute.Visible = true;
                        string[] VideoTributepackages = { " -- ", "Memorial Tribute (Lifetime)", "Tribute Yearly ", "Tribute Free Trial (Announcement)" };
                        lstBoxTrbPackage.DataSource = VideoTributepackages;
                        pnlUpdateExpiry.Visible = false;
                        pnlDegradePackage.Visible = true;
                        TablePackage.Visible = true;
                    }
                    lstBoxTrbPackage.DataBind();
                }
                #endregion

            }
            else
            {
                if (divExpiry.Visible)
                {
                    trGridDetails.Visible = false;
                    trExpiryMsg.Visible = false;
                    trUpdateMsg.Visible = false;
                    lblErrorUpdateExpiry.Text = "Tribute Does not exist.";
                }
                else if (divSwitch.Visible)
                {
                    trGridDetails.Visible = false;
                    trExpiryMsg.Visible = false;
                    trUpdateMsg.Visible = false;
                    lblErrorSwitchTribute.Text = "Tribute Does not exist.";
                }
                else if (DivDegradePackage.Visible)
                {
                    pnlDegradePackage.Visible = true;
                    TablePackage.Visible = false;
                    trGridDetails.Visible = false;
                    lblErrorDegradePackage.Text = "Tribute Does not exist.";
                }
            }
        }
        else
        {
            lblCommonError.Visible = true;
            lblCommonError.Text = "Please enter a valid TributeId.";
            txtTributeId.Text = "";
        }
    }

    protected void btnUpdateExpiry_Click(object sender, EventArgs e)
    {
        bool validDate;

        _objAdminTributeUpdate = new AdminTributeUpdate();
        _presenter.GetTributeDetailsOnTributeId();
        DateTime.TryParse(_objUpdateTribute.EndDate.ToString(), out _oldDate);
        _objAdminTributeUpdate.OldValue = _objUpdateTribute.EndDate.ToString();
        validDate = DateTime.TryParse(txtNewExpiry.Text, out _newdate);
        DateTime initialDate;
        DateTime.TryParse(_objUpdateTribute.EndDate.ToString(), out initialDate);
        //PackageTest
        if (validDate)
            validDate = CheckDateForPackage(_oldDate, _newdate, _objUpdateTribute.PackageId);

        if (validDate && !(initialDate.Equals(_newdate)))
        {
            _objUpdateTribute.EndDate = _newdate;
            _presenter.UpDateTributeExpiryDate();

            _objAdminTributeUpdate.TributeId = _tributeid;
            _objAdminTributeUpdate.ChangeType = "Update Expiry";
            _objAdminTributeUpdate.NewValue = _newdate.ToString();
            _presenter.UpdateAdminTributeUpdate();
            trGridDetails.Visible = false;
            trExpiryMsg.Visible = false;
            trUpdateMsg.Visible = false;
        }
        else
        {
            _updateStatus = false;
        }

        SetUpdateResult();
    }

    protected void btnSwitchTribute_Click(object sender, EventArgs e)
    {
        lblErrorSwitchTribute.Text = "";
        bool isValidNewUrl;
        bool isvalidDates = false;
        bool isValidDate1 = false;
        bool isValidDate2 = false;
        DateTime _newDate1 = DateTime.Now;
        DateTime _newDate2 = DateTime.Now;
        string _strNewDate1;
        string _strNewDate2;
        _presenter.GetTributeDetailsOnTributeId();

        if (lblDate2.Visible)
            isValidDate2 = DateTime.TryParse(txtDate2.Text, out _newDate2);

        if (lblDate1.Visible)
            isValidDate1 = DateTime.TryParse(txtDate1.Text, out _newDate1);

        //set date strings.
        if (isValidDate2)
            _strNewDate2 = _newDate2.ToString();
        else
            _strNewDate2 = null;

        if (isValidDate1)
            _strNewDate1 = _newDate1.ToString();
        else
            _strNewDate1 = null;

        //test specific to new baby.
        if (ddTributeTypes.SelectedValue.ToLower().Equals("new baby"))
        {
            if (isValidDate1 && isValidDate2)
            {
                isvalidDates = false;
                lblErrorSwitchTribute.Text = "Please set either Date of Birth or Expected Date and Leave the other date Blank.";
            }
            else
                isvalidDates = true;
        }
        //test specific to memorial.
        else if (ddTributeTypes.SelectedValue.ToLower().Equals("memorial"))
        {
            if ((txtDate1.Text.Length > 0) && (isValidDate1))
            {
                if (isValidDate2)
                    isvalidDates = true;
                else
                {
                    isvalidDates = false;
                    lblErrorSwitchTribute.Text = "Please set a valid Date of Death.";
                }
            }
            else
            {
                if (isValidDate2)
                {
                    isvalidDates = true;
                    txtDate1.Text = "";
                }
                else
                {
                    isvalidDates = false;
                    lblErrorSwitchTribute.Text = "Please set a valid Date of Death.";
                }
            }
        }
            //for rest of types.
        else
        {
            if (isValidDate1 || isValidDate2)
                isvalidDates = true;
            else
            {
                isvalidDates = false;
                lblErrorSwitchTribute.Text = "Please set a valid Date.";
            }
        }

        isValidNewUrl = CheckValidNewUrl(txtboxNewTributeUrl.Text);
        if (txtboxNewTributeUrl.Text.Equals(_objUpdateTribute.TributeUrl))
            isValidNewUrl = true;

        if (ddTributeTypes.SelectedIndex != 0 && isValidNewUrl && isvalidDates)
        {
            bool validTributeType;
            IsVideoTribute = divVideoTribute.Visible;
            _objAdminTributeUpdate = new AdminTributeUpdate();

            _oldTributeType = _objUpdateTribute.TypeDescription;

            _newTributeType = ddTributeTypes.SelectedValue;
            _newTributeTypeId = GetTributeType(ddTributeTypes.SelectedValue);
            //TestNewTributeURL
            Tributes objTribute = new Tributes();
            objTribute.TributeUrl = txtboxNewTributeUrl.Text;
            objTribute.TypeDescription = _newTributeType;
            validTributeType = _presenter.IsNewURLValid(objTribute);

            if (!validTributeType)
            {
                _updateStatus = false;
                //give option to change to a new TributeUrl
            }
            else
            {
                //copy default directory
                //copy Story and images
                //copy video if exists
                //copy thumbnails
                _newTributeUrl = RemoveSpecialCharacters(txtboxNewTributeUrl.Text);
                _dirError = CreateFoldersForNewTributeUrl(_tributeid, _objUpdateTribute.TributeUrl, _newTributeUrl, _objUpdateTribute.TypeDescription, _newTributeType);
                //update entries - tributeURL and type, image url and event image url in DB
                Tributes _newTribute = new Tributes();
                _newTribute.TributeUrl = _newTributeUrl;
                _newTribute.TypeDescription = _newTributeType;
                
                if (isValidDate1)
                    _newTribute.Date1 = _newDate1;
                else
                    _newTribute.Date1 = null;
                
                if (isValidDate2)
                    _newTribute.Date2 = _newDate2;
                else
                    _newTribute.Date2 = null;

                _dbError = _presenter.UpdateNewTributeUrlTributeTypeinAlltables(_objUpdateTribute, _newTribute);
                //create new url to display and visit.

                if (!(_objUpdateTribute.TypeDescription.Equals(ddTributeTypes.SelectedValue)))
                {
                    //save tributetype update transaction.    
                    _objAdminTributeUpdate.TributeId = _tributeid;
                    _objAdminTributeUpdate.ChangeType = "Update TributeType";
                    _objAdminTributeUpdate.OldValue = _objUpdateTribute.TypeDescription;
                    _objAdminTributeUpdate.NewValue = ddTributeTypes.SelectedValue;
                    _presenter.UpdateAdminTributeUpdate();
                }
                if (!(_objUpdateTribute.TributeUrl.Equals(_newTributeUrl)))
                {
                    //save tributeurl transaction
                    _objAdminTributeUpdate.ChangeType = "Update TributeUrl";
                    _objAdminTributeUpdate.OldValue = _objUpdateTribute.TributeUrl;
                    _objAdminTributeUpdate.NewValue = _newTributeUrl;
                    _presenter.UpdateAdminTributeUpdate();
                }
                if (_objUpdateTribute.Date1.ToString() != null || _strNewDate1 != null)
                {
                    //save tribute Date1 transaction
                    _objAdminTributeUpdate.ChangeType = "Update TributeDate1";
                    _objAdminTributeUpdate.OldValue = _objUpdateTribute.Date1.ToString();
                    _objAdminTributeUpdate.NewValue = _strNewDate1;
                    _presenter.UpdateAdminTributeUpdate();
                }

                if (_objUpdateTribute.Date2 != null || _strNewDate2 !=null)
                {
                //save tribute date2 transaction
                _objAdminTributeUpdate.ChangeType = "Update TributeDate2";
                _objAdminTributeUpdate.OldValue = _objUpdateTribute.Date2.ToString();
                _objAdminTributeUpdate.NewValue = _strNewDate2;
                _presenter.UpdateAdminTributeUpdate();
                }



                trGridDetails.Visible = false;
                trExpiryMsg.Visible = false;
                trUpdateMsg.Visible = false;
                TableSwitch.Visible = false;
                _updateStatus = true;
            }
        }
        else
        {
            _updateStatus = false;
        }
        SetUpdateTypeResult();
    }

    private bool CheckValidNewUrl(string newTributeUrl)
    {
        bool isValid = false;
        string extractedUrl = RemoveSpecialCharacters(newTributeUrl);
        if (newTributeUrl.Equals(extractedUrl))
            isValid = true;

        return isValid;
    }

    public static string RemoveSpecialCharacters(string input)
    {
        Regex r = new Regex("(?:[^a-z0-9_-]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        input = r.Replace(input, String.Empty);
        return input.Replace(" ", String.Empty);
    }


    private bool CreateFoldersForNewTributeUrl(int tributeId, string OldTributeURL, string NewtributeURL, string OldTributeType, string NewTributeType)
    {
        bool result = false;
        try
        {
            // For default.aspx
            if (NewTributeType == "New Baby")
                _presenter.CreateDefaultFolder(WebConfig.NewBabyFolderPath, NewtributeURL);
            else if (NewTributeType == "Birthday")
                _presenter.CreateDefaultFolder(WebConfig.BirthdayFolderPath, NewtributeURL);
            else if (NewTributeType == "Graduation")
                _presenter.CreateDefaultFolder(WebConfig.GraduationFolderPath, NewtributeURL);
            else if (NewTributeType == "Wedding")
                _presenter.CreateDefaultFolder(WebConfig.WeddingFolderPath, NewtributeURL);
            else if (NewTributeType == "Anniversary")
                _presenter.CreateDefaultFolder(WebConfig.AnniversaryFolderPath, NewtributeURL);
            else if (NewTributeType == "Memorial")
                _presenter.CreateDefaultFolder(WebConfig.MemorialFolderPath, NewtributeURL);

            //For TributePhotos
            string[] eventPath = CommonUtilities.GetPath();
            string OldDefaultPath = eventPath[0] + "/" + eventPath[1] + "/" + OldTributeURL.Replace(" ", "_") + "_" + OldTributeType.Replace(" ", "_");
            string NewTributeURLFolderPath = eventPath[0] + "/" + eventPath[1] + "/" + NewtributeURL.Replace(" ", "_") + "_" + NewTributeType.Replace(" ", "_");
            // For Tribute Thumbnails

            string OldDefaultPath_thumbs = eventPath[0] + "/" + eventPath[1] + "/thumbnails/" + OldTributeURL.Replace(" ", "_") + "_" + OldTributeType.Replace(" ", "_");
            string NewTributeURLFolderPath_thumbs = eventPath[0] + "/" + eventPath[1] + "/thumbnails/" + NewtributeURL.Replace(" ", "_") + "_" + NewTributeType.Replace(" ", "_");

            try
            {
                this._presenter.CopyOldURlFolderToNewURLFolder(OldDefaultPath, NewTributeURLFolderPath);
                result = true;
            }
            catch (Exception a)
            {
                //throw a;
                result = false;
            }
            try
            {
                this._presenter.CopyOldURlFolderToNewURLFolder(OldDefaultPath_thumbs, NewTributeURLFolderPath_thumbs);
                result = true;
            }
            catch (Exception a)
            {
                //throw a;
                result = false;
            }

            //For TributeVideo
            string[] paths = CommonUtilities.GetVideoTributePath();
            string srcPath = paths[1] + OldTributeURL + "_" + OldTributeType.Replace(" ","_");
            string destPath = paths[1] + NewtributeURL + "_" + NewTributeType.Replace(" ", "_");
            try
            {
                this._presenter.CopyOldURlFolderToNewURLFolder(srcPath, destPath);
            }
            catch (Exception a)
            {
                //throw a;
                result = false;
            }

        }
        catch (Exception ex)
        {
            //throw ex;
            result = false;
        }
        return result;
    }

    private int GetTributeType(string type)
    {
        int TypeId = 0;
        switch(type.ToLower())
        {
            case "new baby":
                TypeId = 2;
                break;
            case "birthday":
                TypeId = 3;
                break;
            case "graduation":
                TypeId = 4;
                break;
            case "wedding":
                TypeId = 5;
                break;
            case "anniversary":
                TypeId = 6;
                break;
            case "memorial":
                TypeId = 7;
                break;
        }
        return TypeId;
    }

    protected void btnUpgaredPackage_Click(object sender, EventArgs e)
    {
        if (lstBoxTrbPackage.SelectedIndex != 0)
        {
            bool validPackage;
            IsVideoTribute = divVideoTribute.Visible;
            _objAdminTributeUpdate = new AdminTributeUpdate();
            _presenter.GetTributeDetailsOnTributeId();
            _oldPackageId = _objUpdateTribute.PackageId;
            _objAdminTributeUpdate.OldValue = _objUpdateTribute.PackageName;

            if (divVideoTribute.Visible)
                _newPackageId = lstBoxTrbPackage.SelectedIndex;
            else
                _newPackageId = lstBoxTrbPackage.SelectedIndex + 3;
            //PackageTest

            validPackage = CheckNewPackage(IsVideoTribute,_oldPackageId, _newPackageId);
            

            if (validPackage && (_oldPackageId != 0))
            {
                _NewPackageUpdateDate = GetUpDatedDate(_oldPackageId, _newPackageId);
                _objUpdateTribute.PackageId = _newPackageId;
                _objUpdateTribute.EndDate = _NewPackageUpdateDate;
                _presenter.UpdateTributePackage();

                _objAdminTributeUpdate.TributeId = _tributeid;
                _objAdminTributeUpdate.ChangeType = "Update Package";
                _objAdminTributeUpdate.NewValue = lstBoxTrbPackage.SelectedValue.ToString();
                _presenter.UpdateAdminTributeUpdate();
                trGridDetails.Visible = false;
                trExpiryMsg.Visible = false;
                trUpdateMsg.Visible = false;
                TablePackage.Visible = false;
            }
            else
            {
                _updateStatus = false;
            }
        }
        else
        {
            _updateStatus = false;
        }
        SetUpdatePackageResult();
    }

    private void GetAllTransactions()
    {
        _presenter.GetAdminTransactions();
        trGridDetails.Visible = true;
        GridViewTransaction.DataSource = _objAdminTributeUpdateList;
        GridViewTransaction.DataBind();

    }

    private DateTime? GetUpDatedDate(int _oldPackageId, int _newPackageId)
    {
        DateTime? NewEnddate = DateTime.Now;
        switch (_oldPackageId)
        {
            case 1:
                if (_newPackageId != 1 && _newPackageId != 4)
                    NewEnddate =RetunEndDate(_newPackageId);
                break;
            case 2:
                if (_newPackageId != 2 && _newPackageId != 5)
                    NewEnddate = RetunEndDate(_newPackageId);
                break;
            case 3:
                if (_newPackageId != 3 && _newPackageId != 8)
                    NewEnddate = RetunEndDate(_newPackageId);
                break;
            case 4:
                if (_newPackageId != 1 && _newPackageId != 4)
                    NewEnddate = RetunEndDate(_newPackageId);
                break;
            case 5:
                if (_newPackageId != 2 && _newPackageId != 5)
                    NewEnddate = RetunEndDate(_newPackageId);
                break;
            case 6:
                if (_newPackageId != 6)
                    NewEnddate = RetunEndDate(_newPackageId);
                break;
            case 7:
                if (_newPackageId != 7)
                    NewEnddate = RetunEndDate(_newPackageId);
                break;
            case 8:
                if (_newPackageId != 8)
                    NewEnddate = RetunEndDate(_newPackageId);
                break;
        }
        return NewEnddate;
    }

    private DateTime? RetunEndDate(int _newPackageId)
    {
        DateTime? _ReturnDate = DateTime.Now;
        DateTime CurrDate = DateTime.Now;
        switch(_newPackageId)
        {
            case 1:
                _ReturnDate = null;
                break;
            case 2:
                _ReturnDate = CurrDate.AddYears(1);
                break;
            case 3:
                _ReturnDate = CurrDate.AddDays(30);
                break;
            case 4:
                _ReturnDate = null;
                break;
            case 5:
                _ReturnDate = CurrDate.AddYears(1);
                break;
            case 6:
                _ReturnDate = null;
                break;
            case 7:
                _ReturnDate = CurrDate.AddYears(1);
                break;
            case 8:
                _ReturnDate = CurrDate.AddDays(30);
                break;
        }
        return _ReturnDate;
    }

    private bool CheckNewPackage(bool IsVideoTribute,int _oldPckgId, int _newPckjId)
    {
        bool IsValidPackage = false;
        if (IsVideoTribute)
        {
            switch (_oldPckgId)
            {
                case 1:
                    if (_newPckjId != 1 && _newPckjId != 4)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 2:
                    if (_newPckjId != 2 && _newPckjId != 5)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 3:
                    if (_newPckjId != 3 && _newPckjId != 8)
                    {
                        IsValidPackage = true;
                    }
                    break;
            }
        }
        else
        {
            switch (_oldPckgId)
            {
                case 1:
                    if (_newPckjId != 1 && _newPckjId != 4)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 2:
                    if (_newPckjId != 2 && _newPckjId != 5)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 3:
                    if (_newPckjId != 3 && _newPckjId != 8)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 4:
                    if (_newPckjId != 1 && _newPckjId != 4)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 5:
                    if (_newPckjId != 2 && _newPckjId != 5)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 6:
                    if (_newPckjId != 6)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 7:
                    if (_newPckjId != 7)
                    {
                        IsValidPackage = true;
                    }
                    break;
                case 8:
                    if (_newPckjId != 8)
                    {
                        IsValidPackage = true;
                    }
                    break;
            }
        }
        return IsValidPackage;
    }

    private void ResetAll()
    {
        trGridDetails.Visible = false;
        txtTributeId.Text = "";
        lblCommonError.Visible = false;
    }

    private bool CheckDateForPackage(DateTime _oldDate, DateTime _newdate, int packageId)
    {
        bool DateCheck = false;
        DateTime currDate = DateTime.Now;
        switch (packageId)
        {
            case 1:
                DateCheck = false;
                break;
            case 2:
                if ((_newdate - currDate).Days < 365)
                    DateCheck = true;
                break;
            case 3:
                if ((_newdate - currDate).Days < 30)
                    DateCheck = true;
                break;
            case 4:
                DateCheck = false;
                break;
            case 5:
                if ((_newdate - currDate).Days < 365)
                    DateCheck = true;
                break;
            case 6:
                DateCheck = false;
                break;
            case 7:
                if ((_newdate - currDate).Days < 365)
                    DateCheck = true;
                break;
            case 8:
                if ((_newdate - currDate).Days < 30)
                    DateCheck = true;
                break;
        }
        return DateCheck;
    }

    protected void ddTributeType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ddTributeTypes.SelectedIndex)
        {//"New Baby","Birthday","Graduation","Wedding","Anniversary","Memorial"
            case 0:
                lblDate1.Visible = false;
                lblDate2.Visible = false;
                txtDate1.Visible = false;
                txtDate2.Visible = false;
                lblDate1.Text = "Date 1 - ";
                lblDate2.Text = "Date 2 - ";
                break;
            case 1:
                lblDate1.Visible = true;
                lblDate2.Visible = true;
                txtDate1.Visible = true;
                txtDate2.Visible = true;
                lblDate1.Text = "Date of Birth - ";
                lblDate2.Text = "Expected Date - ";
                break;
            case 2:
                lblDate1.Visible = true;
                lblDate2.Visible = false;
                txtDate1.Visible = true;
                txtDate2.Visible = false;
                lblDate1.Text = "Date of Birthday - ";
                break;
            case 3:
                lblDate1.Visible = true;
                lblDate2.Visible = false;
                txtDate1.Visible = true;
                txtDate2.Visible = false;
                lblDate1.Text = "Date of Graduation - ";
                break;
            case 4:
                lblDate1.Visible = true;
                lblDate2.Visible = false;
                txtDate1.Visible = true;
                txtDate2.Visible = false;
                lblDate1.Text = "Date of Wedding - ";
                lblDate2.Text = "Date 2 - ";
                break;
            case 5:
                lblDate1.Visible = true;
                lblDate2.Visible = false;
                txtDate1.Visible = true;
                txtDate2.Visible = false;
                lblDate1.Text = "Date of Anniversary - ";
                lblDate2.Text = "Date 2 - ";
                break;
            case 6:
                lblDate1.Visible = true;
                lblDate2.Visible = true;
                txtDate1.Visible = true;
                txtDate2.Visible = true;
                lblDate1.Text = "Date of Birth - ";
                lblDate2.Text = "Date of Death - ";
                break;

        }
    }
    #endregion

    #region Methods
    protected void UpdateExpiryOn()
    {
        DivUpdateExpiry.Attributes.Add("style", "text-decoration:none; color:black;  background-color: #D4D4D4;");
        DivSwitchTribute.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivDegradePackage.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivShowTrans.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        pnlUpdateExpiry.Attributes.Add("style", "display:block;");
        pnlSwitchTribute.Attributes.Add("style", "display:none;");
        pnlDegradePackage.Attributes.Add("style", "display:none;");
        lblErrorUpdateExpiry.Text = string.Empty;
    }
    protected void SwitchTributeOn()
    {
        DivUpdateExpiry.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivSwitchTribute.Attributes.Add("style", "text-decoration:none; color:black; background-color: #D4D4D4;");
        DivDegradePackage.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivShowTrans.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        pnlUpdateExpiry.Attributes.Add("style", "display:none;");
        pnlSwitchTribute.Attributes.Add("style", "display:block;");
        pnlDegradePackage.Attributes.Add("style", "display:none;");
        lblErrorSwitchTribute.Text = string.Empty;
    }
    protected void DegradePackageOn()
    {
        DivUpdateExpiry.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivSwitchTribute.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivDegradePackage.Attributes.Add("style", "text-decoration:none; color:black; background-color: #D4D4D4;");
        DivShowTrans.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        pnlUpdateExpiry.Attributes.Add("style", "display:none;");
        pnlSwitchTribute.Attributes.Add("style", "display:none;");
        pnlDegradePackage.Attributes.Add("style", "display:block;");
        lblErrorDegradePackage.Text = string.Empty;
    }

    private void DisplayTransactionOn()
    {
        DivUpdateExpiry.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivSwitchTribute.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivDegradePackage.Attributes.Add("style", "text-decoration:none; color:white; background-color: #646464;");
        DivShowTrans.Attributes.Add("style", "text-decoration:none; color:black; background-color: #D4D4D4;");
        pnlUpdateExpiry.Attributes.Add("style", "display:none;");
        pnlSwitchTribute.Attributes.Add("style", "display:none;");
        pnlDegradePackage.Attributes.Add("style", "display:none;");
    }


    protected void SetUpdateResult()
    {
        lblErrorUpdateExpiry.Visible = true;
        if (_updateStatus)
            lblErrorUpdateExpiry.Text = "Tribute Updated Successfully.";
        else
            lblErrorUpdateExpiry.Text = "Please enter a valid date.";
    }

    private void SetUpdateTypeResult()
    {
        lblErrorSwitchTribute.Visible = true;
        if (lblErrorSwitchTribute.Text == "")
        {
            if (_updateStatus)
                lblErrorSwitchTribute.Text = "Tribute Type Updated Successfully.";
            else
            {
                lblErrorSwitchTribute.Text = "Please Select a valid Tribute Type or try with a different TributeUrl and Valid Dates in mm/dd/yyyy format.";
                txtboxNewTributeUrl.Visible = true;
            }
        }
    }

    private void SetUpdatePackageResult()
    {
        lblErrorDegradePackage.Visible = true;
        if (_updateStatus)
            lblErrorDegradePackage.Text = "Tribute Package Updated Successfully.";
        else
            lblErrorDegradePackage.Text = "Please Select a valid Package.";
    }

    #endregion

    #region properties

    [CreateNew]
    public UpdateTributePresenter Presenter
    {
        set
        {
            this._presenter = value;
            this._presenter.View = this;
        }
    }
    

    #region IUpdateTribute Members

    int IUpdateTribute.TributeId
    {
        get 
        {
            int.TryParse(txtTributeId.Text, out _tributeid);
            return _tributeid; 
        }
        set { _tributeid = value; }
    }

    public string ExpiryDate
    {
        get { return objNewExpiry; }
        set { objNewExpiry = value; }
    }

    UpdateTribute IUpdateTribute.objUpdateTribute
    {
        get { return _objUpdateTribute; }
        set { _objUpdateTribute = value; }
    }

    string IUpdateTribute.ExpiryDate
    {
        get { return _expiryDate; }
        set { _expiryDate = value; }
    }

    string IUpdateTribute.ChangeType
    {
        get { return _changeType; }
        set { _changeType = value; }
    }
    
    DateTime IUpdateTribute.ModifiedDate
    {
        get { return _modifiedDate; }
        set { _modifiedDate = value; }
    }

    string IUpdateTribute.OldValue
    {
        get { return _oldValue; }
        set { _oldValue = value; }
    }

    string IUpdateTribute.NewValue
    {
        get { return _newValue; }
        set { _newValue = value; }
    }

    bool IUpdateTribute.UpdateStatus
    {
        get { return _updateStatus; }
        set
        {
            _updateStatus = value;
        }
    }

    public string PackageName
    {
        get { return _PackageName; }
        set { _PackageName = value; }
    }


    AdminTributeUpdate IUpdateTribute.objAdminTributeUpdate
    {
        get { return _objAdminTributeUpdate; }
        set { _objAdminTributeUpdate = value; }
    }

    IList<AdminTributeUpdate> IUpdateTribute.objAdminTributeUpdateList
    {
        get { return _objAdminTributeUpdateList; }
        set { _objAdminTributeUpdateList = value; }
    }
    #endregion
    #endregion

    
}
