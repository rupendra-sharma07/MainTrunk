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
	/// Summary description for InteriaImporter.
	/// </summary>
	public class InteriaImporter
	{
		private bool isLoggedIn = false;
		private String userId=String.Empty;
		private String password=String.Empty;
		private String htmlResponse = "";
		private String postString="";
		private String inpl_mail_token = "";
		private HttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();

		public InteriaImporter()
		{
			httpUtils=new HttpUtils();
		}
		public InteriaImporter(String userid,String password)
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
			String homePage = "http://poczta.interia.pl/";
			String loginUrl = "http://ssl.interia.pl/login.html";
			String frm = "";
			String reff="";
			this.inpl_mail_token="";
			
			try
			{
				htmlResponse=httpUtils.GetHttpResponse(homePage,true);
				String uname=this.userId.Substring(0,this.userId.IndexOf("@"));
				postString = "referer="+HttpUtility.UrlEncode("http://poczta.interia.pl/poczta/")+"&login="+uname+"&domain=interia.pl&pass="+HttpUtility.UrlEncode(this.password)+"&htmlMail=on&formSubmit.x=39&formSubmit.y=14";
				htmlResponse=httpUtils.GetHttpResponse(loginUrl,true,postString,homePage);
				//String reLasturl = "http://poczta.interia.pl/html/";
				//htmlResponse=httpUtils.GetHttpResponse(reLasturl,true,postString,homePage);
				this.inpl_mail_token=htmlResponse.Substring(htmlResponse.IndexOf("inpl_mail_token=")+"inpl_mail_token=".Length);
				this.inpl_mail_token=this.inpl_mail_token.Substring(0,this.inpl_mail_token.IndexOf(";"));
				if(htmlResponse.IndexOf("id=\"formLogin\" name=\"login\"")==-1)
				{
					
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
			String adrUrl,htmlResponse,referer;
			names = new ArrayList();
			emails = new ArrayList();
			referer="http://poczta.interia.pl/html/";
			adrUrl = "http://poczta.interia.pl/html/getcontacts,uid,"+this.inpl_mail_token+"?inpl_network_request=true";
			htmlResponse = httpUtils.GetHttpResponse(adrUrl,true,"","referer");
			String addresses = htmlResponse.Substring(htmlResponse.IndexOf("[{")+"[{".Length);
			addresses=addresses.Substring(0,addresses.IndexOf("}]"));
			string[] rows=StringUtils.Split(addresses,"},");

			
			for(int i= 0;i<rows.Length;i++)
			{
				String tempName="";
				string[] column=StringUtils.Split(rows[i],","); 

				String name=column[1].Substring(column[1].IndexOf(":\"")+":\"".Length);
				name=name.Substring(0,name.IndexOf("\""));

				String fname=column[2].Substring(column[2].IndexOf(":\"")+":\"".Length);
				fname=fname.Substring(0,fname.IndexOf("\""));
				
				String lname=column[3].Substring(column[3].IndexOf(":\"")+":\"".Length);
				lname=lname.Substring(0,lname.IndexOf("\""));

				String nickname=column[4].Substring(column[4].IndexOf(":\"")+":\"".Length);
				nickname=nickname.Substring(0,nickname.IndexOf("\""));

				String email=column[5].Substring(column[5].IndexOf(":\"")+":\"".Length);
				email=email.Substring(0,email.IndexOf("\""));

				if(fname!="" && lname!="")
				{
					tempName=fname+" "+lname;
				}
				else if(name!="")
				{
					tempName=name;
				}
				else if(nickname!="")
				{
					tempName=nickname;
				}
				else if(fname!="")
				{
					tempName=fname;
				}
				else if(lname!="")
				{
					tempName=lname;
				}

				if(tempName=="" && email!="")
				{
					tempName=email.Substring(0,email.IndexOf("@"));
				}

				if(StringUtils.IsValidEmail(email)) 
				{
					names.Add(tempName);
					emails.Add(email);
				}
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
