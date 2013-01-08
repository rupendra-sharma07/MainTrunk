/**

 * @author Sumon sumon@improsys.com, Mamun mamun@improsys.com
 * @history
 *          created  : Mamun : Date : 08-03-2008
 * @version 1.0
 *
 * Copyright Improsys.
 *
 * All rights reserved.
 *
 * This software is the confidential and proprietary information
 * of Improsys. ("Confidential Information").
 * You shall not disclose such Confidential Information and shall use
 * it only in accordance with the terms of the license agreement
 * you entered into with Improsys.
 */
using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util; 

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for PlaxoImporter.
	/// </summary>
	public class PlaxoImporter : IDisposable
	{
		private bool isLoggedIn = false;
		private String userId,password;
		private String htmlResponse = "";
		private String postString = "";
		private HttpUtils httpUtils = null;
		private String reffer = "";
		private String addressBookURL = "";
		private String strFrm ="";
		private string strHidden ="";
		private ArrayList names=null;
		private ArrayList emails=null;

		public PlaxoImporter()
		{
			httpUtils = new HttpUtils();
		}
		public PlaxoImporter(String userId,String password)
		{
			httpUtils = new HttpUtils();
			this.userId = userId;
			this.password = password;
		}

		public String UserId
		{
			get
			{
				return userId;
			}
			set
			{
				Logout(true);
				userId = value;
			}
		}

		public String Password
		{
			get
			{
				return password;
			}
			set
			{
				Logout(true);
				password = value;
			}
		}

		public bool IsLoggedIn
		{
			get
			{
				return isLoggedIn;
			}
		}

		public String[] Names
		{
			get
			{
				return (String[])(names.ToArray(typeof(String)));
			}
		}

		public String[] Emails
		{
			get
			{
				return (String[])(emails.ToArray(typeof(String)));
			}
		}

		private void Logout(bool keepUserInfo)
		{
			isLoggedIn = false;
			if(!keepUserInfo)
			{
				userId = "";
				password = "";
			}
			names = null;
			emails = null;
		}

		public void Logout()
		{
			Logout(false);
		}

		public bool DoLogin(String userId,String password)
		{
			this.UserId = userId;
			this.Password = password; 
			return DoLogin();
		}

		public bool DoLogin()
		{
			Logout(true);
			String homePage = "https://www.plaxo.com/signin?ntmp=1";
			String loginUrl = "";
			String redirect = "";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(homePage,false);
				this.strFrm = htmlResponse.Substring(htmlResponse.IndexOf("<form"));
				this.strFrm = this.strFrm.Substring(0,this.strFrm.IndexOf("</form>"));
				loginUrl = strFrm.Substring(strFrm.IndexOf("action=\"")+"action=\"".Length);
				loginUrl = loginUrl.Substring(0,loginUrl.IndexOf("\""));
				reffer = loginUrl;
				this.strHidden = StringUtils.HiddenFields(this.strFrm);
				postString = this.strHidden+"&signin.email="+HttpUtility.UrlEncode(this.userId)+"&signin.password="+HttpUtility.UrlEncode(this.password);
				htmlResponse = httpUtils.GetHttpResponse(loginUrl,false,postString,reffer);
				if(htmlResponse.IndexOf("<div id=\"error_text\">")==-1)
				{
					isLoggedIn = true;
					redirect = htmlResponse.Substring(htmlResponse.IndexOf("window.location.replace('")+"window.location.replace('".Length);
					redirect = redirect.Substring(0,redirect.IndexOf("')"));
					htmlResponse = httpUtils.GetHttpResponse(redirect,false);
				}
				else
				{
					isLoggedIn = false;
				}
			}
			catch(Exception e)
			{
				isLoggedIn = false;
				Logout();
			}
			return isLoggedIn;
		}

		public bool ImportContacts()
		{
			names = new ArrayList();
			emails = new ArrayList();
			String tmpName = "";
			String tmpEmail = "";
			int count=0,counter;
			addresses b=new addresses();
			addressBookURL = "https://www.plaxo.com/export";
			String exportUrl = "https://www.plaxo.com/export/plaxo_ab_yahoo.csv";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(addressBookURL,false);
				this.strFrm = htmlResponse.Substring(htmlResponse.IndexOf("<form"));
				this.strFrm = this.strFrm.Substring(0,this.strFrm.IndexOf("</form>"));
				this.strHidden = StringUtils.HiddenFields(this.strFrm);
				postString = strHidden+"&paths.0.checked=on&type=Y&x=36&y=19";
				reffer = addressBookURL;
				htmlResponse = httpUtils.GetHttpResponse(exportUrl,false,postString,reffer);
				System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlResponse));
				System.IO.StreamReader str = new System.IO.StreamReader(strm);
				CSVReader csv = new CSVReader(str); 
				string[] fields;
				string[] temp_line;
				while ((fields = csv.GetCSVLine()) != null) 
				{
					if(fields.Length < 5)
						continue;
					temp_line=new string[1024];
					
					if(count!=0)
					{
						counter=0;
						foreach (string field in fields) 
						{   
							temp_line[counter]=field;
							
							counter++;

						}
						
						tmpName=(temp_line[0]=="") ? temp_line[1]:temp_line[0]; 
						tmpName=(temp_line[0]=="" && temp_line[1]=="") ? temp_line[2]:((temp_line[2]!="" && temp_line[0]!="") ? temp_line[0]+" "+temp_line[1]+" "+temp_line[2]:tmpName) ;
					
						tmpEmail = (temp_line[3]=="") ? temp_line[4]:temp_line[3];
						if(StringUtils.IsValidEmail(tmpEmail) && tmpName=="")
						{
							tmpName=tmpEmail.Substring(0,tmpEmail.IndexOf("@"));
						}
					
					
						if(StringUtils.IsValidEmail(tmpEmail))
						{
							names.Add(tmpName);
							emails.Add(tmpEmail);
						}
					}
					count++;
				}
				if(names.Count>0) 
				{
					return true;
				}
				else
				{
					return false;
				}
				
			}
			catch(Exception e)
			{
				Dispose();
				return false;
			}
			
		}
		public void Dispose() 
		{
			Logout();
			GC.SuppressFinalize(this);
		}
		
	}
}
