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
	/// Summary description for WpImporter.
	/// </summary>
	public class WpImporter
	{
		private bool isLoggedIn = false;
		private String userId=String.Empty;
		private String password=String.Empty;
		private String htmlResponse = "";
		private String postString="";
		private String reffer = "";
		private String addressBookURL = "";
		private HttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();

		public WpImporter()
		{
			httpUtils=new HttpUtils();
		}
		public WpImporter(String userid,String password)
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
			String homePage = "http://poczta.wp.pl/";
			String loginUrl = "";
			String frm = "";
			String reff="";
			try
			{

				htmlResponse=httpUtils.GetHttpResponse(homePage,true);
				frm=htmlResponse.Substring(htmlResponse.IndexOf("<form action=")+"<form action=".Length);
				frm=frm.Substring(0,frm.IndexOf("</form>"));
				postString=StringUtils.HiddenFields(frm);
				postString = postString.Replace("&javascript_off=test","");
				postString = postString.Replace("savessl=0","savessl=2");
				postString = postString.Replace("starapoczta=0","starapoczta=2");
				postString = postString.Replace("minipoczta=0","minipoczta=2");

				reff="http://profil.wp.pl/login.html?url=http%3A%2F%2Fpoczta.wp.pl%2Findex.html%3Fflg%3D1&serwis=nowa_poczta_wp";
				postString=postString+"&login_username="+this.userId+"&login_password="+password+"&subm=Zaloguj";
				loginUrl="http://profil.wp.pl/login.html";
				htmlResponse=httpUtils.GetHttpResponse(loginUrl,false,postString,reff);
				if(htmlResponse.IndexOf("<div class=\"userInfo\">")!=-1)
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
			this.names = new ArrayList();
			this.emails = new ArrayList();

			try
			{
						
				addressBookURL="http://ksiazka-adresowa.wp.pl/import-export.html";
				htmlResponse=httpUtils.GetHttpResponse(addressBookURL,false);
				String post="gr_id=0&program=yh&x=29&y=3";
				String exportUrl="http://ksiazka-adresowa.wp.pl/csv.html";
				String addressresponse=httpUtils.GetHttpResponse(exportUrl,false,post,addressBookURL,"");

				System.IO.MemoryStream strm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(addressresponse));
				System.IO.StreamReader str = new System.IO.StreamReader(strm);
				int count=0,counter;
				addresses b=new addresses();
				String temp_name="",temp_email="";
				CSVReader csv = new CSVReader(str); 
				string[] fields;
				string[] temp_line;
				while ((fields = csv.GetCSVLine()) != null) 
				{
					temp_line=new string[100];
					if(count!=0)
					{
						counter=0;
						foreach (string field in fields) 
						{   
							temp_line[counter]=field;
							counter++;
						}
						temp_name=(temp_line[0]+" "+temp_line[1]+" "+temp_line[4]); 
						
						temp_email=(temp_line[2]!="") ? temp_line[2]:(((temp_line[22]!="")? temp_line[22]:""));
						if(temp_email!=null && temp_name==null)
						{
							temp_name=temp_email.Substring(0,temp_email.IndexOf("@"));
						}
						if(temp_email!=null && temp_name!=null)
						{
							names.Add(temp_name);
							emails.Add(temp_email);
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
