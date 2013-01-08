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
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util;

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for MyspaceImporter.
	/// </summary>
	public class MyspaceImporter 
	{
		private bool isLoggedIn = false;
		private String userId,password;
		private String myToken = "";
		private String hash = "";
		private String reffer = "";
		private String addressBookURL = "";
		private String htmlResponse = "";
		private String postString="";
		private HttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();

		int pos = 0;

		public MyspaceImporter()
		{
			httpUtils = new HttpUtils();
		}
		public MyspaceImporter(String user,String pass)
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
		
		public bool DoLogin()
		{
			Logout(true);
			String homePage = "http://www.myspace.com";
			String loginUrl = "http://login.myspace.com/index.cfm?fuseaction=login.process&MyToken=";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(homePage,false);
				reffer=homePage;
				String tempStr="MyToken=";
				int pos = htmlResponse.IndexOf(tempStr);
				myToken = htmlResponse.Substring(pos+tempStr.Length);
				myToken = myToken.Substring(0,myToken.IndexOf("\""));
				loginUrl+=myToken;
				postString = "email="+userId+"&password="+HttpUtility.UrlEncode(password)+"&Submit22_x=33&Submit22_y=10";
				htmlResponse = httpUtils.GetHttpResponse(loginUrl,false,postString,reffer);
				if(htmlResponse.IndexOf("<input type=\"text\" name=\"email\"")==-1)
				{
					// Begin Outer If
					if(htmlResponse.IndexOf("Member Login")==-1)
					{
						// Begin 1st inner if
						if(htmlResponse.IndexOf("Skip this Advertisement")>1)
						{
							htmlResponse=htmlResponse.Substring(0,htmlResponse.IndexOf("Skip this Advertisement"));
							tempStr="<a href=\"";
							String urlToFetch=htmlResponse.Substring(htmlResponse.IndexOf(tempStr)+tempStr.Length);
							urlToFetch=urlToFetch.Substring(0,urlToFetch.IndexOf("\""));
							htmlResponse=httpUtils.GetHttpResponse(urlToFetch,false,"",loginUrl);
						
						}
						//End 1st inner if.

						// Begin of 2nd inner if
						if(htmlResponse.IndexOf("You have\n <span><a href=\"")==-1)
						{
							htmlResponse=httpUtils.GetHttpResponse("http://home.myspace.com/index.cfm?fuseaction=user",false);
						
						}
						// End of 2nd inner if
						tempStr="<span><a href=\"";
						String fPage=htmlResponse.Substring(htmlResponse.IndexOf(tempStr)+tempStr.Length);
						fPage=fPage.Substring(0,fPage.IndexOf("\""));
						String home="";


						isLoggedIn = true;
					}
				}
				else
				{
					isLoggedIn = false;
				}
			}
				//Outer if close
				// End of outer if
			catch (Exception e)
			{
				isLoggedIn = false;
			}
			return isLoggedIn;
		}
		public bool DoLogin(String userId,String password)
		{
			this.UserId = userId;
			this.Password = password; 
			return DoLogin();
		}
		public bool ImportContacts()
		{
			names = new ArrayList();
			emails = new ArrayList();
			string[] nameArray = null;       
			string[] emailArray = null;
			string[] rows = null;
			string[] colums = null;
			//int i = 0;
			addressBookURL = "http://addressbook.myspace.com/index.cfm?fuseaction=adb&MyToken="+myToken;

			try
			{
				htmlResponse = httpUtils.GetHttpResponse(addressBookURL,false); 
				String addTable = htmlResponse.Substring(htmlResponse.IndexOf("<table id=\"addresses\""));
				addTable = addTable.Substring(0,addTable.IndexOf("</table"));
				rows = StringUtils.Split(addTable,"</tr>");
				nameArray = new string[rows.Length];
				emailArray = new string[rows.Length];
				for(int i=1;i<rows.Length-1;i++ )
				{
					colums = StringUtils.Split(rows[i],"</td>");
					nameArray[i] = StringUtils.StripTags(colums[1]).TrimStart();
					emailArray[i] = StringUtils.StripTags(colums[2]).TrimStart();
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
