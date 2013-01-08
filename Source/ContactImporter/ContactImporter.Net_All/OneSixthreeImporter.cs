/**
* @author Sumon sumon@improsys.com, Mamun mamun@improsys.com
* @history
*          created  : Mamun : Date : 13-05-2008
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
	/// Summary description for OneSixthreeImporter.
	/// </summary>
	public class OneSixthreeImporter
	{
		private bool isLoggedIn = false;
		private String userId,password;
		private String addressBookURL = "";
		private String htmlResponse = "";
		private String postString="";
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();
		private HttpUtils httpUtils = null;
		private String sid = "";
		private String domain = "";

		public OneSixthreeImporter()
		{
			httpUtils = new HttpUtils();
		}
		public OneSixthreeImporter(String user,String pass)
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
			String startUrl = "http://reg.163.com/in.jsp?url=http://fm163.163.com/coremail/fcg/ntesdoor2?verifycookie%3D1%26language%3D-1%26style%3D34";
			String loginUrl = "http://reg.163.com/CheckUser.jsp";
			String url = "";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				String pass = this.password;
				postString = "url="+HttpUtility.UrlEncode("http://fm163.163.com/coremail/fcg/ntesdoor2?verifycookie=1&language=-1&style=34")+"&username="+this.userId+"&password="+HttpUtility.UrlEncode(pass)+"&submit=%E7%99%BB%E3%80%80%E5%BD%95";
				htmlResponse = httpUtils.GetHttpResponse(loginUrl,true,postString,startUrl,"",true);
				if(htmlResponse.IndexOf("The URL has moved")==-1)
				{
					isLoggedIn = false;
				}
				else
				{
					if(htmlResponse.IndexOf("<a href=")>-1)
					{
						url = htmlResponse.Substring(htmlResponse.IndexOf("<a href=\"")+"<a href=\"".Length);
						url = url.Substring(0,url.IndexOf("\">"));
						htmlResponse = httpUtils.GetHttpResponse(url,false,"","","",true);
						int a = htmlResponse.IndexOf("<title>302 Found</title>");
						if(a > -1)
						{
							url = htmlResponse.Substring(htmlResponse.IndexOf("<a href=\"")+"<a href=\"".Length);
							url = url.Substring(0,url.IndexOf("\">"));
							url = url.Replace("&amp;","&");
							this.domain = url.Substring(url.IndexOf("http://")+"http://".Length);
							this.domain = this.domain.Substring(0,this.domain.IndexOf("/"));
							this.sid = url.Substring(url.IndexOf("&sid=")+ "&sid=".Length);
							//this.sid = this.sid.Substring(0,this.sid.IndexOf(""));
							htmlResponse = httpUtils.GetHttpResponse(url,false,"","");
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
			catch(Exception e)
			{
				Logout();
				isLoggedIn = false;
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
			String frm = "";
			String strHidden = "";
			Random r = new Random();
			String number = r.Next(1,9999).ToString("0000");
			addressBookURL ="http://"+this.domain+"/coremail/fcg/ldvcapp?funcid=xportadd&sid="+this.sid+"&"+number;
			String exportUrl = "http://"+this.domain+"/coremail/fcg/ldvcapp";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(addressBookURL,false,"",addressBookURL);
				if(htmlResponse.IndexOf("<form name=\"outport\"")> -1)
				{
					frm = htmlResponse.Substring(htmlResponse.IndexOf("<form name=\"outport\""));
					frm = frm.Substring(0,frm.IndexOf("</form"));
					strHidden = StringUtils.HiddenFields(frm);
					postString = strHidden+"&outformat=8&outport.x=%BF%AA%CA%BC%B5%BC%B3%F6";
					htmlResponse = httpUtils.GetHttpResponse(exportUrl,false,postString,addressBookURL);
					System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlResponse));
					System.IO.StreamReader str = new System.IO.StreamReader(strm);
					CSVReader csv = new CSVReader(str); 
					string[] fields;
					string[] temp_line;
					while ((fields = csv.GetCSVLine()) != null) 
					{
						//					if(fields.Length < 5)
						//						continue;
						temp_line=new string[1024];
					
						if(count!=0)
						{
							counter=0;
							foreach (string field in fields) 
							{   
								temp_line[counter]=field;
								counter++;
							}
							tmpName=temp_line[4]; 
							tmpEmail = temp_line[3];
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
				}
				else
				{
					
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
