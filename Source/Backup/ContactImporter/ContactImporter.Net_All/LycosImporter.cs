/**
 * @author Sumon sumon@improsys.com, Mamun mamun@improsys.com
 * @history
 *          created  : Md.Nuruzzaman Iran : Date : 09-11-2008
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
	/// Summary description for LycosImporter.
	/// </summary>
	public class LycosImporter
	{
		private bool isLoggedIn = false;
		private String userId=String.Empty;
		private String password=String.Empty;
		private String htmlResponse = "";
		private String postString="";
		private String addressBookURL = "";
		private String referer = "";
		private HttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();

		public LycosImporter()
		{
			httpUtils=new HttpUtils();
		}
		public LycosImporter(String userid,String password)
		{
			httpUtils=new HttpUtils();
			this.userId=userid;
			this.password=password;
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
			this.userId = userId;
			this.password = password; 
			return DoLogin();
		}
		public bool DoLogin()
		{
			Logout(true);
			String homePage = "http://mail.lycos.com/lycos/mail/IntroMail.lycos";
			String loginUrl = "";
			String frm = "";
			this.addressBookURL="";
			try
			{
				htmlResponse=httpUtils.GetHttpResponse(homePage,true);
				frm=htmlResponse.Substring(htmlResponse.IndexOf("<form method=\"post\" action=\"")+"<form method=\"post\" action=\"".Length);
				loginUrl=frm.Substring(0,frm.IndexOf("\""));
				frm=frm.Substring(0,frm.IndexOf("</form>"));
				postString=StringUtils.HiddenFields(frm);
				//String uname=this.userId.Substring(0,this.userId.IndexOf("@"));
				
				postString=postString+"&m_U="+this.userId+"&m_P="+HttpUtility.UrlEncode(this.password)+"&login=Sign In";
				htmlResponse=httpUtils.GetHttpResponse(loginUrl,true,postString,homePage);
				if(htmlResponse.IndexOf("<input type='text' id='m_U' name='m_U'")==-1)
				{
					String leftform = htmlResponse.Substring(htmlResponse.IndexOf("<frame name=left src=\"")+"<frame name=left src=\"".Length);
					leftform = leftform.Substring(0,leftform.IndexOf("\""));
					leftform = "http://mail.lycos.com"+leftform;
					this.referer=leftform;
					htmlResponse=httpUtils.GetHttpResponse(this.referer,false,"","http://mail.lycos.com/lycos/Index.lycos");
					//this.addressBookURL=htmlResponse.Substring(htmlResponse.IndexOf(">Address Book</a>")+">Address Book</a>".Length);
					//this.addressBookURL = this.addressBookURL.Substring(this.addressBookURL.IndexOf("<a href=\"/")+"<a href=\"/".Length);
					//this.addressBookURL = this.addressBookURL.Substring(0,this.addressBookURL.IndexOf("\""));
					isLoggedIn = true;
				}
				else
				{
					isLoggedIn = false;
				}
			}
			catch
			{
				isLoggedIn=false;
			}
			return isLoggedIn;

			
		}
		public bool ImportContacts()
		{
			String adrUrl,htmlResponse;
			int count=0,counter;
			String tmpName = "";
			String tmpEmail = "";
			names = new ArrayList();
			emails = new ArrayList();

			//adrUrl = "http://mail.lycos.com"+this.addressBookURL;
			//htmlResponse = httpUtils.GetHttpResponse(adrUrl,false,"",this.referer);
			String exportUrl = "http://mail.lycos.com/lycos/addrbook/ExportAddr.lycos";
			htmlResponse = httpUtils.GetHttpResponse(exportUrl,false,"",this.referer);

			String export = "http://mail.lycos.com/lycos/addrbook/ExportAddr.lycos?ptype=act&fileType=EXPRESS";
			String postStr = "ftype=EXPRESS";
			htmlResponse = httpUtils.GetHttpResponse(export,false,postStr,exportUrl);

			System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(htmlResponse));
			System.IO.StreamReader str = new System.IO.StreamReader(strm);
			CSVReader csv = new CSVReader(str); 
			string[] fields;
			string[] temp_line;
				
			while ((fields = csv.GetCSVLine()) != null) 
			{
				//					if(fields.Length < 5)
				//						continue;
				temp_line=new string[100];
				//if(fields.Length< 81)
				//{
				//	fields = StringUtils.Split(fields[0].ToString(),";");
				//}
				if(count!=0) 
				{
					counter=0;
					foreach (string field in fields) 
					{   
						temp_line[counter]=field;
						counter++;
					}

					tmpName=(temp_line[3]=="") ? temp_line[0]:temp_line[2];
					tmpName=(temp_line[0]=="" && temp_line[2]=="") ? temp_line[3]:((temp_line[0]!="" && temp_line[2]!="") ? temp_line[0]+" "+temp_line[1]+" "+temp_line[2]:tmpName) ;	
				
					tmpEmail = (temp_line[4]!="") ? temp_line[4]:(((temp_line[12]!="")? temp_line[12]:((temp_line[13]!="") ? temp_line[13]:"")));
							
					if(tmpEmail!=null)
					{
						tmpName =  tmpName.Replace("\""," ").Trim();
						tmpEmail = tmpEmail.Replace("\""," ").Trim();

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
				}
				count++;
			}

			if(names.Count>0) 
				return true;
			else
				return false;
		}

		public void Dispose() 
		{
			Logout();
			GC.SuppressFinalize(this);
		}

		private int RandomNumber(int min, int max)
		{

			Random random = new Random();

			return random.Next(min, max); 

		}

																			  
	}
}
