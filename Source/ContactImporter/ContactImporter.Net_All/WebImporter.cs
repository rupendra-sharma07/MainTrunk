using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util; 

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for AolImporter.
	/// </summary>
	public class WebImporter : IDisposable 
	{
		private String userId,password;
		private bool isLoggedIn=false;
		private ArrayList names=null;
		private ArrayList emails=null;
		private HttpUtils httpUtils = null;
		private String aCurmbox="";
		private String si="";

		private String domain,uid,verString;
		
		public WebImporter()
		{
			httpUtils = new HttpUtils();
		}

		public WebImporter(String userId,String password)
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
			String htmlResponse="",postString;
			String startUrl = "http://www.web.de";
			String nextUrl,referrer;
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				referrer = "http://www.web.de";
				nextUrl = "https://login.web.de/intern/login/"; 
				postString = "service=freemail&server="+HttpUtility.UrlEncode("https://freemail.web.de")+"&onerror="+HttpUtility.UrlEncode("https://freemail.web.de/msg/temporaer.htm")+"&rv_dologon=Login&password="+HttpUtility.UrlEncode(password)+"&username="+HttpUtility.UrlEncode(userId);
				htmlResponse = httpUtils.GetHttpResponse(nextUrl, true, postString, referrer);
				if(htmlResponse.IndexOf("logonfailed")==-1)
				{
					isLoggedIn = true;
					aCurmbox = StringUtils.ParseValueBetween (htmlResponse, "freemailng", ".");
					si = StringUtils.ParseValueBetween (htmlResponse, "?si=", "&");
				}
				else
				{
					isLoggedIn = false;
				}
			}
			catch(Exception e)
			{
				isLoggedIn = false;
				// Console.WriteLine("Exception occured in DoLogin : " + e.Message );
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
			String adrUrl,htmlResponse;
			names = new ArrayList();
			emails = new ArrayList();

			adrUrl = "https://freemailng"+this.aCurmbox+".web.de/online/adressbuch/?si="+this.si+"&rv_linkfrom=startseite";
			htmlResponse = httpUtils.GetHttpResponse(adrUrl, false);
			string sid= StringUtils.ParseValueBetween (htmlResponse, "?sid=", "\"");
			adrUrl = "https://kontakte.web.de/?ind=ALL&sortier=0&cat=&sid="+sid;
			htmlResponse = httpUtils.GetHttpResponse(adrUrl, false);
			htmlResponse =StringUtils.ParseValueBetween (htmlResponse, "<b>Mobilfunknummer</b></td>", "<td width=\"640\" colspan=\"6\" align=\"left\"><table width=\"640\"");
			string[] rows = StringUtils.Split(htmlResponse,"</tr>");
			string[] colums;
			for(int i= 3;i<rows.Length-1;i++)
			{
				rows[i]=rows[i].Replace("&nbsp;","");
				colums=StringUtils.Split(rows[i],"</td>");
				if(colums.Length>2)
				{
					names.Add(StringUtils.StripTags(colums[2]));
					emails.Add(StringUtils.StripTags(colums[3]));
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

		public void Dispose() 
		{
			Logout();
			GC.SuppressFinalize(this);
		}



																			  
	}
}
