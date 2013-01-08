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
	/// Summary description for OneTwoSixImporter.
	/// </summary>
	public class OneTwoSixImporter
	{
		private bool isLoggedIn = false;
		private String userId="";
		private String password="";
		private String htmlResponse = "";
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();		
		private String sid="";
		private String url="";
		private String url1="";
		private WinHttpUtils httpUtils = null;

		public OneTwoSixImporter()
		{
			httpUtils=new WinHttpUtils();
		}
		public OneTwoSixImporter(String userid,String password)
		{
			httpUtils=new WinHttpUtils();
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
			String homePage = "http://www.126.com";
			String loginUrl = "";
			String referer="";
			String str="";	
			String poststring="";
			String login="";
         
			try
			{
				htmlResponse=httpUtils.GetHttpResponse(homePage,false);
				str=htmlResponse.Substring(htmlResponse.IndexOf("<form method=\"post\""));
				str=str.Substring(0,str.IndexOf("</form>"));
				poststring=StringUtils.HiddenFields(str);
				poststring=poststring+"&user="+ this.userId +"&pass="+ password +"&style=-1&remUser=&secure=&enter.x=%B5%C7+%C2%BC";
				loginUrl="https://entry.mail.126.com/cgi/login?redirTempName=https.htm&hid=10010102&lightweight=1&verifycookie=1&language=0&style=-1";
				login=httpUtils.GetHttpResponse(loginUrl,false,poststring,homePage);
				if(login.IndexOf("<form method=\"post\" action=\"http://entry.126.com/cgi/login\"")==-1)
				{
					this.url=login.Substring(login.IndexOf("(\"http://")+("(\"http://").Length);
					this.url=url.Substring(0,this.url.IndexOf("."));
					this.sid=login.Substring(login.IndexOf("sid=")+("sid=").Length);
					this.sid=sid.Substring(0,this.sid.IndexOf("\""));
					referer="http://"+ this.url + ".mail.126.com/a/p/main.htm?sid=" + this.sid;
					this.url1="http://" + this.url + ".mail.126.com/a/p/main.htm?sid=" + this.sid;
					htmlResponse=httpUtils.GetHttpResponse(this.url1,false);
					isLoggedIn=true;
				}
				else
				{
					isLoggedIn=false;
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
			String referr="",addressPage="",address="";
			this.names=new ArrayList();
			this.emails=new ArrayList();
			Hashtable htcontype=new Hashtable();
			try
			{
			
				String xml="<?xml version=\"1.0\"?><object><array name=\"items\"><object><string name=\"func\">pab:searchContacts</string><object name=\"var\"><array name=\"order\"><object><string name=\"field\">FN</string><boolean name=\"ignoreCase\">true</boolean></object></array></object></object><object><string name=\"func\">user:getSignatures</string></object><object><string name=\"func\">pab:getAllGroups</string></object></array></object>";
				String addressUrl="http://"+ this.url+".mail.126.com/a/s?sid="+this.sid+"&func=global:sequential";
				referr="http://"+this.url+".mail.126.com/a/f/dm3/0804231430/index_v3.htm";
				
				htcontype.Add("Content-Type","application/xml");
				htcontype.Add("Accept","text/javascript");
				htcontype.Add("Host",this.url+".mail.126.com");

				addressPage=httpUtils.GetHttpResponse(addressUrl,false,xml,referr,htcontype);


				address=addressPage.Substring(addressPage.IndexOf("[{\n'id':'1',")+("[{\n'id':'1',").Length);
				address=address.Substring(0,address.IndexOf("'code':'S_OK'"));
				String[] rows=StringUtils.Split(address,"'ADR;HOME':';;;;;;CI'");


				for(int i=0;i<rows.Length-1;i++)
				{
					String tr=rows[i].Substring(rows[i].IndexOf("'FN':'")+("'FN':'").Length);
					String tr_name=tr.Substring(0,tr.IndexOf("'")).Trim();
					tr=rows[i].Substring(rows[i].IndexOf("'EMAIL;PREF':'")+("'EMAIL;PREF':'").Length);
					String tr_email=tr.Substring(0,tr.IndexOf("'")).Trim();
					if(tr_name !="")
					{
						this.names.Add(tr_name);
						this.emails.Add(tr_email);
					}
				}
				if(this.names.Count>0)
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
