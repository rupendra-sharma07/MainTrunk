///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributesPortal.BusinessEntities.CustomerCountry.cs
///Author          : 
///Creation Date   : 
///Description     : This file defines the attributes of the Customer Country object
///Audit Trail     : Date of Modification  Modified By         Description

using System; 
 namespace TributesPortal.BusinessEntities
{
     [Serializable]
	 public class CustomerCountry
	{
	/// <summary>
	/// Default Contructor
	/// <summary>
	public CustomerCountry()
	{}


	public int CountryID
	{ 
		get { return _CountryID; }
		set { _CountryID = value; }
	}
	private int _CountryID;


	public string CountryName
	{ 
		get { return _CountryName; }
		set { _CountryName = value; }
	}
	private string _CountryName;

	/// <summary>
	/// User defined Contructor
	/// <summary>
	public CustomerCountry(int CountryID, 
		string CountryName)
	{
		this._CountryID = CountryID;
		this._CountryName = CountryName;
	}

	}
}
