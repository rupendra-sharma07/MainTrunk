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
	/// Summary description for liberoitImporter.
	/// </summary>
	public class liberoitImporter
	{
		private bool isLoggedIn = false;
		private String userId=String.Empty;
		private String password=String.Empty;
		private String htmlResponse = "";
		private String postString="";
		private String addressBookURL = "";
		private String dediurl = "";
		
		private HttpUtils httpUtils = null;
		private ArrayList names = new ArrayList();
		private ArrayList emails = new ArrayList();

		public liberoitImporter()
		{
			httpUtils=new HttpUtils();
		}
		public liberoitImporter(String userid,String password)
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
			String homePage = "http://liberomail.libero.it/";
			String loginUrl = "https://login.libero.it/logincheck.php";
			String frm = "";
			String reff="";
			this.addressBookURL="";
			this.dediurl = "";
			try
			{
				htmlResponse=httpUtils.GetHttpResponse(homePage,true);
				
				frm=htmlResponse.Substring(htmlResponse.IndexOf("<form action=")+"<form action=".Length);
				frm=frm.Substring(0,frm.IndexOf("</form"));
				
				this.dediurl=frm.Substring(frm.IndexOf("name=RET_URL value=\"")+"name=RET_URL value=\"".Length);
				this.dediurl = this.dediurl.Substring(this.dediurl.IndexOf("http://")+"http://".Length);
				this.dediurl = this.dediurl.Substring(0,this.dediurl.IndexOf("."));
				
				postString=StringUtils.HiddenFields(frm);
				postString="LOGINID="+this.userId+"&PASSWORD="+HttpUtility.UrlEncode(this.password)+"&"+postString;
				
				htmlResponse=httpUtils.GetHttpResponse(loginUrl,true,postString,"");
				
				if(htmlResponse.IndexOf("Ciao, <B>mamunch</B>@libero.it")!=-1)
				{
					this.addressBookURL=htmlResponse.Substring(htmlResponse.IndexOf("Leggi Mail"));
					this.addressBookURL=this.addressBookURL.Substring(this.addressBookURL.IndexOf("<A HREF=\"/")+"<A HREF=\"/".Length);
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
			String adrUrl,htmlResponse,referer;
			names = new ArrayList();
			emails = new ArrayList();
			int count=0,counter;
			String tmpName = "";
			String tmpEmail = "";

			adrUrl = "http://"+this.dediurl+".libero.it/"+this.addressBookURL;
			referer="http://"+this.dediurl+".libero.it/email.php";
			
			htmlResponse = httpUtils.GetHttpResponse(adrUrl,true,"",this.dediurl);
			
			String id=this.addressBookURL.Substring(this.addressBookURL.IndexOf("?ID=")+"?ID=".Length);
			id = id.Substring(0,id.IndexOf("&"));
			String addurl="http://"+this.dediurl+".libero.it/cgi-bin/abook.cgi";
			referer="http://"+this.dediurl+".libero.it/cgi-bin/toolbar.cgi?ID="+id;
			String postString="ID="+id+"&Act_ABook=1&DIRECT=1&Template=&Language=&ab_list_mode=1&C_Folder=";
			
			htmlResponse = httpUtils.GetHttpResponse(addurl,true,postString,referer);
			
			String form = htmlResponse.Substring(htmlResponse.IndexOf("<form name=\"abookForm\""));
			form = form.Substring(0,form.IndexOf("</form>"));
			String hidden = StringUtils.HiddenFields(form);
			hidden = hidden.Replace("&SUB_DUMMY=0","&AB_PATTERN=");
			postString = hidden+"&Act_AB_Export=export";
			String exprturl = "http://"+this.dediurl+".libero.it/cgi-bin/abook.cgi";
			
			htmlResponse = httpUtils.GetHttpResponse(exprturl,true,postString,addurl);
			
			form = htmlResponse.Substring(htmlResponse.IndexOf("<form ACTION=\""));
			form = form.Substring(0,form.IndexOf("</form>"));
			String hiddenfld = StringUtils.HiddenFields(form);
			hiddenfld = hiddenfld.Replace("&SUB_DUMMY=0","");
			hiddenfld = hiddenfld.Replace("AB_Export_Type=Export_Ldif","AB_Export_Type=Export_Csv");
			postString = hiddenfld+"&Act_AB_Export=1&AB_PATTERN=&exp=";
			
			htmlResponse = httpUtils.GetHttpResponse(exprturl,true,postString,addurl);
			
			String contacts=htmlResponse.Substring(htmlResponse.IndexOf("<a href=\"")+"<a href=\"".Length);
			contacts = contacts.Substring(0,contacts.IndexOf("\""));
			contacts = "http://"+this.dediurl+".libero.it"+contacts;
			
			htmlResponse = httpUtils.GetHttpResponse(contacts,true,"",addurl);

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

					tmpName=(temp_line[4]=="") ? temp_line[3]:temp_line[0];
					tmpName=(temp_line[0]=="" && temp_line[1]=="") ? temp_line[3]:((temp_line[0]!="" && temp_line[1]!="") ? temp_line[0]+" "+temp_line[2]+" "+temp_line[1]:tmpName) ;	
					tmpEmail = (temp_line[5]!="") ? temp_line[5]:(((temp_line[12]!="")? temp_line[12]:((temp_line[13]!="") ? temp_line[13]:"")));
							
					if(tmpEmail!=null)
					{
						if(tmpEmail.IndexOf(":")!=-1)
						{
							tmpEmail=tmpEmail.Substring(tmpEmail.IndexOf(":")+1);
					
						}
					}
					
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
