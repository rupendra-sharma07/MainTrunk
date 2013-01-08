///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.UserBusiness.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the business user
///Audit Trail     : Date of Modification  Modified By         Description

using System; 
 namespace TributesPortal.BusinessEntities
{
     [Serializable]
	 public class UserBusiness
	{
	/// <summary>
	/// Default Contructor
	/// <summary>
    /// 

         public enum UserRegistrationEnum
         {
             UserId,
             Website,
             CompanyName,
             BusinessType,
             BusinessAddress,
             WelcomeMessage,
             CompanyLogo,
             UserName,
             Phone,
             HeaderBGColor,
             ZipCode,
             IsActive,
             // LHK-26Oct-CheckBox
             IsAddressOn,
             IsWebAddressOn,
             IsObUrlLinkOn,
             IsPhoneNoOn,
             DisplayCustomHeader,
             HeaderLogo,
             ObituaryLinkPage,
             Attribute1,
             Attribute2,
             ApplicationType
         //Till Here
             
         }
	public UserBusiness()
	{}


	private int _Id;
	public int Id
	{ 
		get { return _Id; }
		set { _Id = value; }
	}


	private int _UserId;
	public int UserId
	{ 
		get { return _UserId; }
		set { _UserId = value; }
	}

    private string _UserName;
    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }

    private string _CompanyLogo;
    public string CompanyLogo
    {
        get { return _CompanyLogo; }
        set { _CompanyLogo = value; }
    }

	private string _Website;
	public string Website
	{ 
		get { return _Website; }
		set { _Website = value; }
	}


	private string _CompanyName;
	public string CompanyName
	{ 
		get { return _CompanyName; }
		set { _CompanyName = value; }
	}

    private string _WelcomeMessage;
    public string WelcomeMessage
    {
        get { return _WelcomeMessage; }
        set { _WelcomeMessage = value; }
    }

	private int _BusinessType;
	public int BusinessType
	{ 
		get { return _BusinessType; }
		set { _BusinessType = value; }
	}


	private string _BusinessAddress;
	public string BusinessAddress
	{ 
		get { return _BusinessAddress; }
		set { _BusinessAddress = value; }
	}

     private string _Phone;
     public string Phone
     {
         get { return _Phone; }
         set { _Phone = value; }
     }

     private string _HeaderBGColor;
     public string HeaderBGColor
     {
         get {return _HeaderBGColor; }
         set { _HeaderBGColor = value; }
     }

	private string _ZipCode;
	public string ZipCode
	{ 
		get { return _ZipCode; }
		set { _ZipCode = value; }
	}

    private string _City;
    public string City
    {
        get { return _City; }
        set { _City = value; }
    }

    private string _State;
    public string State
    {
        get { return _State; }
        set { _State = value; }
    }

    private string _Country;
    public string Country
    {
        get { return _Country; }
        set { _Country = value; }
    }

