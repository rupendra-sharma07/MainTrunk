///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.TributeAdministrator.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the object used to handle the Details 
///                     of the tribute administrator 
///Audit Trail     : Date of Modification  Modified By         Description

using System; 
 namespace TributesPortal.BusinessEntities
{
     [Serializable]
	 public class TributeAdministrator
	{
	/// <summary>
	/// Default Contructor
	/// <summary>
	public TributeAdministrator()
	{}


	private int _TributeAdminId;
	public int TributeAdminId
	{ 
		get { return _TributeAdminId; }
		set { _TributeAdminId = value; }
	}


	private int _UserTributeId;
	public int UserTributeId
	{ 
		get { return _UserTributeId; }
		set { _UserTributeId = value; }
	}


	private int _UserId;
	public int UserId
	{ 
		get { return _UserId; }
		set { _UserId = value; }
	}


	private string _Email;
	public string Email
	{ 
		get { return _Email; }
		set { _Email = value; }
	}


	private bool _IsOwner;
	public bool IsOwner
	{ 
		get { return _IsOwner; }
		set { _IsOwner = value; }
	}


	private bool _IsActive;
	public bool IsActive
	{ 
		get { return _IsActive; }
		set { _IsActive = value; }
	}


	private bool _IsDeleted;
	public bool IsDeleted
	{ 
		get { return _IsDeleted; }
		set { _IsDeleted = value; }
	}

	/// <summary>
	/// User defined Contructor
	/// <summary>
	public TributeAdministrator(int TributeAdminId, 
		int UserTributeId, 
		int UserId, 
		string Email, 
		bool IsOwner, 
		bool IsActive, 
		bool IsDeleted)
	{
		this._TributeAdminId = TributeAdminId;
		this._UserTributeId = UserTributeId;
		this._UserId = UserId;
		this._Email = Email;
		this._IsOwner = IsOwner;
		this._IsActive = IsActive;
		this._IsDeleted = IsDeleted;
	}

         public TributeAdministrator(string Email)
         {
             this._Email = Email;

         }

	}
}
