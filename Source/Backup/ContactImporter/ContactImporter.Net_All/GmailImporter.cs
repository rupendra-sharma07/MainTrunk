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
	public class GmailImporter : IDisposable 
	{
		private String userId,password;
		private bool isLoggedIn=false;
		private ArrayList names=null;
		private ArrayList emails=null;
		private HttpUtils httpUtils = null;

		public GmailImporter()
		{
			httpUtils = new HttpUtils();
		}

		public GmailImporter(String userId,String password)
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
			String startUrl = "http://www.gmail.com/";
			String nextUrl,referrer;
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				referrer = "https://www.google.com/accounts/ServiceLogin?service=mail&passive=true&rm=false&continue=http%3A%2F%2Fmail.google.com%2Fmail%2F%3Fui%3Dhtml%26zy%3Dl";
				nextUrl = "https://www.google.com/accounts/ServiceLoginAuth"; 
				postString = "rm=false&service=mail&Email="+HttpUtility.UrlEncode(userId)+"&Passwd="+HttpUtility.UrlEncode(password)+"&null=Sign%20in&continue=http%3A%2F%2Fmail.google.com%2Fgmail/?";
				htmlResponse = httpUtils.GetHttpResponse(nextUrl, false,postString, referrer);
				if(htmlResponse.IndexOf("errormsg")==-1)
				{
					isLoggedIn = true;
				}
				else
					isLoggedIn = false;
			}
			catch(Exception e)
			{
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
			String adrUrl,referrer,htmlResponse;
            names = new ArrayList();
			emails = new ArrayList();
			string name = "";
			string email = "";
			adrUrl = "http://mail.google.com/mail/contacts/data/contacts?thumb=true&groups=true&show=ALL&psort=Name&max=100000&out=js&rf=&jsx=true";
			referrer = "http://mail.google.com/mail/contacts/ui/ContactManager?js=RAW";
			htmlResponse = httpUtils.GetHttpResponse(adrUrl,false,"",referrer);
			string str1 = htmlResponse.Substring(htmlResponse.IndexOf("\"Contacts\""));
			str1 = str1.Substring(0,str1.IndexOf("\"Groups\":[{\"Count\":"));
			string[] lines;
			lines = StringUtils.Split(str1,"\"Affinity\"");
			string tempstr="";
			foreach(string s in lines)
			{
				if(s.IndexOf("\"Address\":\"")==-1)
				{
					continue;
				}
				else
				{
					tempstr = s.Substring(s.IndexOf("\"Address\":\"")+"\"Address\":\"".Length);
					email = tempstr.Substring(0,tempstr.IndexOf("\""));
					email = email.Replace("\\u0027","'");
				}
				if(s.IndexOf("\"FullName\":{\"Unstructured\":\"")==-1)
				{
					name = email.Substring(0,email.IndexOf("@"));
				}
				else
				{
					tempstr = s.Substring(s.IndexOf("\"FullName\":{\"Unstructured\":\"")+"\"FullName\":{\"Unstructured\":\"".Length);
					name = tempstr.Substring(0,tempstr.IndexOf("\""));
					name = name.Replace("\\u0027","'");
					name = name.Replace("\\u0022","\"");
					name = name.Replace("\\u003D","=");
					name = name.Replace("\\u0026","&");
				}
				names.Add(name);
				emails.Add(email);
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
