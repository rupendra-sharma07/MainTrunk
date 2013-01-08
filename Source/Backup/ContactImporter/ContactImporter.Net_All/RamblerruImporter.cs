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
	/// Summary description for RamblerruImporter.
	/// </summary>
	public class RamblerruImporter
	{
		private bool isLoggedIn = false;
		private String userId=String.Empty;
		private String password=String.Empty;
		private String htmlResponse = "";
		private String postString="";
		private String addressBookURL = "";
		private HttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();

		public RamblerruImporter()
		{
			httpUtils=new HttpUtils();
		}
		public RamblerruImporter(String userid,String password)
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
			String homePage = "http://mail.rambler.ru";
			String loginUrl = "";
			String frm = "";
			String reff="";
			this.addressBookURL="";
			try
			{
				htmlResponse=httpUtils.GetHttpResponse(homePage,true);
				frm=htmlResponse.Substring(htmlResponse.IndexOf("<form action=\"")+"<form action=\"".Length);
				loginUrl=frm.Substring(0,frm.IndexOf("\""));
				frm=frm.Substring(0,frm.IndexOf("</form>"));
				postString=StringUtils.HiddenFields(frm);
				loginUrl=loginUrl;
				postString=postString+"&login="+this.userId+"&passw="+HttpUtility.UrlEncode(this.password)+"&Login="+HttpUtility.UrlEncode("Войти!");
				htmlResponse=httpUtils.GetHttpResponse(loginUrl,true,postString,"");
				if(htmlResponse.IndexOf("<input type=\"password\" name=\"passw\"")==-1)
				{
					this.addressBookURL=htmlResponse.Substring(htmlResponse.IndexOf("<a id=\"addressbook-link\" href=\"")+"<a id=\"addressbook-link\" href=\"".Length);
					this.addressBookURL=this.addressBookURL.Substring(0,this.addressBookURL.IndexOf("\""));
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
			names = new ArrayList();
			emails = new ArrayList();

			adrUrl = "http://mail.rambler.ru/mail/"+this.addressBookURL;
			htmlResponse = httpUtils.GetHttpResponse(adrUrl,false,"","");

			string str1 = htmlResponse.Substring(htmlResponse.IndexOf("<table id=\"contacts-list\""));
			str1 = str1.Substring(0,str1.IndexOf("</table>"));
			
			string[] rows = StringUtils.Split(str1,"<tr class=\"vcard\">");
			string[] colums;
			for(int i= 1;i<rows.Length;i++)
			{
				rows[i]=rows[i].Replace("&nbsp;","");
				colums=StringUtils.Split(rows[i],"</td>");
				if(colums.Length>2)
				{
					emails.Add(StringUtils.StripTags(colums[1]));
					names.Add(StringUtils.StripTags(colums[2]));
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
