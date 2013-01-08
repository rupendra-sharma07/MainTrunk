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
	/// Summary description for MaildotruImporter.
	/// </summary>
	public class MailruImporter
	{
		private bool isLoggedIn = false;
		private String userId,password;
		private String addressBookURL = "";
		private String htmlResponse = "";
		private String postString="";
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();
		private HttpUtils httpUtils = null;

		public MailruImporter()
		{
			httpUtils = new HttpUtils();
		}

		public MailruImporter(String user,String pass)
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
			String startUrl = "http://win.mail.ru/cgi-bin/login";
			String loginUrl = "http://win.mail.ru/cgi-bin/auth";
			string strHidden ="";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				htmlResponse = htmlResponse.Substring(htmlResponse.IndexOf("<form method=\"post\" action=\"auth\""));
				htmlResponse = htmlResponse.Substring(0,htmlResponse.IndexOf("</form>"));
				strHidden = StringUtils.HiddenFields(htmlResponse);
				postString = strHidden+"&Login="+this.userId+"&Domain=mail.ru&Password="+this.password;
				htmlResponse = httpUtils.GetHttpResponse(loginUrl,false,postString,startUrl);
				if(htmlResponse.IndexOf("<input  type=\"password\" name=\"Password\"")==-1)
				{
					isLoggedIn = true;
				}
				else
				{
					isLoggedIn = false;
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
			this.names = new ArrayList();
			this.emails = new ArrayList();
			string[] nameArray = null;       
			string[] emailArray = null;
			string[] rows = null;
			string[] colums = null;
			String tmpName = "";
			String tmpEmail = "";
			int i=0;
			
			addressBookURL ="http://win.mail.ru/cgi-bin/addressbook";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(addressBookURL,false);
				String addTable = htmlResponse.Substring(htmlResponse.IndexOf("<table width=100% class=\"adr_book mt10\""));
				addTable = addTable.Substring(0,addTable.IndexOf("</table>"));
				rows = StringUtils.Split(addTable,"</tr>");
				nameArray = new string[rows.Length];
				emailArray = new string[rows.Length];
				for(i=1;i<rows.Length-2;i++)
				{
					tmpName="";
					tmpEmail="";
					colums = StringUtils.Split(rows[i],"<a id=");
					
					tmpName = colums[1].Substring(colums[1].IndexOf("\">") + "\">".Length);
					tmpName = tmpName.Substring(0,tmpName.IndexOf("</a>"));
					tmpName = tmpName.Replace("&amp;","&");
					
					tmpEmail = colums[3].Substring(colums[3].IndexOf("\">") + "\">".Length);
					tmpEmail = tmpEmail.Substring(0,tmpEmail.IndexOf("</a>"));
					
					nameArray[i] = tmpName;
					emailArray[i] = tmpEmail;
					if(((nameArray[i]==null) || nameArray[i].Equals(""))||((emailArray[i]==null) || emailArray[i].Equals("")))
					{
						continue;
					}
					else
					{
						names.Add(nameArray[i]);
						emails.Add(emailArray[i]);
					}
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
