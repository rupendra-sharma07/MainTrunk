using System;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Improsys.ContactImporter.Util; 

namespace Improsys.ContactImporter
{
	/// <summary>
	/// Summary description for UolImporter.
	/// </summary>
	public class BolUolImporter
	{
		private String userId,password;
		private bool isLoggedIn=false;
		private ArrayList names=null;
		private ArrayList emails=null;
		private HttpUtils httpUtils = null;
		
		public BolUolImporter()
		{
			httpUtils = new HttpUtils();
		}
		public BolUolImporter(String userId,String password)
		{
			this.userId=userId;
			this.password=password;
			httpUtils = new HttpUtils();
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
			String startUrl = "http://www.bol.uol.com.br/";
			String nextUrl,referrer;
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				referrer = startUrl;
				nextUrl = "https://visitante.acesso.uol.com.br/login.html"; 
				postString = "skin=bol-default&dest=WEBMAIL&user="+HttpUtility.UrlEncode(this.userId)+"&pass="+HttpUtility.UrlEncode(this.password)+"&x=32&y=10";
				htmlResponse = httpUtils.GetHttpResponse(nextUrl, false,postString, referrer);
				if(htmlResponse.IndexOf("<title>WEBMAIL</title>")!=-1)
				{
					nextUrl=htmlResponse.Substring(htmlResponse.IndexOf("action=\"")+"action=\"".Length);
					nextUrl=nextUrl.Substring(0,nextUrl.IndexOf("\">"));
					String val=htmlResponse.Substring(htmlResponse.IndexOf("value=\"")+"value=\"".Length);
					val=val.Substring(0,val.IndexOf("\""));
					postString="_webmail_session_id="+val;
					htmlResponse=httpUtils.GetHttpResponse(nextUrl,false,postString,"");
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
			String adrUrl,adrJs,referrer,htmlResponse;
			names = new ArrayList();
			emails = new ArrayList();
			
			try
			{
				adrUrl = "http://bmail.uol.com.br/contact";
			
				referrer = "http://bmail.uol.com.br/contact";
				htmlResponse=httpUtils.GetHttpResponse(adrUrl ,false,"",referrer);
				if(htmlResponse.IndexOf("<div class=\"pagination\">")!=-1)
				{
					string pagination=htmlResponse.Substring(htmlResponse.IndexOf("<div class=\"pagination\">"));
					pagination=pagination.Substring(0,pagination.IndexOf(">>>"));
			
					string pattern=@">\d<";
					string[] pagi=StringUtils.Split(pagination,pattern);
			
					for(int i=1;i<pagi.Length;i++)
					{
				
						adrJs="http://bmail.uol.com.br/contact?folder=INBOX&page="+i.ToString();
						htmlResponse = httpUtils.GetHttpResponse(adrJs,false,"",referrer);
				
						pattern=@"<a href=""/contact/edit_contact/.*?>";
						string[] n_es=StringUtils.Split(htmlResponse,pattern);
						for(int j=1;j<n_es.Length;j++)
						{
							String text=n_es[j];
							String name_email=text.Substring(0,text.IndexOf("<"));
							String[] name_email_arr=name_email.Split(',');
							names.Add(name_email_arr[0].Trim());
							emails.Add(name_email_arr[1].Trim());
						}
					}
				}
				else if(htmlResponse.IndexOf("<a href=\"/contact/edit_contact/")!=-1)
				{
					string pattern=@"<a href=""/contact/edit_contact/.*?>";
					string[] n_es=StringUtils.Split(htmlResponse,pattern);
					for(int j=1;j<n_es.Length;j++)
					{
						String text=n_es[j];
						String name_email=text.Substring(0,text.IndexOf("<"));
						String[] name_email_arr=name_email.Split(',');
						names.Add(name_email_arr[0].Trim());
						emails.Add(name_email_arr[1].Trim());
					}
				
				}
			}
			catch(Exception e)
			{
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
	}
}