    private string _Email;
    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }

	private bool _IsActive;
	public bool IsActive
	{ 
		get { return _IsActive; }
		set { _IsActive = value; }
	}
         //LHK
    private bool _IsAddressOn;
    public bool IsAddressOn
    {
        get { return _IsAddressOn; }
        set { _IsAddressOn = value; }
    }

    private bool _IsWebAddressOn;
    public bool IsWebAddressOn
    {
        get { return _IsWebAddressOn; }
        set { _IsWebAddressOn = value; }
    }

    private bool _IsObUrlLinkOn;
    public bool IsObUrlLinkOn
    {
        get { return _IsObUrlLinkOn; }
        set { _IsObUrlLinkOn = value; }
    }

    private bool _IsPhoneNoOn;
    public bool IsPhoneNoOn
    {
        get { return _IsPhoneNoOn; }
        set { _IsPhoneNoOn = value; }
    }
    private bool _DisplayCustomHeader;
    public bool DisplayCustomHeader
    {
        get { return _DisplayCustomHeader; }
        set { _DisplayCustomHeader = value; }
    }
    private string _HeaderLogo;
    public string HeaderLogo
    {
        get { return _HeaderLogo; }
        set { _HeaderLogo = value; }
    }

    // Added property to get and set the information of user type on businessuser page
    private int _UserType;
    public int UserType
    {
        get { return _UserType; }
        set { _UserType = value; }
    }


    // Added property to get and set the ObituaryLinkPage of user type on businessuser page
    private string _ObituaryLinkPage;
    public string ObituaryLinkPage
    {
        get { return _ObituaryLinkPage; }
        set { _ObituaryLinkPage = value; }
    }
    // Added property to get state and city of businessuser
    private string _Attribute1;
    public string Attribute1
    {
        get { return _Attribute1; }
        set { _Attribute1 = value; }
    }

    private string _Attribute2;
    public string Attribute2
    {
        get { return _Attribute2; }
        set { _Attribute2 = value; }
    }

    private string _ApplicationType;
    public string ApplicationType
    {
        get { return _ApplicationType; }
        set { _ApplicationType = value; }
    }


         public UserBusiness
                (
                string Website,
                string CompanyName,
                int BusinessType,
                string BusinessAddress,
                string Phone,
                string HeaderBGColor,
                string ZipCode,
                 bool IsAddressOn,
                 bool IsWebAddressOn,
                 bool IsObUrlLinkOn,
                 bool IsPhoneNoOn,
             bool DisplayCustomHeader,
             string HeaderLogo,
             string ObituaryLinkPage
                )
         {   
             this._Website = Website;
             this._CompanyName = CompanyName;
             this._BusinessType = BusinessType;
             this._BusinessAddress = BusinessAddress;
             this._Phone = Phone;
             this._HeaderBGColor = HeaderBGColor;
             this._ZipCode = ZipCode;
             this._IsAddressOn = IsAddressOn;
             this._IsWebAddressOn = IsWebAddressOn;
             this._IsObUrlLinkOn = IsObUrlLinkOn;
             this._IsPhoneNoOn = IsPhoneNoOn;
             this._DisplayCustomHeader = DisplayCustomHeader;
             this._HeaderLogo = HeaderLogo;
             this._ObituaryLinkPage = ObituaryLinkPage;
         }

	/// <summary>
	/// User defined Contructor
	/// <summary>
         public UserBusiness(int Id,
             int UserId,
             string Website,
             string CompanyName,
             int BusinessType,
             string BusinessAddress,
             string Phone,
             string HeaderBGColor,
             string ZipCode,
             bool IsActive,
             bool IsAddressOn,
             bool IsWebAddressOn,
             bool IsObUrlLinkOn,
             bool IsPhoneNoOn,
             bool DisplayCustomHeader,
             string HeaderLogo )
         {
             this._Id = Id;
             this._UserId = UserId;
             this._Website = Website;
             this._CompanyName = CompanyName;
             this._BusinessType = BusinessType;
             this._BusinessAddress = BusinessAddress;
             this._Phone = Phone;
             this._HeaderBGColor = HeaderBGColor;
             this._ZipCode = ZipCode;
             this._IsActive = IsActive;
             this._IsAddressOn = IsAddressOn;
             this._IsWebAddressOn = IsWebAddressOn;
             this._IsObUrlLinkOn = IsObUrlLinkOn;
             this._IsPhoneNoOn = IsPhoneNoOn;
             this._DisplayCustomHeader = DisplayCustomHeader;
             this._HeaderLogo = HeaderLogo;             
         }

         /// <summary>
         /// User defined Contructor
         /// <summary>
         public UserBusiness(int Id,
             int UserId,
             string Website,
             string CompanyName,
             int BusinessType,
             string BusinessAddress,
             string Phone,
             string HeaderBGColor,
             string ZipCode,
             bool IsActive,
             bool IsAddressOn,
             bool IsWebAddressOn,
             bool IsObUrlLinkOn,
             bool IsPhoneNoOn,
             bool DisplayCustomHeader,
             string HeaderLogo,
             string ObituaryLinkPage)
         {
             this._Id = Id;
             this._UserId = UserId;
             this._Website = Website;
             this._CompanyName = CompanyName;
             this._BusinessType = BusinessType;
             this._BusinessAddress = BusinessAddress;
             this._Phone = Phone;
             this._HeaderBGColor = HeaderBGColor;
             this._ZipCode = ZipCode;
             this._IsActive = IsActive;
             this._IsAddressOn = IsAddressOn;
             this._IsWebAddressOn = IsWebAddressOn;
             this._IsObUrlLinkOn = IsObUrlLinkOn;
             this._IsPhoneNoOn = IsPhoneNoOn;
             this._DisplayCustomHeader = DisplayCustomHeader;
             this._HeaderLogo = HeaderLogo;
             this._ObituaryLinkPage = ObituaryLinkPage;
         }
         /// <summary>
         /// User defined Contructor
         /// <summary>
         public UserBusiness(int Id,
             int UserId,
             string Website,
             string CompanyName,
             int BusinessType,
             string BusinessAddress,
             string Phone,
             string HeaderBGColor,
             string ZipCode,
             bool IsActive,
             bool IsAddressOn,
             bool IsWebAddressOn,
             bool IsObUrlLinkOn,
             bool IsPhoneNoOn,
             bool DisplayCustomHeader,
             string HeaderLogo,
             string ObituaryLinkPage,
             string Attribute1,
             string Attribute2)
         {
             this._Id = Id;
             this._UserId = UserId;
             this._Website = Website;
             this._CompanyName = CompanyName;
             this._BusinessType = BusinessType;
             this._BusinessAddress = BusinessAddress;
             this._Phone = Phone;
             this._HeaderBGColor = HeaderBGColor;
             this._ZipCode = ZipCode;
             this._IsActive = IsActive;
             this._IsAddressOn = IsAddressOn;
             this._IsWebAddressOn = IsWebAddressOn;
             this._IsObUrlLinkOn = IsObUrlLinkOn;
             this._IsPhoneNoOn = IsPhoneNoOn;
             this._DisplayCustomHeader = DisplayCustomHeader;
             this._HeaderLogo = HeaderLogo;
             this._ObituaryLinkPage = ObituaryLinkPage;
             this._Attribute1 = Attribute1;
             this._Attribute2 = Attribute2;
         }
         public UserBusiness(int Id,
             int UserId,
             string Website,
             string CompanyName,
             int BusinessType,
             string BusinessAddress,
             string Phone,
             string HeaderBGColor,
             string ZipCode,
             bool IsActive,
             bool IsAddressOn,
             bool IsWebAddressOn,
             bool IsObUrlLinkOn,
             bool IsPhoneNoOn,
             bool DisplayCustomHeader,
             string HeaderLogo,
             string ObituaryLinkPage,
             string Attribute1,
             string Attribute2,
             string ApplicationType)
         {
             this._Id = Id;
             this._UserId = UserId;
             this._Website = Website;
             this._CompanyName = CompanyName;
             this._BusinessType = BusinessType;
             this._BusinessAddress = BusinessAddress;
             this._Phone = Phone;
             this._HeaderBGColor = HeaderBGColor;
             this._ZipCode = ZipCode;
             this._IsActive = IsActive;
             this._IsAddressOn = IsAddressOn;
             this._IsWebAddressOn = IsWebAddressOn;
             this._IsObUrlLinkOn = IsObUrlLinkOn;
             this._IsPhoneNoOn = IsPhoneNoOn;
             this._DisplayCustomHeader = DisplayCustomHeader;
             this._HeaderLogo = HeaderLogo;
             this._ObituaryLinkPage = ObituaryLinkPage;
             this._Attribute1 = Attribute1;
             this._Attribute2 = Attribute2;
             this._ApplicationType = ApplicationType;
         }
	}
}
