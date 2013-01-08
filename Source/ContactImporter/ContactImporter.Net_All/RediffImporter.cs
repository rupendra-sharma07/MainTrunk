using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util; 

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for RediffImporter.
	/// </summary>
	public class RediffImporter
	{
		private String userId,password;
		private bool isLoggedIn=false;
		private ArrayList names=null;
		private ArrayList emails=null;
		private HttpUtils httpUtils = null;
		private string addressURL="";
		private String domain = "";
		private String sessionID = "";
		
		public RediffImporter()
		{
			httpUtils = new HttpUtils();
		}
		public RediffImporter(String userId,String password)
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
				password = value;
			}
		}

		public bool IsLoggedIn
		{
			get
			{
				return isLoggedIn;
			}
			set
			{
				this.isLoggedIn = value;
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

		

		public bool DoLogin(String userId,String password)
		{
			this.userId = userId;
			this.Password = password; 
			return DoLogin();
		}
		// Login to Rediff mail server
		public bool DoLogin()
		{
			Logout(true);
			String htmlResponse="",postString;
			String startUrl = "http://www.rediff.com/";
			String nextUrl,referrer;
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				referrer = startUrl;
				nextUrl = "http://mail.rediff.com/cgi-bin/login.cgi"; 
				postString = "FormName=existing&login=" + this.userId + "&passwd=" + this.password+"&proceed=GO";
				htmlResponse = httpUtils.GetHttpResponse(nextUrl, false, postString, referrer);
				if(htmlResponse.IndexOf("window.location.replace")!=-1)
				{
					String redirectUrl = htmlResponse.Substring(htmlResponse.IndexOf("replace(\"")+"replace(\"".Length);
					redirectUrl = redirectUrl.Substring(0,redirectUrl.IndexOf("\")"));
					if(redirectUrl.IndexOf("session_id")!=-1)
					{
						this.sessionID = redirectUrl.Substring(redirectUrl.IndexOf("session_id=")+"session_id=".Length);
						this.sessionID = this.sessionID.Substring(0,this.sessionID.IndexOf("&"));
					}
					htmlResponse = httpUtils.GetHttpResponse(redirectUrl,false);
				}
				if(htmlResponse.IndexOf("Welcome " + this.userId)!=-1)
				{
					isLoggedIn = true;
					int index = htmlResponse.IndexOf("class=\"sb4\"><A HREF=\"") + "class=\"sb4\"><A HREF=\"".Length;
					this.addressURL = htmlResponse.Substring(index);
					this.addressURL = this.addressURL.Substring(0,this.addressURL.IndexOf("\""));
					this.domain = this.addressURL.Substring(0,this.addressURL.IndexOf(".com")+".com".Length);
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
				Console.WriteLine("Exception occured in DoLogin : " + e.Message );
			}
			return isLoggedIn;
		}
		// Get User Contacts
		public bool ImportContacts()
		{
			String referrer="",htmlResponse;
			names = new ArrayList();
			emails = new ArrayList();
			

			if(this.addressURL.IndexOf("session_id")!=-1)
			{
				this.sessionID = this.addressURL.Substring(this.addressURL.IndexOf("session_id=")+"session_id=".Length);
				this.sessionID = this.sessionID.Substring(0,this.sessionID.IndexOf("&SrtFld"));
			}
			

			this.addressURL = this.domain + "/iris/Main?do=getXmlAddressBook&filter=all&login=" + this.userId + "&session_id=" + this.sessionID;

			htmlResponse = httpUtils.GetHttpResponse(this.addressURL, false, "",referrer);
			
			XMLReader xml = new XMLReader(htmlResponse);
			xml.ReadValue();
			names = xml.Name;
			emails = xml.Email;

			if(names.Count>0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private string getHiddenFields(String str)
		{
			string pattern=@"<INPUT.*hidden.*?>";
			MatchCollection mc=Regex.Matches(str, pattern);
			string text="",t_text="",name="",val="";
			string nmstr="name=",vlstr="value=";
			foreach (Match m in mc)
			{
				t_text=m.ToString();
				name=t_text.Substring(t_text.IndexOf(nmstr)+nmstr.Length);
				name=name.Substring(0,name.IndexOf(">"));
				val=t_text.Substring(t_text.IndexOf(vlstr)+vlstr.Length);
				val=val.Substring(0,val.IndexOf(" "));
				if(text!="")
					text+="&"+name+"="+val;
				else
					text+=name+"="+val;
			}
			return text;
		}

		public void Logout()
		{
			Logout(false);
		}

		public void Dispose() 
		{
			Logout();
			GC.SuppressFinalize(this);
		}
	}
}
