///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.Package.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle 
///                     the Details about tribute Packages
///Audit Trail     : Date of Modification  Modified By         Description

using System; 
 namespace TributesPortal.BusinessEntities
{
     [Serializable]
	 public class Package
	{
	/// <summary>
	/// Default Contructor
	/// <summary>
	public Package()
	{}


	public int PackageID
	{ 
		get { return _PackageID; }
		set { _PackageID = value; }
	}
	private int _PackageID;


	public string PackageName
	{ 
		get { return _PackageName; }
		set { _PackageName = value; }
	}
	private string _PackageName;


	public int Price
	{ 
		get { return _Price; }
		set { _Price = value; }
	}
	private int _Price;


	public bool IsAllFeaturesProveided
	{ 
		get { return _IsAllFeaturesProveided; }
		set { _IsAllFeaturesProveided = value; }
	}
	private bool _IsAllFeaturesProveided;


	public bool IsAdvertisingFree
	{ 
		get { return _IsAdvertisingFree; }
		set { _IsAdvertisingFree = value; }
	}
	private bool _IsAdvertisingFree;


	public bool IsRenewalRqd
	{ 
		get { return _IsRenewalRqd; }
		set { _IsRenewalRqd = value; }
	}
	private bool _IsRenewalRqd;


	public int CreatedBy
	{ 
		get { return _CreatedBy; }
		set { _CreatedBy = value; }
	}
	private int _CreatedBy;


	public System.DateTime CreatedDate
	{ 
		get { return _CreatedDate; }
		set { _CreatedDate = value; }
	}
	private System.DateTime _CreatedDate;


	public int ModifiedBy
	{ 
		get { return _ModifiedBy; }
		set { _ModifiedBy = value; }
	}
	private int _ModifiedBy;


	public System.DateTime ModifiedDate
	{ 
		get { return _ModifiedDate; }
		set { _ModifiedDate = value; }
	}
	private System.DateTime _ModifiedDate;


	public bool IsActive
	{ 
		get { return _IsActive; }
		set { _IsActive = value; }
	}
	private bool _IsActive;


	public bool IsDeleted
	{ 
		get { return _IsDeleted; }
		set { _IsDeleted = value; }
	}
	private bool _IsDeleted;

	/// <summary>
	/// User defined Contructor
	/// <summary>
	public Package(int PackageID, 
		string PackageName, 
		int Price, 
		bool IsAllFeaturesProveided, 
		bool IsAdvertisingFree, 
		bool IsRenewalRqd, 
		int CreatedBy, 
		System.DateTime CreatedDate, 
		int ModifiedBy, 
		System.DateTime ModifiedDate, 
		bool IsActive, 
		bool IsDeleted)
	{
		this._PackageID = PackageID;
		this._PackageName = PackageName;
		this._Price = Price;
		this._IsAllFeaturesProveided = IsAllFeaturesProveided;
		this._IsAdvertisingFree = IsAdvertisingFree;
		this._IsRenewalRqd = IsRenewalRqd;
		this._CreatedBy = CreatedBy;
		this._CreatedDate = CreatedDate;
		this._ModifiedBy = ModifiedBy;
		this._ModifiedDate = ModifiedDate;
		this._IsActive = IsActive;
		this._IsDeleted = IsDeleted;
	}

	}
}
