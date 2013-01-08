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
	/// Summary description for SinaImporter.
	/// </summary>
	public class SinaImporter
	{
		private bool isLoggedIn = false;
		private String userId,password;
		private String addressBookURL = "";
		private String htmlResponse = "";
		private String postString="";
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();
		private HttpUtils httpUtils = null;
		private String domain = "";

		public SinaImporter()
		{
			httpUtils = new HttpUtils();
		}
		public SinaImporter(String user,String pass)
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
			String startUrl = "http://mail.sina.com.cn/";
			String loginUrl = "http://mail.sina.com.cn/cgi-bin/login.cgi";
			String url = "";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(startUrl,false);
				postString = "u="+this.userId+"&psw="+this.password+"&product=mail&"+HttpUtility.UrlEncodeUnicode("µÇÂ¼")+"="+HttpUtility.UrlEncodeUnicode("µÇ Â¼");
				htmlResponse = httpUtils.GetHttpResponse(loginUrl,true,postString,startUrl,"",true);
				this.domain = htmlResponse.Substring(htmlResponse.IndexOf("Location: http://")+"Location: http://".Length);
				this.domain = this.domain.Substring(0,this.domain.IndexOf(".sinamail"));
				url = htmlResponse.Substring(htmlResponse.IndexOf("Location: ")+"Location: ".Length);
				url = url.Substring(0,url.IndexOf("\r\nContent-Length"));
				htmlResponse = httpUtils.GetHttpResponse(url,false);
				if(htmlResponse.IndexOf("<input name=\"u\"") == -1)
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
			names = new ArrayList();
			emails = new ArrayList();
			String tmpName = "";
			String tmpEmail = "";
			int count=0,counter;
						
			addressBookURL = "http://"+this.domain+".sinamail.sina.com.cn/cgismarty/addr_export.php";
			try
			{
				htmlResponse = httpUtils.GetHttpResponse(addressBookURL,false);
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
						tmpName=temp_line[0]; 
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
