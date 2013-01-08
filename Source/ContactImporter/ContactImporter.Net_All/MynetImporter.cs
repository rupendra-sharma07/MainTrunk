/**
 * @author Sumon sumon@improsys.com, Mamun mamun@improsys.com
 * @history
 *          created  : Mamun : Date : 11-10-2007
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
using System.Net;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util;

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for MynetImporter.
	/// </summary>
	public class MynetImporter
	{
		private bool isLoggedIn = false;
		private String userId,password;
		private String htmlResponse = "";
		private String postString="";
		private String reffer = "";
		private String addressBookURL = "";
		private HttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();

		public MynetImporter()
		{
			httpUtils = new HttpUtils();
		}
		public MynetImporter(String user,String pass)
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
			String homePage = "http://uyeler.mynet.com/login/?loginRequestingURL=lmail&formname=eposta";
			string strHidden ="";
			String loginUrl = "";
			String frm = "";
			String redirectUrl = "";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(homePage,false);
				frm = htmlResponse.Substring(htmlResponse.IndexOf("<form method=post action=\"")+"<form method=post action=\"".Length);
				loginUrl = frm.Substring(0,frm.IndexOf("\""));
				frm = frm.Substring(0,frm.IndexOf("</form>"));
				strHidden = StringUtils.HiddenFields(frm);
				//strHidden = strHidden.Replace("&rememberstate=","&rememberstate=0");
				postString = strHidden+"&username="+this.UserId+"&password="+HttpUtility.UrlEncode(this.Password);
				htmlResponse = httpUtils.GetHttpResponse(loginUrl,false,postString,homePage,"",false);
				if(htmlResponse.IndexOf("url=")!=-1)
				{
					redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("url=")+"url=".Length);
					redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\">"));
					htmlResponse = httpUtils.GetHttpResponse(redirectUrl,false);
				
					if(htmlResponse.IndexOf("<input type=password name=password")!=-1)
					{
						isLoggedIn = false;
					}
					else
					{
						if(htmlResponse.IndexOf("<div class=\"links-4off\"")!=-1)
						{
							this.addressBookURL = htmlResponse.Substring(htmlResponse.IndexOf("<div class=\"links-4off\"")+"<div class=\"links-4off\"".Length);
							this.addressBookURL = this.addressBookURL.Substring(0,this.addressBookURL.IndexOf("<img src=\""));
							if(this.addressBookURL.IndexOf("<a href=\"")!=-1)
							{
								this.addressBookURL = this.addressBookURL.Substring(this.addressBookURL.IndexOf("<a href=\"")+"<a href=\"".Length);
								this.addressBookURL = this.addressBookURL.Substring(0,this.addressBookURL.IndexOf("\""));
								isLoggedIn = true;
							}
							else
							{
								isLoggedIn = false;
							}
						}
						else
						{
							isLoggedIn = false;
						}
					}
				}
				else
				{
					isLoggedIn = false;
				}
			}
			catch(Exception e)
			{
				isLoggedIn = false;
			}
			return isLoggedIn;
		}
		public bool ImportContacts()
		{
			this.names = new ArrayList();
			this.emails = new ArrayList();
			String exportUrl = "http://adresdefteri.mynet.com/Exim/EximPage.aspx";
			String export = "http://adresdefteri.mynet.com/Exim/ExportFileDownload.aspx";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(this.addressBookURL,false);
				htmlResponse = httpUtils.GetHttpResponse(exportUrl,false);
				postString = "format=microsoft_csv";
				htmlResponse = httpUtils.GetHttpResponse(export,false,postString,exportUrl);
				System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlResponse));
				System.IO.StreamReader str = new System.IO.StreamReader(strm);
				int count=0,counter;
				addresses b=new addresses();
				String temp_name="",temp_email="";
				CSVReader csv = new CSVReader(str); 
				string[] fields;
				string[] temp_line;
				while ((fields = csv.GetCSVLine()) != null) 
				{
					temp_line=new string[100];
					if(count!=0)
					{
						counter=0;
						foreach (string field in fields) 
						{   
							temp_line[counter]=field;
							counter++;
						}
						temp_name=(temp_line[0]+" "+temp_line[1]+" "+temp_line[10]); 
						
						temp_email=(temp_line[9]!="") ? temp_line[9]:(((temp_line[12]!="")? temp_line[12]:((temp_line[13]!="") ? temp_line[13]:"")));
						if(temp_email!=null && temp_name==null)
						{
							temp_name=temp_email.Substring(0,temp_email.IndexOf("@"));
						}
						if(temp_email!=null && temp_name!=null)
						{
							names.Add(temp_name);
							emails.Add(temp_email);
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
			catch(WebException ex)
			{
				Logout();
				return false;
			}
			
		}
	}
}
